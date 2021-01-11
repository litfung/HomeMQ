using HomeMQ.DapperCore;
using System;

namespace HomeMQ.DapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //DapperTest1();
            //MapMultipleObjects("Corey");
            //MultipleSets();
            //RunWithTransaction();
            InsertDataSet();
            //Console.WriteLine("Hello World!");
        }

        static void MapMultipleObjects()
        {
            var db = new HomeDataAccess();

            var people = db.GetMappedPeople();
        }

        static void InsertDataSet()
        {
            var db = new HomeDataAccess();

            db.InsertDataSet();
        }

        static void MapMultipleObjects(string lastName)
        {
            var db = new HomeDataAccess();
            var people = db.GetMappedPeopleByLastName(lastName);
        }

        static void MultipleSets()
        {
            var db = new HomeDataAccess();
            db.MultipleSets();
        }

        static void RunWithTransaction()
        {
            var db = new HomeDataAccess();
            db.RunWithTransaction("Mr.", "Nobody");
        }

        static void DapperTest1()
        {
            var db = new HomeDataAccess();

            //var devin = new Person
            //{
            //    FirstName = "Alvin",
            //    LastName = "Chipmunk"
            //};

            //db.InsertPerson(devin);

            var people = db.GetPeople("");

            foreach(var p in people)
            {
                //Console.WriteLine(p.EmailAddress);
                Console.WriteLine(p.FullInfo);
            }

            
        }
    }
}
