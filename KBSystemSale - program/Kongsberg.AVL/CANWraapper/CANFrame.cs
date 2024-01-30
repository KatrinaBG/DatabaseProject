using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vxlapi_NET20;

namespace CANWrapper
{
    /// <summary>
    /// reprezentacja ramki CAN
    /// </summary>
    public class CANFrame
    {

        public static CANFrame StopIgnitionFrame = new CANFrame()
        {
            Data = new byte[4] { 0x00, 0x00, 0x00, 0x00 },
            AddrId = 0x3C0
        };
        public static CANFrame InstantSleepFrame = new CANFrame()
        {
            Data = new byte[8] { 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
            AddrId = 0x663
        };

        /// <summary>
        /// Konstruktor ramki przyjmujący długość ramki
        /// </summary>
        /// <param name="dlc">Długość ramki</param>
        public CANFrame()
        {
            Data = new byte[0];
        }
        /// <summary>
        /// Konstruktor ramki przyjmującą ramkę z vxlapi
        /// </summary>
        /// <param name="frame">ramka z vxlapi</param>
        public CANFrame(XLClass.xl_event frame)
        {
            AddrId = frame.tagData.can_Msg.id;
            Data = frame.tagData.can_Msg.data;
        }
        /// <summary>
        /// ID nadawcy
        /// </summary>
        public uint AddrId { get; set; }
        /// <summary>
        /// Długugośc ramki
        /// </summary>
        public int Dlc
        {
            get
            {
                return Data.Length;
            }
        }
        /// <summary>
        /// Dane ramki
        /// </summary>
        public byte[] Data { get; set; }

    }
}
