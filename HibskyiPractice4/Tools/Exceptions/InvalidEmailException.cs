using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Tools.Exceptions
{
    internal class InvalidEmailException : FormatException
    {
        public InvalidEmailException() :
            base("Invalid Email!") {}

        public InvalidEmailException(string badEmail) : 
            base($"Invalid Email: {badEmail}") {}
    }
}
