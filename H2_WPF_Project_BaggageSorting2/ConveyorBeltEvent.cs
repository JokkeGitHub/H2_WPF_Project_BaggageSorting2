using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_WPF_Project_BaggageSorting2
{
    class ConveyorBeltEvent : EventArgs
    {
        public ConveyorBelt ConveyorBelt { get; private set; }

        public ConveyorBeltEvent(ConveyorBelt conveyorBelt)
        {
            ConveyorBelt = conveyorBelt;
        }
    }
}
