
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Approve.EntityBase;
using Approve.RuleCenter;
using Approve.EntitySys;
/// <summary>
///		GovDeptDisp 的摘要说明。
/// </summary>
public partial class Common_GovDeptId3 : System.Web.UI.UserControl
{
    private string _sDeptId = "";
    RCenter rc = new RCenter();
    public event EventHandler SelectedIndexChanged;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            if (SelectedIndexChanged != null)
            {
                FCountry.AutoPostBack = true;
            }
            if (this._sDeptId == null || _sDeptId == "")
            {
                if (ShowDefaultDept)
                {
                    fNumber = FNumber = ComFunction.GetDefaultDept(); //获取默认管理部门
                }
                //this.ShowProvince();
            }
        }
    }

    public string FNumber
    {
        set
        {
            _sDeptId = value;
            this.SetData();
        }
        get
        {
            if (this.FCountry.SelectedValue != null && this.FCountry.SelectedValue.Trim() != "")
            {
                return this.FCountry.SelectedValue;
            }
            else
            {
                if (this.FCity.SelectedValue != null && this.FCity.SelectedValue.Trim() != "")
                {
                    return this.FCity.SelectedValue;
                }
                else
                    if (this.FProvince.SelectedValue != null && this.FProvince.SelectedValue.Trim() != "")
                        return this.FProvince.SelectedValue.Trim();
                    else
                        return null;
            }
        }
    }
    public string fNumber
    {
        set
        {
            _sDeptId = value;
            this.SetData();
        }
        get
        {
            if (this.FCountry.SelectedValue != null && this.FCountry.SelectedValue.Trim() != "")
            {
                return this.FCountry.SelectedValue;
            }
            else
            {
                if (this.FCity.SelectedValue != null && this.FCity.SelectedValue.Trim() != "")
                {
                    return this.FCity.SelectedValue;
                }
                else
                    if (this.FProvince.SelectedValue != null && this.FProvince.SelectedValue.Trim() != "")
                        return this.FProvince.SelectedValue.Trim();
                    else
                        return null;
            }
        }
    }
    public void IsDisProvince(bool isD)
    {
        if (isD)
        {
        }
        else
        {
            this.FProvince.Enabled = false;
        }
    }

    public void Dis(int n)
    {
        if (n == 1)
            this.FProvince.Enabled = false;
        else if (n == 2)
        {
            this.FProvince.Enabled = false;
            this.FCity.Enabled = false;
        }
        else if (n == 3)
        {
            this.FProvince.Enabled = false;
            this.FCity.Enabled = false;
            this.FCountry.Enabled = false;
        }
        else
        {
            this.FProvince.Enabled = true;
            this.FCity.Enabled = true;
            this.FCountry.Enabled = true;
        }
    }
    public void Vis(int n)
    {
        if (n == 2)
        {
            this.FCountry.Visible = false;
            this.FCity.Visible = false;
        }
        else if (n == 3)
        {
            this.FCountry.Visible = false;
        }
        else
        {
            this.FProvince.Visible = true;
            this.FCity.Visible = true;
            this.FCountry.Visible = true;
        }
    }

    private bool _ShowDefaultDept = true;
    public bool ShowDefaultDept
    {
        get { return _ShowDefaultDept; }
        set { _ShowDefaultDept = value; }
    }

    private bool _RemoveDefaultDept = false;
    public bool RemoveDefaultDept
    {
        get { return _RemoveDefaultDept; }
        set { _RemoveDefaultDept = value; }
    }


    public string GetProvinceNumber()
    {
        return this.FProvince.SelectedValue.Trim();
    }

    public string GetCityNumber()
    {
        return this.FCity.SelectedValue.Trim();
    }

    public string GetCountryNumber()
    {
        return this.FCountry.SelectedValue;
    }

    public string GetProvinceText()
    {
        return this.FProvince.SelectedItem.Text.Trim();
    }

    public string GetCityText()
    {
        return this.FCity.SelectedItem.Text.Trim();
    }

    public string GetCountryText()
    {
        return this.FCountry.SelectedItem.Text.Trim();
    }

    public string DeptFullName
    {
        get
        {
            if (FNumber != "" && FNumber != null)
            {

                return rc.GetSignValue(EntityTypeEnum.EsManageDept, "ffullname", "fnumber='" + FNumber + "' and fisdeleted=0");
            }
            else
                return "";
        }
    }

    private void SetData()
    {
        ShowProvince();

        if (_sDeptId == null || _sDeptId == "")
        {
            this.FCity.Items.Clear();
            this.FCountry.Items.Clear();
            return;
        }

        EsManageDept Em = (EsManageDept)rc.GetEBase(EntityTypeEnum.EsManageDept, "fid,fparentid,flevel,fnumber", "fnumber='" + _sDeptId + "'and fisdeleted=0 order by forder,fnumber");
        if (Em == null) return;
        if (Em.FLevel == 1)
        {

            ListItem li = this.FProvince.Items.FindByValue(Em.FNumber);
            this.FProvince.SelectedIndex = this.FProvince.Items.IndexOf(li);
            this.ShowCity(Em.FNumber);
            this.FCity.SelectedIndex = -1;
            this.FCountry.Items.Clear();
        }

        if (Em.FLevel == 2)
        {

            ListItem li = this.FProvince.Items.FindByValue(Em.FParentId);
            this.FProvince.SelectedIndex = this.FProvince.Items.IndexOf(li);

            this.ShowCity(Em.FParentId);
            li = this.FCity.Items.FindByValue(Em.FNumber);
            this.FCity.SelectedIndex = this.FCity.Items.IndexOf(li);
            this.ShowCountry(Em.FNumber);
        }
        if (Em.FLevel == 3)
        {

            EsManageDept Emp = (EsManageDept)rc.GetEBase(EntityTypeEnum.EsManageDept, "fid,fparentid,flevel,fnumber", "fnumber='" + Em.FParentId + "'and fisdeleted=0 and fname!='市辖区' order by forder,fnumber");
            ListItem li = this.FProvince.Items.FindByValue(Emp.FParentId);
            this.FProvince.SelectedIndex = this.FProvince.Items.IndexOf(li);

            this.ShowCity(Emp.FParentId);
            li = this.FCity.Items.FindByValue(Em.FParentId);
            this.FCity.SelectedIndex = this.FCity.Items.IndexOf(li);

            this.ShowCountry(Em.FParentId);
            li = this.FCountry.Items.FindByValue(Em.FNumber);
            this.FCountry.SelectedIndex = this.FCountry.Items.IndexOf(li);
        }
    }

    private void ShowProvince()
    {
        this.FProvince.Items.Clear();
        string s = "";
        if (ShowDefaultDept)
        {
            s = " and FNumber='" + ComFunction.GetDefaultDept() + "'";
        }
        if (RemoveDefaultDept)
        {
            s = " and FNumber<>'" + ComFunction.GetDefaultDept() + "'";
        }

        DataTable dt = rc.GetTable(EntityTypeEnum.EsManageDept, "FNumber,fname", " flevel=1 and fisdeleted=0 " + s + " order by forder,fnumber ");
        this.FProvince.DataSource = dt;
        this.FProvince.DataTextField = "fname";
        this.FProvince.DataValueField = "FNumber";
        this.FProvince.DataBind();
        this.FProvince.Items.Insert(0, "");
    }

    private void ShowCity(string ProvinceId)
    {
        DataTable dt = rc.GetTable(EntityTypeEnum.EsManageDept, "FNumber,fname", "fparentid='" + ProvinceId + "' and flevel=2 and FNumber Not in (2115,2116,2117,2118,2119,2120,2121) and fisdeleted=0 order by forder,fnumber");
        this.FCity.DataSource = dt;
        this.FCity.DataTextField = "fname";
        this.FCity.DataValueField = "FNumber";
        this.FCity.DataBind();
        this.FCity.Items.Insert(0, "");
    }

    private void ShowCountry(string CityId)
    {
        DataTable dt = rc.GetTable(EntityTypeEnum.EsManageDept, "FNumber,fname", "fparentid='" + CityId + "'  and flevel=3 and fisdeleted=0 order by forder,fnumber");
        this.FCountry.DataSource = dt;
        this.FCountry.DataTextField = "fname";
        this.FCountry.DataValueField = "FNumber";
        this.FCountry.DataBind();
        this.FCountry.Items.Insert(0, "");
    }

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    ///		设计器支持所需的方法 - 不要使用代码编辑器
    ///		修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    void GovDeptId_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void FCity_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string sCity = this.FCity.SelectedValue.Trim();
        this.ShowCountry(sCity);
        if (SelectedIndexChanged != null)
        {
            SelectedIndexChanged(FNumber, e);
        }
    }

    protected void FProvince_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string sProvince = this.FProvince.SelectedValue.Trim();
        this.ShowCity(sProvince);
        this.FCountry.Items.Clear();
    }
    protected void FCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (SelectedIndexChanged != null)
        {
            SelectedIndexChanged(FNumber, e);
        }
    }
}