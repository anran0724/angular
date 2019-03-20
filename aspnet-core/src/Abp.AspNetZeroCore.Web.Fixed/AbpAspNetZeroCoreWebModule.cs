// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.AbpAspNetZeroCoreWebModule
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using Abp.AspNetCore;
using Abp.Modules;
using System;

namespace Abp.AspNetZeroCore.Web
{
  [DependsOn(new Type[] {typeof (AbpAspNetZeroCoreModule)})]
  [DependsOn(new Type[] {typeof (AbpAspNetCoreModule)})]
  public class AbpAspNetZeroCoreWebModule : AbpModule
  {
    public override void PreInitialize()
    {
    }

    public override void Initialize()
    {
      this.IocManager.RegisterAssemblyByConvention(typeof (AbpAspNetZeroCoreWebModule).Assembly);
    }
  }
}
