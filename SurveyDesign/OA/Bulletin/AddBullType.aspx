<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBullType.aspx.cs" Inherits="OA_Bulletin_AddBullType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公告类型发布</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <style type="text/css">
        </style>
    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"> </script>

    <script type="text/javascript" language="javascript">
    function onchick(){
        
            return true;
        
    }
    function addpreson(obj)
   {  
        var  str=document.getElementById(obj).value;
        var  s =showModalDialog("../main/CheckRole.aspx?PresonList="+str+"","","dialogWidth=608px;dialogHeight=635px");
 
        
         
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
<body style="font-family: Times New Roman">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="Label1" runat="server" Text="公告类型维护"></asp:Label>
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="OFFDuty" runat="server" CssClass="m_btn_w6" Text="添加公告类型" OnClick="OFFDuty_Click" />
                <input id="exit" class="m_btn_w2" onclick="exitt();" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                公告类型名称：
            </td>
            <td>
                <asp:TextBox ID="t_TypeName" runat="server" CssClass="m_txt" Width="285px" TabIndex="3"
                    MaxLength="49"></asp:TextBox>
            </td>
        </tr>
    </table>
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

