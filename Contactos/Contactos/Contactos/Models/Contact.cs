using System;
using System.Collections.Generic;
using System.Text;

namespace Contactos.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}
