// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.OpenIdConnect.OpenIdConnectAuthProviderApi
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.External.OpenIdConnect
{
  public class OpenIdConnectAuthProviderApi : ExternalAuthProviderApiBase
  {
    public const string Name = "OpenIdConnect";

    public override async Task<ExternalAuthUserInfo> GetUserInfo(string token)
    {
      OpenIdConnectAuthProviderApi connectAuthProviderApi = this;
      // ISSUE: explicit non-virtual call
      string additionalParam = (connectAuthProviderApi.ProviderInfo).AdditionalParams["Authority"];
      if (string.IsNullOrEmpty(additionalParam))
        throw new ApplicationException("Authentication:OpenId:Issuer configuration is required.");
      ConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(additionalParam + "/.well-known/openid-configuration", (IConfigurationRetriever<OpenIdConnectConfiguration>) new OpenIdConnectConfigurationRetriever(), (IDocumentRetriever) new HttpDocumentRetriever());
      JwtSecurityToken jwtSecurityToken = await connectAuthProviderApi.ValidateToken(token, additionalParam, (IConfigurationManager<OpenIdConnectConfiguration>) configurationManager, new CancellationToken());
      string str1 = jwtSecurityToken.Claims.First<Claim>((Func<Claim, bool>) (c => c.Type == "name")).Value;
      string str2 = jwtSecurityToken.Claims.First<Claim>((Func<Claim, bool>) (c => c.Type == "unique_name")).Value;
      int num1 = 32;
      string[] strArray = str1.Split((char) num1);
      return new ExternalAuthUserInfo()
      {
        Provider = "OpenIdConnect",
        ProviderKey = jwtSecurityToken.Subject,
        Name = strArray[0],
        Surname = strArray[1],
        EmailAddress = str2
      };
    }

    private async Task<JwtSecurityToken> ValidateToken(string token, string issuer, IConfigurationManager<OpenIdConnectConfiguration> configurationManager, CancellationToken ct = default (CancellationToken))
    {
      OpenIdConnectAuthProviderApi connectAuthProviderApi = this;
      if (string.IsNullOrEmpty(token))
        throw new ArgumentNullException(nameof (token));
      if (string.IsNullOrEmpty(issuer))
        throw new ArgumentNullException(nameof (issuer));
      ICollection<SecurityKey> signingKeys = (await configurationManager.GetConfigurationAsync(ct)).SigningKeys;
      SecurityToken validatedToken;
      ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters()
      {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKeys = (IEnumerable<SecurityKey>) signingKeys,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5.0),
        ValidateAudience = false
      }, out validatedToken);
      // ISSUE: explicit non-virtual call
      if ((connectAuthProviderApi.ProviderInfo).ClientId != claimsPrincipal.Claims.First<Claim>((Func<Claim, bool>) (c => c.Type == "aud")).Value)
        throw new ApplicationException("ClientId couldn't verified.");
      return (JwtSecurityToken) validatedToken;
    }
  }
}
