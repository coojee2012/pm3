<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lz_edit.aspx.cs" Inherits="WYDW_ApplyLPB_Lz_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入楼幢信息</title>
    <base target="_self" />
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../script/default.js"> </script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        window.returnValue = "ok";
        function ShowAppPage() {
            var width = 780;
            var height = 700;
            var sUrl = 'LPBshow.aspx';
            var ret = window.showModalDialog(sUrl + '?rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; status:no; help:no;');

            if (ret == "ok") {
                location.reload();
            }
        }
        function ShowLZedit() {
            var width = 1020;
            var height = 700;
            var sUrl = 'Lz_edit.aspx';
            var ret = window.showModalDialog(sUrl + '?rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; status:no; help:no;');

            if (ret == "ok") {
                location.reload();
            }
        }
    </script>
</head>
<body>
    <div id="container">
        <div id="pageBody">
            <div id="mydomains_content" style="display: block">
                <div>
                    <form runat="server" id="form2">
                        <div class="main-wrap">
                            <div style="border: 2px solid #ADCBEA; padding: 10px 15px;">
                                <div>
                                    <table width="100%" align="center" class="m_title">
                                        <tr>
                                            <th colspan="2">导入楼幢信息
                                            </th>
                                        </tr>
                                    </table>
                                    <table width="100%" align="center" class="m_bar">
                                        <tr>
                                            <td class="m_bar_l"></td>
                                            <td><font color="navy">
                                                    <asp:Label ID="txtCount" runat="server" Text="共有  0个房屋，总面积为:"></asp:Label></font></td>
                                            <td class="t_r">
                                                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
                                                <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                                            </td>
                                            <td class="m_bar_r"></td>
                                        </tr>
                                    </table>
                                    <table width="100%" align="center">
                                        <tr>
                                            <td class="zhtd1" colspan="2" style="text-align: left; width: 640px; height: 20px">
                                                <span style="float: left"><strong id="txtDR">导入Excel:</strong>
                                                    <asp:FileUpload ID="fileLoad" runat="server" onchange=" $('#hidOperation').val('excel');document.forms['form2'].submit();;" />
                                                    <asp:Button ID="BtHZ" runat="server" OnClick="BtHZ_Click" Text="" Width="0px" Height="0px"
                                                        BorderWidth="0px" BackColor="#ffffff" BorderColor="#ffffff" />
                                                </span>
                                            </td>
                                            <td></td>
                                            <%--<td align="right">
                                                <a target="_blank" href="Demo_LPB_Insert.xls">楼盘表上报模板点击下载</a>
                                            </td>--%>

                                        </tr>
                                    </table>
                                    <table width="100%" align="center">
                                        <tr>
                                            <td align="center" valign="top" colspan="2">
                                                <div style="width: 100%; height: 600px; overflow-y: auto">
                                                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                                                        HorizontalAlign="Center" OnItemCommand="DG_List_ItemCommand" OnItemDataBound="DG_List_ItemDataBound"
                                                        Style="margin-top: 7px" Width="100%" AllowSorting="True">
                                                        <HeaderStyle CssClass="m_dg1_h" />
                                                        <ItemStyle CssClass="m_dg1_i" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="ZH" HeaderText="幢号">
                                                                <HeaderStyle></HeaderStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="FH" HeaderText="房号" HeaderStyle-Width="120px">
                                                                <HeaderStyle Width="120px"></HeaderStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DY" HeaderText="单元" HeaderStyle-Width="120px">
                                                                <HeaderStyle Width="120px"></HeaderStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="RHC" HeaderText="入户层" HeaderStyle-Width="120px">
                                                                <HeaderStyle Width="120px"></HeaderStyle>

                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SH" HeaderText="室号" HeaderStyle-Width="120px">
                                                                <HeaderStyle Width="120px"></HeaderStyle>

                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="JZMJ" HeaderText="建筑面积" HeaderStyle-Width="120px">
                                                                <HeaderStyle Width="120px"></HeaderStyle>

                                                            </asp:BoundColumn>

                                                        </Columns>

                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <input id="hidHouseAN" runat="server" type="hidden" />
                        <input id="hidBuildAN" runat="server" type="hidden" name="hidBuildAN" value="" />
                        <input id="hidBuildName" runat="server" type="hidden" value="" />
                        <input id="hidZTS" runat="server" type="hidden" value="0" />
                        <input id="hidZJZMJ" runat="server" type="hidden" value="0" />
                        <input id="hidOperation" runat="server" name="hidOperation" type="hidden" value="" />
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</body>
</html>
