using System.Collections;
using System.Collections.Generic;

namespace lab4
{
    /// <summary>
    /// Represents a route of public transport
    /// </summary>
    class Route<T> where T: Vehicle
    {
        public List<T> AutoPark {get; private set;} 
        public Stop Start {get; private set;}
        public Stop End {get; private set;}

        public int Time {get; private set;}


        public int AutoParkSize
        {
            get {
                return AutoPark.Count;
            }
        }

        public string FullRoute {
            get {
                return Start.Name + "-" + End.Name;
            }
        }


        public Route(List<T> vehicles, Stop start, Stop end, int time)
        {
            AutoPark = vehicles;
            Start = start;
            End = end; 
            Time = time;
        }

    }
}