// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalAuthUserInfo
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public class ExternalAuthUserInfo
  {
    public string ProviderKey { get; set; }

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public string Surname { get; set; }

    public string Provider { get; set; }
  }
}
