# Payment Calculator App
This is a console app to calculate the amount to pay to employees based on the hours which they worked in a specific schedule.

## Solution Overview 
It defines on a text file the amounts to pay per day and the hours worked in the week. The app reads this file to get at memory this configuration to calculate later the final amounts.

The same way the app reads another file to get the list of employees with their worked hours during the week. These data would be the input to the app.

Then the app calculates how much It must be paid to each employee and print the result on the screen.

## Architecture
I created a _class library_ project to encapsulate the functionality of calculating each amount to pay. Thus, this class library could be used as an interface independently, and the same way that it's invoked from the console app it can be used from another kind of interface, such as a windows form, a web service, or a web app.

Inside this _class library_, I've decoupled the main functionality of processing the data by an interface called **IPayrollPayment**. This way it can be used by dependency injection and it could be added new changes without danger in the future.
The same way it's decoupled the formula to calculate de amounts by another interface called **IPaymentCalculator**.

Thus this _class library_ receives only the necessary input to work:
- Payment configuration through a list of **PaymentConfiguration**.
- Input data to work through a list of **ScheduleWorked**.
- The proper implementation of how to calculate the salary by **IPaymentCalculator**.

Finally, this _class library_ returns the result through a list of **SchedulePaid**.

The _classes_ used as **DTO** are placed in the Model folder.
 
 ## Methodology 
- The main problem is how to define which configuration applies based on the worked hours. This is done by the following filter on the configuration:

```csharp
// Filtro de la configuración los rangos de horas a aplicar
var configToApply = _paymentConfig.Where(c => c.Day.Equals(day)
                                    && ((c.StartTime <= start && c.EndTime >= start)
                                    || (c.StartTime <= end && c.EndTime >= end))
                                    ).ToList();
```

- With this, the final step is to calculate the worked hours between the difference of configured range hours and the input range hours. This is made by the following method: 

```csharp
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
```

## Usage
1. Edit the file **PAYMENT_CONFIG.txt** if you would like to change the configuration of the amounts to pay per hour in the week. Note that this file is tabulated. Consider the following fields as columns of the file:
 - Day, from this _list_:  _MO_: Monday
_TU_: Tuesday
_WE_: Wednesday
_TH_: Thursday
_FR_: Friday
_SA_: Saturday
_SU_: Sunday
 - Start hour to work (_format:_ H:mm)
 - End hour to work   (_format:_ H:mm)
 - Amount to pay per hour

  Example:
```txt
MO	9:01	18:00	15
MO	18:01	0:00	20
TU	0:01	9:00	25
TU	9:01	18:00	15
```
2. Edit the file **INPUT_WORKING_HOURS.txt** if you would like to add or change the input data to test the app. Each line of the file describes a work schedule of an employee.
The string before "=" is the employee name. The string after "=" describes the worked hours each day separated by commas, where the first two characters define the day.

  Example:
```txt
RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00
ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00
```
3. Edit the **appsettings.json** file to define the path of your local machine where there will be located the previous files:

```json
{
  "input": {
    "file": "D:\\Projects\\INPUT_WORKING_HOURS.txt",
    "timeFormat": "H:mm"
  },
  "paymentConfig": {
    "file": "D:\\Projects\\PAYMENT_CONFIG.txt",
    "separator": "\t",
    "timeFormat": "H:mm"
  }
```

## License
[MIT](https://choosealicense.com/licenses/mit/)
