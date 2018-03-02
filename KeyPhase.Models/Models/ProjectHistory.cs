using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.Models
{
    public class ProjectHistory
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public String Value { get; set; }
        public DateTime DateSubmitted { get; set; }
        public bool Active { get; set; }
        public User User { get; set; }
    }
}
