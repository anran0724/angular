// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalAuthProviderApiBase
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using Abp.Dependency;
using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public abstract class ExternalAuthProviderApiBase : IExternalAuthProviderApi, ITransientDependency
  {
    public ExternalLoginProviderInfo ProviderInfo { get; set; }

    public void Initialize(ExternalLoginProviderInfo providerInfo)
    {
      this.ProviderInfo = providerInfo;
    }

    public async Task<bool> IsValidUser(string userId, string accessCode)
    {
      return (await this.GetUserInfo(accessCode)).ProviderKey == userId;
    }

    public abstract Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);
  }
}
