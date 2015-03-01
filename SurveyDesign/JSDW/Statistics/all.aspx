<%@ Page Language="C#" AutoEventWireup="true" CodeFile="all.aspx.cs" Inherits="JSDW_Statistics_all" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目办理情况</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#che_showNO").change(function() {
                if ($(this).attr("checked")) $(".no").show(); else $(".no").hide();
            });
            $("input[id^=ch_]").change(function() {
                var v = $(this).val();
                if ($(this).attr("checked")) $("#tr_" + v).show(); else $("#tr_" + v).hide();
            });
        });

        function seePrj(FID) {
            showAddWindow('../../JSDW/appmain/AddPrjRegist.aspx?fid=' + FID, 900, 700);
        }
 
    </script>

    <base target="_self">
    </base>
    <style type="text/css">
        .x, .no, .empty { clear: both; padding: 4px; margin: 2px; line-height: 22px; cursor: pointer; }
        .x { background: #D0F0FF; border: 1px dashed #3199CE; color: #444444; width: 280px; }
        .x:hover { border: 1px dashed #FF0000; background: #FFFFFF; }
        .no { background: #CCCCCC; border: 1px dashed #666666; color: #444444; width: 280px; }
        .no:hover { border: 1px dashed #FF0000; }
        .x b, .no b { color: #3199CE; font-weight: bold; }
        .x span, .no span { color: #333333; }
        .x a, .no a { color: #3199CE; }
        .x a:hover, .no a:hover { color: #FF0000; text-decoration: underline; }
        .x s, .no s { color: #333333; text-decoration: none; }
        .xtable td { padding: 5px; border-bottom: 2px solid #C8EDFF; }
        .alte { background: #EEF8FF; }
        .empty { background: #BBBBBB; border: 1px dashed #FFFFFF; color: #FFFFFF; }
        /*左边业务步骤名*/.ti { width: 160px; }
        .ti span { display: block; background: url(../../image/tip.gif) 3px 3px no-repeat; height: 26px; padding-left: 20px; }
        .ti b { display: block; height: 26px; line-height: 26px; font-family: 楷体_GB2312; color: #378BB0; font-size: 14px; filter: Dropshadow(offx=0,offy=1,color=#CCCCCC) Dropshadow(offx=1,offy=1,color=#CCCCCC); text-shadow: 0px 2px 1px #CCCCCC,2px 2px 1px #CCCCCC; }
        input, label { cursor: pointer; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="2">
                <asp:Literal ID="lit_Title" runat="server" Text="工程业务流程图"></asp:Literal>
            </th>
        </tr>
        <tr runat="server" id="tr_his">
            <td class="t_r t_bg" width="15%">
                <tt>历次变更记录：</tt>
            </td>
            <td class="t_l">
                <asp:DropDownList ID="ddlHis" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHis_SelectedIndexChanged" TabIndex="10">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <input id="ch_0" type="checkbox" checked="checked" value="CBSJWT" /><label for="ch_0">项目初步设计</label>
                <input id="ch_1" type="checkbox" checked="checked" value="CBSJSC" /><label for="ch_1">初步设计文件审查</label>
                <input id="ch_2" type="checkbox" checked="checked" value="KCSMWT" /><label for="ch_2">项目勘察</label>
                <input id="ch_3" type="checkbox" checked="checked" value="KCWJSCWT" /><label for="ch_3">勘察文件审查</label>
                <input id="ch_4" type="checkbox" checked="checked" value="SGTWJBZWT" /><label for="ch_4">施工图设计文件编制</label>
                <input id="ch_5" type="checkbox" checked="checked" value="SGTSJWJSCWT" /><label for="ch_5">施工图设计文件审查</label>
            </td>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
                <asp:CheckBox ID="che_showNO" Checked="true" runat="server" Text="显示未办理成功的业务" />
            </td>
            <td class="t_r">
                <input type="button" id="btnReturn" class="m_btn_w2" value="关闭" onclick="window.close();" tabindex="0" runat="server" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="xtable" width="98%" align="center" style="margin: 4px auto; border: 1px solid #73B9F7;">
        <tr>
            <td class="ti">
                <span><b>项目登记</b></span>
            </td>
            <td>
                <div class="x f_l" onclick='seePrj("<%=ViewState["FID"]%>");' title="点击查看工程详情">
                    工程名称：<b><asp:Literal ID="lit_PrjName" runat="server"></asp:Literal></b>
                    <br />
                    建设单位：<s><asp:Literal ID="lit_JSDW" runat="server"></asp:Literal></s>
                </div>
            </td>
        </tr>
        <tr id="tr_CBSJWT" class="alte">
            <td class="ti">
                <span><b>项目初步设计</b></span>
            </td>
            <td>
                <asp:Repeater ID="rep_CBSJWT" runat="server" OnItemDataBound="rep_CBSJWT_ItemDataBound">
                    <ItemTemplate>
                        <div class='<%#EConvert.ToBool(Eval("isOld"))?"no":"x" %> f_l'>
                            <asp:Literal ID="lit_Content" runat="server"></asp:Literal>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="lit_CBSJWT" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr id="tr_CBSJSC">
            <td class="ti">
                <span><b>初步设计文件审查</b></span>
            </td>
            <td>
                <asp:Repeater ID="rep_CBSJSC" runat="server" OnItemDataBound="rep_CBSJSC_ItemDataBound">
                    <ItemTemplate>
                        <div class='<%#EConvert.ToBool(Eval("isOld"))?"no":"x" %> f_l'>
                            <asp:Literal ID="lit_Content" runat="server"></asp:Literal>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="lit_CBSJSC" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr id="tr_KCSMWT" class="alte">
            <td class="ti">
                <span><b>项目勘察</b></span>
            </td>
            <td>
                <asp:Repeater ID="rep_KCSMWT" runat="server" OnItemDataBound="rep_KCSMWT_ItemDataBound">
                    <ItemTemplate>
                        <div style="width: auto;" class='<%#EConvert.ToBool(Eval("isOld"))?"no":"x" %> f_l'>
                            <div>
                                <asp:Literal ID="lit_Content" runat="server"></asp:Literal>
                            </div>
                            <div>
                                <div style="clear: none; height: 116px;" class='<%#EConvert.ToBool(Eval("isOld"))?"no":"x" %> f_l'>
                                    <asp:Literal ID="lit_ContentKC" runat="server"></asp:Literal>
                                </div>
                                <div style="clear: none; height: 116px;" class='<%#EConvert.ToBool(Eval("isOld"))?"no":"x" %> f_l'>
                                    <asp:Literal ID="lit_ContentJZ" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="lit_KCSMWT" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr id="tr_KCWJSCWT">
            <td class="ti">
                <span><b>勘察文件审查</b></span>
            </td>
            <td>
                <asp:Repeater ID="rep_KCWJSCWT" runat="server" OnItemDataBound="rep_KCWJSCWT_ItemDataBound">
                    <ItemTemplate>
                        <div class='<%#EConvert.ToBool(Eval("isOld"))?"no":"x" %> f_l'>
                            <asp:Literal ID="lit_Content" runat="server"></asp:Literal>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="lit_KCWJSCWT" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr id="tr_SGTWJBZWT" class="alte">
            <td class="ti">
                <span><b>施工图设计文件编制</b></span>
            </td>
            <td>
                <asp:Repeater ID="rep_SGTWJBZWT" runat="server" OnItemDataBound="rep_SGTWJBZWT_ItemDataBound">
                    <ItemTemplate>
                        <div class='<%#EConvert.ToBool(Eval("isOld"))?"no":"x" %> f_l'>
                            <asp:Literal ID="lit_Content" runat="server"></asp:Literal>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="lit_SGTWJBZWT" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr id="tr_SGTSJWJSCWT">
            <td class="ti">
                <span><b>施工图设计文件审查</b></span>
            </td>
            <td>
                <asp:Repeater ID="rep_SGTSJWJSCWT" runat="server" OnItemDataBound="rep_SGTSJWJSCWT_ItemDataBound">
                    <ItemTemplate>
                        <div class='<%#EConvert.ToBool(Eval("isOld"))?"no":"x" %> f_l'>
                            <asp:Literal ID="lit_Content" runat="server"></asp:Literal>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="lit_SGTSJWJSCWT" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
