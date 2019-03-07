using Akka.Actor;
using Akka.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ProductionMonitoring
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterPmServer(this IServiceCollection services)
        {
            services.AddSingleton(provider => ActorSystem.Create("PmServer", provider.GetServerAkkaConfig()));

            services.AddSingleton<ServerActorProvider>(provider =>
            {
                var actorSystem = provider.GetService<ActorSystem>();
                var serverActor = actorSystem.ActorOf(ServerActor.Props(), "server");
                return () => serverActor;
            });

            services.AddSingleton<PmApi>();
        }

        private static Config GetServerAkkaConfig(this IServiceProvider provider)
        {
            //var config = provider.GetService<IConfiguration>();
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
            return ConfigurationFactory.ParseString(config);
        }
    }
}
