using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01_03
{
    class Person
    {
        public string StudentID { get; set; }
        public string FullName { get; set; }
    }

    class Student : Person
    {
        public string Faculty { get; set; }
        public float AverageScore { get; set; }
    }

    class Teacher : Person
    {
        public string Address { get; set; }
    }
}
