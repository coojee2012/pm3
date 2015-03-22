<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stat.aspx.cs" Inherits="Government_AppAQJDBA_Stat" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>办理统计</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            DynamicGrid(".m_dg1_i");
            $("#btnBack").click(function () {
                $("#DetailPanel").hide();
                $("#StatPanel").show();
            });
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
                form1.btnQuery.click();
            }
        }
        function openWinNew(Url) {
            var newopen = window.open(Url, "", ",,,toolbar =no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no");
            if (newopen) {
                newopen.moveTo(0, 0);
                newopen.resizeTo(screen.width, screen.height - 30);
            }
        }

  

    </script>

    </head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="StatPanel" runat="server" >
            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="5">
                        
                        <asp:Label ID="lbTitle" runat="server" Text="施工许可证办理统计情况（截止统计日期）"></asp:Label>
                    </th>
                </tr> 
               
            </table>  
            <div style="width:98%;margin:0 auto;">
            <asp:Repeater ID="rep_List" runat="server"  OnItemCommand="rep_List_ItemCommand">
                <HeaderTemplate>
                    <table  Class="m_dg1" Width="100%" HorizontalAlign="Center" align="center" >
                        <tr Class="m_dg1_h">
                            <td rowspan="2" align="center">序号</td>
                            <td rowspan="2" align="center">属地</td>
                            <td colspan="3" align="center">初次办理</td>
                            <td colspan="3" align="center">延期办理</td>
                            <td colspan="3" align="center">变更办理</td>
                        </tr>
                        <tr Class="m_dg1_h">
                            <td>未上报</td>
                            <td>已上报</td>
                            <td>已审核</td>
                            <td>未上报</td>
                            <td>已上报</td>
                            <td>已审核</td>
                            <td>未上报</td>
                            <td>已上报</td>
                            <td>已审核</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="m_dg1_i">
                        <td>
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td class="t_l">
                           <a href='Stat.aspx?PrjAddressDept=<%#Eval("PrjAddressDept")%>'><%#Eval("SD")%></a> 
                        </td>
                        <td>
                           
                            <asp:LinkButton ID="LinkButton8" runat="server" CommandName="CCBL_WSB" CommandArgument='<%#"0|"+Eval("PrjAddressDept")%>'>
                                <%#Eval("CCBL_WSB")%>
                            </asp:LinkButton>
                        </td>
                        <td>
                            
                            <asp:LinkButton ID="LinkButton7" runat="server" CommandName="CCBL_YSB" CommandArgument='<%#"1|"+Eval("PrjAddressDept")%>'>
                                <%#Eval("CCBL_YSB")%>
                            </asp:LinkButton>
                        </td>
                        <td>
                            
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="CCBL_YSH" CommandArgument='<%#"6|"+Eval("PrjAddressDept")%>'>
                                <%#Eval("CCBL_YSH")%>
                            </asp:LinkButton>
                        </td>
                        <td>
                            
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="YQBL_WSB" CommandArgument='<%#"0|"+Eval("PrjAddressDept")%>'>
                                <%#Eval("YQBL_WSB")%>
                            </asp:LinkButton>
                        </td>
                        <td>
                         
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="YQBL_YSB" CommandArgument='<%#"1|"+Eval("PrjAddressDept")%>'>
                                <%#Eval("YQBL_YSB")%>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="YQBL_YSH" CommandArgument='<%#"6|"+Eval("PrjAddressDept")%>'>
                                <%#Eval("YQBL_YSH")%>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="BGBL_WSB" CommandArgument='<%#"0|"+Eval("PrjAddressDept")%>'>
                                <%#Eval("BGBL_WSB")%>
                            </asp:LinkButton>
                        </td>
                        <td>
                            
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="BGBL_YSB" CommandArgument='<%#"1|"+Eval("PrjAddressDept")%>'>
                                <%#Eval("BGBL_YSB")%>
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="linkBtn_BGBL_YSH" runat="server" CommandName="BGBL_YSH" CommandArgument='<%#"6|"+Eval("PrjAddressDept")%>'>
                                <%#Eval("CCBL_YSH")%>
                            </asp:LinkButton>
                            
                        </td>
                    </tr>  
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <webdiyer:AspNetPager ID="Pager1" runat="server" width="100%" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" HorizontalAlign="Center" CustomInfoTextAlign="Center"></webdiyer:AspNetPager>
     
            </div>
        </asp:Panel>
    
        <asp:Panel ID="DetailPanel" runat="server" Visible="False">
            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="5">
                        <asp:Literal ID="Literal1" runat="server">施工许可证明细</asp:Literal>
                    </th>
                </tr> 
                <tr>
                    <td class="t_r">
                        工程名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrjItemName"  runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                    </td>
                    <td class="t_r">
                        建设单位：
                    </td>
                    <td>
                        <asp:TextBox ID="txtJSDW"  runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>                                          
                    </td>
                    <td  rowspan="4" style="text-align: center; padding-right: 10px">
                        <asp:Button ID="btnQuery1" runat="server" CssClass="m_btn_w2" OnClick="btnQuery1_Click"
                            Text="查询" />
                        &nbsp;
                        <input id="Button2" class="m_btn_w2" style="margin-top: 3px;" type="button" value="重置"
                            onclick="clearPage();" />
                        <asp:Button ID="btnBack" runat="server" CssClass="m_btn_w2" OnClick="btnBack_Click"
                            Text="返回" />
                    </td>
                </tr>
                <tr>
                    <td class="t_r">
               业务类型：
            </td>
            <td >
                <asp:DropDownList ID="ddlMType" runat="server" CssClass="m_txt" Width="169px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="11223"  >初次办理</asp:ListItem>
                    <asp:ListItem Value="11224">延期办理</asp:ListItem> 
                    <asp:ListItem Value="11225">变更办理</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="t_r">
                状态：
            </td>
            <td>
               <asp:DropDownList ID="ddlState" runat="server" CssClass="m_txt" Width="169px">
                    <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                    <asp:ListItem Value="0"  >未上报</asp:ListItem>
                    <asp:ListItem Value="1">已上报</asp:ListItem> 
                    <asp:ListItem Value="6">已审核</asp:ListItem>
                </asp:DropDownList>
            </td>
                   
                    
                </tr>
                <tr>
                    
                     <td class="t_r">
                        工程所属地：
                    </td>
                    <td colspan="3">
                        <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />   
                    </td>
                    
                </tr>
            
            </table>
            <div style="width:98%;margin:0 auto;">
                <asp:GridView ID="gv_detail" runat="server" CssClass="m_dg1" Width="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" OnRowDataBound="gv_detail_RowDataBound"
                    EmptyDataText="没有数据" EnableModelValidation="True" OnRowCommand="gv_detail_RowCommand" OnRowCreated="gv_detail_RowCreated">
                    <HeaderStyle CssClass="m_dg1_h" />
                    <RowStyle CssClass="m_dg1_i" />
                    <EmptyDataRowStyle CssClass="m_dg1_i" />
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <asp:Label ID="lbautoid" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="工程名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PrjItemName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnItemSee" runat="server" CommandName="See" autopostback="true" Text='<%# Bind("PrjItemName") %>' CommandArgument='<%#Eval("FAppId")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField HeaderText="建设单位" DataField="JSDW" />
                        <asp:BoundField HeaderText="上报日期" DataField="FAppDate" />  
                        <asp:BoundField HeaderText="工程所属地" DataField="AddressDeptName" />
                        <asp:BoundField HeaderText="业务类型" DataField="YWLX" />
                        <asp:BoundField HeaderText="项目地址" DataField="Address" />
                       
                                             
                    </Columns>
                </asp:GridView>
                <webdiyer:AspNetPager ID="Pager2" runat="server" width="98%" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager2_PageChanging"
                    PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到"></webdiyer:AspNetPager>
            </div>
            
            
                       
        </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </form>
</body>
</html>
