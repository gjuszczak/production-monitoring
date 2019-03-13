using Newtonsoft.Json.Linq;
using ProductionMonitoring.Extensions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductionMonitoring
{
    public class GrafanaApi
    {
        private readonly Uri baseUrl = new Uri("http://localhost:3000");

        public async Task<LoginCommandResult> Login(LoginCommand loginCommand)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client
                        .BaseUri(baseUrl)
                        .BasicAuth(loginCommand.Username, loginCommand.Password)
                        .GetAsync("/api/orgs")
                        .ReadJsonContentAsync<JContainer>();
                                        
                    return new LoginCommandResult(true);
                }
            }
            catch (Exception exc)
            {
                return new LoginCommandResult(false);
            }
        }
    }
}
