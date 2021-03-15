using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shkolo.Models
{
    public class Teacher : Person
    {
        public Teacher(string fullName, int age, string birthDate, string email, byte[] password, byte[] image, int teacher_id, int subject, int initialClassId = -1) : base(fullName, age, birthDate, email, password, image)
        {
            Teacher_ID = teacher_id;
            Subject = subject;
            Class_Id = initialClassId;
        }

        public int Teacher_ID { get; set; }
        public int Class_Id { get; set; }
        public int Subject { get; set; }
    }
}
