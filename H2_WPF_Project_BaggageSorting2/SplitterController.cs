using System;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    class SplitterController
    {
        // This class is responsible for passing on the baggage to the correct recipients/gates

        ConveyorBeltController conveyorBeltController = new ConveyorBeltController();
        static object _lockGetBaggage = new object();

        #region Event Listeners
        public EventHandler BaggageArrivedInSplitter1;
        public EventHandler BaggageArrivedInSplitter2;

        public EventHandler BaggageLeavesSplitter1;
        public EventHandler BaggageLeavesSplitter2;
        #endregion

        // Here we create our splitter threads
        public SplitterController()
        {
            for (int i = 1; i <= 2; i++)
            {
                int splitterNumber = i;
                Thread splitterThread = new Thread(() => SplitterSorting(splitterNumber));
                splitterThread.Start();
            }
        }

        // This method is called when the threads start
        // it retrieves baggage from the conveyor belt
        private void SplitterSorting(int splitterNumber)
        {
            Random random = new Random();

            while (true)
            {
                Thread.Sleep(random.Next(100, 500));

                Baggage baggage = new Baggage(0, 0, 0);

                Monitor.Enter(_lockGetBaggage);
                try
                {
                    baggage = conveyorBeltController.CheckForBaggage(baggage);
                    Monitor.Pulse(_lockGetBaggage);
                }
                finally
                {
                    Monitor.Exit(_lockGetBaggage);
                }

                BaggageArrivesInSPlitter(baggage, splitterNumber);
            }
        }

        // This method checks whether or not, baggage was received
        // then if baggage was received, it is passed on
        private void BaggageArrivesInSPlitter(Baggage baggage, int splitterNumber)
        {
            if (baggage != null)
            {
                baggage.ArrivedAtSplitter = DateTime.Now;
                Debug.WriteLine($"Bag {baggage.BaggageId} arrived at splitter{splitterNumber}, at {baggage.ArrivedAtSplitter}");

                BaggageArrivedInSplitterDetermineListener(splitterNumber, baggage);
                BaggageLeavesSplitter(baggage, splitterNumber);
            }
        }

        // Here the baggage leaves the splitter
        private void BaggageLeavesSplitter(Baggage baggage, int splitterNumber)
        {
            ConveyorBeltGateController conveyorBeltGateController = new ConveyorBeltGateController();

            baggage.LeftSplitter = DateTime.Now;
            Debug.WriteLine($"Bag {baggage.BaggageId} left splitter at {baggage.LeftSplitter}");

            conveyorBeltGateController.CheckFlightNumbers(baggage);
            BaggageLeavesSplitterDetermineListener(splitterNumber, baggage);

        }

        #region Listeners
        // When this method is called, it checks which splitter has called it
        // Then it invokes the corresponding listener
        private void BaggageArrivedInSplitterDetermineListener(int splitterNumber, Baggage baggage)
        {
            switch (splitterNumber)
            {
                case 1:
                    BaggageArrivedInSplitter1?.Invoke(this, new BaggageEvent(baggage));
                    break;

                case 2:
                    BaggageArrivedInSplitter2?.Invoke(this, new BaggageEvent(baggage));
                    break;

                default:
                    break;
            }
        }

        // When this method is called, it checks which splitter has called it
        // Then it invokes the corresponding listener
        private void BaggageLeavesSplitterDetermineListener(int splitterNumber, Baggage baggage)
        {
            switch (splitterNumber)
            {
                case 1:
                    BaggageLeavesSplitter1?.Invoke(this, new BaggageEvent(baggage));
                    break;

                case 2:
                    BaggageLeavesSplitter2?.Invoke(this, new BaggageEvent(baggage));
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
