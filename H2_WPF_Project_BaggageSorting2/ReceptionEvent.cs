using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_WPF_Project_BaggageSorting2
{
    public class ReceptionEvent : EventArgs
    {
        public Reception Reception { get; private set; }

        public ReceptionEvent(Reception reception)
        {
            Reception = reception;
        }    
    }
}
