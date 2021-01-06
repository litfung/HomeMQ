using HomeMQ.MainContext;
using System;
using System.Linq;

namespace HomeMQ.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new HomeMQMainContext();

            //context.Locations.Add(new LocationSQL
            //{
            //    LocationName = "Patio",
            //    Room = "Outside"
            //});
            var location = (from locs in context.Locations select locs).FirstOrDefault();
            var item = (from sensor in context.Sensors where sensor.Identifier.Equals("Temp Sensor1") select sensor).FirstOrDefault();
            //context.Sensors.Add(new SensorSQL
            //{
            //    Identifier = "Temp Sensor1",
            //    Location = location,

            //});
            var reading = new TemperatureReadingSQL
            {
                Temperature = 55.0,
                Sensor = item
            };

            context.Add(reading);
            context.SaveChanges();



            Console.ReadKey();
        }

        //static void attempt1()
        //{
        //    using var db = new BloggingContext();

        //    // Create
        //    Console.WriteLine("Inserting a new blog");
        //    db.Add(new Blog { Url = "http://blogs.msdn.com/test2" });
        //    db.SaveChanges();

        //    // Read
        //    Console.WriteLine("Querying for a blog");
        //    var blog = db.Blogs
        //        .OrderBy(b => b.BlogId)
        //        .First();

        //    // Update
        //    Console.WriteLine("Updating the blog and adding a post");
        //    blog.Url = "https://devblogs.microsoft.com/test2";
        //    blog.Posts.Add(
        //        new Post
        //        {
        //            Title = "Hello World",
        //            Content = "I wrote an app using EF Core!"
        //        });
        //    db.SaveChanges();

        //    // Delete
        //    Console.WriteLine("Delete the blog");
        //    db.Remove(blog);
        //    db.SaveChanges();
        //}
    }
}
