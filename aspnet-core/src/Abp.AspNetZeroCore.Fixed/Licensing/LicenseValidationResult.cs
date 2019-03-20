// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Licensing.LicenseValidationResult
// Assembly: Abp.AspNetZeroCore, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: D5CD2351-ABB9-413A-813D-612DFA4D9D64

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Abp.AspNetZeroCore.Licensing
{
  public class LicenseValidationResult
  {
    public bool Success { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

    public bool LastRequest { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

    public string ControlCode { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public LicenseValidationResult()
    {
      this.Success = true;
      this.LastRequest = true;
      this.ControlCode = string.Empty;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool n1ycZh1MPbTUQAfKtag()
    {
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool tlfgUB1Na2RVnxrjNO0()
    {
      return false;
    }
  }
}
