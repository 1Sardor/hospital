using HospitalApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HospitalApp.API.Retsept.RetseptItem
{
    public class RetseptItemService
    {
        public async Task<List<RetseptItem>> GetAllRetseptItems(int retsept_id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{AllApi.RETSEPT_ITEM}?retsept={retsept_id}");
                client.DefaultRequestHeaders.Add("Authorization", $"token {MainWindowViewModel.user_token}");

                using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var retsepitems = JsonConvert.DeserializeObject<List<RetseptItem>>(content);
                    return retsepitems;
                }
            }
        }
    }
}
