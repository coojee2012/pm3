<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AcceptJieJian.aspx.cs" Inherits="JSDW_ApplyXZYJS_AuditMain_AcceptJieJian" %>

<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
     <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        function ChooseFile(typeId,YJS_ID,typeName)
        {
            var url = "file.aspx?typeId=" + typeId + "&YJS_ID=" + YJS_ID + "&typeName=" + typeName;
            showAddWindow(url, 500, 200);
        }
        function Save()
        {
            var items = $("#CLQQ").find(".clDetail");
            var array = new Array();
            $.each(items, function (index, item) {
                var arrayTD = $(item).find("td");
                var fileId = $(arrayTD[0]).attr("value");
                var isHave = $(arrayTD[3]).find("input[type=checkbox]").eq(0).attr("checked") == true ? 1 : 0;
                var reMark = $(arrayTD[5]).find("input[type=text]").eq(0).val();
                array.push(fileId + "-" + isHave + "-" + reMark);
            });
           
            $("#hfFile").val(array.join('|'));
            return true;
        }
        function FindUpStuff() {
            var YWBM = $("#hfFLinkId").val();
            var Id = $("#hfId").val();
            var url = "";
            if ($("#hfProjectType").val() == "1")//房建
                url = "../../JSDW/ApplyXZYJS/AppMain/index.aspx?YJS_GUID=" + Id + "&fAppId=" + YWBM + "&audit=1";
            else//市政
                url = "../../JSDW/ApplyXZYJSSZ/AppMain/index.aspx?YJS_GUID=" + Id + "&fAppId=" + YWBM + "&audit=1";
            showAddWindow(url, 1000, 500);
        }
        function BackApp(url) {
            var tmpVal = $("#hfFLinkId").val();
            var fsubid = $("#hfFlowId").val();
            showAddWindow(url + '?FLinkId=' + tmpVal + '&fSubFlowId=' + fsubid, 700, 600);
            return true;
        }
        $(function () {
            $("#allFile").click(function () {
                $("#CLQQ").find("input[type=checkbox]").attr("checked", $(this).attr("checked"));
            });
        });
    </script>

    <%--<script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>--%>

    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
       <asp:HiddenField ID="hfFLinkId" runat="server" />
        <asp:HiddenField ID="hfFlowId" runat="server" />
        <asp:HiddenField ID="hfXMBM" runat="server" />
        <asp:HiddenField ID="hfFile" runat="server" />
        <asp:HiddenField ID="hfFunction" runat="server" />
        <asp:HiddenField ID="hfProcessRecordFID" runat="server" />
        <asp:HiddenField ID="hfProcessInstanceID" runat="server" />
        <asp:HiddenField ID="hfId" runat="server" />
        <asp:HiddenField ID="hfProjectType" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">选址接件
            </th>
        </tr>
        <tr>
            <td class="t_r t_bg">项目名称</td>
            <td><asp:TextBox ID="txtXMMC" runat="server" Enabled=false Width="280"></asp:TextBox></td>
            <td class="t_r t_bg">项目编号</td>
            <td><asp:TextBox ID="txtBH" runat="server" Enabled=false Width="300"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">建设单位</td>
            <td colspan="3"><asp:TextBox ID="txtJSDWMC" runat="server" Enabled=false Width="280"></asp:TextBox></td>
        </tr>
        <tr>
             <td class="t_r t_bg">建设地址</td>
            <td colspan="3"><asp:TextBox ID="txtJSDWDZ" runat="server" Enabled=false Width="280"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="t_r t_bg">联系人</td>
            <td><asp:TextBox ID="txtLXR" Enabled=false runat="server" Width="280"></asp:TextBox></td>
            <td class="t_r t_bg">联系电话</td>
            <td><asp:TextBox ID="txtLXDH" Enabled=false runat="server" Width="300"></asp:TextBox></td>
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
                <td class="t_r t_bg" style="width: 30px;text-align: center;">份数</td>
                <td class="t_r t_bg" style="width:80px;text-align: center;"><input type="checkbox" id="allFile" />是否具备</td>
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
                            <asp:BoundColumn DataField="FAppPerson" HeaderText="审查人">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FCompany" HeaderText="审查机构">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FFunction" HeaderText="审查人职务">
                                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="70px" CssClass="padLeft" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAppTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="审查日期">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="90px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FResult" HeaderText="审查结果">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="70px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FIdea" HeaderText="审查意见">
                                <ItemStyle Font-Underline="False" Wrap="False" Width="150px" HorizontalAlign="Left" />
                                <HeaderStyle Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FRoleDesc" HeaderText="审查环节">
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
                <th colspan="6" style="text-align: left;">接件意见
                </th>
            </tr>
            <tr>
                <td class="t_r">接件人</td>
                <td><asp:TextBox ID="t_FAppPerson" runat="server" CssClass="m_txt"></asp:TextBox></td>
                <td class="t_r">接件人职务</td>
                <td><asp:TextBox ID="txtFunction" runat="server" CssClass="m_txt"></asp:TextBox></td>
                <td class="t_r">接件单位
                </td>
                <td>
                    <asp:TextBox ID="t_FAppPersonUnit" runat="server" CssClass="m_txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">是否受理
                </td>
                <td>
                    <asp:DropDownList ID="dResult" runat="server" CssClass="m_txt">
                        <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不同意" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t_r">接件时间
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FAppDate" runat="server" CssClass="m_txt" onblur="isDate(this);"
                        onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r">接件意见
                </td>
                <td colspan="5">
                    <asp:TextBox ID="t_FAppIdea" runat="server" CssClass="m_txt" Height="99px" TextMode="MultiLine"
                        Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin-top: 2PX">
            <asp:Button ID="btnSaveYJ" runat="server" CssClass="m_btn_w2" Text="保存" OnClientClick="return Save();" OnClick="btnSaveYJ_Click" />
            &nbsp;&nbsp;<asp:Button ID="btnAccept" runat="server"  class="m_btn_w4" Text="接件" OnClick="btnAccept_Click" OnClientClick="return Save();" />
            &nbsp;&nbsp;<asp:Button ID="btnNoAccept" runat="server" class="m_btn_w4" 
                Text="不予受理" onclick="btnNoAccept_Click" />
            <%--<input class="m_btn_w4" onclick="BackApp('BackAccept.aspx')" type="button" value="打回企业" />--%>
            &nbsp;&nbsp;<asp:Button ID="btnExit" runat="server" Text="打回企业"  OnClientClick="return confirm('确认打回？')"  class="m_btn_w4" OnClick="btnExit_Click" />
             &nbsp;&nbsp;<asp:Button ID="btnYes" runat="server" Text="受理通知书打印" CssClass="m_btn_w8" OnClientClick='javascript:document.getElementById("SLDY").click();' />
             <asp:Literal ID="ltrTZSText" runat="server"></asp:Literal>
             
              &nbsp;&nbsp;<asp:Button ID="btnNoYes" runat="server" Text="不受理通知书打印" CssClass="m_btn_w8" OnClientClick='javascript:document.getElementById("NOSLDY").click();' />
            <%--  <a href="http://www.baidu.com" target="_blank" id="NOSLDY" style="display:none"></a>--%>
             
            &nbsp;&nbsp;<input id="btnReturn" type="button" class="m_btn_w2" value="返回" onclick="window.returnValue='1';window.close();" />
        </div>
        </div>
    </form>
</body>
</html>
