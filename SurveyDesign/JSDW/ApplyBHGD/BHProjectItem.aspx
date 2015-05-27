<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BHProjectItem.aspx.cs" Inherits="JSDW_APPLYBHGD_BHProjectItem" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptidfalse2.ascx" TagName="govdeptid1" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>标准化工地工程信息</title>
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
        .m_txt {}
        .auto-style1 {
            height: 20px;
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField  runat="server" ID="hf_FAppId" Value="" />
        <asp:HiddenField  runat="server" ID="hf_FId" Value="" />
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
                标准化工地工程信息
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
                    <asp:UpdatePanel ID="up_Main" runat="server" RenderMode="Inline">
                         <ContentTemplate>
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                            OnClientClick="return checkInfo();" />  
                        
                        </ContentTemplate>
                    </asp:UpdatePanel>                
                    
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="auto-style1" colspan="4">
                    建设单位基本信息
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg">
                    建设单位：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_JSDW" runat="server" CssClass="m_txt" Width="200px" MaxLength="40" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" style="width:18.8%;">
                    建设单位所属地：
                </td>
                <td colspan="1" style="width:29%;">
                    <input type="hidden"  runat="server" ID="t_JSDWAddressDept" value="" />
                    <uc2:govdeptid ID="JSDW_DeptID" runat="server" />
                    
                </td>
                <td class="t_r t_bg">
                    建设单位地址：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSDWDZ" runat="server" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <tt>*</tt></td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    所有制性质：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_JSDWXZ" runat="server" CssClass="m_txt" Width="203px" ></asp:DropDownList>
                </td>
                <td class="t_r t_bg">
                    法定代表人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FDDBR"  runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    法人电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FRDH" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    领证人：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="t_LZR" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td >
                    <asp:TextBox ID="t_LXDH" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    技术负责人：
                </td>
                <td >
                    <asp:TextBox ID="t_JSFZR" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    技术负责人职称：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSFZRZC" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    技术负责人电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_JSFZRDH" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox>
                    
                </td>
            </tr>            
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_l t_bg" colspan="4">
                    工程基本信息
                </td>
             </tr>
            <tr>
                <td class="t_r t_bg"  style="width:18.8%;">
                    项目名称：
                </td>
                <td style="width:29%;">
                    <asp:TextBox ID="t_ProjectName" runat="server" CssClass="m_txt" Width="200px" Enabled="false" ></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    工程名称：
                </td>
                <td >
                    <asp:TextBox ID="t_PrjItemName" runat="server" CssClass="m_txt" Width="200px" Enabled="false" ></asp:TextBox><tt>*</tt>
                    <input id="t_PrjItemId" type="hidden" runat="server" />
                    <input id="t_PrjId" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    工程类别：
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="t_PrjItemType" runat="server" CssClass="m_txt" Width="200px" Enabled="false" ></asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    工程所属地：
                </td>
                <td colspan="1">
                    <input type="hidden" runat="server" ID="t_PrjAddressDept" value="" />
                    <uc1:govdeptid1 ID="PrjGovdeptid" runat="server" />                   
                </td>
            </tr>

            <tr>
                <td class="t_r t_bg">
                    报建时间：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_ProjectTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" ></asp:TextBox><tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    申报时间：
                </td>
                <td colspan="1">
                     <asp:TextBox ID="t_ReportTime" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="200px" Enabled="false"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    建设地址：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_Address" runat="server" CssClass="m_txt" Width="77%" ></asp:TextBox><tt>*</tt>
                </td>
                
            </tr>
            <tr>
                <td>
                    建设规模：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_ConstrScale" runat="server" CssClass="m_txt" Width="77%" Height="40px" TextMode="MultiLine"></asp:TextBox>
                    <tt>*</tt>
                </td>
               
            </tr>
            <tr>
                <td class="t_r t_bg">
                    结构类型：
                </td>
                <td >
                    <asp:DropDownList ID="t_ConstrType" runat="server" CssClass="m_txt" Width="203px">
                    </asp:DropDownList>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    合同价格：
                </td>
                <td >
                    <asp:TextBox ID="t_Price" runat="server" CssClass="m_txt" Width="100px" Enabled="true"></asp:TextBox><tt>*</tt>币种：
                    <asp:DropDownList ID="t_Currency" runat="server" CssClass="m_txt" Width="103px" Enabled="false"></asp:DropDownList><tt>*</tt>
                </td>
            </tr>
            
            <tr>
                <td class="t_r t_bg">
                    合同开工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_StartDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    合同竣工工日期：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_EndDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="200px"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr> 
            <tr>
                <td class="t_r t_bg">
                    备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_Remark" runat="server" CssClass="m_txt" Width="638px" MaxLength="40" Height="40px" TextMode="MultiLine"></asp:TextBox>
                </td>
               
            </tr>           
        </table>
    </div>
    
    </form>
</body>
</html>
