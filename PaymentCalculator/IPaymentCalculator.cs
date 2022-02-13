using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator {
    public interface IPaymentCalculator {
        double Compute();
        double GetWorkingHours(DateTime config_start_time, DateTime config_end_time, DateTime worked_start_time, DateTime worked_end_time);
    }
}
