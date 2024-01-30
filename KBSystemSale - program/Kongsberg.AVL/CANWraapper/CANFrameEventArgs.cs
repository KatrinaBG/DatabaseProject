using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vxlapi_NET20;

namespace CANWrapper
{
    public class CANFrameEventArgs : EventArgs
    {
        public CANFrame Frame { get; set; }
        public XLClass.XLstatus Status { get; set; }
    }
}
