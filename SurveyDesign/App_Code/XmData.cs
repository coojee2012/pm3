using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NJSWebApp
{
    public static class XmData
    {
        //首页显示的列表
        public static DataTable GetHomeXmInfo()
        {
            string sSql = "select FNAME,XMCODE,GCMC,JSDW,JSDD,JDMC,XMBM,FBSJ,FBBM from v_web_HomeXM ";

            try
            {
                DataSet ds = new DataSet();
                SQLServerDALHelper.ExecuteSQLDataSet(ref ds, "v_web_HomeXM", sSql, SqlConType.Work);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
               return new DataTable();
            }
        }

        public static DataTable GetXMList(string GLDW)
        {
            string sSql = "select top 10 FNAME,XMCODE,GCMC,JSDW,JSDD,JDMC,XMBM from v_web_XMList ";
            if (GLDW != "") sSql += " where GLDW like '" + GLDW + "%' and ISNULL(gayz,0)=0 order by GLDW ";
            try
            {
                DataSet ds = new DataSet();
                SQLServerDALHelper.ExecuteSQLDataSet(ref ds, "v_web_HomeXM", sSql, SqlConType.Work);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
               return new DataTable();
            }
        }
        //项目定位地图
        public static DataTable GetXmInfo(string sXmCode)
        {
            try
            {
                string sSql = "select a.GCMC,a.JSDD,b.LONGT,b.LAT from XM_JBXX as a,XM_MapInfo as b where a.XMCODE=b.XMCODE and a.XMCODE='" + sXmCode + "'";
                DataSet ds = new DataSet();
                SQLServerDALHelper.ExecuteSQLDataSet(ref ds, "XM", sSql, SqlConType.Work);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                return new DataTable();
            }
        }

            //查询
        public static DataTable GetQueryData(string sSql, string PageSize, string CurrentPage, out string RecordCount, out string PageCount)
        {
            DataSet dataset = new DataSet();
            try
            {
                SqlParameter[] parameters ={
                   new SqlParameter("@sql", SqlDbType.Text),
                   new SqlParameter("@pagesize", SqlDbType.Int),
                   new SqlParameter("@currentpage", SqlDbType.Int),
                   new SqlParameter("@recordcount", SqlDbType.Int),
                   new SqlParameter("@pagecount", SqlDbType.Int)
                                           };
                parameters[0].Value = sSql;
                parameters[1].Value = PageSize;
                parameters[2].Value = CurrentPage;
                parameters[3].Direction = ParameterDirection.Output;
                parameters[4].Direction = ParameterDirection.Output;

                dataset = SQLServerDALHelper.ExecuteProcedureDataSet("spJKCFlow_GetQueyResult", parameters, SqlConType.Work);

                RecordCount = parameters[3].Value.ToString();
                PageCount = parameters[4].Value.ToString();

                //dataset.Tables.Remove(dataset.Tables[0]);
                return dataset.Tables[1];
            }
            catch (Exception e)
            {
                RecordCount = "0";
                PageCount = "0";
                return new DataTable();
            }
        }


        public static string SetPagerIndex(int CurrentPage, int PageSize, string sRecordCount, string sPageCount)
        {

            string strlist = string.Empty;
            string strMouserEvent = "onmouseover=this.className='FooterNumHover' onmouseout=this.className='FooterNum'";
            int iPager_CurrIndex = CurrentPage;
            int iPager_Count = int.Parse(sPageCount);
            int _pageindex = 0;

            StringBuilder strBuilderPage = new StringBuilder();

            strBuilderPage.Append(string.Format("<span>第<font color='red'>{0}</font>页</span>&nbsp&nbsp", CurrentPage.ToString()));
            strBuilderPage.AppendFormat("共<font color='red'>{0}</font>页&nbsp", sPageCount);
            strBuilderPage.AppendFormat("共<font color='red'>{0}</font>条&nbsp", sRecordCount);
            strBuilderPage.Append("[<span onclick='fnchangePage(1)' class='FooterNum'  " + strMouserEvent + ">第一页</span>]<span style='width:6px'></span>");


            if (iPager_CurrIndex > 1 && iPager_CurrIndex <= iPager_Count)
                _pageindex = iPager_CurrIndex - 1;
            else
                _pageindex = 1;
            strBuilderPage.Append("[<span onclick='fnchangePage(" + _pageindex.ToString() + ")' class='FooterNum'  " + strMouserEvent + ">上一页</span>]<span style='width:6px'></span>");

            if (iPager_CurrIndex < iPager_Count && iPager_Count != 1) _pageindex = iPager_CurrIndex + 1;
            else
            {
                _pageindex = iPager_Count;
            }
            strBuilderPage.Append("[<span onclick='fnchangePage(" + _pageindex.ToString() + ")' class='FooterNum'  " + strMouserEvent + ">下一页</span>]<span style='width:6px'></span>");
            strBuilderPage.Append("[<span onclick='fnchangePage(" + iPager_Count.ToString() + ")' class='FooterNum'  " + strMouserEvent + ">最后一页</span>]<span style='width:6px'></span>");
            return strBuilderPage.ToString();
        }

        public static string GetIsGAYZ(string XMCODE)
        {
            string IsGAYZ = "0";
            try {
                string sSql = string.Format("select CAST(isnull(gayz,0) as int) A from XM_jbxx where XMCODE='{0}'", XMCODE);
                Object o = SQLServerDALHelper.ExecuteSQLScalar(sSql, SqlConType.Work);
                return o == null ? "0" : o.ToString();
            }
            catch
            {
                IsGAYZ = "0";
            }
            return IsGAYZ;

        }
    }

}
