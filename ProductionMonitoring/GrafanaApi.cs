using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMonitoring
{
    public class GrafanaApi
    {
        private readonly Uri baseUrl = new Uri("http://grafana:3000");

        public async Task<LoginCommandResult> Login(LoginCommand loginCommand)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{loginCommand.Username}:{loginCommand.Password}"));
                    client.BaseAddress = baseUrl;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
                    var response = await client.GetAsync("/api/orgs");
                    return new LoginCommandResult("success!");
                }
            }
            catch (Exception exc)
            {
                return new LoginCommandResult($"failure! {exc.Message}");
            }
        }
    }
}
