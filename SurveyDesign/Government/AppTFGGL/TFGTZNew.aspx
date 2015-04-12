<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TFGTZNew.aspx.cs" Inherits="Government_AppTFGGL_TFGTZNew" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <base target="_self"></base>
    <script src="../../script/jquery.js" type="text/javascript"></script>
    <script src="../../script/default.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script type="text/javascript"> 
        function addGCXMInfo() {
            var FAppId = document.getElementById("t_fLinkId").value;
            var FId = document.getElementById("t_FId").value;
            var btn = document.getElementById("btnReload");
            var FPrjItemId = document.getElementById("t_PrjItemId").value;
            //var FPrjId = document.getElementById("t_FPrjId").value;
            //var FPrjItemId = document.getElementById("t_FPrjItemId").value;
            if (FId && FId != "") {
                showAddWindow('XMList.aspx?t_FAppId=' + FId + "&FPrjItemId=" + FPrjItemId, 800, 580, btn);
            }
            else {
                alert("请先保存通告信息！");
            }
        }
    </script>
    <style type="text/css">
        .cBtn7 {
            height: 21px;
        }

        .auto-style3 {
            height: 23px;
        }
        .m_txt {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="t_FId" runat="server" />
        
        <table width="95%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                <asp:Label ID="lbTitle" runat="server" Text="停复工通告">停复工通告</asp:Label>
            </th>
        </tr>
      
     
            <tr>
                <td class="t_r">
                年度：
            </td>
            <td >
                <asp:TextBox ID="t_ND" ReadOnly="true"  runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
           
            <td class="t_r">
                通告批次：
            </td>
            <td >
                <asp:TextBox ID="t_TGPC"  runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
  </tr>
      
       
             
          
             <tr>
            <td class="t_r">
               停工日期：
            </td>
            <td>
                <asp:TextBox ID="t_TGDate" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                复工日期：
            </td>
            <td>
                <asp:TextBox ID="t_FGDate" onfocus="WdatePicker()"   runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
                      <tr>
            <td class="t_r">
                停工原因：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_TGYY"   runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>

    </table>
        
        <div style="text-align: center; margin-top: 2px;">
                            
           
                 <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
              
           &nbsp;&nbsp;<input id="btnReturn" type="button" runat="server" class="m_btn_w2" value="返回" onclick="window.returnValue = '1'; window.close(); " />
        </div>
         <br />
        <div style="width: 95%; margin: 0px auto;">       
        <table width="100%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    工程项目列表
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up25" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input type="button" id="btnAdd" runat="server" value="添加" class="m_btn_w2" onclick="addGCXMInfo();" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
                        OnClick="btn_del_YZ_Click" />
                    <asp:Button ID="btnReload" runat="server" Text="刷新" CssClass="m_btn_w2" OnClick="btnReload_Click"   />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DG_ListYZ" runat="server" AutoGenerateColumns="false" CssClass="m_dg1"
            HorizontalAlign="Center" OnItemDataBound="DG_ListYZ_ItemDataBound" Style="margin-top: 6px;
            margin-bottom: 1px;" Width="100%">
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
                <asp:BoundColumn HeaderText="工程名称" DataField="PrjItemName">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="工程属地" DataField="PrjAddressDeptName">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="建设单位" DataField="JSDW">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                 <asp:BoundColumn HeaderText="证书编号" DataField="SGXKZBH">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                  <asp:BoundColumn HeaderText="发证机关" DataField="FZJG">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="发证时间" DataField="FZTime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
               
                <asp:BoundColumn HeaderText="施工状态" DataField="SGZT">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FId" Visible="false"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
         <div class="d div1 tcen" style="width: 98%; margin: 0px auto;">
        <uc1:pager ID="Pager1" runat="server"></uc1:pager>
    </div>
        </div>
        <br />
       
     
 
        <input id="t_fLinkId" runat="server" type="hidden" />
        <input id="t_PrjItemId" runat="server" type="hidden" />
        <input id="t_fTypeId" runat="server" type="hidden" />
        <input id="t_fSubFlowId" runat="server" type="hidden" />
        <input id="t_fBaseInfoId" runat="server" type="hidden" />
       
        <input id="t_fProcessInstanceID" runat="server" type="hidden" />


           
    </form>
</body>
</html>
