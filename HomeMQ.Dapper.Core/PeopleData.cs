﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMQ.DapperCore
{
    public class PeopleData : IPeopleData
    {
        private readonly IDataAccess database;

        public PeopleData(IDataAccess db)
        {
            database = db;
        }

        public Task<List<Person>> GetPeople()
        {
            var people = database.GetPeopleAsync();
            return people;
        }

        public Task InsertPerson(Person person)
        {
            //no need to await here....
            //supposed to make life easier by having one less await???
            return database.InsertPersonAsync(person);

        }
    }
}