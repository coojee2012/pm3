<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RYQKList.aspx.cs" Inherits="WYDW_ApplyRYQK_RYQKList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>人员情况</title>
    <base target="_self" />
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script>
        function ShowAppPage(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret == "ok") {
                location.reload();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table height="95%" width="98%" align="center">
            <tr>
                <td colspan="3" height="10px;"></td>
            </tr>
            <tr>
                <td class="wxts_top_l"></td>
                <td class="wxts_top"></td>
                <td class="wxts_top_r"></td>
            </tr>
            <tr>
                <td class="wxts_l"></td>
                <td class="wxts_m" valign="top">
                    <div class="wxts_title">
                        人员情况
                    </div>
                    <div style="width: 98%; margin: 0 auto;">
                        <table width="100%" id="appTab" runat="server">
                            <tr>
                                <td class="t_r"></td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px">
                                    <table width="100%" id="Table1" runat="server">
                                        <tr>
                                            <td class="t_c"></td>
                                        </tr>
                                        <tr>
                                            <td height="27" class="txt23" style="padding-left: 1px; margin-top: 6px;">人员姓名：<asp:TextBox ID="t_fPersonName" runat="server" CssClass="m_txt" Width="150px"></asp:TextBox>
                                                &nbsp; 
                                                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;

                                            </td>
                                            <td style="text-align: right">
                                                <input class="m_btn_w6" type="button" id="btnImpRY" onclick="ShowAppPage('../Common/RYListCheck.aspx?T=2', 1000, 450);" value="导入项目人员" />
                                                <input class="m_btn_w2" type="button" id="btnZGRY" onclick="ShowAppPage('RYQKInfo.aspx?T=1',900,640);" value="新增" />
                                                <asp:Button ID="btnSXRY" runat="server" CssClass="m_btn_w2" CommandArgument="1" Text="刷新" OnClick="btnSXRY_Click"></asp:Button>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" colspan="2">
                                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                        HorizontalAlign="Center" OnItemCommand="DG_List_ItemCommand" OnItemDataBound="DG_List_ItemDataBound"
                                        Style="margin-top: 7px" Width="100%">
                                        <HeaderStyle CssClass="m_dg1_h" />
                                        <ItemStyle CssClass="m_dg1_i" />
                                        <Columns>
                                            <asp:BoundColumn HeaderText="序号">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="姓名" DataField="fPersonName">
                                               
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="性别" DataField="fSex">
                                                <HeaderStyle Width="70px"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="fCardType" HeaderText="证件类型">
                                                <HeaderStyle Width="110px"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="证件号码" DataField="fCardID">
                                                <HeaderStyle Width="180px"></HeaderStyle>
                                            </asp:BoundColumn>

                                            <asp:BoundColumn HeaderText="职务" DataField="fPosition">
                                                <HeaderStyle Width="200px"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="联系电话" HeaderStyle-Width="100px" DataField="fgrdh">
                                                <HeaderStyle Width="130px"></HeaderStyle>

                                                <ItemStyle CssClass="lh t_l" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="操作">
                                                <HeaderStyle Width="80" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnOp" runat="server" CommandArgument='<%#Eval("FId")%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="XMBH" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                                    <div style="line-height: 20px; text-align: left;">
                                        <tt>提示：点击“人员姓名”可进行人员信息维护</tt>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td class="wxts_r"></td>
            </tr>
            <tr>
                <td class="wxts_bot_l"></td>
                <td class="wxts_bot"></td>
                <td class="wxts_bot_r"></td>
            </tr>
        </table>
    </form>
</body>
</html>
