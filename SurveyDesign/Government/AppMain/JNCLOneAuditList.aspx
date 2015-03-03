<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JNCLOneAuditList.aspx.cs" Inherits="Government_AppMain_JNCLOneAuditList" %>

<!DOCTYPE html>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        function BackApp(url) {
            var items = $("#JustAppInfo_List").find(".checkboxItem > input[type=checkbox][checked]");
            if (items.length < 1) {
                alert("当前未选择任何项");
                return false;
            } else if (items.length > 1) {
                alert("只能选择一项进行退回");
                return false;
            }
            var tmpVal = $(items[0]).parent("span").attr("name");
            var fsubid = $(items[0]).parent("span").attr("fSubFlowId");
            var obj = new Object();
            obj.name = '';
            obj.id = tmpVal;
            ShowWindow(url + '?ftype=10&FLinkId=' + tmpVal + '&fSubFlowId=' + fsubid, 700, 600, obj);
            return false;
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
            var tmpVal = $(items[0]).parent("span").attr("name");
            var fsubid = $(items[0]).parent("span").attr("fSubFlowId");
            var YWBM = $(items[0]).parent("span").attr("YWBM");
            ShowWindow(url + '?typeId=10&FLinkId=' + tmpVal + '&fSubFlowId=' + fsubid + "&YWBM=" + YWBM, 700, 600);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" class="m_title" style="width: 98%;">
            <tr>
                <th colspan="7">
                    节能材料初审
                </th>
            </tr>
            <tr>
                <td class="t_r t_bg">企业名称：
                </td>
                <td align="left" class="auto-style2">
                    <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">本级状态：
                </td>
                <td>
                    <asp:DropDownList ID="dbSeeState" runat="server" CssClass="m_txt" Width="100px">
                        <asp:ListItem Value="">--全部--</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">未审核</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="3" rowspan="4" style="text-align: left; padding-right: 10px; width: 150px;">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                        Text="查询" />
                    &nbsp;
                <input id="btnClear" class="m_btn_w2" style="margin-top: 3px;" type="button" value="重置"
                    onclick="clearPage();" />
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">产品名称：
                </td>
                <td align="left" class="auto-style2">
                    <asp:TextBox ID="txtCPMC" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">产品类别：
                </td>
                <td>
                    <asp:DropDownList ID="ddlCPLB" runat="server" CssClass="m_txt" Width="100px">
                    
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">接件时间起：
                </td>
                <td>
                    <asp:TextBox ID="t_Stime" onfocus="WdatePicker()" runat="server" Width="150px" CssClass="m_txt"></asp:TextBox>

                </td>
                <td class="t_r t_bg">接件时间止：
                </td>
                <td>
                    <asp:TextBox ID="t_Etime" onfocus="WdatePicker()" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">批次：
                </td>
                <td style="width: 220px;">
                    <asp:DropDownList ID="ddBatch" runat="server" CssClass="m_txt" Width="100px">
                    </asp:DropDownList>
                    <asp:Button ID="btnAddPc" runat="server" CssClass="m_btn_w4" Text="加入批次" OnClick="btnAddPc_Click" />
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:Button ID="btnAccept" runat="server" CssClass="m_btn_w2" Text="审核" OnClientClick="return app('AcceptInfoJNCL.aspx')" />
                    &nbsp;<asp:Button ID="btnBackNext" runat="server" CssClass="m_btn_w2" Text="打回" OnClick="btnBackNext_Click" />
                    &nbsp;<asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w2"
                        OnClick="btnOut_Click" Text="导出" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%" OnItemCommand="JustAppInfo_List_ItemCommand">
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
                 <asp:BoundColumn HeaderText="产品名称" DataField="SQCPMC" HeaderStyle-Width="150px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="产品类型" DataField="CPLBMC" HeaderStyle-Width="100px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="标识等级" DataField="BSDJMC" HeaderStyle-Width="100px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="企业名称" HeaderStyle-Width="150px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnQY" runat="server" CommandName="See" Text='<%#Eval("QYMC") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="上报日期" HeaderStyle-Width="100px" DataField="FReportTime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="本级状态" HeaderStyle-Width="100px" DataField="FStatedesc">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核意见" HeaderStyle-Width="100px" DataField="FIdea">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="批次" HeaderStyle-Width="80px" DataField="">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="专家意见查看" HeaderStyle-Width="80px" DataField="">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FFResult" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FBarCode" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FBaseInfoId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FManageTypeId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FMeasure" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FLinkId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="YWBM" Visible="False"></asp:BoundColumn>
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
