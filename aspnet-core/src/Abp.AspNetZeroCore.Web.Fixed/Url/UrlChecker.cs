// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Url.UrlChecker
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using System.Text.RegularExpressions;

namespace Abp.AspNetZeroCore.Web.Url
{
  public static class UrlChecker
  {
    private static readonly Regex UrlWithProtocolRegex = new Regex("^.{1,10}://.*$");

    public static bool IsRooted(string url)
    {
      return url.StartsWith("/") || UrlChecker.UrlWithProtocolRegex.IsMatch(url);
    }
  }
}
