using DataModels.RequestModels;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Services.Implementations
{
    public class GroupClient : IGroupClient
    {
        private readonly string _host;

        public GroupClient(string host = "http://localhost:51004/")
        {
            _host = host;
        }

        public async Task<IEnumerable<GroupMessage>> GetAllGroupsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_host);

                var request = new HttpRequestMessage(HttpMethod.Get, "gateway/groups");

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<GroupMessage>>(responseString);
            }
        }

        public async Task<GroupMessage> GetGroupByIdAsync(long groupId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_host);

                var request = new HttpRequestMessage(HttpMethod.Get, $"gateway/group?groupId={groupId}");

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                
                return JsonConvert.DeserializeObject<GroupMessage>(responseString);
            }
        }

        public async Task<GroupMessage> AddGroupAsync(GroupMessage groupRequest)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_host);

                var request = new HttpRequestMessage(HttpMethod.Post, $"gateway/group");
                var content = new StringContent(JsonConvert.SerializeObject(groupRequest), Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<GroupMessage>(responseString);
            }
        }

        public async Task<GroupMessage> UpdateGroupAsync(GroupMessage groupRequest)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_host);

                var request = new HttpRequestMessage(HttpMethod.Put, $"gateway/group");
                var content = new StringContent(JsonConvert.SerializeObject(groupRequest), Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<GroupMessage>(responseString);
            }
        }
    }
}
