﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyEdit.aspx.cs" Inherits="JSDW_ApplyJGYS_CommpanyEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        #form1 label.errorMsg 
			{ 
			color:Red; 
			font-size:13px; 
			margin-left:5px;
			}
        .dispaly {display:none;
        }
    </style>
     <script type="text/javascript" src="../../script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../script/messages_zh.js"></script>
    <script type="text/javascript">
        function SaveValidate() {
            var success = $("#form1").valid();
            if (success) {
                var ZZX = $("#txtZZX").val();
                var JGDM = $("#txtZZJGDM").val();
                if ($.trim(ZZX).length == 0) {
                    alert("资质不能为空");
                    return false;
                } else if ($.trim(JGDM).length == 0) {
                    alert("组织机构代码不能为空");
                    return false;
                }
                return true;
            }
            else
                return false;
        }
        $(function () {
            $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
            $("#btnChoosePerson").click(function () {
                var companyId = $("#hfCompanyId").val();
                if ($.trim(companyId).length < 1) {
                    alert("请先保存信息");
                    return false;
                }
                var url = "PerSonList.aspx";
                var type = $("#hfTypeId").val();
                if (type == "6")//监理单位
                    url = "PerSonJLList.aspx";
                var result = showWinByReturn("" + url + "?CompanyId=" + $("#hfCompanyId").val() + "&QYBM=" + $("#hfQYBM").val() + "&typeId=" + type, 800, 500);
                if (result != undefined) {
                    $("#hfPerSonId").val(result);
                    return true;
                }
                return false;
            });
            $("#btnAddPerson").click(function () {
                var companyId = $("#hfCompanyId").val();
                if ($.trim(companyId).length < 1) {
                    alert("请先保存信息");
                    return false;
                }
                var url = "PerSonAdd.aspx";
                var type = $("#hfTypeId").val();
                if (type == "6")//监理单位
                    url = "PerSonJLAdd.aspx";
                var result = showWinByReturn("" + url + "?CompanyId=" + $("#hfCompanyId").val() + "&JG_Id=" + $("#hfId").val() + "&QYBM=" + $("#hfQYBM").val() + "&typeId=" + type, 800, 500);
                if (result != undefined) {
                    $("#hfCompanyId").val(result);
                    return true;
                }
                return false;
            });
        });
        function SearchPerson(obj, perSonId) {
            var url = "PerSonAdd.aspx";
            var type = $("#hfTypeId").val();
            if (type == "6")//监理单位
                url = "PerSonJLAdd.aspx";
            showAddWindow("" + url + "?isShow=1&perSonId=" + perSonId + "&typeId=" + type, 800, 500);
        }
        function Edit(Id) {
            var url = "PerSonAdd.aspx";
            var type = $("#hfTypeId").val();
            if (type == "6")//监理单位
                url = "PerSonJLAdd.aspx";
            var result = showWinByReturn("" + url + "?Id=" + Id + "&typeId=" + type, 800, 500);
            if (result != undefined) {
                //$("#btnRefresh").click();
                document.getElementById("btnRefresh").click();
                return true;
            }
            return false;
        }
        function Show(Id) {
            var url = "PerSonAdd.aspx";
            var type = $("#hfTypeId").val();
            if (type == "6")//监理单位
                url = "PerSonJLAdd.aspx";
            showAddWindow("" + url + "?Id=" + Id + "&IsShow=1" + "&typeId=" + type, 800, 500);
        }
    </script>

    <%--<script language="vbScript">Function  ToHex(str)  ToHex= Hex(str)  End function</script>--%>

    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfPerSonId" runat="server" />
    <asp:HiddenField ID="hfCompanyId" runat="server" />
    <asp:HiddenField ID="hfId" runat="server" />
    <asp:HiddenField ID="hfQYBM" runat="server" />
    <asp:HiddenField ID="hfTypeId" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
               <asp:Literal ID="ltrText" runat="server"></asp:Literal>
            </th>
        </tr>
    </table>
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" OnClick="btnSave_Click" OnClientClick="return SaveValidate()" />&nbsp;&nbsp;
                    <input type="button" value="返回" class="m_btn_w2" onclick="javascript: window.returnValue = '1'; window.close()" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    单位名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDWMC" runat="server" Enabled="false" CssClass="m_txt required" Width="500px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    单位地址：
                </td>
                <td colspan="3">
                     <asp:TextBox ID="txtDWDZ" runat="server" CssClass="m_txt required" Width="500px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    法定代表人：
                </td>
                <td>
                    <asp:TextBox ID="txtFDDBR" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    联系人：
                </td>
                <td>
                    <asp:TextBox ID="txtLXR" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
             <tr>
                <td class="t_r t_bg">
                    移动电话：
                </td>
                <td>
                    <asp:TextBox ID="txtYDDH" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                 <td class="t_r t_bg">
                    联系电话：
                </td>
                <td>
                    <asp:TextBox ID="txtLXDH" runat="server" CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>

             <tr>
                <td class="t_r t_bg">
                    <asp:Literal ID="ltrZZ" runat="server" Text="主项资质："></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtZZX" Enabled="false" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtZZJGDM" Enabled="false" runat="server" CssClass="m_txt" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr runat="server" id="zxzz">
                <td class="t_r t_bg">
                    增项资质：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtZXZZ" runat="server" Enabled="false" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtBZ" runat="server" CssClass="m_txt" Width="500" Height="60" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_table">
            <tr>
                <td width="100%" align="right">
                    <asp:Button ID="btnRefresh" runat="server" CssClass="dispaly" Text="刷 新" OnClick="btnRefresh_Click" />
                     点击<asp:Button ID="btnChoosePerson" runat="server" Text="选择" CssClass="m_btn_w2" OnClick="btnChoosePerson_Click" /> 按钮，从人员库中选择人员；如果人员库中无该人员，则点击<asp:Button ID="btnAddPerson" runat="server" CssClass="m_btn_w2" Text="添加" OnClick="btnAddPerson_Click" />按钮，添加该人员
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DG_List" runat="server" HorizontalAlign="Center" Width="98%" CssClass="m_dg1"
            AutoGenerateColumns="False" OnItemDataBound="DG_List_ItemDataBound" OnItemCommand="DG_List_ItemCommand">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
                <Columns>
                    <asp:BoundColumn HeaderText="序号">
                        <ItemStyle Width="50px" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                   <%-- <asp:TemplateColumn HeaderText="人员名称">
                        <ItemTemplate>
                            <a href="javascript:void(0)" onclick="SearchPerson(this,'<%#Eval("ID") %>')"><%#Eval("PerSonName") %></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
                    <asp:BoundColumn HeaderText="人员姓名" DataField="PerSonName">
                        <ItemStyle CssClass="t_l" Wrap="false" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="人员类型" DataField="RYLXMC">
                        <ItemStyle CssClass="t_l" Wrap="false" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="发证日期" DataField="ZCRQ" DataFormatString="{0:yyyy-MM-dd}">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="注册编号" DataField="ZCBH" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDel" runat="server" Text="删 除" OnClientClick="return confirm('确认删除?')" CommandName="DEL"></asp:LinkButton>&nbsp;&nbsp;
                            <a href="#" onclick="Edit('<%#Eval("ID") %>')">编 辑</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </div>
        <div style="height:50px;"></div>
    </form>
</body>
</html>


