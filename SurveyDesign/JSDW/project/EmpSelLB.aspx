﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpSelLB.aspx.cs" Inherits="JSDW_project_EmpSelLB" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
        function selEmp() {
            var tr = $(this).parent().parent();
            var hLock = $('[id$="h_lock"]');
            if (hLock && hLock.val() == "1") {
                alert("所选人员已参与其他区域在建工程，不允许选入！");
                return false;
            }
            return confirm('确认要选择该人员吗?');
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="3">
                <asp:Label ID="lTitle" runat="server" Text="单位" ></asp:Label>人员列表
            </th>
        </tr>
        <tr>
            <td colspan="1" class="t_r">
                姓名
            </td>
            <td colspan="1" class="t_l">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
            </td>
            <td rowspan="3" class="t_l">
                
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                &nbsp;<input type="button" id="Button1" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
        </tr>
        <tr>
            <td colspan="1" class="t_r">
                证件号码
            </td>
            <td colspan="1" class="t_l">
                <asp:TextBox ID="txtIDCard" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Repeater ID="dg_List" runat="server" OnItemDataBound="dg_List_ItemDataBound" OnItemCommand="dg_List_ItemCommand">
        <HeaderTemplate>
            <table width="98%" align="center" class="m_dg1">
                <tr class="m_dg1_h">
                    <th>
                        序号
                    </th>
                    <th>
                        锁定详情
                    </th>
                    <th>
                        姓名
                    </th>
                    <th>
                        身份证号
                    </th>
                    <th>
                        性别
                    </th>
                    <th>
                       注册编号
                    </th>
                    <th>
                        注册专业
                    </th>
                    <th>
                        证书有效期
                    </th>
                    <th>
                        发证日期
                    </th>
                    <th>
                        选择
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                    <tr class="m_dg1_i">
                        <td>
                            <%# Container.ItemIndex + 1%> 
                        </td>
                        <td>
                            <asp:Label ID="lblLock" runat="server" Text="Label" ></asp:Label>
                            <asp:HiddenField ID="h_lock"  runat="server" />
                        </td>
                        <td>
                            <%# Eval("XM")%>
                        </td>
                        <td>
                            
                            <%# Eval("ZJBH")%>
                        </td>
                        <td>
                            <%# Eval("XB")%>
                        </td>
                        <td>
                            
                            <%# Eval("ZCZSH")%>
                        </td>
                        <td>
                            <%# Eval("ZCZYMC")%>
                        </td>
                        <td>
                            
                            <%# Eval("ZCZSYXQ")%>
                        </td>
                        <td>
                            <%# Eval("ZCZSFZSJ")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnSelect" CommandName="Sel" runat="server">选择</asp:LinkButton>
                             <asp:HiddenField ID="hfEmpId" Value='<%# Eval("RYBH") %>' runat="server" />
                        </td>
                    </tr>
 
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <div style="padding-left: 1%">
        <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
            CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
            CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
            NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
            PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>
