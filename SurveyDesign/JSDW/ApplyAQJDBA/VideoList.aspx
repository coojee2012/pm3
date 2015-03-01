<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoList.aspx.cs" Inherits="JSDW_ApplyAQJDBA_VideoList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>视频登记情况</title>
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
        function addVideo() {
            var fid = document.getElementById("txtFId").value;;
            if (fid == null || fid == '') {
                alert('请先保存上方的供应商信息！');
                return;
            }
            showAddWindow('Video.aspx?fLinkId=' + fid, 800, 550);
            //  alert('dd')
        }
    </script>

    <base target="_self">
    </base>
    <style type="text/css">
        .modalDiv { position: absolute; top: 1px; left: 1px; width: 100%; height: 100%; z-index:9999; background-color:gray; opacity:.50; filter: alpha(opacity=50); }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
      
        <div style="height:100%;width:100%;">
            
        <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                视频登记情况
            </th>
        </tr>
        <tr runat="server" id="tr_his" visible="false">
            <td class="t_r t_bg" width="15%">
                <tt>历次变更记录：</tt>
            </td>
            <td class="t_l">
                <asp:DropDownList ID="ddlHis" runat="server" AutoPostBack="true"
                    TabIndex="10">
                </asp:DropDownList>
            </td>
        </tr>
    </table> 
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                    OnClientClick="return checkInfo();" />                
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" width="12%">
                    网络运营商：
                </td>
                <td colspan="1" width="45%">
                    <asp:TextBox ID="t_NetworkOperator" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg" width="14%">
                    视频供应商：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_VideoProvider" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    网络带宽：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_NetworkBandWidth" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    安装单位：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_InstallUnit" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    安装日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_InstallDate" runat="server" CssClass="m_txt" Width="195px" onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    视频IP：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_IP" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    视频端口：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Port" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    登录用户名：
                </td>
                <td>
                    <asp:TextBox ID="t_UserName" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    登录密码：
                </td>
                <td>
                    <asp:TextBox ID="t_Password" runat="server" CssClass="m_txt" Width="195px"
                        onblur="isFloat(this);"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    视频信息
                </td>
                <td class="t_r">

                    <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="addVideo();" />

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
                <asp:BoundColumn HeaderText="摄像头ID" DataField="VideoCode">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="安装位置" DataField="Address">
                    <ItemStyle Wrap="False"/>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="设备类型" DataField="EquipTyp">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="是否点名处" DataField="IsCallPlaceStr">
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
    </div>
    <input id="txtFId" type="hidden" runat="server" />
    </form>
</body>

<script type="text/javascript">
    function changeCheck(obj) {
        obj.style.background = obj.checked ? '#1eaffc' : "";
    }
    $.each($(":checkbox[id^=t_F]"), function () {
        $(this).click(function () { changeCheck(this); });
        changeCheck(this);
    });
</script>

</html>
