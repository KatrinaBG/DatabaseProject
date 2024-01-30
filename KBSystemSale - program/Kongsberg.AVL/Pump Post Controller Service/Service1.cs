using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Kongsberg.AVL;
using CANWrapper;
using System.Configuration;
using System.IO;
using System.Xml.Linq;
using System.Collections;

namespace Pump_Post_Controller_Service
{
    public partial class Service1 : ServiceBase
    {
        private ADSWrapper.ADSWrapper adsWrapper;
        private Thread WatchDogThread;
        private CANWrapper.CANWrapper selfTestWrapper;
        private TestStage stage;
        private bool stopHeartBeat;
        private bool stopWatchDog;
        private double currentMax;
        private MMWrapper.MMWrapper multi;
        private bool stopPumpCurrent;
        private int errorTimeout;
        private short sleepLag;
        private short wakeupLag;
        private bool stopPumpThread;
        public Service1()
        {
            InitializeComponent();
        }
        public void Start()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {

            OpenAdsWrapper();
            AddAdsHandlers();
            StartAdsWatchDog();
            OpenCanWrapper();
            AddUdsMappings();
            StartCanListener();
            ConnectToMultimeter();

            StartAdsWatchDog();

            AddAdsMappings();
            StartAdsListener();
        }

        private void StartAdsListener()
        {
            adsWrapper.StartListening();
        }

