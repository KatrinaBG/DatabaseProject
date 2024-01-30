using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CANWrapper
{
    public class UDSFrame
    {

        public static UDSFrame StartSelfTestFrame = new UDSFrame()
        {
            Data = new byte[8] { 0x2F, 0x02, 0x49, 0x03, 0x00, 0x00, 0x00, 0x00 },
            FillerData = 0x55
        };
        public static UDSFrame InitSelfTestConfirmationFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x6F, 0x02, 0x49, 0x02 },
            FillerData = 0xAA
        };
        public static UDSFrame StartSelfTestConfirmationFrame = new UDSFrame()
        {
            Data = new byte[8] { 0x6F, 0x02, 0x49, 0x03, 0x00, 0x00, 0x00, 0x00 },
            FillerData=0xAA
        };
        public static UDSFrame SelfTestNotStartedFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x62, 0x01, 0x00, 0x00 },
            FillerData = 0xAA
        };
        public static UDSFrame HeartBeatFrame = new UDSFrame()
        {
            Data=new byte[3] {0x22,0x01,0x00},
            FillerData=0x55
        };
        public static UDSFrame SelfTestOngoingFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x62, 0x01, 0x00, 0xC0 },
            FillerData = 0xAA
        };
        public static UDSFrame SelfTestFailSafeFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x62, 0x01, 0x00, 0x40 },
            FillerData = 0xAA
        };
        public static UDSFrame SelfTestFinishedFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x62, 0x01, 0x00, 0x80 },
            FillerData=0xAA
        };
        public static UDSFrame InitOpenValvesFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x2F, 0x02, 0xE2, 0x02 },
            FillerData = 0x55
        };
        public static UDSFrame InitOpenValvesConfirmationFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x6F, 0x02, 0xE2, 0x02 },
            FillerData = 0xAA
        };
        public static UDSFrame OpenC7ValvesFrame = new UDSFrame()
        {
            Data = new byte[8] { 0x2F, 0x02, 0xE2, 0x03, 0x00, 0x00, 0x2A, 0xAA },
            FillerData = 0x55
        };
        public static UDSFrame OpenT99ValvesFrame = new UDSFrame()
        {
            Data = new byte[8] { 0x2F, 0x02, 0xE2, 0x03, 0x00, 0x00, 0x00, 0x2A },
            FillerData = 0x55
        };
        public static UDSFrame OpenC7ValvesConfirmationFrame = new UDSFrame()
        {
            Data = new byte[8] { 0x6F, 0x02, 0xE2, 0x03, 0x00, 0x00, 0x2A , 0xAA},
            FillerData = 0xAA
        };
        public static UDSFrame OpenT99ValvesConfirmationFrame = new UDSFrame()
        {
            Data = new byte[8] { 0x6F, 0x02, 0xE2, 0x03, 0x00, 0x00, 0x00,0x2A },
            FillerData = 0xAA
        };
        public static UDSFrame PullPressureValuesFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0x10, 0x18 },
            FillerData = 0x55
        };
        public static UDSFrame PullTimeValuesFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0x10, 0x19 },
            FillerData = 0x55
        };
        public static UDSFrame PullErrorValuesFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0x10, 0x1A },
            FillerData = 0x55
        };
        public static UDSFrame PullDTCErrorValues = new UDSFrame()
        {
            Data = new byte[3] { 0x19, 0x02, 0x0C },
            FillerData = 0x55
        };
        public static UDSFrame ClearDTCErrorsFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x14, 0xFF, 0xFF, 0xFF },
            FillerData = 0x55
        };
        public static UDSFrame ClearDTCErrorsConfirmationFrame = new UDSFrame()
        {
            Data = new byte[1] { 0x54 },
            FillerData = 0xAA
        };
        public static UDSFrame PullSoftwareVersionFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0xF1, 0x89 },
            FillerData = 0x55
        };
        public static UDSFrame PullConfigurationVersionFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0xF1, 0x82 },
            FillerData = 0x55
        };
        public static UDSFrame PullPumpSerialNumberFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0xF1, 0x8C },
            FillerData = 0x55
        };
        public static UDSFrame PullFstValveBlockSerialNumberFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0x6A, 0xB5 },
            FillerData = 0x55
        };
        public static UDSFrame PullSndValveBlockSerialNumberFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0x6A, 0xB6},
            FillerData = 0x55
        };
        public static UDSFrame PullThdValveBlockSerialNumberFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0x6A, 0xB7 },
            FillerData = 0x55
        };
        public static UDSFrame OpenWriteSessionFrame = new UDSFrame()
        {
            Data = new byte[2] { 0x10, 0x03 },
            FillerData = 0x55
        };
        public static UDSFrame OpenWriteSessionConfirmationFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x50, 0x03, 0x00, 0x32, 0x01, 0xF4 },
            FillerData = 0xAA
        };
        public static UDSFrame SetEncodingFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x2E, 0xF1, 0x99, 0x11, 0x04,0x05 },
            FillerData = 0x55
        };
        public static UDSFrame SetEncodingConfirmationFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x6E, 0xF1, 0x99 },
            FillerData = 0xAA
        };
        public static UDSFrame FingerprintFrame = new UDSFrame()
        {
            Data = new byte[9] { 0x2E, 0xF1, 0x98, 0x04, 0x91, 0xD0, 0x02, 0x00, 0x01 },
            FillerData = 0x55
        };
        public static UDSFrame FingerprintConfrmationFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x6E, 0xF1, 0x98},
            FillerData = 0xAA
        };
        public static UDSFrame NewT99SettingsFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x2E, 0x06, 0x00, 0x10, 0x00, 0x02 },
            FillerData = 0x55
        };
        public static UDSFrame NewC7WithMassageSettingsFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x2E, 0x06, 0x00, 0x16, 0x00, 0x00 },
            FillerData = 0x55
        };
        public static UDSFrame NewC7WithoutMassageSettingsFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x2E, 0x06, 0x00, 0x06, 0x00, 0x00 },
            FillerData = 0x55
        };
        public static UDSFrame NewT99SettingsConfirmationFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x62, 0x06, 0x00, 0x10, 0x00, 0x02 },
            FillerData = 0xAA
        };
        public static UDSFrame NewC7WithMassageSettingsConfirmationFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x62, 0x06, 0x00, 0x16, 0x00, 0x00 },
            FillerData = 0xAA
        };
        public static UDSFrame NewC7WithoutMassageSettingsConfirmationFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x62, 0x06, 0x00, 0x06, 0x00, 0x00 },
            FillerData = 0xAA
        };
        public static UDSFrame NewSettingsConfirmationFrame = new UDSFrame()
        {
            Data = new byte[3]{0x6E,0x06,0x00},
            FillerData=0xAA
        };
        public static UDSFrame NewSettingsFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0x06, 0x00 },
            FillerData = 0x55
        };
        public static UDSFrame OpenDefaultSessionFrame = new UDSFrame()
        {
            Data = new byte[2] { 0x10, 0x01 },
            FillerData = 0x55
        };
        public static UDSFrame OpenDefaultSessionConfirmationFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x50, 0x01, 0x00, 0x32, 0x01, 0xF4 },
            FillerData = 0xAA
        };
        public static UDSFrame InitSelfTestFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x2F, 0x02, 0x49, 0x02 },
            FillerData = 0x55
        };
        public static UDSFrame OpenVWDiagonosticSessionFrame = new UDSFrame()
        {
            Data = new byte[2] { 0x10, 0x40 },
            FillerData = 0x55
        };
        public static UDSFrame OpenVWDiagnosticSessionConfirmationFrame = new UDSFrame()
        {
            Data = new byte[6] { 0x50, 0x40, 0x00, 0x32, 0x01, 0xF4 },
            FillerData = 0xAA
        };
        public static UDSFrame LoginFrame = new UDSFrame()
        {
            Data = new byte[2] { 0x27, 0x03 },
            FillerData = 0x55
        };
        public static UDSFrame FinishTransferFrame = new UDSFrame()
        {
            Data = new byte[1] { 0x37 },
            FillerData = 0x55
        };
        public static UDSFrame FinishTransferConfirmationFrame = new UDSFrame()
        {
            Data = new byte[1] { 0x77 },
            FillerData = 0xAA
        };
        public static UDSFrame ActivateConfigurationFrame = new UDSFrame()
        {
            Data = new byte[7] { 0x31, 0x01, 0x02, 0xEF, 0x03, 0x01, 0x00 },
            FillerData = 0x055
        };
        public static UDSFrame ActivateConfigurationConfirmtionFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x71, 0x01, 0x02, 0xEF },
            FillerData = 0x0AA
        };
        public static UDSFrame VerifyActivateConfigurationFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x31, 0x03, 0x02, 0xEF },
            FillerData = 0x055
        };
        public static UDSFrame VerifyActivateConfigurationConfirmtionFrame = new UDSFrame()
        {
            Data = new byte[7] { 0x71, 0x03, 0x02, 0xEF, 0x02, 0xFF, 0xFF },
            FillerData = 0x0AA
        };
        public static UDSFrame VerifyNoneActivatedConfigurationConfirmtionFrame = new UDSFrame()
        {
            Data = new byte[7] { 0x71, 0x03, 0x02, 0xEF, 0x02, 0x00, 0x00 },
            FillerData = 0x0AA
        };
        public static UDSFrame ResetECUFrame = new UDSFrame()
        {
            Data = new byte[2] { 0x11,0x02 },
            FillerData = 0x055
        };
        public static UDSFrame ResetECUConfirmationFrame = new UDSFrame()
        {
            Data = new byte[2] { 0x51, 0x02},
            FillerData = 0x0AA
        };
        public static UDSFrame PullProofBitsFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0x05, 0x05 },
            FillerData = 0x55
        };
        public static UDSFrame SetProofBitsConfirmationFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x6E, 0x05, 0x05 },
            FillerData = 0xAA
        };
        public static UDSFrame SetDefaultLocalizationFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x2E, 0x28, 0xC6, 0xFF },
            FillerData = 0x55
        };
        public static UDSFrame SetDefaultLocalizationConfirmationFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x6E, 0x28, 0xC6 },
            FillerData = 0xAA
        };
        public static UDSFrame VerifyDefaultLocalizationFrame = new UDSFrame()
        {
            Data = new byte[3] { 0x22, 0x28, 0xC6 },
            FillerData = 0x55
        };
        public static UDSFrame VerifyDefaultLocalizationConfirmationFrame = new UDSFrame()
        {
            Data = new byte[4] { 0x62, 0x28, 0xC6,0xFF},
            FillerData = 0xAA
        };
        public static UDSFrame ZDC1Frame = new UDSFrame
        {
            Data = new byte[0x0E] { 0x2E, 0xF1, 0xA0, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D, 0x2D },
            FillerData = 0x55
        };
        public static UDSFrame ZDC1ConfirmationFrame = new UDSFrame
        {
            Data = new byte[3]{0x6E,0xF1,0xA0},
            FillerData=0xAA
        };
        public static UDSFrame ZDC2Frame = new UDSFrame
        {
            Data = new byte[7]{0x2E,0xF1,0xA1, 0x2D, 0x2D, 0x2D, 0x2D},
            FillerData=0x55
        };
        public static UDSFrame ZDC2ConfirmationFrame = new UDSFrame
        {
            Data = new byte[3] { 0x6E, 0xF1, 0xA1 },
            FillerData = 0xAA
        };
        public static UDSFrame ZDC3Frame = new UDSFrame
        {
            Data = new byte[0x0F] { 0x2E, 0xF1, 0xA4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            FillerData = 0x55
        };
        public static UDSFrame ZDC3ConfirmationFrame = new UDSFrame
        {
            Data = new byte[3] { 0x6E, 0xF1, 0xA4 },
            FillerData = 0xAA
        };

        public static UDSFrame ReadPressureFrame = new UDSFrame
        {
            FillerData = 0x55,
            Data = new byte[3] { 0x22, 0x10, 0x11 }
        };



        public static byte[] PullPressureValuesResponse = new byte[3] { 0x62, 0x10, 0x18 };
        public static byte[] PullTimeValuesResponse = new byte[3] { 0x62, 0x10, 0x19 };
        public static byte[] PullErrorValuesResponse = new byte[3] { 0x62, 0x10, 0x1A };
        public static byte[] PullSoftwareVersionResponse = new byte[3] { 0x62, 0xF1, 0x89 };
        public static byte[] PullConfigurationVersionResponse = new byte[3] { 0x62,0xF1,0x82 };
        public static byte[] PullPumpSerialNumberResponse = new byte[3] { 0x62, 0xF1, 0x8C };
        public static byte[] PullFstValveBlockSerialNumberResponse = new byte[3] { 0x62, 0x6A, 0xB5 };
        public static byte[] PullSndValveBlockSerialNumberResponse = new byte[3] { 0x62, 0x6A, 0xB6 };
        public static byte[] PullThdValveBlockSerialNumberResponse = new byte[3] { 0x62, 0x6A, 0xB7 };
        public static byte[] PullProofBitsResponse = new byte[3] { 0x62, 0x05, 0x05 };
        public static byte[] ReadPressureReponse = new byte[3] { 0x62, 0x10, 0x11 };


        public static List<int> DTCPositioning = new List<int> {
                0x0D,
                0x0F,
                0x14,
                0x11,
                0x10,
                0x12,
                0x13,
                0x15,
                0x71,
                0x72,
                0x73,
                0x74,
                0x01,
                0x02,
                0x03,
                0x04,
                0x05,
                0x06,
                0x07,
                0x08,
                0x09,
                0x0A,
                0x0B,
                0x0C,
                0x0E,
                0x16,
                0x62,
                0x64,
                0x75,
                0x65,
                0x66,
                0x67,
                0x68,
                0x69,
                0x6A,
                0x6B,
                0x6C,
                0x76,
                0x77,
                0x78,
                0x79,
                0x7A,
                0x7B,
                0x7C,
                0x80,
                0x81,
                0x82,
                0x83,
                0x84,
                0x85,
                0x86,
                0x87,
                0x88,
                0x89,
                0x8A,
                0x8B,
                0x8C,
                0x8D
            };

        private CANFrame[] canFrames;
        private bool receivedFlowControl = false;
        public UDSFrame()
        {
            Data = new byte[0];
        }
        public uint AddrId { get; set; }
        public int Dlc
        {
            get
            {
                return Data.Length;
            }
        }
        public byte[] Data { get; set; }

        public bool Transmit(CANWrapper wrapper)
        {
            ToCan();
            if (canFrames.Length == 0) { return false; }
            if (canFrames.Length == 1)
            {
                wrapper.Transmit(canFrames[0]);
                return true;
            }
            else
            {
                var receivedEventHandler = new CANFrameReceivedEventHandler(wrapper_FrameReceived);
                wrapper.CANFrameReceived += receivedEventHandler;
                wrapper.Transmit(canFrames[0]);
                //AwaitFlowControl();
                Thread.Sleep(20);
                wrapper.CANFrameReceived -= receivedEventHandler;
                for (int i = 1; i < canFrames.Length; i++)
                {
                    wrapper.Transmit(canFrames[i]);
                }
            }
            return true;
        }

        void wrapper_FrameReceived(object sender, CANFrameEventArgs e)
        {
            if (e.Frame.Dlc == 0x08 &&
                e.Frame.Data[0] == 0x30 &&
                e.Frame.Data[1] == 0x00 &&
                e.Frame.Data[2] == 0x00)
            {
                receivedFlowControl = true;
            }
        }

        private void AwaitFlowControl()
        {
            while (!receivedFlowControl)
            {
                System.Threading.Thread.Sleep(10);
            }
        }

        private void ToCan()
        {
            if (Dlc <= 7)
            {
                canFrames = new CANFrame[1];
                var canFrame = new CANFrame();
                canFrame.Data = new byte[8];
                canFrame.AddrId = AddrId;
                canFrame.Data[0] = (byte)Dlc;
                for (int i = 1; i < Dlc + 1; i++)
                {
                    canFrame.Data[i] = Data[i - 1];
                }
                for (int i = Dlc + 1; i < 8; i++)
                {
                    canFrame.Data[i] = FillerData;
                }
                canFrames[0] = canFrame;
            }
            else
            {
                var requiredFrames = CountRequiredFrames();
                canFrames = new CANFrame[requiredFrames];

                var canFrame = new CANFrame();
                canFrame.Data = new byte[8];
                canFrame.AddrId = AddrId;
                canFrame.Data[0] = 0x10;
                canFrame.Data[1] = (byte)Dlc;
                canFrames[0] = canFrame;

                ///interator danych ramki
                var fi = 2;
                ///iterator listy ramek
                var ri = 1;
                ///iterator numeru ramki w liście
                var di = 0x21;
                for (int i = 0; i < Dlc; i++)
                {
                    if (fi == 8)
                    {
                        canFrame = new CANFrame();
                        canFrame.Data = new byte[8];
                        canFrames[ri++] = canFrame;
                        canFrame.Data[0] = (byte)di;
                        canFrame.AddrId = AddrId;
                        if (di < 0x2F)
                        {
                            di++;
                        }
                        else
                        {
                            di = 0x20;
                        }
                        fi = 1;
                    }
                    canFrame.Data[fi++] = Data[i];
                }
                for (; fi < 8; fi++)
                {
                    canFrame.Data[fi] = FillerData;
                }
            }

        }

        private int CountRequiredFrames()
        {
            var s1 = Dlc - 5;
            if (s1 < 0) s1 = 0;
            var s2 = (s1 - 1) / 7;
            return s2 + 2;

        }

        public byte FillerData { get; set; }
        /*
        static public bool operator ==(UDSFrame a, UDSFrame b)
        {
            if ((object)a == null || (object)b == null) { return false; }
            if (a.AddrId != b.AddrId) { return false; }
            if (a.Dlc != b.Dlc) { return false; }
            for (int i = 0; i < a.Dlc; i++)
            {
                if (a.Data[i] != b.Data[i]) { return false; }
            }
            return true;
        }

        static public bool operator !=(UDSFrame a, UDSFrame b)
        {
            return !(a == b);
        }
        */
        public override string ToString()
        {
            var str = String.Format("0x{0:X4} {1} ", AddrId, Dlc);
            foreach (var item in Data)
            {
                str += String.Format("{0:X2}", item);
            }
            return str;
        }
    }
}
