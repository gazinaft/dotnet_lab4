using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;

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
            var routeFact = new RouteFactory();
            var trolleyFact = new TrolleyFactory();
            var autoPark = new TransportSystem<Trolley>();
            autoPark
                .AddRoute(routeFact.Create("KPI", "Poznyaki", 35))
                .AddRoute(routeFact.Create("Polyova", "Vokzalna", 5))
                .AddRoute(routeFact.Create("Bucha", "KPI", 55))
                .AddRoute(routeFact.Create("Vokzalna", "Obolon", 35))
                .AddRoute(routeFact.Create("Bucha", "Vokzalna", 15));
            autoPark
                .AddTransport(trolleyFact.Create(1, 5))
                .AddTransport(trolleyFact.Create(2, 3))
                .AddTransport(trolleyFact.Create(3, 2))
                .AddTransport(trolleyFact.Create(4, 5))
                .AddTransport(trolleyFact.Create(5, 4));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create("pTransport.xml", settings))
            {
                writer.WriteStartElement("transportSystem");
                foreach (var route in autoPark.Routes)
                {
                    writer.WriteStartElement("route");
                    writer.WriteAttributeString("routeNum", route.RouteNum.ToString());
                    writer.WriteAttributeString("startStop", route.Start.Name);
                    writer.WriteAttributeString("endStop", route.End.Name);
                    writer.WriteAttributeString("time", route.Time.ToString());
                    writer.WriteEndElement();
                }
                foreach (var trolley in autoPark.Transport)
                {
                    writer.WriteStartElement("trolley");
                    writer.WriteAttributeString("serialNum", trolley.SerialNum);
                    writer.WriteAttributeString("routeNum", trolley.ActiveRoute.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            var xmlDoc = XDocument.Load("pTransport.xml");

            // q1 all unique stops sorted ascending
            var q1 = xmlDoc.Descendants("route").Select(p => p.Attribute("startStop").Value).Distinct().OrderBy(x => x);
            foreach (var trolley in q1) {
                System.Console.WriteLine(trolley);
            }
            SpaceAround();

            // q2 group busses by routes
            var q2 = xmlDoc.Descendants("route").GroupJoin(
                xmlDoc.Descendants("trolley"),
                route => route.Attribute("routeNum").Value,
                trolley => trolley.Attribute("routeNum").Value,
                (route, trolleys) => new { Start=route.Attribute("startStop").Value, SerialNum=trolleys.Select(tr => tr.Attribute("serialNum").Value) }
                );
            foreach (var info in q2) {
                System.Console.WriteLine("From {0} start busses", info.Start);
                foreach (var serialNum in info.SerialNum) {
                    System.Console.WriteLine(serialNum);
                }
            }
            SpaceAround();

            // q3 how many busses does each route has
            var q3 = xmlDoc.Descendants("trolley").Select(p => p.Attribute("routeNum").Value).GroupBy(x => x).Select(x => x.Count());
            foreach (var busCount in q3)
            {
                System.Console.WriteLine(busCount);
            }
            SpaceAround();

            // q4 busses which route is longer than 15 m
            var q4 = from trolley in xmlDoc.Descendants("trolley")
                join route in xmlDoc.Descendants("route") on trolley.Attribute("routeNum").Value equals route.Attribute("routeNum").Value
                where Convert.ToInt32(route.Attribute("time").Value) < 15
                select trolley.Attribute("serialNum").Value + "-" + route.Attribute("startStop").Value + "-" + route.Attribute("endStop").Value;
            foreach (var route in q4)
            {
                System.Console.WriteLine(route);
            }
            SpaceAround();

            // q5 select top 3 shortest routes
            var q5 = xmlDoc.Descendants("route").OrderBy(x => x.Attribute("time").Value).Select(x => x.Attribute("routeNum").Value).Take(3);
            foreach (var route in q5)
            {
                System.Console.WriteLine(route);
            }
            SpaceAround();
            
            // q6 join
            var q6 = from r1 in xmlDoc.Descendants("route")
                join r2 in xmlDoc.Descendants("route") on r1.Attribute("endStop").Value equals r2.Attribute("startStop").Value
                select r1.Attribute("startStop").Value + "-" + r2.Attribute("startStop").Value + "-" + r2.Attribute("endStop").Value;
            
            foreach (var route in q6)
            {
                System.Console.WriteLine(route);
            }
            SpaceAround();

            // q7 count busses who attend KPI
            var q7 = xmlDoc.Descendants("route").Count(x => x.Attribute("startStop").Value == "KPI" || x.Attribute("endStop").Value == "KPI");
            System.Console.WriteLine(q7);
            SpaceAround();

            // q8 check whether old busses take short rides
            var q8 = (from trolley in xmlDoc.Descendants("trolley")
                join route in xmlDoc.Descendants("route") on trolley.Attribute("routeNum").Value equals route.Attribute("routeNum").Value
                where Convert.ToInt32(route.Attribute("time").Value) < 15
                select trolley.Attribute("serialNum").Value)
                .Any(x => Convert.ToInt32(x) < 5);
            System.Console.WriteLine(q8);
            SpaceAround();

            // q9
            var q9 = xmlDoc.Descendants("route").Where(x => Convert.ToInt32(x.Attribute("time").Value) < 50)
                .Intersect(xmlDoc.Descendants("route").Where(x => Convert.ToInt32(x.Attribute("time").Value) > 10)).Select(x => x.Attribute("routeNum").Value);
            foreach (var route in q9)
            {
                System.Console.WriteLine(route);
            }
            SpaceAround();

            // q 10 select route with most busses on it
            var q10 = (from trolley in xmlDoc.Descendants("trolley")
                group trolley.Attribute("serialNum").Value 
                by trolley.Attribute("routeNum").Value)
                .Select(x => new { routeNum=x.Key, count=x.Count()})
                .OrderByDescending(x => x.count).Take(1);
            foreach (var route in q10)
            {
                System.Console.WriteLine(route);
            }
            SpaceAround();
        }
    }
}
