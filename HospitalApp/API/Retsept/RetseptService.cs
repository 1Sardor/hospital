using HospitalApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HospitalApp.API.Retsept
{
    public class RetseptService
    {
        public async Task<List<Retsept>> GetAllRetsepts(int patient_id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{AllApi.RETSEPTS}?client={patient_id}");
                client.DefaultRequestHeaders.Add("Authorization", $"token {MainWindowViewModel.user_token}");

                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var retsepts = JsonConvert.DeserializeObject<List<Retsept>>(content);
                    return retsepts;
                }
            }
        }
    }
}
