using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace StockAlert.Helpers
{
    class GoogleFinanceApiConnector 
    {
        private static async Task<string> GetGoogleFinTicker(string ticker)
        {
            using(System.Net.Http.HttpClient HC = new System.Net.Http.HttpClient())
            {
                Console.WriteLine(ticker);
                return await HC.GetStringAsync("https://www.google.com/finance/"+"info?q="+ticker);                
            }
        }
        public static string GetRequest(string tic){
            if(tic != null){
                try{
                    var res = GetGoogleFinTicker(tic).Result;
                    res = res.Replace('[', ' ');
                    res = res.Replace(']',' ');
                    return (res.Replace('/', ' '));
                }catch(Exception e){
                    Console.WriteLine("Exception: ");
                    Console.WriteLine(e.Message);
                    return ("ERROR: "+e.Message);
                }
            } 
            throw new Exception("tic args is null"); 
        }
    }
}