﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBTJList.aspx.cs" Inherits="JSDW_ApplySGXKZGL_JBTJList" %>

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
                文件或证明材料
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
                <h3>拆迁许可证或施工现场是否具备条件</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                项目名称：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="195px" ReadOnly="true"></asp:TextBox>
                <input id="txtFId" type="hidden" runat="server" />
            </td>
            <td class="t_r t_bg">
                工程名称： </td>
            <td>
                <asp:TextBox ID="t_PrjItemName" runat="server" CssClass="m_txt" Width="195px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr > 
            <td class="t_r t_bg">
                建设单位： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="195px" ReadOnly="true"></asp:TextBox>
            </td>  
            <td class="t_r t_bg">
                拆迁许可证编号： </td>
            <td colspan="1">
                <asp:TextBox ID="t_CQXKZBH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>     
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                发证日期：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_CreateTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                核发机关： </td>
            <td>
                <asp:TextBox ID="t_HFJG" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                拆迁事由：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:TextBox ID="t_CQSY" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                拆迁依据： </td>
            <td colspan="1">
                <asp:TextBox ID="t_CQYJ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                批准部门： </td>
            <td colspan="1">
                <asp:TextBox ID="t_PZBM" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                公告部门： </td>
            <td colspan="1">
                <asp:TextBox ID="t_GGBM" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                拆迁公告发布媒体： </td>
            <td colspan="1">
                <asp:TextBox ID="t_CQGGFBMT" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                公告日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_GGTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                拆迁负责人： </td>
            <td colspan="1">
                <asp:TextBox ID="t_CQFZR" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                联系方式： </td>
            <td colspan="1">
                <asp:TextBox ID="t_Mobile" onblur="isTel(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                涉及征地：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_SJZD" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                    Width="726px" onblur="checkLength(this,1000,'涉及征地');"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工作方法和原则：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_GZFFHYZ" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                    Width="726px" onblur="checkLength(this,1000,'工作方法和原则');"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                拆迁范围：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_CQFW" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                    Width="726px" onblur="checkLength(this,1000,'拆迁范围');"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                补偿安置方式：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_BCAZFS" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                    Width="726px" onblur="checkLength(this,1000,'补偿安置方式');"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                补偿标准：
            </td>
            <td colspan="3" class="m_txt_M">
                <asp:TextBox ID="t_BCBZ" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                    Width="726px" onblur="checkLength(this,1000,'补偿标准');"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                安置补助费：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_AZBZF" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（万元）
            </td>
            <td class="t_r t_bg">
                地面附着物补偿费：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_DMFZWBCF" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（万元）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                支付方式：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_ZFFS" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                已支付的拆迁补偿费用总额：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_YZFBCF" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（万元）
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
