﻿using Approve.Common;
using Approve.RuleApp;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppTFGGL_RWSDList : govBasePage//System.Web.UI.Page//
{
    //RCenter rc = new RCenter();
    //SaveAsBase sab = new SaveAsBase();
    //RApp ra = new RApp();
    //RAppBacth rap = new RAppBacth();
    //ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.Page_Load(sender, e);
            ShowInfo();
        }
       
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("select  *  from  V_Sgxkz_EmpLock where isMainLock=1 and isLock = 1 ");  //只查主锁定的人员
        //只能查看本地区的项目锁定人员
        string addCondi = getCondi();

        if (addCondi == "")  //如果有附加条件，则根据条件查询，不限定本地区的，保证主管部门可以查看到人员在其他地区的锁定情况，否则，则只查询本地区项目的人员情况。  by zyd
        {
            string strdf = Session["DFId"].ToString();
            if (strdf == "51" || strdf == "510000")
            {
                sb.Append(" and PrjAddressDept like  '51%'");
            }
            else
            {
               sb.Append(" and PrjAddressDept = '" + Session["DFId"] + "' ");
            }
        }
        else
        {
            sb.Append(addCondi); 
        }   

        this.Pager1.className = "dbCenter";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }


    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtXM.Text.Trim() != "" && this.txtXM.Text.Trim() != null)
        {
            sb.Append(" and FHumanName like '%" + this.txtXM.Text.Trim() + "%' ");
        }
        if (this.txtIDCard.Text.Trim() != "" && this.txtIDCard.Text.Trim() != null)
        {
            sb.Append(" and FIdCard like '%" + this.txtIDCard.Text.Trim() + "%' ");
        }
        if (this.txtSGXKZBH.Text.Trim() != "" && this.txtSGXKZBH.Text.Trim() != null)
        {
            sb.Append(" and ZSBH like '%" + this.txtSGXKZBH.Text.Trim() + "%' ");
        }
        if (this.txtQYMC.Text.Trim() != "" && this.txtQYMC.Text.Trim() != null)
        {
            sb.Append(" and FEntName like '%" + this.txtQYMC.Text.Trim() + "%' ");
        }
        //if (this.txtFBZT.SelectedValue != null && this.txtFBZT.SelectedValue != "-1")
        //{
        //    //sb.Append(" and b.ZSBH =  " + this.txtSGXKZBH.SelectedValue);
        //}
        if (this.tbcyxm.Text != "" && this.tbcyxm.Text.Trim() != null)
        {
            sb.Append(" and ProjectName like '%" + this.tbcyxm.Text.Trim() + "%' ");
        }
        

        if (sb.Length > 0)
        {
            return sb.ToString();
        }
        else
        {
            return "";
        }
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "Fid"));
            string idCard = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FIdCard"));

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["fId"] = fId;
            box.Attributes["fAppId"] = fAppId;
            box.Attributes["name"] = fAppId;

            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[4].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('JCSD.aspx?idCard=" + idCard + "&FAppId=" + fAppId + "',1024,600);\">" + e.Item.Cells[4].Text + "</a>";
            e.Item.Cells[7].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('../AppKJGGL/SGXKZXX.aspx?FAppId=" + fAppId + "',1024,600);\">" + e.Item.Cells[7].Text + "</a>";
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}