using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Sinodom.ElevatorCloud.EntityFrameworkCore
{
    public static class ElevatorCloudDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ElevatorCloudDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ElevatorCloudDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
