using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using System.Data;
using Approve.RuleCenter;
using System.Data.SqlClient;
using System.Collections;
using Approve.Common;
using Seaskyer.Strings;
using Seaskyer.WebApp.Utility;

public partial class Government_maintable_setMyTable : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDelImg.Attributes.Add("onclick", "return confirm('确定要清除现有的桌面背景');");
            showInfo();
        }
    }

    #region 公共

    /// <summary>
    /// 显示
    /// </summary>
    private void showInfo()
    {
        showtb();
        list_table1.Items.Clear();
        list_mylefttable.Items.Clear();
        string FLinkId = EConvert.ToString(Session["DFUserRightId"]);
        string sysId = Request.QueryString["sysId"];
        if (!string.IsNullOrEmpty(FLinkId))
        {
            SortedList sl = new SortedList();
            sl.Add("FLinkId", FLinkId);
            sl.Add("FType", sysId);
            DataTable dt = rc.GetTable("select FID,FMyTable,FPic from CF_User where FLinkId=@FLinkId and FType=@FType", rc.ConvertParameters(sl));
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] mytable = dt.Rows[0]["FMyTable"].ToString().Split(':');
                if (mytable.Length > 0)
                {
                    #region 默认模块表
                    Hashtable ht = new Hashtable();
                    ht.Add("1", "业务办理情况综合统计");
                    ht.Add("2", "企业统计");
          
                    #endregion

                    //左则
                    string lefttable = mytable[0];
                    foreach (string str in lefttable.Split(','))
                    {
                        string controlName = EConvert.ToString(ht[str]);
                        if (!string.IsNullOrEmpty(controlName))
                            list_mylefttable.Items.Add(new ListItem(controlName, str));
                    }


                    //可选模块项
                    for (int i = 1; i <= ht.Count; i++)
                    {
                        if (lefttable.IndexOf(i.ToString()) < 0)
                        {
                            string controlName = EConvert.ToString(ht[i.ToString()]);
                            if (!string.IsNullOrEmpty(controlName))
                            {
                                list_table1.Items.Add(new ListItem(EConvert.ToString(ht[i.ToString()]), i.ToString()));

                            }
                        }
                    }
                    //自定义背景图片

                }
            }
        }

    }



    //选项卡切换
    private void showtb()
    {
        this.ClientScript.RegisterStartupScript(GetType(), "jj", "<script>showtb(" + hidd_n.Value + ");</script>");
    }

    //显示 (隐藏按钮)
    protected void btn_show_Click(object sender, EventArgs e)
    {
        showInfo();
    }

    #endregion


    #region 各自
    //保存 左、右并用
    private void saveInfo()
    {
        showtb();
        pageTool tool = new pageTool(this.Page);
        string FLinkId = EConvert.ToString(Session["DFUserRightId"]);
        string sysId = Request.QueryString["sysId"];
        SortedList ss = new SortedList();
        ss.Add("FLinkId", FLinkId);
        ss.Add("FType", sysId);
        string FID = rc.GetSignValue("select FID from cf_user where FLinkId=@FLinkId and FType=@FType", rc.ConvertParameters(ss));
        string mytable = "";
        foreach (ListItem item in list_mylefttable.Items)
        {
            mytable += item.Value + ",";
        }
        mytable += ":";
        SortedList sl = new SortedList();
        SaveOptionEnum so = SaveOptionEnum.Update;
        sl.Add("FID", FID);
        sl.Add("FMyTable", mytable);//保存位置
        if (rc.SaveEBase("CF_User", sl, "FID", so))
        {
            tool.showMessageAndRunFunction("保存成功", "parent.document.getElementById('btnQuery').click();parent.IsR=true;");
        }
        else
        {
            tool.showMessage("保存失败");
        }

    }

    /// <summary>
    /// 上传背景图片
    /// </summary>
    private void UpInfo()
    {
        pageTool tool = new pageTool(this.Page);
        string FLinkId = EConvert.ToString(Session["DFUserRightId"]);
        string sysId = Request.QueryString["sysId"];
        SortedList ss = new SortedList();
        ss.Add("FLinkId", FLinkId);
        ss.Add("FType", sysId);
        string FID = rc.GetSignValue("select FID from cf_user where FLinkId=@FLinkId and FType=@FType", rc.ConvertParameters(ss));
        if (string.IsNullOrEmpty(FID))
        {
            return;
        }
        //	单文件上传
        HttpPostedFile upFile = Request.Files[0];
        string fFileName = upFile.FileName;
        fFileName = fFileName.ToUpper();
        if ((!fFileName.EndsWith("JPG")) && (!fFileName.EndsWith("GIF")))
        {
            tool.showMessage("请上传jpg或gif格式的图片");
            return;
        }
        string fileType = "gif,jpg";
        int fileSize = 100 * 1024;


        string timePath = DateTime.Now.ToString("yyyyMMddHHmmss");
        string uploadSavePath = Function.GetRealPath("~/upload/" + FID + "/");
        string saveName = "";
        int itemp = fFileName.LastIndexOf("\\");
        if (itemp >= 0)
        {
            saveName = timePath + fFileName.Substring(itemp + 1);
        }
        else
        {
            saveName = timePath + fFileName;
        }
        saveName = saveName.Replace("-", "");
        string fReturnPath = "../../upload/" + FID + "/";
        string[] uploadInfo = WebUtility.UploadFile(upFile, fileType, fileSize, false, saveName, uploadSavePath);
        if (uploadInfo[4] == "成功")
        {
            SortedList sl = new SortedList();
            SaveOptionEnum so = SaveOptionEnum.Update;
            so = SaveOptionEnum.Update;
            sl.Add("FID", FID);

            sl.Add("FPic", fReturnPath + uploadInfo[1]);
            if (rc.SaveEBase("CF_User", sl, "FID", so))
            {
                //img_EmpPic.Src = fReturnPath + uploadInfo[1];
                tool.showMessageAndRunFunction("保存成功", "parent.document.getElementById('btnQuery').click();");
            }
            else
                tool.showMessage("保存失败");
        }
        else
        {
            tool.showMessage(uploadInfo[4]);
        }
    }
    //清除背景图片
    private void delImg()
    {
        pageTool tool = new pageTool(this.Page);
        string FLinkId = EConvert.ToString(Session["DFUserRightId"]);
        string sysId = Request.QueryString["sysId"];
        SortedList ss = new SortedList();
        ss.Add("FLinkId", FLinkId);
        ss.Add("FType", sysId);
        string FID = rc.GetSignValue("select FID from cf_user where FLinkId=@FLinkId and FType=@FType", rc.ConvertParameters(ss));
        SortedList sl = new SortedList();
        SaveOptionEnum so = SaveOptionEnum.Update;
        so = SaveOptionEnum.Update;
        sl.Add("FID", FID);

        sl.Add("FPic", "");
        if (rc.SaveEBase("CF_User", sl, "FID", so))
        {
            //img_EmpPic.Src = "";
            tool.showMessageAndRunFunction("清除成功", "parent.document.getElementById('btnQuery').click();");
        }
        else
            tool.showMessage("清除失败");
    }
    #endregion

    #region 桌面左则 按钮
    //删掉
    protected void btn1_del_Click(object sender, EventArgs e)
    {
        showtb();
        ListItemCollection list = new ListItemCollection();
        foreach (ListItem item in list_mylefttable.Items)
        {
            list.Add(item);
        }
        for (int i = 0; i < list.Count; i++)
        {
            int m = list_mylefttable.Items.IndexOf(list[i]);
            if (list_mylefttable.Items[m].Selected)
            {
                list_table1.Items.Add(list[i]);
                list_mylefttable.Items.Remove(list[i]);
            }
        }
    }
    //添加
    protected void btn1_add_Click(object sender, EventArgs e)
    {
        showtb();
        ListItemCollection list = new ListItemCollection();
        foreach (ListItem item in list_table1.Items)
        {
            list.Add(item);
        }
        for (int i = 0; i < list.Count; i++)
        {
            int m = list_table1.Items.IndexOf(list[i]);
            if (list_table1.Items[m].Selected)
            {
                list_mylefttable.Items.Add(list[i]);
                list_table1.Items.Remove(list[i]);
            }
        }

    }
    //向上移动
    protected void btn1_up_Click(object sender, EventArgs e)
    {
        showtb();
        ListItemCollection list = new ListItemCollection();
        foreach (ListItem item in list_mylefttable.Items)
        {
            list.Add(item);
        }
        for (int i = 0; i < list.Count; i++)
        {
            int m = list_mylefttable.Items.IndexOf(list[i]);
            if (list_mylefttable.Items[m].Selected)
            {
                int n = list_mylefttable.Items.IndexOf(list[i]);
                n = n - 1 > 0 ? n - 1 : 0;
                list_mylefttable.Items.Remove(list[i]);
                list_mylefttable.Items.Insert(n, (list[i]));

            }
        }
    }
    //向下移动
    protected void btn1_down_Click(object sender, EventArgs e)
    {
        showtb();
        ListItemCollection list = new ListItemCollection();
        foreach (ListItem item in list_mylefttable.Items)
        {
            list.Add(item);
        }
        for (int i = 0; i < list.Count; i++)
        {
            int m = list_mylefttable.Items.IndexOf(list[i]);
            if (list_mylefttable.Items[m].Selected)
            {
                int n = list_mylefttable.Items.IndexOf(list[i]);
                n = (n + 1) == list_mylefttable.Items.Count ? n : n + 1;
                list_mylefttable.Items.Remove(list[i]);
                list_mylefttable.Items.Insert(n, (list[i]));

            }
        }
    }

    //保存
    protected void btn1_save_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    //还原    
    protected void btn1_reset_Click(object sender, EventArgs e)
    {
        showInfo();
    }



    #endregion



    #region 背景图片 按钮
    //上传图片
    protected void btnGetImg_Click(object sender, EventArgs e)
    {
        showtb();
        UpInfo();
    }
    //清除已有背景图片
    protected void btnDelImg_Click(object sender, EventArgs e)
    {
        showtb();
        delImg();
    }
    #endregion

}
