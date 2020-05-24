// Author Tolulope Ademilua.   
// Arvato.   
// Created on 24.05.2020

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace API_Task
{
    class Test_Suite
    {
        
        [Test]
        public async Task isValidTrue()
        {
            using (var client = new HttpClient())
            {
                /*This test case has following test scenario:
                  Correct token with valid IBAN account are sent to correct API.
                  HTTP 200 or OK status is returned and isValid attribute has True value in response Body
                 */
                var header = "application/json";
                client.BaseAddress = new Uri("https://api-test.afterpay.dev/api/v3/validate/bank-account");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                client.DefaultRequestHeaders.Add("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");
                var bankAccountDic = new Dictionary<string, string>{
                    { "bankAccount", "GB09HAOE91311808002317"}
                };
                var json = JsonConvert.SerializeObject(bankAccountDic, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, header);
                var response = await client.PostAsync(client.BaseAddress, stringContent);
                var statusCode = response.StatusCode.ToString();
                var jsonFinal = await response.Content.ReadAsStringAsync();
                JObject json_test = JObject.Parse(jsonFinal);
                var isValid = json_test["isValid"].ToString();
                Assert.That(isValid, Is.EqualTo("True"));
                if (statusCode != "OK")
                {
                    Assert.Warn("Http status code is {0}", statusCode);
                }
            }

        }

        [Test]
        public async Task isValidFalse()
        {
            using (var client = new HttpClient())
            {
                /*This test case has following test scenario:
                  Correct token with invalid IBAN account are sent to correct API.
                  HTTP 200 or OK status is returned and isValid attribute has False value in response Body
                 */
                var header = "application/json";
                client.BaseAddress = new Uri("https://api-test.afterpay.dev/api/v3/validate/bank-account");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                client.DefaultRequestHeaders.Add("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");
                var bankAccountDic = new Dictionary<string, string>{
                    { "bankAccount", "GB09HAOE91311808002319"}//IBAN is incorrect
                };
                var json = JsonConvert.SerializeObject(bankAccountDic, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, header);
                var response = await client.PostAsync(client.BaseAddress, stringContent);
                var statusCode = response.StatusCode.ToString();
                var jsonFinal = await response.Content.ReadAsStringAsync();
                JObject jsonTest = JObject.Parse(jsonFinal);
                var isValid = jsonTest["isValid"].ToString();
                Assert.That(isValid, Is.EqualTo("False"));
                if (statusCode != "OK")
                {
                    Assert.Warn("Http status code is {0}", statusCode);
                }
            }

        }

        [Test]
        public async Task missingToken()
        {
            using (var client = new HttpClient())
            {
                /*This test case has following test scenario:
                  Valid IBAN account and missing token are sent to correct API.
                  HTTP 401 or Unauthorized status is returned and message attribute has value as "Authorization has been denied for this request" in response Body
                 */
                var header = "application/json";
                client.BaseAddress = new Uri("https://api-test.afterpay.dev/api/v3/validate/bank-account");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                client.DefaultRequestHeaders.Add("X-Auth-Key", ""); // Missing Token
                var bankAccountDic = new Dictionary<string, string>{
                    { "bankAccount", "GB09HAOE91311808002317"}
                };
                var json = JsonConvert.SerializeObject(bankAccountDic, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, header);
                var response = await client.PostAsync(client.BaseAddress, stringContent);
                var statusCode = response.StatusCode.ToString();
                var jsonFinal = await response.Content.ReadAsStringAsync();
                JObject jsonTest = JObject.Parse(jsonFinal);
                var message = jsonTest["message"].ToString();
                Assert.That(message, Is.EqualTo("Authorization has been denied for this request."));
                Assert.That(statusCode, Is.EqualTo("Unauthorized")); // Asserting that 401 eqaul to  Unauthorized
                if (statusCode != "Unauthorized")
                {
                    Assert.Warn("Http status code is {0}", statusCode);
                }

            }

        }

        [Test]
        public async Task incorrectToken()
        {
            /*This test case has following test scenario:
                 Valid IBAN account and incorrect token are sent to correct API.
                 HTTP 401 or Unauthorized status is returned and message attribute has value as "Authorization has been denied for this request" in response Body
                */
            using (var client = new HttpClient())
            {
                var header = "application/json";
                client.BaseAddress = new Uri("https://api-test.afterpay.dev/api/v3/validate/bank-account");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                client.DefaultRequestHeaders.Add("X-Auth-Key", "hjdhjdshjdsghdsgjjsdhshshhshs"); // Incorrect Token
                var bankAccountDic = new Dictionary<string, string>{
                    { "bankAccount", "GB09HAOE91311808002317"}
                };
                var json = JsonConvert.SerializeObject(bankAccountDic, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, header);
                var response = await client.PostAsync(client.BaseAddress, stringContent);
                var statusCode = response.StatusCode.ToString();
                var jsonFinal = await response.Content.ReadAsStringAsync();
                JObject jsonTest = JObject.Parse(jsonFinal);
                var message = jsonTest["message"].ToString();
                Assert.That(message, Is.EqualTo("Authorization has been denied for this request."));
                Assert.That(statusCode, Is.EqualTo("Unauthorized"));// Asserting that 401 eqaul to  Unauthorized
                if (statusCode != "Unauthorized")
                {
                    Assert.Warn("Http status code is {0}", statusCode);
                }
            }

        }

        [Test]
        public async Task incorrectIban()
        {
                /*This test case has the following test scenario:
                 Incorrect IBAN of bank account with shorter length and incorrect token are sent to correct API.
                 HTTP 400 or BadRequest status is returned and type attribute has value as "BussinessError" in response Body
               */
            using (var client = new HttpClient())
            {
                var header = "application/json";
                client.BaseAddress = new Uri("https://api-test.afterpay.dev/api/v3/validate/bank-account");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L"); 
                var bankAccountDic = new Dictionary<string, string>{
                    { "bankAccount", "123"} // IBAN is Incorrect and bank account has shorter length
                };
                var json = JsonConvert.SerializeObject(bankAccountDic, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, header);
                var response = await client.PostAsync(client.BaseAddress, stringContent);
                var statusCode = response.StatusCode.ToString();
                var jsonFinal = await response.Content.ReadAsStringAsync();
                string trimJsonFinal = jsonFinal.Trim().Trim('[', ']');
                JObject jsonTest = JObject.Parse(trimJsonFinal);
                var type = jsonTest["type"].ToString();
                Assert.That(type, Is.EqualTo("BusinessError"));
                Assert.That(statusCode, Is.EqualTo("BadRequest"));
                if (statusCode != "BadRequest")
                {
                    Assert.Warn("Http status code is {0}", statusCode);
                }
            }

        }

        [Test]
        public async Task missingIban()
        {
               /*This test case has the following test scenario:
                 Missing IBAN and correct token are sent to correct API.
                 HTTP 400 or BadRequest status is returned and type attribute has value as "BussinessError" in response Body
               */
            using (var client = new HttpClient())
            {
                var header = "application/json";
                client.BaseAddress = new Uri("https://api-test.afterpay.dev/api/v3/validate/bank-account");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                client.DefaultRequestHeaders.Add("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");
                 var bankAccountDic = new Dictionary<string, string>{
                    { "bankAccount", ""} // IBAN is missing
                };
                var json = JsonConvert.SerializeObject(bankAccountDic, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, header);
                var response = await client.PostAsync(client.BaseAddress, stringContent);
                var statusCode = response.StatusCode.ToString();
                var jsonFinal = await response.Content.ReadAsStringAsync();
                string trimJsonFinal = jsonFinal.Trim().Trim('[', ']');
                JObject jsonTest = JObject.Parse(trimJsonFinal);
                var type = jsonTest["type"].ToString();
                Assert.That(type, Is.EqualTo("BusinessError"));
                Assert.That(statusCode, Is.EqualTo("BadRequest"));
                if (statusCode != "BadRequest")
                {
                    Assert.Warn("Http status code is {0}", statusCode);
                }
            }

        }

        [Test]
        public async Task notIbanFormat()
        {
            /*This test case has the following test scenario:
              credit card which is not in IBAN format and correct token are sent to correct API.
              HTTP 200 or OK status is returned and isValid attribute has value as "False" in response Body
            */
            using (var client = new HttpClient())
            {
                var header = "application/json";
                client.BaseAddress = new Uri("https://api-test.afterpay.dev/api/v3/validate/bank-account");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                client.DefaultRequestHeaders.Add("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");
                var bankAccountDic = new Dictionary<string, string>{
                    { "bankAccount", "6770777646170561"} // credit card which is not in IBAN formathttps://www.bincodes.com/bank-creditcard-generator/
                };
                var json = JsonConvert.SerializeObject(bankAccountDic, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, header);
                var response = await client.PostAsync(client.BaseAddress, stringContent);
                var statusCode = response.StatusCode.ToString();
                var jsonFinal = await response.Content.ReadAsStringAsync();
                string trimJsonFinal = jsonFinal.Trim().Trim('[', ']');
                JObject jsonTest = JObject.Parse(trimJsonFinal);
                var isValid = jsonTest["isValid"].ToString();
                Assert.That(isValid, Is.EqualTo("False"));
                Assert.That(statusCode, Is.EqualTo("OK"));
                if (statusCode != "OK")
                {
                    Assert.Warn("Http status code is {0}",statusCode);
                }
                
            }

        }
    }
}




        