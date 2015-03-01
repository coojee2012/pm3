using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;

public partial class WYDW_ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string returnData = "";
        string action = Request.QueryString["action"];

        //####################Ajax方法###################
        //if (action == "前台action参数")
        //{
        //    string XMMC = Request.QueryString["前台GET的参数"];
        //    returnData = 自定义方法fun(XMMC);
        //}
        //####################Ajax方法###################


        if (action == "CheckXMMC")
        {
            string XMMC = Request.QueryString["XMMC"];
            returnData = checkXMMC(XMMC);
        }
        Response.Write(returnData);
    }

    private string checkXMMC(string xmmc)
    {
        //EgovaDB dbContext = new EgovaDB();
        if (!string.IsNullOrEmpty(xmmc))
        {
            if (xmmc == "1")
            {
                return "该项目已经存在！";
            }
            return "0";
        }
        return "不能为空！";
    }
}