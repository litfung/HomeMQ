using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HomeMQ.MainContext
{
    public class HomeMQMainContext : DbContext
    {
        #region Fields
        //int stringVarLength = 50;
        #endregion
        public HomeMQMainContext()
        {
            //var options = DbContextOptions
        }

        #region Sets
        public DbSet<LocationSQL> Locations { get; set; }
        public DbSet<SensorSQL> Sensors { get; set; }
        public DbSet<TemperatureReadingSQL> TemperatureReadings { get; set; }
        //public DbSet<ControlSQL> Controls { get; set; }
        //public DbSet<LightSQL> Lights { get; set; }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LocationSQL>().HasKey(t => t.LocationId);
            builder.Entity<LocationSQL>().Property(t => t.LocationId).ValueGeneratedOnAdd();
            builder.Entity<LocationSQL>().HasIndex(u => u.LocationName).IsUnique();
            //builder.Entity<LocationSQL>().Property(t => t.LocationName).HasMaxLength(stringVarLength);
            //builder.Entity<LocationSQL>().Property(t => t.Room).HasMaxLength(stringVarLength);
            builder.Entity<LocationSQL>().ToTable("Locations");

            builder.Entity<SensorSQL>().HasKey(t => t.SensorId);
            builder.Entity<SensorSQL>().Property(t => t.SensorId).ValueGeneratedOnAdd();
            //builder.Entity<SensorSQL>().Property(t => t.Identifier).HasMaxLength(stringVarLength);
            //builder.Entity<SensorSQL>().Property(t => t.Type).HasMaxLength(stringVarLength);
            //builder.Entity<SensorSQL>().Property(t => t.IPAddress).HasMaxLength(stringVarLength);
            //builder.Entity<SensorSQL>().Property(t => t.MACAddress).HasMaxLength(stringVarLength);
            builder.Entity<SensorSQL>().HasOne(s => s.Location).WithMany(s => s.SensorSQLs).HasForeignKey(s => s.LocationId);
            builder.Entity<SensorSQL>().ToTable("Sensors");

            builder.Entity<TemperatureReadingSQL>().HasKey(t => t.ReadingId);
            builder.Entity<TemperatureReadingSQL>().Property(t => t.ReadingId).ValueGeneratedOnAdd();
            //builder.Entity<TemperatureReadingSQL>().Property(t => t.ReadingId).HasMaxLength(stringVarLength);
            //builder.Entity<TemperatureReadingSQL>().Property(t => t.Temperature).HasColumnType("decimal(5, 2)");
            builder.Entity<TemperatureReadingSQL>().HasOne(s => s.Sensor).WithMany(t => t.TemperatureReadingSQLs).HasForeignKey(s => s.SensorId);
            builder.Entity<TemperatureReadingSQL>().ToTable("TemperatureReadings");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = DESKTOP-7S00QLE\SQLEXPRESS; Initial Catalog = HomeMQ; uid = master; pwd=control; timeout=100000;");

        }
        #endregion
    }
    //    {
    //        public DbSet<Blog> Blogs { get; set; }
    //        public DbSet<Post> Posts { get; set; }

    //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //        {
    //            optionsBuilder.UseSqlServer(@"Data Source = DESKTOP-7S00QLE\SQLEXPRESS; Initial Catalog = Blogging; uid = master; pwd=control; timeout=100000;",
    //                options => options.EnableRetryOnFailure());
    //            //options => options.EnableRetryOnFailure()//Integrated Security=SSPI;");// 

    //        }
    //    }

    //    public class Blog
    //    {
    //        public int BlogId { get; set; }
    //        public string Url { get; set; }

    //        public List<Post> Posts { get; } = new List<Post>();
    //    }

    //    public class Post
    //    {
    //        public int PostId { get; set; }
    //        public string Title { get; set; }
    //        public string Content { get; set; }

    //        public int BlogId { get; set; }
    //        public Blog Blog { get; set; }
    //    }
}
