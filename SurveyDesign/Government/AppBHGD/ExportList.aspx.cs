using EgovaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppBHGD_SHDCList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) {
            BindDdl();
        }
    }


    protected void BtnQuery(object sender, EventArgs e)
    {

    }

    private void BindDdl() {
        EgovaDB db = new EgovaDB();

        //年份
        var yearlist = from q in db.TC_BHGD_Batch
                       group q by q.FYear into p
                       select new { FYear = p.Key };

        ddlYear.DataTextField = "FYear";
        ddlYear.DataValueField = "FYear";
        ddlYear.DataSource = yearlist;
        ddlYear.DataBind();


        var batchlist = db.TC_BHGD_Batch.DefaultIfEmpty();


        ddl_Batch.DataTextField = "FBatchNumber";
        ddl_Batch.DataValueField = "FYear";
        ddl_Batch.DataSource = batchlist;
        ddl_Batch.DataBind();

        ddl_Batch.Items.Insert(0, new ListItem() { Text = "全部", Value = "", Selected = true });

    }
}