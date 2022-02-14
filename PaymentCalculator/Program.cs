using Payment.Lib;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PaymentCalculator {
    class Program {

        public static IConfigurationRoot _config;
        CultureInfo provider = CultureInfo.InvariantCulture;

        static void Main(string[] args) {
            try {
                var host = AppStartup();
                var serviceScope = host.Services.CreateScope();
                var serviceProvider = serviceScope.ServiceProvider;

                var _log = serviceProvider.GetRequiredService<ILogger<Program>>();

                // CARGA DE CONFIGUACIÓN INICIAL
                _log.LogInformation("Loading payment configuration...");
                var paymentConfig = LoadPaymentConfiguation();

                // CARGA DE DATOS DE ENTRADA
                _log.LogInformation("Loading input data...");
                var employeesScheduleWorked = LoadInputData();

                // CALCULO DE VALORES A PAGAR
                _log.LogInformation("Payment processing...");
                var _payrollPayment = serviceProvider.GetRequiredService<IPayrollPayment>();
                _payrollPayment.PaymentConfig = paymentConfig;

                var paySheet = _payrollPayment.Pay(employeesScheduleWorked);
                _log.LogInformation("Printing results...");
                foreach (var payment in paySheet) {
                    Console.WriteLine(string.Empty);
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine($"Employee: {payment.EmployeeName}");
                    Console.WriteLine($"Final Hours.....>>>> {payment.TotalHours}");
                    Console.WriteLine($"Final Payment...>>>> {payment.Amount}");
                    Console.WriteLine($"The amount to pay {payment.EmployeeName} is: {payment.Amount} USD ");
                    Console.WriteLine("--------------------------------------------------------");
                }

            } catch (Exception e) {
                Console.WriteLine($"Error not expected. \r\nDetails: {e.Message}");
            }

            Console.ReadKey();
        }

        static IHost AppStartup() {
            // 1.- Se define el archivo de configuración
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
            
            _config = builder.Build();
            // 2.- Se define: 
            //    2.1.- Contenedor de las dependencias
            //    2.2.- Configuración de los logs
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<IPaymentCalculator, Payment.Lib.PaymentCalculator>();
                    services.AddTransient<IPayrollPayment, PayrollPayment>();
                })
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.AddSimpleConsole(options => options.IncludeScopes = true);
                    logging.AddConsole();
                    logging.AddEventLog();
                })
                .Build();

            return host;
        }

        /// <summary>
        /// Cargar la configuración de los pagos por las horas trabajadas
        /// </summary>
        /// <returns>Objeto tipado con la configuración</returns>
        static List<PaymentConfiguration> LoadPaymentConfiguation () {
            try {
                List<PaymentConfiguration> paymentConfig = new List<PaymentConfiguration>();

                var config_payment_file = _config["paymentConfig:file"];
                var config_payment_sep_field = _config["paymentConfig:separator"];
                var config_payment_time_format = _config["paymentConfig:timeFormat"];

                //Console.WriteLine($"Archivo a cargar: {config_payment_file}");

                string[] lines = File.ReadAllLines(config_payment_file);
                foreach (string line in lines) {
                    string[] fields = line.Split(config_payment_sep_field.ToCharArray());

                    var inicio = DateTime.ParseExact(fields[1], config_payment_time_format, CultureInfo.InvariantCulture);
                    var fin = DateTime.ParseExact(fields[2], config_payment_time_format, CultureInfo.InvariantCulture);
                    if (fin.Subtract(inicio).TotalHours < 0) {
                        fin = fin.AddDays(1); // Esto es para resolver la diferencia negativa de horas
                    }

                    paymentConfig.Add(new PaymentConfiguration {
                        Day = fields[0],
                        StartTime = inicio,
                        EndTime = fin,
                        AmountByHour = double.Parse(fields[3])
                    });
                    //Console.WriteLine($"Day: {fields[0]}, StartHour:{fields[1]}, EndHour:{fields[2]}, Payment:{fields[3]}");
                }

                return paymentConfig;
            } catch (Exception e) {
                throw;
            }
        }

        /// <summary>
        /// Cargar los datos de los horarios de trabajo de los empleados
        /// </summary>
        /// <returns>Objeto tipado con los datos de entrada</returns>
        static List<ScheduleWorked> LoadInputData() {
            try {
                List<ScheduleWorked> EmployeesScheduleWorked = new List<ScheduleWorked>();

                var input_data_file = _config["input:file"];
                var input_data_time_format = _config["input:timeFormat"];

                string[] lines = File.ReadAllLines(input_data_file);

                foreach (string line in lines) {
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

                return EmployeesScheduleWorked;
            } catch (Exception e) {
                throw;
            }
        }

    }
}
