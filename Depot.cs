using System.Collections.Generic;

namespace lab4
{
    /// <summary>
    /// Transport System of public transport of type T
    /// </summary>
    class TransportSystem<T> where T: Vehicle
    {
        /// <summary>
        /// List of transport routes in Transport System
        /// </summary>
        public List<T> Transport {get; set;}

        public List<Route> Routes {get; set;}

        /// <summary>
        /// Constructor
        /// </summary>
        public TransportSystem()
        {
            Transport = new List<T>();
            Routes = new List<Route>();
        }

        /// <summary>
        /// Convenient way to add new transport to the Transport System one at a time
        /// </summary>
        /// <param name="vehicle">Transport to add to TransportSystem</param>
        /// <returns>Instance of TransportSystem to chain function calls</returns>
        public TransportSystem<T> AddTransport(T vehicle)
        {
            Transport.Add(vehicle);
            return this;
        }

        /// <summary>
        /// Convenient way to add new route to the Transport System one at a time
        /// </summary>
        /// <param name="route">Route to add to TransportSystem</param>
        /// <returns>Instance of TransportSystem to chain function calls</returns>
        public TransportSystem<T> AddRoute(Route route)
        {
            Routes.Add(route);
            return this;
        }

        /// <summary>
        /// Convenient way to add new transport to the Transport System in a list
        /// </summary>
        /// <param name="vehicles">Transport to add to TransportSystem</param>
        /// <returns>Instance of TransportSystem to chain function calls</returns>
        public TransportSystem<T> AddTransport(List<T> vehicles)
        {
            Transport.AddRange(vehicles);
            return this;
        }

    }
}