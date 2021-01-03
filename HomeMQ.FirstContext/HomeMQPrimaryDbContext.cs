using HomeMQ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMQ.FirstContext
{
    public class HomeMQPrimaryDbContext : DbContext
    {
        public HomeMQPrimaryDbContext(DbContextOptions<HomeMQPrimaryDbContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movie { get; set; }
    }
}
