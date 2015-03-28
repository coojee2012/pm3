using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
public partial class Government_AppBHGD_AddBatch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnSave(object sender, EventArgs e)
    {
        var name =EConvert.ToString( txt_batchNumber.Text.Trim());
        var year = EConvert.ToString( ddl_year.SelectedValue);
        var state = EConvert.ToString(ddl_state.SelectedValue);
        EgovaDB db = new EgovaDB();
        var batch = new TC_BHGD_Batch()
        {
            FId = Guid.NewGuid().ToString(),
            FBatchNumber =  name,
            FState = state,
            FYear = year
        };
        db.TC_BHGD_Batch.InsertOnSubmit(batch);
        db.SubmitChanges();
        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
    }
}