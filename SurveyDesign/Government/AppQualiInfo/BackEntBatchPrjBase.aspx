<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackEntBatchPrjBase.aspx.cs"
    Inherits="Government_AppQualiInfo_BackEntBatchPrjBase" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>打回企业</title>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
            //显示打回意见
            var boxs = $("#" + '<%=ckListIdea.ClientID %>').find(":checkbox");
            var idea = '';
            $(boxs).click(function() {
                var idea = '';
                $(boxs).each(function() {
                    if (this.checked) {
                        idea += $(this).parent().find("LABEL").text() + ";\r\n";
                    }
                });
                $("#txtFBackIdea").attr("value", idea);
            });
        });
        function getValue() {
            var obj = window.dialogArguments;
            document.getElementById("HFID").value = obj.id;

        }  
    </script>

    <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th>
                打回
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" style="text-align: right; padding-right: 10px">
                <input id="Submit1" class="m_btn_w6" onclick="showControl(document.getElementById('entList'))"
                    type="button" value="显示数据" />
                <input id="Bottom1" class="m_btn_w6" onclick="hidControl(document.getElementById('entList'))"
                    type="button" value="隐藏数据" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <div id="entList" style="display: none">
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
                <asp:BoundColumn DataField="FPrjName" HeaderText="项目名称">
                    <ItemStyle Wrap="False" CssClass="t_l" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FBaseInfoId" HeaderText="单位名称">
                    <ItemStyle Font-Underline="False" Wrap="False" CssClass="t_l" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="申报内容" Visible="false">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="" Visible="false">
                    <ItemStyle Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="" Visible="false">
                    <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid></div>
    <div id="divDHQY">
        <table align="center" class="m_table" width="98%">
            <tr>
                <td colspan="2" style="vertical-align: top; padding-top: 5px" width="40%">
                    <asp:CheckBoxList ID="ckListIdea" runat="server" CssClass="m_txt" RepeatColumns="1">
                    </asp:CheckBoxList>
                </td>
                <td colspan="2" style="vertical-align: top; padding-top: 5px">
                    <asp:TextBox ID="txtFBackIdea" TextMode="MultiLine" Width="414px" runat="server"
                        CssClass="m_txt" Style="word-break: break-all; word-wrap: break-word; text-align: left"
                        Height="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="4">
                    <asp:Button ID="btnBackEnt" OnClientClick="return checkBackIdea(this);" runat="server"
                        Text="打回企业" CssClass="m_btn_w4" OnClick="btnBackEnt_Click" UseSubmitBehavior="false" />
                </td>
            </tr>
        </table>
    </div>
    <input id="HFID" runat="server" type="hidden" value="INIT" />
    <input id="HFID1" runat="server" type="hidden" />
    <asp:Button ID="btnShowInfo" runat="server" Text="Button" Style="display: none" OnClick="btnShowInfo_Click" />
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

