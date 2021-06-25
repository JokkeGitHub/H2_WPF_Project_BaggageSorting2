using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_WPF_Project_BaggageSorting2
{
    public class GateFactory
    {
        static int gateNumber = 0;

        public Gate Create()
        {
            gateNumber += 1;
            string gateName = $"Gate{gateNumber}";

            return new Gate(gateName, false, 0);
        }
    }
}
