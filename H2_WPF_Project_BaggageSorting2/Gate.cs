using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_WPF_Project_BaggageSorting2
{
    public class Gate
    {
        private string _gateName;
        private bool _open;
        private int _flightNumber;

        public string GateName
        {
            get
            {
                return this._gateName;
            }
            set
            {
                this._gateName = value;
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
        public int FlightNumber
        {
            get
            {
                return this._flightNumber;
            }
            set
            {
                this._flightNumber = value;
            }
        }

        public Gate(string gateName, bool open, int flightNumber)
        {
            GateName = gateName;
            Open = open;
            FlightNumber = flightNumber;
        }
        public bool OpenOrClosed(bool open) // flightNumber?
        {
            // if flightNo != 0
            // open = true
            // else 
            // open false

            // Maybe control it with time? 

            Random random = new Random();

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

            Debug.WriteLine($"{GateName} Open = {open}");

            return open;

            /*
            if (remainingReservations == 0)
            {
                open = false;
            }
            else
            {
                
            }*/
        }
    }
}
