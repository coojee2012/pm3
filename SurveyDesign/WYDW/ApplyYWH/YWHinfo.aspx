<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YWHinfo.aspx.cs" Inherits="WYDW_ApplyYWH_YWHinfo" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>业主委员会</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        function checkInfo() {
            return AutoCheckInfo();
        }
        function mytest() {
            $("#t_BZ").val("返回");

        }
        function showYWHCYWindow(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')
            //解决谷歌浏览器或360极速模式情况下无返回值
            if (ret == null || ret == undefined) {
                ret = window.returnValue;
            }
            if (ret == "1") {
                location.reload();
            }
        }
    </script>

    <style type="text/css">
        .m_btn_w2 {
            height: 21px;
        }
    </style>

</head>
<body>
    <div style="height: 100%; width: 100%;">
        <form id="form1" runat="server">
            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">项目业主委员会备案
                    </th>
                </tr>
            </table>
            <table width="98%" align="center" class="m_bar">
                <tr>
                    <td class="m_bar_l"></td>
                    <td></td>
                    <td class="t_r">
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClientClick="return checkInfo();" OnClick="btnSave_Click" />
                        <%--<input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />--%>
                        <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
                    </td>
                    <td class="m_bar_r"></td>
                </tr>
            </table>
            <table class="m_table" id="Table3" align="center" width="98%">
               <%-- <tr>
                    <td class="t_r t_bg" align="center" width="14%">业主大会名称：
                    </td>
                    <td width="45%" colspan="1">
                        <asp:TextBox ID="t_YZDHMC" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
                    </td>
                    <td class="t_r t_bg" align="center">成立时间：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_YZDHCLSJ" runat="server" Width="195px" CssClass="m_txt" onfocus="WdatePicker()"
                            MaxLength="20"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td class="t_r t_bg" align="center" width="15%">业主委员会名称：
                    </td>
                    <td colspan="1" width="25%">
                        <asp:TextBox ID="t_YZWYHMC" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
                    </td>
                    <td width="10%"></td>
                    <td class="t_r t_bg" align="center" colspan="1">成立时间：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_YZWYHCLSJ" runat="server" Width="195px" CssClass="m_txt" onfocus="WdatePicker()"
                            MaxLength="20"></asp:TextBox>
                    </td>
                    <td width="10%"></td>
                </tr>
                <tr>
                    <td class="t_r t_bg" align="center">业主委员会办公地址：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_YZWYHBGDZ" runat="server" Width="195px" CssClass="m_txt"
                            MaxLength="40"></asp:TextBox>
                    </td>
                    <td width="10%"></td>
                </tr>
              <%--  <tr>
                    <td class="t_r t_bg" align="center" colspan="1">应出席业主大会业主数：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_YCXYZDHYZS" runat="server" CssClass="m_txt" Width="195px" Text="0"
                            MaxLength="20"></asp:TextBox>
                    </td>
                    <td width="10%">人</td>
                    <td class="t_r t_bg" align="center" colspan="1">业主代表大会人数：
                    </td>
                    <td>
                        <asp:TextBox ID="t_YZDBDHRS" runat="server" CssClass="m_txt" Width="195px" Text="0"
                            MaxLength="20"></asp:TextBox>
                    </td>
                    <td width="10%">人</td>
                </tr>
                <tr>
                    <td class="t_r t_bg" align="center">首次业主代表大会召开时间：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_SCYZDBDHZKSJ" runat="server" Width="195px" CssClass="m_txt" onfocus="WdatePicker()"
                            MaxLength="20"></asp:TextBox>
                    </td>
                    <td width="10%"></td>
                    <td class="t_r t_bg" align="center" colspan="1">首次业主大会出席人数：
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="t_SCYZDBDHCXRS" runat="server" CssClass="m_txt" Width="195px" Text="0"
                            MaxLength="20"></asp:TextBox>
                    </td>
                    <td width="10%">人</td>
                </tr>--%>
                <tr>
                    <td class="t_r t_bg" align="center" colspan="1">备注：
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="t_BZ" runat="server" CssClass="m_txt" Width="99.5%"
                            MaxLength="40" Height="100px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div style="width: 100%;">
                <table width="98%" align="center" class="m_title">
                    <tr>
                        <th colspan="2">项目业主委员会成员
                        </th>
                    </tr>
                </table>
                <table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td></td>
                        <td class="t_r">
                            <input type="button" id="btn_addMB" runat="server" value="添加" class="m_btn_w2" onclick="showYWHCYWindow('YWHCYInfo.aspx?e=1', '720', '350');" />
                            <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" />
                            <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                        </td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>
                <asp:DataGrid ID="ywhcy_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                    HorizontalAlign="Center" OnItemDataBound="YWHCY_List_ItemDataBound" Style="margin-top: 6px; margin-bottom: 1px;"
                    Width="98%">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <ItemStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemStyle Width="30px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn HeaderText="序号">
                            <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="XM" HeaderText="姓名">
                            <ItemStyle Wrap="false" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="性别">
                            <ItemStyle Wrap="false" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NL" HeaderText="年龄">
                            <ItemStyle Wrap="false" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SFZH" HeaderText="身份证号">
                            <ItemStyle Wrap="false" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ZZMM" HeaderText="政治面貌">
                            <ItemStyle Wrap="false" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="YZWYHZW" HeaderText="业主委员会职务">
                            <ItemStyle Wrap="false" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="LXDH" HeaderText="联系电话">
                            <ItemStyle Wrap="false" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="GZDW" HeaderText="工作单位">
                            <ItemStyle Wrap="false" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="JTDZ" HeaderText="家庭地址">
                            <ItemStyle Wrap="false" />
                        </asp:BoundColumn>
                        
                        <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="XB" Visible="False"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
        <br />
        <tt>注：</tt>
    </div>
            </div>
            <input id="hidTotal" runat="server" name="hidTotal" type="hidden" value="0" />
            <input id="hidXMID" runat="server" name="hidXMID" type="hidden" value="1" />
            <input id="hidYZWYHDAH" runat="server" name="hidYZWYHDAH" type="hidden" value="" />
            <input id="hidSelectPerson" runat="server" name="text" type="hidden" value="-1" />
        </form>
    </div>
</body>
</html>
