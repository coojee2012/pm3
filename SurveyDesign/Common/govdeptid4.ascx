<%@ Control Language="c#" Inherits="Approve.Common.GovDeptId4" CodeFile="govdeptid4.ascx.cs" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<div style="display: none;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</div>
<asp:UpdatePanel ID="up_govdeptid4" runat="server" RenderMode="Inline">
    <contenttemplate>
        <asp:DropDownList ID="FProvince" runat="server"   AutoPostBack="True"
            CssClass="dropDown" OnSelectedIndexChanged="FProvince_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="FCity" runat="server" AutoPostBack="True"  
            CssClass="dropDown" OnSelectedIndexChanged="FCity_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:DropDownList ID="FCountry" runat="server" CssClass="dropDown" onselectedindexchanged="FCountry_SelectedIndexChanged">
        </asp:DropDownList>
    </contenttemplate>
</asp:UpdatePanel>
