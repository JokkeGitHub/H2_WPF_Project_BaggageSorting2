using System;

namespace H2_WPF_Project_BaggageSorting2
{
    public class CentralServer
    {
        // This class represents a database server

        static FlightPlan[] flightPlanList = new FlightPlan[]
        {
            new FlightPlan(101, "Stockholm", DateTime.Now.AddSeconds(60)),
            new FlightPlan(102, "Berlin", DateTime.Now.AddSeconds(60)),
            new FlightPlan(103, "Paris", DateTime.Now.AddSeconds(120)),
            new FlightPlan(104, "London", DateTime.Now.AddSeconds(120)),
            new FlightPlan(105, "Hong Kong", DateTime.Now.AddSeconds(120)),
            new FlightPlan(106, "Washington D.C.", DateTime.Now.AddSeconds(180)),
            new FlightPlan(107, "Rome", DateTime.Now.AddSeconds(180)),
            new FlightPlan(108, "Vienna", DateTime.Now.AddSeconds(240)),
            new FlightPlan(109, "Tokyo", DateTime.Now.AddSeconds(240)),
            new FlightPlan(110, "Oslo", DateTime.Now.AddSeconds(240))
        };

        static Reservation[] reservationList = new Reservation[]
        {
            new Reservation("Jane Fox", 1001, flightPlanList[0].FlightNumber, flightPlanList[0].Departure),
            new Reservation("Gerald Cucumber", 1002, flightPlanList[0].FlightNumber, flightPlanList[0].Departure),
            new Reservation("Will Smith", 1003, flightPlanList[1].FlightNumber, flightPlanList[1].Departure),
            new Reservation("Neo Anderson", 1004, flightPlanList[1].FlightNumber, flightPlanList[1].Departure),
            new Reservation("Fox Mulder", 1005, flightPlanList[1].FlightNumber, flightPlanList[1].Departure),
            new Reservation("Donald Trump", 1006, flightPlanList[2].FlightNumber, flightPlanList[2].Departure),
            new Reservation("Vladimir Putin", 1007, flightPlanList[2].FlightNumber, flightPlanList[2].Departure),
            new Reservation("Winnie the Pooh", 1008, flightPlanList[2].FlightNumber, flightPlanList[2].Departure),
            new Reservation("Ted Mosby", 1009, flightPlanList[3].FlightNumber, flightPlanList[3].Departure),
            new Reservation("Barney Stinson", 1010, flightPlanList[3].FlightNumber, flightPlanList[3].Departure),
            new Reservation("Marshall Erikson", 1011, flightPlanList[3].FlightNumber, flightPlanList[3].Departure),
            new Reservation("Ching Chong", 1012, flightPlanList[4].FlightNumber, flightPlanList[4].Departure),
            new Reservation("Doge Dog", 1013, flightPlanList[4].FlightNumber, flightPlanList[4].Departure),
            new Reservation("Thomas Train", 1014, flightPlanList[4].FlightNumber, flightPlanList[4].Departure),
            new Reservation("Wario Bro", 1015, flightPlanList[4].FlightNumber, flightPlanList[4].Departure),
            new Reservation("Clark Kent", 1016, flightPlanList[0].FlightNumber, flightPlanList[0].Departure),
            new Reservation("Bruce Wayne", 1017, flightPlanList[0].FlightNumber, flightPlanList[0].Departure),
            new Reservation("James Bond", 1018, flightPlanList[4].FlightNumber, flightPlanList[4].Departure),
            new Reservation("Bugs Bunny", 1019, flightPlanList[1].FlightNumber, flightPlanList[1].Departure),
            new Reservation("Daffy Duck", 1020, flightPlanList[1].FlightNumber, flightPlanList[1].Departure),
            new Reservation("Darth Vader", 1021, flightPlanList[3].FlightNumber, flightPlanList[3].Departure),
            new Reservation("Darth Sidius", 1022, flightPlanList[2].FlightNumber, flightPlanList[2].Departure),
            new Reservation("Count Dooku", 1023, flightPlanList[2].FlightNumber, flightPlanList[2].Departure),
            new Reservation("Obi-Wan Kenobi", 1024, flightPlanList[3].FlightNumber, flightPlanList[3].Departure),
            new Reservation("Peter Parker", 1025, flightPlanList[5].FlightNumber, flightPlanList[5].Departure),
            new Reservation("Tony Stark", 1026, flightPlanList[6].FlightNumber, flightPlanList[6].Departure),
            new Reservation("Steve Rogers", 1027, flightPlanList[7].FlightNumber, flightPlanList[7].Departure),
            new Reservation("Hannibal Lector", 1028, flightPlanList[8].FlightNumber, flightPlanList[8].Departure),
            new Reservation("The Joker", 1029, flightPlanList[9].FlightNumber, flightPlanList[9].Departure),
            new Reservation("Harley Quinn", 1030, flightPlanList[9].FlightNumber, flightPlanList[9].Departure),
            new Reservation("Poison Ivy", 1031, flightPlanList[8].FlightNumber, flightPlanList[8].Departure),
            new Reservation("Viktor Friez", 1032, flightPlanList[7].FlightNumber, flightPlanList[7].Departure),
            new Reservation("Marty McFly", 1033, flightPlanList[6].FlightNumber, flightPlanList[6].Departure),
            new Reservation("Emmet Brown", 1034, flightPlanList[5].FlightNumber, flightPlanList[5].Departure),
            new Reservation("Fred Flintstone", 1035, flightPlanList[5].FlightNumber, flightPlanList[5].Departure),
            new Reservation("Barney Pebbles", 1036, flightPlanList[6].FlightNumber, flightPlanList[6].Departure),
            new Reservation("Bruce Banner", 1037, flightPlanList[7].FlightNumber, flightPlanList[7].Departure),
            new Reservation("John McClane", 1038, flightPlanList[8].FlightNumber, flightPlanList[8].Departure),
            new Reservation("Ellen Ripley", 1039, flightPlanList[9].FlightNumber, flightPlanList[9].Departure),
            new Reservation("Jason Vorhees", 1040, flightPlanList[9].FlightNumber, flightPlanList[9].Departure),
            new Reservation("Freddy Krueger", 1041, flightPlanList[8].FlightNumber, flightPlanList[8].Departure),
            new Reservation("Betty Boop", 1042, flightPlanList[7].FlightNumber, flightPlanList[7].Departure),
            new Reservation("Bilbo Baggins", 1043, flightPlanList[6].FlightNumber, flightPlanList[6].Departure),
            new Reservation("Optimus Prime", 1044, flightPlanList[5].FlightNumber, flightPlanList[5].Departure),
            new Reservation("Eric Cartman", 1045, flightPlanList[5].FlightNumber, flightPlanList[5].Departure),
            new Reservation("Stanley Marsh", 1046, flightPlanList[6].FlightNumber, flightPlanList[6].Departure),
            new Reservation("Mary Poppins", 1047, flightPlanList[7].FlightNumber, flightPlanList[7].Departure),
            new Reservation("Charlie Brown", 1048, flightPlanList[8].FlightNumber, flightPlanList[8].Departure),
            new Reservation("Rocky Balboa", 1049, flightPlanList[9].FlightNumber, flightPlanList[9].Departure),
            new Reservation("John Doe", 1050, flightPlanList[0].FlightNumber, flightPlanList[0].Departure)
        };

        public FlightPlan[] GetFlightPlan()
        {
            return (FlightPlan[])flightPlanList.Clone();
        }

        public Reservation[] GetReservations()
        {
            return (Reservation[])reservationList.Clone();
        }
    }
}
