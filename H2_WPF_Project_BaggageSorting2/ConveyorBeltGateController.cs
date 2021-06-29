using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    public class ConveyorBeltGateController
    {
        static int bufferCounter1 = -1;
        static int bufferCounter2 = -1;
        static int bufferCounter3 = -1;
        static int bufferLostBaggage = -1;

        static Baggage[] conveyorBeltToGate1 = new Baggage[50];
        static Baggage[] conveyorBeltToGate2 = new Baggage[50];
        static Baggage[] conveyorBeltToGate3 = new Baggage[50];

        static Baggage[] lostBaggage = new Baggage[50];

        static int flightNumberGate1 = 0;
        static int flightNumberGate2 = 0;
        static int flightNumberGate3 = 0;

        static object _lockConveyorBeltGate1 = new object();
        static object _lockConveyorBeltGate2 = new object();
        static object _lockConveyorBeltGate3 = new object();

        static object _lockBuffer1 = new object();
        static object _lockBuffer2 = new object();
        static object _lockBuffer3 = new object();

        #region Used by splitter
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
                /*bufferLostBaggage += 1;
                lostBaggage[bufferLostBaggage] = baggage;
                Debug.WriteLine($"Bag {baggage.BaggageId} added to lost baggage");*/
            }
        }

        private void AddBaggageToGate1(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate1);
            try
            {
                Buffer1();
                conveyorBeltToGate1[bufferCounter1] = baggage;

                Monitor.PulseAll(_lockConveyorBeltGate1);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate1);
            }
        }

        private void Buffer1()
        {
            bufferCounter1 =+ 1;
        }

        private void AddBaggageToGate2(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate2);
            try
            {
                Buffer2();
                conveyorBeltToGate2[bufferCounter2] = baggage;

                Monitor.PulseAll(_lockConveyorBeltGate2);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate2);
            }
        }
        private void Buffer2()
        {
            bufferCounter2 =+ 1;
        }

        private void AddBaggageToGate3(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate3);
            try
            {
                Buffer3();
                conveyorBeltToGate3[bufferCounter3] = baggage;

                Monitor.PulseAll(_lockConveyorBeltGate3);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate3);
            }
        }
        private void Buffer3()
        {
            bufferCounter3 =+ 1;
        }
        #endregion

        #region Used by gate
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

        private Baggage Conveyor1(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate1);

            try
            {
                baggage = conveyorBeltToGate1[0];
                MoveBaggageOnConveyorBelt1();
                bufferCounter1 -= 1;

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
                bufferCounter2 -= 1;

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
                bufferCounter3 -= 1;

                Monitor.PulseAll(_lockConveyorBeltGate3);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate3);
            }

            return baggage;
        }

        void MoveBaggageOnConveyorBelt1()
        {
            for (int i = 0; i < conveyorBeltToGate1.Length - 1; i++)
            {
                conveyorBeltToGate1[i] = conveyorBeltToGate1[i + 1];
            }
        }

        void MoveBaggageOnConveyorBelt2()
        {
            for (int i = 0; i < conveyorBeltToGate2.Length - 1; i++)
            {
                conveyorBeltToGate2[i] = conveyorBeltToGate2[i + 1];
            }
        }

        void MoveBaggageOnConveyorBelt3()
        {
            for (int i = 0; i < conveyorBeltToGate3.Length - 1; i++)
            {
                conveyorBeltToGate3[i] = conveyorBeltToGate3[i + 1];
            }
        }

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
