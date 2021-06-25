﻿using System;
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
        static object _lockGetBaggage = new object();

        public EventHandler BaggageArrivedInSplitter1;
        public EventHandler BaggageArrivedInSplitter2;

        public SplitterController()
        {
            for (int i = 1; i <= 2; i++)
            {
                int splitterNumber = i;
                Thread splitterThread = new Thread(() => SplitterSorting(splitterNumber));
                splitterThread.Start();
            }
        }

        private void SplitterSorting(int splitterNumber)
        {
            Random random = new Random();
            while (true)
            {
                Thread.Sleep(random.Next(500, 3000));

                Baggage baggage = new Baggage(0, 0, 0);

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
                    Debug.WriteLine($"Bag {baggage.BaggageId} arrived at splitter{splitterNumber}, at {baggage.ArrivedAtSplitter}");

                    BaggageArrivedInSplitterDetermineListener(splitterNumber, baggage);
                }
            }
        }

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
    }
}
