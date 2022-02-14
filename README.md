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

## License
[MIT](https://choosealicense.com/licenses/mit/)
