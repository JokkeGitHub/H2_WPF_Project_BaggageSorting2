using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_WPF_Project_BaggageSorting2
{
    public class Reception
    {
        // This class is responsible for the reception/check-in

        private string _counterName;
        private bool _open;

        public string CounterName
        {
            get
            {
                return this._counterName;
            }
            set
            {
                this._counterName = value;
            }
        }

        public bool Open
        {
            get
            {
                return this._open;
            }
            set
            {
                this._open = value;
            }
        }

        public Reception(string counterName, bool open)
        {
            CounterName = counterName;
            Open = open;
        }

        public bool OpenOrClosed(bool open, int remainingReservations)
        {
            Random random = new Random();

            if (remainingReservations == 0)
            {
                open = false;
            }
            else
            {
                int tempInt = random.Next(0, 2);
                switch (tempInt)
                {
                    case 0:
                        open = true;
                        break;

                    case 1:
                        open = false;
                        break;

                    default:
                        break;
                }

                Debug.WriteLine($"{CounterName} Open = {open}");
            }

            return open;
        }
    }
}
