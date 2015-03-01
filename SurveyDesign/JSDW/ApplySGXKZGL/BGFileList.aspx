<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BGFileList.aspx.cs" Inherits="JSDW_ApplySGXKZGL_BGFileList" %>

<%@ Register TagPrefix="uc1" TagName="pager" Src="~/Common/pager.ascx" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <style type="text/css">
        .style1 { text-align: left; height: 31px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                附件信息
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="t_r" style="padding-right: 10px;">
                <asp:Button ID="btnQuery" runat="server" Text="刷新" OnClick="btnQuery_Click" class="m_btn_w2" />
            </td>
            <td class="m_bar_r">
            </td>
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
                    需要文件数量
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
                            <%#Eval("FFileCount")%>
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
    </form>
</body>
</html>