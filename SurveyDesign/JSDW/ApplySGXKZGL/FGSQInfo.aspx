<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FGSQInfo.aspx.cs" Inherits="JSDW_ApplySGXKZGL_FGSQInfo" %>

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
        <table width="95%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                <asp:Label ID="lbTitle" runat="server" Text="停工申请">停工申请</asp:Label>
            </th>
        </tr>
      
     
            <tr>
                <td class="t_r">
                工程名称：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_PrjItemName" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
            </tr>
        <tr>
            <td class="t_r">
                建设单位：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JSDW" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
        </tr>
      
       <tr>
            <td class="t_r">
                发证日期：
            </td>
            <td >
                <asp:TextBox ID="t_FZTime" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        
            <td class="t_r">
                发证机关：
            </td>
            <td >
                <asp:TextBox ID="t_FZJG" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
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
                重新开工日期：
            </td>
            <td>
                <asp:TextBox ID="t_CXKGDate" onfocus="WdatePicker()"   runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>

             <tr>
              <td class="t_r">
                复工原因：
                </td>
                 <td colspan="3">
                     <asp:TextBox ID="t_FGYY" runat="server" CssClass="m_txt"  Width="400px"></asp:TextBox>
                  </td>
             </tr>
              <tr>
                <td class="t_r">
                </td>
                <td>
                </td>
                  <td class="t_r">
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                         <ContentTemplate>
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                            />  
                             <asp:Button ID="btnShangBao" runat="server" Text="上报" OnClick="btnShangBao_Click" CssClass="m_btn_w2"
                            />  
                        </ContentTemplate>
                    </asp:UpdatePanel>                
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                
            </tr>
    </table>
        <br />
       
     
        <input id="fid" runat="server" type="hidden" />
        <input id="t_fLinkId" runat="server" type="hidden" />
        <input id="t_PrjItemId" runat="server" type="hidden" />
        <input id="t_fTypeId" runat="server" type="hidden" />
        <input id="t_fSubFlowId" runat="server" type="hidden" />
        <input id="t_fBaseInfoId" runat="server" type="hidden" />
        <input id="t_fProcessRecordID" runat="server" type="hidden" />
        <input id="t_fProcessInstanceID" runat="server" type="hidden" />
    </form>
</body>
</html>
