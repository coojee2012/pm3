<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KZXXList.aspx.cs" Inherits="WYDW_XMQK_KZXXList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目扩展信息</title>
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
                    <th colspan="2">项目扩展信息
                    </th>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
                <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                    HorizontalAlign="Center" Style="margin-top: 6px; margin-bottom: 1px;"
                    Width="98%" OnItemCommand="dg_List_ItemCommand" OnItemDataBound="dg_List_ItemDataBound">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <ItemStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:BoundColumn HeaderText="序号">
                            <ItemStyle Font-Underline="False" Width="30px" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="项目名称">
                            <ItemTemplate>
                                <asp:LinkButton ID="XMMC" runat="server" CommandName="See" Text='<%#Eval("XMMC")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="t_l" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="ZDMJ" HeaderText="占地面积">
                            <ItemStyle CssClass="t_l" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="JMHS" HeaderText="居民户数">
                            <ItemStyle CssClass="t_l" HorizontalAlign="Center" />
                        </asp:BoundColumn>
<%--                        <asp:BoundColumn DataField="JZRS" HeaderText="居住人数">
                            <ItemStyle CssClass="t_l" HorizontalAlign="Center"/>
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="FJMHS" HeaderText="非居民户数">
                            <ItemStyle CssClass="t_l" HorizontalAlign="Center"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="JZRS" HeaderText="居住人数">
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
