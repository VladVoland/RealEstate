using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class User
    {
        public User() { }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public int PhoneNumber { get; set; }

        public List<RealEstate> RealEstates { get; set; }
    }
}
