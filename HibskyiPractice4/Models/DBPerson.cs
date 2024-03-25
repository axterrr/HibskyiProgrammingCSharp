using KMA.ProgrammingCSharp.HibskyiPractice4.Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Models
{
    internal class DBPerson
    {
        public DBPerson(Guid guid, string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (!Regex.IsMatch(email, "^[a-z0-9\\.\\-_]+@[a-z]+(\\.[a-z]+)+$"))
                throw new InvalidEmailException(email);

            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (DateTime.Today < dateOfBirth.AddYears(age))
                --age;

            if (age < 0)
                throw new FutureDateOfBirthException(dateOfBirth.ToString("dd.MM.yyyy"));
            if (age > 135)
                throw new AncientDateOfBirthException(dateOfBirth.ToString("dd.MM.yyyy"));

            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public Guid Guid { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime DateOfBirth { get; }
    }
}
