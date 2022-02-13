using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator {
    public interface IPaymentCalculator {
        Schedule ConfigRangeHours { set; }
        Schedule WorkedRangeHours { set; }
        double AmountByHour { set; }
        double Compute();
        double Compute(double amountByHour);
        double Compute(double amountByHour, Schedule configRangeHours, Schedule workedRangeHours);
        double GetWorkingHours();
        double GetWorkingHours(Schedule configRangeHours, Schedule workedRangeHours);
    }
}
