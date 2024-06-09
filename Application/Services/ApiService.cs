using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfApp3.Models;
using System.IO;

//APIkey in a extra dokument 

namespace WpfApp3.Services
{
    public static class ApiService
    {
        private static string apiUrl;
        private static string username;
        private static string password;

        static ApiService()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\felix\\Desktop\\WpfApp3 - Kopie (4)\\Application\\apiInfo.txt.txt");
            apiUrl = lines[0];
            username = lines[1];
            password = lines[2];
        }

        public static async Task<List<PatientData>> GetPatientsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
                client.DefaultRequestHeaders.Authorization = authValue;

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<PatientData>>(responseData);
                }
                else
                {
                    return null;
                }
            }
        }
        public static async Task<bool> AddPatientAsync(string Infos)
        {
            using (var httpClient = new HttpClient())
            {
                var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
                httpClient.DefaultRequestHeaders.Authorization = authValue;

                
                var content = new StringContent(Infos, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                return response.IsSuccessStatusCode;
            }
        }
        public static async Task<bool> EditPatient(string AlteredPatient, int ID)
        {
            using (var httpClient = new HttpClient())
            {
                var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
                httpClient.DefaultRequestHeaders.Authorization = authValue;

                //var jsonContent = JsonConvert.SerializeObject(AlteredPatient);
                //Console.WriteLine(jsonContent);
                var content = new StringContent(AlteredPatient, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync($"{apiUrl}/{ID}", content);
                Console.WriteLine(response.StatusCode);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
