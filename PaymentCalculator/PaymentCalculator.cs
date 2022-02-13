using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator {
    public class PaymentCalculator : IPaymentCalculator {
        private double _amountByHour;
        private Schedule _configRangeHours;
        private Schedule _workedRangeHours;

        public PaymentCalculator() {

        }

        public double AmountByHour {
            set { _amountByHour = value; }
        }
        public Schedule ConfigRangeHours {
            set { _configRangeHours = value; }
        }
        public Schedule WorkedRangeHours {
            set { _workedRangeHours = value; }
        }

        public double Compute() {
            return GetWorkingHours() * _amountByHour;
        }

        public double Compute(double amountByHour) {
            var workedHours = GetWorkingHours();
            return workedHours * amountByHour;
        }

        public double Compute(double amountByHour, Schedule configRangeHours, Schedule workedRangeHours) {
            var workedHours = GetWorkingHours(configRangeHours, workedRangeHours);
            return workedHours * amountByHour;
        }

        public double GetWorkingHours() {
            var config_start_time = _configRangeHours.start_time;
            var config_end_time = _configRangeHours.end_time;
            var worked_start_time = _workedRangeHours.start_time;
            var worked_end_time = _workedRangeHours.end_time;

            return GetWorkingHours(_configRangeHours, _workedRangeHours);
        }

        public double GetWorkingHours(Schedule configRangeHours, Schedule workedRangeHours) {

            var config_start_time = configRangeHours.start_time;
            var config_end_time = configRangeHours.end_time;
            var worked_start_time = workedRangeHours.start_time;
            var worked_end_time = workedRangeHours.end_time;

            var result = DateTime.Compare(worked_start_time, config_start_time);
            var ini = result >= 0 ? worked_start_time : config_start_time;

            var result2 = DateTime.Compare(worked_end_time, config_end_time);
            var fin = result2 < 0 ? worked_end_time : config_end_time;

            var hours = fin.Subtract(ini).TotalHours;
            if (hours < 0)
                fin = fin.AddDays(1);

            return Math.Ceiling(fin.Subtract(ini).TotalHours);
        }

    }
}
