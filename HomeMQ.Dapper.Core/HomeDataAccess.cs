using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace HomeMQ.DapperCore
{
    public class HomeDataAccess : IDataAccess
    {
        public List<Person> GetPeople(string lastName)
        {
            //var wtf = @"Data Source=DESKTOP-7S00QLE\SQLEXPRESS; Initial Catalog = DemoDB2; uid = master; pwd=control; timeout= 100000;";
            //Console.WriteLine(wtf);
            using (IDbConnection newConnection = new SqlConnection(SQLHelper.ConnectionString))
            {
                //var ienum = newConnection.Query<Person>($"select * from Person where LastName = '{lastName}'");
                var output = newConnection.Query<Person>($"dbo.spPerson_FilterByLastName @LastName", new { LastName = lastName });
                return output.ToList();
            }
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            using (IDbConnection newConnection = new SqlConnection(SQLHelper.ConnectionString))
            {
                var output = await newConnection.QueryAsync<Person>($"dbo.spPerson_ReadAll");
                return output.ToList();
            }
        }

        public List<Person> GetPeople()
        {
            //var wtf = @"Data Source=DESKTOP-7S00QLE\SQLEXPRESS; Initial Catalog = DemoDB2; uid = master; pwd=control; timeout= 100000;";
            //Console.WriteLine(wtf);
            using (IDbConnection newConnection = new SqlConnection(SQLHelper.ConnectionString))
            {
                //var ienum = newConnection.Query<Person>($"select * from Person where LastName = '{lastName}'");
                var output = newConnection.Query<Person>($"dbo.spPerson_ReadAll");
                return output.ToList();
            }
        }

        public void InsertDataSet()
        {
            using (IDbConnection newConnection = new SqlConnection(SQLHelper.ConnectionString))
            {
                var troopers = GetTroopers();

                var p = new
                {
                    dataTable = troopers.AsTableValuedParameter("BasicUDT")
                };

                var recordsAffected = 0;
                try
                {
                    recordsAffected = newConnection.Execute("dbo.spPerson_InsertSet ", p, commandType: CommandType.StoredProcedure);
                }
                catch (SqlException se)
                {
                    Console.WriteLine(se.Message);
                }

                Console.WriteLine($"Records Affected = {recordsAffected}");

                GetMappedPeople();
            }
        }

        private DataTable GetTroopers()
        {
            var output = new DataTable();

            output.Columns.Add("FirstName", typeof(string));
            output.Columns.Add("LastName", typeof(string));

            output.Rows.Add("Trooper", "12344");
            output.Rows.Add("Trooper", "84542");
            output.Rows.Add("Trooper", "94135");
            output.Rows.Add("Trooper", "11111");
            output.Rows.Add("Trooper", "12535");
            output.Rows.Add("Trooper", "41541");
            output.Rows.Add("Trooper", "65258");
            output.Rows.Add("Trooper", "74512");

            return output;
        }

        public List<Person> GetMappedPeople()
        {
            using (IDbConnection newConnection = new SqlConnection(SQLHelper.ConnectionString))
            {
                //var sql = @"select pe.*, ad.* from dbo.Person pe
                //            left join dbo.Address ad
                //            on ad.PersonId = pe.PersonId";

                var sql = $"dbo.spPersonWithAddresses_ReadAll";
                var people = newConnection.Query<Person, Address, Person>(sql, (person, address) => { person.Address = address; return person; });
                foreach (var p in people)
                {
                    Console.WriteLine($"{p.FullInfo}: \nAddress: {p.Address?.ToString()}");
                }

                return people.ToList();
            }
        }

        public void RunWithTransaction(string firstName, string lastName)
        {
            using (IDbConnection newConnection = new SqlConnection(SQLHelper.ConnectionString))
            {
                var dp = new DynamicParameters();
                dp.Add("@FirstName", firstName);
                dp.Add("@LastName", lastName);

                var sql = $@"insert into dbo.Person (FirstName, LastName)
                            values (@FirstName, @LastName)";

                newConnection.Open();
                using (var trans = newConnection.BeginTransaction())
                {
                    var recordsUpdated = newConnection.Execute(sql, dp, trans);
                    Console.WriteLine($"Records Updated: {recordsUpdated}");

                    try
                    {
                        newConnection.Execute("update dbo.Person set Id = 1", transaction: trans);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        trans.Rollback();
                    }
                }

                GetMappedPeople();
            }
        }

        public void MultipleSets()
        {
            using (IDbConnection newConnection = new SqlConnection(SQLHelper.ConnectionString))
            {
                var sql = @"select * from dbo.Person;
                            select * from dbo.Address;";

                List<Person> people = null;
                List<Address> addresses = null;
                using (var lists = newConnection.QueryMultiple(sql))
                {
                    //Order has to match
                    people = lists.Read<Person>().ToList();
                    addresses = lists.Read<Address>().ToList();
                }

                foreach (var p in people)
                {
                    Console.WriteLine(p.FullInfo);
                }

                foreach (var a in addresses)
                {
                    Console.WriteLine(a);
                }
            }
        }

        public List<Person> GetMappedPeopleByLastName(string lastName)
        {
            using (IDbConnection newConnection = new SqlConnection(SQLHelper.ConnectionString))
            {
                //var sql = @"select pe.*, ad.* from dbo.Person pe
                //            left join dbo.Address ad
                //            on ad.PersonId = pe.PersonId";
                var ppl = new { LastName = lastName };

                var sql = $"dbo.spPersonWithAddresses_FilterByLastName @LastName";
                var people = newConnection.Query<Person, Address, Person>(sql, (person, address) => { person.Address = address; return person; }, ppl);
                foreach (var p in people)
                {
                    Console.WriteLine($"{p.FullInfo}: \nAddress: {p.Address?.ToString()}");
                }

                return people.ToList();
            }
        }

        public void InsertPerson(Person person)
        {
            using (IDbConnection newConnection = new SqlConnection(SQLHelper.ConnectionString))
            {
                var people = new List<Person> { person };
                try
                {
                    newConnection.Execute("dbo.spPerson_Insert @FirstName, @LastName", people);//, @EmailAddress, @PhoneNumber");
                }
                catch (SqlException se)
                {
                    Console.WriteLine(se.Message);
                }

            }
        }

        public async Task InsertPersonAsync(Person person)
        {
            using (IDbConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                var people = new List<Person> { person };
                try
                {
                    await conn.ExecuteAsync("dbo.spPerson_Insert @FirstName, @LastName", people);//, @EmailAddress, @PhoneNumber");
                }
                catch (SqlException se)
                {
                    Console.WriteLine(se.Message);
                }

            }
        }
    }
}
