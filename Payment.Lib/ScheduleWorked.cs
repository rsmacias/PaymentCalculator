using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Lib {
    public class ScheduleWorked {
        public string EmployeeName { get; set; }
        public List<WorkedDay> Schedule { get; set; } = new List<WorkedDay>();
    }
}
