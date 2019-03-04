using Akka.Actor;
using System;

namespace ProductionMonitoring.Server
{
    public class SessionManagerActor : ReceiveActor
    {
        public SessionManagerActor()
        {
            Receive<OpenSession>(OnOpenSession);
        }

        public bool OnOpenSession(OpenSession data)
        {
            // do nothing
            return true;
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new SessionManagerActor());
        }
    }

    public sealed class OpenSession
    {
        public OpenSession(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }
    }
}
