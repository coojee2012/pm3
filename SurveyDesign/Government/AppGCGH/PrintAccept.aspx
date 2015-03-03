<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintAccept.aspx.cs" Inherits="Government_AppGCGH_PrintAccept" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
     <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        function FindUpStuff() {
            var YWBM = $("#hfFLinkId").val();
            var Id = $("#hfId").val();
            var url = "";
            if ($("#hfProjectType").val() == "1")//房建
                url = "../../JSDW/ApplyGCGH/AppMain/index.aspx?GC_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            else//市政
                url = "../../JSDW/ApplyGCGHSZ/AppMain/index.aspx?GC_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
            showAddWindow(url, 1000, 500);
        }
        $(function () {
            $("#CLQQ").find("input[type=checkbox]").attr("disabled", "false");
            $("#CLQQ").find("input[type=text]").attr("disabled", "false");
        });
    </script>

    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
       <asp:HiddenField ID="hfFLinkId" runat="server" />
        <asp:HiddenField ID="hfId" runat="server" />
        <asp:HiddenField ID="hfProjectType" runat="server" />
    <table width="98%" align="center" class="m_title" id="m_title">
        <tr>
            <th colspan="5">工程规划办理
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">项目名称</td>
            <td><asp:TextBox ID="txtXMMC" runat="server" Enabled="false" Width="150"></asp:TextBox></td>
            <td class="t_r t_bg">项目编号</td>
            <td><asp:TextBox ID="txtBH" runat="server" Enabled="false" Width="150"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">建设单位</td>
            <td colspan="3"><asp:TextBox ID="txtJSDWMC" runat="server" Enabled="false" Width="300"></asp:TextBox></td>
        </tr>
        <tr>
             <td class="t_r t_bg">建设地址</td>
            <td colspan="3"><asp:TextBox ID="txtJSDWDZ" runat="server" Enabled="false" Width="300"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">联系人</td>
            <td><asp:TextBox ID="txtLXR" runat="server" Enabled="false"></asp:TextBox></td>
            <td class="t_r t_bg">联系电话</td>
            <td><asp:TextBox ID="txtLXDH" runat="server" Enabled="false"></asp:TextBox></td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_title">
        <tr>
            <td>
                <input type="button" value="【查看上报材料】" onclick="FindUpStuff()" class="m_btn_w12" />
            </td>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" id="CLQQ" class="m_title">
            <tr>
                <th colspan="6">材料情况
                </th>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 30px; text-align: center;">序号</td>
                <td class="t_r t_bg" style="width: 200px; text-align: center;">材料名称</td>
                <td class="t_r t_bg" style="width: 30px;text-align: center;">文件数量</td>
                <td class="t_r t_bg" style="width:50px;text-align: center;"><input type="checkbox" id="allFile" />是否具备</td>
                <td class="t_r t_bg" style="width:50px;text-align: center;">电子件</td>
                <td class="t_r t_bg" style="width:auto;text-align: center;">备注</td>
            </tr>
            <asp:Literal ID="ltrText" runat="server"></asp:Literal>
        </table>
         <table width="100%" align="center" class="m_title">
            <tr>
                <th colspan="4">各级审批意见：
                </th>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DG_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                        HorizontalAlign="Center" OnItemDataBound="DG_List_ItemDataBound" Style="margin-top: 7px"
                        Width="100%">
                        <HeaderStyle CssClass="m_dg1_h" />
                        <ItemStyle CssClass="m_dg1_i" />
                        <Columns>
                            <asp:BoundColumn HeaderText="序号">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="30px" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FRoleDesc" HeaderText="审批岗位">
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="70px" CssClass="padLeft" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppPerson" HeaderText="审批人">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FCompany" HeaderText="审批人单位">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="审批日期">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="90px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FResult" HeaderText="审批结果">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FIdea" HeaderText="审批意见">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
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
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="4" style="text-align: left;">审核意见
                </th>
            </tr>
            <tr>
                <td class="t_r"><asp:Literal ID="ltrPerSon" Text="接件人" runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPerson" Enabled="false" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
                <td class="t_r"><asp:Literal ID="ltrUnit" Text="接件单位" runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPersonUnit" Enabled="false" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">是否受理
                </td>
                <td>
                    <asp:DropDownList ID="dResult" runat="server" Enabled="false" CssClass="m_txt">
                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r"><asp:Literal ID="ltrTime" Text="接件时间" runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="t_FAppDate" runat="server" Enabled="false" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r"><asp:Literal ID="ltrComment" Text="接件意见" runat="server"></asp:Literal>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FAppIdea" runat="server" Enabled="false" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                        Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin-top: 2PX">
            &nbsp;&nbsp;<asp:Button ID="btnAccept" runat="server" Text="受理通知书打印" CssClass="m_btn_w8" OnClientClick='javascript:document.getElementById("SLDY").click();' />
             <asp:Literal ID="ltrTZSText" runat="server"></asp:Literal>
          &nbsp;&nbsp;<asp:Button ID="btnNoAccept" runat="server" Text="不受理通知书打印" CssClass="m_btn_w8" OnClientClick='javascript:document.getElementById("NOSLDY").click();' />

            <input id="btnReturn" type="button" class="m_btn_w2" value="返回" onclick="window.close();" />
        </div>
        </div>
    </form>
</body>
</html>

