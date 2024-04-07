using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Models
{
    internal class Filter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirthFrom { get; set; }
        public DateTime? DateOfBirthTo { get; set; }
        public bool? IsAdult { get; set; }
        public string SunSign { get; set; }
        public string ChineseSign { get; set; }
        public bool? IsBirthday { get; set; }
        public SortingBy SortedBy { get; set; }
        public bool DescendingOrder { get; set; }

        public enum SortingBy
        {
            FirstName,
            LastName,
            Email,
            DateOfBirth,
            IsAdult,
            SunSign,
            ChineseSign,
            IsBirthday
        }

        public Filter() 
        {
            SortedBy = SortingBy.FirstName;
            DescendingOrder = false;
        }

        public Filter(string firstName, string lastName, string email, DateTime? dateOfBirthFrom, DateTime? dateOfBirthTo, bool? isAdult,
            string sunSign, string chineseSign, bool? isBirthday, SortingBy sortedBy, bool descendingOrder)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirthFrom = dateOfBirthFrom;
            DateOfBirthTo = dateOfBirthTo;
            IsAdult = isAdult;
            SunSign = sunSign;
            ChineseSign = chineseSign;
            IsBirthday = isBirthday;
            SortedBy = sortedBy;
            DescendingOrder = descendingOrder;
        }
    }
}
