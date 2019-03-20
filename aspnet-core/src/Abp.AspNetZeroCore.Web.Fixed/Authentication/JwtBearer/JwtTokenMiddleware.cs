// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.JwtBearer.JwtTokenMiddleware
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: 292096E6-00F3-4F30-9258-E71EE6CB59D5

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.JwtBearer
{
  public static class JwtTokenMiddleware
  {
    public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app, string schema = "Bearer")
    {
      return app.Use((Func<HttpContext, Func<Task>, Task>) (async (ctx, next) =>
      {
        IIdentity identity = ctx.User.Identity;
        if ((identity != null ? (!identity.IsAuthenticated ? 1 : 0) : 1) != 0)
        {
          AuthenticateResult authenticateResult = await ctx.AuthenticateAsync(schema);
          if (authenticateResult.Succeeded && authenticateResult.Principal != null)
            ctx.User = authenticateResult.Principal;
        }
        await next();
      }));
    }
  }
}
