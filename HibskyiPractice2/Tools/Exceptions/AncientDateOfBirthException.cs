using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice2.Tools.Exceptions
{
    internal class AncientDateOfBirthException : Exception
    {
        public AncientDateOfBirthException() :
           base("Date of Birth cannot be that old!") {}

        public AncientDateOfBirthException(string badDateOfBirth) :
            base($"Date of Birth cannot be that old: {badDateOfBirth}") {}
    }
}
