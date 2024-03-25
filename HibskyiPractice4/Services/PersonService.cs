using KMA.ProgrammingCSharp.HibskyiPractice4.Models;
using KMA.ProgrammingCSharp.HibskyiPractice4.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Services
{
    internal class PersonService
    {
        private static FileRepository Repository = new();

        public async Task AddOrUpdatePerson(EnterPerson enterPerson)
        {
            Thread.Sleep(1000);
            if (String.IsNullOrWhiteSpace(enterPerson.FirstName) || String.IsNullOrWhiteSpace(enterPerson.LastName)
                || String.IsNullOrEmpty(enterPerson.Email) || enterPerson.DateOfBirth == null)
                throw new ArgumentException("First name, Last name, Email or Date of birth is Empty");
            DBPerson dbPerson = new DBPerson(enterPerson.Guid, enterPerson.FirstName, enterPerson.LastName, enterPerson.Email, enterPerson.DateOfBirth.Value);
            await Repository.AddOrUpdate(dbPerson);
        }

        public List<Person> GetAllPersons()
        {
            var res = new List<Person>();
            foreach (var user in Repository.GetAll())
            {
                res.Add(new Person(user.Guid, user.FirstName, user.LastName, user.Email, user.DateOfBirth));
            }
            return res;
        }

        public void DeletePerson(Person person)
        {
            Repository.Delete(person.Guid);
        }
    }
}
