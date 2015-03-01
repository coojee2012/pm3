<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BadActionEdit.aspx.cs" Inherits="Government_AppEntAction_BadActionEdit" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业不良行为</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });
        function CheckInfo() {
            if (!getLength(document.getElementById("t_FFact"), 500, '“行为事实”')) {
                return false;
            }
            if (!getLength(document.getElementById("t_FWay"), 200, '“处罚措施”')) {
                return false;
            } 
            return true;
        }
        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')
            if (ret == "1") {
                form1.btnReload.click();
            }
        }
        function showApproveWindow2(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')
            if (ret != null && ret != "") {
                document.all.t_FBaseInfoId.value = ret;
                form1.btnShowEntName.click();
            }
        }
    </script>

    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                企业不良行为
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btnSave_Click1" />
                <asp:Button ID="btnReport" runat="server" Text="上报" CssClass="m_btn_w2" OnClick="btnReport_Click"
                    Visible="False" />
                <input id="btnRetrun" class="m_btn_w2" type="button" value="返回" onclick="if(document.all.hiddle_IsSaveOk.value!=null&&document.all.hiddle_IsSaveOk.value=='1'){window.returnValue=1;window.close();}else{window.returnValue=0;window.close();}" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                企业名称：
            </td>
            <td>
                <asp:TextBox ID="txtFBaseName" runat="server" CssClass="m_txt" Width="330px" ReadOnly="true"></asp:TextBox>&nbsp;<span
                    style="color: #ff0066"> * </span>
                <input id="Button1" class="m_btn_w2" type="button" value="选择" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程建设单位：
            </td>
            <td>
                <asp:TextBox ID="t_FBuidUnit" runat="server" CssClass="m_txt" Width="330px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td>
                <asp:TextBox ID="t_FProjectName" runat="server" CssClass="m_txt" Width="330px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程地址：
            </td>
            <td>
                <asp:TextBox ID="t_FAddress" runat="server" CssClass="m_txt" Width="330px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                行为发生地<br />
                行政区划
            </td>
            <td style="padding-left: 4px">
                <uc1:Govdept ID="Govdeptid1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                发生时间：
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FHTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                扣除分数：
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FScore" runat="server" CssClass="m_txt" onblur="isFloat(this);"
                    ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <input id="btnAdd" class="m_btn_w2" type="button" value="新增" onclick="IfSave();"
                    runat="server" /><asp:Button ID="btnDel" runat="server" Text="删除" Style="margin-left: 5px"
                        OnClick="btnDel_Click" CssClass="m_btn_w2" />
                <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="Action_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
        HorizontalAlign="Center" OnItemDataBound="Action_List_ItemDataBound" Style="margin-top: 7px"
        Width="98%">
        <HeaderStyle CssClass="m_dg1_h" />
        <ItemStyle CssClass="m_dg1_i" />
        <Columns>
            <asp:TemplateColumn>
                <ItemStyle Width="30px" />
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn HeaderText="序号">
                <ItemStyle Font-Underline="False" Width="50px" Wrap="False" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FNumber" HeaderText="行为代码">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="行为类别">
                <ItemStyle Font-Underline="False" Wrap="False" CssClass="padLeft" />
                <HeaderStyle Font-Underline="False" Wrap="False" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FACtionDesc" HeaderText="行为描述">
                <ItemStyle CssClass="padLeft" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="FScore" HeaderText="分数">
                <ItemStyle Width="30px" />
            </asp:BoundColumn>
            <asp:BoundColumn DataField="fparentid" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FACtionCodeId" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="FId" Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <table class="m_table" width="98%" align="center">
        <tr>
            <td class="t_r t_bg">
                行为事实：
            </td>
            <td class="txt34" colspan="3">
                <asp:TextBox ID="t_FFact" runat="server" CssClass="m_txt" Height="90px" TextMode="MultiLine"
                    Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                处罚决定：
            </td>
            <td class="txt34" colspan="3">
                <asp:TextBox ID="t_FWay" runat="server" CssClass="m_txt" Height="90px" TextMode="MultiLine"
                    Width="98%" Style="word-break: break-all; word-wrap: break-word; text-align: left"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                处罚机构：
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FDeptIdName" runat="server" CssClass="m_txt" Width="180px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                填报单位：
            </td>
            <td class="txt34">
                <asp:TextBox ID="FWDeptiId" runat="server" CssClass="m_txt" ReadOnly="True" Width="180px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                处罚日期：
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FDTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                发布开始日期：
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FBeginTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                发布截止日期：
            </td>
            <td class="txt34">
                <asp:TextBox ID="t_FEndTime" runat="server" CssClass="m_txt" onfocus="WdatePicker()"></asp:TextBox>
            </td>
            <td class="tdCenter" id="TD1" runat="server">
            </td>
            <td class="txt34" id="TD2" runat="server">
            </td>
        </tr>
    </table>
    <asp:Button ID="btnShowEntName" runat="server" Text="Button" Style="display: none"
        OnClick="btnShowEntName_Click" />
    <input id="t_FRegionId" type="hidden" runat="server" />
    <input id="hiddle_IsSaveOk" runat="server" type="hidden" value="0" />
    <input id="t_FBaseInfoId" type="hidden" runat="server" />&nbsp;
    <input id="HFId" type="hidden" runat="server" />
    </form>
</body>
</html>

<script>
    function IfSave() {

        if (document.getElementById("HFId").value == "") {
            alert("请先保存信息");
            return false;
        }
        showApproveWindow1('ActionList.aspx?fid=' + document.getElementById("HFId").value + '&fbid=' + document.getElementById("t_FBaseInfoId").value, 900, 700);
    }
</script>

