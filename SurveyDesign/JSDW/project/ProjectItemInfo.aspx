<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectItemInfo.aspx.cs" Inherits="JSDW_project_ProjectItemInfo" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptidfalse2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>单项工程信息</title>
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
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="up_Main" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv"> 
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
                        <asp:Button ID="btnRefresh" runat="server" Text="同步到标准库"  CssClass="m_btn_w6"
                                onclick="btnRefresh_Click" />  
                    <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    项目名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="200px" MaxLength="40" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    项目所属地：
                </td>
                <td colspan="1">           
                    <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />
                    <!--<asp:TextBox ID="AddressDeptName" runat="server" CssClass="m_txt" Width="200px" MaxLength="40" ReadOnly="true"></asp:TextBox>-->
                    <input id="t_AddressDept" type="hidden" runat="server" />
                </td>
                <td class="t_r t_bg">
                    项目地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设总面积：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Area" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    投资规模：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Investment" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="200px" Enabled="false"></asp:TextBox>(万元)
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设规模：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_ConstrScale" runat="server" Enabled="false" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,1000,'建设规模');"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    法人代表：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="t_LegalPerson" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    组织机构代码：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_JSDWDM" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程名称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_PrjItemName" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    工程类别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_PrjItemType" runat="server" Width="200px" CssClass="m_txt" Enabled="false">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    结构类别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_ConstrType" runat="server" Width="200px" CssClass="m_txt">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    工程造价：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_Cost" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="200px"></asp:TextBox>(万元) <tt>*</tt>
                </td>
            </tr>          
            <tr>
                <td class="t_r t_bg">
                    工程规模（m2）
                </td>
                <td colspan="3">
                      <asp:TextBox ID="t_Scale" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                        MaxLength="20" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程描述：
                </td>
                <td colspan="3" class="m_txt_M">
                    <asp:TextBox ID="t_PrjItemDesc" runat="server" CssClass="m_txt" Height="35px" TextMode="MultiLine"
                        Width="539px" onblur="checkLength(this,1000,'工程描述');"></asp:TextBox>
                </td>
            </tr>              
        </table>
    </div>
    
    
    </form>
</body>
</html>
