using HomeMQ.DapperCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMQ.FirstWebAPI.Controllers
{
    [ApiController]
    public class PeopleController : ControllerBase
    {
        //private IPeopleData peopleService;
        private IDataAccess peopleService;

        public PeopleController()
        {
            //peopleService = new PeopleData();
            peopleService = new HomeDataAccess();
        }

        [HttpGet("people/all")]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllAsync()
        {
            var people = await peopleService.GetPeopleAsync();
            return people;
        }
    }
}
