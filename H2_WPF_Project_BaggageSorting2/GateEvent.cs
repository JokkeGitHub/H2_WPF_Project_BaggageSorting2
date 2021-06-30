using System;

namespace H2_WPF_Project_BaggageSorting2
{
    public class GateEvent : EventArgs
    {
        // This class is responsible for gate events

        public Gate Gate { get; private set; }

        public GateEvent(Gate gate)
        {
            Gate = gate;
        }
    }
}
