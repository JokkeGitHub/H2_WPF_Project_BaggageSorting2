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
        private string _destination;
        private DateTime _departure;
        private Baggage[] _baggageCart;

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
        public string Destination
        {
            get
            {
                return this._destination;
            }
            set
            {
                this._destination = value;
            }
        }
        public DateTime Departure
        {
            get
            {
                return this._departure;
            }
            set
            {
                this._departure = value;
            }
        }
        public Baggage[] BaggageCart
        {
            get
            {
                return this._baggageCart;
            }
            set
            {
                this._baggageCart = value;
            }
        }

        public Gate(string gateName, bool open, int flightNumber, string destination, Baggage[] baggageCart)
        {
            GateName = gateName;
            Open = open;
            FlightNumber = flightNumber;
            Destination = destination;
            BaggageCart = baggageCart;
        }

        public bool OpenOrClosed(bool open, int remainingFlightPlans)
        {
            // Maybe control it with time? 

            Random random = new Random();

            if (remainingFlightPlans == 0)
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
            }

            Debug.WriteLine($"{GateName} Open = {open}");

            return open;
        }
    }
}
