<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ValidatePayUser.ascx.cs"
    Inherits="Common_ValidatePayUser" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<span style="height: 0px; display: none;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="3000">
    </asp:Timer>
    <asp:HiddenField ID="hfFUserId" runat="server" />
    <asp:HiddenField ID="hfFCompany" runat="server" />
</span>
