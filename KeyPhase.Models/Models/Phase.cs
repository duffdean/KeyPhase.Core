using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KeyPhase.Models.Models
{
    public class Phase
    {
        public Phase() { }

        public int ID { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public bool? Active { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
