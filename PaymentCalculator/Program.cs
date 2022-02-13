using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PaymentCalculator {
    class Program {

        public static IConfigurationRoot _config;
        CultureInfo provider = CultureInfo.InvariantCulture;

        /*static void Main(string[] args) {
            double moneyToPay = 0;

            var rangeIni = new DateTime(2022, 02, 12, 9, 0, 0); 
            var rangeFin = new DateTime(2022, 02, 12, 18, 0, 0); 


            var ini = new DateTime(2022, 02, 12, 8, 0, 0); //8   15    10
            var fin = new DateTime(2022, 02, 12, 12, 0, 0); // 13   19    12

            var hours = GetWorkingHours(rangeIni, rangeFin, ini, fin);
            Console.WriteLine($"Config   >>>> {rangeIni.ToShortTimeString()} - {rangeFin.ToShortTimeString()}");
            Console.WriteLine($"Workging >>>> {ini.ToShortTimeString()} - {fin.ToShortTimeString()}");
            Console.WriteLine($"Hours    >>>> {hours}");

            //var result = DateTime.Compare(ini, rangeIni);
            //string relationship;

            //if (result < 0)
            //    relationship = "is earlier than";
            //else if (result == 0)
            //    relationship = "is the same time as";
            //else
            //    relationship = "is later than";

            //Console.WriteLine("{0} {1} {2}", ini, relationship, fin);
            Console.ReadKey();
        }*/

        static double GetWorkingHours(DateTime config_start_time, DateTime config_end_time, DateTime worked_start_time, DateTime worked_end_time) {

            var result = DateTime.Compare(worked_start_time, config_start_time);
            var ini = result >= 0 ? worked_start_time : config_start_time;

            var result2 = DateTime.Compare(worked_end_time, config_end_time);
            var fin = result2 < 0 ? worked_end_time : config_end_time;

            return fin.Subtract(ini).TotalHours;
        }

        /*static double GetWorkingHours( DateTime config_start_time, DateTime config_end_time, DateTime worked_start_time, DateTime worked_end_time) {
            
            var result = DateTime.Compare(worked_start_time, config_start_time);

            if (result >= 0) {
                // worked_start_time Es posterior o igual a config_start_time

                var result2 = DateTime.Compare(worked_end_time, config_end_time);
                if(result2 < 0) {
                    // worked_end_time Es anterior a config_end_time
                    return worked_end_time.Subtract(worked_start_time).TotalHours;
                } else {
                    // worked_end_time Es posterior o igual a config_end_time
                    return config_end_time.Subtract(worked_start_time).TotalHours;
                }
            } else {
                // worked_start_time Es anterior a config_start_time
                // Trabaja con config_start_time
            }

            return 0;
        }*/

        
        static void Main(string[] args) {

            var builder = new ConfigurationBuilder()
                   .AddJsonFile($"appsettings.json", true, true);

            _config = builder.Build();

            // CARGA DE CONFIGUACIÓN INICIAL
            var config_payment_file = _config["paymentConfig:file"];
            var config_payment_sep_field = _config["paymentConfig:separator"];
            var config_payment_time_format = _config["paymentConfig:timeFormat"];

            Console.WriteLine("Hello World!");
            Console.WriteLine($"Archivo a cargar: {config_payment_file}");

            string[] config_lines = File.ReadAllLines(config_payment_file);

            List<PaymentConfiguration> paymentConfig = new List<PaymentConfiguration>();
            foreach (string line in config_lines) {
                string[] fields = line.Split(config_payment_sep_field.ToCharArray());

                var inicio = DateTime.ParseExact(fields[1], config_payment_time_format, CultureInfo.InvariantCulture);
                var fin = DateTime.ParseExact(fields[2], config_payment_time_format, CultureInfo.InvariantCulture);
                if (fin.Subtract(inicio).TotalHours < 0) {
                    fin = fin.AddDays(1);
                }

                paymentConfig.Add(new PaymentConfiguration {
                    Day = fields[0],
                    //StartHour = TimeSpan.Parse(fields[1]),
                    //EndHour = TimeSpan.Parse(fields[2]),
                    //StartTime = DateTime.ParseExact(fields[1], config_payment_time_format, CultureInfo.InvariantCulture),
                    //EndTime = DateTime.ParseExact(fields[2], config_payment_time_format, CultureInfo.InvariantCulture),
                    StartTime = inicio,
                    EndTime = fin,
                    Salary = decimal.Parse(fields[3])
                });
                //Console.WriteLine($"Day: {fields[0]}, StartHour:{fields[1]}, EndHour:{fields[2]}, Payment:{fields[3]}");
            }

            // CARGA DE DATOS DE ENTRADA
            var input_data_file = _config["input:file"];
            var input_data_time_format = _config["input:timeFormat"];

            string[] input_lines = File.ReadAllLines(input_data_file);

            List<ScheduleWorked> EmployeesScheduleWorked = new List<ScheduleWorked>();
            foreach (string line in input_lines) {
                string[] data = line.Split('=');
                string header = data[0];
                string detail = data[1];
                string[] workedDays = detail.Split(',');

                var employeeSchedule = new ScheduleWorked();
                employeeSchedule.EmployeeName = header;
                //Console.WriteLine($"Employee: {header}, \r\n\tSchedule:{detail}");
                foreach (var workedDay in workedDays) {
                    var hours = workedDay.Split('-');
                    var day = hours[0].Substring(0, 2);
                    var startTime = hours[0].Substring(2, hours[0].Length - 2);
                    var endTime = hours[1];
                    //Console.WriteLine($"\t\tDay:{day} \tStartHour:{startTime} \tEndHour:{endTime}");
                    employeeSchedule.Schedule.Add(new WorkedDay() {
                        Day = day,
                        StartTime = DateTime.ParseExact(startTime, input_data_time_format, CultureInfo.InvariantCulture),
                        EndTime = DateTime.ParseExact(endTime, input_data_time_format, CultureInfo.InvariantCulture)
                    });
                }

                EmployeesScheduleWorked.Add(employeeSchedule);
            }


            // CALCULO DE VALORES A PAGAR
            //foreach (var employeeSchedule in EmployeesScheduleWorked) {
            //    Console.WriteLine($"Employee: {employeeSchedule.EmployeeName}");
            //    foreach (var workedDay in employeeSchedule.Schedule) {
            //        var day = workedDay.Day;
            //        var start = workedDay.StartTime;
            //        var end = workedDay.EndTime;
            //        var configToApply = paymentConfig.Where(c => c.Day.Equals(day)
            //                    && ((c.StartTime <= start && c.EndTime >= start)
            //                    || (c.StartTime <= end && c.EndTime >= end))
            //                    ).ToList();
            //    }

            //}

            double moneyToPay = 0;
            var start = new DateTime(2022, 02, 12, 8, 0, 0); //8   15    10
            var end = new DateTime(2022, 02, 12, 13, 0, 0); // 13   19    12
            var configToApply = paymentConfig.Where(c => c.Day.Equals("MO")
                                && ((c.StartTime <= start && c.EndTime >= start)
                                || (c.StartTime <= end && c.EndTime >= end))
                                ).ToList();

            double totalHours = 0;
            
            foreach (var rangeConfigPayment in configToApply) {
                var salary = (double) rangeConfigPayment.Salary;
                var hours = GetWorkingHours(rangeConfigPayment.StartTime, rangeConfigPayment.EndTime, start, end);
                Console.WriteLine($"Config   >>>> {rangeConfigPayment.StartTime.ToShortTimeString()} - {rangeConfigPayment.EndTime.ToShortTimeString()}");
                Console.WriteLine($"Workging >>>> {start.ToShortTimeString()} - {end.ToShortTimeString()}");
                Console.WriteLine($"Hours    >>>> {Math.Ceiling(hours)}");
                Console.WriteLine($"Paying   >>>> {Math.Ceiling(hours) * salary}");

                totalHours += Math.Ceiling(hours);
                moneyToPay += (Math.Ceiling(hours) * salary);
            }
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine($"Total Hours    >>>> {totalHours}");
            Console.WriteLine($"Total Paying   >>>> {moneyToPay}");

            //if (configToApply.Count > 1) {
            //    foreach (var range in configToApply) {

            //    }
            //} else {
            //    double hoursToPay = end.Subtract(start).TotalHours;
            //    double moneyXHour = (double)configToApply.FirstOrDefault().Salary;
            //    moneyToPay = moneyXHour * hoursToPay;
            //}





            
            Console.ReadKey();
        }

        
    }
}
