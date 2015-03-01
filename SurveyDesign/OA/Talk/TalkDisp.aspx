<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TalkDisp.aspx.cs" Inherits="OA_Talk_TalkDisp"
    ValidateRequest="false" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript" language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script language="javascript">
        function clear() {
            document.getElementById("eWebEditor1").value = "";
        }
        function contentEdit() {
            document.getElementById("tr_eWebEditor").style.display = "";
            document.getElementById("tr_clickEdit").style.display = "none";
            document.getElementById("eWebEditor1").focus();
        }

    </script>

    <style type="text/css">
        .user { margin: 10px; }
        .user td { height: 22px; line-height: 22px; color: #333333; vertical-align: top; }
        .user td s { font-style: normal; color: #0061FE; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <a id="ttt" name="ttt"></a>
    <table align="center" class="m_title" width="98%">
        <tr>
            <th align="left">
                <asp:Label ID="t_BB" runat="server" Text="话题讨论过程"></asp:Label>
            </th>
        </tr>
    </table>
    <table align="center" class="disp_table" style="width: 98%; margin: 5px auto;">
        <tr>
            <th style="height: 36px; line-height: 36px; width: 160px; padding-left: 10px;">
                <img runat="server" id="Aimg" src="../../image/question1.gif" />
                <b style="color: #045EBF; font-size: 14px;">
                    <asp:Literal ID="Label_FState" runat="server" Text="讨论"></asp:Literal>
                </b>
            </th>
            <td>
                <b style="color: #201E1F; font-size: 14px; margin-left: 10px;">
                    <asp:Literal ID="Label_Name" runat="server"></asp:Literal>
                </b>
            </td>
            <td class="t_r" style="height: 36px; line-height: 36px; padding-right: 10px; white-space: nowrap;">
                <asp:Button ID="btn_GetOn" runat="server" CssClass="m_btn_w2" Text="提交" ToolTip="提交、公开此话题，以便大家来讨论"
                    OnClick="btn_GetOn_Click" Visible="false" />
                <asp:Button ID="btn_IsOK" runat="server" CssClass="m_btn_w4" Text="中止讨论" ToolTip="中止该话题的讨论"
                    OnClick="btn_IsOK_Click" Visible="false" />
                <asp:Button ID="btn_GetBack" runat="server" CssClass="backbtn" Text="◆返回列表" OnClick="btnGetBack_Click" />
            </td>
        </tr>
        <tr>
            <td class="disp_line_l">
            </td>
            <td class="disp_line_r">
            </td>
            <td class="disp_line_r">
            </td>
        </tr>
        <tr>
            <th align="left" style="color: #333333;">
                <div style="line-height: 32px; height: 32px; padding-left: 10px;" class="disp_dashed">
                    <img src="../../image/img01.gif" />
                    <b style="font-size: 14px;">
                        <asp:LinkButton ID="la_FName" runat="server" OnClick="Link_AppUserName_Click"></asp:LinkButton>
                    </b>
                </div>
            </th>
            <td colspan="2">
                <div style="margin: 0px 20px; line-height: 32px; height: 32px;" class="disp_dashed">
                    <div style="float: left; color: #444444; background: url(../../image/time.gif) 1px 7px no-repeat;
                        padding-left: 25px;">
                        <span>提交时间：</span>
                        <asp:Literal ID="Label_AppTime" runat="server"></asp:Literal>
                        <span>联系方式：</span>
                        <asp:Literal ID="Label_FLinkWay" runat="server"></asp:Literal>
                        <span>所属板块：</span>
                        <asp:LinkButton ID="Link_projectName" runat="server" OnClick="Link_projectName_Click"
                            ToolTip="点击可以搜索此项目的问题"></asp:LinkButton>
                    </div>
                    <div style="float: right; color: #B70D0E;">
                        已有回复：
                        <asp:Literal ID="Label_FAnswerCount1" runat="server"></asp:Literal>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <th valign="top">
                <table class="user">
                    <asp:Literal ID="lit_CreateUserInfo" runat="server"></asp:Literal>
                </table>
            </th>
            <td colspan="2" style="padding: 4px 35px;" valign="top">
                <asp:Literal ID="Literal_Content" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="disp_line_l">
            </td>
            <td class="disp_line_r">
            </td>
            <td class="disp_line_r">
            </td>
        </tr>
        <asp:Repeater ID="dgList" runat="server" OnItemDataBound="dgList_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <th style="padding-left: 10px; font-size: 13px; height: 30px; line-height: 30px;"
                        class="disp_dashed">
                        <img src="../../image/img01.gif" />
                        <b style="color: #333333;">
                            <asp:Literal ID="Link_AppUserName" runat="server"></asp:Literal>
                            <asp:Literal ID="label_IsLZ" runat="server" Visible="false" Text="[楼主]"></asp:Literal>
                        </b>
                    </th>
                    <td style="color: #444444;" colspan="2">
                        <div style="margin: 0px 20px; height: 30px; line-height: 30px; padding-top: 10px"
                            class="disp_dashed">
                            <div style="float: left; color: #444444; background: url(../../image/Talk1.gif) 1px 7px no-repeat;
                                padding-left: 23px;">
                                回复时间：
                                <%#string.Format("{0:d}", Eval("FCreateTime"))%>
                            </div>
                            <div style="float: right;">
                                <asp:Literal ID="Label_L" runat="server" Text="#"></asp:Literal>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th valign="top">
                        <table class="user">
                            <asp:Literal ID="lit_UserInfo" runat="server"></asp:Literal>
                        </table>
                    </th>
                    <td colspan="2" style="padding: 4px 20px;" valign="top">
                        <%#Eval("FContent")%>
                    </td>
                </tr>
                <tr>
                    <td class="disp_line_l">
                    </td>
                    <td class="disp_line_r">
                    </td>
                    <td class="disp_line_r">
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <th>
            </th>
            <td align="right" style="height: 30px" colspan="2">
                <webdiyer:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CssClass="pages"
                    CurrentPageButtonClass="cpb" CustomInfoClass="pagescount" CustomInfoHTML="&lt;b&gt;共%RecordCount%条 第%CurrentPageIndex%/%PageCount%页&lt;/b&gt;"
                    CustomInfoSectionWidth="150px" FirstPageText="首页" LastPageText="尾页" LayoutType="Table"
                    NextPageText="下一页" NumericButtonCount="6" OnPageChanging="Pager1_PageChanging"
                    PageIndexBoxType="TextBox" PageSize="10" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
                </webdiyer:AspNetPager>
            </td>
        </tr>
        <tr>
            <td class="disp_line_l">
            </td>
            <td class="disp_line_r">
            </td>
            <td class="disp_line_r">
            </td>
        </tr>
        <tr>
            <th style="color: #333333; text-align: center;">
                &lt;问题解答 | 回复区 &gt;
            </th>
            <td style="height: auto; padding: 3px;" colspan="2">
                <div style="height: 256px;">
                    <input id="t_FContent" type="hidden" runat="server" />
                    <iframe id="eWebEditor1" src='../../eWebEditor/ewebeditor.htm?id=t_FContent&style=standard650&cusdir=<%= CurrentEntUser.EntId %>'
                        frameborder="0" scrolling="no" width="100%" height="250"></iframe>
                </div>
                <div style="height: 60px;">
                    <div class="f_l">
                        <span class="m_btn_b3"><i></i>
                            <asp:Button ID="btnAnswer" runat="server" Text="提交回复" OnClick="btnOK_Click" /><b></b></span>
                    </div>
                    <div class="f_l" style="margin-left: 10px; padding-top: 24px;">
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
