using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarWebService.Models
{
    public class CarDbContext : DbContext
    {
        public CarDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Car> Cars { get; set; }
    }
}