﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BGEntList.aspx.cs" Inherits="JSDW_ApplySGXKZGL_EntListForBG" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业列表</title>
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
        function addEnt() {
            var FAppId = document.getElementById("hf_FAppId").value;
            var FEntType = document.getElementById("hf_FEntType").value;
            var FPrjId = document.getElementById("t_FPrjId").value;
            var FPrjItemId = document.getElementById("t_FPrjItemId").value;
            showAddWindow('BGEntInfo.aspx?FAppId=' + FAppId + '&FEntType=' + FEntType + '&FPrjId=' + FPrjId +
                 '&FPrjItemId=' + FPrjItemId, 900, 700);
            __doPostBack(this.id, '');
        }

        function dellEnt() {
            $("#isDel").val(1);
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
        <asp:HiddenField  runat="server" ID="HiddenField1" Value="" />
        <input type="hidden"  runat="server" ID="t_FPrjId" value="" />
        <input type="hidden"  runat="server" ID="t_FPrjItemId" value="" />
        <input type="hidden"  runat="server" ID="isDel" value="" />
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
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    <label runat="server" id="lblTitle"> 施工总承包单位</label>     
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="addEnt();" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="dellEnt();return confirm('确认要删除吗?');"
                        OnClick="btnDel_Click" />
                    <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click" />
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
                <asp:BoundColumn HeaderText="单位名称" DataField="FName">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="法定代表人" DataField="FLegalPerson">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="主项资质" DataField="mZXZZ">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="联系人" DataField="FLinkMan">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="状态" >
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="QYID" Visible="false"  HeaderText ="企业id"></asp:BoundColumn> 
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