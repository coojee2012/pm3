<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddReport.aspx.cs" Inherits="JZDW_ApplyXMJZ_AddReport" %>

<%@ Register TagPrefix="uc1" TagName="pager" Src="~/Common/pager.ascx" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>见证人员意见</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();


            //选择是否同意时切换要填写的内容
            $("#t_FInt1").change(function() {
                tab();
            });
        });

        //选择是否同意时切换要填写的内容
        function tab() {
            var v = $("#t_FInt1").val();
            $("table[id^=tab_]").hide();
            $("table[id=tab_" + v + "]").show();
        }

        function checkInfo() {
            if (!AutoCheckInfo()) {
                return false;
            }
            if (!getLength(document.getElementById("t_FContent"), 50, '“见证意见”')) {
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
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                见证意见
            </th>
        </tr>
    </table>
    <asp:Literal ID="lit_TS" runat="server"></asp:Literal>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="3">
                <asp:Literal ID="liPrjName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" width="150">
                见证人员：
            </td>
            <td>
                <asp:Literal ID="li_FName" runat="server"></asp:Literal>
            </td>
            <td class="t_r t_bg" width="150">
                主要职责：
            </td>
            <td>
                <asp:Literal ID="li_FFunction" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr style="display: none">
            <td class="t_r t_bg">
                代填人：
            </td>
            <td colspan="3">
                <asp:TextBox ID="li_FTxt1" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                见证结论：
            </td>
            <td>
                <asp:DropDownList ID="t_FResultInt" runat="server">
                    <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                    <asp:ListItem Value="1" Text="合格"></asp:ListItem>
                    <asp:ListItem Value="2" Text="不合格"></asp:ListItem>
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                见证时间：
            </td>
            <td>
                <asp:TextBox ID="t_FAppTime" runat="server" CssClass="m_txt" onfocus="WdatePicker();"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                见证意见：
            </td>
            <td class="m_txt_M" colspan="3">
                <asp:TextBox ID="t_FContent" runat="server" CssClass="m_txt" Width="320px" MaxLength="50"
                    TextMode="MultiLine" Height="60"></asp:TextBox>
                <tt>* 50字。</tt>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="t_bg" align="center">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                    OnClientClick="return checkInfo();" Visible="false" />
                <input type="button" value="返回" class="m_btn_w2" onclick="window.close();" />
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
        <asp:Button ID="btnQuery" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2"
            Style="display: none" />
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
