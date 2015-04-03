<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportList.aspx.cs" Inherits="Government_AppBHGD_SHDCList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上会导出</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">上会导出</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">年度
                </td>
                <td>

                    <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                </td>

                <td class="t_r">批次
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Batch" runat="server"></asp:DropDownList>

                </td>

                <td class="t_r">
                    工程名称
                </td>
                <td>

                </td>
                <td align="center" rowspan="3" colspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="BtnQuery" CssClass="m_btn_w2" />
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w4" Text="导出Excel" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   

                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>

    </form>
</body>
</html>
