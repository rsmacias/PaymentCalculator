## References

https://blogs.oracle.com/financialservices/post/domaindriven-design-is-at-the-core-of-composable-banking

https://www.milanjovanovic.tech/blog/clean-architecture-folder-structure

banking domain model
https://medium.com/airwallex-engineering/domain-driven-design-practice-modeling-payments-system-f7bc5cf64bb3
https://medium.com/@shek.up/domain-driven-design-for-banking-application-d6279ecdaf2
https://bian.org/wp-content/uploads/2021/03/BIAN_Implementation_Examples_v1.pdf
https://medium.com/airwallex-engineering/domain-driven-design-practice-modeling-payments-system-f7bc5cf64bb3
https://medium.com/@shek.up/domain-driven-design-for-banking-application-d6279ecdaf2
https://pgiriraj4.medium.com/domain-driven-design-in-banking-c445aacfd2d2
https://creately.com/diagram/example/i12t8eep1/domain-model-diagram-of-a-banking-system
https://www.gcreddy.com/2021/09/banking-domain-fundamentals.html
https://medium.com/@sarahisdevs/understanding-the-world-of-event-driven-architecture-ce4bfbcf1cbd
https://medium.com/@daniel.somerfield/domain-driven-design-in-data-engineering-part-1-of-3-2441ecce883b
https://medium.com/@daniel.somerfield/domain-driven-design-in-data-engineering-part-2-of-3-0156ebc71766




Hi Robert,

We are happy that you are interested in being part of ioet. As part of the recruitment process we would like you to solve a programming exercise to evaluate your skills and later we will schedule a meeting to discuss your solution. It’s important to notice that you can use any programming language you want but you can not use any external library to solve the exercise, however you can add any dependency for testing purposes like JUnit, Mockito, etc.

This exercise should be completed within 3 days. If for some reason you are unable to finish on time, please let us know.

Exercise

The company ACME offers their employees the flexibility to work the hours they want. They will pay for the hours worked based on the day of the week and time of day, according to the following table:

Monday - Friday

00:01 - 09:00 25 USD

09:01 - 18:00 15 USD

18:01 - 00:00 20 USD

Saturday and Sunday

00:01 - 09:00 30 USD

09:01 - 18:00 20 USD

18:01 - 00:00 25 USD

The goal of this exercise is to calculate the total that the company has to pay an employee, based on the hours they worked and the times during which they worked. The following abbreviations will be used for entering data:

MO: Monday

TU: Tuesday

WE: Wednesday

TH: Thursday

FR: Friday

SA: Saturday

SU: Sunday

Input: the name of an employee and the schedule they worked, indicating the time and hours. This should be a .txt file with at least five sets of data. You can include the data from our two examples below.

Output: indicate how much the employee has to be paid

For example:

Case 1:

INPUT

RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00

OUTPUT:

The amount to pay RENE is: 215 USD

Case 2:

INPUT

ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00

OUTPUT:

The amount to pay ASTRID is: 85 USD

Once you have finished the exercise, please upload the solution to GitHub and send us the link. Don’t forget to include a README.md file. Your README should include an overview of your solution, an explanation of your architecture, an explanation of your approach and methodology and instructions how to run the program locally.

We evaluate many aspects, including how well your code is structured, applied pattern designs, testing and the quality of your solution.

The solution shouldn’t need any UI, a console application is good enough.

When submitting your exercise, be sure to avoid including compiled files as this could be considered malware. Please include the proper instructions to compile your project in the README file