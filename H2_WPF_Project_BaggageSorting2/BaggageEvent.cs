using System;

namespace H2_WPF_Project_BaggageSorting2
{
    public class BaggageEvent : EventArgs
    {
        public Baggage Baggage { get; private set; }

        public BaggageEvent(Baggage baggage)
        {
            Baggage = baggage;
        }    
    }
}
