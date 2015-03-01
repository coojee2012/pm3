<%@ Control Language="C#" AutoEventWireup="true" CodeFile="online.ascx.cs" Inherits="Common_online" %>

<script type="text/javascript" src="../script/jquery.js"></script>

<script type="text/javascript">
    var win = null;
    function openChat(URL, key, winid, ip) {
        if (!win || win.closed) {
            escape()
            win = window.open(URL + "?SID=" + escape(key) + "&type=3&FName=" + ip, winid.replace(/-/g, "_"), "height=605,titlebar=no, width=845, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
            win.focus();
        }
        else {
            win.focus();
        }
    }

    $(document).ready(function() {
        //子菜单展开和隐藏
        $(".onli_groupTop").click(function() {//一级
            $(this).next("div").first().slideToggle(300);
            if ($(this).attr("class") == "onli_groupTop_h")
                $(this).attr("class", "onli_groupTop");
            else
                $(this).attr("class", "onli_groupTop_h");
        });
    });
</script>

<style type="text/css">
    /*分组栏*展开*/.onli_groupTop
    {
        height: 23px;
        line-height: 23px;
        margin-top: 2px;
        cursor: pointer;
        background: #FFFFFF;
    }
    .onli_groupTop span
    {
        display: block;
        float: left;
        width: 18px;
        height: 20px;
        background: url(../skin/orange/image/online_02.gif) center no-repeat;
    }
    .onli_groupTop div
    {
    	margin-left:3px;
        float: left;
        height: 23px;
        line-height: 23px;
        font-size: 12px;
    }
    .onli_groupTop:hover
    {
        height: 23px;
        line-height: 23px;
        background: url(../skin/orange/image/online_04.gif);
    }
    /*分组栏*关闭*/.onli_groupTop_h
    {
        height: 23px;
        line-height: 23px;
        margin-top: 2px;
        cursor: pointer;
        background: #FFFFFF;
    }
    .onli_groupTop_h span
    {
        display: block;
        float: left;
        width: 18px;
        height: 20px;
        background: url(../skin/orange/image/online_01.gif) center no-repeat;
    }
    .onli_groupTop_h div
    {
    	margin-left:3px;
        float: left;
        height: 23px;
        line-height: 23px;
        font-size: 12px;
    }
    .onli_groupTop_h:hover
    {
        height: 23px;
        line-height: 23px;
        background: url(../skin/orange/image/online_04.gif);
    }
    /*用户列表*/.onli_emp
    {
        height: 28px;
        line-height: 28px;
        margin-left: 4px;
        font-size: 12px;
        margin-right: 4px;
        text-align: left;
    }
    .onli_emp:hover
    {
        background: #C4E3F8;
    }
    .onli_emp span
    {
        display: block;
        width: 28px;
        height: 20px;
        float: left;
        padding-top: 4px;
        margin-left: 2px;
    }
    .onli_emp span img
    {
        height: 18px;
        width: 20px;
        border: none;
    }
    .onli_emp a
    {
        display: block;
        float: left;
        height: 28px;
        line-height: 28px;
        color: #000000;
        text-decoration: none;
    }
</style>
<asp:ScriptManager ID="ScriptManager2" runat="server">
</asp:ScriptManager>
<asp:Repeater ID="rep" runat="server" OnItemDataBound="rep_OnItemDataBound">
    <ItemTemplate>
        <div class="onli_groupTop">
            <span>
                <asp:HiddenField ID="lit_Type" runat="server" Value='<%#Eval("FNumber") %>' />
            </span>
            <div>
                <%#Eval("FName") %>
                <asp:Literal ID="lit_count" runat="server"></asp:Literal>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="rep_updatePane2" runat="server">
                <ContentTemplate>
                    <asp:Literal ID="lit_online" runat="server"></asp:Literal>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:Timer ID="Timer2" runat="server" OnTick="Timer2_Tick" Interval="10000">
</asp:Timer>
