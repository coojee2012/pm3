<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RWSDList.aspx.cs" Inherits="Government_AppTFGGL_RWSDList" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员锁定管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            txtCss();
            DynamicGrid(".m_dg1_i");
        });

        function showApproveWindow1(sUrl, width, height) {
            var ret = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:auto;')

            if (ret == "1") {
                form1.btnQuery.click()
            }
        }
        function ShowWindow(url, width, hieght, obj) {
            var sFeatures = "status:no;dialogHeight:" + hieght + "px;dialogwidth:" + width + "px;scroll=no;center:yes; resizable:yes; status:no; help:no;scroll:auto;";

            var idvalue = window.showModalDialog(url + '&rid=' + Math.random(), obj, sFeatures);

            if (idvalue == "1") {
                //alert(idvalue);
                //alert(form1);
                form1.btnQuery.click();
            }
        }
        function Request(strName) {
            var strHref = window.document.location.href;
            var intPos = strHref.indexOf("?");
            var strRight = strHref.substr(intPos + 1);

            var arrTmp = strRight.split("&");
            for (var i = 0; i < arrTmp.length; i++) {
                var arrTemp = arrTmp[i].split("=");

                if (arrTemp[0].toUpperCase() == strName.toUpperCase()) return arrTemp[1];
            }
            return "";
        }

        function app(url) {
            var tmpVal = '';
            var fAppId = '';
            var fId = '';
            var cou = 0;
            var chkColl = document.getElementsByTagName("input");
            for (var i = 0; i < chkColl.length; i++) {
                if (chkColl[i].type == "checkbox" && chkColl[i].id.indexOf("CheckItem") > -1) {
                    if (!chkColl[i].disabled && chkColl[i].checked == true) {
                        cou += 1;
                        var span = document.getElementById("span" + chkColl[i].id)
                        if (span) {
                            if (tmpVal.indexOf(span.getAttribute("name") + ",") == -1) {
                                tmpVal += span.getAttribute("name") + ",";
                                fId += span.getAttribute("fId") + ",";
                                fAppId += span.getAttribute("fAppId") + ",";
                            }
                        }
                    }
                }
            }
            var obj = new Object();
            if (tmpVal.length > 1) {
                tmpVal = tmpVal.substring(0, tmpVal.length - 1);
                fId = fId.substring(0, fId.length - 1);
                fAppId = fAppId.substring(0, fAppId.length - 1);

            }
            else {
                alert("请选择一条信息进行处理！");
                return false;
            }
            if (cou > 1 || cou <= 0) {
                alert("只能选择一条处理信息！");
                return false;
            }

            obj.name = '';
            obj.id = tmpVal;
            ShowWindow(url + '?fId=' + fId + '&fAppId=' + fAppId, 900, 600, obj);


            return false;
        }
        function appAdd(url) {
            var FAppId = document.getElementById("t_fLinkId").value;
            var btn = document.getElementById("btnReload");
            var FPrjItemId = document.getElementById("t_PrjItemId").value;
            //var FPrjId = document.getElementById("t_FPrjId").value;
            //var FPrjItemId = document.getElementById("t_FPrjItemId").value;
            showAddWindow(url + '?FAppId=' + FAppId + "&FPrjItemId=" + FPrjItemId, 600, 450, btn);
        }

        function btnQueryClickClient() {
            //alert(12);
            // $("#btnQuery").attr("disabled", "disabled");
            return true;
        }

    </script>

    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="7">
                    <asp:Literal ID="lPostion" runat="server">人员锁定管理</asp:Literal>
                </th>
            </tr>
            <tr>
                <td class="t_r">姓名：
                </td>
                <td>
                    <asp:TextBox ID="txtXM" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">身份证号：
                </td>
                <td>
                    <asp:TextBox ID="txtIDCard" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
               <%-- OnClientClick="return btnQueryClickClient();"--%>
                <td colspan="2" rowspan="2" style="text-align: center; padding-right: 10px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2"  OnClick="btnQuery_Click" Text="查询" />
                    &nbsp;                <input id="btnClear" class="m_btn_w2" style="margin-top: 3px;" type="button" value="重置"
                    onclick="clearPage();" />
                </td>
            </tr>
            <tr>
                <td class="t_r">证书编号：
                </td>
                <td>
                    <asp:TextBox ID="txtSGXKZBH" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
                <td class="t_r">企业名称：
                </td>
                <td>
                    <asp:TextBox ID="txtQYMC" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                </td>
            </tr>
             <td class="t_r">参与项目名称：
                </td>
                <td>
                <asp:TextBox ID="tbcyxm" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>

                </td>
       <td>
           &nbsp;</td>
            <td>
                &nbsp;</td>




<tr><td colspan ="5" style="color: #FF0000" class="auto-style1">备注：输入查询条件后可以跨地区查询全省所有符合条件的人员参与在建项目情况。</td></tr>
        </table>



      <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <asp:DataGrid ID="JustAppInfo_List" runat="server" AutoGenerateColumns="False" CssClass="m_dg1"
                    HorizontalAlign="Center" OnItemDataBound="JustAppInfo_List_ItemDataBound" Width="98%">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <ItemStyle CssClass="m_dg1_i" />
                   
                    <Columns>

                        
                       <%-- <asp:BoundColumn HeaderText="锁定次数" DataField="SelectedCount">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>--%>
                       <%--  <asp:BoundColumn HeaderText="锁在项目" DataField="PrjItemName">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>--%>
                       
                       
                       

                    
                        <asp:TemplateColumn>
                            <ItemStyle Width="20px" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn HeaderText="序号">
                            <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundColumn>

                        <asp:BoundColumn HeaderText="姓名" DataField="FHumanName">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="性别" DataField="FSex">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="身份证号" DataField="FIdCard">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                         <%--<asp:BoundColumn HeaderText="注册专业" DataField="ZCZY">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>--%>
                         <asp:BoundColumn HeaderText="企业角色" DataField="FentType">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                         <asp:BoundColumn HeaderText="人员类型" DataField="EmpType">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                         <asp:BoundColumn HeaderText="工程名称" DataField="ProjectName">
                            <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                         <asp:BoundColumn HeaderText="开工时间" DataField="StartDate"  DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                         <asp:BoundColumn HeaderText="预计竣工时间" DataField="EndDate"  DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="职务" DataField="ZW" Visible="False">
                            <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="padLeft" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>

                        <asp:TemplateColumn HeaderText="锁定次数">
                            <HeaderTemplate>                               
                                <asp:Label ID="lbl_lockcount" runat="server"></asp:Label>                                    
                            </HeaderTemplate>
                        </asp:TemplateColumn>

                        
                       

                    
                        <asp:BoundColumn HeaderText="FId" DataField="FId" Visible="False">
                            <ItemStyle Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                    </Columns> 
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:DataGrid>
           <%-- </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>

       <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="500"
            AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div style="text-align: center">
                    数据加载中，请稍后。。。
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>


        <div class="d div1 tcen" style="width: 98%; margin: 0px auto;">
            <uc1:pager ID="Pager1" runat="server"></uc1:pager>
        </div>
        
        <input id="t_fLinkId" runat="server" type="hidden" />
        <input id="t_PrjItemId" runat="server" type="hidden" />
        <input id="HIsPostBack" runat="server" type="hidden" />
    </form>
</body>
</html>
