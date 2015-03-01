using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.RuleApp;
using System.Text;
/// <summary>
/// 建造师业务公共方法
/// </summary>
public class CstCommon
{

    public CstCommon()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 新申请 
    /// </summary>
    /// <param name="pagename"></param>
    public static bool NewApply(Page page, int len, string fidcard, string birthday)
    {
        pageTool tool = new pageTool(page);
        bool retu = false;
        if (len == 18)
        {
            string cardage = fidcard.Substring(6, 8);
            string birthage = birthday.Substring(0, 4) + birthday.Substring(5, 2) + birthday.Substring(8, 2);
            if (birthage != cardage)
            {
                tool.showMessage("您输入的出生年日期和身份证上的不一致.");
                retu = false;
            }
            else
            {
                retu = true;
            }
        }
        if (len == 15)
        {
            string cardage = fidcard.Substring(6, 6);
            string birthage = birthday.Substring(2, 2) + birthday.Substring(5, 2) + birthday.Substring(8, 2);
            if (birthage != cardage)
            {
                tool.showMessage("您输入的出生年日期和身份证上的不一致.");
                retu = false;
            }
            else
            {
                retu = true;
            }
        }

        return retu;
        //    RCenter rc = new RCenter();
        //    RQuali rq = new RQuali();
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(" select count(*) from CF_Construct_UserInfo ");
        //    sb.Append(" where fbaseinfoid='" + HttpContext.Current.Session["FBaseId"].ToString() + "'");
        //    sb.Append(" and fstate=2 ");
        //    sb.Append(" and fpersontypeid=23 ");
        //    int iCount = rc.GetSQLCount(sb.ToString());


        //    int iCount1 = 0;
        //    sb.Remove(0, sb.Length);
        //    sb.Append(" select FCount1 from cf_sys_user where fbaseinfoid='" + HttpContext.Current.Session["FBaseId"].ToString() + "'");
        //    try
        //    {
        //        iCount1 = EConvert.ToInt(rc.GetSignValue(sb.ToString()));
        //    }
        //    catch
        //    {
        //        iCount1 = 0;
        //    }

        //    pageTool tool = new pageTool(page);
        //    if (iCount >= iCount1 && (FManageTypeId == "200" || FManageTypeId == "206"))
        //    {

        //        tool.showMessage("建造师已达到最大许可数量,不能再注册");
        //        return;
        //    }


        //    sb.Remove(0, sb.Length);
        //    sb.Append(" select count(*) from CF_App_ProcessInstance where FBaseInfoId='" + HttpContext.Current.Session["FBaseId"].ToString() + "'");
        //    sb.Append(" and FManageTypeId='207' and  FIsDeleted='0' and fstate=6 ");
        //    if (rc.GetSQLCount(sb.ToString()) > 0)
        //    {

        //        tool.showMessage("必须办理完企业名称变更才能办理此业务");
        //        return;
        //    }
        //    else
        //    {//FirstAppPage.aspx
        //        string sUrl = "" + pagename + "?FManageTypeId=" + FManageTypeId;
        //        HttpContext.Current.Response.Redirect(sUrl);
        //    }

    }

