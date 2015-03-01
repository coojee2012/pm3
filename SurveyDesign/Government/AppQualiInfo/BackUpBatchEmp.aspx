<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackUpBatchEmp.aspx.cs" EnableEventValidation="false"
    Inherits="Government_AppQualiInfo_BackUpBatchEmp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <base target="_self">
    </base>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        function getValue() {
            var obj = window.dialogArguments;
            document.getElementById("HFID").value = obj.id;
        }
        function checkInfo(obj) {
            var ok = true;
            $.each($(':text[id*=txtFUserName]'), function() {
                if ($(this).val() == '') {
                    $(this).css('background-color', 'yellow');
                    ok = false;
                    return;
                }
            });
            if (ok) {
                if (confirm('确认要审核吗?')) {
                    this.disabled = true;
                    this.value = '数据提交中...';
                    __doPostBack(obj.id, '');
                }
            }
            else {
                alert('卡号不能为空');
                return false;
            }
        }
    </script>

    <title>非注册人员批量审核</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                批量审核
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 10px">
                <asp:Button ID="btnApp" runat="server" CssClass="m_btn_w2" Text="审核" OnClick="btnReport_Click"
                    OnClientClick="return checkInfo(this)" UseSubmitBehavior="False" />
                <input id="btnReturn" type="button" class="m_btn_w2" value="返回" onclick="window.close();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <div id="entList">
        <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="50px" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FName" HeaderText="姓名">
                    <ItemStyle Wrap="False" CssClass="t_c" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FEntName" HeaderText="企业名称">
                    <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FIdCard" HeaderText="身份证">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="卡号">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFUserName" runat="server" CssClass="m_txt" Enabled="false"></asp:TextBox>
                        <tt>*</tt>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <input id="HFID" runat="server" type="hidden" value="INIT" />
    <input id="HFID1" runat="server" type="hidden" /><asp:Button ID="btnShowInfo" runat="server"
        OnClick="btnShowInfo_Click" Style="display: none" Text="Button" />
    </form>
</body>
</html>

<script language="javascript">
    if (document.getElementById("HFID").value == "INIT") {
        getValue();
    }
    else {
        document.getElementById("HFID").value = "";
    }
    if (document.getElementById("HFID").value != "") {

        document.getElementById("HFID1").value = document.getElementById("HFID").value;
        document.getElementById("btnShowInfo").click();


    } 
</script>

