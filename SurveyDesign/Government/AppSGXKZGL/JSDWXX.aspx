<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JSDWXX.aspx.cs" Inherits="Government_AppSGXKZGL_JSDWXX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function openWinNew(Url) {
            var newopen = window.open(Url, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no");
            if (newopen) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }
        function addTKJLInfo() {
            var FAppId = document.getElementById("t_fLinkId").value;
            var btn = document.getElementById("btnRefresh");
            showAddWindow('TKJLInfo.aspx?FAppId=' + FAppId, 600, 500, btn);
        }
        function addYZInfo() {
            var FAppId = document.getElementById("t_fLinkId").value;
            var btn = document.getElementById("btnReload");
            var FPrjItemId = document.getElementById("t_PrjItemId").value;
            //var FPrjId = document.getElementById("t_FPrjId").value;
            //var FPrjItemId = document.getElementById("t_FPrjItemId").value;
            showAddWindow('YZInfo.aspx?FAppId=' + FAppId + "&FPrjItemId=" + FPrjItemId, 600, 450, btn);
        }
        function LockEmpInfo() {

            var FAppId = document.getElementById("t_fLinkId").value;
            var FPrjItemId = document.getElementById("t_PrjItemId").value;
            showApproveWindow('SDRYSC.aspx?FAppId=' + FAppId + "&FPrjItemId=" + FPrjItemId, 600, 450);
        }
    </script>
    <style type="text/css">
        .cBtn7 {
            height: 21px;
        }

        .auto-style3 {
            height: 23px;
        }
        .m_txt {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="95%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                <asp:Label ID="lbTitle" runat="server" Text="建设单位用户信息">建设单位用户信息</asp:Label>
            </th>
        </tr>
             <tr>
            <td class="t_r">
                单位名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_DWMC" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                单位地址：
            </td>
            <td colspan="3" class="auto-style3">
                    <asp:TextBox ID="t_Address" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
           
            <td class="t_r">
               单位性质：
            </td>
            <td >
                <asp:TextBox ID="t_DWXZ" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                所属地：
            </td>
            <td>
                <asp:TextBox ID="t_SSD" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                法人代表：
            </td>
            <td>
                <asp:TextBox ID="t_FRDB" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            

            </td>
            <td class="t_r">
                法人手机号：
            </td>
            <td>
              <asp:TextBox ID="t_FRSJH" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td class="t_r">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="t_LXR" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            

            </td>
            <td class="t_r">
                联系人电话：
            </td>
            <td>
              <asp:TextBox ID="t_LXRDH" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
               <tr>
            <td class="t_r">
                组织机构代码：
            </td>
            <td>
                <asp:TextBox ID="t_ZZJGDM" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            

            </td>
            <td class="t_r">
                营业执照注册号：
            </td>
            <td>
              <asp:TextBox ID="t_YYZZZCH" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
            <tr>
                <td class="t_r">
                电子邮件：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_Email" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
            </tr>
       
         
             <tr>
            <td class="t_r">
                备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_Memo" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>
       
    </table>
        <br />
       
     
 
        <input id="t_fLinkId" runat="server" type="hidden" />
        <input id="t_PrjItemId" runat="server" type="hidden" />
        <input id="t_fTypeId" runat="server" type="hidden" />
        <input id="t_fSubFlowId" runat="server" type="hidden" />
        <input id="t_fBaseInfoId" runat="server" type="hidden" />
        <input id="t_fProcessRecordID" runat="server" type="hidden" />
        <input id="t_fProcessInstanceID" runat="server" type="hidden" />
    </form>
</body>
</html>
