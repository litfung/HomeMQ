using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

namespace HomeMQ.Dapper.Core
{
    public class HomeDataAccess
    {
        public List<Person> GetPeople(string lastName)
        {
            var wtf = @"Data Source=DESKTOP-7S00QLE\SQLEXPRESS; Initial Catalog = DemoDB2; uid = master; pwd=control; timeout= 100000;";
            Console.WriteLine(wtf);
            using (IDbConnection newConnection = new SqlConnection(wtf))
            {
                //var ienum = newConnection.Query<Person>($"select * from Person where LastName = '{lastName}'");
                var ienum = newConnection.Query<Person>($"dbo.spPerson_FilterByLastName @LastName", new { LastName = lastName });
                return ienum.ToList();
            }
        }
    }
}
