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

        static double GetWorkingHours(DateTime config_start_time, DateTime config_end_time, DateTime worked_start_time, DateTime worked_end_time) {

            var result = DateTime.Compare(worked_start_time, config_start_time);
            var ini = result >= 0 ? worked_start_time : config_start_time;

            var result2 = DateTime.Compare(worked_end_time, config_end_time);
            var fin = result2 < 0 ? worked_end_time : config_end_time;

            return fin.Subtract(ini).TotalHours;
        }

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
            foreach (var employeeSchedule in EmployeesScheduleWorked) {
                double moneyToPay = 0;
                double totalHours = 0;

                Console.WriteLine(string.Empty);
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine($"Employee: {employeeSchedule.EmployeeName}");
                foreach (var workedDay in employeeSchedule.Schedule) {
                    var day = workedDay.Day;
                    var start = workedDay.StartTime;
                    var end = workedDay.EndTime;

                    // Filtro de la configuración los rangos de horas a aplicar
                    var configToApply = paymentConfig.Where(c => c.Day.Equals(day)
                                && ((c.StartTime <= start && c.EndTime >= start)
                                || (c.StartTime <= end && c.EndTime >= end))
                                ).ToList();

                    foreach (var rangeConfigPayment in configToApply) {
                        var salaryByHour = (double)rangeConfigPayment.Salary;
                        var workedHours = GetWorkingHours(rangeConfigPayment.StartTime, rangeConfigPayment.EndTime, start, end);
                        Console.WriteLine("--------------------------------------------------------");
                        Console.WriteLine($"Config Range Hours   >>>> {rangeConfigPayment.StartTime.ToShortTimeString()} - {rangeConfigPayment.EndTime.ToShortTimeString()}");
                        Console.WriteLine($"Working Range Hours  >>>> {start.ToShortTimeString()} - {end.ToShortTimeString()}");
                        Console.WriteLine($"Workend Hours        >>>> {Math.Ceiling(workedHours)}");
                        Console.WriteLine($"Payment to do        >>>> {Math.Ceiling(workedHours) * salaryByHour}");

                        totalHours += Math.Ceiling(workedHours);
                        moneyToPay += (Math.Ceiling(workedHours) * salaryByHour);
                    }
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine($"Final Hours     >>>> {totalHours}");
                    Console.WriteLine($"Final Payment   >>>> {moneyToPay}");
                    Console.WriteLine($"The amount to pay {employeeSchedule.EmployeeName} is: {moneyToPay} USD ");

                }

            }

            Console.ReadKey();
        }

    }
}
