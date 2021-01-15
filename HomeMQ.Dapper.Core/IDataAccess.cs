using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMQ.DapperCore
{
    public interface IDataAccess
    {
        List<Person> GetMappedPeople();
        List<Person> GetMappedPeopleByLastName(string lastName);
        List<Person> GetPeople();
        List<Person> GetPeople(string lastName);
        Task<List<Person>> GetPeopleAsync();
        void InsertDataSet();
        void InsertPerson(Person person);
        Task InsertPersonAsync(Person person);
        void MultipleSets();
        void RunWithTransaction(string firstName, string lastName);
    }
}