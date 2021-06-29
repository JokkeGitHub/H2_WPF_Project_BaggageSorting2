﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace H2_WPF_Project_BaggageSorting2
{
    public class GateController
    {
        // This class is responsible for receiving bags
        Random random = new Random();

        ConveyorBeltGateController conveyorBeltGateController = new ConveyorBeltGateController();

        static CentralServer centralServer = new CentralServer();
        static FlightPlan[] flightPlan = centralServer.GetFlightPlan();
        int remainingFlightPlans = flightPlan.Length;
        object _lockFlightPlan = new object();

        public EventHandler OpenOrClosedGate1;
        public EventHandler OpenOrClosedGate2;
        public EventHandler OpenOrClosedGate3;
        
        public EventHandler BaggageArrivedGate1;
        public EventHandler BaggageArrivedGate2;
        public EventHandler BaggageArrivedGate3;

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
                gate.Open = gate.OpenOrClosed(gate.Open, remainingFlightPlans);
                GetFlightPlanInfo(gate);
            }
        }

        private void GetFlightPlanInfo(Gate gate)
        {
            Monitor.Enter(_lockFlightPlan);
            try
            {
                // If remainingFlightPlans == 0, No more
                //else if v
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

                        BaggageArrivedInGate(gate, baggage);
                        Debug.WriteLine($"Bag {baggage.BaggageId} arrived in {gate.GateName} at {baggage.ArrivedAtGate} for flight {baggage.FlightNumber}");
                    }
                }

                Debug.WriteLine($"Flight {gate.FlightNumber}, destination {gate.Destination} has left {gate.GateName} at {gate.Departure}");
            }
        }

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

        /*
        private void StartGate()
        {
            GateFactory gateFactory = new GateFactory();
            Gate gate = gateFactory.Create();

            while (true)
            {

                Thread.Sleep(random.Next(700, 4000));
                GetFlightPlanInfo(gate);

                Thread.Sleep(random.Next(200, 2000));
                gate.Open = gate.OpenOrClosed(gate.Open, remainingFlightPlans);
                OpenClosedDetermineListener(gate);


            }
        }

        private void GetFlightPlanInfo(Gate gate)
        {
            DateTime departure = DateTime.Now;

            Monitor.Enter(_lockFlightPlan);
            try
            {
                if (remainingFlightPlans == 0)
                {
                    Debug.WriteLine("No more flights");
                }
                else
                {
                    gate.FlightNumber = flightPlan[0].FlightNumber;
                    Debug.WriteLine($"{gate.GateName} received Flight Number {gate.FlightNumber}");
                    departure = flightPlan[0].Departure;

                    NextFlightPlan();

                    Thread.Sleep(random.Next(100, 1000));
                    Monitor.PulseAll(_lockFlightPlan);

                }
            }
            finally
            {
                Monitor.Exit(_lockFlightPlan);
            }

            FlightNumberDetermineListener(gate);

        }
        */
        // New class for these?
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

        /*
        private void FlightNumberDetermineListener(Gate gate)
        {
            switch (gate.GateName)
            {
                case "Gate1":
                    FlightPlanGate1?.Invoke(this, new GateEvent(gate));
                    break;

                case "Gate2":
                    FlightPlanGate2?.Invoke(this, new GateEvent(gate));
                    break;

                case "Gate3":
                    FlightPlanGate3?.Invoke(this, new GateEvent(gate));
                    break;

                default:
                    break;
            }
        }*/
    }
}
