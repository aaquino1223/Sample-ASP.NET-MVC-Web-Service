using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarWebService.Models
{
    public class CarDbContext : IdentityDbContext<CarServiceUser>
    {
        public CarDbContext() : base("DefaultConnection")
        {

        }

        public static CarDbContext Create()
        {
            return new CarDbContext();
        }

        public virtual DbSet<Car> Cars { get; set; }
    }
}