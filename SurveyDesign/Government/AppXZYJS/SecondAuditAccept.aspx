<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecondAuditAccept.aspx.cs" Inherits="JSDW_ApplyXZYJS_AuditMain_SecondAuditAccept" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">

        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";

            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);

            if (idvalue == "1") {
                form1.btnQuery.click();
            }
        }
        function app(url) {
            var items = $("#JustAppInfo_List").find(".checkboxItem > input[type=checkbox][checked]");
            if (items.length < 1) {
                alert("当前未选择任何项");
                return false;
            } else if (items.length > 1) {
                alert("只能选择一项进行申报");
                return false;
            }
            var YJS_ID = $(items[0]).parent("span").attr("YJS_ID");
            var YWBM = $(items[0]).parent("span").attr("name");
            var fsubid = $(items[0]).parent("span").attr("fSubFlowId");
            showAddWindow(url + '?typeId=5&YJS_ID=' + YJS_ID + '&fSubFlowId=' + fsubid + '&YWBM=' + YWBM, 900, 600);
            return true;
        }
        function BackApp(url) {
            var items = $("#JustAppInfo_List").find(".checkboxItem > input[type=checkbox][checked]");
            if (items.length < 1) {
                alert("当前未选择任何项");
                return false;
            } else if (items.length > 1) {
                alert("只能选择一项进行打回");
                return false;
            }
            var tmpVal = $(items[0]).parent("span").attr("name");
            var fsubid = $(items[0]).parent("span").attr("fSubFlowId");
            showAddWindow(url + '?ftype=5&FLinkId=' + tmpVal + '&fSubFlowId=' + fsubid, 700, 600);
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" class="m_title" style="width: 98%;">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">选址复审</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">项目名称：</td>
                <td><asp:TextBox ID="txtXMMC" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <td class="t_r">项目编号：
                </td>
                <td><asp:TextBox ID="txtBH" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <td class="t_r">建设单位：</td>
                <td><asp:TextBox ID="txtJSDW" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>
                <%--<td class="t_r">项目编号：</td>
                <td><asp:TextBox ID="txtXMBH" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox></td>--%>
                <td rowspan="2" align="center">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="搜索" OnClick="btnQuery_Click" />
                </td>
            </tr>
            <tr>
                <td class="t_r">状态：</td>
                <td>
                    <asp:DropDownList ID="dbSeeState" runat="server" CssClass="m_txt" Width="150px">
                        <asp:ListItem Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">待复审</asp:ListItem>
                        <asp:ListItem Value="1">复审已通过</asp:ListItem>
                        <asp:ListItem Value="2">复审未通过</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">申请日期起：</td>
                <td><asp:TextBox ID="txtStart" runat="server" CssClass="m_txt Wdate" Width="150px" onfocus="WdatePicker({skin:'whyGreen' })"></asp:TextBox></td>
                <td class="t_r">申请日期止：</td>
                <td><asp:TextBox ID="txtEnd" runat="server" CssClass="m_txt Wdate" Width="150px" onfocus="WdatePicker({skin:'whyGreen' })"></asp:TextBox></td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                 <%--   <asp:Button ID="btnAccept" runat="server" CssClass="m_btn_w2" Text="审核" OnClientClick="return app('AcceptInfoGF.aspx')" />--%>
                    <input type="button" value="审核" class="m_btn_w2"  onclick="return app('FirstAcceptJieJian.aspx')" />
                    &nbsp;<input type="button" value="退件" class="m_btn_w2" onclick="return BackApp('BackAccept.aspx')" />
                <%--    &nbsp;<asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w2"
                        OnClick="btnOut_Click" Text="导出" />--%>
                </td>
                <td class="m_bar_r"></td>
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
                        <asp:CheckBox ID="CheckItem" CssClass="checkboxItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="项目编号" DataField="BH" HeaderStyle-Width="200px">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="项目名称" DataField="XMMC" HeaderStyle-Width="200px">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="项目地址" HeaderStyle-Width="150px" DataField="JSDZ">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="建设单位" HeaderStyle-Width="150px" DataField="JSDWMC">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AuditType" HeaderText="工程类型" HeaderStyle-Width="100"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="所属地" HeaderStyle-Width="100px" DataField="XMSDMC">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="业务状态" DataField="FStatedesc" HeaderStyle-Width="100px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="申请时间" HeaderStyle-Width="100px" DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
               <%-- <asp:BoundColumn HeaderText="审核意见" HeaderStyle-Width="100px" DataField="FIdea">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>--%>
                <asp:BoundColumn DataField="FFResult" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FBarCode" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FBaseInfoId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FManageTypeId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="YWBM" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FMeasure" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FLinkId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
            </Columns>
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
        <div class="d div1 tcen" style="width: 98%; margin: 0px auto;">
            <uc1:pager ID="Pager1" runat="server"></uc1:pager>
        </div>
    </form>
</body>
</html>
