using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator {
    public class PaymentConfiguration {
        public string Day { get; set; }
        public  DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Salary { get; set; }
        public double TotalHours { 
            get {
                return EndTime.Subtract(StartTime).TotalHours;
            } 
        }
    }
}
