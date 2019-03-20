using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sinodom.ElevatorCloud.MultiTenancy.Accounting.Dto;

namespace Sinodom.ElevatorCloud.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
