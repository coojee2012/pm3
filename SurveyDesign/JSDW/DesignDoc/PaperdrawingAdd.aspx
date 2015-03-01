<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaperdrawingAdd.aspx.cs"
    Inherits="PrjManage_ConstructionLicence_ApplyBaseinfo_PaperdrawingAdd" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加附件</title>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script language="javascript">
        function check() {
            if (document.getElementById("t_FFileNo").value == "") {
                alert("请填写文号");
                return false;
            }
            return true;
        }
        function showUpPicWindow(sUrl, width, height) {
            if (document.getElementById('hidden_Fid').value == null || document.getElementById('hidden_Fid').value == "") {
                alert('请先保存批准机关及文号');
                return false;
            }
            var idvalue = window.showModalDialog(sUrl + '?FPrjFileId=<%=Request["fid"] %>&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:no;')

            if (idvalue != null && idvalue == "1") {
                document.getElementById('btnReload').click();

            }
        }
 
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                添加附件
            </th>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                文号：
            </td>
            <td>
                <asp:TextBox ID="t_FFileNo" runat="server" Width="247px" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                批准机关：
            </td>
            <td>
                <asp:TextBox ID="t_FAppDeptName" runat="server" Width="247px" CssClass="m_txt"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>
        <tr id="tr1" runat="server" visible="false">
            <td class="t_r t_bg">
                <asp:Literal ID="liFRemark" runat="server"></asp:Literal>
            </td>
            <td>
                <tt>
                <asp:DropDownList ID="t_FIsUp" runat="server">
                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                </asp:DropDownList>
                *</tt>
            </td>
        </tr>
        <tr>
            <td align="center" height="21" colspan="2">
                <input id="btnAdd" onclick="showUpPicWindow('../appmain/FileUp.aspx',554,234);" type="button"
                    value="上 传" class="m_btn_w2" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click" CssClass="m_btn_w2" />
                <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_Click" CssClass="m_btn_w2" />
                <asp:Button ID="btnReload" runat="server" Text="Button" OnClick="btnReload_Click"
                    Style="display: none" CssClass="m_btn_w2" />
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td style="color: #FF0000; height: 25px; font-size: 13px;">
                &nbsp;在上传文件及填写文件编号后，请点击保存，否则不能上传。
            </td>
        </tr>
        <tr>
            <td class="t_c">
                <asp:DataGrid ID="Other_List" CssClass="m_dg1" runat="server" AutoGenerateColumns="False"
                    align="center" BorderWidth="1px" HorizontalAlign="Justify" Style="margin-top: 5px"
                    Width="100%" OnItemDataBound="Other_List_ItemDataBound" OnItemCommand="Other_List_ItemCommand">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <ItemStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:BoundColumn HeaderText="序号">
                            <ItemStyle Width="40px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FFileName" HeaderText="资料名称">
                            <ItemStyle Width="250px" CssClass="dec" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FSize" HeaderText="文件大小(KB)"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="删除">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" CssClass="m_btn_w2" runat="server" CommandName="Delete"
                                    Text="删除" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="FFilePath" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                <div style="width: 100%; margin-top: 8px;">
                    <uc1:pager ID="Pager1" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <input id="hiddle_IsSaveOk" runat="server" type="hidden" value="0" />
    <input id="hidden_Fid" runat="server" type="hidden" />
    </form>
</body>
</html>
<%--<script language="javascript">
window.onbeforeunload = function(){
    alert("请填写文件编号！");
    //return false;
}
</script>--%>