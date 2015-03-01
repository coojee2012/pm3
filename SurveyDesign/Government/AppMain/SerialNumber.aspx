<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SerialNumber.aspx.cs" Inherits="Government_AppMain_SerialNumber" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>待办件</title>
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
        function init() {
            var btnExport = document.getElementById("btnExport");
            if (btnExport) {
                btnExport.onclick = function() {
                    var dbFBatchNoId = document.getElementById("dbFBatchNoId");
                    var FBatchNoId = "";
                    if (dbFBatchNoId) {
                        FBatchNoId = dbFBatchNoId.value;
                    }
                    window.open("ExportReport.aspx?fsystemid=" + Request("fsystemid") + "&ftypeid=" + Request("ftypeid") + "&FBatchNoId=" + FBatchNoId + "", "", ""); return false;
                };
            }
        }
        function openWinNew(Url) {
            var rid = Math.random();
            var newopen = window.open(Url + "&rid=" + rid, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no");
            if (newopen && newopen != null) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }
    </script>

</head>
<body onload="init()">
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                <asp:Literal ID="lPostion" runat="server">发放流水号</asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="159px"></asp:TextBox>
            </td>
            <td class="t_r">
                审查状态：
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server">
                    <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                    <asp:ListItem Value="0" Text="正在审查" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="1" Text="已审查完"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r">
                是否发放：
            </td>
            <td>
                <asp:DropDownList ID="ddlIsCreate" runat="server">
                    <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未发放"></asp:ListItem>
                    <asp:ListItem Value="1" Text="已发放"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_c">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                    Text="查询" />
                <input id="btnClear" style="margin-left: 5px;" class="m_btn_w2" type="button" value="重置"
                    onclick="clearPage();" />
                <asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w2"
                    OnClick="btnOut_Click" Text="导出" />
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 10px">
                <asp:Button ID="btnCreate" runat="server" CssClass="m_btn_w4" Text="自动生成" OnClick="btnCreate_Click" />
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w4" Text="保存全部" OnClick="btnSave_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
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
            <asp:BoundColumn HeaderText="工程名称" DataField="FPrjName">
                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="工程地址" DataField="FAddress">
                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" CssClass="padLeft"/>
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="建设单位" DataField="FJsEnt">
                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="left" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审图机构" DataField="FBaseName">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" HorizontalAlign="left" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="上报时间" DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="业务流水号">
                <ItemTemplate>
                    <asp:TextBox ID="txtFTxt8" runat="server" CssClass="m_txt" Text='<%# DataBinder.Eval(Container, "DataItem.FTxt8") %>'>></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="审查状态">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
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
    <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
        <tr>
            <td align="left" style="height: 32px">
                <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    <input id="HMTypeId" runat="server" type="hidden" />
    </form>
</body>
</html>

<script language="javascript">
    function fIsCanBatchApp() {
        //    if(document.getElementById("HMTypeId").value=="")
        //    {
        //        alert("请先查询出相同业务类型的数据,在批量审批");
        //        return false;
        //    }
        return true;
    }
</script>

