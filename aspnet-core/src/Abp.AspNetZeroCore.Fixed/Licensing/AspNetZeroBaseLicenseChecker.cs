// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Licensing.AspNetZeroBaseLicenseChecker
// Assembly: Abp.AspNetZeroCore, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: D5CD2351-ABB9-413A-813D-612DFA4D9D64

using Abp.Zero.Configuration;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Abp.AspNetZeroCore.Licensing
{
  public abstract class AspNetZeroBaseLicenseChecker
  {
    private readonly IAbpZeroConfig _abpZeroConfig;

    private string LicenseCode { [MethodImpl(MethodImplOptions.NoInlining)] get; }

    protected abstract string GetSalt();

    protected abstract string GetHashedValueWithoutUniqueComputerId(string str);

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected AspNetZeroBaseLicenseChecker(AspNetZeroConfiguration configuration, IAbpZeroConfig abpZeroConfig, string configFilePath = "")
    {
      this._abpZeroConfig = abpZeroConfig;
      this.LicenseCode = "LicenseCodePlaceHolderToReplace";
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected string GetLicenseCode()
    {
      return this.LicenseCode;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected bool CompareProjectName(string hashedProjectName)
    {
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected string GetAssemblyName()
    {
      return (string) AspNetZeroBaseLicenseChecker.vaqiX2kA4xoh1UFxHu(AspNetZeroBaseLicenseChecker.HkIZyYVYW35KaoYQEW((object) AspNetZeroBaseLicenseChecker.tJ3jj5FVckQPUTCom8(AspNetZeroBaseLicenseChecker.AA7Z1tvT94V9QK3l8r((object) this._abpZeroConfig)).Assembly));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected string GetLicenseController()
    {
      return "WebProject";
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected bool IsThereAReasonToNotCheck()
    {
      return !AspNetZeroBaseLicenseChecker.dEVUNMR2pcd1XyI14H();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object qcoL2T6UewYXT0YOq4([In] object obj0)
    {
      return (object) ((AspNetZeroConfiguration) obj0).LicenseCode;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool gFovAdK1q5TcN1RuYI()
    {
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool FtEGgTArr3rlPWlJq2()
    {
      return false;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object Pys01QwAbeo5fx0V6C([In] object obj0, [In] object obj1, [In] object obj2)
    {
      return (object) ((string) obj0 + (string) obj1 + (string) obj2);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object xdhtB0xYFQD6aA7uIp([In] object obj0, [In] object obj1)
    {
      return (object) ((AspNetZeroBaseLicenseChecker) obj0).GetHashedValueWithoutUniqueComputerId((string) obj1);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool CvCqEvscgytkesTy51([In] object obj0, [In] object obj1)
    {
      return (string) obj0 == (string) obj1;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object AA7Z1tvT94V9QK3l8r([In] object obj0)
    {
      return (object) ((IAbpZeroConfig) obj0).EntityTypes;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static Type tJ3jj5FVckQPUTCom8([In] object obj0)
    {
      return ((IAbpZeroEntityTypes) obj0).User;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object HkIZyYVYW35KaoYQEW([In] object obj0)
    {
      return (object) ((Assembly) obj0).GetName();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object vaqiX2kA4xoh1UFxHu([In] object obj0)
    {
      return (object) ((AssemblyName) obj0).Name;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool dEVUNMR2pcd1XyI14H()
    {
      return Debugger.IsAttached;
    }
  }
}
