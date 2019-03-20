using System.Data.SqlClient;
using Shouldly;
using Xunit;

namespace Sinodom.ElevatorCloud.Tests.General
{
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=localhost; Database=ElevatorCloud; Trusted_Connection=True;");
            csb["Database"].ShouldBe("ElevatorCloud");
        }
    }
}
