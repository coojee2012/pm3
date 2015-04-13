<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTBAList.aspx.cs" Inherits="JSDW_ApplySGXKZGL_HTBAList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>合同备案列表</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();


            hideTr1();

        });
        function checkInfo() {
            var value = document.getElementById("t_BL").value;
            if (value == "1") {
                return AutoCheckInfo();
            } else {
                var ly = document.getElementById("t_YL").value;
                if (ly == null || ly == '') {
                    alert('必须填写理由！');
                    return false;
                }
            }
        }
        function addEnt() {
            var FAppId = document.getElementById("hf_FAppId").value;
            showAddWindow('HTBA.aspx?FAppId=' + FAppId, 1100, 700);
            $("tr[name=tr1]").hide();
        }
        function showTr1() {
            $("tr[name=tr1]").show();
            $('[cs=cs1]').each(function (i) {
                $("#" + this.id).attr("disabled", true);
            });
        }
        function hideTr1() {
            $("tr[name=tr1]").hide();
            $('[cs=cs1]').each(function (i) {
                $("#" + this.id).removeAttr("disabled");
            });
        }
        function change(value) {
            if (value == "1") {
                $("tr[name=tr1]").hide();
                $('[cs=cs1]').each(function (i) {
                    $("#" + this.id).removeAttr("disabled");
                });
            }
            else {
                $("tr[name=tr1]").show();
                //$("input").removeAttr("disabled");
                $('[cs=cs1]').each(function (i) {
                    $("#" + this.id).attr("disabled", true);
                });
            }
        }
    </script>
    <base target="_self">
    </base>
    <style type="text/css">
        .modalDiv { position: absolute; top: 1px; left: 1px; width: 100%; height: 100%; z-index:9999; background-color:gray; opacity:.50; filter: alpha(opacity=50); }
        .m_txt {}
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField  runat="server" ID="hf_FEntType" Value="0" />
        <asp:HiddenField  runat="server" ID="hf_FAppId" Value="" />
        <asp:HiddenField  runat="server" ID="hf_FId" Value="" />
        <input type="hidden"  runat="server" ID="t_FPrjId" value="" />
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="up_Main" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv" style="display:none;"> 
                <div style="position:absolute;left:40%;top:50%;background-color:peru;border:solid 3px red;">
                    <table  align="center">
                    <tr>
                    <td ><h1>正在保存数据</h1></td>

                    <td><img src="../../image/load2.gif" alt="请稍候"/></td>
                    </tr>
                                    
                    </table>
                </div>
                    </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

            
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                项目环节材料
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                    OnClientClick="return checkInfo();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table id="table1" class="m_table" width="98%" align="center">
        <tr>
            <td class="t_l t_bg" colspan="4">
                <h3>合同备案信息</h3>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" style="width:18.8%;">
                办理选项：
            </td>
            <td colspan="1" style="width:29%;">
                <asp:DropDownList ID="t_BL" class="cc2" runat="server" CssClass="m_txt" onchange="change(this.value)" Width="60%">
                    <asp:ListItem Value="1">补填</asp:ListItem>
                    <asp:ListItem Value="0">不需要办理</asp:ListItem>
                    <asp:ListItem Value="2">以后补办</asp:ListItem>
                        <asp:ListItem Value="3">已办</asp:ListItem>
                    
                </asp:DropDownList>
                <input id="txtFId" type="hidden" runat="server" />
            </td>
            
        </tr>
        <tr name="tr1">
            <td class="t_r t_bg">
                理由： </td>
            <td colspan="3">
                <asp:TextBox ID="t_YL" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="72.2%"></asp:TextBox>
            </td>
        </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    <label runat="server" id="lblTitle"> 合同备案</label>     
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input type="button" id="btnAdd" cs="cs1" runat="server" value="新增" class="m_btn_w2" onclick="addEnt();" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <asp:Button ID="btnDel" cs="cs1" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                        OnClick="btnDel_Click" />
                    <asp:Button ID="btnReload" cs="cs1" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dg_List" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="App_List_ItemDataBound" Style="margin-top: 6px;
            margin-bottom: 1px;" Width="98%">
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
                <asp:BoundColumn HeaderText="合同备案编号" DataField="HTBABH">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="合同类别" DataField="HTLBStr">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="合同金额(万元)" DataField="HTJE">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="发包单位名称" DataField="FBDWMC">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <div style="padding-left: 1%">
            <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" layouttype="Table"
                NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                pageindexboxtype="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                showpageindexbox="Always" SubmitButtonText="Go" textafterpageindexbox="页" textbeforepageindexbox="转到">
            </webdiyer:AspNetPager>
        </div>
    </div>
    
    </form>
</body>
</html>
