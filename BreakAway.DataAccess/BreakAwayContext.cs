using BreakAway.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace BreakAway.DataAcces
{
    public class BreakAwayContext: DbContext
    {

        public BreakAwayContext(): base()
        {

        }

        public DbSet<Destination> Destinations;
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<Trip> Trips;
        public DbSet<Person> People;
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Activity> Activities { get; set; }

    }
}