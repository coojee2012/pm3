<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aLeft.aspx.cs" Inherits="KcsjSgt_main_aLeft" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#showLeft").click(function() {//隐藏菜单
                $("#t_show").hide("fast", function() { parent.trleft.cols = "191,*"; $("#t_menu").show("slow"); });
            });
            $("#getLeft").click(function() {//显示菜单
                $("#t_menu").hide("slow", function() { parent.trleft.cols = "12,*"; $("#t_show").show("normal"); });
            });
            $("a[href]").click(function() { //点击切换样式
                if ($(this).attr("href") != "#") {
                    //记录最后点击的菜单
                    setCookie("_SYS_ENT_SGT_MENUID", $(this).attr("id"));
                }
            });
            $("a").css("cursor", "pointer");
            //跳转到cookie中的对应页面
            $("a[id=" + getCookie("_SYS_ENT_SGT_MENUID") + "]").click();
            var s = $("a[id=" + getCookie("_SYS_ENT_SGT_MENUID") + "]").attr("href");
            if (s)
                parent.main.location.href = s;


            //整理按钮样式
            $(".o_m01_1").each(function() {//一级
                if ($(this).find("+div").find(":parent").length > 0) {
                    $(this).find("+div:parent").hide();
                    $(this).attr("class", "o_m01_3");
                }
                else {
                    $(this).find("+div").hide();
                }
            });
            $(".o_m02_1").each(function() {//二级
                if ($(this).find("+div").find(":parent").length > 0) {
                    $(this).find("+div:parent").hide();
                    $(this).attr("class", "o_m02_3");
                }
            });
            //第有子菜单的展开
            $(".o_m01_3").find("+div:parent").first().show();
            //$(".o_m02_3").find("+div:parent").show();
            //子菜单展开和隐藏
            $(".o_m01_3").click(function() {//一级
                $(this).find("+div:parent").first().slideToggle("fast");
                if ($(this).attr("class") == "o_m01_3")
                    $(this).attr("class", "o_m01_4");
                else
                    $(this).attr("class", "o_m01_3");
                $(this).blur(); //转移焦点
            });
            $(".o_m02_3").click(function() {//二级
                $(this).find("+div:parent").first().slideToggle("fast");
                if ($(this).attr("class") == "o_m02_3")
                    $(this).attr("class", "o_m02_4");
                else
                    $(this).attr("class", "o_m02_3");
                $(this).blur(); //转移焦点
            });
            //点击没有子菜单
            $(".o_m01_1").click(function() {//一级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(this).attr("class", "o_m01_2");
                $(this).blur(); //转移焦点
            });
            $(".o_m02_1").click(function() {//二级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(this).attr("class", "o_m02_2");
                $(this).blur(); //转移焦点
            });
            $(".o_m03_1").click(function() {//三级
                $(".o_m01_2").attr("class", "o_m01_1");
                $(".o_m02_2").attr("class", "o_m02_1");
                $(".o_m03_2").attr("class", "o_m03_1");
                $(this).attr("class", "o_m03_2");
                $(this).blur(); //转移焦点
            });
        });
        function showMenu() {
            $("#t_show").hide("fast", function() { parent.trleft.cols = "191,*"; $("#t_menu").show("slow"); });
        }
        //屏蔽自定义title，将方法重写
        function tipAuto() {return false; }
        window.onload = function() { change(); }
        function change() {
            var one = document.getElementById("div_menu");
            one.style.height = window.document.body.clientHeight - 36;
        } 
    </script>

