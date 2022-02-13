﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator {
    public interface IPayrollPayment {
        List<PaymentConfiguration> PaymentConfig { set; }
        List<SchedulePaid> Pay(List<ScheduleWorked> employeesWorkSchedule);
    }
}
