// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalAuthConfiguration
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using Abp.Dependency;
using System.Collections.Generic;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public class ExternalAuthConfiguration : IExternalAuthConfiguration, ISingletonDependency
  {
    public List<ExternalLoginProviderInfo> Providers { get; }

    public ExternalAuthConfiguration()
    {
      this.Providers = new List<ExternalLoginProviderInfo>();
    }
  }
}
