// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.IExternalAuthProviderApi
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public interface IExternalAuthProviderApi
  {
    ExternalLoginProviderInfo ProviderInfo { get; }

    Task<bool> IsValidUser(string userId, string accessCode);

    Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);

    void Initialize(ExternalLoginProviderInfo providerInfo);
  }
}
