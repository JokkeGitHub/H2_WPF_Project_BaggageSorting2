using System;

namespace H2_WPF_Project_BaggageSorting2
{
    public class ReceptionEvent : EventArgs
    {
        // This class is responsible for reception events

        public Reception Reception { get; private set; }

        public ReceptionEvent(Reception reception)
        {
            Reception = reception;
        }    
    }
}
