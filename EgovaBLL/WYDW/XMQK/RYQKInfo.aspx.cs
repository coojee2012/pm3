using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Tools;
using SaveOptionEnum = Approve.EntityBase.SaveOptionEnum;

public partial class WYDW_XMQK_RYQKInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string PPositionStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            conBind();
            showInfo();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }
    //private void getFilecount()
    //{
    //    if (Session["FAppId"] != null)
    //    {
    //        string strsql = "select count(*) as FileCount from CF_AppPrj_FileOther where FAppId='" + (string)Session["FAppId"] + "'";
    //        DataTable dt = rc.GetTable(strsql);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            filecount = dt.Rows[0]["FileCount"].ToString();
    //            hidfilecount.Value = filecount;
    //        }
    //    }
    //}

    private void saveData()
    {
        pageTool tool = new pageTool(this.Page);
        object obj = Request.QueryString["Type"];
        if (obj != null)
        {
            DataTable dtXMJBXX = new DataTable();
            dtXMJBXX = (DataTable)Session["JBXX"];
            Approve.EntityBase.SaveOptionEnum so = Approve.EntityBase.SaveOptionEnum.Unknown;
            SortedList sl = new SortedList();
            sl = tool.getPageValue();
            while (sl.IndexOfValue("") != -1)
            {
                sl.RemoveAt(sl.IndexOfValue(""));
            }
            sl.Remove("FTime");

            //添加情况下
            if (obj.ToString() == "1")
            {
                sl.Add("FID", getSessionRYID());
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FAppId", (string)Session["FAppId"]);
                sl.Add("XMBH", dtXMJBXX.Rows[0]["XMBH"].ToString());
                sl.Add("FCreateUser", CurrentEntUser.UserId);
                sl.Add("FSystemId", dtXMJBXX.Rows[0]["FSystemId"].ToString());
                sl.Add("FIsDeleted", 0);
                so = Approve.EntityBase.SaveOptionEnum.Insert;
            }
            //修改情况下
            else if (obj.ToString() == "2")
            {
                sl.Add("FID", Session["RYID"].ToString());
                //sl.Add("FTime", DateTime.Now);
                so = SaveOptionEnum.Update;
            }
            //修正数据
            sl.SetByIndex(sl.IndexOfKey("FCARDTYPE"), t_fCardType.SelectedItem.Text);
            if (rc.SaveEBase("YW_WY_RY_JBXX", sl, "FID", so))
            {
                ViewState["FID"] = sl["FID"].ToString();
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存成功！');window.returnValue = 'ok';window.close();</script>");
            }
            else
            {
                tool.showMessage("保存失败！");
            }

        }
        else
        {
            this.RegisterStartupScript(new Guid().ToString(), "<script>alert('链接地址出错！');parent.location.href='../main/Index.aspx';</script>");
        }


    }

    private void showInfo()
    {
        object obj = "2";
        if (obj != null)
        {
            if (obj.ToString() == "2")
            {
                if (Session["RYID"] != null)
                {
                    string strsql = "select * from WY_RY_JBXX where FID='" + (string)Session["RYID"] + "'";
                    DataTable dt = new DataTable();
                    dt = rc.GetTable(strsql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ViewState["FID"] = dt.Rows[0]["FID"].ToString();
                        t_fPersonName.Text = dt.Rows[0]["fPersonName"].ToString();
                        t_fSex.SelectedValue = dt.Rows[0]["fSex"].ToString();

                        t_fCardType.SelectedValue = t_fCardType.Items.FindByText(dt.Rows[0]["fCardType"].ToString()).Value;//推荐用这种方式来通过text选中

                        t_fCardID.Text = dt.Rows[0]["fCardID"].ToString();
                        t_fMZ.Text = dt.Rows[0]["fMZ"].ToString();
                        t_fBirthday.Text = dt.Rows[0]["fBirthday"].ToString();
                        t_fSchool.Text = dt.Rows[0]["fSchool"].ToString();

                        t_fbysj.Text = objToDateTimeStr(dt.Rows[0]["fCardID"], "", "yyyy-MM-dd");
                        t_fMajor.Text = dt.Rows[0]["fMajor"].ToString();
                        t_fbyzsh.Text = dt.Rows[0]["fbyzsh"].ToString();
                        t_fxl.Text = dt.Rows[0]["fxl"].ToString();

                        t_fxw.Text = dt.Rows[0]["fxw"].ToString();
                        t_fxwzsh.Text = dt.Rows[0]["fxwzsh"].ToString();
                        t_fTechnical.Text = dt.Rows[0]["fTechnical"].ToString();
                        t_zcqdsj.Text = objToDateTimeStr(dt.Rows[0]["fzcqdsj"], "", "yyyy-MM-dd");

                        t_fzczsh.Text = dt.Rows[0]["fzczsh"].ToString();

                        t_fbgdh.Text = dt.Rows[0]["fbgdh"].ToString();
                        t_fgrdh.Text = dt.Rows[0]["fgrdh"].ToString();
                        t_fdzyx.Text = dt.Rows[0]["fdzyx"].ToString();

                        t_fyzbm.Text = dt.Rows[0]["fyzbm"].ToString();
                        t_fAddress.Text = dt.Rows[0]["fAddress"].ToString();
                        t_flxdz.Text = dt.Rows[0]["flxdz"].ToString();

                        t_fMemo.Text = dt.Rows[0]["fMemo"].ToString();

                        //获取电子文件数
                        string sqlGetFileCount = "select ftypeid, count(ftypeid) as filecount from wy_filelist group by ftypeid having ftypeid in('3001','3002','3003') order by ftypeid asc";
                        DataTable dtFileCount = rc.GetTable(sqlGetFileCount);
                        if (dtFileCount.Rows.Count > 0)
                        {
                            //t_fMemo.Text = dtFileCount.Rows[0]["ftypeid"].ToString();
                            foreach (DataRow dr in dtFileCount.Rows)
                            {
                                if (dr["ftypeid"].ToString().Trim().Equals("3002"))
                                {
                                    //hidBYZSCount.Value = dr["filecount"].ToString();
                                    BYZSUpload.Value = "上传文件(" + dr["filecount"].ToString() + ")";
                                }
                                else if (dr["ftypeid"].ToString().Trim() == "3003")
                                {
                                    XWZSUpload.Value = "上传文件(" + dr["filecount"].ToString() + ")";
                                }
                                else if (dr["ftypeid"].ToString().Trim() == "3004")
                                {
                                    ZCZSUpload.Value = "上传文件(" + dr["filecount"].ToString() + ")";
                                }
                            }
                        }

                        //显示照片
                        img_EmpPic.Src = dt.Rows[0]["fphoto"].ToString();
                        // img_EmpPic.Src = rc.GetSignValue("select FFilePath from WY_FileList where FID='31e957e3-d4ff-45b6-8de2-c8b6ce6502c0'");

                        string selPositionStr = dt.Rows[0]["fPosition"].ToString();
                        string selPPositionValue = getValuePPosition("FName", selPositionStr, "FNumber");
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>$('#PfPosition').val('" + selPPositionValue + "');getAndSetSelPosByPid('" + selPPositionValue + "','text','" + selPositionStr + "');</script>");



                    }
                }
                else
                {
                    this.RegisterStartupScript(new Guid().ToString(), "<script>alert('数据加载失败！');parent.location.href='../main/Index.aspx';</script>");
                }
            }
            //新增时
            else if (obj.ToString() == "1")
            {
                //将人员id的session设为空
                Session["RYID"] = null;
            }
        }

    }

    //根据职务子项获取父项（适用于字符串类型的字段）
    public string getValuePPosition(string childColName, string childColValue, string pColName)
    {
        string result = "-1";
        string sql = "select * from CF_Sys_Dic where FNumber in(select FParentId from CF_Sys_Dic where " + childColName + "='" + childColValue + "')";
        try
        {
            DataTable dt = rc.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0][pColName].ToString();
            }
        }
        catch { }
        return result;
    }

    //数据绑定
    public void conBind()
    {
        string sqlCardType = "select * from CF_Sys_Dic where FParentId=1129";
        DataTable dtCardType = rc.GetTable(sqlCardType);

        ListItem liDefault = new ListItem();
        liDefault.Value = "-1";
        liDefault.Text = "---请选择---";

        t_fCardType.DataValueField = "FNumber";
        t_fCardType.DataTextField = "FName";
        t_fCardType.DataSource = dtCardType;
        t_fCardType.DataBind();
        t_fCardType.Items.Insert(0, liDefault);

        StringBuilder sb = new StringBuilder();
        string sql = "select * from CF_Sys_Dic where FParentId=600";
        try
        {
            DataTable dt = rc.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<option value='" + dr["FNumber"].ToString() + "'>" + dr["FName"].ToString() + "</option>");
                }
            }

        }
        catch { }
        PPositionStr = sb.ToString();
    }

    #region [将object转换成时间格式的字符串]
    public string objToDateTimeStr(object obj, string defaultStr, string formartStr)
    {
        string result = defaultStr;
        if (obj != null)
        {
            try
            {
                result = DateTime.Parse(obj.ToString().Trim()).ToString(formartStr);
            }
            catch
            {

            }
        }
        return result;
    }
    #endregion



    #region [人员id的session读取]
    public string getSessionRYID()
    {
        string result = "";
        if (Session["RYID"] == null)
        {
            result = Guid.NewGuid().ToString();
            Session["RYID"] = result;
        }
        else
        {
            result = Session["RYID"].ToString();
        }
        return result;
    }
    #endregion
}