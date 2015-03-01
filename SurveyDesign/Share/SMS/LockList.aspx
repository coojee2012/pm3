<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LockList.aspx.cs" Inherits="Admin_mainother_SmsList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>消息服务监控</title>
      <asp:Link id="skin1" runat="server">
    </asp:Link>

   
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
 <script  type="text/javascript" language="javascript">
     function clearQuery() {
         document.getElementById("t_FAdmin").value = "";
         document.getElementById("t_FUserName").value = "";
         document.getElementById("t_FSubmitTime1").value = "";
         document.getElementById("t_FSubmitTime2").value = "";
         document.getElementById("t_FType").value = "0";
         
     }
 </script>

</head>
<body>
    <form id="form1" runat="server">
    
     <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                   管理员日志
                  </th>
        </tr>         <tr>
               <td class="t_r t_bg">管理员</td>
               <td>
                <asp:TextBox ID=t_FAdmin runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r t_bg">用户名</td>
               <td>
                <asp:TextBox ID=t_FUserName runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">状态</td>
               <td>
                <asp:DropDownList ID="t_FType" runat="server" CssClass="m_txt" ></asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    创建时间：
                </td>
                <td>
                    <asp:TextBox ID="t_FSubmitTime1" runat="server" CssClass="m_txt" Width="90px"
                        onfocus="WdatePicker()"></asp:TextBox>
                    至<asp:TextBox ID="t_FSubmitTime2" runat="server" CssClass="m_txt" Width="90px"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                  <input type="button" value="清空" class="m_btn_w2 " onclick="clearQuery();" />
                </td>
            </tr>
        </table>
        
        

        <asp:DataGrid ID="Dic_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
            Style="margin-top: 7px;" AutoGenerateColumns="False" OnItemDataBound="Dic_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
              
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FCreateName" HeaderText="管理员"></asp:BoundColumn>
                <asp:BoundColumn DataField="FUserName" HeaderText="用户名">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FLockNumber" HeaderText="锁号">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FState" HeaderText="状态"></asp:BoundColumn>
                <asp:BoundColumn DataField="FcreateTime" HeaderText="变更时间"></asp:BoundColumn>
               
                <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <table align="center" width="98%" height="30px;">
            <tr>
                <td>
                    <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
