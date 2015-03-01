<%@ Page Language="C#" AutoEventWireup="true" CodeFile="entDEPT.aspx.cs" Inherits="Government_statis_entDEPT" %>

<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../DateSelect/WdatePicker.js" type="text/javascript"></script>

    <script src="../chart/FusionCharts.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");

            //showChart(2, "div2", "Pie3D.swf");
            //showChart("div3", "MSSpline.swf");
        });

        //调用图形
        function showChart(type, controlid, url) {
            var FStartTime = $("#txtStartTime").val();
            var FEndTime = $("#txtEndTime").val();
            escape
            chart = new FusionCharts("../Chart/" + url + "?ChartNoDataText=没有数据&InvalidXMLText=加载失败", "ChartId", getWidth(document.getElementById("t_bar")), "450", "0", "0");
            var url = location.href + "&type=" + type + "&FStartTime=" + FStartTime + "&FEndTime=" + FEndTime + "&CountWay=diy&DeptId=<%=DeptNumber %>&e=" + Math.random();

            chart.setDataURL(escape(url));
            chart.render(controlid);
        }


        $(function() {
            $("#drop_CountWay").change(function() { tab(); $("#btnCount").click(); });
            tab();
        });


        function tab() {
            $("#dr_Quarter,#dr_Month,#spanDIY").hide();
            if ($("#drop_CountWay").val() == "quarter")
                $("#dr_Quarter").show();
            else if ($("#drop_CountWay").val() == "month")
                $("#dr_Month").show();
            else if ($("#drop_CountWay").val() == "diy") {
                $("#spanDIY").show();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th>
                <asp:Literal ID="lPostion" runat="server"></asp:Literal>
            </th>
        </tr>
        <tr style="display:none">
            
            <td align="right">
                
                <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="m_btn_w2" OnClick="btnReturn_Click" />
            </td>
        </tr>
    </table>
    
    <div id="div1">
        <asp:Repeater ID="DG_List" runat="server" OnItemCommand="DG_List_ItemCommand" OnItemDataBound="DG_List_ItemDataBound">
            <HeaderTemplate>
                <table width="98%" align="center" class="m_dg1">
                    <tr class="m_dg1_h">
                        <td>
                            地区
                        </td>
                        <asp:Repeater ID="repHeader1" runat="server">
                            <ItemTemplate>
                                <td >
                                    <%# Eval("value") %>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                        <td>
                            合计
                        </td>
                    </tr>
                   
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="m_dg1_i">
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Select"
                            CommandArgument='<%# Eval("FNumber") %>' Text='<%# DataBinder.Eval(Container, "DataItem.FName") %>'></asp:LinkButton>
                    </td>
                    <asp:Repeater ID="repItem" runat="server" OnItemDataBound="repItem_ItemDataBound">
                        <ItemTemplate>
                            <td>
                                <asp:HiddenField ID="hfKey" Value='<%# Eval("key") %>' runat="server" />
                                <asp:Literal ID="liCount" runat="server"></asp:Literal>
                            </td>
                            
                        </ItemTemplate>
                    </asp:Repeater>
                    <td>
                        <asp:Literal ID="liCount" runat="server"></asp:Literal>
                    </td>
                    
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr class="m_dg1_i">
                    <td>
                        合计
                    </td>
                    <asp:Repeater ID="repItem" runat="server" OnItemDataBound="repItem_ItemDataBound">
                        <ItemTemplate>
                            <td>
                                <asp:HiddenField ID="hfKey" Value='<%# Eval("key") %>' runat="server" />
                                <asp:Literal ID="liCount" runat="server"></asp:Literal>
                            </td>
                            
                        </ItemTemplate>
                    </asp:Repeater>
                    <td>
                        <asp:Literal ID="liCount" runat="server"></asp:Literal>
                    </td>
                   
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        
        
    </div>


    <div id="div2" class="t_c" style="margin: 4px auto;">
    </div>
    <div id="div3" class="t_c" style="margin: 4px auto;">
    </div>
    </form>
</body>
</html>
