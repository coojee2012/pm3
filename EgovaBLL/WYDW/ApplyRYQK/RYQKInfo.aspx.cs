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

public partial class WYDW_ApplyRYQK_RYQKInfo : System.Web.UI.Page
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


    private void saveData()
    {
        Approve.Common.pageTool tool = new Approve.Common.pageTool(this.Page);

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
            sl.Add("FID", Guid.NewGuid());
            sl.Add("fEntName",CurrentEntUser.EntName);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FAppId", (string)Session["FAppId"]);
            sl.Add("XMBH", (string)Session["XMBH"]);
            sl.Add("FCreateUser", CurrentEntUser.UserId);
            //sl.Add("FSystemId", dtXMJBXX.Rows[0]["FSystemId"].ToString());
            sl.Add("FIsDeleted", 0);
            
            so = SaveOptionEnum.Insert;
        }
        else
        {
            sl.Add("FID", Request.QueryString["FID"]);
            so = SaveOptionEnum.Update;
        }

        if (rc.SaveEBase("YW_WY_RY_JBXX", sl, "FID", so))
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('保存成功！');window.returnValue = 'ok';window.close();</script>");
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
                ViewState["FID"] = dt.Rows[0]["FID"].ToString();
                t_fPersonName.Text = dt.Rows[0]["fPersonName"].ToString();
                t_fSex.SelectedValue = dt.Rows[0]["fSex"].ToString();

               // t_fCardType.SelectedValue = t_fCardType.Items.FindByText(dt.Rows[0]["fCardType"].ToString()).Value;//推荐用这种方式来通过text选中

                t_fCardType.SelectedValue = dt.Rows[0]["fCardType"].ToString();
                t_fCardID.Text = dt.Rows[0]["fCardID"].ToString();
                t_fMZ.Text = dt.Rows[0]["fMZ"].ToString();
                t_fBirthday.Text = dt.Rows[0]["fBirthday"].ToString();
                t_fSchool.Text = dt.Rows[0]["fSchool"].ToString();

                t_fbysj.Text = objToDateTimeStr(dt.Rows[0]["fCardID"], "", "yyyy-MM-dd");
                t_fMajor.Text = dt.Rows[0]["fMajor"].ToString();
                t_fbyzsh.Text = dt.Rows[0]["fbyzsh"].ToString();

                //学历

                t_fxl.SelectedValue = dt.Rows[0]["fxl"].ToString().Trim() == "-1" ? "" : dt.Rows[0]["fxl"].ToString().Trim();
               
                t_fxw.Text = dt.Rows[0]["fxw"].ToString();
                t_fxwzsh.Text = dt.Rows[0]["fxwzsh"].ToString();
                //职称
                t_fTechnical.SelectedValue = dt.Rows[0]["fTechnical"].ToString().Trim()=="-1"?"":dt.Rows[0]["fTechnical"].ToString().Trim();
                
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
                string selPPositionValue = getValuePPosition("FNumber", selPositionStr, "FNumber");

              
                ClientScript.RegisterStartupScript(GetType(), "", "<script>$('#PfPosition').val('" + selPPositionValue + "');getAndSetSelPosByPid('" + selPPositionValue + "','value','" + selPositionStr + "');</script>");

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

        //证件类型
        t_fCardType.DataValueField = "FNumber";
        t_fCardType.DataTextField = "FName";
        t_fCardType.DataSource = dtCardType;
        t_fCardType.DataBind();
        t_fCardType.Items.Insert(0, liDefault);

        //学历
        string sqlXL = "select * from CF_Sys_Dic where FParentId=107";
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

   

    #region [身份证18转化成15位]
    public string getFifteenCardID(string cardID) 
    {
        string result = ";";
        if (cardID.Length == 18)
        {
            string preStr = cardID.Substring(0,6);
            string lastStr = cardID.Substring(8,9);
            result=preStr+lastStr;
        }
        return result;
    }
    #endregion
}