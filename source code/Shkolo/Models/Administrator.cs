using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shkolo.Models
{
    public class Administrator : Person
    {
        public Administrator(string fullName, int age, string birthDate, string email, byte[] password, byte[] image) : base(fullName, age, birthDate, email, password, image)
        {
        }
    }
}
