using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMQ.DapperCore
{
    public interface IPeopleData
    {
        Task<List<Person>> GetPeopleAsync();
        Task InsertPersonAsync(Person person);
    }
}