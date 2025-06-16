# Challenge

## The Exercise

The company ACME offers their employees the flexibility to work the hours they want. They will pay for the hours worked based on the day of the week and time of day, according to the following table:

```cmd
Monday - Friday

00:01 - 09:00 25 USD

09:01 - 18:00 15 USD

18:01 - 00:00 20 USD

Saturday and Sunday

00:01 - 09:00 30 USD

09:01 - 18:00 20 USD

18:01 - 00:00 25 USD

```
The goal of this exercise is to calculate the total that the company has to pay an employee, based on the hours they worked and the times during which they worked. The following abbreviations will be used for entering data:

```cmd
MO: Monday

TU: Tuesday

WE: Wednesday

TH: Thursday

FR: Friday

SA: Saturday

SU: Sunday
```

**Input**: the name of an employee and the schedule they worked, indicating the time and hours. This should be a .txt file with at least five sets of data. You can include the data from our two examples below.

**Output**: indicate how much the employee has to be paid

For example:

Case 1:
```cmd
INPUT

RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00
```
OUTPUT:
```cmd
The amount to pay RENE is: 215 USD
```
Case 2:

INPUT
```cmd
ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00
```
OUTPUT:
```cmd
The amount to pay ASTRID is: 85 USD
```
Don’t forget to include a README.md file. Your README should include an overview of your solution, an explanation of your architecture, an explanation of your approach and methodology and instructions how to run the program locally.

We evaluate many aspects, including how well your code is structured, applied pattern designs, testing and the quality of your solution.

The solution shouldn’t need any UI, a console application is good enough.