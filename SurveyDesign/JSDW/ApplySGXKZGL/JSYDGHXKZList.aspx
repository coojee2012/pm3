<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JSYDGHXKZList.aspx.cs" Inherits="JSDW_ApplySGXKZGL_JSYDGHXKZList" %>

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

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();

        });
        function checkInfo() {
            var value = document.getElementById("t_BL").value;
            if (value == "1") {
                return AutoCheckInfo();
            } else {
                var ly = document.getElementById("t_YL").value;
                if (ly == null || ly == '') {
                    alert('必须填写理由！');
                    return false;
                }
            }
        }
        function addPrjItem() {
            var fid = document.getElementById("txtFId").value;
            var fAppId = '<%=ViewState["FAppId"] %>';
            var fPrjItemId = '<%=ViewState["FPrjItemId"] %>';
            if (fid == null || fid == '') {
                alert('请先保存上方信息！');
                return;
            }
            showAddWindow('File.aspx?fLinkId=' + fid + "&&fAppId=" +fAppId+ "&&fPrjItemId=" +fPrjItemId, 800, 550);
            //  alert('dd')
        }
        function showTr1() {
            $("tr[name=tr1]").show();
            $("#btnAdd").attr("disabled", true);
            $("#btnDel").attr("disabled", true);
            $("#btnReload").attr("disabled", true);
            $("#t_Area").attr("disabled", true);
            $("#t_ConstrScale").attr("disabled", true);
            $("#t_YDXZ").attr("disabled", true);
            $("#t_Others").attr("disabled", true);
            $("#t_YDGHXKZBH").attr("disabled", true);
            $("#t_CreateTime").attr("disabled", true);
            $("#t_HFJG").attr("disabled", true);
        }
        function hideTr1() {
            $("tr[name=tr1]").hide();
            $("#btnAdd").removeAttr("disabled");
            $("#btnDel").removeAttr("disabled");
            $("#btnReload").removeAttr("disabled");
            $("#t_Area").removeAttr("disabled");
            $("#t_ConstrScale").removeAttr("disabled");
            $("#t_YDXZ").removeAttr("disabled");
            $("#t_Others").removeAttr("disabled");
            $("#t_YDGHXKZBH").removeAttr("disabled");
            $("#t_CreateTime").removeAttr("disabled");
            $("#t_HFJG").removeAttr("disabled");
        }
        function change(value) {
            if (value == "1") {
                $("tr[name=tr1]").hide();
                $("#btnAdd").removeAttr("disabled");
                $("#btnDel").removeAttr("disabled");
                $("#btnReload").removeAttr("disabled");
                $("#t_Area").removeAttr("disabled");
                $("#t_ConstrScale").removeAttr("disabled");
                $("#t_YDXZ").removeAttr("disabled");
                $("#t_Others").removeAttr("disabled");
                $("#t_YDGHXKZBH").removeAttr("disabled");
                $("#t_CreateTime").removeAttr("disabled");
                $("#t_HFJG").removeAttr("disabled");
            }
            else {
                $("tr[name=tr1]").show();
                //$("input").removeAttr("disabled");
                $("#btnAdd").attr("disabled", true);
                $("#btnDel").attr("disabled", true);
                $("#btnReload").attr("disabled", true);
                $("#t_Area").attr("disabled", true);
                $("#t_ConstrScale").attr("disabled", true);
                $("#t_YDXZ").attr("disabled", true);
                $("#t_Others").attr("disabled", true);
                $("#t_YDGHXKZBH").attr("disabled", true);
                $("#t_CreateTime").attr("disabled", true);
                $("#t_HFJG").attr("disabled", true);

            }
        }
    </script>

    <style type="text/css">
        .style1 { text-align: left; height: 31px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                项目环节材料
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                    OnClientClick="return checkInfo();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table id="table1" class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>建设用地规划许可证</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                办理选项：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:DropDownList ID="t_BL" class="cc2" runat="server" CssClass="m_txt" onchange="change(this.value)" Width="60%">
                    <asp:ListItem Value="1">补填</asp:ListItem>
                    <asp:ListItem Value="0">不需要办理</asp:ListItem>
                    <asp:ListItem Value="2">以后补办</asp:ListItem>
                    
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr name="tr1">
            <td class="t_r t_bg">
                理由： </td>
            <td colspan="3">
                <asp:TextBox ID="t_YL" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                项目名称：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="60%" Enabled="false"></asp:TextBox>
                <input id="txtFId" type="hidden" runat="server" />
            </td>
            <td class="t_r t_bg" style="width:12.8%;">
                建设单位： </td>
            <td>
                <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="44%" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                建设地址： </td>
            <td colspan="3">
                <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="72.2%" Enabled="false"></asp:TextBox>
            </td>          
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                用地面积：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_Area" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="60%"></asp:TextBox>（m2）
            </td>
            <td class="t_r t_bg">
                其他：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_Others" runat="server" CssClass="m_txt" Width="44%"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设规模： </td>
            <td colspan="3">
                <asp:TextBox ID="t_ConstrScale" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="72.2%" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                用地性质：</td>
            <td colspan="3">
               <asp:TextBox ID="t_YDXZ" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                用地规划许可证编号： </td>
            <td colspan="3">
                <asp:TextBox ID="t_YDGHXKZBH" runat="server" CssClass="m_txt" Width="21.1%"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                发证日期：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_CreateTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="60%"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                核发机关： </td>
            <td>
                <asp:TextBox ID="t_HFJG" runat="server" CssClass="m_txt" Width="44%"></asp:TextBox>
            </td>
        </tr>
        
    </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    材料信息
                </td>
                <td class="t_r">
                    <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="addPrjItem();" />
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
                <asp:BoundColumn HeaderText="材料名称" DataField="FileName">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="上传时间" DataField="ReportTime" DataFormatString="{0:d}">
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
