using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Sinodom.ElevatorCloud.Configuration;
using Sinodom.ElevatorCloud.Web;

namespace Sinodom.ElevatorCloud.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ElevatorCloudDbContextFactory : IDesignTimeDbContextFactory<ElevatorCloudDbContext>
    {
        public ElevatorCloudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ElevatorCloudDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            ElevatorCloudDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ElevatorCloudConsts.ConnectionStringName));

            return new ElevatorCloudDbContext(builder.Options);
        }
    }
}
