using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Models
{
    internal class EnterPerson
    {
        public EnterPerson() 
        {
            Guid = Guid.NewGuid();
        }

        public EnterPerson(Guid guid, string firstName, string lastName, string email, DateTime? dateOfBirth) 
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public Guid Guid { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}

