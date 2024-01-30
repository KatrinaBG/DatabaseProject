using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADSWrapper
{
    class ADSHandle
    {
        public Type Type { get; set; }

        public int Length { get; set; }

        public int Handle { get; set; }

        public Action<object> Action { get; set; }
    }
}
