<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBSJList.aspx.cs" Inherits="JSDW_ApplySGXKZGL_CBSJList" %>

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
                <h3>初步设计</h3>
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
        </tr>
        <tr >
            <td class="t_r t_bg">
                批复文件： </td>
            <td colspan="1">
                <asp:TextBox ID="t_PFWJ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>  
            <td class="t_r t_bg">
                批复编号： </td>
            <td colspan="1">
                <asp:TextBox ID="t_PFBH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>          
        </tr>
        <tr>
            <td class="t_r t_bg">
                总用地面积：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_ZYDArea" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m2）
            </td>
            <td class="t_r t_bg">
                建筑占地面积：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_JZZDArea" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m2）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                总建筑面积：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_ZJZArea" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m2）
            </td>
            <td class="t_r t_bg">
                总建筑面积地上：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_ZJZDSArea" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m2）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                总建筑面积地下：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_ZJZDXArea" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m2）
            </td>
            <td class="t_r t_bg">
                总建筑面积（公建）：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_GJArea" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m2）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                总建筑面积（居住）：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_JZArea" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m2）
            </td>
            <td class="t_r t_bg">
                总建筑面积（其他）：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_QTArea" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m2）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建筑栋数：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_Building" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（栋）
            </td>
            <td class="t_r t_bg">
                建筑最高层数：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_Floor" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（层）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                最大建筑高度：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_Height" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（m）
            </td>
            <td class="t_r t_bg">
                容积率：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_RJL" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（%）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建筑密度：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_JZMD" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（%）
            </td>
            <td class="t_r t_bg">
                绿地率：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_LDL" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（%）
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                停车位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="q_Layers" runat="server" CssClass="m_txt" Width="70px"
                    MaxLength="9"></asp:TextBox>(个) 其中地上：
                <asp:TextBox ID="q_Ground" runat="server" CssClass="m_txt" Width="70px"
                    MaxLength="9"></asp:TextBox>(个) 地下：
                <asp:TextBox ID="q_Underground" runat="server" CssClass="m_txt" Width="70px"
                    MaxLength="9"></asp:TextBox>(个)
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                    设计使用年限：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_SJSYNX" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r t_bg">
                    工程设计等级：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="t_GCSJDJ" runat="server" CssClass="m_txt" Width="201px">
                    <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程总概算：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_GCZGS" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（万元）
            </td>
            <td class="t_r t_bg">
                工程规模：
            </td>
            <td colspan="1">
                <asp:TextBox ID="t_GCGM" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（万元）
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                抗震设防等级： </td>
            <td colspan="1">
                <asp:TextBox ID="t_KZSFDJ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>  
            <td class="t_r t_bg">
                设计工艺或结构模式： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JGMS" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>          
        </tr>
     
    </table>
    </form>
</body>
</html>