        private void AddUdsMappings()
        {
            AVLWrapper.AddRxTx((int)TestStage.Ignore, 0xFF, new byte[0], o =>
            {
            });
            
            AVLWrapper.AddRxTx((int)TestStage.PumpWork, UDSFrame.InitOpenValvesConfirmationFrame.Data, o =>
            {
                StartPumpThread();
                    adsWrapper.Write(".bexiCommandDone", true);
            });
            AVLWrapper.AddRxTx((int)TestStage.ReadPressure,7, UDSFrame.ReadPressureReponse, o =>
            {
                var f = o as UDSFrame;
                adsWrapper.Write(".nexoPumpPressure", Convert.ToInt16(String.Format("{0:X2}{1:X2}", f.Data[5], f.Data[6]), 16));
                adsWrapper.Write(".bexiCommandDone",true);
            });

            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, UDSFrame.ClearDTCErrorsConfirmationFrame.Data, o =>
            {
                UDSFrame.OpenWriteSessionFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.ReadSerials, 7, UDSFrame.PullSoftwareVersionResponse, o =>
            {
                var f = o as UDSFrame;
                var decstr = "";
                for (int i = 3; i < f.Data.Length; i++)
                {
                    decstr += Convert.ToChar(f.Data[i]).ToString();
                }

                adsWrapper.Write(".sexiPumpFirmwareVersion", decstr);

                UDSFrame.PullConfigurationVersionFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.ReadSerials, 8, UDSFrame.PullConfigurationVersionResponse, o =>
            {
                var f = o as UDSFrame;
                adsWrapper.Write(".sexiPumpConfig1Version", ((char)f.Data[4]).ToString() + ((char)f.Data[5]).ToString());
                adsWrapper.Write(".sexiPumpConfig2Version", ((char)f.Data[6]).ToString() + ((char)f.Data[7]).ToString());
                UDSFrame.PullPumpSerialNumberFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.ReadSerials, 17, UDSFrame.PullPumpSerialNumberResponse, o =>
            {
                var f = o as UDSFrame;
                var decstr = "";
                for (int i = 3; i < f.Data.Length; i++)
                {
                    decstr += Convert.ToChar(f.Data[i]).ToString();
                }
                adsWrapper.Write(".sexiPumpSerialNumber", decstr);

                adsWrapper.Write(".bexiCommandDone", true);
            });
            
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.OpenVWDiagnosticSessionConfirmationFrame.Data, o =>
            {
                UDSFrame.LoginFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, 6, new byte[2] { 0x67, 0x03 }, o =>
            {
                try
                {
                    var f = o as UDSFrame;
                    using (FileStream fs = new FileStream(ConfigPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                    {
                        var doc = XDocument.Load(new StreamReader(fs));

                        var LOGIN = Convert.ToInt32(doc.Descendants("PARAMETER_DATA").First().Attribute("LOGIN").Value);
                        var inp = String.Format("{0:X2}{1:X2}{2:X2}{3:X2}", f.Data[2], f.Data[3], f.Data[4], f.Data[5]);
                        var a = Convert.ToInt32(inp, 16);
                        var s = String.Format("{0:X8}", a + LOGIN);
                        new UDSFrame()
                        {
                            Data = new byte[6] { 0x27, 0x04, Convert.ToByte(s.Substring(0, 2), 16), Convert.ToByte(s.Substring(2, 2), 16), Convert.ToByte(s.Substring(4, 2), 16), Convert.ToByte(s.Substring(6, 2), 16) }
                        }.Transmit(selfTestWrapper);
                    }
                }
                catch (Exception e)
                {
                    SelfTestInterrupted();
                }
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, new byte[2] { 0x67, 0x04 }, o =>
            {
                UDSFrame.FingerprintFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.FingerprintConfrmationFrame.Data, o =>
            {
                UDSFrame.SetEncodingFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.SetEncodingConfirmationFrame.Data, o =>
            {
                try
                {
                    using (FileStream fs = new FileStream(ConfigPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                    {
                        var doc = XDocument.Load(new StreamReader(fs));

                        var START_ADDRESS = Convert.ToInt32(doc.Descendants("PARAMETER_DATA").First().Attribute("START_ADDRESS").Value, 16);
                        var PARAMETER_DATA = doc.Descendants("PARAMETER_DATA").First().Nodes().OfType<XText>().First().Value.Split(new char[] { ',' });
                        var PARAMETER_DATA_LENGTH = PARAMETER_DATA.Length;

                        var ma = String.Format("{0:X8}", START_ADDRESS);
                        var ms = String.Format("{0:X8}", PARAMETER_DATA_LENGTH);

                        new UDSFrame()
                        {
                            Data = new byte[11] { 0x34, 0x00, 0x44,
                                        Convert.ToByte(ma.Substring(0, 2), 16), Convert.ToByte(ma.Substring(2, 2), 16), Convert.ToByte(ma.Substring(4, 2), 16), Convert.ToByte(ma.Substring(6, 2), 16),
                                        Convert.ToByte(ms.Substring(0, 2), 16), Convert.ToByte(ms.Substring(2, 2), 16), Convert.ToByte(ms.Substring(4, 2), 16), Convert.ToByte(ms.Substring(6, 2), 16),
                            }
                        }.Transmit(selfTestWrapper);
                    }
                }
                catch (Exception)
                {
                    SelfTestInterrupted();
                }
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, 4, new byte[1] { 0x74 }, o =>
            {
                var f = o as UDSFrame;
                try
                {
                    using (FileStream fs = new FileStream(ConfigPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                    {
                        var doc = XDocument.Load(new StreamReader(fs));
                        stage = TestStage.Ignore;
                        var PARAMETER_DATA = doc.Descendants("PARAMETER_DATA").First().Nodes().OfType<XText>().First().Value.Split(new char[] { ',' });
                        var a = Convert.ToByte(String.Format("{0:X2}", f.Data[1])[0].ToString(), 16);
                        var res = "";
                        for (int i = 0; i < a; i++)
                        {
                            res += String.Format("{0:X2}", f.Data[2 + i]);
                        }
                        var l = Convert.ToInt32(res, 16);
                        var d = PARAMETER_DATA;
                        var nf = new UDSFrame();
                        nf.Data = new byte[l];
                        nf.Data[0] = 0x36;
                        nf.Data[1] = 0x01;
                        nf.FillerData = 0x55;
                        byte mc = 1;
                        int c = 2;
                        foreach (var item in d)
                        {
                            if (c < nf.Data.Length)
                            {
                                nf.Data[c] = Convert.ToByte(item, 16);
                                c++;
                            }
                            else
                            {
                                mc++;
                                nf.Transmit(selfTestWrapper);
                                Thread.Sleep(50);
                                var sent = (l - 2) * (mc - 1);
                                var left = d.Length - sent;
                                var flength = left < (l - 2) ? left + 2 : l;
                                nf = new UDSFrame();
                                nf.Data = new byte[flength];
                                nf.Data[0] = 0x36;
                                nf.Data[1] = mc;
                                nf.Data[2] = Convert.ToByte(item, 16);
                                nf.FillerData = 0x55;
                                c = 3;
                            }
                        }
                        nf.Transmit(selfTestWrapper);

                        Thread.Sleep(200);
                        stage = TestStage.UploadConfig;
                        UDSFrame.FinishTransferFrame.Transmit(selfTestWrapper);
                        //return null;
                    }
                }
                catch (Exception)
                {
                    SelfTestInterrupted();
                }
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.FinishTransferConfirmationFrame.Data, o =>
            {
                UDSFrame.ActivateConfigurationFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.ActivateConfigurationConfirmtionFrame.Data, o =>
            {
                UDSFrame.VerifyActivateConfigurationFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.VerifyActivateConfigurationConfirmtionFrame.Data, o =>
            {
                UDSFrame.ZDC1Frame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.VerifyNoneActivatedConfigurationConfirmtionFrame.Data, o =>
            {
                adsWrapper.Write(".bFirmwareError", true);
                adsWrapper.Write(".bexiCommandError", true);
                EndSelfTest();
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.ZDC1ConfirmationFrame.Data, o =>
            {
                UDSFrame.ZDC2Frame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.ZDC2ConfirmationFrame.Data, o =>
            {
                UDSFrame.ZDC3Frame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.ZDC3ConfirmationFrame.Data, o =>
            {
                StopErrorWatchThread();
                UDSFrame.ResetECUFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.UploadConfig, UDSFrame.ResetECUConfirmationFrame.Data, o =>
            {

                new Thread(new ThreadStart(() =>
                {
                    Thread.Sleep(WakeupLag);
                    adsWrapper.Write(".bexiCommandDone", true);
                })).Start();
            });

            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, UDSFrame.OpenWriteSessionConfirmationFrame.Data, o =>
            {
                UDSFrame.FingerprintFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, UDSFrame.FingerprintConfrmationFrame.Data, o =>
            {
                UDSFrame.SetEncodingFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, UDSFrame.SetEncodingConfirmationFrame.Data, o =>
            {
                UDSFrame.OpenVWDiagonosticSessionFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, UDSFrame.OpenVWDiagnosticSessionConfirmationFrame.Data, o =>
            {
                UDSFrame.PullProofBitsFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, 5, UDSFrame.PullProofBitsResponse, o =>
            {
                var f = o as UDSFrame;
                byte clearByte = 252;
                byte bit = 3;
                if (!IsValid)
                {
                    bit = 1;
                }
                new UDSFrame()
                {
                    Data = new byte[5] { 0x2E, 0x05, 0x05, f.Data[3], Convert.ToByte(bit | (f.Data[4] & clearByte)) },
                    FillerData = 0x55
                }.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, UDSFrame.SetProofBitsConfirmationFrame.Data, o =>
            {
                UDSFrame.SetDefaultLocalizationFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, UDSFrame.SetDefaultLocalizationConfirmationFrame.Data, o =>
            {
                UDSFrame.VerifyDefaultLocalizationFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, UDSFrame.VerifyDefaultLocalizationConfirmationFrame.Data, o =>
            {
                UDSFrame.OpenDefaultSessionFrame.Transmit(selfTestWrapper);
            });
            AVLWrapper.AddRxTx((int)TestStage.SetProofBits, UDSFrame.OpenDefaultSessionConfirmationFrame.Data, o =>
            {
                StopErrorWatchThread();
                StopHeartBeatThread();
                //UDSFrame.ResetECUFrame.Transmit(selfTestWrapper);
                //selfTestWrapper.StopTransmittingSignals();
                //bool stopSleep = false;
                selfTestWrapper.StopTransmittingSignals();
                Thread.Sleep(2000);
                new Thread(new ThreadStart(() =>
                {
                    int i = 0;
                    while (i < 3)
                    {
                        selfTestWrapper.Transmit(new CANFrame
                        {
                            Data =
                               new byte[4] { 0x00, 0x00, 0x00, 0x00 },
                            AddrId = 0x3C0
                        });
                        Thread.Sleep(500);
                        i++;
                    }
                    selfTestWrapper.Transmit(CANFrame.InstantSleepFrame);
                    EndSelfTest();
                    new Thread(new ThreadStart(() =>
                    {
                        Thread.Sleep(7500);
                        adsWrapper.Write(".bexiCommandDone", true);
                    })).Start();
                })).Start();
                //stopSleep = true;
                //TransmitInstantSleepFrame();
            });

        }

        private void TransmitInstantSleepFrame()
        {
            selfTestWrapper.Transmit(CANFrame.InstantSleepFrame);
        }
        private void StartPumpThread()
        {
            stopPumpThread = false;
            new Thread(new ThreadStart(() =>
            {
                while (!stopPumpThread)
                {
                    Thread.Sleep(1800);
                    new UDSFrame()
                    {
                        Data = new byte[8] { 0x2F, 0x02, 0xE2, 0x03, Convert.ToByte(PumpPower), 0x80, 0x00, 0x00 },
                        FillerData = 0x55
                    }.Transmit(selfTestWrapper);
                    Thread.Sleep(1800);
                    new UDSFrame()
                    {
                        Data = new byte[8] { 0x2F, 0x02, 0xE2, 0x03, Convert.ToByte(PumpPower), 0x40, 0x00, 0x00 },
                        FillerData = 0x55
                    }.Transmit(selfTestWrapper);
                }
            })).Start();
            StopErrorWatchThread();
        }

        private void SelfTestInterrupted()
        {
            adsWrapper.Write(".bexiCommandError", true);
            adsWrapper.Write(".bexiSelfTestInterrupted", true);
            EndSelfTest();
        }
        public void ErrorWatch()
        {
            int i = 0;
            int x = 20;
            var timeout = ErrorTimeout / x;
            while (i < x && !ErrorWatchHandled)
            {
                Thread.Sleep(timeout);
                i++;
            }
            if (!ErrorWatchHandled)
            {
                SelfTestInterrupted();
            }
            else
            {
            }
        }

        private void StopErrorWatchThread()
        {
            ErrorWatchHandled = true;
        }

        private void StopCurrentThread()
        {
            //throw new NotImplementedException();
        }

        private void HeartBeat()
        {
            while (!stopHeartBeat)
            {
                UDSFrame.HeartBeatFrame.Transmit(selfTestWrapper);
                Thread.Sleep(2000);
            }
        }

        private void StopHeartBeatThread()
        {
            stopHeartBeat = true;
        }

        private void StartHearBeatThread()
        {
            stopHeartBeat = false;
            new Thread(new ThreadStart(HeartBeat)).Start();
        }

        private void AddAdsMappings()
        {
            adsWrapper.AddAction(".bexoRestart", o =>
            {
                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoRestart", false);
                    adsWrapper.Write(".bexiSelfTestInterrupted", true);
                    EndSelfTest();
                }
            });
            adsWrapper.AddAction(".bexoIgnitionStart", o =>
            {
                pumpPower = default(short);
                errorTimeout = default(int);
                sleepLag = default(short);
                wakeupLag = default(short);
                var val = (bool)o;
                if (val)
                {
                    selfTestWrapper.ClearAllSignals();
                    if (IsC7)
                    {
                        selfTestWrapper.ResponderId = 0x79F;
                        selfTestWrapper.SenderId = 0x735;

                        selfTestWrapper.AddSimpleSignal(SimpleSignal.C7_Charisma_03);
                        selfTestWrapper.AddSimpleSignal(SimpleSignal.C7_MEM_FS_01);
                        selfTestWrapper.AddSimpleSignal(SimpleSignal.C7_MEM_FS_02);
                        selfTestWrapper.AddSimpleSignal(SimpleSignal.C7_TSG_FT_01);

                    }
                    else
                    {
                        selfTestWrapper.ResponderId = 0x7A2;
                        selfTestWrapper.SenderId = 0x738;

                        selfTestWrapper.AddSimpleSignal(SimpleSignal.T99_MEM_BFS_Fond_01);
                        selfTestWrapper.AddSimpleSignal(SimpleSignal.T99_MEM_FS_01);
                        selfTestWrapper.AddSimpleSignal(SimpleSignal.T99_TSG_HL_01);
                        selfTestWrapper.AddSimpleSignal(SimpleSignal.T99_TSG_HR_01);
                    }

                    selfTestWrapper.AddSimpleSignal(SimpleSignal.BCM1_02);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.BCM2_02);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.BEM_02);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.Diagnose_01);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.Gateway_01);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.Klemmen_Status_01);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.Klima_03);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.Motor_07);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.Motor_K_02);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_AAG);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_ALM);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_Armlehne);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_BCM1);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_BCM2);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_Gateway);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_HDSG);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_IVB_BFS);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_IVB_BFS_Fond);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_IVB_FS);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_IVB_FS_Fond);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_MEM_BF);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_MEM_BFS_Fond);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_MEM_F);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_MEM_FS_Fond);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_MFG);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_STS);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_TSG_BT);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_TSG_FT);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_TSG_HL);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_TSG_HR);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.NMH_VSG);
                    selfTestWrapper.AddSimpleSignal(SimpleSignal.RGS_VL_01);

