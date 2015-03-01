<%@ Control Language="C#" AutoEventWireup="true" CodeFile="t1.ascx.cs" Inherits="Government_maintable_t1" %>
<style type="text/css">
    .list_div_top
    {
        width: 600px;
        margin: 0 auto;
        height: 40px;
        line-height: 40px;
    }
    .list_div_top div
    {
        height: 40px;
        line-height: 40px;
        width: 80px;
        float: left;
        text-align: center;
    }
</style>

<script type="text/javascript">
    $(document).ready(function() {
        //文本框样式
        txtCss();
        DynamicGrid(".m_dg1_i");

        $("a[id^=a]").hover(function() {
            if ($(this).attr("class") != "a tab_btn1")
                $(this).attr("class", "tab_btn1")
        },
                function() {
                    if ($(this).attr("class") != "a tab_btn1")
                        $(this).attr("class", "tab_btn")
                });
        $("a[id^=a]").click(function() {
            $("input[id$=hidd_n]").val($(this).attr("id").substring(1, 8));
            showtb($("input[id$=hidd_n]").val());
        });
        showtb("1");
    });
    //显示table
    function showtb(n) {
        $(".tab_btn1").attr("class", "tab_btn");
        $("a[id=a" + n + "]").attr("class", "a tab_btn1").blur();

        $("table[id^=tb_]").hide();
        $("table[id=tb_" + n + "]").fadeIn(800);

        $("#title_b").html($("a[id=a" + n + "]").html());
    }
</script>

<div id="module_1" style="position: relative; padding-bottom: 10px;">
    <table class="TableBlock" width="100%" cellspacing="0" cellpadding="1">
        <tr class="TableHeader">
            <td id="module_1_head" width="100%">
                &nbsp;业务办理情况综合统计
            </td>
        </tr>
        <tr class="TableData">
            <td colspan="2" style="padding-left: 10px; padding-right: 10px; background: #FFFFFF;
                padding-bottom: 10px;" valign="top">
                <div class="tabBar" style="width: 98%; margin: 3px auto 0 auto;">
                    <div class="tabBar_l">
                    </div>
                    <a id="a1" class="tab_btn1"><strong>总体业务情况</strong> </a>
                    <asp:Repeater ID="rep_AppType" runat="server">
                        <ItemTemplate>
                            <a id='a<%#Eval("FNumber") %>' class="tab_btn"><strong>
                                <%#Eval("FName") %></strong> </a>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="tabBar_r">
                    </div>
                </div>
                <table id="tb_1" class="m_table" width="98%" align="center" style="margin-top: 0px;">
                    <tr>
                        <td class="t_l t_bg" colspan="5">
                            &nbsp;&nbsp;<b>总体业务情况</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r" style="width: 100px;">
                            待审核事项：
                        </td>
                        <td>
                            <asp:Literal ID="lit_Count" runat="server"></asp:Literal>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:Repeater ID="rep_Table" runat="server" OnItemDataBound="rep_Table_ItemDataBound">
                    <ItemTemplate>
                        <table id='tb_<%# Eval("FNumber")%>' class="m_table" width="98%" align="center" style="margin-top: 0px;">
                            <tr>
                                <td class="t_l t_bg">
                                    &nbsp;&nbsp;<b><%# Eval("FName")%></b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="list_div_top" style="border-bottom: 1px solid #000000;">
                                        <div>
                                            <b>审核步骤</b>
                                        </div>
                                        <div>
                                            <b>今日上报</b>
                                        </div>
                                        <div>
                                            <b>今日办理</b>
                                        </div>
                                        <div>
                                            <b>本周上报</b>
                                        </div>
                                        <div>
                                            <b>本周办理</b>
                                        </div>
                                        <div>
                                            <b>总办理</b>
                                        </div>
                                        <div>
                                            <b>总未办理</b>
                                        </div>
                                    </div>
                                    <div class="list_div_top">
                                        <asp:Repeater ID="rep_appCount" runat="server">
                                            <ItemTemplate>
                                                <div>
                                                    <b>
                                                        <%#Eval("stepName") %>
                                                    </b>
                                                </div>
                                                <div>
                                                    <%#Eval("DayUpCount") %>
                                                </div>
                                                <div>
                                                    <%#Eval("DayAppCount")%>
                                                </div>
                                                <div>
                                                    <%#Eval("WeekUpCount")%>
                                                </div>
                                                <div>
                                                    <%#Eval("WeekAppCount")%>
                                                </div>
                                                <div>
                                                    <%#Eval("AppCount") %>
                                                </div>
                                                <div>
                                                    <%#Eval("NoAppCount")%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                <input id="hidd_n" type="hidden" runat="server" value="1" />
                <asp:Button ID="btn_show" runat="server" Text="" OnClick="btn_show_Click" Style="display: none;" />
            </td>
        </tr>
    </table>
</div>
