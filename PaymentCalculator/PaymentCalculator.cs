using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator {
    public class PaymentCalculator : IPaymentCalculator {

        public PaymentCalculator() {

        }

        public double Compute() {
            throw new NotImplementedException();
        }

        public double GetWorkingHours(DateTime config_start_time, DateTime config_end_time, DateTime worked_start_time, DateTime worked_end_time) {

            var result = DateTime.Compare(worked_start_time, config_start_time);
            var ini = result >= 0 ? worked_start_time : config_start_time;

            var result2 = DateTime.Compare(worked_end_time, config_end_time);
            var fin = result2 < 0 ? worked_end_time : config_end_time;

            var hours = fin.Subtract(ini).TotalHours;
            if (hours < 0)
                fin = fin.AddDays(1);

            return fin.Subtract(ini).TotalHours;
        }

    }
}
