using Newtonsoft.Json;
using ReseptionApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReseptionApp.API.Doctor
{
    public class DoctorService
    {
        public async Task<List<Doctor>> GetDoctors()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{AllApi.DOCTOR}");
                client.DefaultRequestHeaders.Add("Authorization", $"token {MainWindowViewModel.user_token}");

                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var doctors = JsonConvert.DeserializeObject<List<Doctor>>(content);
                    return doctors;
                }
            }
        }
    }
}
