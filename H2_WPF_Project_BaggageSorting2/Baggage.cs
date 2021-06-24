using System;

namespace H2_WPF_Project_BaggageSorting2
{
    public class Baggage
    {
        // This class is responsible for baggage

        private int _baggageId;
        private int _passengerId;
        private int _flightNumber;
        private DateTime _leftReception;
        private DateTime _arrivedAtSplitter;
        private DateTime _leftSplitter;
        private DateTime _arrivedAtGate;

        public int BaggageId
        {
            get
            {
                return this._baggageId;
            }
            set
            {
                this._baggageId = value;
            }
        }

        public int PassengerId
        {
            get
            {
                return this._passengerId;
            }
            set
            {
                this._passengerId = value;
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

        public DateTime LeftReception
        {
            get
            {
                return this._leftReception;
            }
            set
            {
                this._leftReception = value;
            }
        }

        public DateTime ArrivedAtSplitter
        {
            get
            {
                return this._arrivedAtSplitter;
            }
            set
            {
                this._arrivedAtSplitter = value;
            }
        }

        public DateTime LeftSplitter
        {
            get
            {
                return this._leftSplitter;
            }
            set
            {
                this._leftSplitter = value;
            }
        }

        public DateTime ArrivedAtGate
        {
            get
            {
                return this._arrivedAtGate;
            }
            set
            {
                this._arrivedAtGate = value;
            }
        }

        public Baggage(int baggageId, int passengerId, int flightNumber/*, DateTime leftReception, DateTime arrivedAtSplitter, DateTime leftSplitter, DateTime arrivedAtGate*/)
        {
            BaggageId = baggageId;
            PassengerId = passengerId;
            FlightNumber = flightNumber;
            /*LeftReception = leftReception;
            ArrivedAtSplitter = arrivedAtSplitter;
            LeftSplitter = leftSplitter;
            ArrivedAtGate = arrivedAtGate;*/
        }
    }
}
