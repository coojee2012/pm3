<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntInfo.aspx.cs" Inherits="JSDW_APPLYSGXKZGL_EntInfo" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>企业信息</title>
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
        .auto-style1 {
            height: 23px;
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
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
        <div style="height:100%;width:100%;">
            
        <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                <label runat="server" id="lblTitle"> 施工总承包单位</label>              
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
                    单位名称：
                </td>
                <td colspan="1" width="45%">
                    <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt" Width="195px" MaxLength="40" Enabled="false"></asp:TextBox>
                    <tt>*</tt>
                                    <asp:Button ID="btnAddEnt" runat="server" Text="添加..." CssClass="m_btn_w4" OnClientClick="return selEnt(this,'h_selEntId');"
                    UseSubmitBehavior="false" CommandName="SGT" OnClick="btnAddEnt_Click" Style="margin-bottom: 4px;margin-left:5px;" />
                   
                </td>
                <td class="t_r t_bg" width="14%">
                    单位地址：
                </td>
                <td colspan="2">
                    <asp:TextBox ID="t_FAddress" runat="server" CssClass="m_txt" Width="195px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    法定代表人：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FLegalPerson" runat="server" CssClass="m_txt" Width="195px" MaxLength="40" Enabled="false"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    联系人：
                </td>
                <td colspan="2">
                    <asp:TextBox ID="t_FLinkMan" runat="server" CssClass="m_txt" Width="195px" MaxLength="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    移动电话：
                </td>
                <td colspan="1">
                    <asp:TextBox ID="t_FMobile" runat="server" CssClass="m_txt" Width="195px" ></asp:TextBox>
                </td>
                <td class="t_r t_bg">
                    联系电话：
                </td>
                <td colspan="2">
                    <asp:TextBox ID="t_FTel" runat="server" CssClass="m_txt" Width="195px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td name="td1" class="t_r t_bg">
                    主项资质：
                </td>
                <td name="td2" class="t_r t_bg">
                    资质项：
                </td>
                <td  >
                    <asp:TextBox ID="t_mZXZZ" runat="server" CssClass="m_txt" Width="195px"  TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">
                    组织机构代码：
                </td>
                <td  >
                    <asp:TextBox ID="t_FOrgCode" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            
            <tr name="tr1">
                <td class="t_r t_bg">
                   增项资质：
                </td>
                <td colspan="4">
                    
                    <asp:TextBox ID="t_oZXZZ" runat="server" CssClass="m_txt" 
                        Width="95%" Height="40px" MaxLength="20" TextMode="MultiLine"></asp:TextBox>
                    
                </td>
                
            </tr>
            <tr>
                <td class="t_r t_bg">
                    备注：
                </td>
                <td colspan="4">
                    
                    <asp:TextBox ID="t_Remark" runat="server" CssClass="m_txt" 
                        Width="95%" Height="40px" MaxLength="20" TextMode="MultiLine"></asp:TextBox>
                    
                </td>
                
            </tr>
         
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td>
                    人员信息
                </td>
                <td class="t_r">
                    <asp:UpdatePanel ID="up25" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <input type="button" id="btnAdd" runat="server" value="新增" class="m_btn_w2" onclick="addEmp();" />
                        
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnDel" runat="server" Text="删除" CssClass="m_btn_w2" OnClientClick="return confirm('确认要删除吗?');"
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
                <asp:BoundColumn HeaderText="姓名" DataField="FHumanName">
                    <ItemStyle Wrap="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="注册专业" DataField="ZCZY">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="人员类型" DataField="EmpTypeStr">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="发证日期" DataField="ZCRQ" DataFormatString="{0:d}">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="注册编号" DataField="ZCBH">
                    <ItemStyle Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="人员状态" >
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
    <input id="t_AddressDept" type="hidden" runat="server" />
    <input id="t_Province" type="hidden" runat="server" />
    <input type="hidden"  runat="server" id="t_City" />
    <input type="hidden"  runat="server"  id="t_County"/>
    <input type="hidden"  runat="server" ID="t_FEntType" value="0" />
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

