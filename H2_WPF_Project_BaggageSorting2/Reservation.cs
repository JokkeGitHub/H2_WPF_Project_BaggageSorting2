using System;

namespace H2_WPF_Project_BaggageSorting2
{
    public class Reservation
    {
        // This class is responsible for reservation objects

        #region Attributes
        private string _passengerName;
        private int _passengerId;
        private int _flightNumber;
        private DateTime _departure;
        #endregion

        #region Encapsulations
        public string PassengerName
        {
            get
            {
                return this._passengerName;
            }
            set
            {
                this._passengerName = value;
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
        #endregion

        public Reservation(string passengerName, int passengerId, int flightNumber, DateTime departure)
        {
            PassengerName = passengerName;
            PassengerId = passengerId;
            FlightNumber = flightNumber;
            Departure = departure;
        }
    }
}
