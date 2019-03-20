using Sinodom.ElevatorCloud.Auditing;
using Shouldly;
using Xunit;

namespace Sinodom.ElevatorCloud.Tests.Auditing
{
    public class NamespaceStripper_Tests: AppTestBase
    {
        private readonly INamespaceStripper _namespaceStripper;

        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("Sinodom.ElevatorCloud.Web.Controllers.HomeController");
            controllerName.ShouldBe("HomeController");
        }

        [Theory]
        [InlineData("Sinodom.ElevatorCloud.Auditing.GenericEntityService`1[[Sinodom.ElevatorCloud.Storage.BinaryObject, Sinodom.ElevatorCloud.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]", "GenericEntityService<BinaryObject>")]
        [InlineData("CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...", "EntityService<Book, CreateInput>")]
        [InlineData("Sinodom.ElevatorCloud.Auditing.XEntityService`1[Sinodom.ElevatorCloud.Auditing.AService`5[[Sinodom.ElevatorCloud.Storage.BinaryObject, Sinodom.ElevatorCloud.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[Sinodom.ElevatorCloud.Storage.TestObject, Sinodom.ElevatorCloud.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]", "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            genericServiceName.ShouldBe(result);
        }
    }
}
