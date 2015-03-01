<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePage.aspx.cs" Inherits="Share_WebSide_ManagePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../style/jxmain.css" rel="stylesheet" type="text/css" />

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        //登陆
        function login(str, sysId, url) {
            $("#hidd_FID").val(str);
            $("#hidd_sysId").val(sysId);
            $("#hidd_Url").val(url);
            $("#btnLogin").click();
        }
    </script>

    <title>管理门户</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="header" style="margin-left: auto; margin-right: auto; height: auto; width: 960px">
        <div class="banner1" width="100%" border="0" align="center">
        </div>
        <div class="top_1">
            <div class="top_pic">
            </div>
            <div class="top_font top_2 ">
                您好：<font color="#2594EE"><asp:Label ID="lab_FName" runat="server" Text=""></asp:Label></font></div>
            <div class="top_3 top_font">
                本次登陆时间：<%=DateTime.Now %></div>
            <div class="top_3 top_font">
                上次登陆时间：2011-01-01&nbsp;12:30:25</div>
            <div class="top_5">
            </div>
            <div class="top_4 top_font">
                登录次数：25次&nbsp;&nbsp;&nbsp;退出</div>
        </div>
        <div class="total">
            <div style="width: 713px; height: 389px; float: left;">
                <div class="total_le">
                    <div style="width: 260px; height: 29px;">
                        <div class="blue_t">
                            <div class="menu">
                                通知公告</div>
                        </div>
                        <div class="menu_L">
                            <div class="menu_M">
                                <div class="menu_font">
                                    更多>></div>
                            </div>
                        </div>
                    </div>
                    <div style="width: 258px; height: 200px; border-left: solid 1px #cccccc;">
                        <div class="tree">
                            <div class="point">
                                <div class="inside_font">
                                    &nbsp;<img src="../image/point.jpg" />&nbsp;<a href="#">公司再次承载舟山中海轴承接船</a>
                                </div>
                            </div>
                            <div class="point">
                                <div class="inside_font">
                                    &nbsp;<img src="../image/point.jpg" />&nbsp;<a href="#">公司中标承接再次承载舟山中轴承接船</a>
                                </div>
                            </div>
                            <div class="point">
                                <div class="inside_font">
                                    &nbsp;<img src="../image/point.jpg" />&nbsp;<a href="#">公司再次承载舟山中海轴承接船</a>
                                </div>
                            </div>
                            <div class="point">
                                <div class="inside_font">
                                    &nbsp;<img src="../image/point.jpg" />&nbsp;<a href="#">公司再次承载舟山中海轴承接船</a>
                                </div>
                            </div>
                            <div class="point">
                                <div class="inside_font">
                                    &nbsp;<img src="../image/point.jpg" />&nbsp;<a href="#">公司再次承载舟山中海轴承接船</a>
                                </div>
                            </div>
                            <div class="point">
                                <div class="inside_font">
                                    &nbsp;<img src="../image/point.jpg" />&nbsp;<a href="#">公司再次承载舟山中海轴承接船</a>
                                </div>
                            </div>
                            <div class="point">
                                <div class="inside_font">
                                    &nbsp;<img src="../image/point.jpg" />&nbsp;<a href="#">公司再次承载舟山中海轴承接船</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="middle_menu">
                    <div style="width: 445px; height: 29px;">
                        <div class="blue_t">
                            <div class="menu">
                                查询服务</div>
                        </div>
                        <div class="middle_menu_L">
                            <div class="middle_menu_M">
                            </div>
                        </div>
                    </div>
                    <div style="width: 443px; height: 200px; border-left: solid 1px #cccccc;">
                        <div style="margin-left: 4px; margin-top: 6px;" class="cCursor">
                            <a href="#">
                                <img src="../image/menu_big1.jpg" border="0" />
                            </a>
                        </div>
                    </div>
                </div>
                <div class="down">
                    <div style="width: 259px; height: 29px;">
                        <div class="blue_longer">
                            <div class="menu">
                                服务与互动管理</div>
                        </div>
                        <div class="middle_menu_LR">
                            <div class="middle_menu_LM">
                                <div class="menu_font">
                                    更多>></div>
                            </div>
                        </div>
                    </div>
                    <div style="width: 258px; height: 122px; border-left: solid 1px #cccccc;">
                        <div class="cot">
                            <div class="cot_gcjs">
                                <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/Share/image/cot_ktxt.jpg"
                                    NavigateUrl="#" Target="_blank"></asp:HyperLink></div>
                            <div class="cot_xgmm">
                                <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl="~/Share/image/cot_xgmm.jpg"
                                    NavigateUrl="#" Target="_blank"></asp:HyperLink></div>
                            <div class="cot_zxfw">
                                <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/Share/image/cot_zxfw.jpg"
                                    NavigateUrl="#" Target="_blank"></asp:HyperLink></div>
                        </div>
                    </div>
                </div>
                <div class="log">
                    <div style="width: 444px; height: 29px;">
                        <div class="blue_t">
                            <div class="menu">
                                待办事项</div>
                        </div>
                        <div class="sin">
                            <div class="cos">
                            </div>
                        </div>
                    </div>
                    <div style="width: 443px; height: 122px; border-left: solid 1px #cccccc;">
                        <div class="plant">
                            <div class="span ">
                                <div class="inside_font">
                                    &nbsp;&nbsp;<a href="#"><font color="#da7a30">[待受理]</font>有5家企业数据为受理，请及时办理及时办理</a></div>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="detail">立即办理</asp:LinkButton>
                            </div>
                            <div class="span ">
                                <div class="inside_font">
                                    &nbsp;&nbsp;<a href="#"><font color="#da7a30">[待初审]</font>有6家企业数据为已到了初审阶段，请及时办理</a>
                                </div>
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="detail">立即办理</asp:LinkButton>
                            </div>
                            <div class="span ">
                                <div class="inside_font">
                                    &nbsp;&nbsp;<a href="#"><font color="#da7a30">[待审核]</font>有3家企业数据为已到了审核阶段，请及时办理</a>
                                </div>
                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="detail">立即办理</asp:LinkButton>
                            </div>
                            <div class="span ">
                                <div class="inside_font">
                                    &nbsp;&nbsp;<a href="#"><font color="#da7a30">[待发证]</font>有12家企业数据为已到了待发证阶段，请及时办理</a>
                                </div>
                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="detail">立即办理</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="menu_R">
                <div style="width: 239px; height: 29px;">
                    <div class="blue_long">
                        <div class="menu">
                            应用系统列表</div>
                    </div>
                    <div class="menu2_R">
                        <div class="middle_menu_R">
                            <div class="menu_font">
                                更多>></div>
                        </div>
                    </div>
                </div>
                <div style="width: 238px; height: 360px; border-left: solid 1px #cccccc;">
                    <div class="long_1px">
                        <div class="tan" id="sys_List" runat="server">
                            <div>
                                <a href="">
                                    <img src="" border="0" alt="" />
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottom">
            <div class="bottom_font">
                技术支持：
                <asp:Literal ID="liC_TechSupport" runat="server"></asp:Literal>
                电话：
                <asp:Literal ID="liC_webtel" runat="server"></asp:Literal>
                <br />
                <br />
                <asp:Literal ID="liC_Developer" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <input id="hidd_Url" runat="server" type="hidden" />
    <input id="hidd_sysId" runat="server" type="hidden" />
    <input id="hidd_FID" runat="server" type="hidden" />
    <asp:Button ID="btnLogin" runat="server" Text="" OnClick="btnLogin_Click" Style="display: none;" />
    </form>
</body>
</html>