    public static string GetState(string FSpecialNo, string FEmpId, string FSpecialTypeId, string FIdCard)
    {
        string s = "";
        RCenter rc = new RCenter();
        string sql = " select FSpecialtyId  from CF_Emp_Ini where FCertiNo='" + FSpecialNo + "' ";
        if (!string.IsNullOrEmpty(FSpecialNo))
        {
            DataTable dt = rc.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                if (EConvert.ToString(dt.Rows[0]["FSpecialtyId"]) != FSpecialTypeId)
                {

                    s = "在资质证书库中有资格证号但与注册的专业不相符，";
                }
            }
            else
            {
                s = "在资质证书库没有找到注册的专业。";
            }
        }
        sql = " select * from CF_Construct_UserInfo where FId<>'" + FEmpId + "' and FIdCard='" + FIdCard + "' and Fstate=2 ";
        DataTable t = rc.GetTable(sql);
        for (int i = 0; t.Rows.Count < 0; i++)
        {
            s = s + string.Format("\n {0} {1} {2}", t.Rows[i]["FName"], t.Rows[i]["FIdCard"], t.Rows[i]["FBaseName"]);
        }
        return s;
    }

    public static DataTable Join(DataTable First, DataTable Second, DataColumn[] FJC, DataColumn[] SJC)
    {
        //创建一个新的DataTable
        DataTable table = new DataTable("Join");
        // Use a DataSet to leverage DataRelation
        using (DataSet ds = new DataSet())
        {
            //把DataTable Copy到DataSet中

            ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });

            DataColumn[] parentcolumns = new DataColumn[FJC.Length];

            for (int i = 0; i < parentcolumns.Length; i++)
            {
                parentcolumns[i] = ds.Tables[0].Columns[FJC[i].ColumnName];
            }
            DataColumn[] childcolumns = new DataColumn[SJC.Length];
            for (int i = 0; i < childcolumns.Length; i++)
            {
                childcolumns[i] = ds.Tables[1].Columns[SJC[i].ColumnName];
            }

            //创建关联
            DataRelation r = new DataRelation(string.Empty, parentcolumns, childcolumns, false);
            ds.Relations.Add(r);

            //为关联表创建列
            for (int i = 0; i < First.Columns.Count; i++)
            {
                table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);
            }
            for (int i = 0; i < Second.Columns.Count; i++)
            {
                //看看有没有重复的列，如果有在第二个DataTable的Column的列明后加_Second
                if (!table.Columns.Contains(Second.Columns[i].ColumnName))
                    table.Columns.Add(Second.Columns[i].ColumnName, Second.Columns[i].DataType);
                else
                    table.Columns.Add(Second.Columns[i].ColumnName + "_Second", Second.Columns[i].DataType);
            }
            table.BeginLoadData();
            foreach (DataRow firstrow in ds.Tables[0].Rows)
            {
                //得到行的数据
                DataRow[] childrows = firstrow.GetChildRows(r);
                if (childrows != null && childrows.Length > 0)
                {
                    object[] parentarray = firstrow.ItemArray;
                    foreach (DataRow secondrow in childrows)
                    {
                        object[] secondarray = secondrow.ItemArray;
                        object[] joinarray = new object[parentarray.Length + secondarray.Length];
                        Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                        Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);
                        table.LoadDataRow(joinarray, true);
                    }
                }
            }
            table.EndLoadData();
        }
        return table;
    }
    public static DataTable Join(DataTable First, DataTable Second, DataColumn FJC, DataColumn SJC)
    {
        return Join(First, Second, new DataColumn[] { FJC }, new DataColumn[] { SJC });
    }
    public static DataTable Join(DataTable First, DataTable Second, string FJC, string SJC)
    {
        return Join(First, Second, new DataColumn[] { First.Columns[FJC] }, new DataColumn[] { Second.Columns[SJC] });
    }
    public static DataTable LeftJoin(DataTable left, DataTable right,
            DataColumn[] leftCols, DataColumn[] rightCols,
            bool includeLeftJoin, bool includeRightJoin)
    {
        DataTable result = new DataTable("JoinResult");
        using (DataSet ds = new DataSet())
        {
            ds.Tables.AddRange(new DataTable[] { left.Copy(), right.Copy() });
            DataColumn[] leftRelationCols = new DataColumn[leftCols.Length];
            for (int i = 0; i < leftCols.Length; i++)
                leftRelationCols[i] = ds.Tables[0].Columns[leftCols[i].ColumnName];

            DataColumn[] rightRelationCols = new DataColumn[rightCols.Length];
            for (int i = 0; i < rightCols.Length; i++)
                rightRelationCols[i] = ds.Tables[1].Columns[rightCols[i].ColumnName];

            //create result columns
            for (int i = 0; i < left.Columns.Count; i++)
                result.Columns.Add(left.Columns[i].ColumnName, left.Columns[i].DataType);
            for (int i = 0; i < right.Columns.Count; i++)
            {
                string colName = right.Columns[i].ColumnName;
                while (result.Columns.Contains(colName))
                    colName += "_2";
                result.Columns.Add(colName, right.Columns[i].DataType);
            }

            //add left join relations
            DataRelation drLeftJoin = new DataRelation("rLeft", leftRelationCols, rightRelationCols, false);
            ds.Relations.Add(drLeftJoin);

            //join
            result.BeginLoadData();
            foreach (DataRow parentRow in ds.Tables[0].Rows)
            {
                DataRow[] childrenRowList = parentRow.GetChildRows(drLeftJoin);
                if (childrenRowList != null && childrenRowList.Length > 0)
                {
                    object[] parentArray = parentRow.ItemArray;
                    foreach (DataRow childRow in childrenRowList)
                    {
                        object[] childArray = childRow.ItemArray;
                        object[] joinArray = new object[parentArray.Length + childArray.Length];
                        Array.Copy(parentArray, 0, joinArray, 0, parentArray.Length);
                        Array.Copy(childArray, 0, joinArray, parentArray.Length, childArray.Length);
                        result.LoadDataRow(joinArray, true);
                    }
                }
                else //left join
                {
                    if (includeLeftJoin)
                    {
                        object[] parentArray = parentRow.ItemArray;
                        object[] joinArray = new object[parentArray.Length];
                        Array.Copy(parentArray, 0, joinArray, 0, parentArray.Length);
                        result.LoadDataRow(joinArray, true);
                    }
                }
            }

            if (includeRightJoin)
            {
                //add right join relations
                DataRelation drRightJoin = new DataRelation("rRight", rightRelationCols, leftRelationCols, false);
                ds.Relations.Add(drRightJoin);

                foreach (DataRow parentRow in ds.Tables[1].Rows)
                {
                    DataRow[] childrenRowList = parentRow.GetChildRows(drRightJoin);
                    if (childrenRowList == null || childrenRowList.Length == 0)
                    {
                        object[] parentArray = parentRow.ItemArray;
                        object[] joinArray = new object[result.Columns.Count];
                        Array.Copy(parentArray, 0, joinArray,
                            joinArray.Length - parentArray.Length, parentArray.Length);
                        result.LoadDataRow(joinArray, true);
                    }
                }
            }

            result.EndLoadData();
        }

        return result;
    }
    public static DataTable LeftJoin(DataTable First, DataTable Second, string FJC, string SJC)
    {
        return LeftJoin(First, Second, new DataColumn[] { First.Columns[FJC] }, new DataColumn[] { Second.Columns[SJC] }, true, false);
    }
}
