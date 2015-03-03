using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Xml;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Admin_main_UserQXFB : System.Web.UI.Page
{
    public string xml;
    public string sHTML;
    public DataTable SystemDT = new DataTable();//子系统
    public DataTable GroupDT = new DataTable();//分组
    public DataTable GroupBarDT = new DataTable();//导航栏
    public DataTable NavagateDT = new DataTable();//导航条
    public DataTable GldyDT = new DataTable();//权限
    public DataTable GldyQXDT = new DataTable();//权限细项
    public DataTable GldyQXFP = new DataTable();//权限分配
    public XmlDocument xmlDoc = new XmlDocument();//xml文件
    public XmlNodeList xlSys = null;//子系统节点
    private List<string> QXIDList = new List<string>();  //权限CheckBox IDs
    private string UserID = "";  //用户ID
    private List<string> TreeViewIds = new List<string>(); //树控件IDs
    private List<string> CheckIDs = new List<string>(); //子系统CheckBox IDs


    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!Page.IsPostBack)
        //{
        UserID = Request["PERID"] == null ? "123" : Request["PERID"].ToString();
        string sql = string.Format("exec spNJS_GetQXInfo '{0}'", UserID);
        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds, "SystemInfo");
                SystemDT = ds.Tables[0];
                GroupDT = ds.Tables[1];
                GroupBarDT = ds.Tables[2];
                NavagateDT = ds.Tables[3];
                GldyDT = ds.Tables[4];
                GldyQXDT = ds.Tables[5];
                GldyQXFP = ds.Tables[6];
                SysCount.Value = SystemDT.Rows.Count.ToString();
                xml = GetQXInfo(ds);
                ChangeXML(xmlDoc.GetElementsByTagName("System"));
                xlSys = xmlDoc.GetElementsByTagName("System");
                CreateHTML();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //}
    }

    //生成xml
    private string GetQXInfo(DataSet ds)
    {
        xmlDoc = new XmlDocument();
        XmlElement QX = xmlDoc.CreateElement("QX");
        xmlDoc.AppendChild(QX);
        for (int i = 0; i < SystemDT.Rows.Count; i++)
        {
            XmlElement System = xmlDoc.CreateElement("System");
            QX.AppendChild(System);
            System.SetAttribute("ID", "S_" + SystemDT.Rows[i]["id"].ToString());
            System.SetAttribute("Caption", SystemDT.Rows[i]["systemname"].ToString());
            System.SetAttribute("Check", SystemDT.Rows[i]["ischeck"].ToString());

            for (int j = 0; j < GroupDT.Rows.Count; j++)
            {
                if (GroupDT.Rows[j]["id"].ToString() != SystemDT.Rows[i]["id"].ToString()) continue;
                XmlElement Group = xmlDoc.CreateElement("Group");
                System.AppendChild(Group);
                Group.SetAttribute("ID", "G_" + SystemDT.Rows[i]["id"].ToString() + "_" + GroupDT.Rows[j]["groupid"].ToString());
                Group.SetAttribute("Caption", GroupDT.Rows[j]["groupname"].ToString());
                Group.SetAttribute("Check", GroupDT.Rows[j]["ischeck"].ToString());
                XmlElement Bar = xmlDoc.CreateElement("Bar");//树部分xml
                Group.AppendChild(Bar);
                for (int k = 0; k < GroupBarDT.Rows.Count; k++)
                {
                    if (GroupBarDT.Rows[k]["groupid"].ToString() != GroupDT.Rows[j]["groupid"].ToString()) continue;
                    XmlElement BarList = xmlDoc.CreateElement("BarList");
                    Bar.AppendChild(BarList);
                    BarList.SetAttribute("ID", "B_" + SystemDT.Rows[i]["id"].ToString() + "_" + GroupDT.Rows[j]["groupid"].ToString() + "_" + GroupBarDT.Rows[k]["id"].ToString());
                    BarList.SetAttribute("Caption", GroupBarDT.Rows[k]["caption"].ToString());
                    BarList.SetAttribute("Check", GroupBarDT.Rows[k]["ischeck"].ToString());

                    for (int l = 0; l < NavagateDT.Rows.Count; l++)
                    {
                        if (GroupBarDT.Rows[k]["id"].ToString() != NavagateDT.Rows[l]["barid"].ToString()) continue;
                        XmlElement Nav = xmlDoc.CreateElement("Nav");
                        BarList.AppendChild(Nav);
                        Nav.SetAttribute("ID", "N_" + SystemDT.Rows[i]["id"].ToString() + "_" + GroupDT.Rows[j]["groupid"].ToString() + "_" + GroupBarDT.Rows[k]["id"].ToString() + "_" + NavagateDT.Rows[l]["navid"].ToString());
                        Nav.SetAttribute("Caption", NavagateDT.Rows[l]["NavText"].ToString());
                        Nav.SetAttribute("Check", NavagateDT.Rows[l]["ischeck"].ToString());
                    }
                }
                //功能权限xml
                XmlElement Fun = xmlDoc.CreateElement("Func");//树部分xml
                Group.AppendChild(Fun);
                for (int a = 0; a < GldyDT.Rows.Count; a++)
                {
                    if (GldyDT.Rows[a]["groupid"].ToString() != GroupDT.Rows[j]["groupid"].ToString()) continue;
                    XmlElement FuncList = xmlDoc.CreateElement("FuncList");
                    Fun.AppendChild(FuncList);
                    FuncList.SetAttribute("ID", "F_" + SystemDT.Rows[i]["id"].ToString() + "_" + GroupDT.Rows[j]["groupid"].ToString() + "_" + GldyDT.Rows[a]["classid"].ToString());
                    FuncList.SetAttribute("Caption", GldyDT.Rows[a]["flmc"].ToString());
                    FuncList.SetAttribute("Check", GldyDT.Rows[a]["ischeck"].ToString());

                    for (int b = 0; b < GldyQXDT.Rows.Count; b++)
                    {
                        if (GldyDT.Rows[a]["groupid"].ToString() != GldyQXDT.Rows[b]["groupid"].ToString()) continue;
                        XmlElement FuncItemsList = xmlDoc.CreateElement("Item");
                        FuncList.AppendChild(FuncItemsList);
                        FuncItemsList.SetAttribute("ID", "I_" + SystemDT.Rows[i]["id"].ToString() + "_" + GroupDT.Rows[j]["groupid"].ToString() + "_" + GldyDT.Rows[a]["classid"].ToString() + "_" + GldyQXDT.Rows[b]["id"].ToString());
                        FuncItemsList.SetAttribute("Caption", GldyQXDT.Rows[b]["qxgnmc"].ToString());
                        string ischeck = "0";
                        for (int c = 0; c < GldyQXFP.Rows.Count; c++)
                        {
                            string fpgroupid = GldyQXFP.Rows[c]["systemid"].ToString();
                            string fpclassid = GldyQXFP.Rows[c]["classid"].ToString();
                            string fpfunid = GldyQXFP.Rows[c]["funid"].ToString();
                            if (fpgroupid == GroupDT.Rows[j]["groupid"].ToString() && fpclassid == GldyDT.Rows[a]["classid"].ToString() && fpfunid == GldyQXDT.Rows[b]["id"].ToString())
                            {
                                ischeck = "1";
                                break;
                            }
                        }
                        FuncItemsList.SetAttribute("Check", ischeck);
                    }
                }

            }
        }

        return xmlDoc.OuterXml;
    }

    //改变xml追加子菜单
    private void ChangeXML(XmlNodeList xnList)
    {
        xnList = xmlDoc.GetElementsByTagName("BarList");
        string ID = "";
        if (xnList == null) return;
        foreach (XmlNode xn in xnList)
        {
            XmlElement xe = (XmlElement)xn;
            if (xe.Attributes["Caption"] != null)
            {
                string barName = xe.Attributes["Caption"].Value.ToString();
                if (barName.Length > 5)
                {
                    if (barName.Substring(0, 4) == "子菜单：")
                    {
                        XmlNodeList xnList0 = xmlDoc.SelectNodes(".//Nav[@Caption='" + barName.Substring(4) + "']");
                        foreach (XmlNode xn0 in xnList0)
                        {
                            string xn0ID = xn0.Attributes["ID"].Value.ToString();
                            string xeID = xe.Attributes["ID"].Value.ToString();
                            if (xn0ID.Substring(2, 2) == xeID.Substring(2, 2))
                                ChangeXML_addNode(xn0, xe.ChildNodes);
                        }
                        ID += xe.Attributes["ID"].Value.ToString() + ',';
                    }
                }
            }

        }
        //删除含有"子菜单："的节点
        string[] IDs = ID.Split(',');
        foreach (string s in IDs)
        {
            if (s == "") continue;
            XmlNode xmlDel = xmlDoc.SelectSingleNode(".//BarList[@ID='" + s + "']");
            xmlDel.ParentNode.RemoveChild(xmlDel);
        }
    }

    //递归
    private void ChangeXML_addNode(XmlNode xn0, XmlNodeList xnList)
    {
        if (xnList == null) return;
        foreach (XmlNode xn1 in xnList)
        {
            string xID = xn1.Attributes["ID"].Value;
            XmlElement x = xmlDoc.CreateElement("Nav");//(XmlElement)xmlDoc.SelectNodes(".//Nav[@ID='" + xID + "']")[0];
            x.SetAttribute("ID", xn1.Attributes["ID"].Value);
            x.SetAttribute("Caption", xn1.Attributes["Caption"].Value);
            x.SetAttribute("Check", xn1.Attributes["Check"].Value);
            xn0.AppendChild(x);
            ChangeXML_addNode(x, xn1.ChildNodes);
        }
    }

    //生成树
    private void CreateTree(XmlNodeList sXml, TreeView TV)
    {
        if (sXml == null) return;
        if (TV == null) return;
        TV.ShowCheckBoxes = TreeNodeTypes.All;
        foreach (XmlNode xn in sXml)
        {
            if (xn.Attributes["Caption"] != null)
            {
                TreeNode tN = new TreeNode();
                TV.Nodes.Add(tN);
                tN.Text = xn.Attributes["Caption"].Value;
                tN.Value = xn.Attributes["ID"].Value;
                //tN.
                if (xn.Attributes["Check"].Value == "1")
                    tN.Checked = true;
                else
                    tN.Checked = false;
                tN.NavigateUrl = "javascript:fnNo(this);";
                AddNodes(xn.ChildNodes, tN);
                //TV.CheckedNodes       
                //TV.SelectedNodeChanged
            }
        }
    }

    private void AddNodes(XmlNodeList sXml, TreeNode tN)
    {
        foreach (XmlNode xn in sXml)
        {
            if (xn.Attributes["Caption"] != null)
            {
                TreeNode tN1 = new TreeNode();
                //tN1.SelectAction = TreeNodeSelectAction.Select;
                //tN1.
                tN.ChildNodes.Add(tN1);
                tN1.Text = xn.Attributes["Caption"].Value;
                tN1.Value = xn.Attributes["ID"].Value;
                if (xn.Attributes["Check"].Value == "1")
                    tN1.Checked = true;
                else
                    tN1.Checked = false;
                tN1.NavigateUrl = "javascript:fnNo(this);";
                AddNodes(xn.ChildNodes, tN1);

            }
        }
    }

    //生成页面内容
    private void CreateHTML()
    {
        TreeViewIds = new List<string>();
        CheckIDs = new List<string>();
        QXIDList = new List<string>();
        //系统头
        HtmlTable HTable_SYS = new HtmlTable();
        HTable_SYS.CellPadding = 0;
        HTable_SYS.CellSpacing = 0;
        HTable_SYS.Border = 0;
        HTable_SYS.Attributes.Add("class", "table2");
        HTable_SYS.Attributes.Add("style", "height:100%; width:100%; text-align:center; border-bottom:#B8D1EF solid 1px; ");
        HtmlTableRow tR_SYS = new HtmlTableRow();
        HTable_SYS.Rows.Add(tR_SYS);

        for (int i = 0; i < xlSys.Count; i++)
        {
            HtmlTableCell tc_SYS = new HtmlTableCell();
            tR_SYS.Cells.Add(tc_SYS);

            string calssN = "rb bgSelect";
            string tdID = "td_" + i;
            string cID = "c_" + xlSys[i].Attributes["ID"].Value;
            CheckIDs.Add(cID);
            if (i != 0) { calssN = "rb bg2"; }
            if (i == xlSys.Count - 1) calssN = "b bg2";

            tc_SYS.ID = tdID;
            tc_SYS.Attributes.Add("calss", calssN);
            tc_SYS.Attributes.Add("onclick", "fnShow(this)");
            tc_SYS.Attributes.Add("onmouseover", "fnMouseOver(this)");
            CheckBox cb = new CheckBox();
            cb.ID = cID;
            //cb.Text = xlSys[i].Attributes["Caption"].Value;
            Label la = new Label();
            la.Text = xlSys[i].Attributes["Caption"].Value;
            tc_SYS.Controls.Add(cb);
            tc_SYS.Controls.Add(la);
            cb.Attributes.Add("onclick", "fnCheck(this)");
            if (xlSys[i].Attributes["Check"].Value == "1")
                cb.Checked = true;
            else
                cb.Checked = false;
        }

        TDHead.Controls.Add(HTable_SYS);

        //各系统对应分组和导航栏权限
        int iTVID = 0;
        for (int i = 0; i < xlSys.Count; i++)
        {
            XmlNodeList xlGroup = xlSys[i].ChildNodes;
            if (xlGroup.Count < 1) return;
            string divID = "DIV_" + i;
            string divClassN = "divStyle2";
            if (i != 0) divClassN = "divStyle";
            HtmlTable HTable_DIV = new HtmlTable();
            HTable_DIV.Attributes.Add("class", divClassN);
            HTable_DIV.ID = divID;
            HTable_DIV.CellPadding = 0;
            HTable_DIV.CellSpacing = 0;
            HTable_DIV.Border = 0;
            HTable_DIV.Width = "800px";
            HtmlTableRow tR_DIV = new HtmlTableRow();
            HTable_DIV.Rows.Add(tR_DIV);
            HtmlTableCell tc_DIV = new HtmlTableCell();
            tR_DIV.Cells.Add(tc_DIV);

            HtmlTable HTable = new HtmlTable();
            tc_DIV.Controls.Add(HTable);
            HTable.CellPadding = 0;
            HTable.CellSpacing = 0;
            HTable.Border = 0;
            HTable.Attributes.Add("class", "table3");
            HTable.Attributes.Add("style", "height:100%; width:100%; text-align:center;");

            for (int j = 0; j < xlGroup.Count; j++)
            {
                HtmlTableRow tR = new HtmlTableRow();
                HTable.Rows.Add(tR);
                HtmlTableCell tc = new HtmlTableCell();
                tR.Cells.Add(tc);
                tc.Attributes.Add("class", "rb tdleft");
                tc.Attributes.Add("style", "text-align:center;");
                tc.InnerText = xlGroup[j].Attributes["Caption"].Value;
                HtmlTableCell tc2 = new HtmlTableCell();
                tR.Cells.Add(tc2);
                tc2.Attributes.Add("class", "b");

                XmlNodeList xmlG = xlGroup[j].ChildNodes[0].ChildNodes;
                if (xmlG.Count < 1) continue;
                HtmlTable HTable_Bar = new HtmlTable(); //右边
                tc2.Controls.Add(HTable_Bar);
                HTable_Bar.CellPadding = 0;
                HTable_Bar.CellSpacing = 0;
                HTable_Bar.Border = 0;
                HTable_Bar.Attributes.Add("class", "table3");
                HTable_Bar.Attributes.Add("style", "height:100%; width:100%; text-align:center;");
                HtmlTableRow tR_Bar = new HtmlTableRow();
                HTable_Bar.Rows.Add(tR_Bar);
                HtmlTableCell tc_Bar = new HtmlTableCell();
                tR_Bar.Cells.Add(tc_Bar);
                tc_Bar.Attributes.Add("class", "rb");
                tc_Bar.Attributes.Add("style", "text-align:center; font-weight:bold; height:30px;");
                tc_Bar.InnerText = "导航栏权限";

                HtmlTableRow tR_Bar2 = new HtmlTableRow();
                HTable_Bar.Rows.Add(tR_Bar2);
                HtmlTableCell tc_Bar2 = new HtmlTableCell();
                tR_Bar2.Cells.Add(tc_Bar2);
                tc_Bar2.Attributes.Add("class", "b");
                tc_Bar2.Attributes.Add("style", "height:100%; width:100%; text-align:left;");
                TreeView tv = new TreeView();
                string TreeViewID = "TreeView_" +iTVID;
                TreeViewIds.Add(TreeViewID);
                iTVID++;
                tv.ID = TreeViewID;
                //CreateTree(xlGroup[j] == null ? "" : xlGroup[j].InnerXml, tv);
                CreateTree(xmlG, tv);
                tc_Bar2.Controls.Add(tv);
                tv.Attributes.Add("onClick", "OnCheckEvent('" + xlSys[i].Attributes["ID"].Value + "',this)");

                XmlNodeList xlFun = xlGroup[j].ChildNodes[1].ChildNodes;
                if (xlFun.Count < 1) continue;
                HtmlTableRow tR_Bar3 = new HtmlTableRow();
                HTable_Bar.Rows.Add(tR_Bar3);
                HtmlTableCell tc_Bar3 = new HtmlTableCell();
                tR_Bar3.Cells.Add(tc_Bar3);
                tc_Bar3.Width = "21%";
                tc_Bar3.Attributes.Add("style", "text-align:center; font-weight:bold; height:30px;");
                tc_Bar3.Attributes.Add("class", "rb");
                tc_Bar3.InnerText = "功能权限";

                HtmlTableRow tR_Bar4 = new HtmlTableRow();
                HTable_Bar.Rows.Add(tR_Bar4);
                HtmlTableCell tc_Bar4 = new HtmlTableCell();
                tR_Bar4.Cells.Add(tc_Bar4);
                tc_Bar4.Width = "79%";
                tc_Bar4.Attributes.Add("class", "b");
                tc_Bar4.Attributes.Add("style", "text-align:left;");

                //功能权限
                HtmlTable ht_Fun = new HtmlTable();
                tc_Bar4.Controls.Add(ht_Fun);
                ht_Fun.CellPadding = 0;
                ht_Fun.CellSpacing = 0;
                ht_Fun.Width = "100%";
                ht_Fun.Border = 0;

                for (int b = 0; b < xlFun.Count; b++)
                {
                    HtmlTableRow tr_Fun = new HtmlTableRow();
                    ht_Fun.Rows.Add(tr_Fun);
                    HtmlTableCell tc_Fun1 = new HtmlTableCell();
                    tc_Fun1.InnerText = xlFun[b].Attributes["Caption"] == null ? "" : xlFun[b].Attributes["Caption"].Value.ToString();
                    tr_Fun.Cells.Add(tc_Fun1);
                    tc_Fun1.Attributes.Add("style", "text-align:left;padding-left:10px; width:17%; color:#663300");
                    HtmlTableCell tc_Fun2 = new HtmlTableCell();
                    tr_Fun.Cells.Add(tc_Fun2);
                    tc_Fun2.Attributes.Add("style", "padding-left:10px; text-align:left; width:83%;");

                    XmlNodeList xlFunItems = xlFun[b].ChildNodes;
                    for (int c = 0; c < xlFunItems.Count; c++)
                    {
                        CheckBox cboFun = new CheckBox();
                        cboFun.ID = xlFunItems[c].Attributes["ID"] == null ? "" : "i_" + xlFunItems[c].Attributes["ID"].Value.ToString();
                        QXIDList.Add(cboFun.ID);
                        cboFun.Text = xlFunItems[c].Attributes["Caption"] == null ? "" : xlFunItems[c].Attributes["Caption"].Value.ToString() + "&nbsp;&nbsp;&nbsp;";
                        tc_Fun2.Controls.Add(cboFun);
                        if (xlFunItems[c].Attributes["Check"].Value.ToString() == "1")
                            cboFun.Checked = true;
                        else
                            cboFun.Checked = false;
                    }
                }

            }
            TD.Controls.Add(HTable_DIV);
        }

    }

    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (TreeViewIds == null) return;

        foreach (string s in TreeViewIds)//遍历每棵树
        {
            if (s == "") continue;
            TreeView o = (TreeView)Page.FindControl(s);
            LLTreeNode(o.Nodes);          //遍历树上每个节点  
        }

        //子系统
        IsCheck(CheckIDs, "System");

        //权限
        IsCheck(QXIDList, "Item");

        //遍历xml写入库
        // XmlNodeList xmll=xmlDoc.SelectNodes(".//Nav[@Check='1']");
        //保存之前删除该用户之前的数据
        BefareSave();
        DoXMlAndSave(xmlDoc.GetElementsByTagName("System"));
    }

    private void LLTreeNode(TreeNodeCollection Nodes)
    {
        XmlNodeList xmlList = null;
        foreach (TreeNode tN in Nodes) //遍历每棵树选中的节点o.CheckedNodes
        {
            string tNID = tN.Value;
            string sl = tNID.Substring(0, 2);
            string id = tNID.Substring(2);

            switch (sl)
            {
                case "S_":
                    xmlList = xmlDoc.GetElementsByTagName("System");
                    break;
                case "G_":
                    xmlList = xmlDoc.GetElementsByTagName("Group");
                    break;
                case "B_":
                    xmlList = xmlDoc.GetElementsByTagName("BarList");
                    break;
                case "N_":
                    xmlList = xmlDoc.GetElementsByTagName("Nav");
                    break;
                default:
                    break;
            }
            foreach (XmlNode xn in xmlList)//遍历xml修改check
            {
                XmlElement xe = (XmlElement)xn;
                if ((xn.Attributes["ID"] == null ? "" : xn.Attributes["ID"].Value) == tNID)
                {
                    if (tN.Checked)
                        xe.SetAttribute("Check", "1");
                    else
                        xe.SetAttribute("Check", "0");
                    break;
                }
            }
            LLTreeNode(tN.ChildNodes);
        }
    }

    //保存之前删除该用户之前的数据
    private void BefareSave()
    {
        string sql = string.Format("exec spNJS_BefareSaveQXFP '{0}'", UserID);
        try
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //判断子系统和权限的复选框是否被选中
    private void IsCheck(List<string> IDList, string xmlTag)
    {
        foreach (string str in IDList)
        {
            CheckBox cbo = (CheckBox)Page.FindControl(str);
            XmlNodeList xmlList = xmlDoc.GetElementsByTagName(xmlTag);
            foreach (XmlNode xn in xmlList)//遍历xml修改check
            {
                XmlElement xe = (XmlElement)xn;
                if (cbo.ID.Substring(2) == xe.Attributes["ID"].Value)
                {
                    if (cbo.Checked)
                        xe.SetAttribute("Check", "1");
                    else
                        xe.SetAttribute("Check", "0");
                    break;
                }
            }
        }
    }


    //遍历xml写库
    private void DoXMlAndSave(XmlNodeList xnList)
    {
        if (xnList == null) return;
        foreach (XmlNode xn in xnList)
        {
            if ((xn.Attributes["Check"] == null ? "" : xn.Attributes["Check"].Value) == "1")
            {
                string tNID = xn.Attributes["ID"].Value;
                string sl = tNID.Substring(0, 2);
                string sql = "";

                switch (sl)
                {
                    case "S_":
                        string id = tNID.Substring(2);
                        sql = string.Format("exec spNJS_SaveQXFP {0},'{1}',1", id, UserID);
                        break;
                    case "I_":
                        string[] iid = tNID.Split('_');
                        sql = string.Format("exec spNJS_XM_QXFP {0},{1},{2},'{3}' ", iid[2], iid[3], iid[4], UserID);
                        break;
                    case "B_":
                        string[] bid = tNID.Split('_');
                        sql = string.Format("exec spNJS_SaveQXFP {0},'{1}',2", bid[3], UserID);
                        break;
                    case "N_":
                        string[] nid = tNID.Split('_');
                        sql = string.Format("exec spNJS_SaveQXFP {0},'{1}',3", nid[4], UserID);
                        break;
                    default:
                        break;
                }
                try
                {
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            DoXMlAndSave(xn.ChildNodes);
        }

    }


}
