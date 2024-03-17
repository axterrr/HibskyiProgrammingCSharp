using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice2.Models
{
    internal class EnterPerson
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime? _dateOfBirth;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
