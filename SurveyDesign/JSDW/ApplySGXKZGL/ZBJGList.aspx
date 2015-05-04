<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZBJGList.aspx.cs" Inherits="JSDW_ApplySGXKZGL_ZBJGList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"> </script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            showTr1();
        });
        function checkInfo() {
            return AutoCheckInfo();
            var value = document.getElementById("t_JLZBLX").value;

            if (value == "11220801" || value == "11220802") {
                var ly = document.getElementById("t_JLGCS").value;
                var ly1 = document.getElementById("t_JLGCSZJHM").value;
                var message = "";
                if (value == "11220801") {
                    message = "必须填写项目负责人信息";
                } else {
                    message = "必须填写总监理工程师信息";
                }
                if (ly == null || ly == '' || ly1 == null || ly1 == '') {
                    alert(message);
                    return false;

                }

            }
        }


        function addPrjItemJL() {
            var fid = document.getElementById("t_JLId").value;
            var fAppId = '<%=ViewState["FAppId"] %>';
            var fPrjItemId = '<%=ViewState["FPrjItemId"] %>';
            if (fid == null || fid == '') {
                alert('请先保存上方信息！');
                return;
            }
            showAddWindow('File.aspx?fLinkId=' + fid + "&&fAppId=" + fAppId + "&&fPrjItemId=" + fPrjItemId, 800, 550);          
            //  alert('dd')
        }
        function hideTr1() {
            $("tr[name=tr1]").hide();
        }
        function showTr1() {
            var value = document.getElementById("t_JLZBLX").value;
            $("tr[name=tr1]").show();
            if (value == "11220801") {
                $("td[name=td2]").show();
                $("td[name=td1]").hide();
            } else {
                $("td[name=td2]").hide();
                $("td[name=td1]").show();
            }


        }

        function change(value) {
            if (value == "11220801" || value == "11220802") {
                $("tr[name=tr1]").show();
                if (value == "11220801") {
                    $("td[name=td2]").show();
                    $("td[name=td1]").hide();
                } else {
                    $("td[name=td2]").hide();
                    $("td[name=td1]").show();
                }
            }
            else {
                //$("input").removeAttr("disabled");
                $("tr[name=tr1]").hide();
            }
            //施工如果已选择了中标单位数据，且换成了其他类型 则清空数据
            if (value != "11220801") {
                $("#t_JLZBDW").val("");
                $("#t_JLId").val("");
                $("#t_JLZBDWZZJGDM").val("");
                $("#t_JLZBQYZZDJ").val("");
                $("#t_JLZBQYZZZSH").val("");
            }

        }
        function selEnt(obj, tagId) {
            var url = "../project/EntListSel.aspx?e=1";
            //如果是选择代理机构则需要加类型
            if (tagId.value = 't_SGId')
            {
                url += "&qylx=105";
            }
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }
        function selEmp(obj, tagId) {
            //var qybm = document.getElementById("t_JLId").value;
            var qybm = document.getElementById("t_SGId").value;            
            var url = "../project/EmpListSel.aspx";
            if (qybm != null && qybm != '') {
                url += "?qybm=" + qybm;
                var pid = showWinByReturn(url, 800, 500);
                if (pid != null && pid != '') {
                    $("#" + tagId).val(pid);
                    __doPostBack(obj.id, '');
                }
            } else {
                alert("请先选择中标单位");
                return false;
            }

        }
    </script>
    <base target="_self"></base>
    <style type="text/css">
        .modalDiv {
            position: absolute;
            top: 1px;
            left: 1px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: gray;
            opacity: .50;
            filter: alpha(opacity=50);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField runat="server" ID="hf_FEntType" Value="0" />
        <asp:HiddenField runat="server" ID="hf_FAppId" Value="" />
        <asp:HiddenField runat="server" ID="hf_FId" Value="" />

        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="up_Main" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv" style="display: none;">
                    <div style="position: absolute; left: 40%; top: 50%; background-color: peru; border: solid 3px red;">
                        <table align="center">
                            <tr>
                                <td>
                                    <h1>正在保存数据</h1>
                                </td>

                                <td>
                                    <img src="../../image/load2.gif" alt="请稍候" /></td>
                            </tr>

                        </table>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">项目环节材料
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td></td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                OnClientClick="return checkInfo();" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table id="table1" class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="4">
                    <h3>招投标信息</h3>
                </td>
            </tr>
            <!--<tr>
            <td class="t_r t_bg" style="width:18.8%;">
                办理选项：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:DropDownList ID="t_BL" class="cc2" runat="server" CssClass="m_txt" onchange="change(this.value)" Width="60%">
                    <asp:ListItem Value="1">补填</asp:ListItem>
                    <asp:ListItem Value="0">不需要办理</asp:ListItem>
                    <asp:ListItem Value="2">以后补办</asp:ListItem>
                    
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr name="tr1">
            <td class="t_r t_bg">
                理由： </td>
            <td colspan="3">
                <asp:TextBox ID="t_YL" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox><tt name="tt_t1">*</tt>
            </td>
        </tr>-->
            <tr>
                <td class="t_r t_bg" style="width: 18.8%;">项目名称：
                </td>
                <td colspan="1" style="width: 29%;">
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                    <input id="txtFId" type="hidden" runat="server" />
                </td>
                <td class="t_r t_bg">工程名称： </td>
                <td>
                    <asp:TextBox ID="t_PrjItemName" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">建设单位： </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">总面积：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Area" runat="server" CssClass="m_txt" Enabled="false"
                        Width="195px"></asp:TextBox>（m2）
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 18.8%;">项目编号：
                </td>
                <td colspan="1" style="width: 29%;">
                    <asp:TextBox ID="t_ProjectNo" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="t_r t_bg">建设规模：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_ConstrScale" cs="cs1" runat="server" CssClass="m_txt" Width="638px" Height="40px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="t_r t_bg">招标方式： </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_JLZBFS" cs="cs1" runat="server" CssClass="m_txt" Width="201px">
                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                    </asp:DropDownList><tt>*</tt>
                    <input id="txtJLId" type="hidden" runat="server" />
                </td>
                <td class="t_r t_bg">招标类型： </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_JLZBLX" cs="cs1" runat="server" CssClass="m_txt" Width="201px" onchange="change(this.value)">
                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                    </asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 18.8%;">中标单位：
                </td>
                <td colspan="1" style="width: 29%;">
                    <asp:TextBox ID="t_JLZBDW" cs="cs1" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox><tt>*</tt>
                    <input type="hidden" runat="server" id="t_JLId" value="" />
                    <asp:Button ID="btnAddEnt" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_JLId');"
                        UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntSC_Click" Style="margin-bottom: 4px; margin-left: 5px;" />
                </td>
                <td class="t_r t_bg">中标单位组织机构代码： </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JLZBDWZZJGDM" cs="cs1" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 18.8%;">中标企业资质等级：
                </td>
                <td colspan="1" style="width: 29%;">
                    <asp:TextBox ID="t_JLZBQYZZDJ" cs="cs1" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">中标企业资质证书号： </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JLZBQYZZZSH" cs="cs1" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <!--<tr>
            <td class="t_r t_bg" style="width:18.8%;">
                质量标准：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JLZLBZ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>  
        </tr>-->
            <tr>
                <td class="t_r t_bg" style="width: 18.8%;">中标金额：
                </td>
                <td colspan="1" style="width: 29%;">
                    <asp:TextBox ID="t_JLZBJ" cs="cs1" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（万元）
                <tt>*</tt>
                </td>
                <!--<td class="t_r t_bg">
                大写： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLZBJDX" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>   -->
                <td class="t_r t_bg">中标日期： </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JLZBRQ" cs="cs1" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <!--<tr>
            <td class="t_r t_bg" style="width:18.8%;">
                中标工期：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JLZBGQ" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>（天）
            </td>  
        </tr>
        <tr>
            <td class="t_r t_bg">
                合同开工日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLHTKGTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                合同竣工日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLHTJGTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>  -->
            <tr>
                <!--<td class="t_r t_bg">
                合同签订日期： </td>
            <td colspan="1">
                <asp:TextBox ID="t_JLHTQDTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>-->
                <td class="t_r t_bg">中标通知书编号： </td>
                <td>
                    <asp:TextBox ID="t_ZBTZSBH" cs="cs1" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">备案时间： </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JLHTBATime" cs="cs1" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="t_r t_bg" style="width: 18.8%;">招标代理单位名称：
                </td>
                <td colspan="1" style="width: 29%;">
                    <asp:TextBox ID="t_ZBDLDWMC" cs="cs1" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox><tt>*</tt>
                    <input type="hidden" runat="server" id="t_SGId" value="" />
                    <input type="hidden" runat="server" id="t_SGIdold" value="" />
                    <asp:Button ID="Button1" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'t_SGId');"
                        UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntSG_Click" Style="margin-bottom: 4px; margin-left: 5px;" />
                </td>
                <td class="t_r t_bg">招标代理单位组织机构代码： </td>
                <td>
                    <asp:TextBox ID="t_ZBDLDWZZJGDM" cs="cs1" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr name="tr1">
                <td class="t_r t_bg" name="td1">总监理工程师姓名：
                </td>
                <td class="t_r t_bg" name="td2">项目负责人姓名：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_JLGCS" cs="cs2" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                    <input type="hidden" runat="server" id="t_SJId" value="" />
                    <asp:Button ID="Button2" cs="cs1" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEmp(this,'t_SJId');"
                        UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEntSJ_Click" Style="margin-bottom: 4px; margin-left: 5px;" />
                </td>
            </tr>
            <tr name="tr1">
                <td class="t_r t_bg" style="width: 18.8%;">证件类型：
                </td>
                <td colspan="1" style="width: 29%;">
                    <asp:DropDownList ID="t_JLGCSZJLX" cs="cs2" runat="server" CssClass="m_txt" Width="195px"></asp:DropDownList>
                </td>
                <td class="t_r t_bg">证件号码： </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JLGCSZJHM" cs="cs2" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td>材料信息
                </td>
                <td class="t_r">
                    <%--<input type="button" id="Button4" cs="cs1" runat="server"    value="新增" class="m_btn_w2" onclick="addPrjItemJL();" />--%>
                    <asp:Button id="Button4" cs="cs1" runat="server"  Text="新增"   value="新增" class="m_btn_w2" OnClientClick="addPrjItemJL();" OnClick="Button3_Click" />
                    <asp:Button ID="Button5" cs="cs1" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                        OnClick="btnDel_ClickJL" />
                    <asp:Button ID="Button6" cs="cs1" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_ClickJL" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_ListJL" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBoundJL" Style="margin-top: 6px; margin-bottom: 1px;"
            Width="98%">
            <HeaderStyle CssClass="m_dg1_h" />
            <ItemStyle CssClass="m_dg1_i" />
            <Columns>
                <asp:TemplateColumn>
                    <HeaderStyle Width="30px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="序号">
                    <HeaderStyle Width="30px" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="材料名称" DataField="FileName">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="上传时间" DataField="ReportTime" DataFormatString="{0:d}">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <div style="padding-left: 1%">
            <webdiyer:AspNetPager ID="PagerJL" runat="server" AlwaysShow="True" CssClass="pages"
                CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager_PageChangingJL"
                pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
            </webdiyer:AspNetPager>
        </div>

    </form>
</body>
</html>
