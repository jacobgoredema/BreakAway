using BreakAway.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintAllDestinations();
            PrintAllDestinationsTwice();

        }

        private static void PrintAllDestinations()
        {
            using (var context = new BreakAwayContext())
            {
                foreach (var destination in context.Destinations)
                {
                    System.Console.WriteLine(destination.Name);
                }
            }
        }

        private static void PrintAllDestinationsTwice()
        {
            using (var context= new BreakAwayContext())
            {
                // get all destinations
                var allDestinations = context.Destinations.ToList();
                foreach (var destination in allDestinations)
                {
                    System.Console.WriteLine(destination.Name);
                }

                foreach (var destination in allDestinations)
                {
                    System.Console.WriteLine(destination.Name);
                }
            }
        }

        private static void PrintAllDestinationsSorted()
        {
            using (var context = new BreakAwayContext())
            {
                // order by Name alphabetically
                var query = from d in context.Destinations
                            orderby d.Name
                            select d;

                foreach (var destination in query)
                {
                    System.Console.WriteLine(destination.Name);
                }
            }
        }

        private static void PrintAustralianDestinations()
        {
            using (var context=  new BreakAwayContext())
            {
                // return al destinations in Australia
                var query = from d in context.Destinations
                            where d.Country == "Australia"
                            orderby d
                            select d;

                foreach (var destimation in query)
                {
                    System.Console.WriteLine(destimation.Name);
                }
            }
        }

        private static void PrintDestinationNameOnly()
        {
            using (var context = new BreakAwayContext())
            {
                //return the destination name only
                var query = from d in context.Destinations
                            where d.Country == "Australia"
                            orderby d.Name
                            select d.Name;
            }
        }
    }
}
