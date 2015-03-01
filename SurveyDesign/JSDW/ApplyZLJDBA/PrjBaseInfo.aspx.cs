using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Tools;

public partial class JSDW_ApplyZLJDBA_PrjBaseInfo : System.Web.UI.Page
{
    EgovaDB db = new EgovaDB();
    string fAppId = "";
    string fPrjItemType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fAppId = EConvert.ToString(Session["FAppId"]);
            TC_QA_Record qa = db.TC_QA_Record.Where(t => t.FAppId == EConvert.ToString(Session["FAppId"])).FirstOrDefault();
            fPrjItemType = qa.PrjItemType;
            ViewState["FPrjItemType"] = qa.PrjItemType;
            if (fPrjItemType == "2000101")//房屋建筑
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showDiv2", "<script>showDiv2();</script>");
            }
            else if (fPrjItemType == "2000102")//市政
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showDiv1", "<script>showDiv1();</script>");
            }
            pageTool tool = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                //tool.ExecuteScript("btnEnable();");
                ClientScript.RegisterStartupScript(this.GetType(), "hideSaveBtn1", "<script>hideSaveBtn();</script>");
            }

        }
        foreach (ListItem item in CBLCityPlan.Items)
        {
            item.Attributes.Add("val", item.Value);
        }
        foreach (ListItem item in briBottomType.Items)
        {
            item.Attributes.Add("val", item.Value);
        }
        foreach (ListItem item in briTopType.Items)
        {
            item.Attributes.Add("val", item.Value);
        }
        foreach (ListItem item in DrainageDiameter.Items)
        {
            item.Attributes.Add("val", item.Value);
        }
        foreach (ListItem item in GasPipeType.Items)
        {
            item.Attributes.Add("val", item.Value);
        }
        foreach (ListItem item in BaseConstrType.Items)
        {
            item.Attributes.Add("val", item.Value);
        }
        foreach (ListItem item in MixFoundationType.Items)
        {
            item.Attributes.Add("val", item.Value);
        }
        foreach (ListItem item in PileBaseType.Items)
        {
            item.Attributes.Add("val", item.Value);
        }
        foreach (ListItem item in CurtainWall.Items)
        {
            item.Attributes.Add("val", item.Value);
        }
        showPrjData();
    }
    private void showPrjData()
    {
        EgovaDB db = new EgovaDB();
        TC_QA_PrjAddition pa = db.TC_QA_PrjAddition.Where(t => t.FAppId == EConvert.ToString(Session["FAppId"])).FirstOrDefault();
        if (EConvert.ToString(ViewState["FPrjItemType"]) == "2000101")//房屋建筑
        {
            ClientScript.RegisterStartupScript(this.GetType(), "show2", "<script>showDiv2();</script>");
        }
        else if (EConvert.ToString(ViewState["FPrjItemType"]) == "2000102")//市政
        {
            ClientScript.RegisterStartupScript(this.GetType(), "show1", "<script>showDiv1();</script>");
        }
        if (pa != null)
        {
            if (EConvert.ToString(ViewState["FPrjItemType"]) == "2000101")//房屋建筑
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "show2", "<script>showDiv2();</script>");
                pageTool tool = new pageTool(this.Page, "t_");
                tool.fillPageControl(pa);
                setCheckBoxList(pa.BaseConstrType,BaseConstrType,"BaseConstrTxt",BaseConstrTxt,pa.BaseConstrTxt);
                setRadioButtonList(pa.FoundationType, FoundationType, "", null, "");
                setCheckBoxList(pa.MixFoundationType, MixFoundationType, "MixFoundationTxt", MixFoundationTxt, pa.MixFoundationTxt);
                setCheckBoxList(pa.PileBaseType, PileBaseType, "PileBaseTxt", PileBaseTxt, pa.PileBaseTxt);
                setRadioButtonList(pa.StealStruct, StealStruct, "", null, "");
                setCheckBoxList(pa.EquipInstall, EquipInstall, "", null, "");
                setCheckBoxList(pa.CurtainWall, CurtainWall, "CurtainWallTxt", CurtainWallTxt, pa.CurtainWallTxt);
            }
            else if (EConvert.ToString(ViewState["FPrjItemType"]) == "2000102")//市政
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "show1", "<script>showDiv1();</script>");
                pageTool tool = new pageTool(this.Page, "t_");
                tool.fillPageControl(pa);
                setCheckBoxList(pa.CityPlanType, CBLCityPlan, "", null, "");
                ClientScript.RegisterStartupScript(this.GetType(), "showTable", "<script>showTable();</script>");
                string cpy = pa.CityPlanType+",";
                setRadioButtonList(pa.BriBaseType, briBaseType, "BriBaseTxt", briBaseTxt, pa.BriBaseTxt);
                setCheckBoxList(pa.BriBottomType, briBottomType, "", null, "");
                setCheckBoxList(pa.BriTopType, briTopType, "BriTopTxt", birTopTxt, pa.BriTopTxt);
                if (pa.BriLength!=null)
                {
                    string[] bl = pa.BriLength.Split(',');
                    if (bl.Length > 0)
                    {
                        if (bl[0] == "1")
                        {
                            setRadioButtonList(bl[0], briLength, "BriLengthTxt", briLengthTxt1, bl[1]);
                        }
                        else if (bl[0] == "2")
                        {
                            setRadioButtonList(bl[0], briLength, "BriLengthTxt", briLengthTxt2, bl[1]);
                        }
                    }
                }
                setRadioButtonList(pa.BriLoadLevel, briLoadLevel, "", null, "");
                setRadioButtonList(pa.RBBaseType, RBBaseType, "", null, "");
                setRadioButtonList(pa.RBMainType, RBMainType, "", null, "");
                setRadioButtonList(pa.TunnelBaseType, TunnelBaseType, "", null, "");
                setRadioButtonList(pa.TunnelMainType, TunnelMainType, "", null, "");
                setRadioButtonList(pa.TunnelPumpHouse, TunnelPumpHouse, "", null, "");
                setRadioButtonList(pa.RoadLayerType, RoadLayerType, "", null, "");
                setCheckBoxList(pa.DrainagePipeType, DrainagePipeType, "", null, "");
                if (pa.DrainageDiameter!=null)
                {
                    string[] dd = pa.DrainageDiameter.Split(',');
                    if (dd.Length > 0)
                    {
                        if (dd.Length == 4)
                        {
                            setCheckBoxList(dd[0], DrainageDiameter, "DiameterTxt", DiameterTxt1, dd[1]);
                            setCheckBoxList(dd[2], DrainageDiameter, "DiameterTxt", DiameterTxt2, dd[3]);
                        }
                        else if (dd.Length == 2)
                        {
                            if (dd[0] == "1")
                            {
                                setCheckBoxList(dd[0], DrainageDiameter, "DiameterTxt", DiameterTxt1, dd[1]);
                            }
                            else if (dd[0] == "2")
                            {
                                setCheckBoxList(dd[0], DrainageDiameter, "DiameterTxt", DiameterTxt2, dd[1]);
                            }
                        }
                    }
                }
                setCheckBoxList(pa.WSPipeType, WSPipeType, "", null, "");
                setCheckBoxList(pa.ECConstrType, ECConstrType, "", null, "");
                setCheckBoxList(pa.GasPipeType, GasPipeType, "GasPipeTxt", GasPipeTxt, pa.GasPipeTxt);
                setRadioButtonList(pa.WTBaseType, WTBaseType, "WTBaseTxt", WTBaseTxt, pa.WTBaseTxt);
                setRadioButtonList(pa.WTMainType, WTMainType, "", null, "");
            }
        }
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //EgovaDB db = new EgovaDB();
        TC_QA_PrjAddition pa = db.TC_QA_PrjAddition.Where(t => t.FAppId == EConvert.ToString(Session["FAppId"])).FirstOrDefault();
        if (pa != null)
        {
            ViewState["FPAID"] = pa.FId;
        }
        string fId = EConvert.ToString(ViewState["FPAID"]);
        TC_QA_PrjAddition Emp = new TC_QA_PrjAddition();
        pageTool tool = new pageTool(this.Page,"t_");
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = db.TC_QA_PrjAddition.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FAppId = Convert.ToString(Session["FAppId"]);
            db.TC_QA_PrjAddition.InsertOnSubmit(Emp);
        }
        Emp = tool.getPageValue(Emp);
        if (EConvert.ToString(ViewState["FPrjItemType"]) == "2000101")
        {
            Emp.BaseConstrType = getCheckBoxList(BaseConstrType);
            Emp.BaseConstrTxt = BaseConstrTxt.Text;
            Emp.FoundationType = getRadioButtonList(FoundationType);
            Emp.MixFoundationType = getCheckBoxList(MixFoundationType);
            Emp.MixFoundationTxt = MixFoundationTxt.Text;
            Emp.PileBaseType = getCheckBoxList(PileBaseType);
            Emp.PileBaseTxt = PileBaseTxt.Text;
            Emp.StealStruct = getRadioButtonList(StealStruct);
            Emp.EquipInstall = getCheckBoxList(EquipInstall);
            Emp.CurtainWall = getCheckBoxList(CurtainWall);
        }
        else if (EConvert.ToString(ViewState["FPrjItemType"]) == "2000102")//市政
        {
            Emp.CityPlanType = getCheckBoxList(CBLCityPlan);
            Emp.BriBaseType = getRadioButtonList(briBaseType);
            Emp.BriBaseTxt = briBaseTxt.Text;
            Emp.BriBottomType = getCheckBoxList(briBottomType);
            Emp.BriTopType = getCheckBoxList(briTopType);
            Emp.BriTopTxt = birTopTxt.Text;
            if (getRadioButtonList(briLength) =="1"){
                Emp.BriLength = getRadioButtonList(briLength)+","+briLengthTxt1.Text;
            } else if(getRadioButtonList(briLength) =="2") {
                Emp.BriLength = getRadioButtonList(briLength) + "," + briLengthTxt2.Text;
            }
            Emp.BriLoadLevel = getRadioButtonList(briLoadLevel);
            Emp.RBBaseType = getRadioButtonList(RBBaseType);
            Emp.RBMainType = getRadioButtonList(RBMainType);
            Emp.TunnelBaseType = getRadioButtonList(TunnelBaseType);
            Emp.TunnelMainType = getRadioButtonList(TunnelMainType);
            Emp.TunnelPumpHouse = getRadioButtonList(TunnelPumpHouse);
            Emp.RoadLayerType = getRadioButtonList(RoadLayerType);
            Emp.DrainagePipeType = getCheckBoxList(DrainagePipeType);
            if (getCheckBoxList(DrainageDiameter).IndexOf('1') > -1)
            {
                Emp.DrainageDiameter = "1," + DiameterTxt1.Text;
                if (getCheckBoxList(DrainageDiameter).IndexOf('2') > -1)
                {
                    Emp.DrainageDiameter += ",2," + DiameterTxt2.Text;
                }
            }
            else if (getCheckBoxList(DrainageDiameter).IndexOf('2') > -1)
            {
                Emp.DrainageDiameter = "2," + DiameterTxt2.Text;
            }
            Emp.WSPipeType = getCheckBoxList(WSPipeType);
            Emp.ECConstrType = getCheckBoxList(ECConstrType);
            Emp.GasPipeType = getCheckBoxList(GasPipeType);
            Emp.GasPipeTxt = GasPipeTxt.Text;
            Emp.WTBaseType = getRadioButtonList(WTBaseType);
            Emp.WTBaseTxt = WTBaseTxt.Text;
            Emp.WTMainType = getRadioButtonList(WTMainType);
        }
        db.SubmitChanges();
        ViewState["FPAID"] = fId;
        showPrjData();
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert(\"保存成功\");</script>");
        //tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    private void setCheckBoxList(string idStr, CheckBoxList cbl, string txtNameStr, TextBox txtName, string remarks)
    {
        if (idStr != "" && idStr != null)
        {
            for (int i = 0; i < idStr.Split(',').Length; i++) {//给CheckBoxList选中的复选框 赋值                  { 
                for (int j = 0; j < cbl.Items.Count; j++) 
                { 
                    if (idStr.Split(',')[i] == cbl.Items[j].Value) 
                    { 
                        cbl.Items[j].Selected = true; 
                    } 
                } 
            }
            if (remarks != "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), txtNameStr, "<script>show" + txtNameStr + "();</script>");
                txtName.Text = remarks;
            }
        }
    }
    private void setRadioButtonList(string idStr, RadioButtonList rbl, string txtNameStr, TextBox txtName, string remarks)
    {
        if (idStr != "")
        {
            for (int j = 0; j < rbl.Items.Count; j++)
            {
                if (idStr == rbl.Items[j].Value)
                {
                    rbl.Items[j].Selected = true;
                }
            }
            if (remarks != "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), txtNameStr, "<script>show" + txtNameStr + "();</script>");
                txtName.Text = remarks;
            }
        }
    }
    private string getCheckBoxList(CheckBoxList cbl)
    {
        string ids = "";
        for (int i = 0; i < cbl.Items.Count; i++){//读取CheckBoxList 选中的值,保存起来             { 
            if (cbl.Items[i].Selected) 
            { 
                ids += cbl.Items[i].Value + ","; 
            } 
        }
        return ids.Substring(0,ids.Length-1>0?ids.Length-1:0);
    }
    private string getRadioButtonList(RadioButtonList rbl)
    {
        return rbl.SelectedValue.ToString();
    } 
}