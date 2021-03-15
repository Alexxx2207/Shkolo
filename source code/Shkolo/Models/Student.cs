using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shkolo.Models
{
    public class Student : Person
    {
        public Student(string fullName, int age, string birthDate, string email, byte[] password, byte[] image,  int studentId, int classId) : base(fullName, age, birthDate, email, password, image)
        {
            StudentID = studentId;
            ClassID = classId;
            Grades = new Dictionary<string, List<double>>();
        }

        public Dictionary<string, List<double>> Grades { get; set; }
        public int StudentID { get; set; }
        public int ClassID { get; set; }
    }
}
