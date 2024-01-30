using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CANWrapper
{
    public class SimpleSignal
    {
        #region C7
        public static SimpleSignal BCM1_02 = new SimpleSignal { AddrId = 0x662, Data = new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00 }, Interval = 1000 };
        public static SimpleSignal BCM2_02 = new SimpleSignal { AddrId = 0x581, Data = new byte[8] { 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal BEM_02 = new SimpleSignal { AddrId = 0x663, Data = new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 100 };
        public static SimpleSignal Diagnose_01 = new SimpleSignal { AddrId = 0x6B2, Data = new byte[8] { 0xFF, 0x0F, 0x00, 0x1C, 0xF8, 0x1F, 0xFE, 0xC0 }, Interval = 1000 };
        public static SimpleSignal Gateway_01 = new SimpleSignal { AddrId = 0x3C3, Data = new byte[8] { 0x80, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00 }, Interval = 100 };
        public static SimpleSignal Klemmen_Status_01 = new SimpleSignal { AddrId = 0x3C0, Data = new byte[4] { 0x00, 0x00, 0x07, 0x00 }, Interval = 100 };
        public static SimpleSignal Klima_03 = new SimpleSignal { AddrId = 0x66E, Data = new byte[7] { 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00 }, Interval = 2000 };
        public static SimpleSignal Motor_07 = new SimpleSignal { AddrId = 0x640, Data = new byte[8] { 0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00 }, Interval = 500 };
        public static SimpleSignal Motor_K_02 = new SimpleSignal { AddrId = 0x3A3, Data = new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, }, Interval = 100 };
        public static SimpleSignal NMH_AAG = new SimpleSignal { AddrId = 0x6F4, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_ALM = new SimpleSignal { AddrId = 0x6EB, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_Armlehne = new SimpleSignal { AddrId = 0x6D4, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_BCM1 = new SimpleSignal { AddrId = 0x6E0, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_BCM2 = new SimpleSignal { AddrId = 0x6E1, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_Gateway = new SimpleSignal { AddrId = 0x6C0, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_HDSG = new SimpleSignal { AddrId = 0x6F8, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_IVB_BFS = new SimpleSignal { AddrId = 0x6D6, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_IVB_BFS_Fond = new SimpleSignal { AddrId = 0x6D7, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_IVB_FS = new SimpleSignal { AddrId = 0x6D5, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_IVB_FS_Fond = new SimpleSignal { AddrId = 0x6D8, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_MEM_BF = new SimpleSignal { AddrId = 0x6E7, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_MEM_BFS_Fond = new SimpleSignal { AddrId = 0x6E9, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_MEM_F = new SimpleSignal { AddrId = 0x6E6, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_MEM_FS_Fond = new SimpleSignal { AddrId = 0x6E8, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_MFG = new SimpleSignal { AddrId = 0x6FA, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_STS = new SimpleSignal { AddrId = 0x6F2, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_TSG_BT = new SimpleSignal { AddrId = 0x6E3, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_TSG_FT = new SimpleSignal { AddrId = 0x6E2, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_TSG_HL = new SimpleSignal { AddrId = 0x6E4, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_TSG_HR = new SimpleSignal { AddrId = 0x6E5, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal NMH_VSG = new SimpleSignal { AddrId = 0x6F0, Data = new byte[7] { 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 200 };
        public static SimpleSignal RGS_VL_01 = new SimpleSignal { AddrId = 0x3D8, Data = new byte[8] { 0x00, 0x00, 0x00, 0x01, 0xFF, 0xFF, 0xFF, 0x00 }, Interval = 100 };

        public static SimpleSignal C7_Charisma_03 = new SimpleSignal { AddrId = 0x386, Data = new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 100 };
        public static SimpleSignal C7_MEM_FS_01 = new SimpleSignal { AddrId = 0x49A, Data = new byte[8] { 0x00, 0x00, 0x00, 0x20, 0x3F, 0x01, 0x00, 0x00 }, Interval = 100 };
        public static SimpleSignal C7_MEM_FS_02 = new SimpleSignal { AddrId = 0x3BA, Data = new byte[8] { 0x00, 0xF0, 0xFF, 0x1F, 0xFF, 0x00, 0x00, 0x00 }, Interval = 100 };
        public static SimpleSignal C7_TSG_FT_01 = new SimpleSignal { AddrId = 0x3D0, Data = new byte[8] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 100 };

        public static SimpleSignal T99_MEM_BFS_Fond_01 = new SimpleSignal { AddrId = 0x3BD, Data = new byte[8] { 0x00, 0xF0, 0xFF, 0xDF, 0x17, 0x0C, 0x00, 0x00 }, Interval = 100 };
        public static SimpleSignal T99_MEM_FS_01 = new SimpleSignal { AddrId = 0x49A, Data = new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00 }, Interval = 100 };
        public static SimpleSignal T99_TSG_HL_01 = new SimpleSignal { AddrId = 0x3D2, Data = new byte[8] { 0x01, 0x00, 0x0E, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 100 };
        public static SimpleSignal T99_TSG_HR_01 = new SimpleSignal { AddrId = 0x3D3, Data = new byte[8] { 0x01, 0x00, 0x0E, 0x00, 0x00, 0x00, 0x00, 0x00 }, Interval = 100 };
        private CANFrame canFrame;

        #endregion


        public uint AddrId { get; set; }

        public byte[] Data { get; set; }

        public int Interval { get; set; }

        public void Transmit()
        {
            var frame = new CANFrame()
            {
                AddrId = AddrId,
                Data = Data
            };
            while (!End)
            {
                Wrapper.Transmit(frame);
                Thread.Sleep(Interval);
            }
        }
        public CANFrame Frame
        {
            get
            {
                if (canFrame == null)
                {
                    canFrame = new CANFrame
                    {
                        AddrId=AddrId,
                        Data=Data
                    };
                } return canFrame;
            }
        }

        public bool End { get; set; }

        public CANWrapper Wrapper { get; set; }
    }
}
