using Akka.Actor;
using ProductionMonitoring.Server;
using System;

namespace ProductionMonitoring.Client.Actors
{
    public class AdminActor : ReceiveActor
    {
        private Guid sessionId;

        protected override void PreStart()
        {
            sessionId = Guid.NewGuid();
            var remote = Context.ActorSelection("akka.tcp://pm-server@localhost:8080/user/sessions");
            remote.Tell(new OpenSession(sessionId));
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new AdminActor());
        }
    }
}
