using Approve.RuleCenter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;
using SaveOptionEnum = Approve.EntityBase.SaveOptionEnum;

public partial class WYDW_ApplyRYQK_RYQKInfo : WYPage
{
    RCenter rc = new RCenter();
    public string PPositionStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSession();
        if (!IsPostBack)
        {
            hidXMBH.Value = Session["XMBH"].ToString();
            conBind();
            showInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveData();
    }


    private void saveData()
    {
        string fid = "";
        Approve.Common.pageTool tool = new Approve.Common.pageTool(this.Page);
        if (Request.QueryString["FID"] != null) fid = Request.QueryString["FID"];

        if (hasRYByXMBH(Session["XMBH"].ToString(), t_fCardType.SelectedValue, t_fCardID.Text, fid))
        {
            tool.showMessage("您填入的人员在项目中已经存在，您可进行导入或重新填写！");
            return;
        }


        //DataTable dtXMJBXX = new DataTable();
        //dtXMJBXX = (DataTable)Session["JBXX"];
        SaveOptionEnum so = SaveOptionEnum.Unknown;
        SortedList sl = new SortedList();
        sl = tool.getPageValue();
        //string cardType = t_fCardType.Items[t_fCardType.SelectedIndex].Text.Trim();
        //if (cardType == "身份证" || cardType == "身份证号")
        //{
        //    sl.Add("fFifteenCardID", getFifteenCardID(t_fCardID.Text.Trim()));
        //}
        while (sl.IndexOfValue("") != -1)
        {
            sl.RemoveAt(sl.IndexOfValue(""));
        }
        //sl.Remove("FTime");

        //添加情况下

        if (Request.QueryString["FID"] == null)
        {
            sl.Add("FID", hidFId.Value);
            sl.Add("fEntName", CurrentEntUser.EntName);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FAppId", (string)Session["FAppId"]);
            sl.Add("XMBH", (string)Session["XMBH"]);
            sl.Add("FCreateUser", CurrentEntUser.UserId);
            sl.Add("FSystemId", CurrentEntUser.SystemId);
            sl.Add("FIsDeleted", 0);
            sl.Add("FBaseInfoId",CurrentEntUser.EntId);
            so = SaveOptionEnum.Insert;
        }
        else
        {
            sl.Add("FID", Request.QueryString["FID"]);
            so = SaveOptionEnum.Update;
        }
        //由于职务控件在panel中getPageValue无法获取到，所以手动添加
        sl.Add("fPosition", t_fPosition.SelectedValue);
        if (rc.SaveEBase("YW_WY_RY_JBXX", sl, "FID", so))
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存成功！');if(window.opener){window.opener.returnValue='ok';}else{window.returnValue = 'ok';}window.close();</script>");
        }
        else
        {
            tool.showMessage("保存失败！");
        }
    }


    private void showInfo()
    {
        if (Request.QueryString["FID"] != null)
        {
            string strsql = "select * from YW_WY_RY_JBXX where FID='" + Request.QueryString["FID"] + "'";
            DataTable dt = new DataTable();
            dt = rc.GetTable(strsql);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["FID"] = hidFId.Value = dt.Rows[0]["FID"].ToString();
                t_fPersonName.Text = dt.Rows[0]["fPersonName"].ToString();
                t_fSex.SelectedValue = dt.Rows[0]["fSex"].ToString();

                // t_fCardType.SelectedValue = t_fCardType.Items.FindByText(dt.Rows[0]["fCardType"].ToString()).Value;//推荐用这种方式来通过text选中

                t_fCardType.SelectedValue = dt.Rows[0]["fCardType"].ToString();
                t_fCardID.Text = dt.Rows[0]["fCardID"].ToString();
                t_fMZ.Text = dt.Rows[0]["fMZ"].ToString();
                t_fBirthday.Text = objToDateTimeStr(dt.Rows[0]["fBirthday"].ToString(),"","yyyy-MM-dd");
                t_fSchool.Text = dt.Rows[0]["fSchool"].ToString();

                t_fbysj.Text = objToDateTimeStr(dt.Rows[0]["fbysj"], "", "yyyy-MM-dd");
                t_fMajor.Text = dt.Rows[0]["fMajor"].ToString();
                t_fbyzsh.Text = dt.Rows[0]["fbyzsh"].ToString();

                //学历

                t_fxl.SelectedValue = dt.Rows[0]["fxl"].ToString().Trim() == "-1" ? "" : dt.Rows[0]["fxl"].ToString().Trim();

                t_fxw.Text = dt.Rows[0]["fxw"].ToString();
                t_fxwzsh.Text = dt.Rows[0]["fxwzsh"].ToString();
                //职称

                t_fTechnical.SelectedValue = dt.Rows[0]["fTechnical"].ToString().Trim() == "-1" ? "" : dt.Rows[0]["fTechnical"].ToString().Trim();

                // t_fTechnical.SelectedValue = t_fTechnical.Items.FindByText(dt.Rows[0]["fTechnical"].ToString()).Value;
                t_fzcqdsj.Text = objToDateTimeStr(dt.Rows[0]["fzcqdsj"], "", "yyyy-MM-dd");

                t_fzczsh.Text = dt.Rows[0]["fzczsh"].ToString();

                t_fbgdh.Text = dt.Rows[0]["fbgdh"].ToString();
                t_fgrdh.Text = dt.Rows[0]["fgrdh"].ToString();
                t_fdzyx.Text = dt.Rows[0]["fdzyx"].ToString();

                t_fyzbm.Text = dt.Rows[0]["fyzbm"].ToString();
                t_fAddress.Text = dt.Rows[0]["fAddress"].ToString();
                t_flxdz.Text = dt.Rows[0]["flxdz"].ToString();

                t_fMemo.Text = dt.Rows[0]["fMemo"].ToString();

                //获取电子文件数
                string sqlGetFileCount = "select ftypeid, count(ftypeid) as filecount from wy_filelist where flinkid='" + Request.QueryString["FID"].ToString() + "' group by ftypeid order by ftypeid asc";
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

                //父职务
                string selPositionStr = dt.Rows[0]["fPosition"].ToString();
                string selPPositionValue = getValuePPosition("FNumber", selPositionStr, "FNumber");
                PfPosition.SelectedValue = selPPositionValue;
                getPositionByPP();
                t_fPosition.SelectedValue = selPositionStr;
                //职务html控件
                //ClientScript.RegisterStartupScript(GetType(), "", "<script>$('#PfPosition').val('" + selPPositionValue + "');getAndSetSelPosByPid('" + selPPositionValue + "','value','" + selPositionStr + "');</script>");

            }
        }
        //添加情况
        else
        {
            hidFId.Value = Guid.NewGuid().ToString();
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

        //证件类型
        t_fCardType.DataValueField = "FNumber";
        t_fCardType.DataTextField = "FName";
        t_fCardType.DataSource = dtCardType;
        t_fCardType.DataBind();
        t_fCardType.Items.Insert(0, liDefault);

        //学历
        string sqlXL = "select * from CF_Sys_Dic where FParentId=107 order by forder desc";
        DataTable dtXL = rc.GetTable(sqlXL);
        t_fxl.DataValueField = "FNumber";
        t_fxl.DataTextField = "FName";
        t_fxl.DataSource = dtXL;
        t_fxl.DataBind();
        t_fxl.Items.Insert(0, liDefault);

        //职称
        string sqlTechnical = "select * from CF_Sys_Dic where FParentId=108";
        DataTable dtTechnical = rc.GetTable(sqlTechnical);
        t_fTechnical.DataValueField = "FNumber";
        t_fTechnical.DataTextField = "FName";
        t_fTechnical.DataSource = dtTechnical;
        t_fTechnical.DataBind();
        t_fTechnical.Items.Insert(0, liDefault);

        //StringBuilder sb = new StringBuilder();
        //string sql = "select * from CF_Sys_Dic where FParentId=600";
        //try
        //{
        //    DataTable dt = rc.GetTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            sb.Append("<option value='" + dr["FNumber"].ToString() + "'>" + dr["FName"].ToString() + "</option>");
        //        }
        //    }

        //}
        //catch { }
        //PPositionStr = sb.ToString();
        //父职务

        string sqlPPosition = "select * from CF_Sys_Dic where FParentId=600";
        DataTable dtPPosition = rc.GetTable(sqlPPosition);
        PfPosition.DataValueField = "FNumber";
        PfPosition.DataTextField = "FName";
        PfPosition.DataSource = dtPPosition;
        PfPosition.DataBind();
        PfPosition.Items.Insert(0, liDefault);

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



    #region [身份证18转化成15位]
    public string getFifteenCardID(string cardID)
    {
        string result = ";";
        if (cardID.Length == 18)
        {
            string preStr = cardID.Substring(0, 6);
            string lastStr = cardID.Substring(8, 9);
            result = preStr + lastStr;
        }
        return result;
    }
    #endregion
    private void readOnly()
    {
        btnSave.Visible = false;
        PhotoUpload.Visible = false;
        hidReadOnly.Value = "1";
    }
    protected void PfPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPositionByPP();
        //upZW.Update();
    }

    #region [判断是否在同一个项目中该人员是否存在]
    public bool hasRYByXMBH(string xmbh, string cardType, string cardId, string fid)
    {
        if (cardType != null && cardId != null && xmbh != null && fid != null)
        {
            string sql = "select * from yw_wy_ry_jbxx where xmbh='" + xmbh.ToString() + "' and fcardtype='" + cardType + "' and fcardid='" + cardId + "' and fid<>'" + fid + "'";
            try
            {
                int count = rc.GetCount(sql);
                if (count > 0)
                {
                    return true;
                }
            }
            catch
            {

            }
        }

        return false;
    }
    #endregion

    #region[根据父职务获取子职务]
    protected void getPositionByPP()
    {
        ListItem liDefault = new ListItem();
        liDefault.Value = "-1";
        liDefault.Text = "---请选择---";
        //职务
        string sqlPosition = "select * from CF_Sys_Dic where FParentId=" + PfPosition.SelectedValue;
        DataTable dtPosition = rc.GetTable(sqlPosition);
        t_fPosition.DataValueField = "FNumber";
        t_fPosition.DataTextField = "FName";
        t_fPosition.DataSource = dtPosition;
        t_fPosition.DataBind();
        t_fPosition.Items.Insert(0, liDefault);
    }
    #endregion
}