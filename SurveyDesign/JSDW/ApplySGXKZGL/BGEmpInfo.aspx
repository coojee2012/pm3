<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BGEmpInfo.aspx.cs" Inherits="JSDW_ApplySGXKZGL_EmpInfoForBG" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员信息</title>
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
    </script>
    <base target="_self">
    </base>
    <style type="text/css">
        .modalDiv { position: absolute; top: 1px; left: 1px; width: 100%; height: 100%; z-index:9999; background-color:gray; opacity:.50; filter: alpha(opacity=50); }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <input type="hidden"  runat="server" ID="t_FAppId" value="" />
        <input type="hidden"  runat="server" ID="txtFId" value="" />
        <input type="hidden"  runat="server" ID="t_FEntId" value="" />
        <input type="hidden"  runat="server" ID="t_FPrjId" value="" />
        <input type="hidden"  runat="server" ID="t_FPrjItemId" value="" />
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
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                         <ContentTemplate>
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                            OnClientClick="return checkInfo();" />  
                        </ContentTemplate>
                    </asp:UpdatePanel>                
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    人员类型：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_EmpType" runat="server" Width="203px" CssClass="m_txt">
                        
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    姓名：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FHumanName" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    身份证号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FIdCard" runat="server" CssClass="m_txt" Width="200px"  ></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    性别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_FSex" runat="server" CssClass="m_txt" Width="203px" >
                        <asp:ListItem Value="1">男</asp:ListItem>
                        <asp:ListItem Value="2">女</asp:ListItem>
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    最高学历：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZGXL" runat="server" CssClass="m_txt" Width="203px" ></asp:DropDownList>
                   
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    移动电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FMobile" onblur="isTel(this);" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    职称：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ZC" runat="server" CssClass="m_txt" Width="203px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    职务：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZW" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FTel" onblur="isTel(this);" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    专业：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZY" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    证书编号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZSBH" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    等级：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_DJ" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    注册编号：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZCBH" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>          
            <tr>
                <td class="t_r t_bg">
                    注册专业：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZCZY" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    注册日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ZCRQ" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
                   
        </table>
    </div>
    <input id="t_AddressDept" type="hidden" runat="server" />
    
    </form>
</body>
</html>