namespace Approve.Common
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Approve.RuleBase;
    using Approve.RuleCenter;
    using System.Collections;
    /// <summary>
    ///		Pager 的摘要说明。
    /// </summary>
    public partial class Pager : System.Web.UI.UserControl
    {
        private int _CurPage = 1;
        private int _PageCount = 15;
        private string _ControlToPage = "DG_List";
        private string _ControlType = "DataGrid";
        private string _SQL = "";
        private bool _isbind = true;
        private string _className = "RCenter";
        private bool _isShowOut = false;
        //		private string _SpecialPage="";


        private string _OutSQL = "";
        private string _OutFileName = "";
        private string _OutTitle = "";


        private string _sScriptName = "";
        private string _sSessionName = "";
        private string _sSessionValue = "";


        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (NavPage.Text == "" || isNum(NavPage.Text))
            {

            }
            else
            {
                Response.Clear();
                Response.End();
            }
            CehckSession();
            if (!this.IsPostBack)
            {
                if (this.ViewState["isbind"] == null)
                    this.ViewState["isbind"] = this._isbind;
                if (this.ViewState["SQL"] == null)
                    this.ViewState["SQL"] = this._SQL;//查询的SQL语句
                if (this.ViewState["PageCount"] == null)
                    this.ViewState["PageCount"] = this._PageCount;//每页记录数
                if (this.ViewState["ControlToPage"] == null)
                    this.ViewState["ControlToPage"] = this._ControlToPage;//数据绑定的控件
                if (this.ViewState["CurPage"] == null)
                    this.ViewState["CurPage"] = this._CurPage;//当前页码
                if (this.ViewState["ControlType"] == null)
                    this.ViewState["ControlType"] = this._ControlType;//数据绑定的控件类型

                if (this.ViewState["className"] == null)
                    this.ViewState["className"] = this._className;//类名称

                if (this.ViewState["isshowount"] == null)
                    this.ViewState["isshowount"] = this._isShowOut; //是否显示导出按钮

                if (this.ViewState["OutSQL"] == null)
                    this.ViewState["OutSQL"] = this._OutSQL;//导出的SQL语句
                if (this.ViewState["OutFileName"] == null)
                    this.ViewState["OutFileName"] = this._OutFileName;//导出的文件名
                if (this.ViewState["OutTitle"] == null)
                    this.ViewState["OutTitle"] = this._OutTitle;// 

                if (this.ViewState["SScriptName"] == null)
                    this.ViewState["SScriptName"] = this._sScriptName;// 

                if (this.ViewState["SSessionName"] == null)
                    this.ViewState["SSessionName"] = this._sSessionName;// 

                if (this.ViewState["SSessionValue"] == null)
                    this.ViewState["SSessionValue"] = this._sSessionValue;// 

                //this.Pagination(Convert.ToInt32(this.ViewState["CurPage"]));
                if (IsShowOut == true)
                {
                    this.btnOut.Visible = true;
                }



            }
        }

        public string sScriptName
        {
            get
            {
                if (this.ViewState["SScriptName"] != null)
                    return this.ViewState["SScriptName"].ToString();
                else return this._sScriptName;
            }
            set
            {
                this.ViewState["SScriptName"] = value;
            }
        }

        public string sSessionName
        {
            get
            {
                if (this.ViewState["SSessionName"] != null)
                    return this.ViewState["SSessionName"].ToString();
                else return this._sScriptName;
            }
            set
            {
                this.ViewState["SSessionName"] = value;
            }
        }


        public string sSessionValue
        {
            get
            {
                if (this.ViewState["SSessionValue"] != null)
                    return this.ViewState["SSessionValue"].ToString();
                else return this._sScriptName;
            }
            set
            {
                this.ViewState["SSessionValue"] = value;
            }
        }

        private void CehckSession()
        {

            if (Session[sSessionName] != null && Session[sSessionName].ToString() == sSessionValue)
            {
                this.Page.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>try{" + sScriptName + "();}catch(err){}</script>");
            }
        }
        /// <summary>
        /// 进行查询的SQL语句
        /// </summary>
        public string sql
        {
            get
            {
                if (this.ViewState["SQL"] != null)
                    return this.ViewState["SQL"].ToString();
                else return this._SQL;
            }
            set
            {
                this.ViewState["SQL"] = value;
            }
        }

        public int RecordCount
        {
            get
            {
                if (this.ViewState["RecordCount"] != null)
                    return (int)this.ViewState["RecordCount"];
                else return 0;
            }
        }

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int pagecount
        {
            get
            {
                if (this.ViewState["PageCount"] != null)
                    return (int)this.ViewState["PageCount"];
                else return this._PageCount;
            }
            set
            {
                this.ViewState["PageCount"] = value;
            }
        }

        public int curpage
        {
            get
            {
                if (this.ViewState["CurPage"] != null)
                    return (int)this.ViewState["CurPage"];
                else return this._CurPage;
            }
            set
            {
                this.ViewState["CurPage"] = value;
            }
        }

        public int counts
        {
            get
            {
                if (this.ViewState["counts"] != null)
                    return (int)this.ViewState["counts"];
                else
                {
                    this.ViewState["counts"] = ClassType.GetCount(sql);
                    return (int)this.ViewState["counts"];
                }
            }
        }

        //到处SQL
        public string Outsql
        {
            get
            {
                if (this.ViewState["OutSQL"] != null)
                    return this.ViewState["OutSQL"].ToString();
                else return this._OutSQL;
            }
            set
            {
                this.ViewState["OutSQL"] = value;
            }
        }

        //到处标题 
        public string OutFileName
        {
            get
            {
                if (this.ViewState["OutFileName"] != null)
                    return this.ViewState["OutFileName"].ToString();
                else return this._OutFileName;
            }
            set
            {
                this.ViewState["OutFileName"] = value;
            }
        }

        //到处标题 
        public string OutTitle
        {
            get
            {
                if (this.ViewState["OutTitle"] != null)
                    return this.ViewState["OutTitle"].ToString();
                else return this._OutTitle;
            }
            set
            {
                this.ViewState["OutTitle"] = value;
            }
        }


        /// <summary>
        /// 被控制分页的控件名称
        /// </summary>
        public string controltopage
        {
            get
            {
                if (this.ViewState["ControlToPage"] != null)
                    return this.ViewState["ControlToPage"].ToString();
                else return this._ControlToPage;
            }
            set
            {
                this.ViewState["ControlToPage"] = value;
            }
        }

        /// <summary>
        /// 控制分页的控件类型DataGrid Repeater
        /// </summary>
        public string controltype
        {
            get
            {
                if (this.ViewState["ControlType"] != null)
                    return this.ViewState["ControlType"].ToString();
                else return this._ControlType;
            }
            set
            {
                this.ViewState["ControlType"] = value;
            }
        }

        /// <summary>
        /// 获得需要绑定到控件上的DataTable
        /// </summary>
        public DataTable GetTable
        {
            get
            {
                return this.GetDataTable(this.curpage);
            }
          
        }
       

        public string SpecialPage
        {
            set
            {
                this.ViewState["SpecialPage"] = value;
            }
        }

        public bool IsBind
        {
            set
            {
                this.ViewState["isbind"] = value;
            }
            get
            {
                if (this.ViewState["isbind"] != null)
                    return (bool)this.ViewState["isbind"];
                else return this._isbind;
            }
        }

        public bool IsShowOut
        {
            set
            {
                this.ViewState["isshowout"] = value;
            }
            get
            {
                if (this.ViewState["isshowout"] != null)
                    return (bool)this.ViewState["isshowout"];
                else return this._isShowOut;
            }
        }
        public IDictionary Parameters
        {
            get { return ViewState["Parameter"] as IDictionary; }
            set { ViewState["Parameter"] = value; }
        }
        public string Condition
        {
            set
            {
                this.ViewState["condition"] = value;
            }
            get
            {
                if (this.ViewState["condition"] != null)
                    return (string)this.ViewState["condition"];
                else return "";
            }
        }
        public string className
        {
            set { this.ViewState["className"] = value; }
            get
            {
                if (this.ViewState["className"] != null)
                    return (string)this.ViewState["className"];
                else return "";
            }
        }
        public RBase ClassType
        {
            get
            {
                switch (this.className)
                {
                    case "RCenter":
                        return new RCenter();
                    case "dbShare":
                        return new Share();
                    case "RCenterBackup":
                        return new RCenter(0);
                    case "dbOA":
                        return new OA();
                    case "dbJST":
                        return new RCenter("dbJST");
                    case "JST_XZSPBaseInfo":
                        return new RCenter("JST_XZSPBaseInfo");
                    case "XM_BaseInfo":
                        return new RCenter("XM_BaseInfo");
                    default:
                        return new RCenter();
                }
            }
        }

        public void dataBind()
        {
            this.Pagination(Convert.ToInt32(this.ViewState["CurPage"]));
        }

        protected void lb_First_Click(object sender, System.EventArgs e)
        {
            this.Pagination(1);
        }

        protected void lb_Prev_Click(object sender, System.EventArgs e)
        {


            this.Pagination((int)this.ViewState["CurPage"] - 1);


        }

        protected void lb_Next_Click(object sender, System.EventArgs e)
        {

            this.Pagination((int)this.ViewState["CurPage"] + 1);

        }

        protected void lb_Last_Click(object sender, System.EventArgs e)
        {

            this.Pagination(Convert.ToInt32(this.Pages.Text));

        }

        public void btn_Go_Click(object sender, System.EventArgs e)
        {


            if (this.NavPage.Text.Trim() != String.Empty && isNum(this.NavPage.Text))
            {
                this.Pagination(Convert.ToInt32(this.NavPage.Text));
            }


        }

        private bool Pagination(int curPage)
        {
            this.ViewState["dt"] = null;
            DataTable dt = this.GetDataTable(curPage);

            if (this.ViewState["isbind"] == null)
                this.ViewState["isbind"] = this._isbind;
            string controlToPage = (string)this.ViewState["ControlToPage"];
            string controlType = (string)this.ViewState["ControlType"];
            if (dt == null)
            {
                return false;
            }
            System.Web.UI.Control ctrl = this.Parent.FindControl(controlToPage);
            DataGrid dg = null;
            Repeater rp = null;
            DataList dl = null;
            GridView gv = null;
            switch (controlType)
            {
                case "DataGrid":
                    dg = (DataGrid)ctrl;
                    break;
                case "Repeater":
                    rp = (Repeater)ctrl;
                    break;
                case "DataList":
                    dl = (DataList)ctrl;
                    break;
                case "GridView":
                    gv = (GridView)ctrl;
                    break;
                default:
                    break;
            }
            if ((bool)this.ViewState["isbind"] == true)
            {
                if (dg != null)
                {
                    dg.DataSource = dt.DefaultView;
                    dg.DataBind();
                }
                else if (rp != null)
                {
                    rp.DataSource = dt.DefaultView;
                    rp.DataBind();
                }
                else if (dl != null)
                {
                    dl.DataSource = dt.DefaultView;
                    dl.DataBind();
                }
                else if (gv != null)
                {
                    gv.DataSource = dt.DefaultView;
                    gv.DataBind();
                }
                else
                {
                    return false;
                }
                return true;
            }
            return true;
            //			else
            //			{
            //				if(this.ViewState["SpecialPage"]!=null)
            //                {
            //					DataSet ds= new DataSet();
            //					if(this.ViewState["SpecialPage"].ToString()=="Achievement")
            //					{
            //						ds.Tables.Add(dt.Copy());
            //						ds.Tables[0].TableName="p";
            //						string condition="";
            //						if(dt!=null)
            //						{
            //							if(dt.Rows.Count>0)
            //							{
            //								condition=" and a.fid in(";
            //								for(int i=0;i<dt.Rows.Count;i++)
            //								{
            //									condition+="'"+dt.Rows[i]["FID"].ToString().Trim()+"',";
            //								}
            //								condition=condition.Substring(0,condition.Length-1);
            //								condition+=") ";
            //							}
            //							AchievementManage am = new AchievementManage();
            //							DataTable dtc = am.GetAchievementCheckPara(Session["FId"].ToString(),(DateTime)Session["FValidBegin"],(DateTime)Session["FValidEnd"],condition);
            //							ds.Tables.Add(dtc.Copy());
            //							ds.Tables[1].TableName="c";
            //							DataRelation arel = new DataRelation("AchieveRelation",ds.Tables[0].Columns["FID"],ds.Tables[1].Columns["FRELATIONID"],false);
            //							ds.Relations.Add(arel);
            //						}
            //					}
            //					else if(this.ViewState["SpecialPage"].ToString()=="Device")
            //					{
            //						ds.Tables.Add(dt.Copy());
            //						ds.Tables[0].TableName="p";
            //						string condition="";
            //						if(dt!=null)
            //						{
            //							if(dt.Rows.Count>0)
            //							{
            //								condition=" and d.fid in(";
            //								for(int i=0;i<dt.Rows.Count;i++)
            //								{
            //									condition+="'"+dt.Rows[i]["FID"].ToString().Trim()+"',";
            //								}
            //								condition=condition.Substring(0,condition.Length-1);
            //								condition+=") ";
            //							}
            //							DeviceManage dm = new DeviceManage();
            //							DataTable dtc = dm.GetDeviceCheckPara(Session["FId"].ToString(),(DateTime)Session["FValidBegin"],(DateTime)Session["FValidEnd"],condition);
            //							ds.Tables.Add(dtc.Copy());
            //							ds.Tables[1].TableName="c";
            //							DataRelation drel = new DataRelation("DeviceRelation",ds.Tables[0].Columns["FID"],ds.Tables[1].Columns["FRELATIONID"],false);
            //							ds.Relations.Add(drel);
            //						}
            //					}
            //					else
            //					{
            //						return false;
            //					}
            //					if(dg!=null)
            //					{
            //						dg.DataSource=ds.Tables[0].DefaultView;
            //						Page.DataBind();
            //					}
            //					else if(rp!=null)
            //					{
            //						rp.DataSource=ds.Tables[0].DefaultView;
            //						Page.DataBind();
            //					}
            //					else
            //					{
            //						return false;
            //					}
            //				}
            //				return true;
            //			}
        }

        private DataTable GetDataTable(int curPage)
        {
            DataTable dt = null;
            string sql = (string)this.ViewState["SQL"];
            if (sql.Trim() == null || sql.Trim() == "") return null;
            int pageCount = (int)this.ViewState["PageCount"];

            int counts = ClassType.GetCount(sql, ClassType.ConvertParameters(Parameters));

            TotleCount.Text = counts.ToString();
            //TotleCount.Text = "221";
            this.ViewState["RecordCount"] = counts;
            int pc = (counts % pageCount == 0) ? counts / pageCount : counts / pageCount + 1;//总页数
            if (curPage > 1)//传入的当前页码超过总页码,转到最后一页
            {
                curPage = (curPage > pc) ? pc : curPage;
            }
            else//传入的页码小于1,转到第一页
            {
                curPage = 1;
            }
            this.ViewState["CurPage"] = curPage;
            int startrow = pageCount * (curPage - 1);
            int endrow = pageCount * curPage + 1;
            //Label赋值
            this.CurPage.Text = curPage.ToString();//当前页
            this.Pages.Text = pc.ToString();
            this.Counts.Text = pageCount.ToString();//页记录数
            if (curPage == 1)
            {
                this.lb_First.Enabled = false;
                this.lb_Prev.Enabled = false;
            }
            else
            {
                if (this.lb_First.Enabled == false) this.lb_First.Enabled = true;
                if (this.lb_Prev.Enabled == false) this.lb_Prev.Enabled = true;
            }
            if (curPage == pc)
            {
                this.lb_Last.Enabled = false;
                this.lb_Next.Enabled = false;
            }
            else
            {
                if (this.lb_Last.Enabled == false) this.lb_Last.Enabled = true;
                if (this.lb_Next.Enabled == false) this.lb_Next.Enabled = true;
            }
            if (this.ViewState["dt"] != null)
            {
                dt = (DataTable)this.ViewState["dt"];
            }
            else
            {

                dt = ClassType.GetTable(sql, startrow, endrow, ClassType.ConvertParameters(Parameters));
                this.ViewState["dt"] = dt;
            }
            if (curPage == 1 && dt.Rows.Count == 0)
            {
                this.lb_First.Enabled = false;
                this.lb_Last.Enabled = false;
                this.lb_Next.Enabled = false;
                this.lb_Prev.Enabled = false;
                this.btn_Go.Attributes.Add("disabled", "true");
            }
            return dt;
        }

        protected void NavPage_TextChanged(object sender, EventArgs e)
        {
            if (this.NavPage.Text.Trim() != String.Empty && isNum(this.NavPage.Text))
            {
                this.Pagination(Convert.ToInt32(this.NavPage.Text));
            }

        }
        public bool isNum(string num)
        {
            try
            {
                Convert.ToInt32(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
        protected void btnOut_Click(object sender, EventArgs e)
        {
            if (this.ViewState["OutSQL"] == null)
            {
                return;
            }

            DataTable dt = ClassType.GetTable(this.ViewState["OutSQL"].ToString());
            //			bc.ToExcel(this.Page,dt,"资质申请企业名单","资质申请企业名单");
            string sFileName = this.ViewState["OutFileName"].ToString();
            string sHeadTittle = this.ViewState["OutTitle"].ToString(); ;
            System.Web.UI.WebControls.DataGrid D_list = new System.Web.UI.WebControls.DataGrid();
            D_list.DataSource = dt.DefaultView;
            D_list.AllowPaging = false;
            D_list.HeaderStyle.BackColor = System.Drawing.Color.SkyBlue;
            D_list.HeaderStyle.Font.Bold = true;
            D_list.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";  //设置了类型为中文防止乱码的出现   
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8)

+ ".xls"); //定义输出文件和文件名 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文 
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。  

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            D_list.RenderControl(oHtmlTextWriter);
            //oStringWriter.Write(oHtmlTextWriter.ToString());
            Response.Write(oStringWriter.ToString());
            oHtmlTextWriter.Close();
            oStringWriter.Close();


            D_list.Dispose();
            D_list = null;
            //			this.dataBind();
            Response.End();
        }
    }
}
