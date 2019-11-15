# Foreign Exchange with Front-end

This is a project that reads in data from a CSV file and makes calculations to exchange amounts of money from one currency to another. It makes use of Razor pages from ASAP.net and is coded in C#. The code for doing the exchange is found in the  **Pages/Index.cshtml.cs** file along with the **transaction.cs** file for modelling the data and the **index.cshtml** file for the front-end.

## Installation

To run this project you will need to have .NET installed on your machine. Once you have that installed and set up, create a folder and pull down the project using this address.


[https://github.com/conorcorcoran/ForeignExchangeWithFrontEnd.git](https://github.com/conorcorcoran/ForeignExchangeWithFrontEnd.git)


## Usage
Use the following command in the root directory of the project to run it. In this case a folder called 'foreignExchange' was created.

```console
.\foreignExchange dotnet run
```
It should start on [https://localhost:5001/](https://localhost:5001/) . Check the command line if it specifies elsewhere. 

There you will see a print out of the transactions along with the time and date they have been completed at. It also gives the name of the file the data came from. These files are found in the **foreignExchange \TransactionFiles** .

## Files
The files are in the following format: 


|ID   | Source Currency|Destination Currency|Amount |
| --- | ---            | ---                | ---   |
|1    |USD             |EUR                 |100    |
|2    |EUR             |AUD                 |2050.5 |

Currently they are all .csv files and are handled as such. 

Future requirements may include other types such as JSON data. This would require some minor changes to the transaction code but as long as the format says the same there should be no problems.

## Future developments
The API that was used for getting the currency conversions was from [Fixer.io](https://fixer.io) . The free version has some limitations. It only allows 1000 requests per month and only gives a basic response with all currencies against the Euro.

Paid versions allow you to do the conversion with the API and get the amount as a response. This would cut down on the code needed. The rates are only updated every 60 minutes with the free version as well which could result different than expected sums. However, there might be an agreement to pick a time to do the exchange so both parties know which rate the transaction will be done at. 
