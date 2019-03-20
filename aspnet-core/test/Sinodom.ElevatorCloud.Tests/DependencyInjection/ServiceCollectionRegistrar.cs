using Abp.Dependency;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sinodom.ElevatorCloud.EntityFrameworkCore;
using Sinodom.ElevatorCloud.Identity;

namespace Sinodom.ElevatorCloud.Tests.DependencyInjection
{
    public static class ServiceCollectionRegistrar
    {
        public static void Register(IIocManager iocManager)
        {
            RegisterIdentity(iocManager);

            var builder = new DbContextOptionsBuilder<ElevatorCloudDbContext>();

            var inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            builder.UseSqlite(inMemorySqlite);

            iocManager.IocContainer.Register(
                Component
                    .For<DbContextOptions<ElevatorCloudDbContext>>()
                    .Instance(builder.Options)
                    .LifestyleSingleton()
            );

            inMemorySqlite.Open();
            new ElevatorCloudDbContext(builder.Options).Database.EnsureCreated();
        }

        private static void RegisterIdentity(IIocManager iocManager)
        {
            var services = new ServiceCollection();
            IdentityRegistrar.Register(services);
            WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, services);
        }
    }
}
