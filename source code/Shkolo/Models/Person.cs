using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shkolo.Models
{
    public abstract class Person
    {
        protected Person(string fullName, int age, string birthDate, string email, byte[] password, byte[] image)
        {
            FullName = fullName;
            Age = age;
            BirthDate = birthDate;
            Email = email;
            Password = password;
            Image = image;
        }

        public long ID { get; set; }
        public string FullName { get; private set; }
        public int Age { get; private set; }
        public string BirthDate { get; private set; }
        public string Email { get; private set; }
        public byte[] Password { get; private set; }
        public byte[] Image { get; private set; }
    }
}   
