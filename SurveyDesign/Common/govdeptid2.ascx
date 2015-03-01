<%@ Control Language="c#" Inherits="Approve.Common.GovDeptId2" CodeFile="govdeptid2.ascx.cs" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:UpdatePanel ID="up_govdeptid2" runat="server" RenderMode="Inline">
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
