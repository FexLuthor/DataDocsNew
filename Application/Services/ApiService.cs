using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfApp3.Models;

//APIkey in a extra dokument 

namespace WpfApp3.Services
{
    public static class ApiService
    {
        private static readonly string apiUrl = "https://www.datadoctors.patpatwithhat.de/api/patients/";
        private static readonly string username = "docData";
        private static readonly string password = "DataIsMyJam321#";

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
    }
}
