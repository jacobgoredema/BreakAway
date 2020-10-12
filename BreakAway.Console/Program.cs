using BreakAway.DataAcces;
using BreakAway.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            FindDestination();
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

        private static void FindDestination()
        {
            System.Console.Write("Enter id of Destination to find: ");
            var id = int.Parse(System.Console.ReadLine());
            using (var context = new BreakAwayContext())
            {
                var destination = context.Destinations.Find(id);
                //using composite key to find
                //context.Destinations.Find("USA", "1122");

                if (destination == null)
                {
                    System.Console.WriteLine("Destination not found");
                }
                else
                {
                    System.Console.WriteLine(destination.Name);
                }
            }
        }

        private static void FindGreatBarrierReef()
        {
            using (var context = new BreakAwayContext())
            {
                var query = from d in context.Destinations
                            where d.Name == "Great Barrier Reef"
                            select d;

                var reef = query.SingleOrDefault();

                if(reef == null)
                {
                    System.Console.WriteLine("Cant Find the reef");
                }
                else
                {
                    System.Console.WriteLine(reef.Description);

                }
            }
        }

        public static void GetLocalDestinationCount()
        {
            using (var context = new BreakAwayContext())
            {
                context.Destinations.Load();

                var count = context.Destinations.Local.Count;
                System.Console.WriteLine("Destinations in memory: {0}", count);
            }
        }

        private static void LoadAustralianDestinations()
        {
            using (var context = new BreakAwayContext())
            {
                var query = from d in context.Destinations
                            where d.Country == "Australia"
                            select d;

                query.Load();

                var count = context.Destinations.Local.Count;
                System.Console.WriteLine("Aussie destinations in memeory: {0} ", count);
            }
        }

        private static void LocalLinqQueries()
        {
            using (var context =  new BreakAwayContext())
            {
                context.Destinations.Load();
                var sortedDestinations = from d in context.Destinations.Local
                                         orderby d.Name
                                         select d;

                System.Console.WriteLine("All destinations:");
                foreach (var destination in sortedDestinations)
                {
                    System.Console.WriteLine(destination.Name);
                }

                var aussieDestinations = from d in context.Destinations.Local
                                         where d.Country == "Australia"
                                         select d;

                System.Console.WriteLine();
                System.Console.WriteLine("Australian Destinations:");

                foreach (var destination in aussieDestinations)
                {
                    System.Console.WriteLine(destination.Name);

                }
            }
        }

        private static void ListenToLocalChanges()
        {
            using (var context =  new BreakAwayContext())
            {
                context.Destinations.Local.CollectionChanged += (sender, args) =>
                  {
                      if (args.NewItems != null)
                      {
                          foreach (Destination item in args.NewItems)
                          {
                              System.Console.WriteLine("Added: " + item.Name);
                          }
                      }

                      if (args.OldItems != null)
                      {
                          foreach (Destination item in args.OldItems)
                          {
                              System.Console.WriteLine("Removed: " +item.Name);

                          }
                      }
                  };

                context.Destinations.Load();
            }
           
        }

        // Lazy loading
        private static void TestLazyLoading()
        {
            using (var context =  new BreakAwayContext())
            {
                var query = from d in context.Destinations
                            where d.Name == "Grand Canyon"
                            select d;

                var canyon = query.SingleOrDefault();

                System.Console.WriteLine("Grand Canyon Loding");
                if (canyon.Lodgings != null)
                {
                    foreach (var lodging in canyon.Lodgings)
                    {
                        System.Console.WriteLine(lodging.Name);

                    }
                }
            }
        }

        // Eager Loading
        private static void TestEagerLoading()
        {
            using (var context = new BreakAwayContext())
            {
                var allDestinations = context.Destinations.Include(d => d.Lodgings);

                foreach (var destination in allDestinations)
                {
                    System.Console.WriteLine(destination.Name);

                    foreach (var lodging in destination.Lodgings)
                    {
                        System.Console.WriteLine(" - " + lodging.Name);

                    }
                }
            }
        }

        // Explicit Loading
        private static void TestExplicitLoading()
        {
            using (var context = new BreakAwayContext())
            {
                var query = from d in context.Destinations
                            where d.Name == "Grand Canyon"
                            select d;

                var canyon = query.SingleOrDefault();

                context.Entry(canyon).Collection(d => d.Lodgings).Load();

                System.Console.WriteLine("Grabd Canyon Lodging");
                foreach (var lodging in canyon.Lodgings)
                {
                    System.Console.WriteLine(lodging.Name);
                }
            }
        }
    }
}
