using System.Collections.Generic;

namespace lab4
{
    class TrolleyFactory
    {
        private int CurrentVal {get; set;}
        private int Increment {get; set;}
        public TrolleyFactory()
        {
            CurrentVal = 0;
            Increment = 1;
        }

        public List<Trolley> Produce(int count = 1) {
            List<Trolley> res = new List<Trolley>();
            for (int i = 0; i < count; ++i) {
                res.Add(item: new Trolley(CurrentVal.ToString()));
                CurrentVal += Increment;
            }
            return res;
        }
    }
    
    class RouteFactory
    {
        private TrolleyFactory fact;

        public RouteFactory()
        {
            fact = new TrolleyFactory();
        }

        public Route<Trolley> Create(int trolleyCount, string start, string end, int time)
        {
            return new Route<Trolley>(fact.Produce(trolleyCount), new Stop(start), new Stop(end), time);
        } 
    }

}