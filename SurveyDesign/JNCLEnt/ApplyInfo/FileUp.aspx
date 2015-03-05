<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUp.aspx.cs" Inherits="KC_AppMain_FileUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        function SelectFiles() {
            var width = 600;
            var height = 400;
            sUrl = '<%=ProjectBLL.RBase.GetSysObjectName("FileServerPath") %>tiny_mce/plugins/ajaxfilemanager/filemanager.aspx?type=file&iseditor=1&p=<%=SecurityEncryption.DesEncrypt("../../|"+Session["FUserId"]+"|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now.AddHours(1)),"12345687")%>';
            var rv = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;');
            if (rv != null && rv.split('|')[0] != 'undefined') {
                $('#t_FUrl').val(rv.split('|')[0]);
                $('#t_FFileSize').val(rv.split('|')[1]);
                $("#btnQuery").click();
                return true;
            }
            return false;
        }


        function check() {
            if ($("#t_FUrl").val() == "") {
                alert("请选择文件");
                return false;
            }
            if ($("#t_FName").val() == "") {
                alert("请填写附件名称");
                $("#t_FName").focus();
                return false;
            }
            return trim;
        }



    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="2">上传
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
                    <asp:Button ID="btnNext" runat="server" Text="上传下一个" class="m_btn_w6" OnClick="btnNext_Click" />
                    <input id="btnGetBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" /><asp:Button ID="btnDel" runat="server" Text="删除" class="m_btn_w2" OnClick="btnDel_Click" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table align="center" class="m_table" style="width: 98%">
            <tr>
                <td class="t_r t_bg">附件类型：
                </td>
                <td>
                    <asp:Literal ID="lType" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">附件：
                </td>
                <td>
                    <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
                        AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
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
                                <ItemStyle Width="30px" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FFileName" HeaderText="附件名" HeaderStyle-Width="200px">
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="查看" HeaderStyle-Width="50px">
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FFilePath" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr id="tr1" runat="server">
                <td class="t_r t_bg">附件名称：
                </td>
                <td>
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="300"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr id="tr2" runat="server">
                <td class="t_r t_bg">上传附件：
                </td>
                <td style="padding: 4px; line-height: 28px;">
                    <input type="hidden" id="t_FUrl" runat="server" />
                    <input type="hidden" id="t_FFileType" runat="server" />
                    <input type="hidden" id="t_FFileSize" runat="server" />
                    <asp:Literal ID="name" runat="server" Text="<tt>请选择文件</tt>"></asp:Literal>
                    <br />
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Style="display: none;" />
                    <input id="btnSelect" runat="server" class="m_btn_w6" onclick="SelectFiles();" type="button"
                        value="选择文件..." />
                </td>
            </tr>
        </table>
        <input id="t_YWBM" runat="server" type="hidden" />
        <input id="t_type" runat="server" type="hidden" />
    </form>
</body>
</html>
