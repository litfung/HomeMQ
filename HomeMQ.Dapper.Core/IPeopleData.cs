using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMQ.DapperCore
{
    public interface IPeopleData
    {
        Task<List<Person>> GetPeople();
        Task InsertPerson(Person person);
    }
}