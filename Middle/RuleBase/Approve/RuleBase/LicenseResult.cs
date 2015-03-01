namespace Approve.RuleBase
{
    using System;

    public class LicenseResult
    {
        public DateTime EndTime = DateTime.MaxValue;
        public bool IsPass = false;
        public string strMessage = "";
    }
}

