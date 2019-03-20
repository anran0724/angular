// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.Microsoft.MicrosoftAccountHelper
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using Newtonsoft.Json.Linq;
using System;

namespace Abp.AspNetZeroCore.Web.Authentication.External.Microsoft
{
  public static class MicrosoftAccountHelper
  {
    public static string GetId(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "id");
    }

    public static string GetDisplayName(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "displayName");
    }

    public static string GetGivenName(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "givenName");
    }

    public static string GetSurname(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "surname");
    }

    public static string GetEmail(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "mail") ?? (string) ((JToken) user).Value<string>((object) "userPrincipalName");
    }
  }
}
