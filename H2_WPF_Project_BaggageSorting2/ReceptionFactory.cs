using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_WPF_Project_BaggageSorting2
{
    public class ReceptionFactory
    {
        static int receptionNumber = 0;

        public Reception Create()
        {
            receptionNumber += 1;
            string counterName = $"Counter{receptionNumber}";            

            return new Reception(counterName, true);
        }
    }
}
