using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agilent.Agilent34410.Interop;

namespace MMWrapper
{
    public class MMWrapper
    {
        private string p;
        private Agilent34410 driver34410;

        public MMWrapper(string p)
        {
            driver34410 = new Agilent34410();
            string standardInitOptions = "Cache=true, InterchangeCheck=false, QueryInstrStatus=true, RangeCheck=true, RecordCoercions=false, Simulate=false";
            driver34410.Initialize(p, false, false, standardInitOptions);
            driver34410.Status.Clear();
            

            driver34410.Current.DCCurrent.Configure(3, Agilent34410ResolutionEnum.Agilent34410ResolutionDefault);
            driver34410.Current.DCCurrent.AutoRange = Agilent34410AutoRangeEnum.Agilent34410AutoRangeOn;
            driver34410.Current.DCCurrent.Aperture = 10E-2;
            


            driver34410.Trigger.TriggerSource = Agilent34410TriggerSourceEnum.Agilent34410TriggerSourceImmediate;
            driver34410.Trigger.TriggerCount = 1;
            driver34410.Trigger.TriggerDelay = 0;
            driver34410.Trigger.SampleCount = 1;

            driver34410.DataFormat.DataFormat = Agilent34410DataFormatEnum.Agilent34410DataFormatReal64;
            driver34410.Measurement.Initiate();
        }

        public void Close()
        {
            driver34410.Close();
        }

        public double Measure()
        {
            double[] data1 = driver34410.Measurement.Fetch();
            driver34410.Measurement.Initiate();
            return data1[0];
        }
    }
}
