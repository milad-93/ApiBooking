
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ApiBooking.Models;


namespace ApiBooking.Models
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {

        }

        //https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        

              modelBuilder.Entity<Booking>()
              .HasOne(k => k.BookableHours)
             .WithMany(x => x.Person)
             .HasForeignKey(y => y.BookableHoursID)
             //.IsRequired();
             .OnDelete(DeleteBehavior.Cascade);


        }


        public DbSet<Booking> Person { get; set; }
        public DbSet<BookableHours> BookableHours { get; set; }
      
    }
}
