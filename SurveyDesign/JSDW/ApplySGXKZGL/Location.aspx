<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Location.aspx.cs" Inherits="JSDW_ApplySGXKZGL_Location" %>

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
            return AutoCheckInfo();
        }
        function addPrjItem() {
            var fid = document.getElementById("txtFId").value;
            var fAppId = '<%=ViewState["FAppId"] %>';
            var fPrjItemId = '<%=ViewState["FPrjItemId"] %>';
            if (fid == null || fid == '') {
                alert('请先保存上方信息！');
                return;
            }
            showAddWindow('File.aspx?fLinkId=' + fid + "&&fAppId=" + fAppId + "&&fPrjItemId=" + fPrjItemId, 800, 550);
            //  alert('dd')
        }
        function showTr1() {
            $("tr[name=tr1]").show();
            //$(".cc1").removeAttr("disabled");
            $(".cc1").attr("disabled", true);
            //$("input").removeAttr("disabled");
            $("#btnAdd").attr("disabled", true);
            $("#btnDel").attr("disabled", true);
            $("#btnReload").attr("disabled", true);
            //$("#t_Scale").removeAttr("disabled");
            $("#t_Scale").attr("disabled", true);
            //$("#t_ProjectBasis").removeAttr("disabled");
            $("#t_ProjectBasis").attr("disabled", true);
            //$("#btnSave").removeAttr("disabled");
        }
        function hideTr1() {
            $("tr[name=tr1]").hide();
            $("tt[name=tt_t1]").empty();
            $("tt[name=tt_t1]").each(function () {
                var t = $(this).html();
                $(this).replaceWith(t);
            });
            $(".cc1").removeAttr("disabled");
            $("#btnAdd").removeAttr("disabled");
            $("#btnDel").removeAttr("disabled");
            $("#btnReload").removeAttr("disabled");
            $("#t_Scale").removeAttr("disabled");
            $("#t_ProjectBasis").removeAttr("disabled");
            //$("#btnSave").removeAttr("disabled");
        }
        function change(value) {
            if (value == "1") {
                $("tr[name=tr1]").hide();
                $("tt[name=tt_t1]").empty();
                $("tt[name=tt_t1]").each(function () {
                    var t = $(this).html();
                    $(this).replaceWith(t);
                });
                $(".cc1").removeAttr("disabled");
                $("#btnAdd").removeAttr("disabled");
                $("#btnDel").removeAttr("disabled");
                $("#btnReload").removeAttr("disabled");
                $("#t_Scale").removeAttr("disabled");
                $("#t_ProjectBasis").removeAttr("disabled");
                //$("#btnSave").removeAttr("disabled");
            }
            else {
                $("tr[name=tr1]").show();
                $(".cc1").attr("disabled", true);
                //$("input").removeAttr("disabled");
                $("#btnAdd").attr("disabled", true);
                $("#btnDel").attr("disabled", true);
                $("#btnReload").attr("disabled", true);
                //$("#t_Scale").removeAttr("disabled");
                $("#t_Scale").attr("disabled", true);
                //$("#t_ProjectBasis").removeAttr("disabled");
                $("#t_ProjectBasis").attr("disabled", true);
                //$("#btnSave").removeAttr("disabled");
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
                <h3>选址意见书信息</h3> 
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
        <tr name=tr1 >
            <td class="t_r t_bg">
                理由： </td>
            <td colspan="3">
                <asp:TextBox ID="t_YL" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox><tt name="tt_t1">*</tt>
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
                项目拟选位置： </td>
            <td colspan="3">
                <asp:TextBox ID="t_LocationAddress" class="cc1" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox>
            </td>          
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                拟用地面积：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_Area" class="cc1" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="60%"></asp:TextBox>（m2）
            </td>
            <td class="t_r t_bg">
                选址意见书证书编号： </td>
            <td colspan="1">
                <asp:TextBox ID="t_XZYJSZSBH" class="cc1" runat="server" CssClass="m_txt" Width="44%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                拟建设规模： </td>
            <td colspan="3">
                <asp:TextBox ID="t_Scale" class="cc1" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设项目依据：</td>
            <td colspan="3">
               <asp:TextBox ID="t_ProjectBasis" class="cc1" Height="35px" TextMode="MultiLine" onblur="checkLength(this,1000,'建设项目依据');" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                用地批准手续：</td>
            <td colspan="3">
               <asp:TextBox ID="t_YDPZSX" class="cc1" Height="35px" TextMode="MultiLine" onblur="checkLength(this,1000,'用地批准手续');" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox><tt>*</tt>&nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                发证日期：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_CreateTime" class="cc1" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="60%"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                核发机关： </td>
            <td>
                <asp:TextBox ID="t_HFJG" class="cc1" runat="server" CssClass="m_txt" Width="44%"></asp:TextBox>
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
                    <asp:Button ID="btnDel" class="cc1" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                        OnClick="btnDel_Click" />
                    <asp:Button ID="btnReload" class="cc1" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
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

