﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pump_Post_Controller_Service
{
    public enum TestStage : int
    {
        Initial = 0,
        SelfTest = 1,
        OpenValves = 2,
        PullResults = 3,
        PullDTC = 4,
        ReadSerials = 5,
        UploadConfig = 6,
        Ignore = 7,
        SetProofBits = 8,
        PumpWork = 9,
        ReadPressure = 10
    }
}