<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpReport.aspx.cs" Inherits="KcsjSgt_ApplyKCJSXSC_EmpReport" %>

<%@ Register TagPrefix="uc1" TagName="pager" Src="~/Common/pager.ascx" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>结论</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            //选择是否同意时切换要填写的内容
            $("#t_FResultInt").change(function() { tab(); });
            //是否要补正
            $("#t_FTxt1").change(function() { changeBZ(); });
        });

        //选择是否同意时切换要填写的内容
        function tab(v1) {
            var v = $("#t_FResultInt").val();
            if (v == undefined)
                v = v1;
            if (v != undefined) {
                $("table[id^=tab_]").hide();
                $("table[id=tab_" + v + "]").show();
            }
        }
        //是否要补正
        function changeBZ(s1) {
            var s = $("#t_FTxt1").val();
            if (s == undefined)
                s = s1;
            if (s != undefined) {
                if (s == "1") {

                    $("#div_BZ").show();
                }
                else {
                    $("#div_BZ").hide();
                }
            }
        }

        function checkInfo() {
            if ($("#t_FResultInt").val() == "7") {
                if (!confirm("确定审查结论为“补正材料”吗？这样建设单位将要重新办理审查业务。")) {
                    return false;
                }
            }

            if (!AutoCheckInfo())
                return false;

            if (!getLength(document.getElementById("t_FTxt3"), 100, '“违反工程建设强制性标准编号及条文编号”')) {
                return false;
            }
            if (!getLength(document.getElementById("t_FContent"), 100, '“审查意见”')) {
                return false;
            }
            return true;
        }

        function up() {
            var width = "554";
            var height = "234";
            var idvalue = window.showModalDialog('UploadPhoto.aspx?rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:no;')
            if (idvalue != null && idvalue == "1") {
                document.getElementById('btnShowFile').click();
            }
        }
        function doAdd() {
            showAddWindow('EmpReportAdd.aspx?FEmpBaseinfoId=<%= ViewState["FEmpBaseInfo"]%>&FAppId=<%=Request.QueryString["FAppId"] %>', 500, 350);
        }
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                结论
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg" width="20%">
                工程名称：
            </td>
            <td colspan="3">
                <asp:Literal ID="liPrjName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查人：
            </td>
            <td>
                <asp:Literal ID="t_FName" runat="server"></asp:Literal>
            </td>
            <td class="t_r t_bg">
                主要职责：
            </td>
            <td>
                <asp:Literal ID="t_FFunction" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查结论：
            </td>
            <td>
                <asp:DropDownList ID="t_FResultInt" runat="server">
                    <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                    <asp:ListItem Value="6" Text="合格"></asp:ListItem>
                    <asp:ListItem Value="3" Text="不合格"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                审查时间：
            </td>
            <td>
                <asp:TextBox ID="t_FAppTime" runat="server" CssClass="m_txt" onfocus="WdatePicker();"
                    Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                是否需要补正材料：
            </td>
            <td colspan="3">
                <div class="f_l">
                    <asp:DropDownList ID="t_FTxt1" runat="server">
                        <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="div_BZ" class="f_l" runat="server">
                    补正说明：
                    <asp:TextBox ID="t_FTxt2" runat="server" CssClass="m_txt" MaxLength="50" Width="380"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                违返强条数量：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FOrder" runat="server" CssClass="m_txt" onblur="isInt(this);"
                    MaxLength="4"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                违反工程建设强制性标准编号及条文编号：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_FTxt3" runat="server" CssClass="m_txt" MaxLength="10" TextMode="MultiLine"
                    Height="60" Width="320px"></asp:TextBox>
                <tt>*（100字内）</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                审查意见：
            </td>
            <td class="m_txt_M" colspan="3">
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Width="320px" MaxLength="50"
                    TextMode="MultiLine" Height="60"></asp:TextBox>
                <tt>*（50字内）</tt>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="t_bg" align="center">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
            </td>
        </tr>
    </table>
    <table id="tab_3" style="display: none;" class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" style="padding-left: 20px; border-right: none; color: Red;">
                问题列表：
            </td>
            <td class="t_r t_bg" style="border-left: none;">
                <input id="btnAdd" class="m_btn_w2" value="新增" type="button" onclick='doAdd();' runat="server" />
                <asp:Button ID="btnQuery" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="m_txt_M" style="border-left: none;">
                <asp:GridView ID="DGList" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" DataKeyNames="FId" EmptyDataText="暂时没有" Style="margin: 0px;"
                    OnRowCommand="DGList_RowCommand" OnRowDataBound="DGList_RowDataBound">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_select" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="30px" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="资料类型" DataField="FTxt1"></asp:BoundField>
                        <asp:BoundField HeaderText="图号/页码" DataField="FTxt2"></asp:BoundField>
                        <asp:BoundField HeaderText="初审（复审）意见" DataField="FContent">
                            <ItemStyle CssClass="t_l" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="问题类别" DataField="FRemark"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <HeaderStyle Width="80" />
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDel" CommandName="Del" runat="server" Text="删除" CommandArgument='<%#Eval("FID") %>'
                                    OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                附件信息
            </th>
        </tr>
    </table>
    <div style="width: 98%; margin: 0px auto;">
        <table class="m_dg1" width="100%" align="center">
            <tr class="m_dg1_h">
                <th style="width: 30px;">
                    序号
                </th>
                <th>
                    资料名称
                </th>
                 <th>
                    是否必需
                </th>
                <th style="width: 60px;">
                    已上传<br />
                    文件个数
                </th>
                <th style="width: 160px;">
                    <font color="green">是</font>/<font color="red">否</font> 上传
                </th>
            </tr>
            <asp:Repeater ID="rep_List" runat="server" OnItemDataBound="rep_List_ItemDataBound">
                <ItemTemplate>
                    <tr class="m_dg1_select">
                        <td>
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td class="t_l">
                            <%#Eval("FFileName")%>
                        </td>
                         <td>
                            <%#Eval("FIsMust")%>
                        </td>
                        <td>
                            <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="lit_Has" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <asp:Repeater ID="rep_File" runat="server" OnItemCommand="rep_File_ItemCommand">
                        <ItemTemplate>
                            <tr class="m_dg1_i">
                                <td colspan="6" class="t_l" style="padding-left: 50px;">
                                    (<%# Container.ItemIndex+1 %>)、 <a href='<%#Eval("FFilePath") %>' target="_blank"
                                        title="点击查看该文件">
                                        <%#Eval("FFileName")%>
                                    </a>
                                    <asp:LinkButton ID="btnDel" runat="server" Text="[删除]" CommandName="cnDel" CommandArgument='<%#Eval("FID") %>'
                                        OnClientClick="return confirm('确定要删除吗？');" Visible="false"></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
            showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
        </webdiyer:AspNetPager>
    </div>
    <input id="k_FBaseInfoId" type="hidden" runat="server" />
    <input id="j_FBaseInfoId" type="hidden" runat="server" />
    </form>
</body>
</html>
