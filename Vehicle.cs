namespace lab4
{
    /// <summary>
    /// Abstract class to describe public transport
    /// </summary>
    abstract class Vehicle
    {
        /// <summary>
        /// Unique serial number to identify a vehicle
        /// </summary>
        public string SerialNum { get; private set; }

        /// <summary>
        /// The current route the vehicle follows
        /// </summary>
        public int ActiveRoute { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serialNum">Serial number of this transport</param>
        /// <param name="route">Current route</param>
        public Vehicle(string serialNum, int route)
        {
            SerialNum = serialNum;
            ActiveRoute = route;
        }
    }

    /// <summary>
    /// Class of public transport: Trolley 
    /// </summary>
    class Trolley : Vehicle
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Trolley(string serialNum, int route) : base(serialNum, route) {}

        /// <summary>
        /// Override to string method to print type of vehicle and its serial number
        /// </summary>
        public override string ToString()
        {
            return "Trolley " + SerialNum;
        }
    }
}
