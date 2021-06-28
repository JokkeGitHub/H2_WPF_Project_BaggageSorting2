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

        static Baggage[] conveyorBeltToGate1 = new Baggage[50];
        static Baggage[] conveyorBeltToGate2 = new Baggage[50];
        static Baggage[] conveyorBeltToGate3 = new Baggage[50];

        static int flightNumberGate1 = 0;
        static int flightNumberGate2 = 0;
        static int flightNumberGate3 = 0;

        static object _lockConveyorBeltGate1 = new object();
        static object _lockConveyorBeltGate2 = new object();
        static object _lockConveyorBeltGate3 = new object();

        public void CheckFlightNumbers(Baggage baggage)
        {
            Random random = new Random();
            Thread.Sleep(random.Next(500, 3000));

            if (flightNumberGate1 == baggage.FlightNumber)
            {
                AddBaggageToGate1(baggage);
            }
            else if (flightNumberGate2 == baggage.FlightNumber)
            {
                AddBaggageToGate2(baggage);
            }
            else if (flightNumberGate3 == baggage.FlightNumber)
            {
                AddBaggageToGate3(baggage);
            }
            else
            {
                // Make somewhere to put lost bags
                Debug.WriteLine($"Bag {baggage.BaggageId} got tossed in the trash.");
            }
        }

        private void AddBaggageToGate1(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBeltGate1);
            try
            {
                bufferCounter1 += 1;
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
                baggage.LeftSplitter = DateTime.Now;
                Debug.WriteLine($"Bag {baggage.BaggageId} left splitter at {baggage.LeftSplitter}");

                bufferCounter2 += 1;
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
                baggage.LeftSplitter = DateTime.Now;
                Debug.WriteLine($"Bag {baggage.BaggageId} left splitter at {baggage.LeftSplitter}");

                bufferCounter3 += 1;
                conveyorBeltToGate3[bufferCounter3] = baggage;

                Monitor.PulseAll(_lockConveyorBeltGate3);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBeltGate3);
            }
        }
    }
}
