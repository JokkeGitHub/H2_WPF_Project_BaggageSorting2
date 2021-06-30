using System;

namespace H2_WPF_Project_BaggageSorting2
{
    public class BaggageEvent : EventArgs
    {
        // This class is responsible for baggage events

        public Baggage Baggage { get; private set; }

        public BaggageEvent(Baggage baggage)
        {
            Baggage = baggage;
        }    
    }
}
