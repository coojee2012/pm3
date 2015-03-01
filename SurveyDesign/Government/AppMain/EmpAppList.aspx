<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpAppList.aspx.cs" Inherits="Government_AppMain_EmpAppList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>非注册人员审核</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script>
        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret == "1") {
                form1.btnQuery.click()
            }
        }
        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";
            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);
            if (idvalue == "1") {
                form1.btnQuery.click();
            }
        }
        function app(url) {
            var chkColl = document.getElementsByTagName("input");
            var tmpVal = '';
            for (var i = 0; i < chkColl.length; i++) {
                if (chkColl[i].type == "checkbox"
                && chkColl[i].id.indexOf("CheckItem") > -1) {
                    if (!chkColl[i].disabled && chkColl[i].checked) {
                        var span = document.getElementById("span" + chkColl[i].id);
                        if (span) {
                            if (tmpVal.indexOf(span.attributes["name"].nodeValue + ",") == -1) {
                                tmpVal += span.attributes["name"].nodeValue + ",";
                            }
                        }
                    }
                }
            }
            var obj = new Object();
            if (tmpVal.length > 1) {
                tmpVal = tmpVal.substring(0, tmpVal.length - 1);
            }
            else {
                alert("请选择要审批的项");
                return false;
            }
            obj.name = '';
            obj.id = tmpVal;
            var dbSystemId = document.getElementById("dbSystemId");
            if (dbSystemId) {
                obj.fsystemid = dbSystemId.value;
            }
            //'批量审核';  
            ShowWindow(url + '?e=0', 700, 600, obj);
            return false
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Literal ID="lPostion" runat="server">审批</asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                姓名：
            </td>
            <td>
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
            <td class="t_r">
                审核状态：
            </td>
            <td class="t_l">
                <asp:DropDownList ID="ddlFState" runat="server">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="1" Selected="True">未审核</asp:ListItem>
                    <asp:ListItem Value="2">已打回</asp:ListItem>
                    <asp:ListItem Value="6">已审核</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_c" rowspan="2">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                    Text="查询" />
                <input id="btnClear" style="margin-left: 5px;" class="m_btn_w2" type="button" value="重置"
                    onclick="clearPage();" />
                <asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w2"
                    OnClick="btnOut_Click" Text="导出" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                企业名称：
            </td>
            <td>
                <asp:TextBox ID="txtFEntName" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
            </td>
            <td class="t_r">
                企业类型：
            </td>
            <td class="t_l">
                <asp:DropDownList ID="ddlFSystemId" runat="server">
                    <asp:ListItem Value="" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1554" Text="勘察单位"></asp:ListItem>
                    <asp:ListItem Value="1553" Text="设计单位"></asp:ListItem>
                    <asp:ListItem Value="1451" Text="审图机构"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 10px">
                <input id="btnBatchApp" type="button" value="批量审批" class="m_btn_w4" onclick="app('../AppQualiInfo/BackUpBatchEmp.aspx?e=0');" />
                <input id="btnBatchBack" type="button" value="批量打回" class="m_btn_w4" onclick="app('../AppQualiInfo/BackEntBatchEmp.aspx?e=0');" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="20px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <HeaderStyle Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="姓名" DataField="FName">
                <ItemStyle Wrap="False" HorizontalAlign="Center" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="企业名称" DataField="FEntName">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="身份证" DataField="FIdCard">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="卡号" DataField="FUserName">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="上报时间" DataField="FTime" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核状态" DataField="FState">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
        <tr>
            <td align="left" style="height: 32px">
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
