// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Validation.ValidationHelper
// Assembly: Abp.AspNetZeroCore, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: D5CD2351-ABB9-413A-813D-612DFA4D9D64

using Abp.Extensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Abp.AspNetZeroCore.Validation
{
  public static class ValidationHelper
  {
    public const string EmailRegex = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool IsEmail(string value)
    {
      if (ValidationHelper.Eejd3qNhT2dI2i09cB((object) value))
        return false;
      return ValidationHelper.n7gSta5INFqGCmjwXj((object) new Regex("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$"), (object) value);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool Eejd3qNhT2dI2i09cB([In] object obj0)
    {
      return ((string) obj0).IsNullOrEmpty();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool n7gSta5INFqGCmjwXj([In] object obj0, [In] object obj1)
    {
      return ((Regex) obj0).IsMatch((string) obj1);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool XmZsuhdK39t02eaPVv()
    {
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool x96HDbMeDVl4ilcY5Y()
    {
      return false;
    }
  }
}
