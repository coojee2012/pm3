<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjFileAppPager1.aspx.cs"
    Inherits="Government_AppPage_PrjFileAppPager1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务审核</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../DateSelect/WdatePicker.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkInfo() {
            if (document.getElementById("tab_BJ") && $("#tab_BJ").css("dispaly") != "none") {
                if ($("#t_FResult").val() == "1")//同意审核
                {
                    if ($("#txtFPrjItemNo").val() == '') {
                        alert('请填写备案编号!');
                        return false;
                    }
                }
            }
            return AutoCheckInfo();
        }


        $(document).ready(function() {
            //选择卡 指向效果
            $(".tab_btn").hover(function() {
                if ($(this).attr("class") != "a tab_btn1")
                    $(this).attr("class", "tab_btn1");
            },
             function() {
                 if ($(this).attr("class") != "a tab_btn1")
                     $(this).attr("class", "tab_btn");
             });

            //选项卡 点击效果
            $("a[name^=a_]").click(function() {
                $(".tab_btn1").attr("class", "tab_btn");
                $(this).attr("class", "a tab_btn1");
                $("div[id^=div]").hide();
                $("#div" + $(this).attr("name")).show();
                $("#HbtnId").val($(this).attr("name"));

                //整理时序图
                if ($(this).attr("name") == "a_5") {
                    showsxt();
                }
            });

            //显示打回意见
            var boxs = $("#" + '<%=ckListIdea.ClientID %>').find(":checkbox");
            var idea = '';
            $(boxs).click(function() {
                var idea = '';
                $(boxs).each(function() {
                    if (this.checked) {
                        idea += $(this).parent().find("LABEL").text() + ";\r\n";
                    }
                });
                $("#txtFBackIdea").attr("value", idea);
            });

            var value = $("#HbtnId").val();
            if ($.trim(value) != "") {
                $("#" + value).click();
            }


        });
        function showdiv() {
            var v = $("#HbtnId").val();
            if (!v) { v = "a_1"; }
            $("div[id^=div]").hide();
            $("#div" + v).show();
            $("a[name=" + v + "]").attr("class", "tab_btn1");
        }

        function showsxt() {
            try {
                // 整理时序号，一定要放这第一个位置，否则会被下面的JS影响
                $("div.step_sp,div.step_jj").each(function() {
                    var divElem = document.getElementById($(this).prev().attr("id"));
                    var fheight = getHeight(divElem);
                    $(this).find(".step_xx").css("padding-top", (fheight - 93) + "px");
                });
                //时序图宽，以保不换行
                var w = 0;
                $("div[id^=step_0]").each(function() { w += getWidth(document.getElementById($(this).attr("id"))); });
                $("#table_divSXT").css("width", w + "px");
            } catch (e) { }
        }
    </script>

    <script type="text/javascript">
        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
            if (ret == "1") {
                form1.btnQuery1.click();
            }
        }
        function seeSignInfo(sUrl) {
            var fappId = '<%=ViewState["lid"] %>';
            showApproveWindow(sUrl + '&FAppId=' + fappId, 700, 400);
        }
        function selIterance() {
            showApproveWindow('../AppQualiInfo/Emprepeat.aspx?fbid=<%=ViewState["FBaseinfoId"] %>&fappid=<%=ViewState["lid"] %>', 996, 732);
        }
        function toMyUrl(obj) {
            if ('<%=this.ViewState["FBaseinfoId"] %>'.length <= 0) {
                alert('登录时间过长，信息过期！');
                return false;
            }
            if ('<%=this.ViewState["lid"] %>'.length <= 0) {
                return false;
            }
            var fbid = '<%=this.ViewState["FBaseinfoId"] %>';
            var fappid = '<%=this.ViewState["lid"] %>';

            if (obj == "1")//到人员查重 
            {
                showApproveWindow('../AppQualiInfo/Emprepeat.aspx?fbid=' + fbid + '&fappid=' + fappid, 996, 732);
            }
            else if (obj == "2")//到查看各级意见
            {
                showApproveWindow('../AppQualiInfo/ShowAppInfo.aspx?lid=' + fappid, 830, 500);
            }
            else if (obj == "3")//到查看市场行为
            {
                showApproveWindow('../AppEntAction/EntActions.aspx?fbid=' + fbid, 972, 800);
            }
        }
        function checkBackIdea() {
            var vStep = $("#ddlFStep").val();
            if (vStep == "") {
                alert('请选择要打回到的具体环节!');
                $("#ddlFStep").focus();
                return false;
            }
            if ($("#txtFBackIdea").val() == "") {
                alert("请填写打回意见！");
                return false;
            }
            return confirm('确认要打回到[' + $("#ddlFStep option:selected").text() + ']吗？');
        }
        function backToPre(obj) {
            var sUrl = '../AppQualiInfo/ShowAppInfoToBackPre.aspx?fTypeId=<%=ViewState["fTypeId"] %>&fRoleId=<%=Session["DFRoleId"] %>&lid=<%=ViewState["lid"] %>&forder=<%=ViewState["FOrder"] %>';
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:750px; dialogHeight:550px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
            if (ret == "1") {
                window.returnValue = "1";
                window.close();
            }
        }
        var openobj = window;
        if (typeof (window.dialogArguments) == "object") {
            this.openobj = window.dialogArguments;
        }
        function openWinNew(Url) {
            var newopen = openobj.open(Url, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no");
            if (newopen) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="8">
                主管部门审核
            </th>
        </tr>
        <tr>
            <td class="t_r">
                工程名称
            </td>
            <td style="color: red">
                <asp:Literal ID="t_FPrjItemName" runat="server"></asp:Literal>
            </td>
            <td class="t_r">
                业务类型
            </td>
            <td style="color: red">
                <asp:Literal ID="liter_FManageType" runat="server"></asp:Literal>
            </td>
            <td class="t_r">
                上报日期
            </td>
            <td style="color: red">
                <asp:Literal ID="liter_FReportDate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                <asp:Literal ID="liJsEnt" runat="server">建设单位</asp:Literal>
            </td>
            <td style="color: red">
                <asp:Literal ID="liter_FBaseName0" runat="server"></asp:Literal>
            </td>
            <td class="t_r">
                <asp:Literal ID="liEntType" runat="server">备案单位</asp:Literal>
            </td>
            <td style="color: red" colspan="5">
                <asp:Literal ID="liter_FBaseName" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <div class="tabBar" style="width: 98%; margin: 2px auto;">
        <div class="tabBar_l">
        </div>
        <a name="a_1" class="tab_btn" id="btnSPYJ" tabindex="0"><strong>审核意见</strong></a>
        <a name="a_2" class="tab_btn" id="btnDHQY" runat="server"><strong>打&nbsp;&nbsp;回</strong></a>
        <a class="tab_btn" runat="server" id="HSeeReportInfo"><strong>上报资料</strong></a>
        <a class="tab_btn" runat="server" id="HSeePrintInfo"><strong>资料打印</strong></a><a
            name="a_3" class="tab_btn" runat="server" id="btnLCT"><strong> 流程图</strong></a><a
                name="a_4" class="tab_btn" runat="server" id="btnSXT"><strong> 时序图</strong></a>
        <div class="tabBar_r">
        </div>
    </div>
    <div id="diva_1" style="display: none; margin-top: 4px">
        <table align="center" width="98%" class="m_title">
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
                <asp:BoundColumn HeaderText="申报内容" Visible="False">
                    <ItemStyle CssClass="padLeft" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核岗位" DataField="FRoleDesc">
                    <ItemStyle CssClass="padLeft" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核人" DataField="FAppPerson">
                    <ItemStyle CssClass="padLeft" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核人单位" DataField="FCompany">
                    <ItemStyle CssClass="padLeft" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核人职务" DataField="FFunction">
                    <ItemStyle CssClass="padLeft" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核日期" DataField="FAppTime" DataFormatString="{0:yyyy-MM-dd}">
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="审核结果" DataField="FResult"></asp:BoundColumn>
                <asp:BoundColumn DataField="FIdea" HeaderText="审核意见"></asp:BoundColumn>
                <asp:BoundColumn DataField="flistid" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ftypeid" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="flevelid" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <span style='display: none'>
            <asp:DataGrid ID="ReportInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                HorizontalAlign="Center" OnItemDataBound="ReportInfo_List_ItemDataBound" Style="margin-top: 7px"
                Width="98%">
                <HeaderStyle CssClass="m_dg1_h" />
                <ItemStyle CssClass="m_dg1_i" />
                <Columns>
                    <asp:BoundColumn HeaderText="序号">
                        <ItemStyle Width="50px" Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FEmpName" HeaderText="工程名称">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FEntName" HeaderText="勘察单位">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn Visible="false">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn Visible="false">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="是否暂定" Visible="false">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="是否通过">
                        <ItemTemplate>
                            <asp:DropDownList ID="dResult" runat="server" Width="60px">
                                <asp:ListItem Value="1">通过</asp:ListItem>
                                <asp:ListItem Value="3">不通过</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:TemplateColumn>
                    <asp:BoundColumn HeaderText="各级审查结果">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="备注" Visible="False">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="就位证书" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="预审" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FRId" HeaderText="FRId" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FIsQuali" HeaderText="FIsQuali" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="FListId" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="FTypeId" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="FLevelId" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </span>
        <table align="center" width="98%" class="m_title">
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
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
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
                    <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">
                    审核结果
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="t_FResult" runat="server" Width="90px" AutoPostBack="True"
                        OnSelectedIndexChanged="t_FResult_SelectedIndexChanged">
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
       
        <table align="center" width="98%" class="m_title" runat="server" id="tab_BJ" visible="false">
            <tr>
                <th colspan="4">
                    批文情况：<asp:Label ID="liTechnical" runat="server" ForeColor="Red"></asp:Label>
                </th>
            </tr>
            <tr>
                <td class="t_r">
                    批文号：
                </td>
                <td>
                    <asp:TextBox ID="txtFCertiNo" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r">
                    批文标题：
                </td>
                <td>
                    <asp:TextBox ID="txtFTitle" runat="server" CssClass="m_txt" MaxLength="50"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r">
                    批文内容：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtFContent" runat="server" TextMode="MultiLine" CssClass="m_txt"
                        Columns="60" Height="80px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
        </table>
         <table width="98%" align="center" class="m_bar" id="Table2">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    附件列表
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="aFile" runat="server" Text="上传附件" CssClass="m_btn_w4" OnClick="btnFileList_Click" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DG_FileList" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" Width="98%" OnItemDataBound="DG_FileList_ItemDataBound"
            OnItemCommand="DG_FileList_ItemCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="50px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="文件名称" ItemStyle-HorizontalAlign="Left">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="附件类型" Visible="false">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:ButtonColumn></asp:ButtonColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <table align="center" width="98%" style="margin-top: 10px">
            <tr>
                <td style="text-align: center">
                    <input id="btnSeeSign" runat="server" type="button" runat="server" class="m_btn_w6"
                        value="签字情况" visible="false" onclick="seeSignInfo('../../Common/Ent/SignAppEntList.aspx?e=1')" />&nbsp;
                    <asp:Button ID="btnSaveIdea" runat="server" CssClass="m_btn_w6" Text="保存意见" OnClick="btnSaveIdea_Click"
                        OnClientClick="return checkInfo()" />&nbsp;
                    <%--<input id="btnBackSubDept" runat="server" type="button" class="m_btn_w6" value="打回下级"
                        onclick="backToPre(this);" />--%>
                    <asp:Button ID="btnReport" runat="server" CssClass="m_btn_w6" Text=" 提 交 " OnClick="btnReport_Click"
                        UseSubmitBehavior="False" />
                    <asp:Button ID="btnFinallyApp" runat="server" CssClass="m_btn_w6" Text=" 办 结 " OnClick="btnFinallyApp_Click"
                        UseSubmitBehavior="False" />&nbsp;
                    <input type="button" id="btnReturn" runat="server" class="m_btn_w6" value=" 返 回 "
                        onclick="window.close()" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="diva_2" style="display: none">
        <table align="center" width="98%" class="m_title">
            <tr>
                <th colspan="4">
                    打回意见
                </th>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: top; padding-top: 5px" width="40%" rowspan="2">
                    <asp:CheckBoxList ID="ckListIdea" runat="server" CssClass="m_txt" RepeatColumns="1">
                    </asp:CheckBoxList>
                </td>
                <td colspan="2" style="vertical-align: top; padding-top: 5px" width="40%">
                    打回到：
                    <asp:DropDownList ID="ddlFStep" runat="server" Width="200px">
                    </asp:DropDownList>
                    <span style='color: Red'>*</span>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: top; padding-top: 5px">
                    打回意见：<asp:TextBox ID="txtFBackIdea" TextMode="MultiLine" Width="414px" runat="server"
                        CssClass="m_txt" Style="word-break: break-all; word-wrap: break-word; text-align: left"
                        Height="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="4">
                    <asp:Button ID="btnBackEnt" OnClientClick="return checkBackIdea();" runat="server"
                        Text="打回" CssClass="m_btn_w4" OnClick="btnBackEnt_Click1" />
                    <input type="button" id="Button1" runat="server" class="m_btn_w4" value="返回" onclick="window.close()" />
                </td>
            </tr>
        </table>
    </div>
    <div id="diva_3" style="display: none;">
        <table align="center" style="margin-top: 5px;" width="98%">
            <asp:Repeater ID="repSubFlow" runat="server">
                <HeaderTemplate>
                    <tr>
                        <td style="background: center url(../../image/Start.jpg) no-repeat; height: 60px;
                            text-align: center">
                            开始
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <img src="../../image/Flow.jpg" />
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style='background: url(../../image/Step<%#Eval("IsNow")%>.jpg) center  no-repeat;
                            height: 55px; text-align: center'>
                            <%#Eval("FName") %>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <img src='../../image/Flow.jpg' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td style="background: url(../../image/end.jpg) center  no-repeat; height: 60px;
                            text-align: center">
                            结束
                        </td>
                    </tr>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div id="diva_4">
        <table id="table_divSXT" align="center" style="margin: 10px; width: 1000px;">
            <tr>
                <td>
                    <div id="step_0" class="step_first">
                        <table>
                            <tr>
                                <td class="step_left">
                                    &nbsp;
                                </td>
                                <td class="step_word">
                                    名 &nbsp;称：企业上报<br />
                                    执行者：<asp:Literal ID="lit_step_EntName" runat="server"></asp:Literal><br />
                                    时 &nbsp;间：<asp:Literal ID="lit_step_EntReportDate1" runat="server"></asp:Literal><br />
                                </td>
                            </tr>
                        </table>
                        <div class="step_jt">
                            <span>&nbsp;</span>
                            <div>
                                <asp:Literal ID="lit_step_EntReportDate" runat="server"></asp:Literal>
                            </div>
                            <samp>
                                &nbsp;
                            </samp>
                        </div>
                    </div>
                    <asp:Repeater ID="rep_SXT" runat="server" OnItemDataBound="rep_SXT_ItemDataBound">
                        <ItemTemplate>
                            <asp:Literal ID="SXT_Step" runat="server"></asp:Literal>
                            <%--<div id='step_0<%#Eval("FOrder") %>' class="step_sp">
                                <div class="step_name">
                                    <%#Eval("FName")%>
                                </div>
                                <div class="step_xx">
                                </div>
                                <table id="step_table_content">
                                    <tr>
                                        <td class="step_left">
                                            &nbsp;
                                        </td>
                                        <td class="step_word">
                                            名 &nbsp;称：<b><%#Eval("FName")%></b><br />
                                            执行者：<%#Eval("FName")%><br />
                                            开始时间：<%#Eval("FName")%><br />
                                            结束时间：<%#Eval("FName")%><br />
                                        </td>
                                    </tr>
                                </table>
                                <div class="step_jt">
                                    <span>&nbsp;</span>
                                    <div>
                                        &nbsp;
                                    </div>
                                    <samp>
                                        &nbsp;
                                    </samp>
                                </div>
                            </div>--%>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </div>
    <input id="HSaveState" runat="server" type="hidden" />
    <asp:HiddenField ID="hfTechnical" runat="server" />
    <input id="HbtnId" runat="server" type="hidden" value="a_1" />
    <asp:Button ID="btnQuery1" runat="server" Text="刷新已有证书" Style="display: none" OnClick="btnQuery1_Click" />
    <a href="#" onclick="showApproveWindow1('../../Admin/main/EntCertiList1.aspx?fbid=<%=ViewState["FBaseinfoId"] %>&fsid=<%=ViewState["fsid"] %>',800,800)"
        style="display: none" id='a2'></a>
    </form>
</body>
</html>
