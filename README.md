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

## License
[MIT](https://choosealicense.com/licenses/mit/)
