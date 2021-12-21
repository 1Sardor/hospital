using Newtonsoft.Json;
using ReseptionApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReseptionApp.API.Patient
{
    public class PatientService
    {
        public async Task<List<Patient>> GetDoctors(string from_date, string to_date)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{AllApi.PATIENT}?st_date={from_date} 00:00:00&end_date={to_date} 23:59:59");
                client.DefaultRequestHeaders.Add("Authorization", $"token {MainWindowViewModel.user_token}");

                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var patients = JsonConvert.DeserializeObject<List<Patient>>(content);
                    return patients;
                }
            }
        }
    }
}
