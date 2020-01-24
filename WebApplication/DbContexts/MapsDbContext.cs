using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.DbContexts
{
    public class MapsDbContext : DbContext
    {
        public DbSet<Maps> Map { get; set; }
    }
}