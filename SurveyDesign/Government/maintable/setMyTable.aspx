<%@ Page Language="C#" AutoEventWireup="true" CodeFile="setMyTable.aspx.cs" Inherits="Government_maintable_setMyTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>定置我的桌面</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("a[id^=a]").hover(function() {
                if ($(this).attr("class") != "a tab_btn1")
                    $(this).attr("class", "tab_btn1")
            },
                function() {
                    if ($(this).attr("class") != "a tab_btn1")
                        $(this).attr("class", "tab_btn")
                });
            $("a[id^=a]").click(function() {
                $("#hidd_n").val($(this).attr("id").substring(1, 2));
                $("#btn_show").click();
            });
        });
        //显示table
        function showtb(n) {

            $(".tab_btn1").attr("class", "tab_btn");
            $("a[id^=a" + n + "]").attr("class", "a tab_btn1").blur();

            $("table[id^='tb_']").hide();
            $("table[id=tb_" + n + "]").fadeIn(600);
        }
    </script>

    <style type="text/css">
        .btn_up
        {
            width: 20px;
            padding: 0px;
            text-align: center;
        }
    </style>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="tabBar" style="width: 98%; margin: 3px auto 0 auto;">
        <div class="tabBar_l">
        </div>
        <a id="a1" class="tab_btn1"><strong>板块自定义</strong> </a><a id="a2" class="tab_btn"><strong>
            桌面背景</strong> </a>
        <div class="tabBar_r">
        </div>
    </div>
    <table id="tb_1" class="m_table" width="98%" align="center" style="margin-top: 0px;">
        <tr>
            <td class="t_l t_bg">
                <b>自定义桌面 - 桌面左则</b>
            </td>
        </tr>
        <tr>
            <td style="padding: 0px;">
                <div style="height: 20px; margin: 0px; color: #444444; text-align: center; font-weight: bold;
                    line-height: 20px;" class="t_bg">
                    <div style="width: 44%; height: 20px; float: left; border-right: 1px solid #BFBFBF;">
                        显示以下桌面模块
                    </div>
                    <div style="float: left; width: 10%; height: 20px;">
                    </div>
                    <div style="width: 44%; height: 20px; float: left; border-left: 1px solid #BFBFBF;">
                        备选桌面模块
                    </div>
                </div>
                <div style="border-top: 1px solid #BFBFBF; height: 150px; text-align: center;">
                    <div style="width: 6%; height: 150px; float: left; border-right: 1px solid #BFBFBF;">
                        <div style="height: 50px;">
                            <br />
                            排<br />
                            序
                        </div>
                        <div style="height: 50px;">
                            <asp:Button ID="btn1_up" runat="server" Text="↑" CssClass="btn_up" OnClick="btn1_up_Click" />
                        </div>
                        <div style="height: 50px;">
                            <asp:Button ID="btn1_down" runat="server" Text="↓" CssClass="btn_up" OnClick="btn1_down_Click" />
                        </div>
                    </div>
                    <div style="width: 38%; height: 150px; float: left; border-right: 1px solid #BFBFBF;">
                        <asp:ListBox ID="list_mylefttable" runat="server" Width="98%" Height="98%" SelectionMode="Multiple">
                        </asp:ListBox>
                    </div>
                    <div style="float: left; width: 10%; height: 150px;">
                        <div style="height: 30px; margin-top: 40px;">
                            <asp:Button ID="btn1_del" runat="server" Text=">>" CssClass="m_btn_w2" OnClick="btn1_del_Click" />
                        </div>
                        <div style="height: 30px; margin-top: 10px;">
                            <asp:Button ID="btn1_add" runat="server" Text="<<" CssClass="m_btn_w2" OnClick="btn1_add_Click" />
                        </div>
                    </div>
                    <div style="width: 44%; height: 150px; float: left; border-left: 1px solid #BFBFBF;">
                        <asp:ListBox ID="list_table1" runat="server" Width="98%" Height="98%" SelectionMode="Multiple">
                        </asp:ListBox>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td class="t_c">
                <div>
                    点击条目时，可以组合CTRL或SHIFT键进行多选
                </div>
                <div>
                    <asp:Button ID="btn1_save" runat="server" Text="保存" CssClass="m_btn_w2" OnClick="btn1_save_Click" />
                    &nbsp;
                    <asp:Button ID="btn1_reset" runat="server" Text="还原" CssClass="m_btn_w2" OnClick="btn1_reset_Click"
                        ToolTip="在未保存前点击可还原到开始的状态" />
                </div>
            </td>
        </tr>
    </table>
    <table id="tb_2" class="m_table" width="98%" align="center" style="margin-top: 0px;">
        <tr>
            <td class="t_l t_bg">
                <b>自定义桌面 - 桌面背景</b>
            </td>
        </tr>
        <tr>
            <td class="t_c">
                <input id="File1" type="file" runat="server" class="m_txt" style="width: 160px;" />
                请上传jpg或gif格式的图片
            </td>
        </tr>
        <tr>
            <td class="t_c">
                <asp:Button ID="btnGetImg" runat="server" Text="确定" CssClass="m_btn_w2" OnClick="btnGetImg_Click" />
                &nbsp;<asp:Button ID="btnDelImg" runat="server" Text="清除" CssClass="m_btn_w2" ToolTip="点击可清除现有背景图片"
                    OnClick="btnDelImg_Click" />
            </td>
        </tr>
    </table>
    <input id="hidd_n" type="hidden" runat="server" value="1" />
    <asp:Button ID="btn_show" runat="server" Text="" OnClick="btn_show_Click" Style="display: none;" />
    </form>
</body>
</html>
