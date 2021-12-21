using HospitalApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp.API.Patient.AddPatient
{
    public class AddPatientService
    {
        public async Task<bool> PostPatient(AddPatient model)
        {
            var response = false;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AllApi.PATINET_ADD);
                client.DefaultRequestHeaders.Add("Authorization", $"token {MainWindowViewModel.user_token}");

                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage result = await client.PostAsync(client.BaseAddress, content))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        response = true;
                    }
                }
            }
            return response;
        }
    }
}
