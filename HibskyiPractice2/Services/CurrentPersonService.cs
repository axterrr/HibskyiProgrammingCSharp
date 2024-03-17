using KMA.ProgrammingCSharp.HibskyiPractice2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice2.Services
{
    internal class CurrentPersonService
    {
        private static Person _currentPerson;

        public static Person CurrentPerson 
        { 
            get { return _currentPerson; }
            private set { _currentPerson = value; }
        }

        public void EnterPerson(EnterPerson enterPerson)
        {
            Thread.Sleep(3000);
            if (String.IsNullOrWhiteSpace(enterPerson.FirstName) || String.IsNullOrWhiteSpace(enterPerson.LastName)
                || String.IsNullOrEmpty(enterPerson.Email) || enterPerson.DateOfBirth == null)
                throw new ArgumentException("First name, Last name, Email or Date of birth is Empty");
            CurrentPerson = new Person(enterPerson.FirstName, enterPerson.LastName, enterPerson.Email, enterPerson.DateOfBirth);
        }

    }
}
