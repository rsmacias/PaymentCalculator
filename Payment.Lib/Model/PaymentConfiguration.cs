using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Lib {
    public class PaymentConfiguration {
        public string Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double AmountByHour { get; set; }
        public double TotalHours {
            get {
                return EndTime.Subtract(StartTime).TotalHours;
            }
        }
    }
}
