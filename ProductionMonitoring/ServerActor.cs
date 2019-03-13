using Akka.Actor;
using System;

namespace ProductionMonitoring
{
    public delegate IActorRef ServerActorProvider();

    public class ServerActor : ReceiveActor
    {
        private readonly GrafanaApi grafana;

        public ServerActor(GrafanaApi grafana)
        {
            this.grafana = grafana;

            Receive<LoginCommand>(cmd =>
            {
                grafana.Login(cmd).PipeTo(Sender);
            });
        }
    }

    public sealed class LoginCommand
    {
        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }

        public string Password { get; }
    }

    public sealed class LoginCommandResult
    {
        public LoginCommandResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}
