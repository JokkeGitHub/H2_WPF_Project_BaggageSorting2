using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    class SplitterController
    {
        ConveyorBeltController conveyorBeltController = new ConveyorBeltController();

        static SplitterController splitterController = new SplitterController();
        static Thread splitterAThread = new Thread(new ThreadStart(splitterController.SplitterA));
        static Thread splitterBThread = new Thread(new ThreadStart(splitterController.SplitterB));

        static object _lockGetBaggage = new object();

        public static void StartThreads()
        {
            splitterAThread.Start();
            splitterBThread.Start();
        }

        void SplitterA()
        {
            while (true)
            {
                Baggage baggage = new Baggage(0, 0, 0/*, default, default, default, default*/);

                Monitor.Enter(_lockGetBaggage);

                try
                {
                    baggage = conveyorBeltController.GetBaggage(baggage);
                    Monitor.Pulse(_lockGetBaggage);
                }
                finally
                {
                    Monitor.Exit(_lockGetBaggage);
                }

                if (baggage != null)
                {
                    baggage.ArrivedAtSplitter = DateTime.Now;
                    Debug.WriteLine($"Splitter A, bag {baggage.BaggageId} arrived");
                }
            }
        }

        void SplitterB()
        {
            while (true)
            {
                Baggage baggage = new Baggage(0, 0, 0/*, default, default, default, default*/);

                Monitor.Enter(_lockGetBaggage);

                try
                {
                    baggage = conveyorBeltController.GetBaggage(baggage);
                    Monitor.Pulse(_lockGetBaggage);
                }
                finally
                {
                    Monitor.Exit(_lockGetBaggage);
                }

                if (baggage != null)
                {
                    baggage.ArrivedAtSplitter = DateTime.Now;
                    Debug.WriteLine($"Splitter B, bag {baggage.BaggageId} arrived");
                }
            }
        }
    }
}
