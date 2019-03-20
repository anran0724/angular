// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.AspNetZeroConfigurationExtensions
// Assembly: Abp.AspNetZeroCore, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: D5CD2351-ABB9-413A-813D-612DFA4D9D64

using Abp.Configuration.Startup;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Abp.AspNetZeroCore
{
  public static class AspNetZeroConfigurationExtensions
  {
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static AspNetZeroConfiguration AspNetZero(this IModuleConfigurations modules)
    {
      return ((IAbpStartupConfiguration) AspNetZeroConfigurationExtensions.WaFdFW38y1gX4r3T4c((object) modules)).Get<AspNetZeroConfiguration>();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object WaFdFW38y1gX4r3T4c([In] object obj0)
    {
      return (object) ((IModuleConfigurations) obj0).AbpConfiguration;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool qlDkEQBxIqCdgneXkm()
    {
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool WEOIF5O4b4Fjai2qpf()
    {
      return false;
    }
  }
}
