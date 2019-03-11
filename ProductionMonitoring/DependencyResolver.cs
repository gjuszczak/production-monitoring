using Akka.Actor;
using Akka.DI.Core;
using System;
using System.Collections.Concurrent;

namespace ProductionMonitoring
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider provider;
        private readonly ConcurrentDictionary<string, Type> typeCache;
        private readonly ActorSystem system;

        public DependencyResolver(IServiceProvider provider, ActorSystem system)
        {
            this.provider = provider ?? throw new ArgumentNullException("serviceCollection");
            typeCache = new ConcurrentDictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
            this.system = system ?? throw new ArgumentNullException("system");
        }

        public Props Create<TActor>() where TActor : ActorBase
        {
            return Create(typeof(TActor));
        }

        public Props Create(Type actorType)
        {
            return system.GetExtension<DIExt>().Props(actorType);
        }

        public Func<ActorBase> CreateActorFactory(Type actorType)
        {
            return () => (ActorBase)provider.GetService(actorType);
        }

        public Type GetType(string actorName)
        {
            return typeCache.GetOrAdd(actorName, actorName.GetTypeValue());
        }

        public void Release(ActorBase actor)
        {
            // not used
        }
    }
}
