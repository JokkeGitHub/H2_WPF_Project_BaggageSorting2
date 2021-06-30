using System;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    class ReceptionController
    {
        // This class is responsible for handling the check-in process

        static CentralServer centralServer = new CentralServer();
        static Reservation[] reservations = centralServer.GetReservations();
        static int remainingReservations = reservations.Length;
        static object _lockReservation = new object();
        static object _lockConveyorBelt = new object();

        public Random random = new Random();

        // Create listeners
        #region EventHandler Listeners
        public EventHandler BaggageCreated1;
        public EventHandler BaggageCreated2;
        public EventHandler BaggageCreated3;
        public EventHandler BaggageCreated4;

        public EventHandler OpenOrClosedCounter1;
        public EventHandler OpenOrClosedCounter2;
        public EventHandler OpenOrClosedCounter3;
        public EventHandler OpenOrClosedCounter4;
        #endregion

        // This is where we create our threads
        public ReceptionController()
        {
            for (int i = 1; i <= 4; i++)
            {
                Thread receptionThread = new Thread(() => CheckIn());
                receptionThread.Start();
            }
        }

        // This method is called by threads when they start
        // It creates receptions, and starts the Check-In process
        private void CheckIn()
        {
            ReceptionFactory receptionFactory = new ReceptionFactory();
            Reception reception = receptionFactory.Create();

            while (true)
            {
                OpenOrClose(reception);
            }
        }

        // This method controls when the receptions opens or closes
        private void OpenOrClose(Reception reception)
        {
            Thread.Sleep(random.Next(100, 500));
            reception.Open = reception.OpenOrClosed(reception.Open, remainingReservations);
            OpenClosedDetermineListener(reception);

            if (reception.Open == true)
            {
                Thread.Sleep(random.Next(100, 500));
                GetReservationInfo(reception);
            }
        }

        // This method is responsible for retrieving all the necessary info from the reservations
        // then the data can be passed on into the system
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

                    Thread.Sleep(random.Next(100, 500));
                    Monitor.PulseAll(_lockReservation);

                    CreateBaggage(reception, passengerId, flightNumber);
                }
            }
            finally
            {
                Monitor.Exit(_lockReservation);
            }
        }

        // This method is using the data which is passed on in the parameters
        // to call the baggage factory and the create the baggage in the system
        private void CreateBaggage(Reception reception, int passengerId, int flightNumber)
        {
            BaggageFactory baggageFactory = new BaggageFactory();
            Baggage baggage = baggageFactory.Create(passengerId, flightNumber);

            BaggageCreatedDetermineListener(reception, baggage);

            Debug.WriteLine($"{reception.CounterName} created bag {baggage.BaggageId}, passenger {baggage.PassengerId}, flight {baggage.FlightNumber}");

            BaggageLeavingReception(baggage, reception);
        }

        // When the baggage has been created in the system, it will be passed on to the conveyor belt
        private void BaggageLeavingReception(Baggage baggage, Reception reception)
        {
            ConveyorBeltController conveyorBeltController = new ConveyorBeltController();

            Monitor.Enter(_lockConveyorBelt);
            try
            {
                Thread.Sleep(random.Next(100, 500));
                baggage.LeftReception = DateTime.Now;

                Debug.WriteLine($"Bag {baggage.BaggageId} left {reception.CounterName} at {baggage.LeftReception}");
                conveyorBeltController.AddBagToConveyorBelt(baggage, reception.CounterName);

                Monitor.PulseAll(_lockConveyorBelt);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBelt);
            }
        }

        // This method moves on to the next reservation in the array
        // then decreases the reservations counter
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

        #region Listeners
        // When this method is called, it checks which reception/counter has called it
        // Then it invokes the corresponding listener
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

        // When this method is called, it checks which reception/counter has called it
        // Then it invokes the corresponding listener
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
        #endregion
    }
}
