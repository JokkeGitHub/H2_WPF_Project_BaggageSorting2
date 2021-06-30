namespace H2_WPF_Project_BaggageSorting2
{
    public class ReceptionFactory
    {
        // This class is our reception factory, and is responsible for creating reception objects

        static int receptionNumber = 0;

        public Reception Create()
        {
            receptionNumber += 1;
            string counterName = $"Counter{receptionNumber}";            

            return new Reception(counterName, true);
        }
    }
}
