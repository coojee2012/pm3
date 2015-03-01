<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RYQKList.aspx.cs" Inherits="WYDW_XMQK_RYQKList" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目人员情况</title>
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
                    <th colspan="2">项目人员情况
                    </th>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
                <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                    HorizontalAlign="Center" OnItemCommand="DG_List_ItemCommand" OnItemDataBound="DG_List_ItemDataBound"
                    Style="margin-top: 7px" Width="98%">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <ItemStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:BoundColumn HeaderText="序号">
                            <HeaderStyle Width="40px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="姓名" DataField="fPersonName">
                            <HeaderStyle Width="200px"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="性别" DataField="fSex">
                            <HeaderStyle Width="70px"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="fCardType" HeaderText="证件类型">
                            <HeaderStyle Width="110px"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="证件号码" DataField="fCardID">
                            <HeaderStyle Width="180px"></HeaderStyle>
                        </asp:BoundColumn>

                        <asp:BoundColumn HeaderText="职务" >
                            <HeaderStyle Width="200px"></HeaderStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="联系电话" HeaderStyle-Width="100px" DataField="fgrdh">
                            <HeaderStyle Width="130px"></HeaderStyle>

                            <ItemStyle CssClass="lh t_l" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="XMBH" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="fPosition" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="fCardType" Visible="False"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                <uc1:pager ID="Pager1" runat="server"></uc1:pager>
            </div>
        </div>
    </form>
</body>
</html>
