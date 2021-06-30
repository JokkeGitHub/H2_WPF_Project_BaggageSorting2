namespace H2_WPF_Project_BaggageSorting2
{
    // This class is our baggage factory, and is responsible for creating baggage objects

    public class BaggageFactory
    {
        static int id = 10000;

        public Baggage Create(int passengerId, int flightNumber)
        {
            id += 1;
            return new Baggage(id, passengerId, flightNumber);
        }
    }
}
