using System.Collections.Generic;

namespace lab4
{
    class TrolleyFactory
    {
        private int CurrentVal;
        private int Increment;
        public TrolleyFactory()
        {
            CurrentVal = 0;
            Increment = 1;
        }

        public List<Trolley> Create(int routeNum, int bussesPerRoute = 1) {
            List<Trolley> res = new List<Trolley>();
            for (int i = 0; i < bussesPerRoute; ++i) {
                res.Add(item: new Trolley(CurrentVal.ToString(), routeNum));
                CurrentVal += Increment;
            }
            return res;
        }
    }
    
    class RouteFactory
    {
        private int StartValue;
        private int Increment;

        public RouteFactory()
        {
            StartValue = 0;
            Increment = 1;
        }

        public Route Create(string start, string end, int time)
        {
            StartValue += Increment;
            return new Route(StartValue, new Stop(start), new Stop(end), time);
        } 
    }

}