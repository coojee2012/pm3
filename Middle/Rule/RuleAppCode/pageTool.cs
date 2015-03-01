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
    /// pageTool 的摘要说明。
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



        public pageTool()
        {
        }
        #endregion

        #region 填充转换过后的数据
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

        #region 数据相关的

        #region 给指定字段变色（施工许可证专用）

        /// <summary>
        /// 置空所有控件的颜色
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
        /// 给字段变色
        /// </summary>
        /// <param name="dr">用于填充页面的数据行</param>
        /// <param name="newColor">颜色</param>
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


        #region 填充页面
        /// <summary>
        /// 填充页面的控件
        /// </summary>
        /// <param name="dr">用于填充页面的数据行</param>
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
        /// 自动填充指定容器内的控件
        /// </summary>
        /// <param name="dr">用于填充页面的数据行</param>
        /// <param name="container">容器</param>
        /// <returns>是否填充成功</returns>
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

        #region 获取页面值
        /// <summary>
        /// 保存页面输入的数据时获取页面输入的数据
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
        public SortedList getBDValue()//自定义表单写值
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
                        //if (keys == "传阅"||keys=="")
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

        #region loaind 动画
        public void initloading()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" <script language=JavaScript type=text/javascript>");
            sb.Append(" document.write('<div id=loader_container><div id=loader>');");
            sb.Append(" document.write('<div align=center>页面正在加载中 ...</div>');");
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

        #region 页面弹出消息
        /// <summary>
        /// 页面弹出信息并且跳转到新页面
        /// </summary>
        /// <param name="message">要弹出的信息</param>
        public void showMessage(string message)
        {
            page.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('" + message + "')</script>");

        }
        /// <summary>
        /// 页面弹出信息并且跳转到新页面
        /// </summary>
        /// <param name="message">要弹出的信息</param>
        /// <param name="nextUrl">要跳转的新页面的Url</param>
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

        #region 过N秒后跳转
        /// <summary>
        /// 过N秒后跳转到新页
        /// </summary>
        /// <param name="url">新页的地址</param>
        /// <param name="n">间隔的秒数</param>
        public void goNextPageAfterLater(string url, int n)
        {
            page.RegisterStartupScript("js", "<script>window.setTimeout(\"window.location='" + url + "'\"," + (n * 1000).ToString() + ")</script>");
        }
        #endregion

        #region 填充页面的数据控件
        /// <summary>
        /// 用SQL填充页面的控件
        /// </summary>
        /// <param name="myControl">要填充的控件</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="isHaveNull">是否加入空的选择项</param> 
        public void bindControl(System.Web.UI.Control myControl, string sql, bool isHaveNull)
        {
            RBase rule = new RBase();
            if (myControl is System.Web.UI.WebControls.DropDownList)
            {
                ((System.Web.UI.WebControls.DropDownList)myControl).DataSource = rule.GetTable(sql);
                ((System.Web.UI.WebControls.DropDownList)myControl).DataBind();
                if (isHaveNull)
                {
                    System.Web.UI.WebControls.ListItem nullItem = new System.Web.UI.WebControls.ListItem("请选择...", "");
                    ((System.Web.UI.WebControls.DropDownList)myControl).Items.Insert(0, nullItem);
                }
            }
        }
        #endregion

        #region 填充页面的数据控件
        /// <summary>
        /// 用SQL填充页面的控件
        /// </summary>
        /// <param name="myControl">要填充的控件</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="isHaveNull">是否加入空的选择项</param> 
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

        #region 列表项删除

        public void DelInfoFromGrid(DataGrid grid, EntityTypeEnum en, string className)
        {
            RBase rb = null;
            switch (className)
            {
                case "RCenter":
                    rb = new RCenter();
                    break;
                case "dbOA"://oa库
                    rb = new OA();
                    break;
                case "dbShare"://share库
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
                this.showMessage("请选择");
                return;
            }

            //删除数据操作 
            for (int i = 0; i < RowCount; i++)
            {
                CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
                    rb.DelEBase(en, "fid='" + FId + "'", true);
                    this.showMessageAndRunFunction("删除成功!", "window.returnValue='1';");
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
                case "dbOA"://oa库
                    rb = new OA();
                    break;
                case "dbShare"://share库
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
                this.showMessage("请选择");
                return;
            }

            //删除数据操作 
            for (int i = 0; i < RowCount; i++)
            {
                CheckBox cbx = (CheckBox)grid.Items[i].Cells[0].Controls[1];
                if (cbx.Checked)
                {
                    FId = grid.Items[i].Cells[grid.Columns.Count - 1].Text.Trim();
                    rb.PExcute("delete from " + tabName + " where fid='" + FId + "'", true);
                    this.showMessageAndRunFunction("删除成功!", "window.returnValue='1';");
                }
            }

        }
        /// <summary>
        /// 删除列表项（支持多表、支持GridView和DataGrid）
        /// </summary>
        /// <param name="grid">控件对象</param>
        /// <param name="sl">表名和主键的集合</param>
        /// <param name="className">库名</param>
        public void DelInfoFromGrid(object obj, SortedList sl, string className)
        {
            #region 库名
            RBase rb = null;
            switch (className)
            {
                case "RCenter":
                    rb = new RCenter();
                    break;
                case "RCenterBackup":
                    rb = new RCenter(0);
                    break;
                case "dbOA"://oa库
                    rb = new OA();
                    break;
                case "dbShare"://share库
                    rb = new Share();
                    break;
                default:
                    rb = new RCenter();
                    break;
            }
            #endregion

            #region 控件
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
                        if (string.IsNullOrEmpty(FID))//GridView用以上方法可能取不到值，采用以下主键方法取
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
                        this.showMessage("删除失败");
                    }
                    else
                    {
                        if (rb.PExcute(sb.ToString()))
                            this.showMessage("删除成功");
                        else
                            this.showMessage("删除失败");
                    }
                    Count++;
                }
            }
            if (Count == 0)
            {
                this.showMessage("请选择要删除的项");
                return;
            }


        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid">DataGrid</param>
        /// <param name="en">实体</param>
        /// <param name="className">类名</param>
        /// <param name="fMothod">方法名</param>
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

                case "dbOA"://oa库
                    classSampleType = classSampleAssembly.GetType("Approve.RuleCenter.OA");
                    rb = Activator.CreateInstance(classSampleType) as OA;
                    break;
                case "dbShare"://share库
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
                this.showMessage("请选择");
                return;
            }

            //删除数据操作

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
        #region　关于表单的 2009-10-29
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
        //判断Session失效后,执行JavaScript
        public void ExceScript(string sessionName, string script)
        {
            if (Session[sessionName] == null)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<scritp>" + script + "</scritp>");
            }
        }
        /// <summary>
        /// 传输数据
        /// 将table中的数据转换到sortedList中，返回
        /// 如果为空，返回空
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
                    sl.Add(drSource.Table.Columns[j].ColumnName.ToUpper(), drSource[j]);//存储数据
                }
            }
            catch
            {
                return null;
            }
            return sl;
        }



        //时间差（天）
        public string TimeCha(DateTime dt1, DateTime dt2)
        {
            System.TimeSpan ts = dt2.Date - dt1.Date;
            string dt = ts.TotalDays.ToString();
            return dt;
        }


        /// <summary>
        /// 按字节截取字符串
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public string staticStringbSubstring(string s, int length)
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
            return System.Text.Encoding.Unicode.GetString(bytes, 0, i).ToString();
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

            string v = System.Text.Encoding.Unicode.GetString(bytes, 0, i).ToString();
            if (bytes.Length > length)
            {
                v += str;
            }

            return v;
        }
        /// <summary>
        /// 获得列名列表，
        /// </summary>
        /// <param name="tag">表名前缀 格式tab. 没有留空""</param>
        /// <param name="tablename">表名</param>
        /// <param name="except">排除的列</param>
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
