using Autofac;
using ContentSystem.Core.Infrastructure.TypeFinders;

namespace ContentSystem.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
