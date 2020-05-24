// Author Tolulope Ademilua.   
// Arvato.   
// Created on 24.05.2020
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
namespace API_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api-test.afterpay.dev/api/v3/validate/bank-account");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");
                var bankAccountDic = new Dictionary<string, string>{
                    { "bankAccount", "GB09HAOE91311808002317"}
                };
                var header = "application/json";
                var json = JsonConvert.SerializeObject(bankAccountDic, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, header);
                var response = client.PostAsync(client.BaseAddress, stringContent).Result;
                var statusCode = response.StatusCode.ToString();
                var jsonFinal = response.Content.ReadAsStringAsync().Result;
                JObject jsonTest = JObject.Parse(jsonFinal);
                var isValid = jsonTest["isValid"].ToString();
                if (response.IsSuccessStatusCode)
                 {
                    Console.Write(jsonTest);
                       
                 }
                 else
                  Console.Write("status code is {0}, ", statusCode);   
            }

        }

    }
}
