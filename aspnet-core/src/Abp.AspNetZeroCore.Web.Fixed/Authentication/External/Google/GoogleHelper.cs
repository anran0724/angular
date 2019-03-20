﻿// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.Google.GoogleHelper
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using Newtonsoft.Json.Linq;
using System;

namespace Abp.AspNetZeroCore.Web.Authentication.External.Google
{
  public static class GoogleHelper
  {
    public static string GetId(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "id");
    }

    public static string GetName(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "displayName");
    }

    public static string GetGivenName(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return GoogleHelper.TryGetValue(user, "name", "givenName");
    }

    public static string GetFamilyName(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return GoogleHelper.TryGetValue(user, "name", "familyName");
    }

    public static string GetProfile(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "url");
    }

    public static string GetEmail(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return GoogleHelper.TryGetFirstValue(user, "emails", "value");
    }

    private static string TryGetValue(JObject user, string propertyName, string subProperty)
    {
      JToken jtoken;
      if (user.TryGetValue(propertyName, out jtoken))
      {
        JObject jobject = JObject.Parse(((object) jtoken).ToString());
        if (jobject != null && jobject.TryGetValue(subProperty, out jtoken))
          return ((object) jtoken).ToString();
      }
      return (string) null;
    }

    private static string TryGetFirstValue(JObject user, string propertyName, string subProperty)
    {
      JToken jtoken;
      if (user.TryGetValue(propertyName, out jtoken))
      {
        JArray jarray = JArray.Parse(((object) jtoken).ToString());
        if (jarray != null && ((JContainer) jarray).Count > 0)
        {
          JObject jobject = JObject.Parse(((object) ((JToken) jarray).First).ToString());
          if (jobject != null && jobject.TryGetValue(subProperty, out jtoken))
            return ((object) jtoken).ToString();
        }
      }
      return (string) null;
    }
  }
}
