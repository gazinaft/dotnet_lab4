using System.Collections;
using System.Collections.Generic;

namespace lab4
{
    /// <summary>
    /// A route of public transport
    /// </summary>
    class Route
    {
        /// <summary>
        /// First stop in the route
        /// </summary>
        public Stop Start {get; private set;}
        
        /// <summary>
        /// Last stop in the route
        /// </summary>
        public Stop End {get; private set;}

        /// <summary>
        /// Time to get from first to last stop
        /// </summary>
        public int Time {get; private set;}

        /// <summary>
        /// Start and End of the route
        /// </summary>
        public string FullRoute {
            get {
                return Start.Name + "-" + End.Name;
            }
        }
        
        /// <summary>
        /// Identifier of a route
        /// </summary>
        public int RouteNum {get; private set;}
        /// <summary>
        /// Constructor
        /// </summary>
        public Route(int number, Stop start, Stop end, int time)
        {
            RouteNum = number;
            Start = start;
            End = end; 
            Time = time;
        }

    }
}