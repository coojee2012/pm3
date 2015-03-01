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
    public enum SaveOptionEnum   //��������
    {
        Insert,
        Update,
        Unknown
    }

    /// <summary>
    /// pageTool ��ժҪ˵����
    /// </summary>
    public class pageTool : System.Web.UI.Page
    {
        /// <summary>
        /// ����ɾ����ί��
        /// </summary>
        /// <param name="FIdList">������</param>
        /// <param name="context">���ݿ���������ģ�ʹ���������һ��������ִ��</param>
        public delegate void DeletingDelegate(IList<string> FIdList, DataContext context);
        private string sign = "t_";
        private System.Web.UI.Page page = null;


        private string dsign = "t_";
        private string msign = "t_";

        //private string qdsign = "d_";
        //private string qmsign = "m_";
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="myPage">Ҫ������ҳ��</param>
        /// <param name="controlSign">Ҫ������ҳ��ؼ��ı�־</param>
        public pageTool(System.Web.UI.Page myPage, string controlSign)//��ʼ���ؼ���־
        {
            this.page = myPage;
            sign = controlSign;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="myPage">Ҫ������ҳ��</param>
        public pageTool(System.Web.UI.Page myPage)
        {
            this.page = myPage;
        }



        #endregion

        #region ���ת�����������
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

        #region ������ص�
        #region ���ҳ��
        /// <summary>
        /// ���ҳ��Ŀؼ�
        /// </summary>
        /// <param name="dr">�������ҳ���������</param>
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

        #region ��ȡҳ��ֵ
        /// <summary>
        /// �ѹ�ʱ������ҳ�����������ʱ��ȡҳ��Form�ڵ�һ���ؼ���������ݷ���һ������
        /// </summary>
        /// <returns>SortedList</returns>
        [Obsolete("�ѹ�ʱ��")]
        public SortedList getPageValue()
        {
            System.Web.UI.Control container = page.Form as System.Web.UI.Control;
            return getPageValue(container);
        }
        /// <summary>
        /// �ѹ�ʱ������ҳ�����������ʱ��ȡҳ��ָ������container�ڵ�һ���ؼ���������ݣ�����һ������
        /// </summary>
        /// <param name="container">����</param>
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
                for (int j = 0; j < myform.Controls[i].Controls.Count; j++) //�ڶ���
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
                    for (int n = 0; n < myform.Controls[i].Controls[j].Controls.Count; n++) //������
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
        /// ��ȡҳ��Form�ڵ�һ���ؼ����������,����TSourceָ�������͵ı������Ը���ֵ
        /// </summary>
        /// <typeparam name="TSource">����:�磺��CF_Sys_Dic�����ɲ�д����дʱ������obj������</typeparam>
        /// <param name="obj">ʵ���������Ե���Ҳ����</param>
        /// <returns>�Ѿ�����ֵ�ı���</returns>
        public TSource getPageValue<TSource>(TSource obj)
        {
            IList<string> RemoveKeys = new List<string>(); RemoveKeys.Remove("FID");
            return getPageValue<TSource>(obj, RemoveKeys);
        }

        /// <summary>
        /// ��ȡҳ��Form�ڵ�һ���ؼ����������,����typeָ�������͵ı�������ֵ,���� RemoveKeys ���ᱻ��ֵ
        /// </summary>
        /// <typeparam name="TSource">����:�磺��CF_Sys_Dic�����ɲ�д����дʱ������obj������</typeparam>
        /// <param name="obj">ʵ���������Ե���Ҳ����</param>
        /// <param name="RemoveKeys">Ҫ�ų�����Щ����</param>
        /// <returns>�Ѿ�����ֵ�ı���</returns>
        public TSource getPageValue<TSource>(TSource obj, IList<string> RemoveKeys)
        {
            System.Web.UI.Control container = page.Form as System.Web.UI.Control;
            return getPageValue(obj, container, RemoveKeys);
        }

        /// <summary>
        /// ��ȡҳ��ָ������container�ڵ�һ���ؼ���������ݣ�����TSourceָ�������͵ı������Ը���ֵ
        /// </summary>
        /// <typeparam name="TSource">����:�磺��CF_Sys_Dic�����ɲ�д����дʱ������obj������</typeparam>
        /// <param name="obj">ʵ���������Ե���Ҳ����</param>
        /// <param name="container">����</param>
        /// <returns>�Ѿ�����ֵ�ı���</returns>
        public TSource getPageValue<TSource>(TSource obj, System.Web.UI.Control container)
        {
            IList<string> RemoveKeys = new List<string>(); RemoveKeys.Remove("FID");
            return getPageValue(obj, container, RemoveKeys);
        }
        /// <summary>
        /// ��ȡҳ��ָ������container�ڵ�һ���ؼ���������ݣ�����TSourceָ�������͵ı������Ը���ֵ,���� RemoveKeys ���ᱻ��ֵ
        /// </summary>
        /// <typeparam name="TSource">����:�磺��CF_Sys_Dic�����ɲ�д����дʱ������obj������</typeparam>
        /// <param name="obj">ʵ���������Ե���Ҳ����</param>
        /// <param name="container">����</param>
        /// <param name="RemoveKeys">Ҫ�ų�����Щ����</param>
        /// <returns>�Ѿ�����ֵ�ı���</returns>
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

        #region ��ҳ��ؼ����WHERE�Ӿ�
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

        #region ��ȡҳ��ؼ�����
        private string getControlNameFromPage(string columnName)
        {
            return sign + columnName;
        }
        #endregion

        #region ��ҳ��ؼ���ȡ���ݱ�������
        private string getColumnsNameFromPage(string controlName)
        {
            return controlName.Substring(sign.Length).ToUpper();
        }
        #endregion

        #region �жϿؼ��Ƿ������Ա����
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



        #region ҳ�浯����Ϣ
        /// <summary>
        /// ҳ�浯����Ϣ������ת����ҳ��
        /// </summary>
        /// <param name="message">Ҫ��������Ϣ</param>
        public void showMessage(string message)
        {
            page.ClientScript.RegisterStartupScript(ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "')</script>");
        }
        /// <summary>
        /// ҳ�浯����Ϣ������ת����ҳ��
        /// </summary>
        /// <param name="message">Ҫ��������Ϣ</param>
        /// <param name="nextUrl">Ҫ��ת����ҳ���Url</param>
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
        [Obsolete("��ʱ��")]
        public string alert(string scriptstr)
        {
            return "<script>alert('" + scriptstr + "')</script>";
        }
        #endregion

        #region ��N�����ת
        /// <summary>
        /// ��N�����ת����ҳ
        /// </summary>
        /// <param name="url">��ҳ�ĵ�ַ</param>
        /// <param name="n">���������</param>
        public void goNextPageAfterLater(string url, int n)
        {
            page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "js", "<script>window.setTimeout(\"window.location='" + url + "'\"," + (n * 1000).ToString() + ")</script>");
        }
        #endregion





        #region �ϴ�ͼƬ
        /// <summary>
        /// �ϴ�ͼƬ
        /// </summary>
        /// <param name="postControl">�ϴ��ؼ�</param>
        /// <param name="aimFilePath">ͼƬ�ڷ�������Ҫ�洢�ģ����������Ŀ¼��·������\\images\\</param>
        /// <param name="maxLength">�����ϴ�������ֽ���</param>
        /// <param name="leftFileName">����ʱ�趨���Ƶı��</param>
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
                    this.showMessage("��ѡ����Ҫ�ϴ���ͼƬ��");
                    return false;
                }
                else if (fileLength > maxLength)
                {
                    this.showMessage("ͼƬ�ļ�Ӧ����" + (maxLength / 1024000).ToString() + "M���ڣ�");
                    return false;
                }
                string tempstr = mypost.Value.Substring(mypost.Value.LastIndexOf(".") + 1, 3);
                if (tempstr != "jpg" && tempstr != "JPG" && tempstr != "gif" && tempstr != "gif")
                {
                    this.showMessage("ֻ���ϴ�JPG��GIF��ʽ��ͼƬ��");
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

        #region ����ͼƬ��ʾ��С
        /// <summary>
        /// ����ͼƬ��ʾ�Ĵ�С
        /// </summary>
        /// <param name="?">ͼƬ�ؼ�</param>
        /// <param name="width">���ܳ����Ŀ��,��Ϊ0�ո��ݸ߶Ȱ���������</param>
        /// <param name="height">���ܳ����ĸ߶�,��Ϊ0�ո��ݿ�Ȱ���������</param>
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
        /// ����ɾ��DataGridѡ�е��С�ɾ���ύ֮ǰ����ί��DeletingDelegate�󶨵ķ���������ͬһ��������tool.DelInfoFromGrid(EntInfo_List, db.CF_Ent_BaseInfo, tool_Deleting);
        /// </summary>
        /// <typeparam name="TEntity">ʵ������</typeparam>
        /// <param name="grid">DataGrid</param>
        /// <param name="table">ʵ��</param>
        /// <param name="Deleting">����private void tool_Deleting(System.Collections.Generic.IList��string�� FIdList, System.Data.Linq.DataContext context)</param>
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
        /// ����ɾ��DataGridѡ�е��С�
        /// </summary>
        /// <typeparam name="TEntity">ʵ������</typeparam>
        /// <param name="grid">DataGrid</param>
        /// <param name="table">ʵ��</param>
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
                        //�����﷨�� in ('','',''),FIdList.ToArray().Contains(d.FID)
                        ParameterExpression param = Expression.Parameter(typeof(TEntity), "t");
                        Expression right = Expression.Constant(ovalue); // ֵ

                        Expression left = Expression.Property(param, property.Name);//t=>t.FID==
                        Expression filter = Expression.Equal(left, right); // ����
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
                this.showMessage("�ɹ�ɾ��" + FIdList.Count + "��");
            }
            else
            {
                this.showMessage("��ѡ��Ҫɾ������");
            }
        }
        /// <summary>
        /// ����ɾ��GridViewѡ�е��С�ɾ���ύ֮ǰ����ί��DeletingDelegate�󶨵ķ�����ʹ������ͬһ��������tool.DelInfoFromGrid(EntInfo_List, db.CF_Ent_BaseInfo, tool_Deleting);
        /// </summary>
        /// <typeparam name="TEntity">ʵ������</typeparam>
        /// <param name="gridView">GridView</param>
        /// <param name="table">ʵ��</param>
        /// <param name="Deleting">����private void tool_Deleting(System.Collections.Generic.IList��string�� FIdList, System.Data.Linq.DataContext context)</param>
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
        /// ����ɾ��GridViewѡ�е��С�
        /// </summary>
        /// <typeparam name="TEntity">ʵ������</typeparam>
        /// <param name="grid">GridView</param>
        /// <param name="table">ʵ��</param>
        public void DelInfoFromGrid<TEntity>(GridView gridView, Table<TEntity> table) where TEntity : class
        {
            DelInfoFromGrid(gridView, table, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid">DataGrid</param>
        /// <param name="en">ʵ��</param>
        /// <param name="className">����</param>
        /// <param name="fMothod">������</param>
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
        //        this.showMessage("��ѡ��");
        //        return;
        //    }

        //    //ɾ�����ݲ���

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

        //�ж�SessionʧЧ��,ִ��JavaScript
        public void ExceScript(string sessionName, string script)
        {
            if (Session[sessionName] == null)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<scritp>" + script + "</scritp>");
            }
        }


        /// <summary>
        /// ���ֽڽ�ȡ�ַ���
        /// </summary>
        /// <param name="s">�ַ���</param>
        /// <param name="length">����</param>
        /// <returns></returns>
        public string staticStringbSubstring(string s, int length)
        {
            string v = "";
            if (!string.IsNullOrEmpty(s))
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
                int n = 0;��//����ʾ��ǰ���ֽ���
                int i = 0;��//��Ҫ��ȡ���ֽ���
                for (; i < bytes.GetLength(0) && n < length; i++)
                {
                    //��ż��λ�ã���0��2��4�ȣ�ΪUCS2�����������ֽڵĵ�һ���ֽ�
                    if (i % 2 == 0)
                    {
                        n++;������//����UCS2��һ���ֽ�ʱn��1
                    }
                    else
                    {
                        //����UCS2����ĵڶ����ֽڴ���0ʱ����UCS2�ַ�Ϊ���֣�һ�������������ֽ�
                        if (bytes[i] > 0)
                        {
                            n++;
                        }
                    }
                }
                //�����iΪ����ʱ�������ż��
                if (i % 2 == 1)
                {
                    //����UCS2�ַ��Ǻ���ʱ��ȥ�������һ��ĺ���
                    if (bytes[i] > 0)
                        i = i - 1;
                    //����UCS2�ַ�����ĸ�����֣��������ַ�
                    else
                        i = i + 1;
                }
                v = System.Text.Encoding.Unicode.GetString(bytes, 0, i).ToString();
            }
            return v;
        }

        /// <summary>
        /// ���ֽڽ�ȡ�ַ���
        /// </summary>
        /// <param name="s">�ַ���</param>
        /// <param name="length">����</param>
        /// <param name="str">��������ʱ��������str�ַ���</param>
        /// <returns></returns>
        public string staticStringbSubstring(string s, int length, string str)
        {
            string v = "";
            if (!string.IsNullOrEmpty(s))
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
                int n = 0;��//����ʾ��ǰ���ֽ���
                int i = 0;��//��Ҫ��ȡ���ֽ���
                for (; i < bytes.GetLength(0) && n < length; i++)
                {
                    //��ż��λ�ã���0��2��4�ȣ�ΪUCS2�����������ֽڵĵ�һ���ֽ�
                    if (i % 2 == 0)
                    {
                        n++;������//����UCS2��һ���ֽ�ʱn��1
                    }
                    else
                    {
                        //����UCS2����ĵڶ����ֽڴ���0ʱ����UCS2�ַ�Ϊ���֣�һ�������������ֽ�
                        if (bytes[i] > 0)
                        {
                            n++;
                        }
                    }
                }
                //�����iΪ����ʱ�������ż��
                if (i % 2 == 1)
                {
                    //����UCS2�ַ��Ǻ���ʱ��ȥ�������һ��ĺ���
                    if (bytes[i] > 0)
                        i = i - 1;
                    //����UCS2�ַ�����ĸ�����֣��������ַ�
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
        /// �õ���������ַ
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