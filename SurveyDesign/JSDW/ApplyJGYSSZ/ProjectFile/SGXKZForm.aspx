<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGXKZForm.aspx.cs" Inherits="JSDW_ApplyJGYS_ProjectFile_SGXKZForm" %>
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
         .m_btn_w2 {
             height: 21px;
         }
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
    <asp:HiddenField ID="hfXMBH" runat="server" />
    <asp:HiddenField ID="hfBH" runat="server" />
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                施工许可证
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
                    &nbsp;&nbsp;
                    <input type="button" value="返 回" class="m_btn_w2" onclick="javascript:window.close()" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    施工许可证编号：
                </td>
                <td>
                    <asp:TextBox ID="txtSGXKZBH" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    发证日期：
                </td>
                <td>
                    <asp:TextBox ID="txtFZRQ" runat="server" CssClass="m_txt Wdate" onfocus="WdatePicker({skin:'whyGreen' })" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程名称：
                </td>
                <td>
                    <asp:TextBox ID="txtGCMC" runat="server" Enabled="false" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    合同金额(万元)：
                </td>
                <td>
                    <asp:TextBox ID="txtHTJE" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    面积（m2）：
                </td>
                <td>
                    <asp:TextBox ID="txtMJ" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td>
                    <asp:TextBox ID="txtJSGM" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    勘察单位名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfKCDWBM" runat="server" />
                    <asp:TextBox ID="txtKCDWMC" runat="server" Enabled="false"  CssClass="m_txt" Width="200"></asp:TextBox>
                    <asp:Button ID="btnKCDW" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchKCDW()" Text="选 择" onclick="btnKCDW_Click" /><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    勘察单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtKCDWZZJGDM" Enabled="false" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                   
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    设计单位名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfSJDWBM" runat="server" />
                    <asp:TextBox ID="txtSJDWMC" runat="server" Enabled="false" CssClass="m_txt" Width="200"></asp:TextBox>
                    <asp:Button ID="btnSJDW" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchSJDW()" Text="选 择" onclick="btnSJDW_Click" /><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    设计单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtSJDWZZJGDM" Enabled="false" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    施工单位名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfSGDWBM" runat="server" />
                    <asp:TextBox ID="txtSGDWMC" runat="server" Enabled="false" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <asp:Button ID="btnSGDW" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchSGDW()" Text="选 择" onclick="btnSGDW_Click" /><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    施工单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtSGDWZZJGDM" runat="server" Enabled="false"  CssClass="m_txt" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                 <td class="t_r t_bg">
                    监理单位名称：
                </td>
                <td>
                    <asp:HiddenField ID="hfJLDWBM" runat="server" />
                    <asp:TextBox ID="txtJLDWMC" runat="server" Enabled="false" CssClass="m_txt" Width="200"></asp:TextBox>
                    <asp:Button ID="btnJLDW" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchJLDW()" Text="选 择" onclick="btnJLDW_Click" /><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    监理单位组织机构代码：
                </td>
                <td>
                    <asp:TextBox ID="txtJLDWZZJGDM" runat="server" Enabled="false" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    施工单位安全生产许可证编号：
                </td>
                <td>
                    <asp:TextBox ID="txtSGDWAQSCXKZ" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    项目经理姓名：
                </td>
                <td>
                    <asp:HiddenField ID="hfXMJLRYBH" runat="server" />
                    <asp:TextBox ID="txtXMJLXM" Enabled="false" runat="server"  CssClass="m_txt" Width="200"></asp:TextBox>
                    <asp:Button ID="btnXMJL" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchPerson()" Text="选择" onclick="btnXMJL_Click" /><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目经理证件类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlXMJLZJLX" runat="server" Width="200px" CssClass="required"></asp:DropDownList><tt>*</tt>
                  <%--  <asp:TextBox ID="txtXMJLZJLX" runat="server" CssClass="m_txt required" Width="200px"></asp:TextBox>--%>
                </td>
                 <td class="t_r t_bg">
                    项目经理证件号码：
                </td>
                <td>
                    <asp:TextBox ID="txtXMJLZJHM" Enabled="false" runat="server"  CssClass="m_txt" Width="200"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    总监理工程师姓名：
                </td>
                <td>
                    <asp:HiddenField ID="hfZJLRYBH" runat="server" />
                    <asp:TextBox ID="txtZJLGCSZXZ" runat="server" Enabled="false" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <asp:Button ID="btnZJL" runat="server" CssClass="m_btn_w2 cancel" 
                        OnClientClick="return SearchJLPerson()" Text="选择" onclick="btnZJL_Click" /><tt>*</tt>
                </td>
                 <td class="t_r t_bg">
                    总监理工程师证件类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddlZJLGCSZJLX" runat="server" Width="200" CssClass="required"></asp:DropDownList><tt>*</tt>
                <%--    <asp:TextBox ID="txtZJLGCSZJLX" runat="server"  CssClass="m_txt required" Width="200"></asp:TextBox>--%>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    总监理工程师证件号码：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtZJLGCSZJHM" Enabled="false" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
        </table>
        <table width="98%" align="center" class="m_table">
            <tr>
                <td width="100%" align="right">
                     <%--<asp:Button ID="btnAddPerson" runat="server" CssClass="m_btn_w2" Text="添 加"  />--%>
                    <asp:Button ID="btnRefresh" runat="server" Text="刷 新" CssClass="m_btn_w2" OnClientClick="return  ReFresh();" OnClick="btnRefresh_Click" />
                    <%--<input type="button" value="添 加" onclick="return Add()" class="m_btn_w2" />--%>
                    <asp:Button ID="btnAdd" runat="server" Text="新 增" CssClass="m_btn_w2" OnClientClick="return Add();" OnClick="btnAdd_Click" />
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
                    <%--<asp:TemplateColumn HeaderText="人员姓名">
                        <ItemTemplate>
                            <a href="javascript:void(0)" onclick="SearchPerson(this,'<%#Eval("ID") %>')"><%#Eval("PerSonName") %></a>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
                     <asp:BoundColumn HeaderText="人员姓名" DataField="RYXM">
                        <ItemStyle CssClass="t_l" Wrap="false" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="证件类型" DataField="ZJLXMC">
                        <ItemStyle CssClass="t_l" Wrap="false" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="证件号码" DataField="ZJHM">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="安全生产考核合格证书编号" DataField="AQSCKHHGZBH" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="AQSCGLRYLXMC" HeaderText="安全生产管理人员类型"></asp:BoundColumn>
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
    });
    function SaveValidate() {
        var success = $("#form1").valid();
        if (success)
            return true;
        else
            return false;
    }
    function Add()
    {
        if ($.trim($("#hfId").val()).length == 0)
        {
            alert("请先保存");
            return false;
        }
        showAddWindow("SGXKZFormAdd.aspx?SGXKZID=" + $("#hfId").val(), 800, 500);
        return true;
    }
    function ReFresh()
    {
        if ($.trim($("#hfId").val()).length == 0) {
            alert("请先保存");
            return false;
        }
        return true;
    }

    function Edit(Id)
    {
        showAddWindow("SGXKZFormAdd.aspx?SGXKZID=" + $("#hfId").val() + "&ID=" + Id, 800, 500);
        document.getElementById("btnRefresh").click();
    }
    function Show(Id) {
        showAddWindow("SGXKZFormAdd.aspx?SGXKZID=" + $("#hfId").val() + "&ID=" + Id+"&IsShow=1", 800, 500);
    }
    function SearchKCDW() {
        var result = showWinByReturn("ChooseQY.aspx?typeId=4", 800, 500);
        if (result != undefined) {
            var str = result.split('|');
            $("#hfKCDWBM").val(str[0]);
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
            $("#hfSJDWBM").val(str[0]);
//            $("#txtSJDWMC").val(str[1]);
//            $("#txtSJDWZZJGDM").val(str[2]);
            return true;
        }
        return false;
    } 
    function SearchSGDW() {
        var result = showWinByReturn("ChooseQYSG.aspx?", 800, 500);
        if (result != undefined) {
            var str = result.split('|');
            $("#hfSGDWBM").val(str[0]);
//            $("#txtSGDWMC").val(str[1]);
            //            $("#txtSGDWZZJGDM").val(str[2]);
            return true;
        }
        return false;
    }
    function SearchJLDW() {
        var result = showWinByReturn("ChooseQY.aspx?typeId=6", 800, 500);
        if (result != undefined) {
            var str = result.split('|');
            $("#hfJLDWBM").val(str[0]);
//            $("#txtJLDWMC").val(str[1]);
            //            $("#txtJLDWZZJGDM").val(str[2]);
            return true;
        }
        return false; 
    }
    function SearchJLPerson() {
        var QYBM = $("#hfJLDWBM").val();
        if ($.trim(QYBM).length == 0) {
            alert("请选择监理单位");
            return false;
        }
        var result = showWinByReturn("ChooseManagePerson.aspx?QYBM=" + QYBM, 700, 500);
        if (result != undefined) {
            // var str = result.split('|');
            $("#hfZJLRYBH").val(result);
            return true;
        }
        return false;
    }
    function SearchPerson() {
        var QYBM = $("#hfSGDWBM").val();
        if ($.trim(QYBM).length == 0) {
            alert("请选择施工单位");
            return false;
        }
        var result = showWinByReturn("ChooseManagePerson.aspx?QYBM=" + QYBM, 700, 500);
        if (result != undefined) {
           // var str = result.split('|');
            $("#hfXMJLRYBH").val(result);
            return true;
        }
        return false;
    }
</script>



