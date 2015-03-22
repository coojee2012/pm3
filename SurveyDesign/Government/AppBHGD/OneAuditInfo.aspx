<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OneAuditInfo.aspx.cs"
    Inherits="Government_AppZLJDBA_OneAuditInfo" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>初审</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
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
                form1.btnShowInfo.click();
            }

        }

        function checkBoxSelect(fParent, arrayIds) {

            var con = document.getElementById(fParent);
            var Ids = arrayIds.split(",");
            if (Ids == null || Ids.length == 0) {
                return;
            }
            else {
                for (var i = 0; i < Ids.length; i++) {

                    document.getElementById(Ids[i]).checked = con.checked;
                }
            }
        }



        function getValue() {
            var obj = window.dialogArguments;
            document.getElementById("HFID").value = obj.id;
            document.getElementById("HFSystemId").value = obj.fsystemid;

        }


        function WriteInfo(obj) {
            obj.disabled = true;
            obj.className = "m_btn_w6"
            obj.value = '提交中，请等待';
            return true;
        }
    </script>

    <base target="_self"></base>
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_bar">
        
        <tr>
            <td class="m_bar_l">
                <asp:HiddenField  runat="server" ID="t_fSubFlowId" Value=""/>
                <asp:HiddenField  runat="server" ID="t_YWBM" Value=""/>
                <asp:HiddenField  runat="server" ID="t_fBaseInfoId" Value=""/>            
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 3px">
                <input id="HSeeReportInfo" type="button" runat="server" class="m_btn_w2" value="上报资料" onclick="window.close();" />
                <input id="btnSave" type="button" runat="server" class="m_btn_w2" value="保存" onserverclick="btnSave_Click" />
                <input id="btnReport" runat="server" class="m_btn_w4" onserverclick="btnAccept_Click"  type="button" value="上报审批" />
                <input id="btnRefuse" type="button" runat="server" class="m_btn_w4" value="不通过" onserverclick="btnEndApp_Click" onclick="window.close();" />
                <input id="btnReturnJSDW" type="button" runat="server" class="m_btn_w6" value="回退建设单位" onserverclick="btnBack_Click"/>
                <input id="btnReturn" type="button" runat="server" class="m_btn_w2" value="返回" onclick="window.close();" />&nbsp;
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="8">
                主管部门初审
            </th>
        </tr>
        <tr>
            <td class="t_r">
                项目名称：
            </td>
            <td>
                <asp:TextBox ID="t_ProjectName" ReadOnly="true" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="t_PrjItemName" ReadOnly="true" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r">
                工程类别：
            </td>
            <td>
                <asp:DropDownList ID="t_PrjItemType" runat="server" ReadOnly="true" CssClass="m_txt" Width="151px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                备案编号：
            </td>
            <td>
                <asp:TextBox ID="t_RecordNo" Enabled="false" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r">
                建设单位：
            </td>
            <td>
                <asp:TextBox ID="t_JSDW" ReadOnly="true" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r">
                建设地址：
            </td>
            <td colspan="3">
                    
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="224px" MaxLength="30" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div id="diva_1" margin-top: 4px">
        <table align="center" width="100%" class="m_title">
            <tr>
                <th>
                    各级审核意见：
                </th>
            </tr>
        </table>
        <asp:DataGrid ID="AppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Style="margin-top: 7px" Width="98%" OnItemDataBound="AppInfo_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核环节" DataField="FRoleDesc">
                    <ItemStyle CssClass="padLeft" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核人" DataField="FAppPerson">
                    <ItemStyle CssClass="padLeft" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核部门" DataField="FCompany">
                    <ItemStyle CssClass="padLeft" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核人职务" DataField="FFunction">
                    <ItemStyle CssClass="padLeft" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核结果" DataField="FResultStr"></asp:BoundColumn>
                <asp:BoundColumn DataField="FIdea" HeaderText="审核意见"></asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <table align="center" width="100%" class="m_title">
            <tr>
                <th colspan="4">
                    本级审核意见：
                </th>
            </tr>
            <tr>
                <td class="t_r">
                    审核人
                </td>
                <td style="height: 35px">
                    <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt" MaxLength="15" ></asp:TextBox>
                </td>
                <td class="t_r" style="height: 35px">
                    审核人单位
                </td>
                <td style="height: 35px">
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt" 
                        MaxLength="15" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">
                    审核人职务
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPersonJob" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
                </td>
                <td class="t_r">
                    审核时间
                </td>
                <td>
                    <asp:TextBox ID="txtFSeeTime" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">
                    审核结果
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_FResult" runat="server" Width="90px" 
                        AutoPostBack="True">
                        <asp:ListItem Value="1">通过</asp:ListItem>
                            <asp:ListItem Value="3">不通过</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r">
                    审核意见
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_FAppIdea" runat="server" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                        Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

<script language="javascript">
    if (document.getElementById("HFID").value == "INIT") {
        getValue();
    }
    else {
        document.getElementById("HFID").value = "";
    }
    if (document.getElementById("HFID").value != "") {
        document.getElementById("HFID1").value = document.getElementById("HFID").value;
        document.getElementById("btnShowInfo").click();
    }
    function checkAllItem() {
        var form = form1;
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == "checkbox" && !form.elements[i].disabled) {
                if (form.elements[i].id.indexOf('CheckItem') > -1) {
                    var e = form.elements[i];
                    e.checked = true;

                }
            }
        }
    }
    checkAllItem();
</script>

