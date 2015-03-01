<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPrjItem.aspx.cs" Inherits="JSDW_appmain_AddPrjRegist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单体工程信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
        });
        function checkInfo() {
            return AutoCheckInfo();
        }

        function addParameter() {
            var fid = '<%=ViewState["FID"] %>';
            if (fid == null || fid == '') {
                alert('请先保存单项子工程信息！');
                return;
            }
            showAddWindow('AddParameter.aspx?FPrjItem=' + fid+"&FPrjId=<%=Request.QueryString["fprjId"] %>&type=<%= p_FType.SelectedValue  %>", 300, 250);
        } 
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Label ID="lTitle" runat="server" Text="房屋建筑" ForeColor="Red"></asp:Label>单体工程信息
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
                <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <div id="div_t1" runat="server">
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" width="12%">
                    工程名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FPrjItemName" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程设计等级：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FLevel" runat="server" CssClass="m_txt t_r" Width="70px" MaxLength="25"
                        onblur="isInt(this)"></asp:TextBox>(级) <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建筑使用性质：
                </td>
                <td colspan="3">
                    <asp:CheckBoxList ID="t_FNature" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        RepeatColumns="8">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    设计使用年限：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FYear" onblur="isInt(this)" runat="server" CssClass="m_txt t_r"
                        Width="70px"></asp:TextBox>(年) <tt>*</tt>
                </td>
                <td class="t_r t_bg" width="12%">
                    建筑面积：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FArea" onblur="isFloat(this)" runat="server" CssClass="m_txt t_r"
                        Width="70px"></asp:TextBox>(㎡) <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建筑层数：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FLayers" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"></asp:TextBox>(层)
                    地上：
                    <asp:TextBox ID="t_FGround" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"></asp:TextBox>(层)
                    地下：
                    <asp:TextBox ID="t_FUnderground" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"></asp:TextBox>(层)
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    耐火等级：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FFire" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"></asp:TextBox>(级)
                    地上：
                    <asp:TextBox ID="t_FFireUp" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"></asp:TextBox>(级)
                    地下：
                    <asp:TextBox ID="t_FFireDown" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"></asp:TextBox>(级)
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    防水等级：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FWaterproof" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"></asp:TextBox>(级)
                    屋面：
                    <asp:TextBox ID="t_FWaterUp" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"></asp:TextBox>(级)
                    地下：
                    <asp:TextBox ID="t_FWaterDown" runat="server" CssClass="m_txt t_r" Width="70px" onblur="isInt(this)"></asp:TextBox>(级)
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    结构类型：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FStruType" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    抗震设防烈度：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FIntensity" runat="server" CssClass="m_txt t_r" MaxLength="30" Width="70px"
                        onblur="isFloat(this)"></asp:TextBox>(度) <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    抗震等级：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FSeismic" runat="server" CssClass="m_txt t_r" MaxLength="30" Width="70px"></asp:TextBox>(级)
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    是否节能审查：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FEnergy" runat="server" CssClass="m_txt">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="div_t2" runat="server" visible="false">
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    工程名称：
                </td>
                <td>
                    <asp:TextBox ID="p_FPrjItemName" runat="server" CssClass="m_txt" Width="250px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程类别：
                </td>
                <td>
                    <asp:DropDownList ID="p_FType" runat="server" CssClass="m_txt">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m" align="left">
                    技术指标↓↓
                </td>
                <td class="m_bar_m" align="right">
                    <asp:Button ID="btnAdd" runat="server" Text="新增" class="m_btn_w2" OnClientClick="addParameter();"
                        OnClick="btnSave1_Click" />
                    <asp:Button ID="btnSave1" runat="server" CssClass="m_btn_w2" Text="刷新" OnClick="btnSave1_Click" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
            Width="98%" OnCancelCommand="DG_List_CancelCommand" OnEditCommand="DG_List_EditCommand"
            OnUpdateCommand="DG_List_UpdateCommand" OnDeleteCommand="DG_List_DeleteCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号" ReadOnly="True">
                    <ItemStyle Width="30px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FType" HeaderText="技术指标" ReadOnly="True"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="值">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FValue") %>'
                            CssClass="m_txt" ID="txtFValue"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FValue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="FUNIT" HeaderText="单位" ReadOnly="True"></asp:BoundColumn>
                <asp:EditCommandColumn CancelText="取消" EditText="编辑" HeaderText="编辑" UpdateText="更新">
                </asp:EditCommandColumn>
                <asp:ButtonColumn CommandName="Delete" HeaderText="删除" Text="删除"></asp:ButtonColumn>
                <asp:BoundColumn DataField="FID" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    </form>
</body>

<script type="text/javascript">
    function changeCheck(obj) {
        obj.style.background = obj.checked ? '#1eaffc' : "";
    }
    $.each($(":checkbox[id^=t_F]"), function() {
        $(this).click(function() { changeCheck(this); });
        changeCheck(this);
    }); 
</script>

</html>
