<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectList.aspx.cs" Inherits="Government_AppBHGD_BLCXList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>办理查询</title>
     <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
      <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="9">
                    <asp:Literal ID="lPostion" runat="server">办理查询</asp:Literal>
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
                    <asp:TextBox ID="txt_prjName" runat="server"></asp:TextBox>
                </td>


                 <td class="t_r">
                    申报单位
                </td>
                <td>
                    <asp:TextBox ID="txt_deptName" runat="server"></asp:TextBox>
                </td>
               




                <td align="center" rowspan="3" colspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" />
                </td>


                
               
            </tr>
        </table>
    </form>
</body>
</html>
