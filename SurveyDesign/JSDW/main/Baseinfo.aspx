﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Baseinfo.aspx.cs" Inherits="JSDW_QMain_Baseinfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>建设单位基本信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
        });
        function CheckInfo() {
            var ok = true;
            var v = $('#t_FEntTypeId').val();
            // var t_FJuridcialCode = document.getElementById("t_FJuridcialCode");
            // if (t_FJuridcialCode) {
            //     var patrn = /^[A-Za-z0-9]{8}-[A-Za-z0-9]{1}$/;
            //     if (!patrn.exec(t_FJuridcialCode.value)) {
            //         alert("组织结构代码格式不正确");
            //         t_FJuridcialCode.focus();
            //         return false
            //     }
            // } 
            $.each($(".c" + v).find(":text"), function() {
                if (this.value == '') {
                    ok = false;
                    return false;
                }
            });
            $.each($(".b").not($(".b" + v)).find(":text"), function() {
                if (this.value == '') {
                    ok = false;
                    return false;
                }
            });
            if (!ok) {
                alert('请将信息填写完整！');
            }
            return ok;
        }
        var postBackElement, prm;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);
            postBackElement = args.get_postBackElement();
            initUpdateProgress();
            $get('UpdateProgress1').style.display = 'block';
        }
        function EndRequest(sender, args) {
            $get('UpdateProgress1').style.display = 'none';
        }
        function showDiv() {
            var v = $('#t_FEntTypeId').val();
            $(".c").hide();
            $(".c" + v).show();

            $(".b").show();
            $(".b" + v).hide();

            $(".c").not($(".c" + v)).find(":text").val('');
            $(".b" + v).find(":text").val('');
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业基本信息管理
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
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
              <asp:Button ID="btnDownload" runat="server" Text="同步" CssClass="m_btn_w2" onclick="btnDownload_Click" Visible="false"  />
                <asp:Button ID="btnUpdate" runat="server" Text="保存" CssClass ="m_btn_w2" 
                    onclick="btnSave_Click"  Visible="false"  />
            
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                单位名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="303px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                单位性质：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_FEntTypeId" runat="server" CssClass="m_txt" Enabled="false"
                    onchange="showDiv();">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                单位地址：
            </td>
            <td colspan="3">
                <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                <asp:TextBox ID="t_FRegistAddress" runat="server" CssClass="m_txt" Width="224px"
                    MaxLength="75"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr class="c c18102 c18103">
            <td align="center" class="t_r t_bg" height="21">
                法人代表：
            </td>
            <td class="txt31">
                <asp:TextBox ID="t_FOTxt5" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td align="center" class="t_r t_bg">
                法人手机号：
            </td>
            <td class="txt31">
                <asp:TextBox ID="t_FMobile" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr class="b b18106">
            <td class="t_r t_bg">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                联系人电话：
            </td>
            <td>
                <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr class="c c18102 c18103">
            <td class="t_r t_bg">
                组织机构代码：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FJuridcialCode" runat="server" CssClass="m_txt" MaxLength="10"
                    Width="210px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr class="c c18103">
            <td class="t_r t_bg ">
                营业执照号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FLicence" runat="server" CssClass="m_txt" Width="210px" MaxLength="15"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr class=" c c18106">
            <td class="t_r t_bg ">
                身份证：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FIdCard" runat="server" CssClass="m_txt" Width="210px" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg ">
                联系电话：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_FCall" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                电子邮箱：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FEMail" runat="server" CssClass="m_txt" MaxLength="30" Width="302px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FRemark" runat="server" CssClass="m_txt" MaxLength="30" Width="302px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar" style="display: none">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                注册人员信息
            </td>
            <td class="t_r">
                <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="showAddWindow('AddEmpInfo.aspx?e=1',600,400);" />
                <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                    OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        Visible="false" HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound"
        Style="margin-top: 6px; margin-bottom: 1px;" Width="98%">
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
            <asp:BoundColumn DataField="FName" HeaderText="姓名"></asp:BoundColumn>
            <asp:BoundColumn DataField="FIdCard" HeaderText="身份证号码"></asp:BoundColumn>
            <asp:BoundColumn DataField="FCertiNo" HeaderText="证书编号"></asp:BoundColumn>
            <asp:BoundColumn DataField="FEndTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="有效期">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FUserName" HeaderText="用户名"></asp:BoundColumn>
            <asp:BoundColumn DataField="FPassWord" HeaderText="密码"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <div style="padding-left: 1%; display: none">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnDownload" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    </asp:UpdateProgress>
    <asp:Button ID="btnReload3" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload3_Click"
        Style="display: none" />
    </form>
</body>
</html>
