using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.Models
{
    public class User
    {
        public User() {}

        public int ID { get; set; }        
        //public string Username { get; set; }        
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string BgColour { get; set; }
        public bool? Active { get; set; }
    }
}
