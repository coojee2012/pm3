<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyBaseInfo.aspx.cs" Inherits="SJ_applysjwjbzwt_ApplyBaseInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合同备案基本信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function checkInfo() {
            return AutoCheckInfo();
        } 
    </script>

    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                合同备案基本信息
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_FPrjName" runat="server" CssClass="m_txt" Width="250px" Enabled="false"></asp:TextBox>
                <asp:Button ID="btnSee" runat="server" Text="查看工程详细信息" CssClass="m_btn_w8" />
            </td>
            <td class="t_r t_bg">
                工程编号：
            </td>
            <td colspan="1">
                <asp:TextBox ID="p_FPrjNo" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地点：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="p_FAllAddress" runat="server" CssClass="m_txt" Width="224px" MaxLength="30"
                    Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同备案单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FName" runat="server" CssClass="m_txt" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="e_FLinkMan" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="e_FTel" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                Email：
            </td>
            <td colspan="3">
                <asp:TextBox ID="e_FAddress" runat="server" CssClass="m_txt" Enabled="false" Width="250px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                合同备案内容及范围（100字内）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt10" runat="server" CssClass="m_txt" Width="600px" Height="30px"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同备案承诺和要求（500字内）：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FTxt11" runat="server" CssClass="m_txt" Width="600px" Height="80px"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                设计单位信息
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" align="right">
                是否联合体
            </td>
            <td colspan="3">
                <asp:CheckBox runat="server" ID="t_FFloat10" />
            </td>
        </tr>
        <tr class="t_l t_bg" id="mainSJ" style="display: none;" runat="server">
            <td colspan="4">
                主要设计单位
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                单位名称：
            </td>
            <td class="t_l t_bg" colspan="3">
                <asp:TextBox ID="k_FName" runat="server" CssClass="m_txt" Width="300px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                资质等级：
            </td>
            <td>
                <asp:TextBox ID="k_FLevelName" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                证书编号：
            </td>
            <td>
                <asp:TextBox ID="k_FCertiNo" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同金额：
            </td>
            <td>
                <asp:TextBox ID="k_FMoney" runat="server" CssClass="m_txt"></asp:TextBox>(万元)
            </td>
            <td class="t_r t_bg">
                要求完成日期：
            </td>
            <td>
                <asp:TextBox ID="k_FPlanDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table runat="server" class="m_table" width="98%" align="center" id="otherSJ" style="display: none;">
        <tr>
            <td class="t_l t_bg" colspan="2">
                其他设计单位列表
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <table style="width: 100%">
                    <tr class="t_c t_bg">
                        <td>
                            序号
                        </td>
                        <td>
                            企业名称
                        </td>
                        <td>
                            操作
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="repeaterDisplay" OnItemDataBound="repeaterDisplay_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="lit_NO"></asp:Literal>
                                </td>
                                <td align="left">
                                    <%#Eval("FName") %>
                                </td>
                                <td>
                                    --
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
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
                    <asp:Repeater ID="rep_File" runat="server">
                        <ItemTemplate>
                            <tr class="m_dg1_i">
                                <td colspan="6" class="t_l" style="padding-left: 50px;">
                                    (<%# Container.ItemIndex+1 %>)、 <a href='<%#Eval("FFilePath") %>' target="_blank"
                                        title="点击查看该文件">
                                        <%#Eval("FFileName")%>
                                    </a>
                                    <asp:LinkButton ID="btnDel" runat="server" Text="[删除]" CommandName="cnDel" CommandArgument='<%#Eval("FID") %>'
                                        OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
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
    <input id="p_FAddressDept" type="hidden" runat="server" />
    <input id="k_FBaseInfoId" type="hidden" runat="server" />
    <input id="k_FId" type="hidden" runat="server" />
    <input id="e_FBaseInfoId" type="hidden" runat="server" />
    <input id="e_FId" type="hidden" runat="server" />
    </form>
</body>
</html>
