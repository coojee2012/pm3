using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Seaskyer.Strings;
using Seaskyer.WebApp.Utility;
using System.Configuration;

public partial class NewsAdd : Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, System.EventArgs e)
    {

        pageTool tool = new pageTool(this.Page);
        if (!base.IsPostBack)
        {
            this.t_FValidEnd.Text = DateTime.Now.AddYears(10).ToString("yyyy-MM-dd HH:mm:ss");
            if (Request["fcol"] != null && Request["fcol"] != "")
            {
                this.txtFClassID.Text = Request["fcol"];
                if (Request["fcol"] == "200005002")
                {
                    this.t_FValidEnd.Text = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (Request["fcol"] == "200005003")
                {
                    this.t_FValidEnd.Text = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (Request["fcol"] == "200005004")
                {
                    this.t_FValidEnd.Text = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (Request["fcol"] == "200005005")
                {
                    this.t_FValidEnd.Text = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (Request["fcol"] == "200005006")
                {
                    this.t_FValidEnd.Text = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd HH:mm:ss");
                }

            }

            this.ControlBind();
            this.btnSave.Attributes.Add("onclick", "if(!validate(this)){return false}else{return true}");
            if (this.Request["fid"] == null)
            {

                this.t_FPubTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                this.t_FOrder.Text = "50000";
            }
            else
            {
                string fid = Request["fid"].ToString();
                this.ViewState["FID"] = this.Request["fid"];
                this.txtFClassID.Text = rc.GetNewsColNumber(fid);
                DataTable dt = rc.GetTable(EntityTypeEnum.EnTitle, "*", "fid='" + this.Request["fid"].ToString() + "' ");
                if (dt.Rows.Count < 1)
                {
                    tool.showMessageAndEndPage("数据有误，没有最新版本数据！");
                    return;
                }
                else
                {
                    DataRow dr = dt.Rows[0];
                    tool.fillPageControl(dr);
                    //impic.ImageUrl = this.t_FPicUrl.Text.Trim();
                    //label_FSize.Visible = true;
                    //label_FSize.Text = dt.Rows[0]["Fsize"].ToString() + " (M)";
                    if (dr["fstate"].ToString().Trim() == "1")
                        CBPublish.Checked = true;
                    else
                        CBPublish.Checked = false;

                    string sOperType = dr["FOperType"].ToString();
                    switch (sOperType)
                    {
                        case "1":
                            //this.rOper1.Checked = true;
                            //this.rOper2.Checked = false;
                            this.rOper3.Checked = false;
                            break;
                        case "2":
                            //this.rOper1.Checked = false;
                            //this.rOper2.Checked = true;
                            this.rOper3.Checked = false;
                            break;
                        case "0":
                            //this.rOper1.Checked = false;
                            //this.rOper2.Checked = false;
                            this.rOper3.Checked = true;
                            break;
                        default:
                            //this.rOper1.Checked = false;
                            //this.rOper2.Checked = false;
                            this.rOper3.Checked = true;
                            break;
                    }

                    Hfnewsid.Value = this.ViewState["FID"].ToString();
                    DataTable dtt = new DataTable();
                    dtt = rc.GetTable(EntityTypeEnum.EnContent, "", " FNewsId='" + this.Request["fid"].ToString() + "'");

                    if (dtt.Rows.Count > 0)
                    {
                        this.FMain.Value = dtt.Rows[0]["FContent"].ToString();
                        this.ViewState["TFID"] = dtt.Rows[0]["FId"].ToString();
                    }

                    //新闻分布栏目表
                    DataTable colDt = new DataTable();
                    colDt = rc.GetTable(EntityTypeEnum.EnCol, "", " FNewsId='" + this.Request["fid"].ToString() + "'");

                    if (colDt.Rows.Count > 0 && colDt != null)
                    {
                        this.ViewState["CFId"] = colDt.Rows[0]["FId"].ToString();
                    }

                }
            }

        }
    }

    private void ControlBind()
    {
        DataTable dt = rc.getDicTbByFNumber("");
        this.t_FTypeId.DataSource = dt;
        this.t_FTypeId.DataTextField = "fname";
        this.t_FTypeId.DataValueField = "fnumber";
        this.t_FTypeId.DataBind();
    }
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        if (t_FMain.Text.Length > 200)
        {
            tool.showMessage("摘要内容不能多于200字，现在字数为" + t_FMain.Text.Length + "，请删除多余部分");
            return;
        }
        SortedList sl = tool.getPageValue();//news
        SortedList sl1 = new SortedList();//newsclass
        SortedList sl2 = new SortedList();//newsCol

        SaveOptionEnum ou = SaveOptionEnum.Update;
        string FId = Guid.NewGuid().ToString();
        string nFId = Guid.NewGuid().ToString();
        string CFId = Guid.NewGuid().ToString(); // newsCol

        if (this.ViewState["FID"] == null)
        {
            ou = SaveOptionEnum.Insert;
            sl.Add("fcreatetime", DateTime.Now.ToString("yyyy-MM-dd"));
            sl1.Add("fcreatetime", DateTime.Now.ToString("yyyy-MM-dd"));
            sl2.Add("fcreatetime", DateTime.Now.ToString("yyyy-MM-dd")); // newsCol
            sl2.Add("fpubtime", DateTime.Now.ToString("yyyy-MM-dd")); //发布时间
            sl.Add("FCount", 0);
            //sl.Add("FPubPersonKind", Session["DFId"]);

        }
        else
        {
            FId = this.ViewState["FID"].ToString();
            nFId = this.ViewState["TFID"].ToString();
            //newsCol
            CFId = this.ViewState["CFId"].ToString();
        }
        sl.Add("FID", FId);
        sl.Add("Fisdeleted", 0);
        //if (this.rOper1.Checked)
        //{
        //    sl.Add("FOperType", 1);
        //}
        //if (this.rOper2.Checked)
        //{
        //    sl.Add("FOperType", 2);
        //}
        if (this.rOper3.Checked)
        {
            sl.Add("FOperType", 0);
        }
        if (CBPublish.Checked)
        {
            sl.Add("fstate", "1");
        }
        else
        {
            sl.Add("fstate", "0");
        }

        //新闻内容 
        sl1.Add("FID", nFId);
        sl2.Add("FID", CFId);
        sl1.Add("FNewsId", sl["FID"]);
        sl2.Add("FNewsId", sl["FID"]);
        sl2.Add("fstate", 1);
        sl1.Add("FIsDeleted", 0);
        sl2.Add("FIsDeleted", 0);
        string content = this.FMain.Value.ToString();
        content = content.Replace("'", "''");
        sl1.Add("FContent", this.FMain.Value.ToString());
        sl2.Add("FColNumber", this.txtFClassID.Text.Trim());

        SortedList[] sls = new SortedList[3];
        sls[0] = sl; //新闻标题
        sls[1] = sl1;//新闻内容
        sls[2] = sl2;//新闻对应的栏目

        EntityTypeEnum[] en = new EntityTypeEnum[3];
        en[0] = EntityTypeEnum.EnTitle;
        en[1] = EntityTypeEnum.EnContent;
        en[2] = EntityTypeEnum.EnCol;

        string[] fkey = new string[3];
        fkey[0] = "FID";
        fkey[1] = "FID";
        fkey[2] = "FID";

        SaveOptionEnum[] so = new SaveOptionEnum[3];
        so[0] = ou;
        so[1] = ou;
        so[2] = ou;

        bool Result = rc.SaveEBaseM(en, sls, fkey, so);

        if (Result)
        {

            this.ViewState["FID"] = sl["FID"].ToString();
            this.ViewState["TFID"] = sl1["FID"].ToString();
            this.ViewState["CFId"] = sl2["FID"].ToString();

            if (string.IsNullOrEmpty(txtFClassID.Text))
            {
                rc.PExcute(" delete from cf_news_Col where fnewsid='" + ViewState["FID"] + "' ");
            }


            Hfnewsid.Value = this.ViewState["FID"].ToString();
            tool.showMessageAndRunFunction("保存成功！", "window.returnValue=1;");
        }
        else
        {
            tool.showMessage("保存失败！");
        }

    }

    protected void backBtn_Click(object sender, System.EventArgs e)
    {
        this.RegisterStartupScript("js", "<script>getPageValue(1);</script>");
    }

    protected void btnUpPic_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        //if (this.FPicSelect.Value.Trim() == null || FPicSelect.Value.Trim() == "")
        //{
        //    tool.showMessage("请先选择文件");
        //    return;
        //}

        //	单文件上传
        HttpPostedFile upFile = Request.Files[0];
        string fileType = "gif,jpg";
        int fileSize = 100000 * 1024;
        string fFileName = upFile.FileName;
        fFileName = fFileName.ToUpper();
        if ((!fFileName.EndsWith("JPG")) && (!fFileName.EndsWith("GIF")))
        {
            tool.showMessage("请上传jpg或gif格式的图片");
            return;
        }

        string uploadSavePath = Function.GetRealPath("~/upload");

        string timePath = String.Format("{0}/{1}/{2}", DateTime.Now.ToString("yyyyMM"), DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH"));

        string userFilePath = String.Format("{0}/", timePath);
        string savePath = uploadSavePath + userFilePath;

        string returnPath = "../../upload/" + userFilePath;

        string[] uploadInfo = WebUtility.UploadFile(upFile, fileType, fileSize, false, savePath);
        string path = returnPath + uploadInfo[1];
        //this.t_FPicUrl.Text = path;
        //impic.ImageUrl = this.t_FPicUrl.Text;
        if (this.ViewState["FID"] != null)
        {
            if (uploadInfo[4] == "成功")
            {
                SortedList sl = new SortedList();
                sl.Add("FID", this.ViewState["FID"].ToString());
                //sl.Add("FPicUrl", this.t_FPicUrl.Text);
                rc.SaveEBase(EntityTypeEnum.EnTitle, sl, "FID", SaveOptionEnum.Update);
            }
        }
    }

    protected void BtnUpFile_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        //if (this.FFileSelect.Value.Trim() == null || FFileSelect.Value.Trim() == "")
        //{
        //    tool.showMessage("请先选择文件");
        //    return;
        //}

        //	单文件上传
        HttpPostedFile upFile = Request.Files[1];

        string fileType = "";
        int fileSize = 100000 * 1024;

        string uploadSavePath = Function.GetRealPath("~/upload");

        string timePath = String.Format("{0}/{1}/{2}", DateTime.Now.ToString("yyyyMM"), DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH"));

        string userFilePath = String.Format("{0}/", timePath);
        string savePath = uploadSavePath + userFilePath;
        string returnPath = "../../upload/" + userFilePath;

        string[] uploadInfo = WebUtility.UploadFile(upFile, fileType, fileSize, false, savePath);
        string path = returnPath + uploadInfo[1];
        //this.t_FFileNote.Text = path;
        if (this.ViewState["FID"] != null)
        {
            if (uploadInfo[4] == "成功")
            {
                //label_FSize.Visible = true;
                float fSize = upFile.ContentLength / 1048576f;
                //label_FSize.Text = fSize + " (M)";
                SortedList sl = new SortedList();
                sl.Add("FID", this.ViewState["FID"].ToString());
                //sl.Add("FFileNote", this.t_FFileNote.Text);
                sl.Add("FSize", fSize);
                rc.SaveEBase(EntityTypeEnum.EnTitle, sl, "FID", SaveOptionEnum.Update);
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveInfo();
    }
}
