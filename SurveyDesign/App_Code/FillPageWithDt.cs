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
/// <summary>
/// FillPageWithDt 的摘要说明
/// </summary>
public class FillPageWithDt: System.Web.UI.Page
{
    public  FillPageWithDt()
    { }

    public bool fillPageControl(DataTable o, System.Web.UI.Control container,string sign)
    {
        if (o != null)
        {
             for (int i = 0; i < o.Columns.Count; i++)
            {
                object value = o.Rows[0][i];

                //this.setValueToControl(container, "t_" + o.Columns[i].ColumnName, EConvert.ToString(value));
                if (value.GetType() == typeof(DateTime))
                {
                    this.setValueToControl(container, sign + o.Columns[i].ColumnName, EConvert.ToShortDateString(value));
                }
                else
                {
                    if (value.GetType() == typeof(bool))
                    {
                        setChkValueToControl(container, sign + o.Columns[i].ColumnName, EConvert.ToBool(value));
                        if (EConvert.ToBool(value))
                        {
                            value = 1;
                        }
                        else
                        {
                            value = 0;
                        }
                    }
                    this.setValueToControl(container, sign + o.Columns[i].ColumnName, EConvert.ToString(value));
                }
            }
        }
        return true;

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

	}
