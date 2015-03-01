<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LPBList.aspx.cs" Inherits="WYDW_ApplyLPB_LPBList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>楼盘表</title>
    <base target="_self" />
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        function ShowLPBInfo(vBuildID, type) {
            var width = 780;
            var height = 545;
            var sUrl = 'LPBInfo.aspx';
            window.showModalDialog(sUrl + '?Type=' + type + '&BuildID=' + vBuildID + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; status:no; help:no;');
            return false;
        }
        function ShowLZedit() {
            var width = 1020;
            var height = 700;
            var sUrl = 'Lz_edit.aspx';
            var ret = window.showModalDialog(sUrl + '?rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; status:no; help:no;');

            if (ret == "ok") {
                location = location;
            }
        }
    </script>
</head>
<body>
    <div id="container">
        <div id="pageBody">
            <div id="mydomains_content" style="display: block">
                <div>
                    <form runat="server" id="form1">
                        <div class="main-wrap">
                            <div style="border: 2px; padding: 10px 15px;">
                                <div>
                                    <table width="100%" align="center" class="m_title">
                                        <tr>
                                            <th colspan="2">项目楼盘表
                                            </th>
                                        </tr>
                                    </table>
                                    <table width="100%" align="center" class="m_bar">
                                        <tr>
                                            <td class="m_bar_l"></td>
                                            <td class="t_l" style="padding-left:20px"><a target="_blank" href="Demo_LPB_Insert.xls">楼盘表上报模板点击下载</a></td>
                                            <td class="t_r">
                                                <input type="button" id="btnReturn" runat="server" value="导入楼幢Excel" class="m_btn_w6" onclick="ShowLZedit()" />
                                            </td>
                                            <td class="m_bar_r"></td>
                                        </tr>
                                    </table>
                                    <div style="overflow: auto; height: 45%">
                                        <table width="100%" align="center">
                                            <tr>
                                                <td align="center" valign="top" colspan="2">
                                                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                                        HorizontalAlign="Center" OnItemCommand="DG_List_ItemCommand" OnItemDataBound="DG_List_ItemDataBound"
                                                        Style="margin-top: 7px" Width="100%" AllowSorting="True">
                                                        <HeaderStyle CssClass="m_dg1_h" />
                                                        <ItemStyle CssClass="m_dg1_i" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="BuildName" HeaderText="幢号">
                                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="ZTS" HeaderText="房屋总套数" HeaderStyle-Width="120px">
                                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="ZJZMJ" HeaderText="房屋总面积" HeaderStyle-Width="120px">
                                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                            </asp:BoundColumn>
                                                            <asp:ButtonColumn HeaderText="查看楼盘表" Text="查看楼盘表">
                                                                <HeaderStyle Width="20%" />
                                                            </asp:ButtonColumn>
                                                            <asp:TemplateColumn HeaderText="操作">
                                                                <HeaderStyle Width="20%" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnItemdel" runat="server" Text='删除'>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="BuildId" Visible="False"></asp:BoundColumn>
                                                        </Columns>

                                                    </asp:DataGrid>
                                                    <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div width="100%" height="100px">
                                <table style="width: 98%;">
                                    <tr>
                                        <td style="color: red; height: 25px">注：已归档的楼盘表不需要再重新添加</td>
                                    </tr>
                                    <tr>
                                        <td style="color: red; height: 25px"></td>
                                    </tr>
                                    <tr>
                                        <td style="color: red; height: 25px"></td>
                                    </tr>
                                    <tr>
                                        <td style="color: red; height: 25px"></td>
                                    </tr>
                                </table>
                            </div>
                            <div style="border: 2px; padding: 10px 15px;">
                                <div>
                                    <table width="100%" align="center" class="m_title">
                                        <tr>
                                            <th colspan="2">已经归档的楼盘表
                                            </th>
                                        </tr>
                                    </table>
                                    <div style="overflow: auto; height: 45%"></div>
                                    <table width="100%" align="center">
                                        <tr>
                                            <td align="center" valign="top" colspan="2">
                                                <asp:DataGrid ID="DG_GDList" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                                    HorizontalAlign="Center" Style="margin-top: 7px" Width="100%" AllowSorting="True" OnItemDataBound="DG_GDList_ItemDataBound">
                                                    <HeaderStyle CssClass="m_dg1_h" />
                                                    <ItemStyle CssClass="m_dg1_i" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="BuildName" HeaderText="幢号">
                                                            <HeaderStyle Width="25%"></HeaderStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ZTS" HeaderText="房屋总套数" HeaderStyle-Width="120px">
                                                            <HeaderStyle Width="25%"></HeaderStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ZJZMJ" HeaderText="房屋总面积" HeaderStyle-Width="120px">
                                                            <HeaderStyle Width="25%"></HeaderStyle>
                                                        </asp:BoundColumn>
                                                        <asp:ButtonColumn HeaderText="查看楼盘表" Text="查看楼盘表">
                                                            <HeaderStyle Width="25%" />
                                                        </asp:ButtonColumn>
                                                        <asp:BoundColumn DataField="BuildId" Visible="False"></asp:BoundColumn>
                                                    </Columns>

                                                </asp:DataGrid>
                                                <uc1:pager ID="Pager2" runat="server"></uc1:pager>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <input id="hidHouseAN" runat="server" type="hidden" />
                        <input id="hidBuildAN" runat="server" type="hidden" name="hidBuildAN" value="" />
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</body>
</html>
