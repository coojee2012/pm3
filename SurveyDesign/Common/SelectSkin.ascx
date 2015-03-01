<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectSkin.ascx.cs" Inherits="Common_SelectSkin" %>
风格：<asp:DropDownList ID="dro_Style" CssClass="m_txt" runat="server" onchange="changeCSS(this.value)">
</asp:DropDownList>

<script type="text/javascript">
    function changeCSS(value) {
        $("#skinLink").attr("href", "../../Skin/" + value + "/main.css");
        try {
            $.each(parent.frames, function() {
                $("#skinLink", this.document).attr("href", "../../Skin/" + value + "/main.css");
                $.each(this.frames, function() {
                    $("#skinLink", this.document).attr("href", "../../Skin/" + value + "/main.css");
                });
            });
        }
        catch (e)
        { }
        setCookie("_SYS_QS_SKINNAME", value);
    }
    function setCookie(name, value) {
        var argv = setCookie.arguments;
        var argc = setCookie.arguments.length;
        var exp = (argc > 2) ? argv[2] : 90;
        var path = (argc > 3) ? argv[3] : null;
        var domain = (argc > 4) ? argv[4] : null;
        var secure = (argc > 5) ? argv[5] : false;
        var expires = new Date();
        expires.setTime(expires.getTime() + (exp * 24 * 60 * 60 * 1000));
        document.cookie = name + "=" + value +
    "; expires=" + expires.toGMTString() +
    ((domain === null) ? "" : ("; domain=" + domain)) + "; path=/" +
    ((secure === true) ? "; secure" : "");
    } 
</script>