                    stage = TestStage.Initial;
                    adsWrapper.Write(".bexoIgnitionStart", false);
                    StartIgnitionThread();
                    Thread.Sleep(WakeupLag);
                    adsWrapper.Write(".bexiCommandDone", true);
                }
            });
        //adsWrapper.AddAction(".bexoIgnitionStop", o =>
        //{
        //    var val = (bool)o;
        //    if (val)
        //    {
        //        adsWrapper.Write(".bexoIgnitionStop", false);
        //        StopIgnitionThread();

        //        adsWrapper.Write(".bexiCommandDone", true);

        //    }
        //});
            adsWrapper.AddAction(".bexoReadSerialNumbers", o =>
            {
                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoReadSerialNumbers", false);
                    adsWrapper.Write(".bexiCommandDone", false);
                    stage = TestStage.ReadSerials;
                    StartErrorWatchThread();
                    UDSFrame.PullSoftwareVersionFrame.Transmit(selfTestWrapper);
                }
            });
            adsWrapper.AddAction(".bexoUploadConfig", o =>
            {
                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoUploadConfig", false);
                    adsWrapper.Write(".bexiCommandDone", false);
                    stage = TestStage.UploadConfig;
                    StartErrorWatchThread();
                    UDSFrame.OpenVWDiagonosticSessionFrame.Transmit(selfTestWrapper);
                }
            });
            adsWrapper.AddAction(".bexoSetProofBits", o =>
            {
                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoSetProofBits", false);
                    adsWrapper.Write(".bexiCommandDone", false);
                    stage = TestStage.SetProofBits;
                    StartErrorWatchThread();
                    UDSFrame.ClearDTCErrorsFrame.Transmit(selfTestWrapper);
                }
            });
            adsWrapper.AddAction(".bexoReadLoadedPumpCurrent", o =>
            {
                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoReadLoadedPumpCurrent", false);

                    adsWrapper.Write(".rexiLoadedPumpCurrent", currentMax);
                    adsWrapper.Write(".bexiCommandDone", true);
                }
            });
            adsWrapper.AddAction(".bexoReadPostSleepPumpCurrent", o =>
            {

                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoReadPostSleepPumpCurrent", false);

                    multi.Measure();
                    Thread.Sleep(6000);
                    multi.Measure();
                    adsWrapper.Write(".rexiPostSleepPumpCurrent", multi.Measure());
                    adsWrapper.Write(".bexiCommandDone", true);
                }
            });
            adsWrapper.AddAction(".bexoReadPreSleepPumpCurrent", o =>
            {

                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoReadPreSleepPumpCurrent", false);

                    multi.Measure();
                    Thread.Sleep(6000);
                    multi.Measure();
                    adsWrapper.Write(".rexiPreSleepPumpCurrent", multi.Measure());
                    adsWrapper.Write(".bexiCommandDone", true);
                }
            });
            adsWrapper.AddAction(".bexoPumpStart", o =>
            {
                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoPumpStart", false);
                    StartPumpCurrentThread();
                    StartErrorWatchThread();
                    stage = TestStage.PumpWork;
                    UDSFrame.InitOpenValvesFrame.Transmit(selfTestWrapper);
                    adsWrapper.Write(".bexiCommandDone", true);
                }
            });
            adsWrapper.AddAction(".bexoPumpStop", o =>
            {
                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoPumpStop", false);
                    stopPumpThread= true;
                    Thread.Sleep(2000);
                    adsWrapper.Write(".bexiCommandDone", true);
                }
            });
            adsWrapper.AddAction(".bexoReadPressure", o =>
            {
                var val = (bool)o;
                if (val)
                {
                    adsWrapper.Write(".bexoReadPressure", false);
                    stage=TestStage.ReadPressure;
                    UDSFrame.ReadPressureFrame.Transmit(selfTestWrapper);
                }
            });
        }

        private void StartPumpCurrentThread()
        {
            stopPumpCurrent = false;
            new Thread(new ThreadStart(PumpCurrentThread)).Start();
        }
        private void StopPumpCurrentThread()
        {
            stopPumpCurrent = true;
        }

        private void PumpCurrentThread()
        {
            while (!stopPumpCurrent)
            {
                currentMax = Math.Max(multi.Measure(), currentMax);
                Thread.Sleep(PumpCurrentSamplingRate);
            }
        }

        private void StartErrorWatchThread()
        {
            ErrorWatchHandled = false;
            new Thread(new ThreadStart(ErrorWatch)).Start();
        }

        private void StopIgnitionThread()
        {
            selfTestWrapper.StopTransmittingSignals();
            //Thread.Sleep(100);
            //selfTestWrapper.Transmit(CANFrame.StopIgnitionFrame);
        }

        private void StartIgnitionThread()
        {
            selfTestWrapper.StartTransmittingSignals();
        }

        private void ConnectToMultimeter()
        {
            multi = new MMWrapper.MMWrapper(ConfigurationSettings.AppSettings["Multimeter"].ToString());
        }

        private void StartCanListener()
        {
            selfTestWrapper.StartRxThread();
        }

        private void OpenCanWrapper()
        {
            selfTestWrapper = new CANWrapper.CANWrapper("SelfTestService");
            selfTestWrapper.FillerByte = 0x55;

            selfTestWrapper.UDSFrameReceived += new UDSFrameReceivedEventHandler(wrapper_UDSFrameReceived);
        }
        void wrapper_UDSFrameReceived(object sender, UDSFrameEventArgs e)
        {
            try
            {
                if (stage == TestStage.Ignore)
                {
                    return;
                }

                var path = new byte[e.Frame.Data.Length + 2];
                path[0] = (byte)stage;
                path[1] = (byte)e.Frame.Data.Length;
                for (int i = 0; i < e.Frame.Data.Length; i++)
                {
                    path[i + 2] = e.Frame.Data[i];
                }
                if (stage == TestStage.PullDTC)
                {
                    path = new byte[2] { (byte)stage, 0xFF };
                }
                var val = AVLWrapper.RxTxTree.FindNode(path);
                if (val.Leaf.Value is Action<object>)
                {
                    ((Action<object>)val.Leaf.Value).Invoke(e.Frame);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void AddAdsHandlers()
        {
            adsWrapper.AddHandle(".bexoIgnitionStart");
            //adsWrapper.AddHandle(".bexoIgnitionStop");
            adsWrapper.AddHandle(".bexoPumpStart");
            adsWrapper.AddHandle(".bexoPumpStop");
            adsWrapper.AddHandle(".bexoReadSerialNumbers");
            adsWrapper.AddHandle(".bexoRestart");
            adsWrapper.AddHandle(".bexoReadPreSleepPumpCurrent");
            adsWrapper.AddHandle(".bexoReadPostSleepPumpCurrent");
            adsWrapper.AddHandle(".bexoReadLoadedPumpCurrent");
            adsWrapper.AddHandle(".rexiPreSleepPumpCurrent");
            adsWrapper.AddHandle(".rexiPostSleepPumpCurrent");
            adsWrapper.AddHandle(".rexiLoadedPumpCurrent");
            adsWrapper.AddHandle(".bexiCommandDone");
            adsWrapper.AddHandle(".bexiCommandError");
            adsWrapper.AddHandle(".bexiSPCBlinker");
            adsWrapper.AddHandle(".sexiPumpSerialNumber");
            adsWrapper.AddHandle(".sexiPumpFirmwareVersion");
            adsWrapper.AddHandle(".sexiPumpConfig1Version");
            adsWrapper.AddHandle(".sexiPumpConfig2Version");
            adsWrapper.AddHandle(".nexoSleepLag");
            adsWrapper.AddHandle(".nexoWakeupLag");
            adsWrapper.AddHandle(".nexoPumpCurrentSamplingRate");
            adsWrapper.AddHandle(".nexoErrorTimeout");
            adsWrapper.AddHandle(".bexoC7");
            //adsWrapper.AddHandle(".bexoC7MassageEnabled");
            //adsWrapper.AddHandle(".bexiSelfTestInterrupted");
            adsWrapper.AddHandle(".bexoUploadConfig");
            adsWrapper.AddHandle(".sexoConfigPath", 255);
            adsWrapper.AddHandle(".bexoSetProofBits");
            adsWrapper.AddHandle(".bTestNOKPaluch");

            adsWrapper.AddHandle(".nexoPumpPower");
            adsWrapper.AddHandle(".bexoReadPressure");
            adsWrapper.AddHandle(".nexoPumpPressure");
            adsWrapper.AddHandle(".bFirmwareError");
        }

        private void StartAdsWatchDog()
        {
            stopWatchDog = false;
            new Thread(new ThreadStart(WatchDog)).Start();
        }
        private void WatchDog()
        {
            while (!stopWatchDog)
            {
                try
                {
                    adsWrapper.Write(".bexiSPCBlinker", !(bool)adsWrapper.Read(".bexiSPCBlinker"));
                    Thread.Sleep(700);
                }
                catch (Exception) { }
            }
        }

        private void OpenAdsWrapper()
        {
            adsWrapper = new ADSWrapper.ADSWrapper();
        }

        protected override void OnStop()
        {
            EndSelfTest();
            StopAdsWatchDog();
            CloseAdsWrapper();
            CloseCanWrapper();

            multi.Close();
        }

        private void CloseCanWrapper()
        {
            selfTestWrapper.AbortRxThread();
            selfTestWrapper.Close();
        }

        private void CloseAdsWrapper()
        {
            adsWrapper.Close();
        }

        private void StopAdsWatchDog()
        {
            stopWatchDog = true;
        }

        private void EndSelfTest()
        {
            stopPumpThread = true;
            StopErrorWatchThread();
            StopPumpCurrentThread();
            selfTestWrapper.StopTransmittingSignals();
            StopHeartBeatThread();
            StopIgnitionThread();

            StopCurrentThread();
            StopSelfTestWrapperRx();
        }

        private void StopSelfTestWrapperRx()
        {
            //selfTestWrapper.AbortRxThread();
        }

        public bool IsC7
        {
            get
            {
                return (bool)adsWrapper.Read(".bexoC7");
            }
        }

        public int WakeupLag
        {
            get
            {
                if (wakeupLag == default(int))
                {
                    wakeupLag = (Int16)adsWrapper.Read(".nexoWakeupLag");
                }
                return wakeupLag;
            }
        }

        public bool HasMassage
        {
            get
            {
                return !IsC7 || (bool)adsWrapper.Read(".bexoC7MassageEnabled");
            }
        }

        public string ConfigPath
        {
            get
            {
                return (string)adsWrapper.Read(".sexoConfigPath");
            }
        }

        public bool IsValid
        {
            get
            {
                return !(bool)adsWrapper.Read(".bTestNOKPaluch");
            }
        }

        private Int16 pumpPower;

        public Int16 PumpPower
        {
            get
            {
                if (pumpPower == default(Int16))
                {
                    pumpPower = (Int16)adsWrapper.Read(".nexoPumpPower");
                } return pumpPower;
            }
        }


        public short SleepLag
        {
            get
            {
                if (sleepLag == default(short))
                {
                    sleepLag = (short)adsWrapper.Read(".nexoSleepLag");
                }
                return sleepLag;
            }
        }

        public bool ErrorWatchHandled { get; set; }

        public int ErrorTimeout
        {
            get
            {
                if (errorTimeout == default(int))
                {
                    errorTimeout = (short)adsWrapper.Read(".nexoErrorTimeout");
                }
                return errorTimeout;
            }
        }

        public short PumpCurrentSamplingRate
        {
            get
            {
                return (short)adsWrapper.Read(".nexoPumpCurrentSamplingRate");
            }
        }
    }
}
