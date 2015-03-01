<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTBAseeAppList.aspx.cs" Inherits="Government_AppMain_HTBAseeAppList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>接件</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });

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
        function Request(strName) {
            var strHref = window.document.location.href;
            var intPos = strHref.indexOf("?");
            var strRight = strHref.substr(intPos + 1);

            var arrTmp = strRight.split("&");
            for (var i = 0; i < arrTmp.length; i++) {
                var arrTemp = arrTmp[i].split("=");

                if (arrTemp[0].toUpperCase() == strName.toUpperCase()) return arrTemp[1];
            }
            return "";
        }

        function app(url) {
            var tmpVal = '';
            $(":checkbox[id$=CheckItem]").each(function() {
                if ($(this).attr("checked")) {
                    var id = $("#span" + $(this).attr("id")).attr("name");
                    if (tmpVal.indexOf(id + ",") == -1) {
                        tmpVal += id + ",";
                    }
                }
            });

            var obj = new Object();
            if (tmpVal.length > 1) {
                tmpVal = tmpVal.substring(0, tmpVal.length - 1);
            }
            else {
                alert("请选择");
                return false;
            }
            obj.name = '';
            obj.id = tmpVal; 

            var dbSystemId = document.getElementById("dbSystemId");
            if (dbSystemId) {
                obj.fsystemid = dbSystemId.value;
            }

            //'批量审批'; 

            ShowWindow(url + '?e=0', 700, 600, obj);

            return false
        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                <asp:Literal ID="lPostion" runat="server">受理</asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                单位名称：
            </td>
            <td align="left">
                <asp:DropDownList ID="dbSystemId" runat="server" CssClass="m_txt" Width="100px" AutoPostBack="True"
                    OnSelectedIndexChanged="dbSystemId_SelectedIndexChanged" Enabled="false" Style="display: none;">
                </asp:DropDownList>
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
            </td>
            <td class="t_r">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
            </td>
            <td class="t_r" style='display: none'>
                主管部门：
            </td>
            <td class="t_r" style='display: none'>
                <asp:DropDownList ID="dbFManageDeptId" runat="server" Width="100px">
                </asp:DropDownList>
            </td>
            <td colspan="2" rowspan="2" style="text-align: center; padding-right: 10px">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                    Text="查询" />
                &nbsp;
                <input id="btnClear" class="m_btn_w2" style="margin-top: 3px;" type="button" value="重置"
                    onclick="clearPage();" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                合同类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FInt3" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                    <asp:ListItem Value="280" Text="项目勘察"></asp:ListItem>
                    <asp:ListItem Value="28001" Text="项目勘察见证"></asp:ListItem>
                    <asp:ListItem Value="291" Text="初步设计"></asp:ListItem>
                    <asp:ListItem Value="296" Text="施工图设计文件编制"></asp:ListItem>
                    <asp:ListItem Value="287" Text="勘察文件审查"></asp:ListItem>
                    <asp:ListItem Value="300" Text="施工图设计文件审查"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r">
                状&nbsp; 态：
            </td>
            <td>
                <asp:DropDownList ID="dbSeeState" runat="server" CssClass="m_txt" Width="100px">
                    <asp:ListItem Value="">请选择</asp:ListItem>
                    <asp:ListItem Value="0" Selected="True">未接件</asp:ListItem>
                    <asp:ListItem Value="1">已接件</asp:ListItem> 
                    <asp:ListItem Value="5">打回企业,重新上报</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r">
                <asp:Button ID="btnAccept" runat="server" CssClass="m_btn_w2" Text="接件" OnClientClick="return app('AcceptSeeOneReportInfo.aspx')" />
                <asp:Button ID="btnBack" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w2"
                    Text="退件" OnClientClick="return app('BackSeeOneReportInfo.aspx')" />
                <asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w2"
                    OnClick="btnOut_Click" Text="导出" />
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
            <asp:BoundColumn HeaderText="工程名称" DataField="FEmpName">
                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="单位名称" DataField="FEntName">
                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="合同类型">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="申报内容" DataField="FListId" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="申请类别" Visible="False" DataField="FTypeId">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="申请等级" Visible="False" DataField="FLevelId">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="是否主项" DataField="FIsPrime" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="当前所在位置" DataField="FSubFlowId">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="状态" DataField="FStateDesc" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="年度" DataField="FYear" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="上报时间" DataField="FReporttime" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSeeState" HeaderText="打印通知书" Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FSeeState" HeaderText="状态">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FAppTime" HeaderText="接/退件时间" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="批次" Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="向省政府发送" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FFResult" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FBarCode" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FBaseInfoId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FState" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FManageTypeId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FMeasure" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FLinkId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
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
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="98%" style="display: none">
        <tr>
            <td align="left" nowrap="nowrap" style="height: 16px">
                <font color="#339933">企业名称是绿色：准予受理；</font> &nbsp; <font color="#ff9900">企业名称是橙色：未接件；
                    &nbsp;</font><font color="#ff0000">企业名称是红色：不准予受理； &nbsp;</font> &nbsp; <font color="#b6589d">
                企业名称是紫色：打回企业，重新上报。
            </td>
        </tr>
    </table>
    <input id="HIsPostBack" runat="server" type="hidden" />
    </form>
</body>
</html>
