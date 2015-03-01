<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XMSearch.aspx.cs" Inherits="Government_AppWY_XMSearch" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="~/common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目失去申请</title>
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
        function ShowXMPage(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret == "ok") {
                location.reload();
            }
        }
        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret == "ok") {
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

        function app1(url) {
            var tmpVal = '';
            $(":checkbox[id$=CheckItem]").each(function () {
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
        function app(url) {
            var tmpVal = ''; var fsubid = '';
            var fbaseInfoid = '';
            var ferid = '';
            var fpid = '';
            var fMeasure = '';
            var cou = 0;
            var chkColl = document.getElementsByTagName("input");
            for (var i = 0; i < chkColl.length; i++) {
                if (chkColl[i].type == "checkbox" && chkColl[i].id.indexOf("CheckItem") > -1) {
                    if (!chkColl[i].disabled && chkColl[i].checked == true) {
                        cou += 1;
                        var span = document.getElementById("span" + chkColl[i].id)
                        if (span) {
                            if (tmpVal.indexOf(span.getAttribute("name") + ",") == -1) {
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
                alert("请选择一条质量监督备案信息审核！");
                return false;
            }
            if (cou > 1 || cou <= 0) {
                alert("只能选择一条质量监督备案信息审核！");
                return false;
            }
            if (fMeasure != '0') {
                alert("非未初审案件，不能在此阶段处理！");
                return false;
            }
            obj.name = '';
            obj.id = tmpVal;
            ShowWindow(url + '?ftype=10&FLinkId=' + tmpVal + '&fSubFlowId=' + fsubid + '&fBaseInfoId=' + fbaseInfoid
                + '&fpid=' + fpid
                + '&ferid=' + ferid, 1000, 800, obj);
            return false
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">项目查询</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">项目名称：
                </td>
                <td align="left">

                    <asp:TextBox ID="txtXMMC" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">企业名称：
                </td>
                <td>
                    <asp:TextBox ID="txtQYMC" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>

                <td colspan="2" rowspan="4" style="text-align: center; padding-right: 10px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" Text="查询" OnClick="btnQuery_Click" />
                    &nbsp;
                <input id="btnClear" class="m_btn_w2" style="margin-top: 3px;" type="button" value="重置"
                    onclick="clearPage();" />
                </td>
            </tr>
            <tr>
                <td class="t_r">项目属地：
                </td>
                <td>
                    <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                </td>
                <td class="t_r">建设单位：
                </td>
                <td>
                    <asp:TextBox ID="txtJSDW" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>

                </td>
            </tr>

        </table>
        <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Style="margin-top: 6px; margin-bottom: 1px;"
            Width="98%" OnItemDataBound="dg_List_ItemDataBound" OnItemCommand="dg_List_ItemCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <%-- <asp:BoundColumn DataField="XMMC" HeaderText="项目名称">
                    <ItemStyle Wrap="false" />
                </asp:BoundColumn>--%>
                <asp:BoundColumn HeaderText="企业名称" DataField="FName">
                    <ItemStyle Wrap="false" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="项目名称">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnMC" runat="server" CommandName="See" Text='<%#Eval("XMMC") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle CssClass="t_l" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="项目属地">
                    <ItemStyle Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="JSDW" HeaderText="建设单位">
                    <ItemStyle Wrap="false" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="项目类型">
                    <ItemStyle Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="接管日期" DataField="SCJGRQ"  DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="XMBH" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="XMSD" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="XMLX" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="Fname" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
                <%--<asp:BoundColumn DataField="SCJGRQ" Visible="False"></asp:BoundColumn>--%>
                <%--<asp:BoundColumn DataField="PrjItemType" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ConstrType" Visible="False"></asp:BoundColumn>--%>
            </Columns>
        </asp:DataGrid>
        <div style="padding-left: 1%">
            <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            <br />
            <tt>注：</tt>
        </div>

        <input id="HIsPostBack" runat="server" type="hidden" />
    </form>
</body>
</html>
