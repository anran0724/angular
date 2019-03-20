// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.AbpAspNetZeroCoreModule
// Assembly: Abp.AspNetZeroCore, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: D5CD2351-ABB9-413A-813D-612DFA4D9D64

using Abp.AspNetZeroCore.Licensing;
using Abp.Dependency;
using Abp.Modules;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Abp.Reflection.Extensions;

namespace Abp.AspNetZeroCore
{
  public class AbpAspNetZeroCoreModule : AbpModule
  {
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override void PreInitialize()
    {
      IocManager.Register<AspNetZeroConfiguration>(DependencyLifeStyle.Singleton);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public override void Initialize()
    {
      // ISSUE: type reference
      IocManager.RegisterAssemblyByConvention(typeof(AbpAspNetZeroCoreModule).GetAssembly());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public override void PostInitialize()
    {
      
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public AbpAspNetZeroCoreModule()
    {
      
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool Cr1x3hm34wnOq82Qtr()
    {
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool PZcJkcXkl6FM93gnLO()
    {
      return false;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static Type M5mLX27cvYLN43fqWw([In] RuntimeTypeHandle obj0)
    {
      return Type.GetTypeFromHandle(obj0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void uMlB0JpGpuwMCUVirX([In] object obj0, [In] object obj1)
    {
      ((IIocRegistrar) obj0).RegisterAssemblyByConvention((Assembly) obj1);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void gTcHH04hig3sQ5jLid([In] object obj0)
    {
      ((AspNetZeroLicenseChecker) obj0).Check();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void iXU84KaHObVr4NhyPY([In] object obj0)
    {
      ((IDisposable) obj0).Dispose();
    }
  }
}
