// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalLoginProviderInfo
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using System;
using System.Collections.Generic;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public class ExternalLoginProviderInfo
  {
    public string Name { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public Type ProviderApiType { get; set; }

    public Dictionary<string, string> AdditionalParams { get; set; }

    public ExternalLoginProviderInfo(string name, string clientId, string clientSecret, Type providerApiType, Dictionary<string, string> additionalParams = null)
    {
      this.Name = name;
      this.ClientId = clientId;
      this.ClientSecret = clientSecret;
      this.ProviderApiType = providerApiType;
      this.AdditionalParams = additionalParams;
    }
  }
}
