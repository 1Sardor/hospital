using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp.API.Login
{
    public class LoginService
    {
        public async Task<string> Post(Login model)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AllApi.LOGIN);

                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage result = await client.PostAsync(client.BaseAddress, content))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        response = await result.Content.ReadAsStringAsync();

                        LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response);

                        if (loginResponse.status != 200)
                        {
                            response = "error";
                        }

                    }

                    else
                    {
                        response = "error";
                    }
                }
            }
            return response;
        }
    }
}
