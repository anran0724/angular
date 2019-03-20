// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.Facebook.FacebookAuthProviderApi
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;

namespace Abp.AspNetZeroCore.Web.Authentication.External.Facebook
{
  public class FacebookAuthProviderApi : ExternalAuthProviderApiBase
  {
    public const string Name = "Facebook";

    public override async Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
    {
      string requestUri = QueryHelpers.AddQueryString(QueryHelpers.AddQueryString(QueryHelpers.AddQueryString("https://graph.facebook.com/v2.8/me", "access_token", accessCode), "appsecret_proof", this.GenerateAppSecretProof(accessCode)), "fields", "email,last_name,first_name,middle_name");
      ExternalAuthUserInfo externalAuthUserInfo;
      using (HttpClient client = new HttpClient())
      {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft ASP.NET Core OAuth middleware");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        client.DefaultRequestHeaders.Host = "graph.facebook.com";
        client.Timeout = TimeSpan.FromSeconds(30.0);
        client.MaxResponseContentBufferSize = 10485760L;
        HttpResponseMessage async = await client.GetAsync(requestUri);
        async.EnsureSuccessStatusCode();
        JObject user = JObject.Parse(await async.Content.ReadAsStringAsync());
        string firstName = FacebookHelper.GetFirstName(user);
        string middleName = FacebookHelper.GetMiddleName(user);
        if (!middleName.IsNullOrEmpty())
          firstName += middleName;
        externalAuthUserInfo = new ExternalAuthUserInfo()
        {
          Name = firstName,
          EmailAddress = FacebookHelper.GetEmail(user),
          Surname = FacebookHelper.GetLastName(user),
          Provider = "Facebook",
          ProviderKey = FacebookHelper.GetId(user)
        };
      }
      return externalAuthUserInfo;
    }

    private string GenerateAppSecretProof(string accessToken)
    {
      using (HMACSHA256 hmacshA256 = new HMACSHA256(Encoding.ASCII.GetBytes(this.ProviderInfo.ClientSecret)))
      {
        byte[] hash = hmacshA256.ComputeHash(Encoding.ASCII.GetBytes(accessToken));
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
          stringBuilder.Append(hash[index].ToString("x2", (IFormatProvider) CultureInfo.InvariantCulture));
        return stringBuilder.ToString();
      }
    }
  }
}
