<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGTSCXXForm.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_SGTSCXXForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.1 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业用户维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        .display {
        display:none;}
    </style>
     <script type="text/javascript" src="../../../script/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../../script/default.js"></script>
    <script type="text/javascript" src="../../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript" src="../../../script/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../../script/messages_zh.js"></script>

    <base target="_self" />
</head>
<body id="body1">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfId" runat="server" />
    <asp:HiddenField ID="hfPerSonId" runat="server" />
    <asp:HiddenField ID="hfSrouce" runat="server" /><!--来源 True标准库 false新增-->
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                施工图审查信息
            </th>
        </tr>
    </table>
    <div>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保 存" OnClientClick="return SaveValidate()" OnClick="btnSave_Click"/>
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    办理选项：
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlIsTrans" runat="server" Width="200">
                        <asp:ListItem Value="1">补填</asp:ListItem>
                        <asp:ListItem Value="2">不需要办理</asp:ListItem>
                        <asp:ListItem Value="3">以后补办</asp:ListItem>
                        <asp:ListItem Value="4">已办</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="LY">
                <td class="t_r t_bg">
                    理由：
                </td>
                <td colspan="3"><asp:TextBox ID="txtLY" runat="server" CssClass="m_txt" Width="500" Height="40" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    施工图审查合格书编号：
                </td>
                <td>
                    <asp:TextBox ID="txtSGTSCHGSBH" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    项目编号：
                </td>
                <td>
                    <asp:TextBox ID="txtXMBH" runat="server"  CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    施工图审查机构名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfSGTId" runat="server" />
                    <asp:TextBox ID="txtSGTSCJGMC" runat="server" Enabled="false" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <%--<input type="button" class="m_btn_w2" onclick="return SearchSGT()" value="选 择" />--%>
                    <asp:Button ID="btnSearchSGT" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchSGT()" Text="选 择" onclick="btnSearchSGT_Click" /><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    施工图审查机构组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtSGTSCJGZZJGDM" Enabled="false" runat="server"  CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    审查完成日期：
                </td>
                <td>
                    <asp:TextBox ID="txtSCWCRQ" runat="server" CssClass="m_txt Wdate required" onfocus="WdatePicker({skin:'whyGreen' })" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td>
                    <asp:TextBox ID="txtJSGM" runat="server"   CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    勘察单位名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfKCDWId" runat="server" />
                    <asp:TextBox ID="txtKCDWMC" runat="server" Enabled="false"  CssClass="m_txt" Width="200"></asp:TextBox>
                    <%--<input type="button" class="m_btn_w2" onclick="return SearchKCDW()" value="选 择" />--%>
                    <asp:Button ID="btnKC" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchKCDW()" Text="选 择" onclick="btnKC_Click" /><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    勘察单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtKCDWZZJGDM" Enabled="false" runat="server"  CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    设计单位名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfSJDWId" runat="server" />
                    <asp:TextBox ID="txtSJDWMC" runat="server" Enabled="false" CssClass="m_txt required" Width="200"></asp:TextBox>
                    <%--<input type="button" class="m_btn_w2" onclick="return SearchSJDW()" value="选 择" />--%>
                    <asp:Button ID="btnSJDW" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchSJDW()" Text="选 择" onclick="btnSJDW_Click" /><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    设计单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtSJDWZZJGDM" Enabled="false" runat="server"  CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    一次审查是否通过：
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSFTG">
                        <asp:ListItem Value="1">通过</asp:ListItem>
                        <asp:ListItem Value="0">不通过</asp:ListItem>
                    </asp:DropDownList><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    一次审查时违反强条数：
                </td>
                <td>
                    <asp:TextBox ID="txtYCSCWFQTS" runat="server" CssClass="m_txt required number" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    一次审查时违反的强条条目：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtYCSCWFNum" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_table">
            <tr>
                <td width="100%" align="right">
                     <asp:Button ID="btnRefreash" runat="server" CssClass="display" Text="刷 新" OnClick="btnRefreash_Click" />
                     点击<asp:Button ID="btnChoosePerson" runat="server" Text="选 择" OnClientClick="return SearchPerson()" CssClass="m_btn_w2 cancel" OnClick="btnChoosePerson_Click" /> 按钮，从人员库中选择人员<%--；如果人员库中无该人员，则点击<asp:Button ID="btnAddPerson" runat="server" CssClass="m_btn_w2" Text="添加"  />按钮，添加该人员--%>
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
                    <%--<asp:TemplateColumn HeaderText="所属单位名称">
                        <ItemTemplate>
                            <a href="javascript:void(0)" onclick="SearchPerson(this,'<%#Eval("ID") %>')"><%#Eval("SSDWMC") %></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
                    <asp:BoundColumn HeaderText="所属单位名称" DataField="SSDWMC">
                        <ItemStyle CssClass="t_l" Wrap="false" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="所属单位组织机构代码" DataField="SSDWZZJGDM">
                        <ItemStyle CssClass="t_l" Wrap="false" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="人员姓名" DataField="RYXM">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="证件类型" DataField="ZJLXMC" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ZJHM" HeaderText="证件号码"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ZCLXDJMC" HeaderText="注册类型及等级"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CDJS" HeaderText="承担角色"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblDel" runat="server" Text="删 除" OnClientClick="return confirm('确认删除?')" CommandName="del"></asp:LinkButton>
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
<script type="text/javascript">
    $(function () {
        $("#form1").validate({ onfocusout: function (element) { $(element).valid(); } });
        if ($("#hfSrouce").val() == "True") {//来源标准库
            $("#ddlIsTrans").val('4');
            $("#ddlIsTrans").attr("disabled", "disabled");
            $("#LY").hide();
        } else {
            $("#ddlIsTrans").children().eq(3).remove();
            var isTrans = $("#ddlIsTrans").val();
            if (isTrans == "1") {
                $("#txtLY").val('');
                $("#txtLY").attr("disabled", "disabled");
                $("#LY").hide();
            } else {
                $("#txtLY").removeAttr("disabled");
                $("#LY").show();
            }
            $("#ddlIsTrans").change(function () {
                var isTrans = $(this).val();
                if (isTrans == "1") {
                    $("#txtLY").val('');
                    $("#txtLY").attr("disabled", "disabled");
                    $("#LY").hide();
                } else {
                    $("#txtLY").removeAttr("disabled");
                    $("#LY").show();
                }
            });
        }
    });
    function SaveValidate() {
        var isTrans = $("#ddlIsTrans").val();
        var LY = $("#txtLY").val();
        if (isTrans != "1") {
            if ($.trim(LY).length == 0) {
                alert("请输入理由");
                return false;
            }
            var items = $("#form1").find("input[type=text]");
            items.each(function (index, element) {
                $(element).removeClass("required");
            });
            var ddlItems = $("#form1").find("input[type=text]");
            ddlItems.each(function (index, element) {
                $(element).removeClass("required");
            });
            return true;
        }
        var success = $("#form1").valid();
        if (success)
            return true;
        else
            return false;
    }
    function SearchSGT() {
        var result = showWinByReturn("ChooseQY.aspx?typeId=7", 800, 500);
        if (result != undefined) {
            var str = result.split('|');
            $("#hfSGTId").val(str[0]);
//            $("#txtSGTSCJGMC").val(str[1]);
            //            $("#txtSGTSCJGZZJGDM").val(str[2]);
            return true;
        }
        return false;
    }
    function SearchKCDW() {
        var result = showWinByReturn("ChooseQY.aspx?typeId=4", 800, 500);
        if (result != undefined) {
            var str = result.split('|');
            $("#hfKCDWId").val(str[0]);
//            $("#txtKCDWMC").val(str[1]);
            //            $("#txtKCDWZZJGDM").val(str[2]);
            return true;
        }
        return false; 
    }
    function SearchSJDW() {
        var result = showWinByReturn("ChooseQY.aspx?typeId=5", 800, 500);
        if (result != undefined) {
            var str = result.split('|');
            $("#hfSJDWId").val(str[0]);
//            $("#txtSJDWMC").val(str[1]);
            //            $("#txtSJDWZZJGDM").val(str[2]);
            return true;
        }
        return false;
    }
    function SearchPerson() {
        if ($.trim($("#hfId").val()).length > 0) {
            var result = showWinByReturn("ChooseSGTPerson.aspx?QYBM=" + $("#hfSGTId").val() + "&SGTId=" + $("#hfId").val(), 800, 500);
            if (result != undefined) {
                $("#hfPerSonId").val(result);
                return true;
            }
        } else
            alert("请先保存信息");
        return false;
    }
    function Edit(Id) {
        showAddWindow("PerSonAdd.aspx?ID=" + Id, 800, 500);
        document.getElementById("btnRefreash").click();
    }
    function Show(Id) {
        showAddWindow("PerSonAdd.aspx?ID=" + Id+"&IsShow=1", 800, 500);
        document.getElementById("btnRefreash").click();
    }
</script>

