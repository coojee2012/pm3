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
/// SaveDevelopment 的摘要说明
/// </summary>
public static class SaveDevelopment
{


    /// <summary>
    /// 加入动态条目
    /// </summary>
    /// <param name="title">动态内容</param>
    /// <param name="imgAdd">动态图标</param>
    /// <param name="userID">添加动态人</param>
    /// <param name="content">动态内容</param>
    /// <returns></returns>
    public static bool SaveDevel(string title, string imgAdd, string userID, string content, IList presonFID, IList orgFIDS)
    {
        bool temp = false;
        RCenter rc = new RCenter();
        SaveOptionEnum so = SaveOptionEnum.Insert;
        string FID = Guid.NewGuid().ToString();
        System.Collections.SortedList sl = new SortedList();
        sl.Add("FID", FID);
        sl.Add("FTitle", title);
        sl.Add("FUserId", userID);
        sl.Add("FImageADD", imgAdd);
        sl.Add("FContent", content);
        sl.Add("FDate", DateTime.Now);
        sl.Add("FIsDeleted", false);
        //if (rc.SaveEBase(EntityTypeEnum.EOADevelopment, sl, "FID", so))
        //{
        //    temp = false;
        //}

        if (temp)
        {
            int m = 0;
            if (presonFID != null)
            {
                m = presonFID.Count;
            }
            int s = 0;
            if (orgFIDS != null)
            {
                s = orgFIDS.Count;
            }
            if (m > 0)
            {
                temp = false;

                System.Collections.SortedList[] sl1 = new SortedList[m];
                EntityTypeEnum[] en = new EntityTypeEnum[m];
                SaveOptionEnum[] so1 = new SaveOptionEnum[m];
                string[] key = new string[m];

                for (int i = 0; i < m; i++)
                {
                    sl1[i] = new SortedList();
                    //en[i] = EntityTypeEnum.EOADeveReal;
                    so1[i] = SaveOptionEnum.Insert;
                    key[i] = "FID";
                    string FMsgTO = presonFID[i].ToString();
                    string FID1 = Guid.NewGuid().ToString();
                    sl1[i].Add("FID", FID1);
                    sl1[i].Add("FDeveID", FID);
                    sl1[i].Add("FDeveToPre", FMsgTO);
                    sl1[i].Add("fisdeleted", false);
                    sl1[i].Add("FCreateTime", DateTime.Now);
                }
                if (rc.SaveEBaseM(en, sl1, key, so1))
                {
                    temp = true;

                }
            }
            if (s > 0)
            {
                temp = false;
                SortedList[] sl2 = new SortedList[s];
                EntityTypeEnum[] en = new EntityTypeEnum[s];
                SaveOptionEnum[] so1 = new SaveOptionEnum[s];
                string[] key = new string[s];

                for (int i = 0; i < s; i++)
                {
                    sl2[i] = new SortedList();
                    //en[i] = EntityTypeEnum.EOADeveReal;
                    so1[i] = SaveOptionEnum.Insert;
                    key[i] = "FID";
                    string FMsgTO = orgFIDS[i].ToString();

                    string FID1 = Guid.NewGuid().ToString();
                    sl2[i].Add("FID", FID1);
                    sl2[i].Add("FDeveID", FID);
                    sl2[i].Add("FDeveToOrg", FMsgTO);
                    sl2[i].Add("FIsDeleted", false);
                    sl2[i].Add("FCreateTime", DateTime.Now);

                }
                if (rc.SaveEBaseM(en, sl2, key, so1))
                {
                    temp = true;

                }
            }
            if (m == 0 && s == 0)
            {
                temp = false;
                SaveOptionEnum so3 = SaveOptionEnum.Insert;
                string FID3 = Guid.NewGuid().ToString();
                System.Collections.SortedList sl3 = new SortedList();
                sl3.Add("FID", FID3);
                sl3.Add("FDeveID", FID);
                sl3.Add("FIsAll", true);
                sl3.Add("FCreateTime", DateTime.Now);
                sl3.Add("FIsDeleted", false);
                //if (rc.SaveEBase(EntityTypeEnum.EOADeveReal, sl3, "FID", so3))
                {
                    temp = true;
                }
            }

        }


        return temp;
    }
}
