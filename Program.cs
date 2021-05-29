using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{
    class Program
    {
        static void SpaceAround(char delimiter = '=')
        {
            Console.WriteLine(new String(delimiter, 15));
        }
        static void Main(string[] args)
        {
            var fact = new RouteFactory();
            var depot = new Depot<Trolley>()
                .AddRoute(fact.Create(5, "Poznyaky", "Akademmistechko", 25))
                .AddRoute(fact.Create(6, "Poznyaky", "KPI", 40))
                .AddRoute(fact.Create(4, "Poznyaky", "Polyana", 25))
                .AddRoute(fact.Create(3, "Bucha", "Poznyaky", 12))
                .AddRoute(fact.Create(2, "KPI", "Polyana", 1)); 

            // select
            // 1 select all trolley numbers
            var q1 = depot.Routes.Select(route => route.AutoPark.Select(trolley => trolley.SerialNum));
            foreach(var row in q1) {
                foreach (var elem in row) {
                    Console.Write(elem + ", ");
                }
                Console.WriteLine();
            }
            SpaceAround();
            // sort
            // 2 select all routes sorted by time ascending and autiparkSize descending
            var q2 = from route in depot.Routes
                orderby route.Time ascending, route.AutoParkSize descending
                select  route.FullRoute;
            foreach (var route in q2) {
                Console.WriteLine(route);
            }
            SpaceAround();

            // filter
            // 3 select routes without "Poznyaki"
            var q3 = depot.Routes.Where(route => route.End.Name != "Poznyaky" && route.Start.Name != "Poznyaky")
                .Select(route => route.FullRoute);
            foreach (var route in q3) {
                Console.WriteLine(route);
            }
            SpaceAround();
            
            // group by
            // 4 select grouped by start stop routes
            var q4 = from route in depot.Routes
                    group route.FullRoute
                    by route.End.Name;
            foreach (var route in q4) {
                foreach(var group in route) Console.WriteLine(group);
                SpaceAround('-');
            }
            SpaceAround();

            // filter + union
            // 5 select routes which are shorter than 13 minutes long and longer than 30
            var q5 = depot.Routes.Where(route => route.Time < 13)
                .Union(depot.Routes.Where(route => route.Time > 30));
                foreach (var route in q5) {
                    Console.WriteLine(route.FullRoute);
                }
                SpaceAround();

            // join
            // 6 select routes joined by first and last stop
            var q6 = from route in depot.Routes
                join route2 in depot.Routes on route.End.Name equals route2.Start.Name
                select route.FullRoute + "-" + route2.End.Name;
            foreach (var route in q6) {
                Console.WriteLine(route);
            }
            SpaceAround();

            // sorting
            // 7 select top 2 shortest rides
            var q7 = depot.Routes.OrderBy(route => route.Time).Take(2);
            foreach (var route in q7) {
                Console.WriteLine(route.FullRoute);
            }
            SpaceAround();

            // aggregation
            // 8 select how much stops contain KPI
            var q8 = depot.Routes.Count(route => route.FullRoute.Contains("KPI"));
            Console.WriteLine(q8);
            SpaceAround();
            
            // sorting + aggregation
            // 9 select top 3 routes by bus count, which serial num is more than 12
            var q9 = (from route in depot.Routes
                orderby route.AutoPark.Count(bus => Convert.ToInt32(bus.SerialNum) > 12) descending
                select route.FullRoute).Take(3);
            foreach (var route in q9) {
                Console.WriteLine(route);
            }
            SpaceAround();

            // aggregation + filtration
            // 10 count how many busses which serial number is less than 10 attend KPI station
            var q10 = (from route in depot.Routes
                where route.FullRoute.Contains("KPI")
                select route.AutoPark.Count(trolley => Convert.ToInt32(trolley.SerialNum) < 10)).Sum();
            Console.WriteLine(q10);
            SpaceAround();

            // intersect + selectmany + filter
            // 11 select busses which route is less than 30 minutes long and more than 10 minutes long
            var q11 = depot.Routes.Where(r => r.Time > 10)
                .Intersect(depot.Routes.Where(r => r.Time < 30)).SelectMany(route => route.AutoPark);
            foreach (var bus in q11) {
                Console.WriteLine(bus);
            }
            SpaceAround();

            // grouping + aggregation
            // 12 select average time to ride from every start station
            var q12 = depot.Routes.GroupBy(route => route.Start.Name).Select(route => route.Average(rt => rt.Time));
            foreach (var route in q12) {
                Console.WriteLine(route);
                SpaceAround('-');
            }
            SpaceAround();

            // aggregation
            // 13 check skip routes while they have busses with number < 15 
            var q13 = depot.Routes.SkipWhile(route => route.AutoPark.Any(bus => Convert.ToInt32(bus.SerialNum) < 15))
                .Select(route => route.FullRoute);
            foreach (var route in q13) {
                Console.WriteLine(route);
            }
            SpaceAround();

            // join + filter + aggregation
            // 14 count cirlce routes
            var q14 = (from route in depot.Routes
                join route2 in depot.Routes on route.End.Name equals route2.Start.Name
                where route.Start.Name == route2.End.Name
                select route).Count();
            Console.WriteLine(q14);
            SpaceAround();

            //15 select all unique stops
            var q15 = depot.Routes.Select(route => route.Start.Name)
                .Union(depot.Routes.Select(route => route.End.Name)).Aggregate((acc, val) => acc + "|" + val);
            Console.WriteLine(q15);
        }
    }
}
