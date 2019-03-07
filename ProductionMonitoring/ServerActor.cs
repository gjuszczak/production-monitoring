using Akka.Actor;
using System;

namespace ProductionMonitoring
{
    public delegate IActorRef ServerActorProvider();

    public class ServerActor : ReceiveActor
    {
        public ServerActor()
        {
            Receive<LoginCommand>(cmd =>
            {
                Console.WriteLine("Login");
                Sender.Tell(new LoginCommandResult("Success!"));
            });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ServerActor());
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
        public LoginCommandResult(string status)
        {
            Status = status;
        }

        public string Status { get; }
    }
}
