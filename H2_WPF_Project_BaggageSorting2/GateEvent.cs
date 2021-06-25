﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_WPF_Project_BaggageSorting2
{
    public class GateEvent : EventArgs
    {
        public Gate Gate { get; private set; }

        public GateEvent(Gate gate)
        {
            Gate = gate;
        }
    }
}