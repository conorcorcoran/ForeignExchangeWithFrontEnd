using System;
namespace fxUI
{
    /*
        Class for modeling the Transaction object. Contains fields for all the properties that need
        to be kept track of.
    */
    public class Transaction{
        private int id;
        private string fromFile;
        private string sourceCurrency;
        private string destinationCurrency;
        private double sourceAmount;
        private double convertedAmount;
        private double exchangeRate;
        private DateTime date;
        public Transaction(int id, string fromFile, string sourceCurrency, string destinationCurrency, double sourceAmount, double convertedAmount, double exchangeRate){
            this.id = id;
            this.fromFile = fromFile;
            this.sourceCurrency = sourceCurrency;
            this.destinationCurrency = destinationCurrency;
            this.sourceAmount = sourceAmount;
            this.convertedAmount = convertedAmount;
            this.exchangeRate = exchangeRate;
            date = DateTime.Now;
        }

        /*
            Getters
        */
        public int Id {
            get{
                return id;
            }
        }

        public string SourceCurrency { 
            get{
                return sourceCurrency;
            } 
        }

        public string DestinationCurrency { 
            get{
                return destinationCurrency;
            }
        }

        public double SourceAmount{
            get{
                return sourceAmount;
            }        
        }

        public double ConvertedAmount{ 
            get{
                return convertedAmount;
            }
        }

        public double ExchangeRate{
            get{
                return exchangeRate;
            }
        }

        public DateTime Date{
            get{
                return date;
            }
        }

        public string FromFile{
            get{
                return fromFile;
            }
        }

        public string printTransaction(){

            return "ID: " + id + 
            " Base Currency: " + sourceCurrency+ 
            " Destination Currency: " + destinationCurrency + 
            " Amount: " + sourceAmount + 
            " Coverted amount: " + convertedAmount + 
            " Excahnge rate: " + (convertedAmount/sourceAmount);
        }
    }
}