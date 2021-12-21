using Newtonsoft.Json;
using ReseptionApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReseptionApp.API.Hospital
{
    public class HospitalService
    {
        public async Task<List<Hospital>> GetHospital()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{AllApi.HOSPITAL}");
                client.DefaultRequestHeaders.Add("Authorization", $"token {MainWindowViewModel.user_token}");

                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var hospitals = JsonConvert.DeserializeObject<List<Hospital>>(content);
                    return hospitals;
                }
            }
        }
    }
}
