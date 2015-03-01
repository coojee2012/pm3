<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaseinfoDept.aspx.cs" Inherits="JSDW_QMain_BaseinfoDept" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>图审机构基本信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            //选择卡 指向效果
            $(".tab_btn").hover(function() {
                if ($(this).attr("class") != "a tab_btn1")
                    $(this).attr("class", "tab_btn1");
            },
             function() {
                 if ($(this).attr("class") != "a tab_btn1")
                     $(this).attr("class", "tab_btn");
             });

            //选项卡 点击效果
            $("a[name^=a_]").click(function() {
                $(".tab_btn1").attr("class", "tab_btn");
                $(this).attr("class", "a tab_btn1");
                $("div[id^=div]").hide();
                $("#div" + $(this).attr("name")).show();
                $("#HbtnId").val($(this).attr("name"));
            });
        });
        function CheckInfo() {
            var t_FJuridcialCode = document.getElementById("t_FJuridcialCode");
            if (t_FJuridcialCode) {
                var patrn = /^[A-Za-z0-9]{8}-[A-Za-z0-9]{1}$/;
                if (!patrn.exec(t_FJuridcialCode.value)) {
                    alert("组织结构代码格式不正确");
                    t_FJuridcialCode.focus();
                    return false
                }
            }
            return AutoCheckInfo();
        }
        function showdiv() {
            var v = $("#HbtnId").val();
            if (!v) { v = "a_1"; }
            $("div[id^=div]").hide();
            $("#div" + v).show();
            $("a[name=" + v + "]").attr("class", "tab_btn1");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                图审机构基本信息
            </th>
        </tr>
    </table>
    <div class="tabBar" style="width: 98%; margin: 2px auto;">
        <div class="tabBar_l">
        </div>
        <a name="a_1" class="tab_btn1"><strong>企业基本信息</strong></a> <a name="a_2" class="tab_btn">
            <strong>人员配备情况</strong></a> <a name="a_3" class="tab_btn"><strong>信用信息</strong></a>
        <a name="a_4" class="tab_btn"><strong>业绩信息</strong></a>
        <div class="tabBar_r">
        </div>
    </div>
    <div id="diva_1">
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    单位名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="303px" ReadOnly="true"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    注册地址：
                </td>
                <td colspan="3">
                    <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                    <asp:TextBox ID="t_FRegistAddress" runat="server" CssClass="m_txt" Width="224px"
                        MaxLength="30"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    组织机构代码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FJuridcialCode" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td align="center" class="t_r t_bg" height="21" nowrap="nowrap">
                    法定代表人：
                </td>
                <td class="txt31">
                    <asp:TextBox ID="t_FOTxt5" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    资质等级：
                </td>
                <td>
                    <asp:DropDownList ID="c_FLevel" runat="server">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    证书编号：
                </td>
                <td>
                    <asp:TextBox ID="c_FCertiNo" runat="server" CssClass="m_txt" MaxLength="20"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    单位联系人：
                </td>
                <td>
                    <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt" MaxLength="10"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    联系人手机：
                </td>
                <td>
                    <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt" MaxLength="15" onblur="isInt(this);"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    EMail：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FEMail" runat="server" CssClass="m_txt" MaxLength="30" Width="302px"></asp:TextBox>
                </td>
            </tr>
        </table>
        
        
         <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    资质证书信息
                </td>
                <td class="t_r">
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgCerti_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="dgCerti_List_ItemDataBound" Style="margin-top: 3px;
            margin-bottom: 1px;" Width="98%">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn Visible="false">
                    <ItemStyle Width="30px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll1" runat="server" onclick="checkAllByTag(this,'CheckItem2');" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem2" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FCertiNo" HeaderText="证书编号"></asp:BoundColumn>
                <%--<asp:BoundColumn DataField="FLevelName" HeaderText="证书等级"></asp:BoundColumn>
                <asp:BoundColumn DataField="FCertiType" HeaderText="证书类别"></asp:BoundColumn>--%>
                <asp:BoundColumn DataField="FAppDeptId" HeaderText="颁发部门"></asp:BoundColumn>
                <asp:BoundColumn DataField="FAppTime" HeaderText="发证日期" DataFormatString="{0:yyyy-MM-dd}">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FEndTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="有效期">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    
    </div>
    <div id="diva_2" style="display: none">
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_c t_bg">
                    <b>执业类型</b>
                </td>
                <td class="t_c t_bg">
                    <b>人员数量</b>
                </td>
                <td class="t_c t_bg">
                    <b>执业类型</b>
                </td>
                <td class="t_c t_bg">
                    <b>人员数量</b>
                </td>
            </tr>
            <asp:Literal ID="litEmp" runat="server"></asp:Literal>
        </table>
    </div>
    <div id="diva_3" style="display: none">
    </div>
    <div id="diva_4" style="display: none">
        <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 3px;
            margin-bottom: 1px;" Width="98%">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FName" HeaderText="工程名称"></asp:BoundColumn>
                <asp:BoundColumn DataField="FProjectAddress" HeaderText="工程地址"></asp:BoundColumn>
                <asp:BoundColumn DataField="FConUnit" HeaderText="建设单位"></asp:BoundColumn>
                <asp:BoundColumn DataField="FBeginDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="工程开始时间">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FEndTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="工程结束时间">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <div style="padding-left: 1%">
            <uc1:pager ID="Pager1" runat="server"></uc1:pager>
        </div>
    </div>
    <input id="HbtnId" runat="server" type="hidden" value="a_1" />
    </form>
</body>
</html>
