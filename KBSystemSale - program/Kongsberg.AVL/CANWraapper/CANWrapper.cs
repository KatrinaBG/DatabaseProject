using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using vxlapi_NET20;
using System.Threading;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace CANWrapper
{
    /// <summary>
    /// Delegat dla odbioru ramek
    /// </summary>
    /// <param name="sender">obiekt wystawiający zdarzenie</param>
    /// <param name="e">dane zdarzenia</param>
    public delegate void CANFrameReceivedEventHandler(object sender, CANFrameEventArgs e);
    public delegate void UDSFrameReceivedEventHandler(object sender, UDSFrameEventArgs e);
    public delegate void ErrorFrameReceivedHandler(object sender, EventArgs e);

    /// <summary>
    /// obiekt opakowujący sterownika Vector CAN XL
    /// </summary>
    /// 
    public class CANWrapper
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WaitForSingleObject(int handle, int timeOut);

        /// <summary>
        /// sterownik Vector CAN XL
        /// </summary>
        private XLDriver CANDemo = new XLDriver();
        /// <summary>
        /// konfihuracja sterownika
        /// </summary>
        private XLClass.xl_driver_config driverConfig = new XLClass.xl_driver_config();
        private uint hwType = (uint)XLClass.XLhwTypes.XL_HWTYPE_CANCASEXL;
        private uint hwIndex = 0;
        private uint hwChannel = 0;
        private uint busType = (uint)XLClass.XLbusTypes.XL_BUS_TYPE_CAN;
        private uint flags = 0;
        private int portHandle = -1;
        private int eventHandle = -1;
        private UInt64 accessMask = 0;
        private UInt64 permissionMask = 0;
        private UInt64 txMask = 0;
        private int channelIndex = 0;
        private ArrayList allowedIds = new ArrayList();
        /// <summary>
        /// wątek odbiory ramek
        /// </summary>
        private Thread rxThread;
        private uint appChannel = 0;
        private Dictionary<int, CANFrame[]> frames = new Dictionary<int, CANFrame[]>();
        private enum WaitResults : int
        {
            WAIT_OBJECT_0 = 0x0,
            WAIT_ABANDONED = 0x80,
            WAIT_TIMEOUT = 0x102,
            INFINITE = 0xFFFF,
            WAIT_FAILED = 0xFFFFFFF
        }

        /// <summary>
        /// konstruktor wrappera nawiązujący połączenie z urządzeniem
        /// </summary>
        /// <param name="name">nazwa aplikacji korzystającej z wrappera</param>
        /// 
        public CANWrapper(string name)
        {
            signals = new List<SimpleSignal>();
            AppName = name;
            if (CANDemo.XL_OpenDriver() == XLClass.XLstatus.XL_ERR_DLL_NOT_FOUND)
            {
                throw new Exception(String.Format("Brak dll w {0}", Path.GetFullPath(".")));
            }
            CANDemo.XL_GetDriverConfig(ref driverConfig);

            if ((CANDemo.XL_GetApplConfig(AppName, appChannel, ref hwType, ref hwIndex, ref hwChannel, busType) != XLClass.XLstatus.XL_SUCCESS))
            {
                CANDemo.XL_SetApplConfig(AppName, appChannel, hwType, hwIndex, hwChannel, busType);
                throw new Exception(String.Format("Dodaj konfigurację dla {0}", AppName));
            }
            else
            {
                var c = CANDemo.XL_GetApplConfig(AppName, appChannel, ref hwType, ref hwIndex, ref hwChannel, busType);
                accessMask = CANDemo.XL_GetChannelMask((int)hwType, (int)hwIndex, (int)hwChannel);
                txMask = accessMask;
                permissionMask = accessMask;

                CANDemo.XL_OpenPort(ref portHandle, AppName, accessMask, ref permissionMask, 1024, busType);
                CANDemo.XL_CanRequestChipState(portHandle, accessMask);
                CANDemo.XL_ActivateChannel(portHandle, accessMask, busType, flags);
                CANDemo.XL_SetNotification(portHandle, ref eventHandle, 0);
                CANDemo.XL_ResetClock(portHandle);

                rxThread = new Thread(new ThreadStart(RXThread));
            }
        }


        /// <summary>
        /// funkacja odczytu ramek uruchamiana w wątku
        /// </summary>
        private void RXThread()
        {
            XLClass.xl_event receivedEvent = new XLClass.xl_event();            // create new object containing received data       
            XLClass.XLstatus xlStatus = XLClass.XLstatus.XL_SUCCESS;       // result of XL driver func. calls
            WaitResults waitResult = new WaitResults();                 // result values of WaitForSingleObject          

            // note: this thread is destroyed by MAIN
            while (!end)
            {
                // wait for hardware events
                waitResult = (WaitResults)WaitForSingleObject(eventHandle, 100);
                // if event occured...
                if (waitResult != WaitResults.WAIT_TIMEOUT)
                {
                    // ...init xlStatus first
                    xlStatus = XLClass.XLstatus.XL_SUCCESS;

                    // afterwards: while hw queue is not empty...
                    while (xlStatus != XLClass.XLstatus.XL_ERR_QUEUE_IS_EMPTY && !end)
                    {
                        // ...reveice data from hardware...
                        xlStatus = CANDemo.XL_Receive(portHandle, ref receivedEvent);
                        if (ResponderId!=receivedEvent.tagData.can_Msg.id)
                        {
                            continue;
                        }
                        //  if receiving succeed....
                        if (xlStatus == XLClass.XLstatus.XL_SUCCESS)
                        {
                            // ...and data was Rx msg...
                            if (receivedEvent.tag == (byte)XLClass.XLeventType.XL_RECEIVE_MSG)
                            {
                                if ((receivedEvent.tagData.can_Msg.flags & (ushort)XLClass.XLmessageFlags.XL_CAN_MSG_FLAG_ERROR_FRAME)
                                       == (ushort)XLClass.XLmessageFlags.XL_CAN_MSG_FLAG_ERROR_FRAME)
                                {

                                    if (ErrorFrameReceived != null)
                                    {
                                        ErrorFrameReceived(this, new EventArgs());
                                    }
                                }
                                else if ((receivedEvent.tagData.can_Msg.flags & (ushort)XLClass.XLmessageFlags.XL_CAN_MSG_FLAG_ERROR_FRAME) != (ushort)XLClass.XLmessageFlags.XL_CAN_MSG_FLAG_ERROR_FRAME &&
                                    (receivedEvent.tagData.can_Msg.flags & (ushort)XLClass.XLmessageFlags.XL_CAN_MSG_FLAG_REMOTE_FRAME) != (ushort)XLClass.XLmessageFlags.XL_CAN_MSG_FLAG_REMOTE_FRAME)
                                {
                                    var f = receivedEvent.tagData.can_Msg;
                                    var id = receivedEvent.tagData.can_Msg.id;
                                    if (id == 0x0735 || id == 0x079F || id == 0x03B6 || id == 0x06D5 ||
                                        id == 0x0736 || id == 0x07A0 || id == 0x03B7 || id == 0x06D6 ||
                                        id == 0x0737 || id == 0x07A1 || id == 0x03B8 || id == 0x06D7 ||
                                        id == 0x0738 || id == 0x07A2 || id == 0x03B9 || id == 0x06D8)
                                    {
                                        OnCANFrameReceived(new CANFrameEventArgs()
                                        {
                                            Frame = new CANFrame(receivedEvent),
                                            Status = xlStatus
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// zdarzenie odebrania ramki
        /// </summary>
        public event CANFrameReceivedEventHandler CANFrameReceived;
        public event UDSFrameReceivedEventHandler UDSFrameReceived;
        public event ErrorFrameReceivedHandler ErrorFrameReceived;
        /// <summary>
        /// flaga dla oczekiwania na odpowiedź
        /// </summary>
        private bool conditionMatched;
        /// <summary>
        /// warunek dla nadchodzącej ramki
        /// </summary>
        private Func<CANFrame, bool> currentCondition;
        private UDSFrame longUDSFrame;
        private int expectedLongFramePartNumber;
        private int mainByteCounter;
        private bool udsConditionMatched;
        private Func<UDSFrame, bool> currentUDSCondition;
        private bool end = false;
        private List<SimpleSignal> signals;

        /// <summary>
        /// wynkcja odpalająca zdarzenia
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCANFrameReceived(CANFrameEventArgs e)
        {
            //Log.Write("???");
            if (ResponderId != default(uint) && e.Frame.AddrId != ResponderId) { return; }
            //Log.Write(" zaczynam budować ramkę UDS");
            if (e.Frame.Data[0] <= 0x07)
            {
                var udsFrame = new UDSFrame();
                udsFrame.Data=new byte[e.Frame.Data[0]];
                udsFrame.AddrId = e.Frame.AddrId;
                for (int i = 1; i <= e.Frame.Data[0]; i++)
                {
                    udsFrame.Data[i - 1] = e.Frame.Data[i];
                }
                OnUDSFrameReceived(new UDSFrameEventArgs() { Frame = udsFrame });
            }
            else if (e.Frame.Data[0] == 0x10)
            {
                longUDSFrame = new UDSFrame();
                longUDSFrame.Data = new byte[e.Frame.Data[1]];
                longUDSFrame.AddrId = e.Frame.AddrId;
                expectedLongFramePartNumber = 0x21;
                mainByteCounter = 0;
                for (int i = 2; i < 8; i++)
                {
                    longUDSFrame.Data[mainByteCounter++] = e.Frame.Data[i];
                }
                if (e.Frame.AddrId != SenderId)
                {
                    ControlFlowTransmit(SenderId);
                }
            }
            else if (e.Frame.Data[0] == expectedLongFramePartNumber)
            {
                for (int i = 1;
                    i < 8 && mainByteCounter < longUDSFrame.Dlc; i++)
                {
                    longUDSFrame.Data[mainByteCounter] = e.Frame.Data[i];
                    mainByteCounter++;
                }
                if (mainByteCounter == longUDSFrame.Dlc)
                {
                    OnUDSFrameReceived(new UDSFrameEventArgs() { Frame = longUDSFrame });
                    longUDSFrame = null;
                    expectedLongFramePartNumber = 0;
                    mainByteCounter = 0;
                }
                else
                {
                    expectedLongFramePartNumber++;
                }
            }
            if (CANFrameReceived != null)
            {
                CANFrameReceived(this, e);
            }
        }

        protected virtual void OnUDSFrameReceived(UDSFrameEventArgs e)
        {
            if (UDSFrameReceived != null)
            {
                UDSFrameReceived(this, e);
            }
        }
        /// <summary>
        /// nazwa aplikacji korzystającej z wrapera
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// funkcja uruchamiająca wątek odczytu
        /// </summary>
        public void StartRxThread()
        {
            if (!rxThread.IsAlive)
            {
                rxThread.Start();
            }
        }
        /// <summary>
        /// funkcja zatzrymująca wątek odczytu
        /// </summary>
        public void AbortRxThread()
        {
            end = true;

            
        }

        /// <summary>
        /// funckaj wysyłajc ramkę CAN
        /// </summary>
        /// <param name="frame">ramka CAN</param>
        public void Transmit(CANFrame frame)
        {
            if (frame.AddrId == default(uint))
            {
                frame.AddrId = SenderId;
            }
            var e = new XLClass.xl_event();
            e.tagData.can_Msg.id = frame.AddrId;
            e.tagData.can_Msg.dlc = (ushort)frame.Dlc;
            for (int i = 0; i < frame.Dlc; i++)
            {
                e.tagData.can_Msg.data[i] = frame.Data[i];
            }
            e.tag = (byte)XLClass.XLeventType.XL_TRANSMIT_MSG;
            CANDemo.XL_CanTransmit(portHandle, accessMask, e);
        }

        /// <summary>
        /// wysłanie ramki kontroli przepływu i oczekiwanie na pierwszą część odpowiedzi
        /// </summary>
        /// <param name="id">id nadawcy</param>
        public void ControlFlowTransmit(uint id)
        {
            var frame = new CANFrame()
            {
                AddrId = id,
                Data = new byte[8] { 0x30, 0x00, 0x00, FillerByte, FillerByte, FillerByte, FillerByte, FillerByte }
            };
            Transmit(frame);
        }
        /// <summary>
        /// transmisja ramki UDS i oczekiwanie na odpowiedź
        /// </summary>
        /// <param name="frame">ramka UDS</param>
        /// <param name="condition">warunek dla nadchodzących odpowiedzi</param>
        public void TransmitAndWaitForRX(UDSFrame frame, Func<CANFrame, bool> condition)
        {
            conditionMatched = false;
            currentCondition = condition;
            var waitHandler = new CANFrameReceivedEventHandler(CANWrapper_FrameReceived);
            CANFrameReceived += waitHandler;
            frame.Transmit(this);
            while (!conditionMatched)
            {
                Thread.Sleep(10);
            }
            CANFrameReceived -= waitHandler;
        }

        public void TransmitAndWaitForUDSRx(UDSFrame frame, Func<UDSFrame, bool> condition)
        {

            udsConditionMatched = false;
            currentUDSCondition = condition;
            var waitHandler = new UDSFrameReceivedEventHandler(CANWrapper_UDSFrameReceived);
            UDSFrameReceived += waitHandler;
            if (frame.Transmit(this))
            {
                while (!udsConditionMatched)
                {
                    Thread.Sleep(10);
                }
            }
            UDSFrameReceived -= waitHandler;
        }

        /// <summary>
        /// handler dla nadchodzących ramek sprawdzający czy można zwolnić oczekiwanie
        /// </summary>
        /// <param name="sender">nadawca zdarzenia</param>
        /// <param name="e">argumenty zdarzenia zawierające ramkę CAN</param>
        void CANWrapper_FrameReceived(object sender, CANFrameEventArgs e)
        {
            if (currentCondition != null)
            {
                if (currentCondition(e.Frame))
                {
                    conditionMatched = true;
                    currentCondition = null;
                }
            }
        }

        void CANWrapper_UDSFrameReceived(object sender, UDSFrameEventArgs e)
        {
            if (currentUDSCondition != null)
            {
                if (currentUDSCondition(e.Frame))
                {
                    udsConditionMatched = true;
                    currentUDSCondition = null;
                }
            }
        }




        public uint SenderId { get; set; }
        public uint ResponderId { get; set; }
        public byte FillerByte { get; set; }

        public void StartTransmittingSignals()
        {
            End = false;
            //foreach (var item in signals)
            //{
            //    new Thread(new ThreadStart(item.Transmit)).Start();
            //}
            foreach (var item in frames)
            {

                new Thread(new ThreadStart(() =>
                {
                    var sleepTime = item.Key-50;
                    var arr = item.Value;
                    while (!End)
                    {
                        foreach (var item2 in arr)
                        {
                            if (!End)
                            {
                                Transmit(item2);
                            }
                        }
                        Thread.Sleep(sleepTime);
                    }
                })).Start();
            }
        }

        public void StopTransmittingSignals()
        {
            End = true;
            
        }

        public void ClearAllSignals()
        {
            signals.Clear();
            frames.Clear();
        }

        public void AddSimpleSignal(SimpleSignal simpleSignal)
        {
            simpleSignal.Wrapper = this;
            simpleSignal.End = false;
            if (!frames.ContainsKey(simpleSignal.Interval))
            {
                frames[simpleSignal.Interval] = new CANFrame[0];
            }
            frames[simpleSignal.Interval] = AddFrameToArray(frames[simpleSignal.Interval], simpleSignal.Frame);//.Add(simpleSignal.Frame);
            signals.Add(simpleSignal);
        }

        private CANFrame[] AddFrameToArray(CANFrame[] cANFrame, CANFrame cANFrame_2)
        {
            var newArr = new CANFrame[cANFrame.Length + 1];
            int i =0;
            for (i = 0; i < cANFrame.Length; i++)
            {
                newArr[i] = cANFrame[i];
            }
            newArr[i] = cANFrame_2;
            return newArr;
        }

        public void AddSimpleSignal(ushort addrId, byte[] data, int interval)
        {
            AddSimpleSignal(new SimpleSignal()
            {
                AddrId = addrId,
                Data = data,
                Interval = interval
            });
        }


        public void Close()
        {
            CANDemo.XL_ClosePort(portHandle);
            CANDemo.XL_CloseDriver();
        }

        public bool End { get; set; }
    }
}
