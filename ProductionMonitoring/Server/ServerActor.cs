using Akka.Actor;

namespace ProductionMonitoring.Server
{
    public class ServerActor : ReceiveActor
    {
        private IActorRef sessionManager;

        protected override void PreStart()
        {
            sessionManager = Context.ActorOf(SessionManagerActor.Props());
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ServerActor());
        }
    }
}
