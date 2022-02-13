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
        //public TimeSpan StartHour { get; set; }
        //public TimeSpan EndHour { get; set; }
        public decimal Salary { get; set; }
        public double TotalHours { 
            get {
                //var hours = EndTime.Subtract(StartTime).TotalHours;
                //EndTime.AddDays(1);
                //return hours < 0 ? 24 + hours : hours;
                return EndTime.Subtract(StartTime).TotalHours;
            } 
        }
    }
}
