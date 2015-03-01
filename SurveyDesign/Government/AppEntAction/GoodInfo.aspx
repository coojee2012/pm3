<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodInfo.aspx.cs" Inherits="Government_AppEntAction_GoodInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>举报</title>
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
            if (!AutoCheckInfo()) {
                return false;
            }
            if (!getLength(document.getElementById("t_FContent"), 50, '“举报内容”')) {
                return false;
            }

            return true;
        }
        
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业良好行为
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
                <asp:Button ID="btnSave" runat="server" Text="通过" CssClass="m_btn_w2" CommandArgument="6"
                    OnClientClick="return checkInfo();" OnCommand="btnSave_Click"  />
                <asp:Button ID="btnReport" runat="server" Text="不通过" CssClass="m_btn_w2" CommandArgument="9"
                    OnClientClick="return checkInfo();" OnCommand="btnSave_Click" />
                <input type="button" value="返回" class="m_btn_w2" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                企业名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FProjectName" runat="server" CssClass="m_txt" Width="285px" ReadOnly="true"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                良好行为名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FAddress" runat="server" CssClass="m_txt" Width="285px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                奖项级别：
            </td>
            <td>
                <asp:DropDownList ID="t_FTxt1" runat="server">
                    
                </asp:DropDownList>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                获奖类别：
            </td>
            <td>
                <asp:DropDownList ID="t_FTxt2" runat="server">
                </asp:DropDownList>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                证书(文件)名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FWay" runat="server" CssClass="m_txt" Width="285px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                证书(文件)编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FRule" runat="server" CssClass="m_txt" Width="185px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                发证(发文)机关：
            </td>
            <td>
                <asp:TextBox ID="t_FDeptIdname" runat="server" CssClass="m_txt" Width="185px"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                颁证(发文)时间：
            </td>
            <td>
                <asp:TextBox ID="t_FHTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()" Width="185px"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                备注：
            </td>
            <td class="m_txt_M" colspan="3">
                <asp:TextBox ID="t_FTxt3" runat="server" CssClass="m_txt" Width="287px" MaxLength="30"
                    Height="60px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_title">
        <tr>
            <td colspan="4" class="t_bg" style="padding-left: 20px; color: Red;">
                附件：
                <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnShowFile_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="m_txt_M">
                <table class="m_dg1" width="100%" align="center">
                    <tr class="m_dg1_h">
                        <td style="width: 30px;">
                            序号
                        </td>
                        <td>
                            资料名称
                        </td>
                        <td>
                            是否必需
                        </td>
                        <td style="width: 60px;">
                            已上传<br />
                            文件个数
                        </td>
                        <td style="width: 160px;">
                            <font color="green">是</font>/<font color="red">否</font> 上传
                        </td>
                    </tr>
                    <asp:Repeater ID="rep_List" runat="server" OnItemDataBound="rep_List_ItemDataBound">
                        <ItemTemplate>
                            <tr class="m_dg1_select">
                                <td>
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td class="t_l">
                                    <%#Eval("FFileName")%>
                                </td>
                                <td>
                                    <%#Eval("FIsMust")%>
                                </td>
                                <td>
                                    <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="lit_Has" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <asp:Repeater ID="rep_File" runat="server" OnItemCommand="rep_File_ItemCommand">
                                <ItemTemplate>
                                    <tr class="m_dg1_i">
                                        <td colspan="6" class="t_l" style="padding-left: 50px;">
                                            (<%# Container.ItemIndex+1 %>)、 <a href='<%#Eval("FFilePath") %>' target="_blank"
                                                title="点击查看该文件">
                                                <%#Eval("FFileName")%>
                                            </a>
                                            <asp:LinkButton ID="btnDel" runat="server" Text="[删除]" CommandName="cnDel" CommandArgument='<%#Eval("FID") %>'
                                                OnClientClick="return confirm('确定要删除吗？');"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
