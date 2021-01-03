using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HomeMQ.Models;

namespace HomeMQ.WebApp.Data
{
    public class HomeMQWebAppContext : DbContext
    {
        public HomeMQWebAppContext (DbContextOptions<HomeMQWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<HomeMQ.Models.Movie> Movie { get; set; }
    }
}
