namespace Approve.RuleBase
{
    using System;
    using System.ComponentModel;
    using System.Web;

    internal class MyLicenseProvider : LicenseProvider
    {
        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            if ((context.UsageMode == LicenseUsageMode.Runtime) && (HttpContext.Current != null))
            {
                LicenseTools tools = new LicenseTools();
                LicenseResult result = tools.GetResult();
                if (!result.IsPass)
                {
                    HttpContext.Current.Response.Write(result.strMessage + "<font color='#ffffff'>" + tools.GetProduct() + "," + HardDiskVal.HDVal() + "</font>");
                    HttpContext.Current.Response.End();
                }
            }
            return new RuntimeRegistryLicense(type);
        }
    }
}

