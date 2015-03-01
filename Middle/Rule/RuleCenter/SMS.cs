using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Text;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.Data.SqlClient;

/// <summary>
/// SMS 的摘要说明
/// </summary>
public class SMS
{
    RCenter rc = new RCenter();
 
    public SMS()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }


    /// <summary>
    /// 转换成字符串，能在SQL中执行。
    /// </summary>
    /// <param name="al"></param>
    /// <returns></returns>
    private string ArrayToString(ArrayList al)
    {
        string str = "";
        for (int i = 0; i < al.Count; i++)
        {
            if (!string.IsNullOrEmpty(str))
                str += ",";
            str += "'" + al[i].ToString() + "'";
        }
        return str;
    }

    /// <summary>
    /// 发送短消息
    /// </summary>
    /// <param name="TO">发给谁？(用对方的FBaseinfoId)</param>
    /// <param name="Content">发什么内容？(内容自已编，例"XXXX给您发来XXXX项目的项目勘察委托。请及时处理")</param>
    public void SendMessage(string TO, string Content)
    {
        int count = 1;

        EntityTypeEnum[] en = new EntityTypeEnum[count];
        SaveOptionEnum[] so = new SaveOptionEnum[count];
        SortedList[] sl = new SortedList[count];
        string[] key = new string[count];

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < count; i++)
        {
            en[i] = EntityTypeEnum.EsMessage;
            so[i] = SaveOptionEnum.Insert;
            sl[i] = new SortedList();
            key[i] = "FID";

            sl[i].Add("FID", Guid.NewGuid());
            sl[i].Add("FCreateTime", DateTime.Now);
            sl[i].Add("FIsDeleted", 0);
            sl[i].Add("FState", 0);//0：未发送；1：发送中；2：已发送
            sl[i].Add("FIsRead", 0);//0：未读；1：已读
            sl[i].Add("FSender", "系统");//发送人
            sl[i].Add("FAccept", TO);//接收人
            sl[i].Add("FSendTime", DateTime.Now);// 提交 时间

            //内容
            sl[i].Add("FText", Content);
            sl[i].Add("FTitle", Content);

        }
        if (count > 0)
        {
            rc.SaveEBaseM(en, sl, key, so);
        }

    }

 
    public void SendMessage(ArrayList appList, string Content)
    {
        string appStr = ArrayToString(appList);
        if (!string.IsNullOrEmpty(appStr))
        {

            DataTable dt = null;

            int count = appList.Count;

            EntityTypeEnum[] en = new EntityTypeEnum[count];
            SaveOptionEnum[] so = new SaveOptionEnum[count];
            SortedList[] sl = new SortedList[count];
            string[] key = new string[count];

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                string AppID = appList[i].ToString();//CF_App_List.FID
                en[i] = EntityTypeEnum.EsMessage;
                so[i] = SaveOptionEnum.Insert;
                sl[i] = new SortedList();
                key[i] = "FID";

                sl[i].Add("FID", Guid.NewGuid());
                sl[i].Add("FCreateTime", DateTime.Now);
                sl[i].Add("FIsDeleted", 0);
                sl[i].Add("FState", 0);//0：未发送；1：发送中；2：已发送
                sl[i].Add("FAccept", HttpContext.Current.Session["DFUserId"]);//操作人
                sl[i].Add("FSendTime", DateTime.Now);// 提交 时间


                sb.Remove(0, sb.Length);
                sb.Append("select FName CF_App_List where FId=@FLinkId");
                dt = rc.GetTable(sb.ToString(), new SqlParameter("@FLinkId", AppID));
                string FAppMangeTypeName = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    FAppMangeTypeName = dt.Rows[0]["FName"].ToString();
                }
                //业务名称


                //内容


                sl[i].Add("FText", Content);//手机号
                sl[i].Add("FTitle", Content);//短信内容

            }
            if (count > 0)
            {
                rc.SaveEBaseM(en, sl, key, so);
            }
        }
    }

 
    /// <summary>
    /// 整理保存项
    /// </summary>
    /// <param name="FType">发送类型。1008601：受理通知；1008602：不予受理通知；1008603：补正材料通知；1008604：打回通知；1008606：办结通知；</param>
    /// <param name="FAppName">业务名称</param>
    /// <param name="FBaseinfoId">企业FBaseinfoId</param>
    /// <param name="FManageTypeId">业务类型</param>
    /// <param name="alen">EntityTypeEnum</param>
    /// <param name="slso">SaveOptionEnum</param>
    /// <param name="alsl">SortedList</param>
    /// <param name="slke">key</param>
    private void SortOut(string FType, string FAppName, string FBaseinfoId, string FManageTypeId, string IDear, ref ArrayList alen, ref ArrayList slso, ref ArrayList alsl, ref ArrayList slke)
    {
        string Phone = getSMS_Phone(FBaseinfoId, FManageTypeId);
        if (!string.IsNullOrEmpty(Phone))
        {
            //内容模板
            string txt = rc.GetSignValue("select FRemark from CF_Sys_Dic where FNumber=@FNumber ", new SqlParameter("@FNumber", FType));
            //上报业务名
            txt = txt.Replace("&CONTENT&", FAppName);
            //审核意见（原因）
            txt = txt.Replace("&IDEAR&", IDear);


            EntityTypeEnum en = EntityTypeEnum.EOASMSList;
            SaveOptionEnum so = SaveOptionEnum.Insert;
            SortedList sl = new SortedList();
            string key = "FID";

            sl.Add("FID", Guid.NewGuid());
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FIsDeleted", 0);
            sl.Add("FState", 0);//0：未发送；1：发送中；2：已发送
            sl.Add("FUserId", HttpContext.Current.Session["DFUserId"]);//操作人
            sl.Add("FSubmitTime", DateTime.Now);// 提交 时间
            sl.Add("FPlanTime", DateTime.Now);//计划发送时间
            sl.Add("FMobile", Phone);//手机号
            sl.Add("FContent", txt);//短信内容

            alen.Add(en);
            slso.Add(so);
            alsl.Add(sl);
            slke.Add(key);
        }
    }

    /// <summary>
    /// 判断企业是否订制了当前业务所在系统的短信提醒，并返回接收手机号
    /// </summary>
    /// <param name="FBaseinfoId">企业BaseinfoId</param>
    /// <param name="FManageTypeId">业务类型</param>
    /// <returns></returns>
    private string getSMS_Phone(string FBaseinfoId, string FManageTypeId)
    {
        string phone = "";

        SortedList sl = new SortedList();
        sl.Add("FNumber", FManageTypeId);
        sl.Add("FBaseinfoId", FBaseinfoId);

        StringBuilder sb = new StringBuilder();
        sb.Append("select FIsSMS,FSMSPhone from CF_Sys_UserRight ");
        sb.Append("where FSystemId=(select FSystemId from CF_Sys_ManageType where FNumber=@FNumber) ");//从业务类型得到系统编号
        sb.Append("and FUserId=(select FID from CF_Sys_User where FBaseinfoId=@FBaseinfoId) ");//从BaseinfoId得到企业用户表FID
        DataTable dt = rc.GetTable(sb.ToString(), rc.ConvertParameters(sl));
        if (dt != null && dt.Rows.Count > 0)
        {
            string FIsSMS = dt.Rows[0]["FIsSMS"].ToString();
            string FSMSPhone = dt.Rows[0]["FSMSPhone"].ToString();
            if (FIsSMS == "1")//判断是否订制了短信提醒
            {
                if (!string.IsNullOrEmpty(FSMSPhone))//判断手机号不为空
                {
                    phone = FSMSPhone;
                }
            }
        }
        return phone;
    }


}
