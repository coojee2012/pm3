<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGTSC.aspx.cs" Inherits="JSDW_ApplySGXKZGL_SGTSC" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptidfalse2.ascx" TagName="govdeptid1" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>施工图审查信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function checkInfo() {
            var value = document.getElementById("t_BL").value;
            if (value == "1") {
                return AutoCheckInfo();
            } else {
                var ly = document.getElementById("t_YL").value;
                if (ly == null || ly == '') {
                    alert('必须填写理由！');
                    return false;
                }
            }
        }
        function addPrjItem() {
            var fid = document.getElementById("txtFId").value;
            var fPrjItemId = document.getElementById("t_FPrjItemId").value;
            if (fid == null || fid == '') {
                alert('请先保存上方的施工图审查信息！');
                return;
            }
            showAddWindow('SGTSCRY.aspx?fSGTSCId=' + fid + '&&fPrjItemId=' + fPrjItemId, 1000, 700);
            //  alert('dd')
        }
        function showTr1() {
            $("tr[name=tr1]").show();
            $('[cs=cs1]').each(function (i) {
                $("#" + this.id).attr("disabled", true);
            });
        }
        function hideTr1() {
            $("tr[name=tr1]").hide();
            $('[cs=cs1]').each(function (i) {
                $("#" + this.id).removeAttr("disabled");
            });
        }
        function change(value) {
            if (value == "1") {
                $("tr[name=tr1]").hide();
                $('[cs=cs1]').each(function (i) {
                    $("#" + this.id).removeAttr("disabled");
                });
            }
            else {
                $("tr[name=tr1]").show();
                //$("input").removeAttr("disabled");
                $('[cs=cs1]').each(function (i) {
                    $("#" + this.id).attr("disabled", true);
                });
            }
        }
        function selEnt(obj, tagId) {
            var qylx = "";
            if (tagId == "t_SGTSCJGId") {
                qylx = "109";
            } else if (tagId == "t_KCDWId") {
                qylx = "102";
            } else if (tagId == "t_SJDWId") {
                qylx = "103";
            }
            var url = "../project/EntListSel.aspx";
            url += "?qylx=" + qylx;
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }
    </script>
    <base target="_self"></base>
    <style type="text/css">
        .modalDiv {
            position: absolute;
            top: 1px;
            left: 1px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: gray;
            opacity: .50;
            filter: alpha(opacity=50);
        }

        .m_txt {
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField runat="server" ID="hf_FAppId" Value="" />
        <asp:HiddenField runat="server" ID="hf_FId" Value="" />
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="up_Main" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv" style="display: none;">
                    <div style="position: absolute; left: 40%; top: 50%; background-color: peru; border: solid 3px red;">
                        <table align="center">
                            <tr>
                                <td>
                                    <h1>正在保存数据</h1>
                                </td>
                                <td>
                                    <img src="../../image/load2.gif" alt="请稍候" /></td>
                            </tr>

                        </table>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>


        <div id="divSetup2" runat="server">
            <table width="98%" align="center" class="m_bar">
                <tr>
                    <td class="m_bar_l"></td>
                    <td></td>
                    <td class="t_r">
                        <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                    OnClientClick="return checkInfo();" />
                                <input id="txtFId" type="hidden" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                    <td class="m_bar_r"></td>
                </tr>
            </table>
            <table class="m_table" width="98%" align="center">
                <tr>
                    <td class="t_r t_bg" style="width: 18.8%;">办理选项：
                    </td>
                    <td colspan="1" style="width: 29%;">
                        <asp:DropDownList ID="t_BL" class="cc2" runat="server" CssClass="m_txt" onchange="change(this.value)" Width="60%">
                            <asp:ListItem Value="1">补填</asp:ListItem>
                            <asp:ListItem Value="0">不需要办理</asp:ListItem>
                            <asp:ListItem Value="2">以后补办</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr name="tr1">
                    <td class="t_r t_bg">理由： </td>
                    <td colspan="3">
                        <asp:TextBox ID="t_YL" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg" style="width: 18.8%;">施工图审查合格书编号：
                    </td>
                    <td colspan="1" style="width: 29%;">
                        <asp:TextBox ID="t_SGTSCHGSBH" cs="cs1" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                    </td>
                    <td class="t_r t_bg">项目编号：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_ProjectNo" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                        <input id="t_FPrjItemId" type="hidden" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td class="t_r t_bg">施工图审查机构名称：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_SGTSCJGMC" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                        <input type="hidden" runat="server" id="t_SGTSCJGId" value="" />
                        <asp:Button ID="btnAddEnt" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_SGTSCJGId');"
                            UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntSC_Click" Style="margin-bottom: 4px; margin-left: 5px;" />
                    </td>
                    <td class="t_r t_bg">施工图审查机构组织机构代码：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_SGTSCZZJGDM" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">审查完成日期：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="t_SCWCRQ" cs="cs1" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                    </td>

                </tr>
                <tr>
                    <td class="t_r t_bg">建设规模：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="t_ConstrScale" cs="cs1" runat="server" CssClass="m_txt" Width="638px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                        <tt>*</tt>
                    </td>

                </tr>
                <tr>
                    <td class="t_r t_bg">勘察单位名称：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_KCDWMC" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                        <input type="hidden" runat="server" id="t_KCDWId" value="" />
                        <asp:Button ID="btnAddEnt1" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_KCDWId');"
                            UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntKC_Click" Style="margin-bottom: 4px; margin-left: 5px;" />
                    </td>
                    <td class="t_r t_bg">勘察单位组织机构代码：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_KCDWZZJGDM" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">设计单位名称：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_SJDWMC" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                        <input type="hidden" runat="server" id="t_SJDWId" value="" />
                        <asp:Button ID="btnAddEnt2" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_SJDWId');"
                            UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntSJ_Click" Style="margin-bottom: 4px; margin-left: 5px;" />
                    </td>
                    <td class="t_r t_bg">设计单位组织机构代码：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_SJDWZZJGDM" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                    </td>
                </tr>
                <tr>
                    <td class="t_r t_bg">一次审查是否通过：
                    </td>
                    <td colspan="1">
                        <asp:DropDownList ID="t_YCSCSFTG" cs="cs1" runat="server" CssClass="m_txt" Width="203px">
                            <asp:ListItem Value="1">通过</asp:ListItem>
                            <asp:ListItem Value="0">不通过</asp:ListItem>
                        </asp:DropDownList><tt>*</tt>
                    </td>
                    <td class="t_r t_bg">一次审查时违反强条数：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_YCSCWFTS" cs="cs1" onblur="isInt(this)" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                    </td>

                </tr>
                <tr>
                    <td class="t_r t_bg">一次审查时违反的强条条目：
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="t_YCSCWFTM" cs="cs1" runat="server" CssClass="m_txt" Width="638px" Height="40px" TextMode="MultiLine"></asp:TextBox><tt>*</tt>
                    </td>
                </tr>

            </table>
            <table width="98%" align="center" class="m_bar">
                <tr>
                    <td class="m_bar_l"></td>
                    <td>勘察设计审图人员明细
                    </td>
                    <td class="t_r">
                        <asp:UpdatePanel ID="up25" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <input type="button" id="btnAdd" cs="cs1" runat="server" value="新增" class="m_btn_w2" onclick="addPrjItem();" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:Button ID="btnDel" cs="cs1" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                            OnClick="btnDel_Click" />
                        <asp:Button ID="btnReload" cs="cs1" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                    </td>
                    <td class="m_bar_r"></td>
                </tr>
            </table>
            <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
                HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px; margin-bottom: 1px;"
                Width="98%">
                <HeaderStyle CssClass="m_dg1_h" />
                <ItemStyle CssClass="m_dg1_i" />
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderStyle Width="30px" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckItem" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn HeaderText="序号">
                        <HeaderStyle Width="30px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="人员姓名" DataField="RYXM">
                        <ItemStyle Wrap="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="所属单位名称" DataField="DWMC">
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="所属单位组织机构代码" DataField="DWZZJGDM">
                        <ItemStyle Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
            <div style="padding-left: 1%">
                <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
                </webdiyer:AspNetPager>
            </div>
        </div>
    </form>
</body>
</html>
