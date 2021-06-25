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
        Random random = new Random();

        static CentralServer centralServer = new CentralServer();
        static FlightPlan[] flightPlan = centralServer.GetFlightPlan();
        static int remainingFlightPlans = flightPlan.Length;

        public EventHandler OpenOrClosedGate1;
        public EventHandler OpenOrClosedGate2;
        public EventHandler OpenOrClosedGate3;

        public EventHandler FlightPlanGate1;
        public EventHandler FlightPlanGate2;
        public EventHandler FlightPlanGate3;

        public GateController()
        {
            for (int i = 1; i <= 3; i++)
            {
                Thread gateThread = new Thread(() => StartGate());
                gateThread.Start();
            }
        }

        private void StartGate()
        {
            GateFactory gateFactory = new GateFactory();
            Gate gate = gateFactory.Create();

            while (true)
            {
                if (gate.Open == true)
                {
                    Thread.Sleep(random.Next(700, 4000));
                    GetFlightPlanInfo(gate);
                }

                Thread.Sleep(random.Next(300, 2500));
                gate.Open = gate.OpenOrClosed(gate.Open, remainingFlightPlans);
                OpenClosedDetermineListener(gate);
            }
        }

        private void GetFlightPlanInfo(Gate gate)
        {

        }
        
        // New class for these?
        private void OpenClosedDetermineListener(Gate gate)
        {
            switch (gate.GateName)
            {
                case "Counter1":
                    OpenOrClosedGate1?.Invoke(this, new GateEvent(gate));
                    break;

                case "Counter2":
                    OpenOrClosedGate2?.Invoke(this, new GateEvent(gate));
                    break;

                case "Counter3":
                    OpenOrClosedGate3?.Invoke(this, new GateEvent(gate));
                    break;

                default:
                    break;
            }
        }
    }
}
