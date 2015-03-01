<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTBAList.aspx.cs" Inherits="WYDW_XMQK_HTBAInfo" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目合同备案</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../JSDW/../script/jquery.js"></script>

    <script type="text/javascript" src="../../JSDW/../script/default.js"></script>

    <script type="text/javascript" src="../../JSDW/../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();

        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function ShowAppPage(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; status:no; help:no;');

            if (ret == "ok") {
                location.reload();
            }
        }
    </script>

    <base target="_self"></base>
    <style type="text/css">
        .modalDiv {
            position: absolute;
            top: 1px;
            left: 1px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: gray;
            opacity: .50;
            filter: alpha(opacity=50);
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div style="height: 100%; width: 100%;">

            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">项目合同备案
                    </th>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
                <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                    HorizontalAlign="Center" Style="margin-top: 6px; margin-bottom: 1px;"
                    Width="98%" OnItemDataBound="dg_List_ItemDataBound" OnItemCommand="dg_List_ItemCommand">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <ItemStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:BoundColumn HeaderText="序号">
                            <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="合同编号">
                            <ItemTemplate>
                                <asp:LinkButton ID="HTBH" runat="server" CommandName="See" Text='<%#Eval("HTBH")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="t_l" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="WTDW" HeaderText="委托单位">
                            <ItemStyle CssClass="t_l" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="JGRQ" HeaderText="接管日期">
                            <ItemStyle CssClass="t_l" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="HTQDRQ" HeaderText="签订日期">
                            <ItemStyle CssClass="t_l" HorizontalAlign="Center"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="HTSXRQ" HeaderText="生效日期">
                            <ItemStyle CssClass="t_l" HorizontalAlign="Center"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="HTJZRQ" HeaderText="截止日期">
                            <ItemStyle CssClass="t_l" HorizontalAlign="Center"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="PrjItemType" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ConstrType" Visible="False"></asp:BoundColumn>--%>
                    </Columns>
                </asp:DataGrid>
                <div style="padding-left: 1%">
                    <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                    <br />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
