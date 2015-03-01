using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NJSWebApp;

namespace NJSWebApp.PageXm
{
    public partial class XmForm1 : System.Web.UI.Page
    {
        private string formid = string.Empty;
        public string formhtml = string.Empty;
        private string xmcode = string.Empty;
        public string sPath = string.Empty;
        public string XMMC = string.Empty;
        public string isGAYJ = "0";
        public string code = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            formid = Request["formid"];
            xmcode = Request["xmcode"];
            code=Request["code"];
            if (xmcode != null && xmcode!="")
            {
                isGAYJ = XmData.GetIsGAYZ(xmcode);
                if (code != null && code.ToString() != "" && isGAYJ=="0")
                {
                    isGAYJ = XmData.GetIsGAYZ(code);
                }
            }
            if (string.IsNullOrEmpty(formid)) formid = "62ac84ba-13c0-49cd-99e0-1e405b0741df"; //formid = "cd0dab3f-95b1-4323-a14a-5b81d203cd2f";
   
            Boolean bReadOnly = true;
            JKCFlowPageBase.InOutParams InParams = new JKCFlowPageBase.InOutParams();
            formhtml = JKCFlowPlatform.Factory.BLLFactory.CreateFlowEngineBLL().GetFormData(new Guid(formid), xmcode, ref bReadOnly, InParams);
           
            formhtml = formhtml.Replace("<script", "<span style='display:none'");
            formhtml = formhtml.Replace("</script>", "</span>");
            formhtml = formhtml.Replace("onclick=\"", "o=\"");
            formhtml = formhtml.Replace("onblur=\"", "o=\"");
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //sPath = left1.GetPath;
            //XMMC = XM_Title1.GetXMMC;
        }
    }
}
