using System.Collections.Generic;

namespace lab4
{
    /// <summary>
    /// Depot of public transport of type T
    /// </summary>
    class Depot<T> where T: Vehicle
    {
        /// <summary>
        /// List of transport routes in depot
        /// </summary>
        public List<Route<T>> Routes {get; set;}

        /// <summary>
        /// Constructor
        /// </summary>
        public Depot()
        {
            Routes = new List<Route<T>>();
        }

        /// <summary>
        /// Convenient way to add new routes to the depot
        /// </summary>
        /// <param name="route">Route to add to depot</param>
        /// <returns>Instance of depot to chain function calls</returns>
        public Depot<T> AddRoute(Route<T> route)
        {
            Routes.Add(route);
            return this;
        }

    }
}