<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Participat.aspx.cs" Inherits="JSDW_ApplyAQJDBA_Participat" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参建单位信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function selEnt(obj, tagId) {
            //默认为选择勘察、设计、监理类企业
            var url = "../project/EntListSel.aspx";
            var qylx = "101";
            var type = document.getElementById("t_CJJS").value;
            if (type == "11220102" || type == "11220103" || type == "11220104") {
                qylx = "101";
                //施工总承包、专业承包、劳务分包使用新的选择单位窗口 modify by psq 20150319
                url = "../project/EntListSelSg.aspx";
            } else if (type == "11220105") {//勘察
                qylx = "102";
            } else if (type == "11220106")
            {
                qylx = "103";
            }
            else if (type == "11220107") {
                qylx = "104";
            } else {
                alert("请先选择参建角色");
                return;
            }
            url += "?qylx=" + qylx;
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }
        
        //施工总承包	11220102
        //设计	11220106
        //监理	11220107
        //勘察	11220105
        //专业承包	11220103
        //劳务分包	11220104
    </script>
    <base target="_self">
    </base>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                        OnClientClick="return checkInfo();" />
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
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
                <td colspan="1">
                    <asp:TextBox ID="t_QYMC" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                    <tt>*</tt>
                    <input type="hidden"  runat="server" ID="t_QYId" value="" />
                <asp:Button ID="btnAddEnt" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_QYId');"
                UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEnt_Click" Style="margin-bottom: 4px;margin-left:5px;" />
                </td>
                <td class="t_r t_bg">
                    参建角色：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_CJJS" runat="server" CssClass="m_txt" Width="202px">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    企业地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_QYDZ" runat="server" CssClass="m_txt" Width="274px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    组织机构代码：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZZJGDM" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    资质等级：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZZDJ" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    资质证书号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZZZS" runat="server" CssClass="m_txt"
                        Width="195px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    营业执照号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_YYZZH" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    企业法定代表人：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="t_QYFDDB" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_LXDH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td colspan="1">
                   <asp:TextBox ID="t_LXR" runat="server" CssClass="m_txt" onblur="isFloat(this)" Width="195px" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
                  
        </table>
    </div>
    </form>
</body>
</html>
