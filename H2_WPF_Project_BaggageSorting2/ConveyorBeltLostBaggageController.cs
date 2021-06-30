using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    public class ConveyorBeltLostBaggageController
    {
        // This class is responsible for the lost baggage. The baggage that arrives in this class, is sent back into the system.

        public int bufferCounter = -1;
        public Baggage[] lostBaggage = new Baggage[50];
        static object _lockLostBaggage = new object();

        public ConveyorBeltLostBaggageController()
        {
            Thread lostBaggageThread = new Thread(() => Start());
            lostBaggageThread.Start();
        }

        #region Methods used by Splitters
        // This method is called by threads in the splitter controller class
        // it adds baggage to the LostBaggage array, and increases the bufferCounter
        public void AddBagToLostBaggage(Baggage baggage)
        {
            Monitor.Enter(_lockLostBaggage);
            try
            {
                bufferCounter += 1;
                lostBaggage[bufferCounter] = baggage;
                Debug.WriteLine($"Lost baggage {lostBaggage[bufferCounter].BaggageId}, arrived at lost baggage conveyor belt");

                Monitor.PulseAll(_lockLostBaggage);
            }
            finally
            {
                Monitor.Exit(_lockLostBaggage);
            }
        }
        #endregion

        //This method is called when the thread starts
        private void Start()
        {
            Random random = new Random();

            while (true)
            {
                Thread.Sleep(random.Next(500, 1000));
                CheckForBaggage();
            }
        }

        // This method checks the LoastBaggage array for baggage
        private void CheckForBaggage()
        {
            if (lostBaggage[0] != null)
            {
                ConveyorBeltController conveyorBeltController = new ConveyorBeltController();

                string lostBaggageName = "Lost Baggage Conveyor Belt";
                Baggage baggage = new Baggage(0, 0, 0);
                baggage = GetBaggage(baggage);

                conveyorBeltController.AddBagToConveyorBelt(baggage, lostBaggageName);
            }
        }

        // This method is called if there is any baggage on the lostBaggage array
        // and then returns the baggage
        private Baggage GetBaggage(Baggage baggage)
        {
            Monitor.Enter(_lockLostBaggage);
            try
            {
                baggage = lostBaggage[0];
                MoveLostBaggage();

                Monitor.PulseAll(_lockLostBaggage);
            }
            finally
            {
                Monitor.Exit(_lockLostBaggage);
            }
            return baggage;
        }

        // This method moves baggage around in the array when a bag has been removed from the array
        private void MoveLostBaggage()
        {
            for (int i = 0; i < lostBaggage.Length - 1; i++)
            {
                lostBaggage[i] = lostBaggage[i + 1];
            }
            bufferCounter -= 1;
        }


    }
}
