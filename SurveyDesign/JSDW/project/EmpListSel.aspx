<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpListSel.aspx.cs"
    Inherits="JSDW_project_EmpListSel" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            //如果有“锁定” 就把行颜色设置为灰色
            $("td").each(function (i) {
                if ($.trim($(this).text()) === "锁定") {
                    $(this).parents("tr").css('color', '#0000ff');
                }
            });
            SetDDLEmpType();
        });
        function CheckInfo() {
            return AutoCheckInfo();
        }
        
        function selEmp(item) {

            var trNode = item.parentNode.parentNode.childNodes[3];
           
            if (trNode) {
                var nodetext = trNode.innerText;

                if ($.trim(nodetext) == "锁定") {
                    alert("所选人员已参与其他区域在建工程，不允许选入！");
                    return false;
                } else {
                    return true;
                }
            }
            return confirm('确认要选择该人员吗?');
            return true;
        }
        //MODIFY:YTB 增加显示锁定人员信息函数
        function showEmpinfo(idcard) {
            showAddWindow('EmpLockInfo.aspx?idCard=' + idcard, 700, 300);
        }
        function SetDDLEmpType()
        {
            var rylx = document.getElementById("<%=lblRylx.ClientID%>").value;
                if (rylx == "t_SGRYId") {
                    document.getElementById("<%=ddlEmpType.ClientID%>").value = "2";
                }
                if (rylx == "t_JLRYId") {
                    document.getElementById("<%=ddlEmpType.ClientID%>").value = "1";
                }
        }
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="3">
                <input type="hidden" name="lblRylx" id="lblRylx" runat="server" />
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
        <tr>
            <td colspan="1" class="t_r">
                人员类型
            </td>
            <td colspan="1" class="t_l">
              <asp:DropDownList ID="ddlEmpType" runat="server" Width="200px" Enabled="false">
                    <asp:ListItem Value="-1">--全部--</asp:ListItem>
                    <asp:ListItem Value="1">项目技术负责人</asp:ListItem>
                    <asp:ListItem Value="2">项目负责人</asp:ListItem>
                  <asp:ListItem Value="3">安全负责人</asp:ListItem>
                  <asp:ListItem Value="4">建造师</asp:ListItem>
                  <asp:ListItem Value="29">施工员</asp:ListItem>
                  <asp:ListItem Value="101">质量员</asp:ListItem>
                  <asp:ListItem Value="100">安全员</asp:ListItem>
                  <asp:ListItem Value="105">材料员</asp:ListItem>
                  <asp:ListItem Value="102">预算员</asp:ListItem>
                </asp:DropDownList>

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
                    <tr class="m_dg1_i" runat="server" id ="row">
                        <td>
                            <%# Container.ItemIndex + 1%> 
                        </td>
                        <td>
                            <asp:LinkButton ID="lkb_Lock" runat="server" Text="Label"></asp:LinkButton>                            
                            <asp:HiddenField ID="h_lock"  runat="server" />
                        </td>
                        <td>
                            <%# Eval("XM")%>
                        </td>
                        <td>
                            
                            <%# Eval("SFZH")%>
                        </td>
                        <td>
                            <%# Eval("XBStr")%>
                        </td>
                        <td>
                            
                            <%# Eval("ZCZSH")%>
                        </td>
                        <td>
                            <%# Eval("ZCZY")%>
                        </td>
                        <td>
                            <%# Eval("ZSYXQJSSJStr")%>
                            <asp:HiddenField ID="zsyxq" Value='<%# Eval("ZSYXQJSSJStr") %>' runat="server" />
                            <%--<asp:Label ID="yxq" runat="server" Text="Label" ></asp:Label>
                            <asp:HiddenField ID="yxqks"  runat="server" value='<%# Eval("ZSYXQKSSJ")%>'/>
                            <asp:HiddenField ID="yxqjs"  runat="server" value='<%# Eval("ZSYXQJSSJ")%>'/>--%>
                        </td>
                        <td>
                            <%# Eval("FZSJStr")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnSelect" CommandName="Sel" runat="server">选择</asp:LinkButton>
                            <asp:HiddenField ID="hfEmpId" Value='<%# Eval("RYZSXXID") %>' runat="server" />   
                            <asp:HiddenField ID="RYBH" Value='<%# Eval("RYZSXXID") %>' runat="server" /><!--千万不能改成RYBH,太多的地方用的是证书编号 by zyd-->
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
