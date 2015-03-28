<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title>发证审核详情页面</title>
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
                    form1.btnShowInfo.click();
                }

            }

            function checkBoxSelect(fParent, arrayIds) {

                var con = document.getElementById(fParent);
                var ids = arrayIds.split(",");
                if (ids == null || ids.length === 0) {
                    return;
                }
                else {
                    for (var i = 0; i < ids.length; i++) {

                        document.getElementById(ids[i]).checked = con.checked;
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
                obj.className = "m_btn_w6";
                obj.value = '提交中，请等待';
                return true;
            }
        </script>
  
    <base target="_self" />
</head>
<body>
    <body style="margin-left: 5px; margin-right: 5px;">
        <form id="form1" runat="server">
            <table width="100%" align="center" class="m_bar">

                <tr>
                    <td class="m_bar_l">
                        <asp:HiddenField runat="server" ID="t_fSubFlowId" Value="" />
                        <asp:HiddenField runat="server" ID="t_YWBM" Value="" />
                        <asp:HiddenField runat="server" ID="t_fBaseInfoId" Value="" />
                    </td>
                    <td class="m_bar_m" style="text-align: right; padding-right: 3px">
                        <input id="HSeeReportInfo" type="button" runat="server" class="m_btn_w2" value="上报资料" onclick="window.close();" />
                        <input id="btnSave" type="button" runat="server" class="m_btn_w2" value="保存"   />
                        <input id="btnReport" runat="server" class="m_btn_w4"   type="button" value="上报审批" />
                        <input id="btnRefuse" type="button" runat="server" class="m_btn_w4" value="不通过"   onclick="window.close();" />
                        <input id="btnReturnJSDW" type="button" runat="server" class="m_btn_w6" value="回退建设单位"  />
                        <input id="btnReturn" type="button" runat="server" class="m_btn_w2" value="返回" onclick="window.close();" />&nbsp;
                    </td>
                    <td class="m_bar_r"></td>
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
            <div id="diva_1" margin-top=margin-top 4px"=4px">
                <table align="center" width="100%" class="m_title">
                    <tr>
                        <th>
                            各级审核意见：
                        </th>
                    </tr>
                </table>
                <asp:DataGrid ID="AppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                              HorizontalAlign="Center" Style="margin-top: 7px" Width="98%" >
                    <headerstyle cssclass="m_dg1_h" />
                    <itemstyle cssclass="m_dg1_i" />
                    <columns>
                        <asp:boundcolumn headertext="序号">
                            <itemstyle width="30px" />
                        </asp:boundcolumn>
                        <asp:boundcolumn headertext="审核环节" datafield="FRoleDesc">
                            <itemstyle cssclass="padLeft" />
                        </asp:boundcolumn>
                        <asp:boundcolumn headertext="审核人" datafield="FAppPerson">
                            <itemstyle cssclass="padLeft" />
                        </asp:boundcolumn>
                        <asp:boundcolumn headertext="审核部门" datafield="FCompany">
                            <itemstyle cssclass="padLeft" />
                        </asp:boundcolumn>
                        <asp:boundcolumn headertext="审核人职务" datafield="FFunction">
                            <itemstyle cssclass="padLeft" />
                        </asp:boundcolumn>
                        <asp:boundcolumn headertext="审核结果" datafield="FResultStr"></asp:boundcolumn>
                        <asp:boundcolumn datafield="FIdea" headertext="审核意见"></asp:boundcolumn>
                        <asp:boundcolumn datafield="FId" visible="False"></asp:boundcolumn>
                    </columns>
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
                            <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
                        </td>
                        <td class="t_r" style="height: 35px">
                            审核人单位
                        </td>
                        <td style="height: 35px">
                            <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt"
                                         MaxLength="15"></asp:TextBox>
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
                                <asp:listitem value="1">通过</asp:listitem>
                                <asp:listitem value="3">不通过</asp:listitem>
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
</body>
</html>
