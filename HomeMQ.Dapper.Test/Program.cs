using HomeMQ.Dapper.Core;
using System;

namespace HomeMQ.Dapper.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DapperTest1();
            //Console.WriteLine("Hello World!");
        }

        static void DapperTest1()
        {
            var db = new HomeDataAccess();

            var people = db.GetPeople("Storm");

            foreach(var p in people)
            {
                Console.WriteLine(p.EmailAddress);
                Console.WriteLine(p.FullInfo);
            }

            
        }
    }
}
