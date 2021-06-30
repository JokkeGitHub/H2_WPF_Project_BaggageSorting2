using System;
using System.Diagnostics;
using System.Threading;

namespace H2_WPF_Project_BaggageSorting2
{
    public class GateController
    {
        // This class is responsible for the threads which run the gates

        Random random = new Random();

        ConveyorBeltGateController conveyorBeltGateController = new ConveyorBeltGateController();
        static CentralServer centralServer = new CentralServer();

        static FlightPlan[] flightPlan = centralServer.GetFlightPlan();
        int remainingFlightPlans = flightPlan.Length;
        object _lockFlightPlan = new object();

        int bufferBaggageCart = -1;

        #region Event listeners
        // our event listeners
        public EventHandler OpenOrClosedGate1;
        public EventHandler OpenOrClosedGate2;
        public EventHandler OpenOrClosedGate3;
        
        public EventHandler BaggageArrivedGate1;
        public EventHandler BaggageArrivedGate2;
        public EventHandler BaggageArrivedGate3;
        #endregion

        // This is where we start our threads
        public GateController()
        {
            for (int i = 1; i <= 3; i++)
            {
                Thread gateThread = new Thread(() => StartGate());
                gateThread.Start();
            }
        }

        // This method is called by threads when they start
        private void StartGate()
        {
            GateFactory gateFactory = new GateFactory();
            Gate gate = gateFactory.Create();

            while (true)
            {
                OpenOrClose(gate);
            }
        }

        private void OpenOrClose(Gate gate)
        {
            gate.Open = gate.OpenOrClosed(gate.Open, remainingFlightPlans);
            OpenClosedDetermineListener(gate);

            Thread.Sleep(random.Next(500, 2000));
            GetFlightPlanInfo(gate);
        }

        // This method is responsible for retrieving all the necessary info and passing them on to the gate
        private void GetFlightPlanInfo(Gate gate)
        {
            Monitor.Enter(_lockFlightPlan);
            try
            {
                if (gate.Open == true)
                {
                    gate.FlightNumber = flightPlan[0].FlightNumber;
                    gate.Destination = flightPlan[0].Destination;
                    gate.Departure = flightPlan[0].Departure;

                    Debug.WriteLine($"{gate.GateName} flight {gate.FlightNumber} arrived. Detination {gate.Destination}, departs at {gate.Departure}");
                    conveyorBeltGateController.AddFlightNumber(gate);

                    NextFlightPlan();
                }

                Monitor.PulseAll(_lockFlightPlan);
            }
            finally
            {
                Monitor.Exit(_lockFlightPlan);
            }

            OpenClosedDetermineListener(gate);
            PlaneBoarding(gate);
        }

        // This method is responsible for the "Plane boarding", before departure, all the baggage is collected from the buffer, 
        // and is forwarded to the BaggageCart arrays
        private void PlaneBoarding(Gate gate)
        {
            if (gate.Open == true)
            {
                Baggage baggage = new Baggage(0, 0, 0);
                while (DateTime.Now < gate.Departure)
                {
                    baggage = conveyorBeltGateController.GetBaggage(gate);

                    if (baggage != null)
                    {
                        baggage.ArrivedAtGate = DateTime.Now;

                        bufferBaggageCart = +1;
                        gate.BaggageCart[bufferBaggageCart] = baggage;

                        BaggageArrivedInGate(gate, baggage);
                        Debug.WriteLine($"Bag {baggage.BaggageId} arrived in {gate.GateName} at {baggage.ArrivedAtGate} for flight {baggage.FlightNumber}");
                    }
                }

                PlaneLeaves(gate);
            }
        }

        // This method is resetting the buffer counter and the baggageCart arrays
        private void PlaneLeaves(Gate gate)
        {
            gate.BaggageCart = null;
            bufferBaggageCart = -1;
            Debug.WriteLine($"Flight {gate.FlightNumber}, destination {gate.Destination} has left {gate.GateName} at {gate.Departure}");
        }

        // When this method is called it moves the flightplans around in the array, and decreases
        // the flightplan counter
        private void NextFlightPlan()
        {
            Monitor.Enter(_lockFlightPlan);
            try
            {
                for (int i = 0; i < flightPlan.Length - 1; i++)
                {
                    flightPlan[i] = flightPlan[i + 1];
                }
                remainingFlightPlans -= 1;
            }
            finally
            {
                Monitor.Exit(_lockFlightPlan);
            }
        }

        #region Listener Methods
        // This checks the gate name, then invokes the corresponding listeners
        private void OpenClosedDetermineListener(Gate gate)
        {
            switch (gate.GateName)
            {
                case "Gate1":
                    OpenOrClosedGate1?.Invoke(this, new GateEvent(gate));
                    break;

                case "Gate2":
                    OpenOrClosedGate2?.Invoke(this, new GateEvent(gate));
                    break;

                case "Gate3":
                    OpenOrClosedGate3?.Invoke(this, new GateEvent(gate));
                    break;

                default:
                    break;
            }
        }

        // This checks the gate name, then invokes the corresponding listeners
        private void BaggageArrivedInGate(Gate gate, Baggage baggage)
        {
            switch (gate.GateName)
            {
                case "Gate1":
                    BaggageArrivedGate1?.Invoke(this, new BaggageEvent(baggage));
                    break;

                case "Gate2":
                    BaggageArrivedGate2?.Invoke(this, new BaggageEvent(baggage));
                    break;

                case "Gate3":
                    BaggageArrivedGate3?.Invoke(this, new BaggageEvent(baggage));
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
