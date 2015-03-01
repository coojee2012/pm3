using System;
using System.Collections;
using System.Web;
using System.Data;
using System.Configuration;
using Approve.EntityBase;
using Approve.RuleBase;
using Approve.RuleCenter;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Drawing;
namespace Approve.Common
{
    /// <summary>
    /// pageTool ��ժҪ˵����
    /// </summary>
    public class pageTool : System.Web.UI.Page
    {
        private string sign = "t_";
        private System.Web.UI.Page page = null;
        public RCenter rc = new RCenter();


        private string dsign = "t_";
        private string msign = "t_";

        private string qdsign = "d_";
        private string qmsign = "m_";
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



        public pageTool()
        {
        }
        #endregion

        #region ���ת�����������
        public void setPageDropList()
        {
            RBase rule = new RBase();
            System.Web.UI.HtmlControls.HtmlForm myform = (System.Web.UI.HtmlControls.HtmlForm)page.FindControl("Form1");
            for (int i = 0; i < myform.Controls.Count; i++)
            {
                if (myform.Controls[i].ID != null)
                {
                    string str = myform.Controls[i].ID;
                    string ss = str;
                    if (myform.Controls[i].ID.IndexOf(qdsign) > -1 && myform.Controls[i].ID != "")
                    {
                        System.Web.UI.Control control = myform.Controls[i];
                        if (control is System.Web.UI.WebControls.TextBox)
                        {
                            string sValue = ((System.Web.UI.WebControls.TextBox)control).Text;
                            //							((System.Web.UI.WebControls.TextBox)control).Text=rule.GetSignValue(EntityTypeEnum.EDic,"fname","fid='"+sValue+"' and fisdeleted=0 ");
                        }
                    }

                    if (myform.Controls[i].ID.IndexOf(qmsign) > -1 && myform.Controls[i].ID != "")
                    {
                        System.Web.UI.Control control = myform.Controls[i];
                        if (control is System.Web.UI.WebControls.TextBox)
                        {
                            string sValue = ((System.Web.UI.WebControls.TextBox)control).Text;
                            //							((System.Web.UI.WebControls.TextBox)control).Text=rule.GetSignValue(EntityTypeEnum.EManageDept,"ffullname","fid='"+sValue+"' and fisdeleted=0 ");
                        }
                    }
                }
            }

        }
        #endregion

        #region ������ص�

        #region ��ָ���ֶα�ɫ��ʩ�����֤ר�ã�

