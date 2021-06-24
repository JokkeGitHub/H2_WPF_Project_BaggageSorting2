using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    class ReceptionController
    {
        // This class is responsible for reception threads
        public Random random = new Random();

        static object _lockConveyorBelt = new object();

        static CentralServer centralServer = new CentralServer();
        static Reservation[] reservations = centralServer.GetReservations();
        static int remainingReservations = reservations.Length;
        static object _lockReservation = new object();

        public EventHandler BaggageCreated1;
        public EventHandler BaggageCreated2;
        public EventHandler BaggageCreated3;
        public EventHandler BaggageCreated4;

        public EventHandler OpenOrClosedCounter1;
        public EventHandler OpenOrClosedCounter2;
        public EventHandler OpenOrClosedCounter3;
        public EventHandler OpenOrClosedCounter4;

        public ReceptionController()
        {
            for (int i = 1; i <= 4; i++)
            {
                Thread receptionThread = new Thread(() => CheckIn());
                receptionThread.Start();
            }
        }

        private void CheckIn()
        {
            ReceptionFactory receptionFactory = new ReceptionFactory();
            Reception reception = receptionFactory.Create();

            while (true)
            {
                if (reception.Open == true)
                {
                    Thread.Sleep(random.Next(100, 1000));
                    GetReservationInfo(reception);
                }

                Thread.Sleep(random.Next(100, 1000));
                reception.Open = reception.OpenOrClosed(reception.Open, remainingReservations);
                OpenOrClosedCounter1?.Invoke(this, new ReceptionEvent(reception));
                OpenClosedDetermineListener(reception);
            }
        }

        private void GetReservationInfo(Reception reception)
        {
            Monitor.Enter(_lockReservation);
            try
            {
                if (remainingReservations == 0)
                {
                    Debug.WriteLine("No more reservations");
                }
                else
                {
                    int passengerId = reservations[0].PassengerId;
                    int flightNumber = reservations[0].FlightNumber;
                    NextReservation();

                    Thread.Sleep(random.Next(100, 1000));
                    Monitor.PulseAll(_lockReservation);

                    CreateBaggage(reception, passengerId, flightNumber);
                }
            }
            finally
            {
                Monitor.Exit(_lockReservation);
            }
        }

        private void CreateBaggage(Reception reception, int passengerId, int flightNumber)
        {
            BaggageFactory baggageFactory = new BaggageFactory();
            Baggage baggage = baggageFactory.Create(passengerId, flightNumber);

            BaggageCreatedDetermineListener(reception, baggage);

            Debug.WriteLine($"{reception.CounterName} created bag {baggage.BaggageId}, passenger {baggage.PassengerId}, flight {baggage.FlightNumber}");
        }


        // add bag event
        // add timestamp
        // Add bag to conveyor belt


        private void NextReservation()
        {
            Monitor.Enter(_lockReservation);

            try
            {
                for (int i = 0; i < reservations.Length - 1; i++)
                {
                    reservations[i] = reservations[i + 1];
                }

                remainingReservations -= 1;
            }
            finally
            {
                Monitor.Exit(_lockReservation);
            }
        }


        // New class for these?
        private void OpenClosedDetermineListener(Reception reception)
        {
            switch (reception.CounterName)
            {
                case "Counter1":
                    OpenOrClosedCounter1?.Invoke(this, new ReceptionEvent(reception));
                    break;

                case "Counter2":
                    OpenOrClosedCounter2?.Invoke(this, new ReceptionEvent(reception));
                    break;

                case "Counter3":
                    OpenOrClosedCounter3?.Invoke(this, new ReceptionEvent(reception));
                    break;

                case "Counter4":
                    OpenOrClosedCounter4?.Invoke(this, new ReceptionEvent(reception));
                    break;

                default:
                    break;
            }
        }

        private void BaggageCreatedDetermineListener(Reception reception, Baggage baggage)
        {
            switch (reception.CounterName)
            {
                case "Counter1":
                    BaggageCreated1?.Invoke(this, new BaggageEvent(baggage));
                    break;

                case "Counter2":
                    BaggageCreated2?.Invoke(this, new BaggageEvent(baggage));
                    break;

                case "Counter3":
                    BaggageCreated3?.Invoke(this, new BaggageEvent(baggage));
                    break;

                case "Counter4":
                    BaggageCreated4?.Invoke(this, new BaggageEvent(baggage));
                    break;

                default:
                    break;
            }
        }
    }
}
