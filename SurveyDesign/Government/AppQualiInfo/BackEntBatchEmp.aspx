<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackEntBatchEmp.aspx.cs"
    EnableEventValidation="false" Inherits="Government_AppQualiInfo_BackEntBatchEmp" %>

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
    </script>

    <title>非注册人员批量打回</title>
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
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <div id="divDHQY">
        <table align="center" class="m_table" width="98%">
            <tr>
                <td colspan="2" class="t_r t_bg" width="20%">
                    打回意见：
                </td>
                <td colspan="2" style="vertical-align: top; padding-top: 5px">
                    <asp:TextBox ID="txtFBackIdea" MaxLength="100" Width="414px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="4">
                    <asp:Button ID="btnBackEnt" OnClientClick="return checkBackIdea(this);" runat="server"
                        Text="打回企业" CssClass="m_btn_w4" OnClick="btnBackEnt_Click" UseSubmitBehavior="false" />
                    &nbsp;
                    <input id="btnReturn" type="button" class="m_btn_w2" value="返回" onclick="window.close();" />
                </td>
            </tr>
        </table>
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
    function checkBackIdea(obj) {
        if ($("#txtFBackIdea").val() == "") {
            alert("请填写打回意见！");
            return false;
        }
        if (confirm('确认要打回吗？')) {
            obj.disabled = true;
            obj.value = "请稍后...";
            __doPostBack(obj.id, '');
        }
        else {
            return false;
        }
    }
</script>

