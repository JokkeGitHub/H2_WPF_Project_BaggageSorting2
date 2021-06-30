namespace H2_WPF_Project_BaggageSorting2
{
    public class GateFactory
    {
        // This class is our gate factory, and is responsible for creating gate objects

        static int gateNumber = 0;

        public Gate Create()
        {
            gateNumber += 1;
            string gateName = $"Gate{gateNumber}";
            Baggage[] baggageCart = new Baggage[15];

            return new Gate(gateName, false, 0, "", baggageCart);
        }
    }
}
