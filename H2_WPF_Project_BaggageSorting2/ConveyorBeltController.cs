using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace H2_WPF_Project_BaggageSorting2
{
    public class ConveyorBeltController
    {
        public static int bufferCounter = -1;
        public static Baggage[] conveyorBelt = new Baggage[50];

        static object _lockConveyorBelt = new object();

        public Baggage[] GetConveyorBelt()
        {
            return (Baggage[])conveyorBelt.Clone();
        }

        public void AddBagToConveyorBelt(Baggage baggage, string counterName)
        {
            Monitor.Enter(_lockConveyorBelt);
            try
            {
                bufferCounter += 1;
                conveyorBelt[bufferCounter] = baggage;
                Debug.WriteLine($"Bag {conveyorBelt[bufferCounter].BaggageId}, arrived at conveyor belt, from {counterName}");

                /*MainWindow mainWindow = new MainWindow();
                mainWindow.ConveyorTest();*/

                Monitor.PulseAll(_lockConveyorBelt);
            }
            finally
            {
                Monitor.Exit(_lockConveyorBelt);
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
        }

        void MoveBaggageOnConveyorBelt()
        {
            for (int i = 0; i < conveyorBelt.Length - 1; i++)
            {
                conveyorBelt[i] = conveyorBelt[i + 1];

                // BAGGAGE EVENT HERE ?????? ? :D
            }
            bufferCounter -= 1;
        }
        #endregion
    }
}
