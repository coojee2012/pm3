using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// chcekcard 的摘要说明
/// </summary>
public class chcekcard
{
    public chcekcard()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public bool check(string card1)
    {



        if (card1 == "")
        {
            //tool.showMessage("请您输入正确的15位或18位的身份证号码");
            return false;
        }
        else if (card1.Length == 15)
        {
            if (IsNum(card1) == false)
            {
                //tool.showMessage("请您输入正确的15位或18位的身份证号码");
                return false;
            }
            int cardYear = 0;
            if (card1.Substring(6, 1) == "0")
            {
                cardYear = Convert.ToInt16(card1.Substring(7, 1));
            }
            else
            {
                cardYear = Convert.ToInt16(card1.Substring(6, 2));
            }

            cardYear = cardYear + 1900;
            if (cardYear < 1900 || cardYear > DateTime.Now.Year)
            {
                //tool.showMessage("请您输入正确的15位或18位的身份证号码");

                return false;
            }
            else
            {
                int cardMoth = Convert.ToInt16(card1.Substring(8, 2));
                if (cardMoth == 0)
                {
                    cardMoth = Convert.ToInt16(card1.Substring(9, 1));
                }
                if (cardMoth == 0 || cardMoth > 12)
                {
                    //tool.showMessage("请您输入正确的15位或18位的身份证号码");

                    return false;
                }
                else
                {

                    int cardDay = Convert.ToInt16(card1.Substring(10, 2));
                    if (cardDay == 0)
                    {
                        cardDay = Convert.ToInt16(card1.Substring(11, 1));
                    }
                    if (cardDay == 0 || cardDay > 31)
                    {
                        //tool.showMessage("请您输入正确的15位或18位的身份证号码");

                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }

        }
        else if ((card1.Length == 18))
        {

            if (IsNum(card1.Substring(0, 17)) == false)
            {
                //tool.showMessage("请您输入正确的15位或18位的身份证号码");
                return false;
            }

            int cardYear = 0;
            string str = card1.Substring(6, 4);
            if (card1.Substring(6, 4) == "0000")
            {
                return false;
            }
            else
            {
                cardYear = Convert.ToInt16(card1.Substring(6, 4));
            }


            if (cardYear < 1900 || cardYear > DateTime.Now.Year)
            {
                //tool.showMessage("请您输入正确的15位或18位的身份证号码");

                return false;
            }
            else
            {
                int cardMoth = Convert.ToInt16(card1.Substring(10, 2));
                if (cardMoth == 0)
                {
                    cardMoth = Convert.ToInt16(card1.Substring(11, 1));
                }
                if (cardMoth == 0 || cardMoth > 12)
                {
                    //tool.showMessage("请您输入正确的15位或18位的身份证号码");

                    return false;
                }
                else
                {

                    int cardDay = Convert.ToInt16(card1.Substring(12, 2));
                    if (cardDay == 0)
                    {
                        cardDay = Convert.ToInt16(card1.Substring(13, 1));
                    }
                    if (cardDay == 0 || cardDay > 31)
                    {
                        //tool.showMessage("请您输入正确的15位或18位的身份证号码");

                        return false;
                    }
                    else
                    {
                        return true;
                    }


                }
            }
        }
        else
        {
            //tool.showMessage("请您输入正确的15位或18位的身份证号码");
            return false;
        }

        return false;


    }

    public bool IsNum(String str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!Char.IsNumber(str, i))
                return false;
        }
        return true;
    }
    /// <summary>
    /// 判断是否为延安/陕西部门
    /// </summary>
    /// <param name="deptId"></param>
    /// <returns></returns>
    public static bool CheckDept(object deptId)
    {
        string deptStr = Convert.ToString(deptId);
        if (string.IsNullOrEmpty(deptStr))//为空
        {
            return false;
        }
        else if (deptStr.Trim().Length >= 4)
        {
            if (deptStr.Trim().Substring(0, 4) != "6106")//不是延安市（及县以内）的部门
            {
                return false;
            }
        }
        return true;
    }
}
