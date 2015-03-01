using System;
using System.Collections;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Tools;
namespace Tools
{
    public enum SaveOptionEnum   //操作类型
    {
        Insert,
        Update,
        Unknown
    }

    /// <summary>
    /// pageTool 的摘要说明。
    /// </summary>
    public class pageTool : System.Web.UI.Page
    {
        /// <summary>
        /// 批量删除的委托
        /// </summary>
        /// <param name="FIdList">主键列</param>
        /// <param name="context">数据库操作上下文，使用这个能在一个事务里执行</param>
        public delegate void DeletingDelegate(IList<string> FIdList, DataContext context);
        private string sign = "t_";
        private System.Web.UI.Page page = null;


        private string dsign = "t_";
        private string msign = "t_";

        //private string qdsign = "d_";
        //private string qmsign = "m_";
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="myPage">要操作的页面</param>
        /// <param name="controlSign">要操作的页面控件的标志</param>
        public pageTool(System.Web.UI.Page myPage, string controlSign)//初始化控件标志
        {
            this.page = myPage;
            sign = controlSign;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="myPage">要操作的页面</param>
        public pageTool(System.Web.UI.Page myPage)
        {
            this.page = myPage;
        }



        #endregion

        #region 填充转换过后的数据
        public void setPageDropList()
        {
            //RBase rule = new RBase();
            //System.Web.UI.HtmlControls.HtmlForm myform = (System.Web.UI.HtmlControls.HtmlForm)page.FindControl("Form1");
            //for (int i = 0; i < myform.Controls.Count; i++)
            //{
            //    if (myform.Controls[i].ID != null)
            //    {
            //        string str = myform.Controls[i].ID;
            //        string ss = str;
            //        if (myform.Controls[i].ID.IndexOf(qdsign) > -1 && myform.Controls[i].ID != "")
            //        {
            //            System.Web.UI.Control control = myform.Controls[i];
            //            if (control is System.Web.UI.WebControls.TextBox)
            //            {
            //                string sValue = ((System.Web.UI.WebControls.TextBox)control).Text;
            //                //							((System.Web.UI.WebControls.TextBox)control).Text=rule.GetSignValue(EntityTypeEnum.EDic,"fname","fid='"+sValue+"' and fisdeleted=0 ");
            //            }
            //        }

            //        if (myform.Controls[i].ID.IndexOf(qmsign) > -1 && myform.Controls[i].ID != "")
            //        {
            //            System.Web.UI.Control control = myform.Controls[i];
            //            if (control is System.Web.UI.WebControls.TextBox)
            //            {
            //                string sValue = ((System.Web.UI.WebControls.TextBox)control).Text;
            //                //							((System.Web.UI.WebControls.TextBox)control).Text=rule.GetSignValue(EntityTypeEnum.EManageDept,"ffullname","fid='"+sValue+"' and fisdeleted=0 ");
            //            }
            //        }
            //    }
            //}

        }
        #endregion

        #region 数据相关的
        #region 填充页面
        /// <summary>
        /// 填充页面的控件
        /// </summary>
        /// <param name="dr">用于填充页面的数据行</param>
        /// <returns></returns>
        public bool fillPageControl(object o, System.Web.UI.Control container)
        {
            if (o != null)
            {
                PropertyInfo[] p = o.GetType().GetProperties();


                for (int i = 0; i < p.Length; i++)
                {
                    object value = p[i].GetValue(o, null);
                    if (p[i].GetValue(o, null) != null && value.GetType() == typeof(DateTime))
                    {
                        this.setValueToControl(container, this.getControlNameFromPage(p[i].Name), EConvert.ToShortDateString(value));
                    }
                    else
                    {
                        if (p[i].GetValue(o, null) != null && value.GetType() == typeof(bool))
                        {
                            setChkValueToControl(container, this.getControlNameFromPage(p[i].Name), EConvert.ToBool(value));
                            if (EConvert.ToBool(value))
                            {
                                value = 1;
                            }
                            else
                            {
                                value = 0;
                            }
                        }
                        this.setValueToControl(container, this.getControlNameFromPage(p[i].Name), EConvert.ToString(value));
                    }
                }
            }
            return true;

        }
        public bool fillPageControl(object o)
        {
            System.Web.UI.HtmlControls.HtmlForm myform = (System.Web.UI.HtmlControls.HtmlForm)page.FindControl("Form1");
            return fillPageControl(o, myform);
        }
        private void setValueToControl(System.Web.UI.Control myform, string controlName, string controlValue)
        {
            System.Web.UI.Control control = myform.FindControl(controlName);
            if (controlValue == null || controlValue == "")
            {
                return;
            }
            if (control != null)
            {
                if (control is System.Web.UI.WebControls.Label)
                {
                    ((System.Web.UI.WebControls.Label)control).Text = controlValue;
                }
                if (control is System.Web.UI.WebControls.Literal)
                {
                    ((System.Web.UI.WebControls.Literal)control).Text = controlValue;
                }
                if (control is System.Web.UI.WebControls.TextBox)
                {
                    ((System.Web.UI.WebControls.TextBox)control).Text = controlValue;
                }
                if (control is System.Web.UI.WebControls.CheckBox)
                {
                    ((System.Web.UI.WebControls.CheckBox)control).Checked = EConvert.ToInt(controlValue) == 1;
                }
                if (control is System.Web.UI.WebControls.DropDownList)
                {
                    System.Web.UI.WebControls.ListItem li = ((System.Web.UI.WebControls.DropDownList)control).Items.FindByValue(controlValue);
                    ((System.Web.UI.WebControls.DropDownList)control).SelectedIndex = ((System.Web.UI.WebControls.DropDownList)control).Items.IndexOf(li);
                    if (li != null)
                    {
                        ((System.Web.UI.WebControls.DropDownList)control).SelectedValue = controlValue;
                    }
                }
                if (control is System.Web.UI.WebControls.RadioButtonList)
                {
                    System.Web.UI.WebControls.ListItem li = ((System.Web.UI.WebControls.RadioButtonList)control).Items.FindByValue(controlValue);
                    if (li != null)
                    {
                        ((System.Web.UI.WebControls.RadioButtonList)control).SelectedValue = controlValue;
                    }
                }
                if (control is System.Web.UI.WebControls.CheckBoxList)
                {
                    for (int i = 0; i < ((CheckBoxList)control).Items.Count; i++)
                    {
                        if (controlValue.IndexOf(((CheckBoxList)control).Items[i].Value) != -1)
                        {
                            ((CheckBoxList)control).Items[i].Selected = true;
                        }
                    }

                }
                if (control is System.Web.UI.WebControls.Image)
                {
                    ((System.Web.UI.WebControls.Image)control).ImageUrl = controlValue;
                }
                if (control is System.Web.UI.HtmlControls.HtmlInputHidden)
                {
                    ((System.Web.UI.HtmlControls.HtmlInputHidden)control).Value = controlValue;
                }
                if (control is System.Web.UI.HtmlControls.HtmlSelect)
                {
                    System.Web.UI.WebControls.ListItem li = ((System.Web.UI.HtmlControls.HtmlSelect)control).Items.FindByValue(controlValue);
                    if (li != null)
                    {
                        ((System.Web.UI.HtmlControls.HtmlSelect)control).Value = controlValue;
                    }
                }
                if (control is System.Web.UI.HtmlControls.HtmlTextArea)
                {
                    ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value = controlValue;
                }
                if (control is System.Web.UI.HtmlControls.HtmlInputText)
                {
                    ((System.Web.UI.HtmlControls.HtmlInputText)control).Value = controlValue;
                }
                if (control is System.Web.UI.HtmlControls.HtmlInputImage)
                {
                    ((System.Web.UI.HtmlControls.HtmlInputImage)control).Src = controlValue;
                }
            }
        }

        public bool fillChkControl(DataRow dr)
        {
            try
            {
                if (dr != null)
                {
                    System.Web.UI.HtmlControls.HtmlForm myform = (System.Web.UI.HtmlControls.HtmlForm)page.FindControl("Form1");
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        this.setChkValueToControl(myform, this.getControlNameFromPage(dr.Table.Columns[i].ColumnName), dr[i].ToString());
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void setChkValueToControl(System.Web.UI.Control myform, string controlName, string controlValue)
        {
            System.Web.UI.Control control = myform.FindControl(controlName);
            if (control != null)
            {
                if (control is System.Web.UI.WebControls.CheckBox)
                {
                    ((System.Web.UI.WebControls.CheckBox)control).Checked = false;
                    if (controlValue == "1")
                        ((System.Web.UI.WebControls.CheckBox)control).Checked = true;
                }
            }
        }
        private void setChkValueToControl(System.Web.UI.Control myform, string controlName, bool controlValue)
        {
            System.Web.UI.Control control = myform.FindControl(controlName);
            if (control != null)
            {
                if (control is System.Web.UI.WebControls.CheckBox)
                {
                    ((System.Web.UI.WebControls.CheckBox)control).Checked = controlValue;
                }
            }
        }
        #endregion

        #region 获取页面值
        /// <summary>
        /// 已过时，保存页面输入的数据时获取页面Form内第一级控件输入的数据返回一个集合
        /// </summary>
        /// <returns>SortedList</returns>
        [Obsolete("已过时。")]
        public SortedList getPageValue()
        {
            System.Web.UI.Control container = page.Form as System.Web.UI.Control;
            return getPageValue(container);
        }
        /// <summary>
        /// 已过时，保存页面输入的数据时获取页面指定容器container内第一级控件输入的数据，返回一个集合
        /// </summary>
        /// <param name="container">容器</param>
        /// <returns>SortedList</returns>
        public SortedList getPageValue(System.Web.UI.Control container)
        {
            SortedList sl = new SortedList();
            System.Web.UI.Control myform = container;
            for (int i = 0; i < myform.Controls.Count; i++)
            {
                if (myform.Controls[i].ID != null)
                {
                    string str = myform.Controls[i].ID;
                    string ss = str;
                    if ((myform.Controls[i].ID.IndexOf(sign) > -1 && myform.Controls[i].ID != "" && this.isControlUsefull(myform.Controls[i].ID)))// || (myform.Controls[i].ID.IndexOf(dsign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) || (myform.Controls[i].ID.IndexOf(msign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) )
                    {
                        this.setValueToSortedList(sl, myform.Controls[i]);
                    }
                }
                for (int j = 0; j < myform.Controls[i].Controls.Count; j++) //第二层
                {
                    if (myform.Controls[i].Controls[j].ID != null)
                    {
                        string str = myform.Controls[i].Controls[j].ID;
                        string ss = str;
                        if (myform.Controls[i].Controls[j].ID != null && (myform.Controls[i].Controls[j].ID.IndexOf(sign) > -1 && myform.Controls[i].Controls[j].ID != "" && this.isControlUsefull(myform.Controls[i].Controls[j].ID)))// || (myform.Controls[i].ID.IndexOf(dsign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) || (myform.Controls[i].ID.IndexOf(msign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) )
                        {
                            this.setValueToSortedList(sl, myform.Controls[i].Controls[j]);
                        }
                    }
                    for (int n = 0; n < myform.Controls[i].Controls[j].Controls.Count; n++) //第三层
                    {
                        if (myform.Controls[i].Controls[j].Controls[n].ID != null)
                        {
                            string str = myform.Controls[i].Controls[j].Controls[n].ID;
                            string ss = str;
                            if ((myform.Controls[i].Controls[j].Controls[n].ID.IndexOf(sign) > -1 && myform.Controls[i].Controls[j].Controls[n].ID != "" && this.isControlUsefull(myform.Controls[i].Controls[j].Controls[n].ID)))// || (myform.Controls[i].ID.IndexOf(dsign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) || (myform.Controls[i].ID.IndexOf(msign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) )
                            {
                                this.setValueToSortedList(sl, myform.Controls[i].Controls[j].Controls[n]);
                            }
                        }
                    }
                }

            }
            if (!sl.ContainsKey("FTime"))
            {
                sl.Add("FTime", DateTime.Now);
            }
            return sl;
        }
        /// <summary>
        /// 获取页面Form内第一级控件输入的数据,并给TSource指定的类型的变量属性赋上值
        /// </summary>
        /// <typeparam name="TSource">类型:如：《CF_Sys_Dic》，可不写，不写时类型由obj决定的</typeparam>
        /// <param name="obj">实体类或带属性的类也可以</param>
        /// <returns>已经赋好值的变量</returns>
        public TSource getPageValue<TSource>(TSource obj)
        {
            IList<string> RemoveKeys = new List<string>(); RemoveKeys.Remove("FID");
            return getPageValue<TSource>(obj, RemoveKeys);
        }

        /// <summary>
        /// 获取页面Form内第一级控件输入的数据,并给type指定的类型的变量赋上值,集合 RemoveKeys 不会被赋值
        /// </summary>
        /// <typeparam name="TSource">类型:如：《CF_Sys_Dic》，可不写，不写时类型由obj决定的</typeparam>
        /// <param name="obj">实体类或带属性的类也可以</param>
        /// <param name="RemoveKeys">要排除的哪些属性</param>
        /// <returns>已经赋好值的变量</returns>
        public TSource getPageValue<TSource>(TSource obj, IList<string> RemoveKeys)
        {
            System.Web.UI.Control container = page.Form as System.Web.UI.Control;
            return getPageValue(obj, container, RemoveKeys);
        }

        /// <summary>
        /// 获取页面指定容器container内第一级控件输入的数据，并给TSource指定的类型的变量属性赋上值
        /// </summary>
        /// <typeparam name="TSource">类型:如：《CF_Sys_Dic》，可不写，不写时类型由obj决定的</typeparam>
        /// <param name="obj">实体类或带属性的类也可以</param>
        /// <param name="container">容器</param>
        /// <returns>已经赋好值的变量</returns>
        public TSource getPageValue<TSource>(TSource obj, System.Web.UI.Control container)
        {
            IList<string> RemoveKeys = new List<string>(); RemoveKeys.Remove("FID");
            return getPageValue(obj, container, RemoveKeys);
        }
        /// <summary>
        /// 获取页面指定容器container内第一级控件输入的数据，并给TSource指定的类型的变量属性赋上值,集合 RemoveKeys 不会被赋值
        /// </summary>
        /// <typeparam name="TSource">类型:如：《CF_Sys_Dic》，可不写，不写时类型由obj决定的</typeparam>
        /// <param name="obj">实体类或带属性的类也可以</param>
        /// <param name="container">容器</param>
        /// <param name="RemoveKeys">要排除的哪些属性</param>
        /// <returns>已经赋好值的变量</returns>
        public TSource getPageValue<TSource>(TSource obj, System.Web.UI.Control container, IList<string> RemoveKeys)
        {
            SortedList sl = getPageValue(container);
            Type type = typeof(TSource);
            foreach (string key in sl.Keys)
            {
                if (RemoveKeys.Contains(key, StringComparer.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                object ovalue = sl[key];
                if (ovalue == null)
                {
                    continue;
                }
                PropertyInfo property = type.GetProperty(key, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    Type valueType = property.PropertyType;

                    if (property.PropertyType.GetGenericArguments().Count() > 0)//System.Nullable<int>
                    {
                        valueType = property.PropertyType.GetGenericArguments()[0];
                    }
                    if (valueType == typeof(bool))
                    {
                        ovalue = (EConvert.ToInt(ovalue) == 1);
                    }
                    object value = null;
                    if (ovalue.ToString() != "")//System.Nullable<int>
                    {
                        value = Convert.ChangeType(ovalue, valueType);
                    }

                    property.SetValue(obj, value, null);
                }
            }
            return obj;
        }
        public SortedList getPageViewValue(string target)
        {
            SortedList sl = new SortedList();
            //System.Web.UI.HtmlControls.HtmlTable myform = (System.Web.UI.HtmlControls.HtmlTable)page.FindControl(target);
            System.Web.UI.WebControls.View myform = (System.Web.UI.WebControls.View)page.FindControl(target);
            for (int i = 0; i < myform.Controls.Count; i++)
            {
                if (myform.Controls[i].ID != null)
                {
                    string str = myform.Controls[i].ID;
                    string ss = str;
                    if ((myform.Controls[i].ID.IndexOf(sign) > -1 && myform.Controls[i].ID != "" && this.isControlUsefull(myform.Controls[i].ID)))// || (myform.Controls[i].ID.IndexOf(dsign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) || (myform.Controls[i].ID.IndexOf(msign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) )
                    {
                        this.setValueToSortedList(sl, myform.Controls[i]);
                    }
                }
            }
            return sl;
        }
        private void setValueToSortedList(SortedList sl, System.Web.UI.Control control)
        {
            if (control != null)
            {
                if (control is System.Web.UI.WebControls.Label)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.WebControls.Label)control).Text);
                }
                if (control is System.Web.UI.WebControls.TextBox)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.WebControls.TextBox)control).Text);
                }
                if (control is System.Web.UI.WebControls.RadioButtonList)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.WebControls.RadioButtonList)control).SelectedValue);
                }
                if (control is System.Web.UI.WebControls.DropDownList)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.WebControls.DropDownList)control).SelectedValue);
                }
                if (control is System.Web.UI.WebControls.CheckBox)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.WebControls.CheckBox)control).Checked);
                }
                if (control is System.Web.UI.WebControls.RadioButton)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.WebControls.RadioButton)control).Checked);
                }
                if (control is System.Web.UI.WebControls.CheckBoxList)
                {
                    string sValue = "";
                    ArrayList array = new ArrayList();
                    for (int i = 0; i < ((CheckBoxList)control).Items.Count; i++)
                    {
                        if (((CheckBoxList)control).Items[i].Selected == true)
                        {
                            array.Add(((CheckBoxList)control).Items[i].Value);
                        }
                    }
                    for (int j = 0; j < array.Count; j++)
                    {
                        if (j == 0)
                        {
                            sValue += array[j].ToString();
                        }
                        else
                        {
                            sValue += "," + array[j].ToString();

                        }
                    }
                    sl.Add(this.getColumnsNameFromPage(control.ID), sValue);
                }

                if (control is System.Web.UI.WebControls.Image)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.WebControls.Image)control).ImageUrl);
                }
                if (control is System.Web.UI.HtmlControls.HtmlInputHidden)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.HtmlControls.HtmlInputHidden)control).Value);
                }
                if (control is System.Web.UI.HtmlControls.HtmlSelect)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.HtmlControls.HtmlSelect)control).Value);
                }
                if (control is System.Web.UI.HtmlControls.HtmlTextArea)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value);
                }
                if (control is System.Web.UI.HtmlControls.HtmlInputText)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.HtmlControls.HtmlInputText)control).Value);
                }
                if (control is System.Web.UI.HtmlControls.HtmlInputImage)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.HtmlControls.HtmlInputImage)control).Src);
                }
            }
        }
        public SortedList getChkValue()
        {
            SortedList sl = new SortedList();
            System.Web.UI.HtmlControls.HtmlForm myform = (System.Web.UI.HtmlControls.HtmlForm)page.FindControl("Form1");
            for (int i = 0; i < myform.Controls.Count; i++)
            {
                if (myform.Controls[i].ID != null)
                {
                    string str = myform.Controls[i].ID;
                    string ss = str;
                    if ((myform.Controls[i].ID.IndexOf(sign) > -1 && myform.Controls[i].ID != "" && this.isControlUsefull(myform.Controls[i].ID)))// || (myform.Controls[i].ID.IndexOf(dsign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) || (myform.Controls[i].ID.IndexOf(msign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) )
                    {
                        this.setChkValueToSortedList(sl, myform.Controls[i]);
                    }
                }
            }
            return sl;
        }
        private void setChkValueToSortedList(SortedList sl, System.Web.UI.Control control)
        {
            if (control != null)
            {
                if (control is System.Web.UI.WebControls.CheckBox)
                {
                    int Judge = 0;
                    if (((System.Web.UI.WebControls.CheckBox)control).Checked)
                        Judge = 1;
                    sl.Add(this.getColumnsNameFromPage(control.ID), Judge);
                }
            }
        }
        #endregion

        public void ClearPageValue()
        {
            SortedList sl = new SortedList();
            System.Web.UI.HtmlControls.HtmlForm myform = (System.Web.UI.HtmlControls.HtmlForm)page.FindControl("Form1");
            for (int i = 0; i < myform.Controls.Count; i++)
            {
                if (myform.Controls[i].ID != null)
                {
                    string str = myform.Controls[i].ID;
                    string ss = str;
                    if (myform.Controls[i].ID.IndexOf(sign) > -1 && myform.Controls[i].ID != "" && this.isControlUsefull(myform.Controls[i].ID))
                    {
                        this.ClearValueToSortedList(sl, myform.Controls[i]);
                    }
                }
            }

        }
        private void ClearValueToSortedList(SortedList sl, System.Web.UI.Control control)
        {
            if (control != null)
            {
                if (control is System.Web.UI.WebControls.Label)
                {
                    ((System.Web.UI.WebControls.Label)control).Text = null;
                }
                if (control is System.Web.UI.WebControls.TextBox)
                {
                    ((System.Web.UI.WebControls.TextBox)control).Text = null;
                }
                //				if(control is System.Web.UI.WebControls.RadioButtonList)
                //				{
                //					sl.Add(this.getColumnsNameFromPage(control.ID),((System.Web.UI.WebControls.RadioButtonList)control).SelectedValue);			
                //				}
                if (control is System.Web.UI.WebControls.DropDownList)
                {
                    ((System.Web.UI.WebControls.DropDownList)control).SelectedIndex = -1;
                }

                //				if(control is System.Web.UI.WebControls.Image)
                //				{
                //					sl.Add(this.getColumnsNameFromPage(control.ID),((System.Web.UI.WebControls.Image)control).ImageUrl);									
                //				}
                if (control is System.Web.UI.HtmlControls.HtmlInputHidden)
                {
                    ((System.Web.UI.HtmlControls.HtmlInputHidden)control).Value = null;
                }
                if (control is System.Web.UI.HtmlControls.HtmlSelect)
                {
                    ((System.Web.UI.HtmlControls.HtmlSelect)control).Value = null;
                }
                if (control is System.Web.UI.HtmlControls.HtmlTextArea)
                {
                    ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value = null;
                }
                if (control is System.Web.UI.HtmlControls.HtmlInputText)
                {
                    ((System.Web.UI.HtmlControls.HtmlInputText)control).Value = null;
                }
                //				if(control is System.Web.UI.HtmlControls.HtmlInputImage)
                //				{
                //					((System.Web.UI.HtmlControls.HtmlInputImage)control).Src);			
                //				}
            }
        }
        #endregion

        #region 从页面控件组合WHERE子句
        public string getConditionFromPage()
        {
            string type = "and";
            string condition = "";
            SortedList sl = this.getPageValue();
            foreach (string key in sl.Keys)
            {
                if (sl[key].ToString() != "")
                {
                    condition += key + " LIKE '%" + sl[key].ToString() + "%' " + type + " ";
                }
            }
            if (condition != "")
            {
                condition = condition.Substring(0, condition.Length - type.Length - 2);
            }
            return condition;
        }
        #endregion

        #region 获取页面控件名称
        private string getControlNameFromPage(string columnName)
        {
            return sign + columnName;
        }
        #endregion

        #region 从页面控件获取数据表列名称
        private string getColumnsNameFromPage(string controlName)
        {
            return controlName.Substring(sign.Length).ToUpper();
        }
        #endregion

        #region 判断控件是否是用以保存的
        private bool isControlUsefull(string controlName)
        {
            if ((controlName.Substring(0, sign.Length) == sign) || (controlName.Substring(0, dsign.Length) == dsign) || (controlName.Substring(0, msign.Length) == msign))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion



        #region 页面弹出消息
        /// <summary>
        /// 页面弹出信息并且跳转到新页面
        /// </summary>
        /// <param name="message">要弹出的信息</param>
        public void showMessage(string message)
        {
            page.ClientScript.RegisterStartupScript(ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "')</script>");
        }
        /// <summary>
        /// 页面弹出信息并且跳转到新页面
        /// </summary>
        /// <param name="message">要弹出的信息</param>
        /// <param name="nextUrl">要跳转的新页面的Url</param>
        public void showMessageAndGoNewPage(string message, string nextUrl)
        {
            page.ClientScript.RegisterStartupScript(ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "');window.location='" + nextUrl + "';</script>");
        }

        public void showMessageAndEndPage(string message)
        {
            page.Response.Write("<font color=red size=2>" + message + "</font>");
            page.Response.End();
        }
        public void showMessageAndRunFunction(string message, string functionName)
        {

            page.ClientScript.RegisterStartupScript(ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "');" + functionName + ";</script>");
        }
        public void showMessageAndRunFunction(string message, string functionName, string funName)
        {
            page.ClientScript.RegisterStartupScript(ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "');" + functionName + ";" + funName + ";</script>");
        }

        public void ExecuteScript(string scriptstr)
        {
            page.ClientScript.RegisterStartupScript(ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>" + scriptstr + "</script>");
        }
        [Obsolete("过时了")]
        public string alert(string scriptstr)
        {
            return "<script>alert('" + scriptstr + "')</script>";
        }
        #endregion

        #region 过N秒后跳转
        /// <summary>
        /// 过N秒后跳转到新页
        /// </summary>
        /// <param name="url">新页的地址</param>
        /// <param name="n">间隔的秒数</param>
        public void goNextPageAfterLater(string url, int n)
        {
            page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "js", "<script>window.setTimeout(\"window.location='" + url + "'\"," + (n * 1000).ToString() + ")</script>");
        }
        #endregion





        #region 上传图片
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="postControl">上传控件</param>
        /// <param name="aimFilePath">图片在服务器端要存储的，相对于虚拟目录的路径，如\\images\\</param>
        /// <param name="maxLength">允许上传的最大字节数</param>
        /// <param name="leftFileName">保存时设定名称的标记</param>
        /// <returns></returns>
        public bool upPicToServer(System.Web.UI.Control postControl, string aimFilePath, int maxLength, string leftFileName, ref string returnFileName)
        {
            if (postControl is System.Web.UI.HtmlControls.HtmlInputFile)
            {
                System.Web.UI.HtmlControls.HtmlInputFile mypost = (System.Web.UI.HtmlControls.HtmlInputFile)postControl;
                System.Web.HttpPostedFile pic = mypost.PostedFile;
                int fileLength = pic.ContentLength;
                if (fileLength == 0)
                {
                    this.showMessage("请选择你要上传的图片！");
                    return false;
                }
                else if (fileLength > maxLength)
                {
                    this.showMessage("图片文件应该在" + (maxLength / 1024000).ToString() + "M以内！");
                    return false;
                }
                string tempstr = mypost.Value.Substring(mypost.Value.LastIndexOf(".") + 1, 3);
                if (tempstr != "jpg" && tempstr != "JPG" && tempstr != "gif" && tempstr != "gif")
                {
                    this.showMessage("只能上传JPG、GIF格式的图片！");
                    return false;
                }
                string fileName = leftFileName + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(":", "").Replace(" ", "").Replace("-", "");
                returnFileName = fileName + "." + tempstr;
                string path = this.Server.MapPath(ConfigurationSettings.AppSettings["SiteName"].ToString());

                path += aimFilePath + fileName + "." + tempstr;
                try
                {
                    pic.SaveAs(path);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region 控制图片显示大小
        /// <summary>
        /// 控制图片显示的大小
        /// </summary>
        /// <param name="?">图片控件</param>
        /// <param name="width">不能超过的宽度,如为0刚根据高度按比例设置</param>
        /// <param name="height">不能超过的高度,如为0刚根据宽度按比例设置</param>
        public void setImagesWidthAndHeight(System.Web.UI.Control image, int width, int height)
        {
            if (image is System.Web.UI.WebControls.Image)
            {
                System.Web.UI.WebControls.Image myimage = (System.Web.UI.WebControls.Image)image;
                try
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(base.Server.MapPath(myimage.ImageUrl));
                    if (height == 0)
                    {
                        if (img.Width > width)
                        {
                            myimage.Width = width;
                        }
                    }
                    if (width == 0)
                    {
                        if (img.Height > height)
                        {
                            myimage.Height = height;
                        }
                    }
                    if (height > 0 && width > 0)
                    {
                        if (img.Width > width && img.Height > height)
                        {
                            if (img.Width >= img.Height)
                            {
                                Decimal ih = Decimal.Parse(img.Height.ToString());
                                Decimal iw = Decimal.Parse(img.Width.ToString());

                                myimage.Width = width;
                                myimage.Height = int.Parse(Math.Round(width * (ih / iw), 0).ToString());

                                //								myimage.Width=width;
                                //								myimage.Height=Math.Round(Decimal.Parse(((Decimal)(width)*((Decimal)img.Height/(Decimal)img.Width)).Tostring()),0);
                            }
                            else
                            {
                                Decimal ih = Decimal.Parse(img.Height.ToString());
                                Decimal iw = Decimal.Parse(img.Width.ToString());

                                myimage.Height = height;
                                myimage.Width = int.Parse(Math.Round(height * (iw / ih), 0).ToString());
                            }
                        }
                        else
                        {
                            if (img.Width > width)
                            {
                                myimage.Width = width;
                            }
                            if (img.Height > height)
                            {
                                myimage.Height = height;
                            }
                        }
                    }
                }
                catch
                {
                    //	return;
                }

            }
        }

        #endregion

        #region
        /// <summary>
        /// 批量删除DataGrid选中的行。删除提交之前调用委托DeletingDelegate绑定的方法用能在同一个事物内tool.DelInfoFromGrid(EntInfo_List, db.CF_Ent_BaseInfo, tool_Deleting);
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="grid">DataGrid</param>
        /// <param name="table">实体</param>
        /// <param name="Deleting">方法private void tool_Deleting(System.Collections.Generic.IList《string》 FIdList, System.Data.Linq.DataContext context)</param>
        public void DelInfoFromGrid<TEntity>(DataGrid grid, Table<TEntity> table, DeletingDelegate Deleting) where TEntity : class
        {

            string FId = "";

            int RowCount = grid.Items.Count;
            IList<string> FIdList = new List<string>();
            for (int i = 0; i < grid.Items.Count; i++)
            {
                CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();

                    FIdList.Add(FId);
                }
            }

            DelInfoItem(FIdList, table, Deleting);
        }
        /// <summary>
        /// 批量删除DataGrid选中的行。
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="grid">DataGrid</param>
        /// <param name="table">实体</param>
        public void DelInfoFromGrid<TEntity>(DataGrid grid, Table<TEntity> table) where TEntity : class
        {
            DelInfoFromGrid(grid, table, null);
        }
        private void DelInfoItem<TEntity>(IList<string> FIdList, Table<TEntity> table, DeletingDelegate Deleting) where TEntity : class
        {
            if (FIdList.Count > 0)
            {
                string keyName = "FId";
                Type type = typeof(TEntity);
                PropertyInfo property = type.GetProperty(keyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    foreach (string ovalue in FIdList)
                    {
                        //抽象语法树 in ('','',''),FIdList.ToArray().Contains(d.FID)
                        ParameterExpression param = Expression.Parameter(typeof(TEntity), "t");
                        Expression right = Expression.Constant(ovalue); // 值

                        Expression left = Expression.Property(param, property.Name);//t=>t.FID==
                        Expression filter = Expression.Equal(left, right); // 等于
                        Expression<Func<TEntity, bool>> pred = Expression.Lambda<Func<TEntity, bool>>(filter, param);
                        TEntity Target = table.FirstOrDefault(pred);
                        if (Target != null)
                        {
                            table.DeleteOnSubmit(Target);
                        }
                    }
                }
                if (Deleting != null)
                {
                    Deleting(FIdList, table.Context);
                }
                table.Context.SubmitChanges();
                this.showMessage("成功删除" + FIdList.Count + "项");
            }
            else
            {
                this.showMessage("请选择要删除的项");
            }
        }
        /// <summary>
        /// 批量删除GridView选中的行。删除提交之前调用委托DeletingDelegate绑定的方法，使用能在同一个事务内tool.DelInfoFromGrid(EntInfo_List, db.CF_Ent_BaseInfo, tool_Deleting);
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="gridView">GridView</param>
        /// <param name="table">实体</param>
        /// <param name="Deleting">方法private void tool_Deleting(System.Collections.Generic.IList《string》 FIdList, System.Data.Linq.DataContext context)</param>
        public void DelInfoFromGrid<TEntity>(GridView gridView, Table<TEntity> table, DeletingDelegate Deleting) where TEntity : class
        {
            string FId = "";
            IList<string> FIdList = new List<string>();
            for (int i = 0; i < gridView.DataKeys.Count; i++)
            {
                CheckBox cbx = (CheckBox)gridView.Rows[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = EConvert.ToString(gridView.DataKeys[i]["FId"]);
                    FIdList.Add(FId);
                }
            }

            DelInfoItem(FIdList, table, Deleting);
        }
        /// <summary>
        /// 批量删除GridView选中的行。
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="grid">GridView</param>
        /// <param name="table">实体</param>
        public void DelInfoFromGrid<TEntity>(GridView gridView, Table<TEntity> table) where TEntity : class
        {
            DelInfoFromGrid(gridView, table, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid">DataGrid</param>
        /// <param name="en">实体</param>
        /// <param name="className">类名</param>
        /// <param name="fMothod">方法名</param>
        /// <returns></returns>
        //public void DelInfoFromGrid(DataGrid grid, EntityTypeEnum en, string className, string fMothod)
        //{
        //    string path = Server.MapPath("~/bin/");
        //    string filePath = path.Substring(0, path.LastIndexOf('\\')) + @"\RuleCenter.dll";
        //    Assembly classSampleAssembly = Assembly.LoadFrom(filePath);
        //    Type classSampleType = null;
        //    RBase rb = null;
        //    switch (className)
        //    {
        //        case "RCenter":
        //            classSampleType = classSampleAssembly.GetType("Approve.RuleCenter.RCenter");
        //            rb = Activator.CreateInstance(classSampleType) as RCenter;
        //            break;
        //        case "RQuali":
        //            classSampleType = classSampleAssembly.GetType("Approve.RuleCenter.RQuali");
        //            rb = Activator.CreateInstance(classSampleType) as RQuali;
        //            break;
        //        case "RQualiSX":
        //            classSampleType = classSampleAssembly.GetType("Approve.RuleCenter.RQualiSX");
        //            rb = Activator.CreateInstance(classSampleType) as RQualiSX;
        //            break;
        //        default:
        //            classSampleType = classSampleAssembly.GetType("Approve.RuleCenter.RCenter");
        //            rb = Activator.CreateInstance(classSampleType) as RCenter;
        //            break;
        //    }


        //    string FId = "";
        //    int Count = 0;
        //    int RowCount = grid.Items.Count;
        //    for (int i = 0; i < RowCount; i++)
        //    {
        //        CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
        //        if (cbx.Checked)
        //        {
        //            FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
        //            Count++;

        //        }
        //    }
        //    if (Count == 0)
        //    {
        //        this.showMessage("请选择");
        //        return;
        //    }

        //    //删除数据操作

        //    for (int i = 0; i < RowCount; i++)
        //    {
        //        CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
        //        if (cbx.Checked)
        //        {
        //            FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
        //            classSampleType.InvokeMember(fMothod, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, rb, new object[] { FId });
        //        }
        //    }
        //}
        #endregion

        //判断Session失效后,执行JavaScript
        public void ExceScript(string sessionName, string script)
        {
            if (Session[sessionName] == null)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<scritp>" + script + "</scritp>");
            }
        }


        /// <summary>
        /// 按字节截取字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public string staticStringbSubstring(string s, int length)
        {
            string v = "";
            if (!string.IsNullOrEmpty(s))
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
                int n = 0;　//　表示当前的字节数
                int i = 0;　//　要截取的字节数
                for (; i < bytes.GetLength(0) && n < length; i++)
                {
                    //　偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
                    if (i % 2 == 0)
                    {
                        n++;　　　//　在UCS2第一个字节时n加1
                    }
                    else
                    {
                        //　当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                        if (bytes[i] > 0)
                        {
                            n++;
                        }
                    }
                }
                //　如果i为奇数时，处理成偶数
                if (i % 2 == 1)
                {
                    //　该UCS2字符是汉字时，去掉这个截一半的汉字
                    if (bytes[i] > 0)
                        i = i - 1;
                    //　该UCS2字符是字母或数字，则保留该字符
                    else
                        i = i + 1;
                }
                v = System.Text.Encoding.Unicode.GetString(bytes, 0, i).ToString();
            }
            return v;
        }

        /// <summary>
        /// 按字节截取字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="length">长度</param>
        /// <param name="str">超出长度时截完后面加str字符串</param>
        /// <returns></returns>
        public string staticStringbSubstring(string s, int length, string str)
        {
            string v = "";
            if (!string.IsNullOrEmpty(s))
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
                int n = 0;　//　表示当前的字节数
                int i = 0;　//　要截取的字节数
                for (; i < bytes.GetLength(0) && n < length; i++)
                {
                    //　偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
                    if (i % 2 == 0)
                    {
                        n++;　　　//　在UCS2第一个字节时n加1
                    }
                    else
                    {
                        //　当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                        if (bytes[i] > 0)
                        {
                            n++;
                        }
                    }
                }
                //　如果i为奇数时，处理成偶数
                if (i % 2 == 1)
                {
                    //　该UCS2字符是汉字时，去掉这个截一半的汉字
                    if (bytes[i] > 0)
                        i = i - 1;
                    //　该UCS2字符是字母或数字，则保留该字符
                    else
                        i = i + 1;
                }

                v = System.Text.Encoding.Unicode.GetString(bytes, 0, i).ToString();
                if (bytes.Length > length)
                {
                    v += str;
                }
            }

            return v;
        }


        /// <summary>
        /// 得到服务器地址
        /// </summary>
        /// <returns></returns>
        public string getHttpUrl()
        {
            string str = "";
            string first = "http://" + Context.Request.Headers["Host"];

            string root = Context.Request.ApplicationPath;
            if (root != "/")
                root += "/";
            str = first + root;

            return str;
        }
    }
}