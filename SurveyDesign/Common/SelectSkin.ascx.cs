using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.Common;
using System.IO;
using Approve.EntityBase;
using Approve.RuleCenter;

public partial class Common_SelectSkin : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            showInfo();
    }


    private void showInfo()
    {
        string pathtext = Context.Request.PhysicalApplicationPath;
        if (!string.IsNullOrEmpty(pathtext))
        {
            //得到文件名，目录
            string[] filename = Directory.GetDirectories(pathtext + "skin\\");
            foreach (string file in filename)
            {
                string styleName = file.Split('\\')[file.Split('\\').Length - 1];
                string styleText = "深海蓝";
                if (styleName.Equals("blue", StringComparison.InvariantCultureIgnoreCase))
                    styleText = "深海蓝";
                else if (styleName.Equals("orange", StringComparison.InvariantCultureIgnoreCase))
                    styleText = "柠檬橙";
                else
                    styleText = styleName;
                dro_Style.Items.Add(new ListItem(styleText, styleName));
            }

            try //
            {
                string skinName = "";
                if (Request.Cookies["_SYS_QS_SKINNAME"] == null)
                {
                    Share rc = new Share();
                    skinName = rc.GetSysObjectContent("_sys_skinName");
                }
                else
                {
                    skinName = EConvert.ToString(Request.Cookies["_SYS_QS_SKINNAME"].Value);//得到cookies中的风格
                }
                dro_Style.SelectedValue = skinName;
            }
            catch (Exception ex) { }
        }

    }


    //public string getlUrl()
    //{
    //    string str = "";
    //    string first = "http://" + Context.Request.Headers["Host"];
    //    string root = Context.Request.ApplicationPath;
    //    if (root != "/")
    //        root += "/";
    //    str = first + root;

    //    return str + "Common/SaveSkinCookie.ashx";
    //}

    ///// <summary>
    ///// 风格切换，保存在Cookies
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void dro_Style_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    pageTool tool = new pageTool(this.Page);
    //    string skin = dro_Style.SelectedValue;
    //    if (!string.IsNullOrEmpty(skin))
    //    {
    //        Response.Cookies["_SYS_QS_SKINNAME"].Value = skin;
    //        Response.Cookies["_SYS_QS_SKINNAME"].Expires = DateTime.Now.AddYears(5);
    //        this.Page.ClientScript.RegisterStartupScript(GetType(), "jj", "<script>top.document.location.reload();</script>");
    //    }
    //    else
    //        tool.showMessage("保存风格失败！");
    //}
}
