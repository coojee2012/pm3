<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGXKZXX.aspx.cs" Inherits="Government_AppTFGGL_SGXKZXX" %>

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
                <asp:Label ID="lbTitle" runat="server" Text="施工许可证信息">施工许可证信息</asp:Label>
            </th>
        </tr>
        <tr>
           
            <td class="t_r">
               证书编号：
            </td>
            <td >
                <asp:TextBox ID="t_SGXKZBH" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                合同价格(万元)：
            </td>
            <td>
                <asp:TextBox ID="t_Price" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                发证日期：
            </td>
            <td>
                <asp:TextBox ID="t_FZTime" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            

            </td>
            <td class="t_r">
                发证机关：
            </td>
            <td>
              <asp:TextBox ID="t_FZJG" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
            <tr>
                <td class="t_r">
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_PrjItemName" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
            </tr>
        <tr>
            <td class="t_r">
                建设单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JSDW" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                建设地址：
            </td>
            <td colspan="3" class="auto-style3">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="400px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td class="t_r">
                建筑面积：
            </td>
            <td>
                <asp:TextBox ID="t_Area" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                建筑高度(米)：
            </td>
            <td>
                <asp:TextBox ID="t_Height" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
             <tr>
            <td class="t_r">
                勘察单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_KCDW" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                设计单位：
            </td>
            <td colspan="3" class="auto-style3">
                    <asp:TextBox ID="t_SJDW" runat="server" CssClass="m_txt" Width="400px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
               <tr>
            <td class="t_r">
                监理单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JLDW" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                施工单位：
            </td>
            <td colspan="3" class="auto-style3">
                    <asp:TextBox ID="t_SGDW" runat="server" CssClass="m_txt" Width="400px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
                    <tr>
            <td class="t_r">
                专业分包单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_ZYFBDW" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                劳务分包单位：
            </td>
            <td colspan="3" class="auto-style3">
                    <asp:TextBox ID="t_LWFBDW" runat="server" CssClass="m_txt" Width="400px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
               <tr>
            <td class="t_r">
                项目总监：
            </td>
            <td>
                <asp:TextBox ID="t_XMZJ" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                监理工程师：
            </td>
            <td>
                <asp:TextBox ID="t_JLGCS" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
              <tr>
            <td class="t_r">
                项目负责人：
            </td>
            <td>
                <asp:TextBox ID="t_XMFZR" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                执业资格：
            </td>
            <td>
                <asp:TextBox ID="t_ZYZG" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
               <tr>
            <td class="t_r">
                技术负责人：
            </td>
            <td>
                <asp:TextBox ID="t_JSFZR" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                安全负责人：
            </td>
            <td>
                <asp:TextBox ID="t_AQFZR" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
             <tr>
            <td class="t_r">
                开工日期：
            </td>
            <td>
                <asp:TextBox ID="t_StartDate" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                竣工日期：
            </td>
            <td>
                <asp:TextBox ID="t_EndDate" Enabled="false" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
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