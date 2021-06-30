﻿using System;

namespace H2_WPF_Project_BaggageSorting2
{
    public class FlightPlan
    {
        // This class is responsible for flight plan objects

        #region Attributes
        private int _flightNumber;
        private string _destination;
        private DateTime _departure;
        #endregion

        #region Encapsulations
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
        #endregion

        public FlightPlan(int flightNumber, string destination, DateTime departure)
        {
            FlightNumber = flightNumber;
            Destination = destination;
            Departure = departure;
        }
    }
}
