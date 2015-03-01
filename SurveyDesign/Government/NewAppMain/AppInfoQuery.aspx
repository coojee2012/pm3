<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppInfoQuery.aspx.cs" Inherits="Government_NewAppMain_AppInfoQuery" %>

<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>接见件</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/Govdept.js" language="javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>

    <script>
        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret == "1") {
                form1.btnQuery.click()
            }
        }
        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";

            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);

            if (idvalue == "1") {
                form1.btnQuery.click();
            }


        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                <asp:Literal ID="lPostion" runat="server"></asp:Literal>
            </th>
        </tr>
        <tr>
            <td class="t_r">
                单位名称：
            </td>
            <td class="txt34">
                <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="100px"></asp:TextBox>
            </td>
            <td class="t_r">
                业务类型：
            </td>
            <td class="txt34">
                <asp:DropDownList ID="dmType" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td align="center" rowspan="2">
                <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                    Text="查询" />&nbsp;<input id="btnClear" class="m_btn_w2" onclick="clearPage();" type="button"
                        value="重置" />
                <asp:Button ID="btnOut" runat="server" CssClass="m_btn_w2" OnClick="btnOut_Click"
                    Text="导出" />
            </td>
        </tr>
        <tr>
            <td class="t_r">
                上报时间：
            </td>
            <td class="txt34" colspan="3">
                <asp:TextBox ID="txtFReportDate" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="65px"></asp:TextBox>
                至<asp:TextBox ID="txtFReportDate1" runat="server" CssClass="m_txt" onfocus="WdatePicker()"
                    Width="65px"></asp:TextBox>
                <asp:DropDownList ID="dbFState" runat="server" CssClass="m_txt" Width="100px" Visible="false">
                    <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                    <asp:ListItem Text="已办结" Value="6"></asp:ListItem>
                    <asp:ListItem Text="未办结" Value="1"></asp:ListItem>
                    <asp:ListItem Text="打回" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
 
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <HeaderStyle Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="工程名称" DataField="FEmpName">
                <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="工程地址">
                <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left"/>
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="建设单位"  DataField="FLeadName">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" HorizontalAlign="Left" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="业务类型">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="当前所在位置">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="当前状态"  Visible="false">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="上报时间" DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle Font-Underline="False" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" Wrap="False" />
        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" Wrap="False" />
        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" Wrap="False" />
        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" Wrap="False" />
    </asp:DataGrid>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="98%">
        <tr>
            <td align="left" style="height: 32px">
                <font color="#339933">企业名称是绿色：已经办结；</font><font color="#ff9900"> </font><font color="#000099">
                    企业名称是蓝色：未办结；</font> <font color="#ff0000">企业名称是红色：打回企业；</font>
            </td>
        </tr>
    </table>
    <table align="center" width="98%">
        <tr>
            <td>
                <uc1:pager ID="Pager1" runat="server" />
            </td>
        </tr>
    </table>
    <font color='white'>
        <%=new TimeSpan(DateTime.Now.Ticks-Context.Timestamp.Ticks).TotalSeconds %></font>
    </form>
</body>
</html>
