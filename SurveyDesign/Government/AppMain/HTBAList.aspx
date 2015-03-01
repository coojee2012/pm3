<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTBAList.aspx.cs" Inherits="Government_AppMain_HTBAList" %>

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

        function openWinNew(Url) {
            var rid = Math.random();
            var newopen = window.open(Url + "&rid=" + rid, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no");
            if (newopen && newopen != null) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }

        function app(url) {
            var chkColl = document.getElementsByTagName("input");
            var tmpVal = '';
            for (var i = 0; i < chkColl.length; i++) {
                if (chkColl[i].type == "checkbox" && chkColl[i].id.indexOf("CheckItem") > -1) {
                    if (!chkColl[i].disabled && chkColl[i].checked == true) {
                        var span = document.getElementById("span" + chkColl[i].id)

                        if (span) {
                            if (tmpVal.indexOf(span.attributes["name"].nodeValue + ",") == -1) {
                                tmpVal += span.attributes["name"].nodeValue + ",";
                            }

                            if (url.indexOf("BackEntBatch") > -1) {
                                var fisQuali = span.attributes["fisQuali"].nodeValue;
                                if (fisQuali == "3") {
                                    alert("[" + span.attributes["prjName"].nodeValue + "]备案号已经发放不能打回");
                                    return;
                                }
                            }
                            else if (url.indexOf("BackUpBatchPrjBase") > -1) {
                                var fisQuali = span.attributes["fisQuali"].nodeValue;
                                if (fisQuali == "1") {
                                    alert("[" + span.attributes["prjName"].nodeValue + "]需要填写备案编号不能批量审核或办结");
                                    return;
                                }
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
        <tr style="display: none">
            <td class="t_r">
                业务类型：
            </td>
            <td>
                <asp:DropDownList ID="dmType" runat="server" CssClass="m_txt" Width="126px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                备案单位：
            </td>
            <td>
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
            </td>
            <td class="t_r">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="120px"></asp:TextBox>
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
                合同类型：
            </td>
            <td colspan="3">
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
            <td class="t_r" style='display: none'>
                主管部门：
            </td>
            <td class="t_l" style='display: none'>
                <asp:DropDownList ID="dbFManageDeptId" runat="server" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="dbSelfAppState" runat="server" CssClass="m_txt" Visible="false">
                <asp:ListItem Value="">全部</asp:ListItem>
                <asp:ListItem Value="0">未审核</asp:ListItem>
                <asp:ListItem Selected="True" Value="1">已审核</asp:ListItem>
                <asp:ListItem Value="3">打回到下级</asp:ListItem>
                <asp:ListItem Value="2">打回企业</asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar" style="display: none">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 10px">
                <input id="btnBatchBack" type="button" value="批量打回" class="m_btn_w4" onclick="if(fIsCanBatchApp()){app('../AppQualiInfo/BackEntBatchPrjBase.aspx?e=0')}else{return false;}" />
                <input id="btnBatchApp" type="button" value="批量审批" class="m_btn_w4" onclick="if(fIsCanBatchApp()){app('../AppQualiInfo/BackUpBatchPrjBase.aspx?e=0')}else{return false;}" />
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
            <asp:TemplateColumn Visible="false">
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
            <asp:BoundColumn HeaderText="工程地址">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="合同类型">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="建设单位" DataField="FLeadName">
                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="left" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="备案单位" DataField="FEntName">
                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="left" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="上报时间" DataField="FReporttime" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核状态" Visible="False">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核时间" DataField="FAppTime" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="审核结果" DataField="FFResult">
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
    <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0" style="display: none">
        <tr>
            <td align="left" style="height: 32px">
                <font color="#339933">企业名称是绿色：已填写审批意见、并上报；</font> <font color="#ff9900">企业名称是橙色：已填写审批意见、未上报；</font>
                <font color="#000099">企业名称是蓝色：未填写审批意见、未上报；</font> <font color="#ff0000">企业名称是红色：打回企业；</font>
                <font color="#B6589D">企业名称是紫色：已经办结。</font>
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

