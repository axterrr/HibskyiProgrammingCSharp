using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Tools.Exceptions
{
    internal class FutureDateOfBirthException : Exception
    {
        public FutureDateOfBirthException() :
           base("Date of Birth cannot be in the Future!") {}

        public FutureDateOfBirthException(string badDateOfBirth) :
            base($"Date of Birth cannot be in the Future: {badDateOfBirth}") {}
    }
}
