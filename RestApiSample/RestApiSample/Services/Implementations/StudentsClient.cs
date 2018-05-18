using DataModels.DTO;
using DataModels.RequestModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Services
{
    public class StudentsClient : IStudentsClient
    {
        private readonly string _host;

        public StudentsClient(string host = "http://localhost:50860/")
        {
            _host = host;
        }

        public async Task<IEnumerable<StudentMessage>> GetStudentsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_host);

                var request = new HttpRequestMessage(HttpMethod.Get, "gateway/students");

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<StudentMessage>>(responseString);
            }
        }

        public async Task<StudentMessage> GetStudentByIdAsync(long studentId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_host);

                var request = new HttpRequestMessage(HttpMethod.Get, $"gateway/student?studentId={studentId}");

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<StudentMessage>(responseString);
            }
        }

        public async Task<StudentMessage> AddStudentAsync(StudentMessage studentRequest)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_host);

                var request = new HttpRequestMessage(HttpMethod.Post, $"gateway/student");
                var content = new StringContent(JsonConvert.SerializeObject(studentRequest), Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<StudentMessage>(responseString);
            }
        }

        public async Task<StudentMessage> UpdateStudentAsync(StudentMessage studentRequest)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_host);

                var request = new HttpRequestMessage(HttpMethod.Put, $"gateway/student");
                var content = new StringContent(JsonConvert.SerializeObject(studentRequest), Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<StudentMessage>(responseString);
            }
        }

        public async Task<IEnumerable<StudentMessage>> GetStudentsOfGroupAsync(long groupId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_host);

                var request = new HttpRequestMessage(HttpMethod.Get, $"gateway/students/{groupId}");

                var response = await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<StudentMessage>>(responseString);
            }
        }
    }
}
