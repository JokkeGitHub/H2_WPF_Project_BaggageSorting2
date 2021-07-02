using System;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    public class ConveyorBeltController
    {
        // This class is responsible for our ConveyorBelt class, which is basically a buffer, between Reception and Splitter

        static int bufferCounter = -1;
        static Baggage[] conveyorBelt = new Baggage[50]; // !! Lav conveyor object , navn, array, counter, 
        static object _lockConveyorBelt = new object();

        #region Methods used by Receptions
        // This method is called by threads in the reception controller class
        // it adds baggage to the conveyorBelt array, and increases the bufferCounter
        public void AddBagToConveyorBelt(Baggage baggage, string counterName)
        {
            Monitor.Enter(_lockConveyorBelt);
            try
            {
                bufferCounter += 1;
                conveyorBelt[bufferCounter] = baggage;
                Debug.WriteLine($"Bag {conveyorBelt[bufferCounter].BaggageId}, arrived at conveyor belt, from {counterName}");

                Monitor.PulseAll(_lockConveyorBelt);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBelt);
            }
        }
        #endregion

        #region Methods used Splitters
        // This method is called by threads in the splitter controller class
        // and it checks the conveyorBelt array for baggage
        public Baggage CheckForBaggage(Baggage baggage)
        {
            if (conveyorBelt[0] == null)
            {
                return null;
            }
            else
            {
                baggage = GetBaggage(baggage);
                return baggage;
            }
        }

        // This method is called if there is any baggage on the conveyorBelt array
        // and then returns the baggage
        private Baggage GetBaggage(Baggage baggage)
        {
            Monitor.Enter(_lockConveyorBelt);
            try
            {
                baggage = conveyorBelt[0];
                MoveBaggageOnConveyorBelt();

                Monitor.PulseAll(_lockConveyorBelt);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBelt);
            }
            return baggage;
        }

        // This method moves baggage around in the array when a bag has been removed from the array
        void MoveBaggageOnConveyorBelt()
        {
            for (int i = 0; i < conveyorBelt.Length - 1; i++)
            {
                conveyorBelt[i] = conveyorBelt[i + 1];
            }
            bufferCounter -= 1;
        }
        #endregion
    }
}
