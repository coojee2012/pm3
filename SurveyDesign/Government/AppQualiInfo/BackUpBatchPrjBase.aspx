<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackUpBatchPrjBase.aspx.cs"
    EnableEventValidation="false" Inherits="Government_AppQualiInfo_BackUpBatchPrjBase" %>

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

    <title>批量审核</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="6">
                批量审核&nbsp;
                <input id="Submit1" class="m_btn_w6" type="button" value="显示数据" onclick="showControl(document.getElementById('entList'))" />
                <input id="Bottom1" class="m_btn_w6" type="button" value="隐藏数据" onclick="hidControl(document.getElementById('entList'))" />
            &nbsp; </td>
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
                    <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="" Visible="false">
                    <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="" Visible="false">
                    <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FRId" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                本级审核意见
            </th>
        </tr>
        <tr>
            <td class="t_r">
                审核人
            </td>
            <td>
                <asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r">
                审核人单位
            </td>
            <td>
                <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                审核人职务
            </td>
            <td>
                <asp:TextBox ID="t_FAppPersonJob" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
            <td class="t_r">
                审核时间
            </td>
            <td>
                <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" onblur="isDate(this);"
                    onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                审核结果
            </td>
            <td>
                <asp:DropDownList ID="dResult" runat="server" CssClass="m_txt">
                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                    <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="t_r">
                审核意见
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FAppIdea" runat="server" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                    Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="text-align: center; margin-top: 2PX">
        <asp:Button ID="btnApp" runat="server" CssClass="m_btn_w2" Text="审核" OnClick="btnReport_Click"
            OnClientClick="this.disabled=true;this.value='数据提交中...';" UseSubmitBehavior="False" />
        <asp:Button ID="btnReturn" runat="server" CssClass="m_btn_w2" Text="返回" OnClick="btnReturn_Click" /></div>
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

