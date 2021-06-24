using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    public class ConveyorBelt
    {
        public static int bufferCounter = -1;
        static Baggage[] conveyorBelt = new Baggage[12];
        public EventHandler ConveyorBeltUpdate;

        void AddBagToConveyorBelt(Baggage baggage, string counterName)
        {
            //Monitor.Enter(_lockConveyorBelt);
            try
            {
                bufferCounter += 1;
                conveyorBelt[bufferCounter] = baggage;
                Debug.WriteLine($"{counterName} bag {baggage.BaggageId}, arrived at conveyor belt.");
                //Monitor.PulseAll(_lockConveyorBelt);
            }
            finally
            {
                //Monitor.Exit(_lockConveyorBelt);
            }
        }

        #region SPLITTERS CONVEYOR METHODS
        public Baggage GetBaggage(Baggage baggage)
        {
            if (conveyorBelt[0] == null)
            {
                return null;
            }
            else
            {
                //Monitor.Enter(_lockConveyorBelt);

                try
                {
                    baggage = conveyorBelt[0];
                    

                   // Monitor.PulseAll(_lockConveyorBelt);
                }
                finally
                {
                    //Monitor.Exit(_lockConveyorBelt);
                }

                MoveBaggageOnConveyorBelt();

                return baggage;
            }
        }

        void MoveBaggageOnConveyorBelt()
        {
            //Monitor.Enter(_lockConveyorBelt);

            try
            {
                for (int i = 0; i < conveyorBelt.Length - 1; i++)
                {
                    conveyorBelt[i] = conveyorBelt[i + 1];
                }

                bufferCounter -= 1;
            }
            finally
            {
                //Monitor.Exit(_lockConveyorBelt);
            }
        }
        #endregion
    }
}
