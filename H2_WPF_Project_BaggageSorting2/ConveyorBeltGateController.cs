using System;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    public class ConveyorBeltGateController
    {
        // This class is responsible for our ConveyorBeltGate class, which is basically a few buffers, between Splitters and Gates

        #region Datatypes
        // Counters for buffers
        public int bufferCounter1 = -1;
        public int bufferCounter2 = -1;
        public int bufferCounter3 = -1;

        // Arrays / buffers for baggage
        static Baggage[] conveyorBeltToGate1 = new Baggage[50];
        static Baggage[] conveyorBeltToGate2 = new Baggage[50];
        static Baggage[] conveyorBeltToGate3 = new Baggage[50];

        // object locks used by threads
        static object _lockConveyorBeltGate1 = new object();
        static object _lockConveyorBeltGate2 = new object();
        static object _lockConveyorBeltGate3 = new object();

        // flightNumbers which is received from gates
        static int flightNumberGate1 = 0;
        static int flightNumberGate2 = 0;
        static int flightNumberGate3 = 0;
        #endregion

        #region Methods used by splitters
        // This method is called by the splitter threads
        // it checks whether or not, the flight numbers on the baggage, corresponds to
        // the the flight numbers received from the gates
        public void CheckFlightNumbers(Baggage baggage)
        {
            Random random = new Random();
            Thread.Sleep(random.Next(500, 3000));

            if (baggage.FlightNumber == flightNumberGate1)
            {
                AddBaggageToGate1(baggage);
            }
            else if (baggage.FlightNumber == flightNumberGate2)
            {
                AddBaggageToGate2(baggage);
            }
            else if (baggage.FlightNumber == flightNumberGate3)
            {
                AddBaggageToGate3(baggage);
            }
            else
            {
                AddBaggageToLostBaggage(baggage);
            }
        }

        #region Adding baggage to gates
        // These methods increase the bufferCounters and add baggage to the conveyorBeltGate buffers/arrays
        private void AddBaggageToGate1(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate1);
            try
            {
                bufferCounter1 = +1;
                conveyorBeltToGate1[bufferCounter1] = baggage;

                Monitor.PulseAll(_lockConveyorBeltGate1);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate1);
            }
        }

        private void AddBaggageToGate2(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate2);
            try
            {
                bufferCounter2 = +1;
                conveyorBeltToGate2[bufferCounter2] = baggage;

                Monitor.PulseAll(_lockConveyorBeltGate2);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate2);
            }
        }

        private void AddBaggageToGate3(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate3);
            try
            {
                bufferCounter3 = +1;
                conveyorBeltToGate3[bufferCounter3] = baggage;

                Monitor.PulseAll(_lockConveyorBeltGate3);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate3);
            }
        }
        #endregion

        // This method is called, when the flight number on a bag, doesn't match up with any gates.
        // They will be send back into the system
        private void AddBaggageToLostBaggage(Baggage baggage)
        {
            ConveyorBeltLostBaggageController conveyorBeltLostBaggageController = new ConveyorBeltLostBaggageController();

            Debug.WriteLine($"Bag {baggage.BaggageId} added to lost baggage");
            conveyorBeltLostBaggageController.AddBagToLostBaggage(baggage);
        }
        #endregion

        #region Used by gate
        // This method is called by the gate threads
        // it checks which gate has called the method
        // then returns the baggage from the correct buffer
        public Baggage GetBaggage(Gate gate)
        {
            Baggage baggage = new Baggage(0, 0, 0);

            switch (gate.GateName)
            {
                case "Gate1":
                    baggage = Conveyor1(baggage);
                    break;

                case "Gate2":
                    baggage = Conveyor2(baggage);
                    break;

                case "Gate3":
                    baggage = Conveyor3(baggage);
                    break;

                default:
                    break;
            }

            return baggage;
        }

        #region Getting baggage from the conveyor buffers
        // When these methods are called, they return the baggage from the buffers
        private Baggage Conveyor1(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate1);

            try
            {
                baggage = conveyorBeltToGate1[0];
                MoveBaggageOnConveyorBelt1();

                Monitor.PulseAll(_lockConveyorBeltGate1);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate1);
            }

            return baggage;
        }

        private Baggage Conveyor2(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate2);

            try
            {
                baggage = conveyorBeltToGate2[0];
                MoveBaggageOnConveyorBelt2();

                Monitor.PulseAll(_lockConveyorBeltGate2);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate2);
            }

            return baggage;
        }
        private Baggage Conveyor3(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate3);

            try
            {
                baggage = conveyorBeltToGate3[0];
                MoveBaggageOnConveyorBelt3();

                Monitor.PulseAll(_lockConveyorBeltGate3);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate3);
            }

            return baggage;
        }
        #endregion

        #region Moving baggage in conveyorBelts
        // When these methods are called, they move the baggage on the conveyorBelts/ buffers
        // and decrease the buffer counters
        void MoveBaggageOnConveyorBelt1()
        {
            for (int i = 0; i < conveyorBeltToGate1.Length - 1; i++)
            {
                conveyorBeltToGate1[i] = conveyorBeltToGate1[i + 1];
            }
            bufferCounter1 -= 1;
        }

        void MoveBaggageOnConveyorBelt2()
        {
            for (int i = 0; i < conveyorBeltToGate2.Length - 1; i++)
            {
                conveyorBeltToGate2[i] = conveyorBeltToGate2[i + 1];
            }
            bufferCounter2 -= 1;
        }

        void MoveBaggageOnConveyorBelt3()
        {
            for (int i = 0; i < conveyorBeltToGate3.Length - 1; i++)
            {
                conveyorBeltToGate3[i] = conveyorBeltToGate3[i + 1];
            }
            bufferCounter3 -= 1;
        }
        #endregion

        // This method is called by the gate threads, to forward their flightnumbers
        // then the splitters can identify where the baggage should go
        public void AddFlightNumber(Gate gate)
        {
            switch (gate.GateName)
            {
                case "Gate1":
                    flightNumberGate1 = gate.FlightNumber;
                    break;

                case "Gate2":
                    flightNumberGate2 = gate.FlightNumber;
                    break;

                case "Gate3":
                    flightNumberGate3 = gate.FlightNumber;
                    break;

                default:
                    break;
            }
            Debug.WriteLine($"Conveyor at {gate.GateName} received new flight number {gate.FlightNumber}");
        }
        #endregion
    }
}
