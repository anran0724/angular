// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.Google.GoogleAuthProviderApi
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using Microsoft.AspNetCore.Authentication.Google;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.External.Google
{
  public class GoogleAuthProviderApi : ExternalAuthProviderApiBase
  {
    public const string Name = "Google";

    public override async Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
    {
      ExternalAuthUserInfo externalAuthUserInfo;
      using (HttpClient client = new HttpClient())
      {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft ASP.NET Core OAuth middleware");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        client.Timeout = TimeSpan.FromSeconds(30.0);
        client.MaxResponseContentBufferSize = 10485760L;
        HttpResponseMessage httpResponseMessage = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, GoogleDefaults.UserInformationEndpoint)
        {
          Headers = {
            Authorization = new AuthenticationHeaderValue("Bearer", accessCode)
          }
        });
        httpResponseMessage.EnsureSuccessStatusCode();
        JObject user = JObject.Parse(await httpResponseMessage.Content.ReadAsStringAsync());
        externalAuthUserInfo = new ExternalAuthUserInfo()
        {
          Name = GoogleHelper.GetName(user),
          EmailAddress = GoogleHelper.GetEmail(user),
          Surname = GoogleHelper.GetFamilyName(user),
          ProviderKey = GoogleHelper.GetId(user),
          Provider = "Google"
        };
      }
      return externalAuthUserInfo;
    }
  }
}
