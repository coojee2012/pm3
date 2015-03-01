<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dReview.aspx.cs" Inherits="Government_expert_dReview" %>

<!DOCTYPE html>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";

            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);

            if (idvalue == "1") {
                form1.btnQuery.click();
            }
        }
        function app(url) {
            var tmpVal = ''; var psid = '';var isend='';
            //$(":checkbox[id$=CheckItem]").each(function () {
            //    if ($(this).attr("checked")) {
            //        var id = $("#span" + $(this).attr("id")).attr("name");
            //        if (tmpVal.indexOf(id + ",") == -1) {
            //            tmpVal += id + ",";
            //        }
            //    }
            //});
            var cou = 0;
            var chkColl = document.getElementsByTagName("input");
            for (var i = 0; i < chkColl.length; i++) {
                if (chkColl[i].type == "checkbox" && chkColl[i].id.indexOf("CheckItem") > -1) {
                    if (!chkColl[i].disabled && chkColl[i].checked == true) {
                        cou += 1;
                        var span = document.getElementById("span" + chkColl[i].id)
                        if (span) {
                            if (tmpVal.indexOf(span.getAttribute("name") + ",") == -1) {
                                tmpVal += span.getAttribute("name") + ",";
                            }
                            if (psid.indexOf(span.getAttribute("psid") + ",") == -1) {
                                psid += span.getAttribute("psid") + ",";
                            }
                            if (psid.indexOf(span.getAttribute("isend") + ",") == -1) {
                                psid += span.getAttribute("isend") + ",";
                            }
                        }
                    }
                }
            }
            var obj = new Object();
            if (tmpVal.length > 1) {
                tmpVal = tmpVal.substring(0, tmpVal.length - 1);
            }

            else {
                alert("请选择一条工法信息审核！");
                return false;
            }
            if (psid.length > 1) {
                psid = psid.substring(0, psid.length - 1);
            }
            if (cou > 1 || cou <= 0) {
                alert("只能选择一条工法信息审核！");
                return false;
            }
            obj.name = '';
            obj.id = tmpVal;
            ShowWindow(url + '?FLinkId=' + tmpVal + '&psid=' + psid+'&isend='+isend, 700, 800, obj);
            return false

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">工法待接件</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">企业名称：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">工法名称：
                </td>
                <td>
                    <asp:TextBox ID="txtFPrjName" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>

                <td colspan="2" rowspan="2" style="text-align: center; padding-right: 10px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                        Text="查询" />
                    &nbsp;
                <input id="btnClear" class="m_btn_w2" style="margin-top: 3px;" type="button" value="重置"
                    onclick="clearPage();" />
                </td>
            </tr>
            <tr>
                <td class="t_r">类别：
                </td>
                <td>
                    <asp:DropDownList ID="t_FListName" Width="120px" CssClass="m_txt" runat="server" OnSelectedIndexChanged="t_FListName_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="">--请选择--</asp:ListItem>
                        <asp:ListItem Value="房屋建筑工程">房屋建筑工程</asp:ListItem>
                        <asp:ListItem Value="土木工程">土木工程</asp:ListItem>
                        <asp:ListItem Value="工业安装工程">工业安装工程</asp:ListItem>
                        <asp:ListItem Value="其他">其他</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">专业分类：
                </td>
                <td>
                    <asp:DropDownList ID="t_FTypeName" Width="120px" CssClass="m_txt" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="t_FTypeName1" CssClass="m_txt" Visible="false" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r">
                    <asp:Button ID="btnAccept" runat="server" CssClass="m_btn_w2" Text="评审" OnClientClick="return app('reviewDetail.aspx')" />
                    &nbsp;<asp:Button ID="btnOut" runat="server" Style="margin-left: 5px;" CssClass="m_btn_w2"
                        OnClick="btnOut_Click" Text="导出" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%" OnItemCommand="JustAppInfo_List_ItemCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <ItemStyle Width="20px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="企业名称" DataField="FName" HeaderStyle-Width="150px">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="工法名称" HeaderStyle-Width="150px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnQY" runat="server" CommandName="See" Text='<%#Eval("GFMC") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="类别" HeaderStyle-Width="100px" DataField="FListName">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="专业分类" DataField="FTypeName" HeaderStyle-Width="100px">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="专家意见" HeaderStyle-Width="200px" DataField="Fresult">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="上报时间" HeaderStyle-Width="100px" DataField="FReportDate" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="公示开始时间" HeaderStyle-Width="100px" DataField="stime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False " HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="公示结束时间" HeaderStyle-Width="100px" DataField="etime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="批次" HeaderStyle-Width="100px" DataField="FBatchNoId">
                    <ItemStyle Font-Underline="False" Wrap="False" HorizontalAlign="Left" />
                    <HeaderStyle Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="isEnd" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="PsID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="Fappid" Visible="False"></asp:BoundColumn>
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
        <div class="d div1 tcen" style="width: 98%; margin: 0px auto;">
            <uc1:pager ID="Pager1" runat="server"></uc1:pager>
        </div>
    </form>
</body>
</html>
