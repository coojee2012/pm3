<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stat.aspx.cs" Inherits="Government_AppZLJDBA_Stat" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Common/pager.ascx" TagName="pager" TagPrefix="uc1" %>
<%@ Register Src="../../common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>质量监督备案接件</title>
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

    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>

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
                        <asp:Literal ID="lPostion" runat="server">质量监督备案统计</asp:Literal>
                    </th>
                </tr> 
                <tr>
                    <td class="t_r">
                        备案时间起：
                    </td>
                    <td>
                        <asp:TextBox ID="txtSDate" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                    </td>
                    <td class="t_r">
                        备案时间止：
                    </td>
                    <td>
                        <asp:TextBox ID="txtEDate" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>                                          
                    </td>
                    <td  style="text-align: center; padding-right: 10px">
                        <asp:Button ID="btnQuery" runat="server" CssClass="m_btn_w2" OnClick="btnQuery_Click"
                            Text="查询" />
                        &nbsp;
                        <input id="btnClear" class="m_btn_w2" style="margin-top: 3px;" type="button" value="重置"
                            onclick="clearPage();" />
                    </td>
                </tr>
            </table>  
            <asp:GridView ID="gv_stat" runat="server" CssClass="m_dg1" Width="98%" HorizontalAlign="Center"
                    AutoGenerateColumns="False" OnRowDataBound="gv_stat_RowDataBound"
                    EmptyDataText="没有数据" EnableModelValidation="True" OnRowCommand="gv_stat_RowCommand" OnRowCreated="gv_stat_RowCreated">
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
                        <asp:BoundField HeaderText="属地" DataField="AddressDeptName" />
                        <asp:TemplateField HeaderText="市政基础设施工程">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SZJCSS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnItemSee1" runat="server" CommandName="See1" autopostback="true" Text='<%# Bind("SZJCSS") %>' CommandArgument='<%#Eval("AddressDept")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="房屋建筑工程">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FWJZGC") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnItemSee2" runat="server" CommandName="See2" autopostback="true" Text='<%# Bind("FWJZGC") %>' CommandArgument='<%#Eval("AddressDept")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="改造工程（装饰）">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Other") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnItemSee3" runat="server" CommandName="See3" autopostback="true" Text='<%# Bind("Other") %>' CommandArgument='<%#Eval("AddressDept")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="合计">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("HJ") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnItemSeeAll" runat="server" CommandName="SeeAll" autopostback="true" Text='<%# Bind("HJ") %>' CommandArgument='<%#Eval("AddressDept")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            <webdiyer:AspNetPager ID="Pager1" runat="server" width="98%" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到"></webdiyer:AspNetPager>
     
   
        </asp:Panel>
    
        <asp:Panel ID="DetailPanel" runat="server" Visible="False" >
            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="5">
                        <asp:Literal ID="Literal1" runat="server">质量监督备案明细</asp:Literal>
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
                        项目名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjectName"  runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>                                          
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
                        工程性质：
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="p_PrjItemType" runat="server"  CssClass="m_txt" DataSourceID="SqlDataSource1" DataTextField="FName" DataValueField="FNumber" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbCenter %>" SelectCommand="SELECT '-1' [FNumber], '   ' [FName] union SELECT [FNumber], [FName] FROM [CF_Sys_Dic] WHERE ([FParentId] = @FParentId)">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="20001" Name="FParentId" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td class="t_r">
                        属地：
                    </td>
                    <td class="auto-style1">
                        <uc1:govdeptid ID="govd_FRegistDeptId" runat="server" />   
                    </td>
                    
                </tr>
                <tr>
                    <td class="t_r">
                        备案编号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecordNo"  runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                    </td>
                    <td class="t_r">
                        建设单位：
                    </td>
                    <td>
                        <asp:TextBox ID="txtJSDW"  runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>                                          
                    </td>
                    
                </tr>
                <tr>
                    <td class="t_r">
                        备案时间起：
                    </td>
                    <td>
                        <asp:TextBox ID="txtSDate1" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>
                    </td>
                    <td class="t_r">
                        备案时间止：
                    </td>
                    <td>
                        <asp:TextBox ID="txtEDate1" onfocus="WdatePicker()" runat="server" CssClass="m_txt" Width="169px"></asp:TextBox>                                          
                    </td>
                    
                </tr>
            </table>
            <div style="width:98%;margin-left: auto;margin-right: auto;">
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
                        <asp:BoundField HeaderText="属地" DataField="AddressDeptName" />
                        <asp:TemplateField HeaderText="工程名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PrjItemName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnItemSee" runat="server" CommandName="See" autopostback="true" Text='<%# Bind("PrjItemName") %>' CommandArgument='<%#Eval("FAppId")%>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="项目名称" DataField="ProjectName" />
                        <asp:BoundField HeaderText="备案编号" DataField="RecordNo" />
                        <asp:BoundField HeaderText="建设单位" DataField="JSDW" />
                        <asp:BoundField HeaderText="备案时间" DataField="FAppDate" />                       
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
