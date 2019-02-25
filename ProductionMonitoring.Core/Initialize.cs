using Akka.Actor;
using Akka.Configuration;
using ProductionMonitoring.Admin;
using ProductionMonitoring.Server;
using System.Threading.Tasks;

namespace ProductionMonitoring
{
    public static class Initialize
    {
        public static Task Server()
        {
            var config = @"
                akka {  
                    actor {
                        provider = remote
                    }
                    remote {
                        dot-netty.tcp {
                            port = 8080
                            hostname = localhost
                        }
                    }
                }";
            var sys = ActorSystem.Create("pm-server", ConfigurationFactory.ParseString(config));
            sys.ActorOf(ServerActor.Props());
            return sys.WhenTerminated;
        }

        public static Task Admin()
        {
            var config = @"
                akka {  
                    actor {
                        provider = remote
                    }
                    remote {
                        dot-netty.tcp {
                            port = 0
                            hostname = localhost
                        }
                    }
                }";
            var sys = ActorSystem.Create("pm-admin", ConfigurationFactory.ParseString(config));
            sys.ActorOf(AdminActor.Props());
            return sys.WhenTerminated;
        }
    }
}
