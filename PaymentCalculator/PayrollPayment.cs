using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PaymentCalculator {
    public class PayrollPayment : IPayrollPayment {

        private List<PaymentConfiguration> _paymentConfig = null;
        private readonly IPaymentCalculator _paymentCalculator = null;
        private readonly ILogger<PayrollPayment> _log = null;

        public PayrollPayment(List<PaymentConfiguration> paymentConfig,
                              IPaymentCalculator paymentCalculator,
                              ILogger<PayrollPayment> log) {
            _paymentConfig = paymentConfig;
            _paymentCalculator = paymentCalculator;
            _log = log;
        }

        public PayrollPayment(IPaymentCalculator paymentCalculator,
                              ILogger<PayrollPayment> log) {
            _paymentCalculator = paymentCalculator;
            _log = log;
        }

        public List<PaymentConfiguration> PaymentConfig {
            set { _paymentConfig = value; }
        }

        public List<SchedulePaid> Pay(List<ScheduleWorked> employeesWorkSchedule) {
            try {
                List<SchedulePaid> paySheet = new List<SchedulePaid>();

                foreach (var employeeSchedule in employeesWorkSchedule) {
                    var employeePayment = new SchedulePaid();
                    double moneyToPay = 0;
                    double totalHours = 0;

                    employeePayment.EmployeeName = employeeSchedule.EmployeeName;
                    _log.LogDebug($"Processing payment to {employeeSchedule.EmployeeName}");

                    foreach (var workedDay in employeeSchedule.Schedule) {
                        var day = workedDay.Day;
                        var start = workedDay.StartTime;
                        var end = workedDay.EndTime;

                        // Filtro de la configuración los rangos de horas a aplicar
                        var configToApply = _paymentConfig.Where(c => c.Day.Equals(day)
                                    && ((c.StartTime <= start && c.EndTime >= start)
                                    || (c.StartTime <= end && c.EndTime >= end))
                                    ).ToList();

                        foreach (var rangeConfigPayment in configToApply) {
                            _paymentCalculator.ConfigRangeHours = new Schedule(rangeConfigPayment.StartTime, rangeConfigPayment.EndTime);
                            _paymentCalculator.WorkedRangeHours = new Schedule(start, end);
                            _paymentCalculator.AmountByHour = rangeConfigPayment.AmountByHour;

                            _log.LogDebug($"Config Range Hours        >>>> {rangeConfigPayment.StartTime.ToShortTimeString()} - {rangeConfigPayment.EndTime.ToShortTimeString()}");
                            _log.LogDebug($"Working Range Hours       >>>> {start.ToShortTimeString()} - {end.ToShortTimeString()}");

                            totalHours += _paymentCalculator.GetWorkingHours();
                            moneyToPay += _paymentCalculator.Compute();

                            _log.LogDebug($"Accumulated Worked Hours  >>>> {totalHours}");
                            _log.LogDebug($"Accumulated Payment to do >>>> {moneyToPay}");
                        }
                    }
                    employeePayment.TotalHours = totalHours;
                    employeePayment.Amount = moneyToPay;

                    paySheet.Add(employeePayment);
                }

                return paySheet;
            } catch (Exception e) {
                _log.LogError($"There is a problem while processing the payments", e);
                throw;
            }
        }

    }
}
