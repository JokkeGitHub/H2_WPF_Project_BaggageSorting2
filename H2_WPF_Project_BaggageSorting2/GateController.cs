using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace H2_WPF_Project_BaggageSorting2
{
    public class GateController
    {
        // This class is responsible for receiving bags

        static CentralServer centralServer = new CentralServer();
        static FlightPlan[] flightPlan = centralServer.GetFlightPlan();
        static int remainingFlightPlans = flightPlan.Length;

        public EventHandler FlightPlanGate1;
        public EventHandler FlightPlanGate2;
        public EventHandler FlightPlanGate3;

        public GateController()
        {
            for (int i = 1; i <= 3; i++)
            {
                Thread gateThread = new Thread(() => PlaneArrival());
                gateThread.Start();
            }
        }

        private void PlaneArrival()
        {
            GateFactory gateFactory = new GateFactory();
            Gate gate = gateFactory.Create();

            while (true)
            {

            }
        }
    }
}
