using System;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    public class Reception
    {
        // This class is responsible for the reception objects

        #region Attributes
        private string _counterName;
        private bool _open;
        #endregion

        #region Encapsulations
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
        #endregion

        public Reception(string counterName, bool open)
        {
            CounterName = counterName;
            Open = open;
        }

        // When this method is called by a reception, it determines whether the reception should open or close
        public bool OpenOrClosed(bool open, int remainingReservations)
        {
            Random random = new Random();

            if (remainingReservations == 0)
            {
                open = false;
            }
            else
            {
                switch (random.Next(0, 2))
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
