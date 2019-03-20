// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Timing.AppTimes
// Assembly: Abp.AspNetZeroCore, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: D5CD2351-ABB9-413A-813D-612DFA4D9D64

using Abp.Dependency;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Abp.AspNetZeroCore.Timing
{
  public class AppTimes : ISingletonDependency
  {
    public DateTime StartupTime { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public AppTimes()
    {
      
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool p1cuE893RvpBGF0wI0()
    {
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool C6qydyEfDt3sanon5q()
    {
      return false;
    }
  }
}
