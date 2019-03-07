using Akka.Actor;
using System.Threading.Tasks;

namespace ProductionMonitoring
{
    public class PmApi
    {
        private readonly IActorRef serverActor;

        public PmApi(ServerActorProvider serverActorProvider)
        {
            serverActor = serverActorProvider();
        }

        public async Task<LoginCommandResult> Login(string username, string passsword)
        {
            return await serverActor.Ask<LoginCommandResult>(new LoginCommand(username, passsword));
        }
    }
}
