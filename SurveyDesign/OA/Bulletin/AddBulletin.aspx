<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="AddBulletin.aspx.cs" Inherits="OA_Bulletin_AddBulletin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公告发布</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <style type="text/css">
        </style>
<script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../../script/default.js"> 

    </script>
<script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>


    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });       
    </script>
    <script type="text/javascript" language="javascript">
    function onchick(){
           
            return true;
        
    }
    function addpreson(obj)
   {  
        var  str=document.getElementById(obj).value;
        var  s =showModalDialog("../main/CheckRole.aspx?PresonList="+str+"","","dialogWidth=400px;dialogHeight=400px");
 
        
         
        if (s!=null&&s!="undefined")
        {
            document.getElementById(obj).value=s;
            
        }
        else{
            return;
        } 
        
        document.getElementById('btnReload').click();
   }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="Label1" runat="server" Text="公告发布"></asp:Label>
            </th>
        </tr>
    </table>
   <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style=" text-align:right">
                <asp:Button ID="OFFDuty" runat="server" CssClass="m_btn_w4" Text="公告发布" OnClick="OFFDuty_Click" />
                <input id="exit" class="m_btn_w2" onclick="exitt();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="100%" align="center">
        <tr>
            <td class="t_r t_bg">
                公告标题：
            </td>
            <td>
                <asp:TextBox ID="t_FTitle" runat="server" CssClass="m_txt" Width="285px" TabIndex="3"></asp:TextBox><tt>*</tt>
                &nbsp; &nbsp;
                <asp:CheckBox ID="RadioButton1" runat="server" class="tdRight td14" /><span style="font-weight: bold;
                    font-size: 16px; height: 17px; color: Red">置顶 </span>&nbsp;<asp:CheckBox
                        ID="CheckBox1" runat="server" class="tdRight td14" /><span style="font-weight: bold;font-size: 16px; height: 17px; color: Red">上报审核
                </span>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                有效期：
            </td>
            <td>
                起始日期：<asp:TextBox CssClass="m_txt" ID="t_FDateOn" runat="server" onfocus="WdatePicker()"></asp:TextBox><tt>*</tt>
                &nbsp; &nbsp;&nbsp; 终止日期：<asp:TextBox CssClass="m_txt" ID="t_FDateOff" runat="server"
                    onfocus="WdatePicker()"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <%--<tr>
            <td class="t_r t_bg">
                消息类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FBulTypeId" runat="server">
                </asp:DropDownList><tt>*</tt>
            </td>
        </tr>--%>
        <tr>
            <td class="t_r t_bg">
                发布范围：
            </td>
            <td>
                <%-- <table border="0" class="" style="width:98%" >
                                <tr>
                                    <td class="" style="width: 75%">--%>
                <asp:TextBox CssClass="m_txt" ID="presonList" runat="server" Width="70%" TextMode="MultiLine"
                    ReadOnly="True"></asp:TextBox><tt>*</tt>
                <%-- </td>
                                    <td class="" style="width: 25%">--%>
                <input id="btnAddPreson" type="button" class="m_btn_w2" value="添加" onclick="addpreson('presonFID')" />
                <input id="presonFID" runat="server" type="hidden" />
                <%--</td>
                                </tr>
                            </table>--%>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                公告内容：
            </td>
            <td style="height: 220px">
                <input id="t_FContent" type="hidden" value="" name="content1" runat="server" class="m_txt"
                    style="width: 166px" enableviewstate="true" />
                    
                    
                    &nbsp;<iframe id="eWebEditor1"  src="../../eWebEditor/ewebeditor.htm?id=t_FContent&style=mini"
                    frameborder="0" scrolling="no" width="100%" height="100%" language="javascript"></iframe>
            </td>
        </tr>
    </table>
    <input type="button" style="display: none;" id="btnReload" runat="server" onserverclick="btnReload_ServerClick" />
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
function exitt()
{
window.returnValue=1;
window.close();
}

</script>

