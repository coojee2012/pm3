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
using Approve.EntityBase;
using System.Collections;

/// <summary>
/// SendSMsg 的摘要说明
/// </summary>
public class SendSMsg
{
    private bool temp = false;
    OA rc = new OA();
    public string userId = "C4A53042-55EB-4FA5-AA91-747B36EDC375";
    SaveOptionEnum so = SaveOptionEnum.Insert;
    public IList presonFID;
    public string MsgType;
    public string MsgContent;
    public SendSMsg()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public bool SentMsg(string FID)
    {
        
        int m = presonFID.Count;
        temp = false;
        
        System.Collections.SortedList[] sl = new SortedList[m];
        EntityTypeEnum[] en = new EntityTypeEnum[m];
        SaveOptionEnum[] so1 = new SaveOptionEnum[m];
        string[] key = new string[m];

        for (int i = 0; i < m; i++)
        {
            sl[i] = new SortedList();
            en[i] = EntityTypeEnum.EOAMsgReal;
            so1[i] = SaveOptionEnum.Insert;
            key[i] = "FID";
            string FMsgTO = presonFID[i].ToString();
            string FID1 = Guid.NewGuid().ToString();
            sl[i].Add("FID", FID1);
            sl[i].Add("FMsgID", FID);
            sl[i].Add("FMsgTO", FMsgTO);
            sl[i].Add("FMsgState", 0);
            sl[i].Add("FMsgIsCall", 1);
            sl[i].Add("fisdeleted", false);
        }
        if (rc.SaveEBaseM(en, sl, key, so1))
        {
            temp = true;

        }
        else
        {
            rc.PExcute("delete CF_OA_ShortMsg where FID='" + FID + "'");
        }

        return temp;
    }

    public bool SendNewMsg()
    {
        string FID = Guid.NewGuid().ToString() ;
        System.Collections.SortedList sl = new SortedList();
        sl.Add("FID", FID);
        sl.Add("MsgType", MsgType);
        sl.Add("MsgContent", MsgContent);
        sl.Add("MsgFrom", userId);
        sl.Add("MsgTime", DateTime.Now);
        sl.Add("FIsDeleted", false);
        if (rc.SaveEBase(EntityTypeEnum.EOAShortMsg, sl, "FID", so))
        {
            temp = true;
        }
        temp = SentMsg(FID);
        return temp;
    }
}
