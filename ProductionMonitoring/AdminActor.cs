using Akka.Actor;
using System;

namespace ProductionMonitoring
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
