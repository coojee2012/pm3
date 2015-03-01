<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="EntApprove_main_Message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/Govdept.js" language="javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>

    </style> <title>建设厅行政审批系统</title>
</head>
<body>
    <form id="form1" runat="server">
    <%--<table height="100%" width="100%">
            <tr>
                <td valign="top" align="center">
                    <table width="734" class="martop1">
                        <tr>
                            <td background="../gzimage/index-12.jpg" height="13" width="723">
                            </td>
                            <td background="../gzimage/index-11.jpg" width="11">
                            </td>
                        </tr>
                    </table>
                    <table width="734" align="center">
                        <tr>
                            <td background="../gzimage/index-13.jpg" height="17" width="27">
                            </td>
                            <td background="../gzimage/index-15.jpg" width="262">
                            </td>
                            <td align="center" class="txt3" width="157">
                                文件通知</td>
                            <td background="../gzimage/index-15.jpg" width="262">
                            </td>
                            <td background="../gzimage/index-14.jpg" width="26">
                            </td>
                        </tr>
                        <tr height="10px">
                            <td colspan="5">
                            </td>
                        </tr>
                    </table>
                   
                   <div style="width:100%; height:460px; overflow-y:scroll">
                    <asp:DataGrid ID="DG_Apply" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                        Width="700px" CssClass="cGrid1" Style="margin-top: 7px;" OnItemDataBound="DG_Apply_ItemDataBound">
                        <ItemStyle CssClass="cGridItem1" />
                        <AlternatingItemStyle CssClass="cGridAlterItem1" />
                        <HeaderStyle CssClass="td6 txt6" />
                        <Columns>
                            <asp:BoundColumn HeaderText="序号">
                                <ItemStyle Width="40px"/>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fname" HeaderText="标题">
                                <ItemStyle  CssClass="txt21" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FPubDepart" HeaderText="发送单位">
                                <ItemStyle  HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FPubTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="发送日期">
                                
                            </asp:BoundColumn>
                            
                           

                            <asp:BoundColumn DataField="FFileNote"  Visible="False"></asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="FWebId"  Visible="False"></asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="FOperType"  Visible="False"></asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="fid" HeaderText="fid" Visible="False"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>--%>
    <table height="95%" width="98%" align="center" style="margin-top: 10px; margin-bottom: 10px;">
        <tr>
            <td class="wxts_top_l">
            </td>
            <td class="wxts_top">
            </td>
            <td class="wxts_top_r">
            </td>
        </tr>
        <tr>
            <td class="wxts_l">
            </td>
            <td class="wxts_m">
                <div class="wxts_title">
                    文件通知
                </div>
                <div style="padding-left: 40px; padding-right: 40px; line-height: 30px; text-align: center;">
                </div>
                <div id="appTab" runat="server">
                    <div>
                        <asp:DataGrid ID="DG_Apply" runat="server" AutoGenerateColumns="False" HorizontalAlign="Left"
                            Width="100%" CssClass="m_dg1" Style="margin-top: 7px;" OnItemDataBound="DG_Apply_ItemDataBound">
                            <ItemStyle CssClass="m_dg1_i" />
                            <HeaderStyle CssClass="m_dg1_h" />
                            <Columns>
                                <asp:BoundColumn HeaderText="序号">
                                    <ItemStyle Width="40px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FName" HeaderText="标题">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FPubDepart" HeaderText="发布单位">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FPubTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="发布日期">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FID" Visible="False"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>
            </td>
            <td class="wxts_r">
            </td>
        </tr>
        <tr>
            <td class="wxts_bot_l">
            </td>
            <td class="wxts_bot">
            </td>
            <td class="wxts_bot_r">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
