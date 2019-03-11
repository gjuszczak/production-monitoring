using Akka.Actor;
using Akka.Configuration;
using Akka.DI.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ProductionMonitoring
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterPmServer(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var system = ActorSystem.Create("PmServer", provider.GetServerAkkaConfig());
                var di = new DependencyResolver(provider, system);
                system.AddDependencyResolver(di);
                return system;
            });
            services.AddSingleton<ServerActorProvider>(provider =>
            {
                var system = provider.GetService<ActorSystem>();
                var serverActor = system.ActorOf(system.DI().Props<ServerActor>(), "server");
                return () => serverActor;
            });

            // register services
            services.AddSingleton<PmApi>();
            services.AddSingleton<GrafanaApi>();

            // register actors
            services.AddTransient<ServerActor>();
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
