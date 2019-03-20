// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Licensing.AspNetZeroLicenseChecker
// Assembly: Abp.AspNetZeroCore, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: D5CD2351-ABB9-413A-813D-612DFA4D9D64

using Abp.Dependency;
using Abp.Threading;
using Abp.Web.Models;
using Abp.Zero.Configuration;
using Castle.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Licensing
{
  public sealed class AspNetZeroLicenseChecker : AspNetZeroBaseLicenseChecker, ISingletonDependency
  {
    private string _licenseCheckFilePath;
    private string _uniqueComputerId;

    public ILogger Logger { [MethodImpl(MethodImplOptions.NoInlining)] get; [MethodImpl(MethodImplOptions.NoInlining)] set; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public AspNetZeroLicenseChecker(AspNetZeroConfiguration configuration = null, IAbpZeroConfig abpZeroConfig = null, string configFilePath = "")
      : base(configuration, abpZeroConfig, configFilePath)
    {
      this.Logger = (ILogger) NullLogger.Instance;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Check()
    {
      if (this.IsThereAReasonToNotCheck())
        return;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void CheckSync()
    {
      
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private bool CheckedBefore()
    {
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private bool IsProjectNameValid()
    {
      return this.CompareProjectName(this.GetHashedProjectName());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private string GetHashedProjectName()
    {
      return (string) AspNetZeroLicenseChecker.iKo1Uonp7ETVIZw1Xo((object) this.GetLicenseCode(), AspNetZeroLicenseChecker.BrOSf0UAtm08ckUh8J((object) this.GetLicenseCode()) - 32, 32);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private string GetLicenseCodeWithoutProjectNameHash()
    {
      return (string) AspNetZeroLicenseChecker.iKo1Uonp7ETVIZw1Xo((object) this.GetLicenseCode(), 0, AspNetZeroLicenseChecker.BrOSf0UAtm08ckUh8J((object) this.GetLicenseCode()) - 32);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void UpdateLastLicenseCheckDate()
    {
      AspNetZeroLicenseChecker.B2vNE0SbABeLf9FiBR((object) this._licenseCheckFilePath, (object) this.GetHashedValue((string) AspNetZeroLicenseChecker.McqJFAyRsWpY6nErbw()));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void MarkAsLastRequest()
    {
      AspNetZeroLicenseChecker.B2vNE0SbABeLf9FiBR((object) this._licenseCheckFilePath, (object) this.GetHashedValue((string) AspNetZeroLicenseChecker.HWT60viIax5Xv2MGZ9()));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private string GetLastLicenseCheckDate()
    {
      return (string) AspNetZeroLicenseChecker.BiqLTIDpEiHEmWXf1M((object) this._licenseCheckFilePath);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static string GetComputerName()
    {
      return (string) AspNetZeroLicenseChecker.tiSlpBJvmdY8sQw6EI();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static string GetTodayAsString()
    {
      return AspNetZeroLicenseChecker.r9tb9JewTKaoIFEEQw().ToString("yyyy-MM-dd");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private string GetHashedValue(string str)
    {
      MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
      try
      {
        return (string) AspNetZeroLicenseChecker.FFcF3u1I8murr5nfSkF(AspNetZeroLicenseChecker.eueBhH11OQDEhGwoVRY((object) cryptoServiceProvider, AspNetZeroLicenseChecker.I9mMrQ1ZppOvKCE0U3r(AspNetZeroLicenseChecker.rP749tWw3VTXuwVGdx(), AspNetZeroLicenseChecker.XbpgJBzO0GaWmwsmp7((object) str, (object) this._uniqueComputerId, AspNetZeroLicenseChecker.DMbFjjljBAxrx1vH1C((object) this)))));
      }
      finally
      {
        if (cryptoServiceProvider != null)
          AspNetZeroLicenseChecker.QVk9qC1fkD7Th7W8vSt((object) cryptoServiceProvider);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected override string GetSalt()
    {
      return "abc";
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static string GetLicenseExpiredString()
    {
      return "9999-12-1";
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static string StringGeneratorFromInteger(object letters)
    {
      return "abc";
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    protected override string GetHashedValueWithoutUniqueComputerId(string str)
    {
      MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
      try
      {
        return (string) AspNetZeroLicenseChecker.FFcF3u1I8murr5nfSkF(AspNetZeroLicenseChecker.eueBhH11OQDEhGwoVRY((object) cryptoServiceProvider, AspNetZeroLicenseChecker.I9mMrQ1ZppOvKCE0U3r(AspNetZeroLicenseChecker.rP749tWw3VTXuwVGdx(), AspNetZeroLicenseChecker.JbmnOL1X66TJqUv4KYs((object) str, AspNetZeroLicenseChecker.DMbFjjljBAxrx1vH1C((object) this)))));
      }
      finally
      {
        if (cryptoServiceProvider != null)
          AspNetZeroLicenseChecker.QVk9qC1fkD7Th7W8vSt((object) cryptoServiceProvider);
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static string EncodeBase64(object ba)
    {
      return "abc";
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool wEvMeZGqosn6qhQnlD()
    {
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool S4kXakg64Cg0wrP0nw()
    {
      return false;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object can4xerVklW0h403EM([In] object obj0)
    {
      return (object) ((Exception) obj0).Message;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void RIavv3cE8XoI24uWkp([In] object obj0)
    {
      Console.WriteLine((string) obj0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void gUi6k3qO8eFkFjSNCU([In] int obj0)
    {
      Environment.Exit(obj0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static AsyncTaskMethodBuilder vMF24UL5q8IBHDU8Mm()
    {
      return AsyncTaskMethodBuilder.Create();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool d9RbXFTV12kEL1rHqL([In] object obj0)
    {
      return File.Exists((string) obj0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object McqJFAyRsWpY6nErbw()
    {
      return (object) AspNetZeroLicenseChecker.GetTodayAsString();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static bool EtoCaF0wb2rZpL3c1i([In] object obj0, [In] object obj1)
    {
      return (string) obj0 != (string) obj1;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object HWT60viIax5Xv2MGZ9()
    {
      return (object) AspNetZeroLicenseChecker.GetLicenseExpiredString();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void Qs3FB38q8A7wwP4Lpa([In] object obj0)
    {
      File.Delete((string) obj0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static int BrOSf0UAtm08ckUh8J([In] object obj0)
    {
      return ((string) obj0).Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object iKo1Uonp7ETVIZw1Xo([In] object obj0, [In] int obj1, [In] int obj2)
    {
      return (object) ((string) obj0).Substring(obj1, obj2);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void B2vNE0SbABeLf9FiBR([In] object obj0, [In] object obj1)
    {
      File.WriteAllText((string) obj0, (string) obj1);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object BiqLTIDpEiHEmWXf1M([In] object obj0)
    {
      return (object) File.ReadAllText((string) obj0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object oX9Regbho1LTNAOyjC()
    {
      return (object) NetworkInterface.GetAllNetworkInterfaces();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object tiSlpBJvmdY8sQw6EI()
    {
      return (object) Environment.MachineName;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static DateTime r9tb9JewTKaoIFEEQw()
    {
      return DateTime.Now;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object rP749tWw3VTXuwVGdx()
    {
      return (object) Encoding.UTF8;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object DMbFjjljBAxrx1vH1C([In] object obj0)
    {
      return "abc";
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object XbpgJBzO0GaWmwsmp7([In] object obj0, [In] object obj1, [In] object obj2)
    {
      return (object) ((string) obj0 + (string) obj1 + (string) obj2);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object I9mMrQ1ZppOvKCE0U3r([In] object obj0, [In] object obj1)
    {
      return (object) ((Encoding) obj0).GetBytes((string) obj1);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object eueBhH11OQDEhGwoVRY([In] object obj0, [In] object obj1)
    {
      return (object) ((HashAlgorithm) obj0).ComputeHash((byte[]) obj1);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object FFcF3u1I8murr5nfSkF([In] object obj0)
    {
      return (object) AspNetZeroLicenseChecker.EncodeBase64(obj0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void QVk9qC1fkD7Th7W8vSt([In] object obj0)
    {
      ((IDisposable) obj0).Dispose();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void BN5tUN1tSZprE3aWV5s([In] object obj0, [In] RuntimeFieldHandle obj1)
    {
      RuntimeHelpers.InitializeArray((Array) obj0, obj1);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object nqqmgQ1m6SEEqJKcTVt([In] object obj0)
    {
      return (object) AspNetZeroLicenseChecker.StringGeneratorFromInteger(obj0);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object JbmnOL1X66TJqUv4KYs([In] object obj0, [In] object obj1)
    {
      return (object) ((string) obj0 + (string) obj1);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static object mlKc5d1YtfE0MeZsJWP([In] object obj0, [In] object obj1, [In] object obj2)
    {
      return (object) ((StringBuilder) obj0).AppendFormat((string) obj1, obj2);
    }
  }
}
