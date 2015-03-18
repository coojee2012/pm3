<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lift.aspx.cs" Inherits="JSDW_ApplyAQJDBA_Lift" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>起重设备信息</title>
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
            return AutoCheckInfo();
        }
        function selEmp(obj, tagId) {
            var url = "../project/JqsbxxSel.aspx?1=1";
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }

        }
        function addCZRY() {
            var fid = document.getElementById("txtFId").value;;
            if (fid == null || fid == '') {
                alert('请先保存上方的起重设备信息！');
                return;
            }
            showAddWindow('Lift_CZRY.aspx?fLinkId=' + fid, 800, 550);
            //  alert('dd')
        }
    </script>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click"
                        OnClientClick="return checkInfo();" />
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    设备名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_SBMC" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                    <tt>*</tt>
                    <input type="hidden"  runat="server" ID="t_SBID" value="" />
                    <asp:Button ID="btnAdd" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEmp(this,'t_SBID');"
                    UseSubmitBehavior="false" CommandName="SGT" Style="margin-bottom: 4px;margin-left:5px;" OnClick="btnAdd_Click" />
                </td>
                <td class="t_r t_bg">
                    备案编号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_BABH" runat="server" CssClass="m_txt" Width="195px"  Enabled="false"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    设备型号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_SBXH" runat="server" CssClass="m_txt" Width="195px"  Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    出厂编号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_CCBH" runat="server" CssClass="m_txt" Width="195px"  Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    生产日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_SCRQ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    使用单位：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_SYDW" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    制造单位：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZZDW" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    产权单位：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_CQDW" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    安装单位：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_AZDW" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    检验检测机构：
                </td>
                <td colspan="1">
                   <asp:TextBox ID="t_JYJCJG" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    备案机关：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_BAJG" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                <input id="txtFId" type="hidden" runat="server" />
                </td>
            </tr>  
        </table>
        
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    操作人员列表
                </td>
                <td class="t_r">

                    <input type="button" id="Button1" runat="server" value="新增" class="m_btn_w2" onclick="addCZRY();" />

                    <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                        OnClick="btnDel_Click" />
                    <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px;
            margin-bottom: 1px;" Width="98%">
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
                <asp:BoundColumn HeaderText="姓名" DataField="Name">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="工种" DataField="Trades">
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="操作证号" DataField="CZZH">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
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
