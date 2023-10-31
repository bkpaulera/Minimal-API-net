using Azure.Core;

namespace API_Test
{
    public class UnitTest1
    {
        private readonly HttpClient _client;

        public UnitTest1()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:7175");
           
        }

        [Fact]
        public async Task TestSeuEndpoint()
        {
            try
            {
                var response = await _client.GetAsync("/api/User/Login-User");
               WebApi.Domain.Request.UserRequest req = new WebApi.Domain.Request.UserRequest();

                WebApi.Controllers.Auth.UserController.Logins(req);

            Assert.True(response.IsSuccessStatusCode);

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("sua resposta esperada", responseString);

            }catch (Exception ex)
            {

            }
        }
    }

}