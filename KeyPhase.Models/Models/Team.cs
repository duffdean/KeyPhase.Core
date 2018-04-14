using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public bool Active { get; set; }
    }
}
