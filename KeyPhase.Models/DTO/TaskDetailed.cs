using KeyPhase.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models.DTO
{
    public class TaskDetailed
    {
        public Task Task { get; set; }
        public List<TaskHistory> TaskHistory { get; set; }
    }
}