</head>
<body onresize="change();">
    <form id="form1" runat="server">
    <table id="t_menu" border="0" cellpadding="0" cellspacing="0" height="100%" class="l_table">
        <tr>
            <td class="l_top">
                <div class="f_l" style="width: 125px;">
                    <asp:Label ID="l_title" runat="server">系统菜单</asp:Label>
                </div>
            </td>
            <td rowspan="2" width="9px" id="getLeft">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="l_main" valign="top" align="left">
                <div class="l_main_bg" style="height: 100%; overflow: auto; width: 180px; text-align: center;"
                    id="div_menu">
                    <div class="o_menu">
                        <a id="a1" class="o_m01_1" href='../Main/zWXTS.aspx' target="main"><span>温馨提示</span></a>
                    </div>
                    <div class="o_menu">
                        <a id="a2" class="o_m01_1" href='../Main/Message.aspx' target="main"><span>文件通知</span></a>
                    </div>
                    <div class="o_menu">
                        <a id="a3" class="o_m01_1" target="main"><span>单位信息</span></a>
                        <div class="o_smenu">
                            <a id="a13" class="o_m02_1" href='../Main/Baseinfo1.aspx' target="main"><span>基本信息</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a28" class="o_m02_1" href='../Main/Baseinfo4.aspx' target="main"><span>证书信息</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a29" class="o_m02_1" target="main"><span>人员信息</span></a>
                            <div class="o_smenu2">
                                <a id="a22" class="o_m03_1" href='../Main/Baseinfo2.aspx' target="main"><span>注册人员信息</span></a>
                                <a id="a24" class="o_m03_1" href='../Main/Baseinfo3.aspx' target="main"><span>非注册人员信息</span></a>
                            </div>
                        </div>
                    </div>
                    <div class="o_menu">
                        <a id="a4" class="o_m01_1" style="cursor: pointer" target="main"><span>勘察文件审查</span></a>
                        <div class="o_smenu">
                            <a id="a41" class="o_m02_1" href='../AppMain/KCWJSClist.aspx' target="main"><span>勘察文件审查合同</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a18" class="o_m02_1" href='../AppMain/HTBAlist.aspx?type=287' target="main"><span>
                                合同备案</span></a>
                            <div class="o_smenu2">
                            </div>
                            <%-- <a id="a22" class="o_m02_1" href='../AppMain/HTBAlist.aspx?type=287&t=2' target="main">
                                <span>省外合同备案</span></a>
                            <div class="o_smenu2">
                            </div>--%>
                            <a id="a42" class="o_m02_1" href='../AppMain/KCCXXSClist.aspx' target="main"><span>程序性审查</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a43" class="o_m02_1" href='../AppMain/KCSCRYAPlist.aspx' target="main"><span>
                                人员安排</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a44" class="o_m02_1" href='../AppMain/KCJSXSClist.aspx' target="main"><span>技术性审查</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a45" class="o_m02_1" href='../AppMain/KCWJBAlist.aspx' target="main"><span>勘察文件备案</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a46" class="o_m02_1" href='../AppMain/Print_KCHGZ.aspx' target="main"><span>打印合格证</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a47" class="o_m02_1" href='../AppMain/Print_KCGZS.aspx' target="main"><span>打印告知书</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a32" class="o_m02_1" href='../ApplySGTSCYJHF/SgtScyjHfList.aspx?n=1' target="main">
                                <span>审查意见回复确认</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                    <div class="o_menu">
                        <a id="a5" class="o_m01_1" style="cursor: pointer" target="main"><span>设计文件审查</span></a>
                        <div class="o_smenu">
                            <a id="a51" class="o_m02_1" href='../AppMain/SGTSJWJSClist.aspx' target="main"><span>
                                设计文件审查合同</span></a>
                            <div>
                            </div>
                            <a id="a23" class="o_m02_1" href='../AppMain/HTBAlist.aspx?type=300' target="main"><span>
                                合同备案</span></a>
                            <div class="o_smenu2">
                            </div>
                            <%-- <a id="a24" class="o_m02_1" href='../AppMain/HTBAlist.aspx?type=300&t=2' target="main">
                                <span>省外合同备案</span></a>
                            <div class="o_smenu2">
                            </div>--%>
                            <a id="a52" class="o_m02_1" href='../AppMain/SGTSJCXXSClist.aspx' target="main"><span>
                                程序性审查</span></a>
                            <div>
                            </div>
                            <a id="a53" class="o_m02_1" href='../AppMain/SGTSJSCRYAPlist.aspx' target="main"><span>
                                人员安排</span></a>
                            <div>
                            </div>
                            <a id="a54" class="o_m02_1" href='../AppMain/SGTSJJSXSClist.aspx' target="main"><span>
                                技术性审查</span></a>
                            <div>
                            </div>
                            <a id="a55" class="o_m02_1" href='../AppMain/SGTSJWJBAlist.aspx' target="main"><span>
                                施工图设计文件备案</span></a>
                            <div>
                            </div>
                            <a id="a56" class="o_m02_1" href='../AppMain/Print_SJHGZ.aspx' target="main"><span>打印合格证</span></a>
                            <div>
                            </div>
                            <a id="a57" class="o_m02_1" href='../AppMain/Print_SJGZS.aspx' target="main"><span>打印告知书</span></a>
                            <a id="a30" class="o_m02_1" href='../AppMain/KZSFSCB.aspx' target="main" title="建设工程抗震设防审查表">
                                <span>抗震设防审查表</span></a> <a id="a31" class="o_m02_1" href='../AppMain/GGJZJN.aspx'
                                    target="main" title="公共建筑节能设计审查备案登记表"><span>公共建筑节能审查表</span></a> <a id="a31"
                                        class="o_m02_1" href='../AppMain/JZJZJN.aspx' target="main" title="居住建筑节能设计审查备案登记表">
                                        <span>居住建筑节能审查表</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a33" class="o_m02_1" href='../ApplySGTSCYJHF/SgtScyjHfList.aspx?n=2' target="main">
                                <span>审查意见回复确认</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                    <div class="o_menu">
                        <a id="a6" class="o_m01_1" style="cursor: pointer" target="main"><span>市场行为</span></a>
                        <div class="o_smenu">
                            <a id="a25" class="o_m02_1" href='../../BadBehavior/main/GoodList.aspx' target="main">
                                <span>良好行为</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a26" class="o_m02_1" href='EntBadActionList.aspx' target="main"><span>不良行为</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a27" class="o_m02_1" href='../../BadBehavior/main/BadList.aspx' target="main">
                                <span>投诉举报</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                    <%--  <div class="o_menu">
                        <a id="a7" class="o_m01_1" href='../AppMain/FedBack.aspx' target="main"><span>图审结果反馈</span></a>
                    </div>--%>
                    <div class="o_menu">
                        <a id="a11" class="o_m01_1" href='../../OA/Talk/TalkList.aspx' target="main"><span>交流讨论</span></a>
                    </div>
                    <div class="o_menu">
                        <a id="a12" class="o_m01_1" href='javascript:'><span>查询统计</span></a>
                        <div class="o_smenu">
                            <a id="a121" class="o_m02_1" href='../Statistics/PrjList.aspx' target="main"><span>项目查询</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a122" class="o_m02_1" href='../AppMain/AnPaiInfoList.aspx' target="main"><span>
                                人员安排查询</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a8" class="o_m02_1" href='../AppMain/PersonInfoList.aspx' target="main"><span>
                                人员参与项目</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a9" class="o_m02_1" href='../AppMain/AppInfoList.aspx' target="main"><span>业务统计</span></a>
                            <%-- <div class="o_smenu2">
                                <a id="a13" class="o_m03_1" href='../AppMain/AppInfoList.aspx' target="main"><span>按审查类别</span></a>
                                <a id="a10" class="o_m03_1" href='../AppMain/AppTypeList.aspx' target="main"><span>按工程类别</span></a>
                                <a id="a18" class="o_m03_1" href='../AppMain/AppSJGMList.aspx' target="main"><span>按设计规模</span></a>
                            </div>--%>
                            <a id="a14" class="o_m02_3" href='javascript:'><span>项目统计</span></a>
                            <div class="o_smenu2">
                                <a id="a15" class="o_m03_1" href='../AppMain/AppInfoList2.aspx' target="main"><span>
                                    按审查类别</span></a> <a id="a16" class="o_m03_1" href='../AppMain/AppTypeList2.aspx'
                                        target="main"><span>按工程类别</span></a> <a id="a17" class="o_m03_1" href='../AppMain/AppSJGMList2.aspx'
                                            target="main"><span>按设计规模</span></a>
                            </div>
                            <a id="a19" class="o_m02_3" href='javascript:'><span>报表统计</span></a>
                            <div class="o_smenu2">
                                <a id="a20" class="o_m03_1" href='../Statistics/SCXMBB_KC.aspx' target="main"><span>
                                    勘察文件审查</span></a>
                                <div>
                                </div>
                                <a id="a21" class="o_m03_1" href='../Statistics/SCXMBB_SJ.aspx' target="main"><span>
                                    施工图设计文件审查</span></a>
                            </div>
                            <a id="a10" class="o_m02_1" href='EntBadActionList.aspx' target="main"><span>不良行为</span></a>
                        </div>
                    </div>
                    <div class="o_menu">
                        <a id="a7" class="o_m01_1" href='../Main/ZDTEntList.aspx' target="main"><span>代填其他企业业务</span></a>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <table id="t_show" border="0" cellpadding="0" cellspacing="0" height="100%">
        <tr>
            <td id="showLeft" width="9px">
            </td>
        </tr>
    </table>
    <input id="Hidden1" runat="server" type="hidden" value="155001" />
    </form>
</body>
</html>
