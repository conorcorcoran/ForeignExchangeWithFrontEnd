using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace fxUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            transactions.Clear();
            runTransactions(files);
        }

        public void OnGet()
        {

        }
        
        // URL to Fix.io api. This returns current exhange rate data.
        private const string URL = "http://data.fixer.io/api/latest?access_key=a94666c6342e3bbf7fb4c218f6afb915";
        public static List<fxUI.Transaction> transactions = new List<fxUI.Transaction>();
        private string[] files = Directory.GetFiles(@".\TransactionFiles", "*.csv");


        /*
            This is the main method for doing the transaction.
        */
        public void runTransactions(string[] files)
        {
            /* Gets the exchange rates from Fixer.io
               The API has different paid levels. The free version
               only allows you to get all current rates against the Euro.
               Paid version will allow the conversion to be done with the
               API with a specified base and destination currency.
            */
            dynamic exchangeData = JObject.Parse(HttpGet(URL));
            JObject rates = exchangeData.rates;

            // Creates a list of files names to be used later.
            List<string> fileNames = new List<string>();
            for(int i = 0; i < files.Length; i++){
                fileNames.Add(files[i].Substring(files[i].LastIndexOf(@"\") + 1));
            }
            /*
                Goes through each file in the collection of files.
            */
            int fileIndex = 0;
            foreach(string file in files){
                using(StreamReader reader = new StreamReader(file))
                {     
                    //A list for each column           
                    List<string> id = new List<string>();
                    List<string> sourceCurrency = new List<string>();
                    List<string> destinationCurrency = new List<string>();
                    List<string> sourceAmount = new List<string>();
              
                    //Spliting up the CSV and adding the values to the lists.
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        var items = values[0].Split(',');

                        id.Add(items[0]);
                        sourceCurrency.Add(items[1]);
                        destinationCurrency.Add(items[2]);
                        sourceAmount.Add(items[3]);
                    }
                    /*
                        The index 'i' repersents a line. Each line (Excluding the first as it's just text)
                        will be passed through this for loop where the values are passed to the calculateExchange
                        method. A transaction object is then made and that's added to a list of transactions.
                    */
                    for(int i = 1; i < id.Count; i++){
                        double exchangedAmount = calculateExchange(sourceCurrency[i], destinationCurrency[i], Convert.ToDouble(sourceAmount[i]), rates);
                        double exchangeRate = exchangedAmount/Convert.ToDouble(sourceAmount[i]);
                        Transaction currentTransaction = new Transaction(Int32.Parse(id[i]), fileNames[fileIndex],sourceCurrency[i], destinationCurrency[i],  Convert.ToDouble(sourceAmount[i]), exchangedAmount, exchangeRate);
                        transactions.Add(currentTransaction);
                    }
                }
                fileIndex++;
            }
        }

        /*
            Takes in the data from the CSV files and performs a calculation to determine the exchanged amount.
            A JObject of all the rates is also passed in. 
            A double containing the exchanged amount is returned. It is rounded using Math.round.
            This can be changed for production as rounding not done right can result in hugely different amounts
            over the course many transactions.
        */
        public static double calculateExchange(string srcCurr, string desCurr, double amount, JObject rates){
            double conversionPrice;

            if(srcCurr.Equals("EUR")){
                conversionPrice = rates[desCurr].ToObject<double>();
            }else if(desCurr.Equals("EUR")){
                double srcCurrToEuro = rates[srcCurr].ToObject<double>();
                conversionPrice = 1 /srcCurrToEuro;
            }else{
                double srcCurrToEuro = rates[srcCurr].ToObject<double>();
                double conversionPriceInEuro = 1/srcCurrToEuro;
                conversionPrice = conversionPriceInEuro * rates[desCurr].ToObject<double>();
            }

            double convertedCurrency = amount*conversionPrice;

            return Math.Round(convertedCurrency, 2);
        }

        /*
            For GET requests. Gives resposne as a string.
        */
        public static string HttpGet(string url){
           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
           HttpWebResponse response = (HttpWebResponse)request.GetResponse();

           try{
               using(Stream stream = response.GetResponseStream()){
                   StreamReader reader = new StreamReader(stream);

                   return reader.ReadToEnd();
               }
           } finally{
               response.Close();
           }
        }
    }
}
