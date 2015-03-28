<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertifiList.aspx.cs" Inherits="Government_AppBHGD_FZList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发证审核列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });

        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret === "1") {
                form1.btnQuery.click();
            }
        }
        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";

            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);

            if (idvalue === "1") {
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

        function app1(url) {
            var tmpVal = '';
            $(":checkbox[id$=CheckItem]").each(function () {
                if ($(this).attr("checked")) {
                    var id = $("#span" + $(this).attr("id")).attr("name");
                    if (tmpVal.indexOf(id + ",") === -1) {
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

            return false;
        }
        function app(url) {
            var tmpVal = ''; var fsubid = '';
            var fbaseInfoid = '';
            var ferid = '';
            var fpid = '';
            var fMeasure = '';
            var cou = 0;
            var chkColl = document.getElementsByTagName("input");
            for (var i = 0; i < chkColl.length; i++) {
                if (chkColl[i].type === "checkbox" && chkColl[i].id.indexOf("CheckItem") > -1) {
                    if (!chkColl[i].disabled && chkColl[i].checked === true) {
                        cou += 1;
                        var span = document.getElementById("span" + chkColl[i].id);
                        if (span) {
                            if (tmpVal.indexOf(span.getAttribute("name") + ",") === -1) {
                                tmpVal += span.getAttribute("name") + ",";
                                fsubid += span.getAttribute("fSubFlowId") + ",";
                                fbaseInfoid += span.getAttribute("fBaseInfoId") + ",";
                                fpid += span.getAttribute("fpid") + ",";
                                ferid += span.getAttribute("ferid") + ",";
                                fMeasure += span.getAttribute("fMeasure") + ",";
                            }
                        }
                    }
                }
            }
            var obj = new Object();
            if (tmpVal.length > 1) {
                tmpVal = tmpVal.substring(0, tmpVal.length - 1);
                fsubid = fsubid.substring(0, fsubid.length - 1);
                fbaseInfoid = fbaseInfoid.substring(0, fbaseInfoid.length - 1);
                fpid = fpid.substring(0, fpid.length - 1);
                ferid = ferid.substring(0, ferid.length - 1);
                fMeasure = fMeasure.substring(0, fMeasure.length - 1);
            }
            else {
                alert("请选择一条标准化工地信息审核！");
                return false;
            }
            if (cou > 1 || cou <= 0) {
                alert("只能选择一条标准化工地信息审核！");
                return false;
            }
            if (fMeasure !== '5') {
                alert("非未复审案件，不能在此阶段处理！");
                return false;
            }
            obj.name = '';
            obj.id = tmpVal;
            ShowWindow(url + '?ftype=5&FLinkId=' + tmpVal + '&fSubFlowId=' + fsubid + '&fBaseInfoId=' + fbaseInfoid
                + '&fpid=' + fpid
                + '&ferid=' + ferid, 1000, 800, obj);
            return false;
        }

    </script>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">发证审核</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">工程名称
                </td>
                <td>
                  <asp:TextBox ID="txtFPrjItemName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">工程所属地
                </td>
                <td>
                    <input runat="server" />
                </td>
                <td align="center" rowspan="3" colspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" />
                </td>
            </tr>
            <tr>
                <td class="t_r">申报单位
                </td>
                <td>
                    <input runat="server" cssclass="m_txt" width="130px" />
                </td>
                <td class="t_r">年度
                </td>
                <td>
                    <input runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r">批次
                </td>
                <td></td>
                <td class="t_r">办理状态
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlState">
                        <asp:ListItem runat="server" Value="-1">--全部--</asp:ListItem>
                        <asp:ListItem runat="server" Value="0">未审核</asp:ListItem>
                        <asp:ListItem runat="server" Value="1">已审核</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:Button ID="btnAccept" runat="server" CssClass="m_btn_w2" Text="审核" OnClientClick="return app('FSAuditInfo.aspx')"  />
                    <asp:Button runat="server" CssClass="m_btn_w4" Text="分配批次" OnClientClick="return app('ChooseBatch.aspx')" />
                    <asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w4"
                        Text="导出Excel" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:DataGrid ID="gv_list" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
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
                        <asp:BoundColumn HeaderText="工程名称" DataField="ProjectName">
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="工程所属地" >
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="申报单位" DataField="JSDW">
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="申报日期" DataField="FReporttime" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="不良行为记录" >
                            <ItemStyle Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="上报时间" DataField="FSeeTime">
                            <ItemStyle Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="办理状态" DataField="FStateDesc">

                            <ItemStyle Font-Underline="False" Wrap="False"></ItemStyle>
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="FId" DataField="FId" Visible="False">
                            <ItemStyle Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
