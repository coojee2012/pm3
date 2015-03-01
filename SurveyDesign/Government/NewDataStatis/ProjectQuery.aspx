<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectQuery.aspx.cs" Inherits="Government_AppMain_ProjectQuery" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
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
            <th colspan="5">
                <asp:Literal ID="lPostion" runat="server">项目查询</asp:Literal>
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
            建设单位：</td>
            <td>
              <asp:TextBox ID="TextBox1" runat="server" CssClass="m_txt" Width="159px"></asp:TextBox></td>
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
                <asp:Button ID="btnExport" runat="server" CssClass="m_btn_w4" Text="导出审核表" Visible="False" />
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
            <asp:BoundColumn HeaderText="项目登记">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="项目勘察合同备案" >
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="勘察文件审查" DataField="FEntName">
                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="left" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="初步设计文件审查">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="施工图设计文件审查" >
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="查看详情" DataField="FFResult">
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
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
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

