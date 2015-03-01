<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qyBadDQTJ.aspx.cs" Inherits="audit_XM_qyBadDQTJ" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript">
        function getFilUp(url) {
            var fid = document.getElementById("t_FID").value;
            if (fid == null || fid == undefined || fid == "") {
                alert("当前业务信息错误!"); return;
            }
            showAddWindow(url + "?id=" + fid, 550, 400);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">分地区当前统计
                </th>
            </tr>
        </table>
        <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
            AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FNAME" HeaderText="属地" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A100_1" HeaderText="建设单位最新处罚" HeaderStyle-Width="150px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A100_2" HeaderText="建设单位累计处罚" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A101_1" HeaderText="施工企业最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>              
                <asp:BoundColumn DataField="A101_1" HeaderText="施工企业累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A125_1" HeaderText="工程监理最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A125_2" HeaderText="工程监理累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A155_1" HeaderText="勘察设计最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A155_2" HeaderText="勘察设计累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A120_1" HeaderText="招标代理最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A120_2" HeaderText="招标代理累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A130_1" HeaderText="房地产开发最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A130_2" HeaderText="房地产开发累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A135_1" HeaderText="园林绿化最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A135_2" HeaderText="园林绿化累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A175_1" HeaderText="检测机构最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A175_2" HeaderText="检测机构累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A186_1" HeaderText="房地产评估最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A186_2" HeaderText="房地产评估累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A187_1" HeaderText="物业服务最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A187_2" HeaderText="物业服务累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A196_1" HeaderText="设计施工一体化最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A196_2" HeaderText="设计施工一体化累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A202_1" HeaderText="规划编制最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A202_2" HeaderText="规划编制累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A270_1" HeaderText="审图机构最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A270_2" HeaderText="审图机构累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A180_1" HeaderText="入川企业最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A180_2" HeaderText="入川企业累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A190_1" HeaderText="入川监理最新处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="A190_2" HeaderText="入川监理累计处罚" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ZXCFCS" HeaderText="最新处罚累计" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LJCFCS" HeaderText="历史处罚次数累计" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ZXXJ" HeaderText="最新处罚小计" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LSXJ" HeaderText="历史处罚次数小累计" HeaderStyle-Width="80px">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemStyle CssClass="t_l" Wrap="false" />
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <table align="center" width="98%">
            <tr>
                <td>
                    <uc1:pager ID="Pager1" runat="server"></uc1:pager>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
