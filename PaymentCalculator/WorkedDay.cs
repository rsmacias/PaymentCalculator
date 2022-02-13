using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator {
    public class WorkedDay {
        public string Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double TotalHours {
            get {
                var hours = EndTime.Subtract(StartTime).TotalHours;
                return hours < 0 ? 24 + hours : hours;
            }
        }
    }
}
