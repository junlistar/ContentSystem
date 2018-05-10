using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using ContentSystem.Business;
using ContentSystem.Core.Data;
using ContentSystem.Core.Infrastructure;
using ContentSystem.Core.Infrastructure.DependencyManagement;
using ContentSystem.Core.Infrastructure.TypeFinders;
using ContentSystem.Data;
using ContentSystem.Data.Repositories; 
using ContentSystem.Service; 
using System.Linq;
using System.Reflection;

namespace ContentSystem.Admin.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            #region 数据库

            const string MAIN_DB = "ContentDB";

            builder.Register(c => new ContentDbContext(MAIN_DB))
                    .As<IDbContext>()
                    .Named<IDbContext>(MAIN_DB)
                    .SingleInstance();

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(MAIN_DB))
                .SingleInstance();

            #endregion
             

            // 注入Business及接口
            builder.RegisterAssemblyTypes(typeof(UserInfoBusiness).Assembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
             

            builder.RegisterAssemblyTypes(typeof(UserInfoService).Assembly)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
        }
    }
}