<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrjDetailInfo.aspx.cs" Inherits="JSDW_APPLYSGXKZGL_PrjDetailInfo" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工程明细信息</title>
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

        //保存单位后，刷新人员信息
        function reloadEmpList() {
            $("#btnReload").click();
        }
        function addEmp() {
            var fid = document.getElementById("h_selEntId").value;          
            var FPrjItemId = document.getElementById("t_FPrjItemId").value;
            var t_FEntType = document.getElementById("t_FEntType").value;
            if (fid == null || fid == '') {
                alert('请先保存上方的企业信息！');
                return;
            }
            showAddWindow('EmpInfo.aspx?FEntId=' + fid + 
                 '&FPrjItemId=' + FPrjItemId + '&t_FEntType=' + t_FEntType, 1000, 600);
        }
        function showTr1() {
            $("td[name=td1]").show();
            $("tr[name=tr1]").show();
            $("td[name=td2]").hide();
        }
        function showTr2() {
            $("td[name=td1]").hide();
            $("tr[name=tr1]").hide();
            $("td[name=td2]").show();       
        }
        function selEnt(obj, tagId) {
            var type = document.getElementById("t_FEntType").value;
            var qylx = "";
            //根据企业类型跳转到不同的企业选择页面
            switch (type) {
                case "2":
                    qylx = "101";
                    var url = "../project/EntListSelSg.aspx";
                    url += "?qylx=" + qylx;
                    var pid = showWinByReturn(url, 1000, 600);
                
                    if (pid != null && pid != '') {
                         $("#" + tagId).val(pid);
                        __doPostBack(obj.id, '');
                    }
                    break;
                case "3":
                    qylx = "101";
                    var url = "../project/EntListSelSg.aspx";
                    url += "?qylx=" + qylx;
                    var pid = showWinByReturn(url, 1000, 600);
                 
                    if (pid != null && pid != '') {
                     
                        $("#" + tagId).val(pid);
                        __doPostBack(obj.id, '');
                    }
                    break;
                case "4":
                    qylx = "101";
                    var url = "../project/EntListSelSg.aspx";
                    url += "?qylx=" + qylx;
                    var pid = showWinByReturn(url, 1000, 600);
                    if (pid != null && pid != '') {
                        $("#" + tagId).val(pid);
                        __doPostBack(obj.id, '');
                    }
                    break;
                case "5":
                    qylx = "102";
                    var url = "../project/EntListSel.aspx";

                    url += "?qylx=" + qylx;                 
                    var pid = showWinByReturn(url, 1000, 600);
                    if (pid != null && pid != '') {
                        $("#" + tagId).val(pid);
                        __doPostBack(obj.id, '');
                    }
                    break;
                case "6":
                    qylx = "103";
                    var url = "../project/EntListSel.aspx";

                    url += "?qylx=" + qylx;
                    var pid = showWinByReturn(url, 1000, 600);
                    if (pid != null && pid != '') {
                        $("#" + tagId).val(pid);
                        __doPostBack(obj.id, '');
                    }
                    break;
                case "7":
                    qylx = "104";
                    var url = "../project/EntListSel.aspx";
                    url += "?qylx=" + qylx;
                    var pid = showWinByReturn(url, 1000, 600);
                    if (pid != null && pid != '') {
                        $("#" + tagId).val(pid);
                        __doPostBack(obj.id, '');
                    }
                    break;
            }        
        }
    </script>

    <base target="_self">
    </base>
    <style type="text/css">
        .modalDiv { position: absolute; top: 1px; left: 1px; width: 100%; height: 100%; z-index:9999; background-color:gray; opacity:.50; filter: alpha(opacity=50); }
        .auto-style2 {
            height: 36px;
        }
        .m_txt {}
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">

        </asp:UpdateProgress>
        <div style="height:100%;width:100%;">
            
        <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                <label runat="server" id="lblTitle"> 工程项目明细信息</label>              
            </th>
        </tr>
    </table> 
    <div id="divSetup2" runat="server">
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                    OnClientClick="return checkInfo();" />
                            <input id="txtFId" type="hidden" runat="server" />
                            <input id="t_QYID" type="hidden" runat="server" />
                            <input type="hidden"  runat="server" ID="h_selEntId" value="" />
                        </ContentTemplate>
                    </asp:UpdatePanel>                  
                    
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg" width="12%">
                    单体工程名称：
                </td>
                <td colspan="4" width="45%">
                    <asp:TextBox ID="t_DetailName" runat="server" CssClass="m_txt" Width="617px" MaxLength="40" Enabled="false"></asp:TextBox>
                    <tt>*</tt>
                   
                </td>

            </tr>
            <tr>
                <td class="auto-style2">
                    建筑面积/长度：
                </td>
                <td colspan="4" class="auto-style2">
                    <asp:TextBox ID="t_Scale" runat="server" CssClass="m_txt" Width="195px" MaxLength="40" Enabled="false"></asp:TextBox>
                    <tt>（平方米/米）*</tt>
                </td>

            </tr>
            <tr>
                <td class="t_r t_bg">
                    其中：地上
                </td>
                <td colspan="2">
                    <asp:TextBox ID="t_UpScale" runat="server" CssClass="m_txt" Width="195px" ></asp:TextBox>平方米/米
                </td>
                <td class="t_r t_bg">
                    地下
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_DoScale" runat="server" CssClass="m_txt" Width="195px" ></asp:TextBox>平方米/米
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    层数：地上
                </td>
                <td colspan="2">
                    <asp:TextBox ID="t_AbLayerNum" runat="server" CssClass="m_txt" Width="195px" ></asp:TextBox>&nbsp;</td>
                <td class="t_r t_bg">
                    地下
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_UnLayerNum" runat="server" CssClass="m_txt" Width="195px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    备注：
                </td>
                <td colspan="4">
                    
                    <asp:TextBox ID="t_Remark" runat="server" CssClass="m_txt" 
                        Width="73%" Height="40px" MaxLength="20" TextMode="MultiLine"></asp:TextBox>
                    
                </td>
                
            </tr>
         
        </table>
    </div>
    <input id="t_AddressDept" type="hidden" runat="server" />
    <input id="t_Province" type="hidden" runat="server" />
    <input type="hidden"  runat="server" id="t_City" />
    <input type="hidden"  runat="server"  id="t_County"/>
    <input type="hidden"  runat="server" ID="t_SgxkzId" value="" />
    <input type="hidden"  runat="server" ID="t_FAppId" />
    <input type="hidden"  runat="server" id="hf_FId" />
    <input type="hidden"  runat="server" ID="t_FPrjId" />
    <input type="hidden"  runat="server" ID="t_FPrjItemId" />
    
    </div>
     
    
    </form>
</body>

<script type="text/javascript">
    function changeCheck(obj) {
        obj.style.background = obj.checked ? '#1eaffc' : "";
    }
    $.each($(":checkbox[id^=t_F]"), function () {
        $(this).click(function () { changeCheck(this); });
        changeCheck(this);
    });
</script>

</html>

