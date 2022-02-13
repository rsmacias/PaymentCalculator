﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator {
    public class PayrollPayment {

        private readonly List<PaymentConfiguration> _paymentConfig = null;
        private readonly IPaymentCalculator _paymentCalculator = null;

        public PayrollPayment(List<PaymentConfiguration> paymentConfig,
                              IPaymentCalculator paymentCalculator) {
            _paymentConfig = paymentConfig;
            _paymentCalculator = paymentCalculator;
        }

        public List<SchedulePaid> Pay(List<ScheduleWorked> employeesWorkSchedule) {
            try {
                List<SchedulePaid> paySheet = new List<SchedulePaid>();

                foreach (var employeeSchedule in employeesWorkSchedule) {
                    var employeePayment = new SchedulePaid();
                    double moneyToPay = 0;
                    double totalHours = 0;

                    employeePayment.EmployeeName = employeeSchedule.EmployeeName;

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
                            var salaryByHour = (double)rangeConfigPayment.Salary;
                            var workedHours = _paymentCalculator.GetWorkingHours(rangeConfigPayment.StartTime, rangeConfigPayment.EndTime, start, end);

                            totalHours += Math.Ceiling(workedHours);
                            moneyToPay += (Math.Ceiling(workedHours) * salaryByHour);
                        }
                    }
                    employeePayment.TotalHours = totalHours;
                    employeePayment.Amount = moneyToPay;

                    paySheet.Add(employeePayment);
                }

                return paySheet;
            } catch (Exception) {
                throw;
            }
        }

    }
}
