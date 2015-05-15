<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SGXKZZYSL.aspx.cs" Inherits="Government_AppSGXKZGL_SGXKZZYSL" %>

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
        function appPrint() {
            var FAppId = document.getElementById('t_fLinkId').value;
            var vffid = $('#t_ffid').val();
            var vissave = document.getElementById('issave').value;

            if (vissave != "1") {
                alert("请先保存！");
                return false;
            } else {              
                window.open('Print.aspx?FAppId=' + FAppId + "&printType=1", '_blank');                
            }
        }
    </script>
    <style type="text/css">
        .cBtn7 {
            height: 21px;
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
                <asp:Label ID="lbTitle" runat="server" Text="准予受理">准予受理</asp:Label>
            </th>
        </tr>
      
     <tr>
            <td class="t_r">
                编号：
            </td>
            <td>
                <asp:TextBox ID="t_BH" ReadOnly="true" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                受理日期：
            </td>
            <td>
                <asp:TextBox ID="t_RQ" ReadOnly="true"    runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>

            <tr>
                <td class="t_r">
                申请内容：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_NR" ReadOnly="true" runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td class="t_r">
                联系人：
            </td>
            <td>
                <asp:TextBox ID="t_LXR"  runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="t_LXDH"    runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
            <td class="t_r">
                监督电话：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_JDDH"  runat="server" CssClass="m_txt" Width="400px"></asp:TextBox>
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
                        <asp:Button ID="btnPrint" runat="server" Text="打印"  OnClientClick="appPrint();" CssClass="m_btn_w2"
                            /> 
                        </ContentTemplate>
                    </asp:UpdatePanel>                
                    &nbsp;<input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" /></td>
                
            </tr>
    </table>
        <br />
       
     
 
        <input id="t_fLinkId" runat="server" type="hidden" />
        <input id="t_PrjItemId" runat="server" type="hidden" />
        <input id="t_fTypeId" runat="server" type="hidden" />
        <input id="t_fSubFlowId" runat="server" type="hidden" />
        <input id="t_fBaseInfoId" runat="server" type="hidden" />
        <input id="t_fProcessRecordID" runat="server" type="hidden" />
        <input id="t_fProcessInstanceID" runat="server" type="hidden" />
        <input id="t_ffid" runat="server" type="hidden" />
        <input id="issave" runat="server" type="hidden" />
    </form>
</body>
</html>
