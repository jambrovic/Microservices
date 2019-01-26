using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LocationService.Models;
using Newtonsoft.Json;

namespace TeamService.LocationClient
{
    public class HttpLocationClient : ILocationClient
    {
        public string URL { get; set; }

        public HttpLocationClient(string url)
        {
            URL = url;
        }
        public async Task<LocationRecord> GetLatestForMember(Guid memberId)
        {
            LocationRecord locationRecord = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(string.Format($"/locations/{memberId}/latest"));

                if(response.IsSuccessStatusCode)
                {
                    string json=await response.Content.ReadAsStringAsync();
                    locationRecord=JsonConvert.DeserializeObject<LocationRecord>(json);
                }
            }

            return locationRecord;
        }
    }
}