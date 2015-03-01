<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubFlowList.aspx.cs" Inherits="Admin_main_SubFlowList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
            $(".tab_btn").hover(function() {
                if ($(this).attr("class") != "a tab_btn1")
                    $(this).attr("class", "tab_btn1");
            },
             function() {
                 if ($(this).attr("class") != "a tab_btn1")
                     $(this).attr("class", "tab_btn");
             });
            $("#btnXGZL").click(function() {
                $(".tab_btn1").attr("class", "tab_btn");
                $(this).attr("class", "a tab_btn1");
                $("#table1").hide();
                $("#tablePager").show();                
                $("#SubFlow_List").show();
            });
            $("#btnSPYJ").click(function() {
                $(".tab_btn1").attr("class", "tab_btn");
                $(this).attr("class", "a tab_btn1");
                $("#table1").show();
                $("#tablePager").hide();                
                $("#SubFlow_List").hide();
            }); 
        });  
    </script>

    <script language="javascript">
        function clearQuery() {
            document.getElementById("text_FName").value = "";
            document.getElementById("text_FLevel").value = "";
            document.getElementById("drop_fRoleId").value = "";
        }
    </script>

    <base target="_self"></base>
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                子流程维护
            </th>
        </tr>
    </table>
    <table width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m">
                <input id="btnAdd" class="m_btn_w2" type="button" value="新增" onclick="showAddWindow('ProcessAdd.aspx?e=0',800,700);"
                    runat="server" />
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <asp:Button ID="btnDel" runat="server" CssClass="m_btn_w2" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReload" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="100%" align="center" id="QueryTable1" style="display: none">
        <tr>
            <td class="tdRight" width="100">
                子流程名称：
            </td>
            <td width="100">
                <asp:TextBox ID="text_FName" runat="server" CssClass="cTextBox1" Width="80px"></asp:TextBox>
            </td>
            <td width="100">
                等级：
            </td>
            <td width="100">
                <asp:TextBox ID="text_FLevel" runat="server" CssClass="cTextBox1" Width="70px"></asp:TextBox>
            </td>
            <td width="100">
                角色：
            </td>
            <td width="100">
                <asp:DropDownList ID="drop_fRoleId" runat="server" CssClass="cTextBox1" Width="80px">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:Button ID="btnQuery" runat="server" CssClass="cBtn3" Text="搜索" OnClick="btnQuery_Click" />
                <input id="Button1" type="button" value="清空" class="cBtn3" onclick="clearQuery();"
                    style="margin-left: 10px" />
            </td>
            <td>
            </td>
        </tr>
    </table>
        <div class="tabBar">
        <div class="tabBar_l">
        </div>
        <a class="tab_btn1" id="btnXGZL"><strong>列表</strong></a> 
        <a class="tab_btn" id="btnSPYJ"><strong>流程图</strong></a> 
        <div class="tabBar_r">
        </div>
    </div>
    <asp:DataGrid ID="SubFlow_List" runat="server" HorizontalAlign="Center" Width="100%"
        CssClass="m_dg1" Style="margin-top: 7px;" AutoGenerateColumns="False" OnItemDataBound="SubFlow_List_ItemDataBound"
        OnItemCommand="SubFlow_List_ItemCommand">
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
                <ItemStyle Width="50px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FName" HeaderText="子流程名称"></asp:BoundColumn>
            <asp:BoundColumn DataField="FProcessName" HeaderText="主流程"></asp:BoundColumn>
            <asp:BoundColumn DataField="FLevel" HeaderText="等级"></asp:BoundColumn>
            <asp:BoundColumn DataField="FTypeId" HeaderText="所属阶段"></asp:BoundColumn>
            <asp:BoundColumn DataField="FIsEnd" HeaderText="流程是否结束"></asp:BoundColumn>
            <asp:BoundColumn DataField="FRoleName" HeaderText="角色"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="顺序">
                <ItemStyle Width="100px" />
                <ItemTemplate>
                    <asp:TextBox ID="FOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FOrder") %>'
                        Width="80" CssClass="m_txt" onblur="isInt(this);"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="保存">
                <ItemStyle Width="100px" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnItemSave" runat="server" CommandName="Save" Text="保存"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="FProcessId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table id="tablePager" align="center" width="100%" style="margin-top: 5px;">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </td>
        </tr>
    </table>
    <table align="center" id="table1" style="margin-top: 5px;display:none">
        <asp:Repeater ID="repSubFlow" runat="server">
            <HeaderTemplate>
                <tr>
                    <td style="background-image: url(../image/Start.jpg); height: 60px; background-repeat: no-repeat;
                        text-align: center">
                      开始
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../image/Flow.jpg" />
                    </td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="background-image: url(../image/Step.jpg); height: 55px; background-repeat: no-repeat;
                        text-align: center">
                        <a href="javascript:showApproveWindow('SubFlowAdd.aspx?fid=<%# Eval("FId") %>&fprocessid=<%#Eval("fProcessId") %>',800,600)">
                            <%#Eval("FName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../image/Flow.jpg" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr>
                    <td style="background-image: url(../image/end.jpg); height: 60px; background-repeat: no-repeat;  text-align: center">
                        结束
                    </td>
                </tr>
            </FooterTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