        /// <summary>
        /// �ÿ����пؼ�����ɫ
        /// </summary>
        public void fillPageContorlForeColorNull()
        {
            System.Web.UI.HtmlControls.HtmlForm myform = (System.Web.UI.HtmlControls.HtmlForm)page.FindControl("Form1");
            for (int i = 0; i < myform.Controls.Count; i++)
            {
                if (myform.Controls[i].ID != null)
                {
                    string str = myform.Controls[i].ID;
                    string ss = str;
                    if ((myform.Controls[i].ID.IndexOf(sign) > -1 && myform.Controls[i].ID != "" && this.isControlUsefull(myform.Controls[i].ID)))// || (myform.Controls[i].ID.IndexOf(dsign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) || (myform.Controls[i].ID.IndexOf(msign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) )
                    {
                        this.setValueToControlToForeColor(myform, myform.Controls[i].ID.ToString(), Color.Empty);
                    }
                }
            }
        }
        /// <summary>
        /// ���ֶα�ɫ
        /// </summary>
        /// <param name="dr">�������ҳ���������</param>
        /// <param name="newColor">��ɫ</param>
        /// <returns></returns>
        public bool fillPageControlToForeColor(SortedList sl, Color newColor)
        {
            try
            {
                if (sl.Count > 0)
                {
                    System.Web.UI.HtmlControls.HtmlForm myform = (System.Web.UI.HtmlControls.HtmlForm)page.FindControl("Form1");
                    for (int i = 0; i < sl.Count; i++)
                    {
                        this.setValueToControlToForeColor(myform, this.getControlNameFromPage(sl.GetKey(i).ToString()), newColor);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void setValueToControlToForeColor(System.Web.UI.HtmlControls.HtmlForm myform, string controlName, Color newColor)
        {
            System.Web.UI.Control control = myform.FindControl(controlName);
            if (control != null)
            {
                if (control is System.Web.UI.WebControls.Label)
                {
                    ((System.Web.UI.WebControls.Label)control).ForeColor = newColor;
                }
                if (control is System.Web.UI.WebControls.TextBox)
                {
                    ((System.Web.UI.WebControls.TextBox)control).ForeColor = newColor;
                }
                if (control is System.Web.UI.WebControls.DropDownList)
                {

                    ((System.Web.UI.WebControls.DropDownList)control).ForeColor = newColor;

                }
                if (control is System.Web.UI.WebControls.RadioButtonList)
                {

                    ((System.Web.UI.WebControls.RadioButtonList)control).ForeColor = newColor;

                }
                if (control is System.Web.UI.WebControls.CheckBoxList)
                {

                    ((CheckBoxList)control).ForeColor = newColor;

                }
            }
        }
        #endregion


        #region ���ҳ��
        /// <summary>
        /// ���ҳ��Ŀؼ�
        /// </summary>
        /// <param name="dr">�������ҳ���������</param>
        /// <returns></returns>
        public bool fillPageControl(DataRow dr)
        {
            System.Web.UI.HtmlControls.HtmlForm myform = page.FindControl("Form1") as System.Web.UI.HtmlControls.HtmlForm;
            if (myform != null)
            {
                return fillPageControl(dr, myform);
            }
            return false;
        }
        /// <summary>
        /// �Զ����ָ�������ڵĿؼ�
        /// </summary>
        /// <param name="dr">�������ҳ���������</param>
        /// <param name="container">����</param>
        /// <returns>�Ƿ����ɹ�</returns>
        public bool fillPageControl(DataRow dr, System.Web.UI.Control container)
        {
            try
            {
                if (dr != null)
                {
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        if (dr[i].GetType().ToString() == "System.DateTime" && dr[i] != System.DBNull.Value && dr[i].ToString() != "")
                        {
                            this.setValueToControl(container, this.getControlNameFromPage(dr.Table.Columns[i].ColumnName), EConvert.ToShortDateString(dr[i]));
                        }
                        else
                        {
                            this.setValueToControl(container, this.getControlNameFromPage(dr.Table.Columns[i].ColumnName), dr[i].ToString());
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void setValueToControl(System.Web.UI.Control container, string controlName, string controlValue)
        {
            System.Web.UI.Control control = container.FindControl(controlName);
            if (control != null)
            {
                if (control is System.Web.UI.WebControls.Literal)
                {
                    ((System.Web.UI.WebControls.Literal)control).Text = controlValue;
                }
                if (control is System.Web.UI.WebControls.Label)
                {
                    ((System.Web.UI.WebControls.Label)control).Text = controlValue;
                }
                if (control is System.Web.UI.WebControls.TextBox)
                {
                    ((System.Web.UI.WebControls.TextBox)control).Text = controlValue;
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
                if (control is System.Web.UI.WebControls.CheckBox)
                {
                    ((System.Web.UI.WebControls.CheckBox)control).Checked = controlValue == "1";
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
        private void setChkValueToControl(System.Web.UI.HtmlControls.HtmlForm myform, string controlName, string controlValue)
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
        #endregion

        #region ��ȡҳ��ֵ
        /// <summary>
        /// ����ҳ�����������ʱ��ȡҳ�����������
        /// </summary>
        /// <returns></returns>
        public SortedList getPageValue()
        {
            return getPageValue(page.Form);
        }
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
            }
            return sl;
        }
        public SortedList getBDValue()//�Զ����дֵ
        {
            SortedList sl = new SortedList();
            for (int i = 0; i < page.Request.Form.Keys.Count; i++)
            {
                if (page.Request.Form.Keys[i] != null)
                {
                    string str = page.Request.Form.Keys[i].ToString();

                    if ((str.IndexOf(sign) > -1 && str != "" && this.isControlUsefull(str)))// || (myform.Controls[i].ID.IndexOf(dsign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) || (myform.Controls[i].ID.IndexOf(msign)>-1 && myform.Controls[i].ID!="" && this.isControlUsefull(myform.Controls[i].ID)) )
                    {
                        string keys = str.Replace(sign, "");
                        //if (keys == "����"||keys=="")
                        sl.Add(keys, page.Request.Form[str]);
                    }
                }
            }
            return sl;
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
                if (control is System.Web.UI.WebControls.CheckBox)
                {
                    sl.Add(this.getColumnsNameFromPage(control.ID), ((System.Web.UI.WebControls.CheckBox)control).Checked);
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

        #region loaind ����
        public void initloading()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" <script language=JavaScript type=text/javascript>");
            sb.Append(" document.write('<div id=loader_container><div id=loader>');");
            sb.Append(" document.write('<div align=center>ҳ�����ڼ����� ...</div>');");
            sb.Append(" document.write('<div id=loader_bg><div id=progress> </div></div>');");
            sb.Append(" document.write('</div></div>');");
            sb.Append(" var t_id = setInterval(animate,20);");
            sb.Append(" var pos=0;var dir=2;var len=0;");
            sb.Append(" function animate(){");
            sb.Append(" var elem = document.getElementById('progress');");
            sb.Append(" if(elem != null) {");
            sb.Append(" if (pos==0) len += dir;");
            sb.Append(" if (len>32 || pos>79) pos += dir;");
            sb.Append(" if (pos>79) len -= dir;");
            sb.Append(" if (pos>79 && len==0) pos=0;");
            sb.Append(" elem.style.left = pos;");
            sb.Append(" elem.style.width = len;");
            sb.Append(" }}");
            sb.Append(" function remove_loading() {");
            sb.Append(" this.clearInterval(t_id);");
            sb.Append(" var targelem = document.getElementById('loader_container');");
            sb.Append(" targelem.style.display='none';");
            sb.Append(" targelem.style.visibility='hidden';");
            sb.Append(" }");
            sb.Append(" </script>");
            this.page.ClientScript.RegisterStartupScript(this.page.GetType(), Guid.NewGuid().ToString(), sb.ToString());
        }
        #endregion

        #region ҳ�浯����Ϣ
        /// <summary>
        /// ҳ�浯����Ϣ������ת����ҳ��
        /// </summary>
        /// <param name="message">Ҫ��������Ϣ</param>
        public void showMessage(string message)
        {
            page.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('" + message + "')</script>");

        }
        /// <summary>
        /// ҳ�浯����Ϣ������ת����ҳ��
        /// </summary>
        /// <param name="message">Ҫ��������Ϣ</param>
        /// <param name="nextUrl">Ҫ��ת����ҳ���Url</param>
        public void showMessageAndGoNewPage(string message, string nextUrl)
        {
            page.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('" + message + "');window.location='" + nextUrl + "';</script>");
        }

        public void showMessageAndEndPage(string message)
        {
            page.Response.Write("<font color=red size=2>" + message + "</font>");
            page.Response.End();
        }
        public void showMessageAndRunFunction(string message, string functionName)
        {
            page.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('" + message + "');" + functionName + ";</script>");
        }
        public void showMessageAndRunFunction(string message, string functionName, string funName)
        {
            page.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('" + message + "');" + functionName + ";" + funName + ";</script>");
        }

        public void ExecuteScript(string scriptstr)
        {
            page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>" + scriptstr + "</script>");
        }
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
            page.RegisterStartupScript("js", "<script>window.setTimeout(\"window.location='" + url + "'\"," + (n * 1000).ToString() + ")</script>");
        }
        #endregion

        #region ���ҳ������ݿؼ�
        /// <summary>
        /// ��SQL���ҳ��Ŀؼ�
        /// </summary>
        /// <param name="myControl">Ҫ���Ŀؼ�</param>
        /// <param name="sql">SQL���</param>
        /// <param name="isHaveNull">�Ƿ����յ�ѡ����</param> 
        public void bindControl(System.Web.UI.Control myControl, string sql, bool isHaveNull)
        {
            RBase rule = new RBase();
            if (myControl is System.Web.UI.WebControls.DropDownList)
            {
                ((System.Web.UI.WebControls.DropDownList)myControl).DataSource = rule.GetTable(sql);
                ((System.Web.UI.WebControls.DropDownList)myControl).DataBind();
                if (isHaveNull)
                {
                    System.Web.UI.WebControls.ListItem nullItem = new System.Web.UI.WebControls.ListItem("��ѡ��...", "");
                    ((System.Web.UI.WebControls.DropDownList)myControl).Items.Insert(0, nullItem);
                }
            }
        }
        #endregion

        #region ���ҳ������ݿؼ�
        /// <summary>
        /// ��SQL���ҳ��Ŀؼ�
        /// </summary>
        /// <param name="myControl">Ҫ���Ŀؼ�</param>
        /// <param name="sql">SQL���</param>
        /// <param name="isHaveNull">�Ƿ����յ�ѡ����</param> 
        public void bindDicControl(System.Web.UI.Control myControl, string sKindId, bool isHaveNull)
        {
            RBase rule = new RBase();
            if (myControl is System.Web.UI.WebControls.DropDownList)
            {
                string sql = "select fid,fname from cf_sys_dic where fclassid='" + sKindId + "' order by forder ";
                ((System.Web.UI.WebControls.DropDownList)myControl).DataSource = rule.GetTable(sql);
                ((System.Web.UI.WebControls.DropDownList)myControl).DataTextField = "fname";
                ((System.Web.UI.WebControls.DropDownList)myControl).DataValueField = "fid";
                ((System.Web.UI.WebControls.DropDownList)myControl).DataBind();
                if (isHaveNull)
                {
                    System.Web.UI.WebControls.ListItem nullItem = new System.Web.UI.WebControls.ListItem("", "");
                    ((System.Web.UI.WebControls.DropDownList)myControl).Items.Insert(0, nullItem);
                }
            }
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

        #region �б���ɾ��

        public void DelInfoFromGrid(DataGrid grid, EntityTypeEnum en, string className)
        {
            RBase rb = null;
            switch (className)
            {
                case "RCenter":
                    rb = new RCenter();
                    break;
                case "dbOA"://oa��
                    rb = new OA();
                    break;
                case "dbShare"://share��
                    rb = new Share();
                    break;
                default:
                    rb = new RBase();
                    break;
            }
            string FId = "";
            int Count = 0;
            int RowCount = grid.Items.Count;
            for (int i = 0; i < RowCount; i++)
            {
                CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
                    Count++;
                }
            }
            if (Count == 0)
            {
                this.showMessage("��ѡ��");
                return;
            }

            //ɾ�����ݲ��� 
            for (int i = 0; i < RowCount; i++)
            {
                CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
                    rb.DelEBase(en, "fid='" + FId + "'", true);
                    this.showMessageAndRunFunction("ɾ���ɹ�!", "window.returnValue='1';");
                }
            }

        }
        public void DelInfoFromGrid(DataGrid grid, string tabName, string className)
        {
            RBase rb = null;
            switch (className)
            {
                case "RCenter":
                    rb = new RCenter();
                    break;
                case "dbOA"://oa��
                    rb = new OA();
                    break;
                case "dbShare"://share��
                    rb = new Share();
                    break;
                default:
                    rb = new RBase();
                    break;
            }
            string FId = "";
            int Count = 0;
            int RowCount = grid.Items.Count;
            for (int i = 0; i < RowCount; i++)
            {
                CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
                    Count++;
                }
            }
            if (Count == 0)
            {
                this.showMessage("��ѡ��");
                return;
            }

            //ɾ�����ݲ��� 
            for (int i = 0; i < RowCount; i++)
            {
                CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
                    rb.PExcute("delete from " + tabName + " where fid='" + FId + "'", true);
                    this.showMessageAndRunFunction("ɾ���ɹ�!", "window.returnValue='1';");
                }
            }

        }
        /// <summary>
        /// ɾ���б��֧�ֶ��֧��GridView��DataGrid��
        /// </summary>
        /// <param name="grid">�ؼ�����</param>
        /// <param name="sl">�����������ļ���</param>
        /// <param name="className">����</param>
        public void DelInfoFromGrid(object obj, SortedList sl, string className)
        {
            #region ����
            RBase rb = null;
            switch (className)
            {
                case "RCenter":
                    rb = new RCenter();
                    break;
                case "RCenterBackup":
                    rb = new RCenter(0);
                    break;
                case "dbOA"://oa��
                    rb = new OA();
                    break;
                case "dbShare"://share��
                    rb = new Share();
                    break;
                default:
                    rb = new RCenter();
                    break;
            }
            #endregion

            #region �ؼ�
            string controlType = obj.GetType().Name;
            DataGrid dg = null;
            GridView gv = null;
            switch (controlType)
            {
                case "DataGrid":
                    dg = (DataGrid)obj;
                    break;
                case "GridView":
                    gv = (GridView)obj;
                    break;
                default:
                    break;
            }

            #endregion

            string FID = "";
            int Count = 0;
            int RowCount = 0;
            if (dg != null)
            {
                RowCount = dg.Items.Count;
            }
            else if (gv != null)
            {
                RowCount = gv.Rows.Count;
            }

            for (int i = 0; i < RowCount; i++)
            {
                CheckBox cbx = new CheckBox();
                if (dg != null)
                {
                    cbx = (CheckBox)dg.Items[i].Cells[0].Controls[1];
                }
                else if (gv != null)
                {
                    cbx = (CheckBox)gv.Rows[i].Cells[0].Controls[1];
                }

                if (cbx.Checked)
                {
                    if (dg != null)
                    {
                        FID = dg.Items[i].Cells[dg.Columns.Count - 1].Text.Trim();
                    }
                    else if (gv != null)
                    {
                        FID = gv.Rows[i].Cells[gv.Columns.Count - 1].Text.Trim();
                        if (string.IsNullOrEmpty(FID))//GridView�����Ϸ�������ȡ����ֵ������������������ȡ
                        {
                            if (gv.DataKeys.Count > 0)
                            {
                                FID = gv.DataKeys[i].Value.ToString();
                            }
                        }
                    }
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < sl.Count; j++)
                    {
                        sb.Append("delete  " + sl.GetKey(j).ToString() + " where " + sl[sl.GetKey(j).ToString()].ToString() + "='" + FID + "'  ");
                    }
                    if (string.IsNullOrEmpty(sb.ToString()))
                    {
                        this.showMessage("ɾ��ʧ��");
                    }
                    else
                    {
                        if (rb.PExcute(sb.ToString()))
                            this.showMessage("ɾ���ɹ�");
                        else
                            this.showMessage("ɾ��ʧ��");
                    }
                    Count++;
                }
            }
            if (Count == 0)
            {
                this.showMessage("��ѡ��Ҫɾ������");
                return;
            }


        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid">DataGrid</param>
        /// <param name="en">ʵ��</param>
        /// <param name="className">����</param>
        /// <param name="fMothod">������</param>
        /// <returns></returns>
        public void DelInfoFromGrid(DataGrid grid, EntityTypeEnum en, string className, string fMothod)
        {
            string path = Server.MapPath("~/bin/");
            string filePath = path.Substring(0, path.LastIndexOf('\\')) + @"\RuleCenter.dll";
            Assembly classSampleAssembly = Assembly.LoadFrom(filePath);
            Type classSampleType = null;
            RBase rb = null;

            switch (className)
            {
                case "RCenter":
                    classSampleType = classSampleAssembly.GetType("Approve.RuleCenter.RCenter");
                    rb = Activator.CreateInstance(classSampleType) as RCenter;
                    break;

                case "dbOA"://oa��
                    classSampleType = classSampleAssembly.GetType("Approve.RuleCenter.OA");
                    rb = Activator.CreateInstance(classSampleType) as OA;
                    break;
                case "dbShare"://share��
                    classSampleType = classSampleAssembly.GetType("Approve.RuleCenter.Share");
                    rb = Activator.CreateInstance(classSampleType) as Share;
                    break;
                default:
                    classSampleType = classSampleAssembly.GetType("Approve.RuleCenter.RCenter");
                    rb = Activator.CreateInstance(classSampleType) as RCenter;
                    break;
            }


            string FId = "";
            int Count = 0;
            int RowCount = grid.Items.Count;
            for (int i = 0; i < RowCount; i++)
            {
                CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
                    Count++;

                }
            }
            if (Count == 0)
            {
                this.showMessage("��ѡ��");
                return;
            }

            //ɾ�����ݲ���

            for (int i = 0; i < RowCount; i++)
            {
                CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
                    classSampleType.InvokeMember(fMothod, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, rb, new object[] { FId });
                }
            }
        }
        #endregion
        #region�����ڱ��� 2009-10-29
        public bool fillBDControl(DataRow dr, string fieldList)
        {
            System.Web.UI.HtmlControls.HtmlForm myform = page.FindControl("Form1") as System.Web.UI.HtmlControls.HtmlForm;
            if (myform != null)
            {
                return fillBDControl(dr, myform, fieldList, "");
            }
            return false;
        }
        public bool fillBDControl(DataRow dr, string fieldList, string Ronly)
        {
            System.Web.UI.HtmlControls.HtmlForm myform = page.FindControl("Form1") as System.Web.UI.HtmlControls.HtmlForm;
            if (myform != null)
            {
                return fillBDControl(dr, myform, fieldList, Ronly);
            }
            return false;
        }
        public bool fillBDControl(DataRow dr, System.Web.UI.Control container, string fieldList, string Ronly)
        {
            try
            {
                if (dr != null)
                {
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        if (fieldList == null)
                        {
                            fieldList = "";
                        }
                        System.Web.UI.Control control_text = container.FindControl(this.getControlNameFromPage(dr.Table.Columns[i].ColumnName));
                        if (control_text != null)
                        {
                            if (control_text is System.Web.UI.WebControls.TextBox)
                            {
                                if (((System.Web.UI.WebControls.TextBox)control_text).TextMode.ToString().Trim() == "MultiLine")
                                {
                                    ((System.Web.UI.WebControls.TextBox)control_text).Height = 50;
                                }
                                ((System.Web.UI.WebControls.TextBox)control_text).Text = ((System.Web.UI.WebControls.TextBox)control_text).Text.Trim().Replace("<br />;&nbsp", "");
                            }
                        }
                        if (Ronly == "1")
                        {
                            this.setReadonlyToControl(container, this.getControlNameFromPage(dr.Table.Columns[i].ColumnName));
                        }
                        else
                        {
                            if (!fieldList.Replace("'", "").Split(',').Contains(dr.Table.Columns[i].ColumnName))
                            {
                                this.setReadonlyToControl(container, this.getControlNameFromPage(dr.Table.Columns[i].ColumnName));
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void setReadonlyToControl(System.Web.UI.Control container, string controlName)
        {
            System.Web.UI.Control control = container.FindControl(controlName);
            if (control != null)
            {
                if (control is System.Web.UI.WebControls.TextBox)
                {
                    ((System.Web.UI.WebControls.TextBox)control).ReadOnly = true;
                    ((System.Web.UI.WebControls.TextBox)control).Attributes.Remove("onfocus");
                    ((System.Web.UI.WebControls.TextBox)control).BackColor = Color.FromArgb(232, 232, 232);
                }
                if (control is System.Web.UI.WebControls.DropDownList)
                {

                    ((System.Web.UI.WebControls.DropDownList)control).Enabled = false;
                    ((System.Web.UI.WebControls.DropDownList)control).BackColor = Color.FromArgb(232, 232, 232);

                }
                if (control is System.Web.UI.WebControls.RadioButtonList)
                {
                    ((System.Web.UI.WebControls.RadioButtonList)control).Enabled = false;
                }
                if (control is System.Web.UI.WebControls.CheckBoxList)
                {
                    ((CheckBoxList)control).Enabled = false;
                }
                if (control is System.Web.UI.WebControls.Button)
                {
                    ((Button)control).Visible = false;
                }
                if (control is System.Web.UI.HtmlControls.HtmlGenericControl)
                {
                    ((HtmlGenericControl)control).Visible = false;
                }
                //if (control is System.Web.UI.WebControls.Image)
                //{
                //    ((System.Web.UI.WebControls.Image)control).ImageUrl = controlValue;
                //}
                //if (control is System.Web.UI.HtmlControls.HtmlInputHidden)
                //{
                //    ((System.Web.UI.HtmlControls.HtmlInputHidden)control).Value = controlValue;
                //}
                if (control is System.Web.UI.HtmlControls.HtmlSelect)
                {

                    ((System.Web.UI.HtmlControls.HtmlSelect)control).Disabled = false;

                }
                if (control is System.Web.UI.HtmlControls.HtmlTextArea)
                {
                    ((System.Web.UI.HtmlControls.HtmlTextArea)control).Disabled = false;
                }
                if (control is System.Web.UI.HtmlControls.HtmlInputText)
                {
                    ((System.Web.UI.HtmlControls.HtmlInputText)control).Disabled = false;
                }
                if (control is System.Web.UI.HtmlControls.HtmlInputImage)
                {
                    ((System.Web.UI.HtmlControls.HtmlInputImage)control).Disabled = false;
                }
            }
        }
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
        /// ��������
        /// ��table�е�����ת����sortedList�У�����
        /// ���Ϊ�գ����ؿ�
        /// </summary>
        /// <param name="dtSource"></param>
        public SortedList TranSferData(DataRow drSource)
        {
            if (drSource == null)
                return null;
            SortedList sl = new SortedList();
            try
            {
                for (int j = 0; j < drSource.Table.Columns.Count; j++)
                {
                    sl.Add(drSource.Table.Columns[j].ColumnName.ToUpper(), drSource[j]);//�洢����
                }
            }
            catch
            {
                return null;
            }
            return sl;
        }



        //ʱ���죩
        public string TimeCha(DateTime dt1, DateTime dt2)
        {
            System.TimeSpan ts = dt2.Date - dt1.Date;
            string dt = ts.TotalDays.ToString();
            return dt;
        }


        /// <summary>
        /// ���ֽڽ�ȡ�ַ���
        /// </summary>
        /// <param name="s">�ַ���</param>
        /// <param name="length">����</param>
        /// <returns></returns>
        public string staticStringbSubstring(string s, int length)
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
            return System.Text.Encoding.Unicode.GetString(bytes, 0, i).ToString();
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

            string v = System.Text.Encoding.Unicode.GetString(bytes, 0, i).ToString();
            if (bytes.Length > length)
            {
                v += str;
            }

            return v;
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="tag">����ǰ׺ ��ʽtab. û������""</param>
        /// <param name="tablename">����</param>
        /// <param name="except">�ų�����</param>
        /// <returns></returns>
        public string GetColumnName(string dbName, string tag, string tablename, string[] except)
        {
            StringBuilder sbd = new StringBuilder();
            string sql = string.Format("select 'Column_name' = name from syscolumns where id = (select id from sysobjects where id = object_id('{0}'))", tablename);
            RBase rq = new RCenter();
            if (dbName.ToLower() == "dbcenter")
                rq = new RCenter();
            DataTable dt = rq.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool find = false;
                string ColName = dt.Rows[i]["Column_name"].ToString();
                for (int j = 0; j < except.Length; j++)
                {
                    if (ColName.ToUpper() == except[j].ToUpper())
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    sbd.AppendFormat(",{1}{0}", ColName, tag);
                }
                else
                    continue;
            }
            return sbd.ToString();
        }
    }
}
