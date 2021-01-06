//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace HomeMQ.MainContext
//{
//    public class BloggingContext : DbContext
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
//}
