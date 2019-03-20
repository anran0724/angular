using System.Threading.Tasks;
using Abp.Application.Services;
using Sinodom.ElevatorCloud.Authorization.Accounts.Dto;

namespace Sinodom.ElevatorCloud.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<int?> ResolveTenantId(ResolveTenantIdInput input);

        Task<RegisterOutput> Register(RegisterInput input);

        Task SendPasswordResetCode(SendPasswordResetCodeInput input);

        Task<ResetPasswordOutput> ResetPassword(ResetPasswordInput input);

        Task SendEmailActivationLink(SendEmailActivationLinkInput input);

        Task ActivateEmail(ActivateEmailInput input);

        Task<ImpersonateOutput> Impersonate(ImpersonateInput input);

        Task<ImpersonateOutput> BackToImpersonator();

        Task<SwitchToLinkedAccountOutput> SwitchToLinkedAccount(SwitchToLinkedAccountInput input);

        Task<QueryTenantStatusOutput> QueryTenantStatus(QueryTenantStatusInput input);
    }
}
