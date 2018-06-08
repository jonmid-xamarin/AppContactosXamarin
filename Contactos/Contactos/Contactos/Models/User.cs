using System;
using System.Collections.Generic;
using System.Text;

namespace Contactos.Models
{
    public class User
    {
        public Boolean Success { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}
