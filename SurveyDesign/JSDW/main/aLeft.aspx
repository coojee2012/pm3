<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aLeft.aspx.cs" Inherits="Admin_main_aLeft" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <style type="text/css">
        html, body, form
        {
            height: 100%;
            overflow: hidden;
            font-size: 12px;
        }
        body
        {
            padding: 0;
            margin: 0;
        }
        #main
        {
            height: 100%;
            text-align: center;
        }
    </style>

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
                    if ($(this).attr("id") != "a155")
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
                    //$(this).find("+div:parent").hide();
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
        
    </script>

</head>
<body style="height: 100%" onload="AUTOoverflow('div_menu');" onresize="AUTOoverflow('div_menu');">
    <form id="form1" runat="server">
    <table id="t_menu" border="0" cellpadding="0" cellspacing="0" height="100%" class="l_table">
        <tr>
            <td class="l_top">
                <div class="f_l">
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
                    <div id="dv1" runat="server" class="o_menu" style="">
                        <a id="a1" class="o_m01_1" href='../main/zWXTS.aspx' target="main"><span>温馨提示</span></a>
                    </div>
      
                    <div class="o_menu" style="display:none">
                        <a id="a3" class="o_m01_1" href='../appmain/PrjRegistList.aspx' target="main"><span>
                            项目登记</span></a>
                    </div>
                    <div class="o_menu">
                        <a id="a19" class="o_m01_1" href='../project/ProjectList.aspx' target="main"><span>
                            工程项目</span></a>
                    </div>
                    <div class="o_menu" >
                        <a id="a20" class="o_m01_1" href='../project/ProjectItemList.aspx' target="main"><span>
                            单项工程</span></a>
                    </div>
                    <div class="o_menu">
                        <a id="a49" class="o_m01_1" style="cursor: pointer" target="main"><span>选址意见书</span></a>
                        <div class="o_smenu">
                            <a id="a52" class="o_m02_1" href='../ApplyXZYJS/ApplyIndex.aspx' target="main"><span>意见书申请</span></a>
                            <div class="o_smenu2">
                          
                            </div>
                        </div>
                    </div>
                    <div class="o_menu">
                        <a id="a51" class="o_m01_1" style="cursor: pointer" target="main"><span>用地规划许可</span></a>
                        <div class="o_smenu">
                            <a id="a53" class="o_m02_1" href='../ApplyYDGH/ApplyIndex.aspx' target="main"><span>用地规划申请</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                    <div class="o_menu">
                        <a id="a50" class="o_m01_1" style="cursor: pointer" target="main"><span>工程规划许可</span></a>
                        <div class="o_smenu">
                            <a id="a54" class="o_m02_1" href='../ApplyGCGH/ApplyIndex.aspx' target="main"><span>工程规划申请</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                    <div class="o_menu">
                        <a id="a2" class="o_m01_1" style="cursor: pointer" target="main"><span>竣工验收备案</span></a>
                        <div class="o_smenu">
                            <a id="a24" class="o_m02_1" href='../ApplyJGYS/ApplyIndex.aspx' target="main"><span>竣工验收备案申请</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                    <div class="o_menu">
                        <a id="a25" class="o_m01_1" style="cursor: pointer" target="main"><span>项目报建申报</span></a>
                        <div class="o_smenu">
                            <a id="a26" class="o_m02_1" href='../ApplyXMBJ/ApplyIndex.aspx' target="main"><span>项目报建申报</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                    <div class="o_menu" >
                        <a id="a46" class="o_m01_1" href='../ApplyZLJDBA/ZLJDBAList.aspx' target="main"><span>
                            质量监督备案</span></a>
                    </div>
                    <div class="o_menu" >
                        <a id="a47" class="o_m01_1"  style="cursor: pointer"  target="main"><span>
                            安全监督备案</span></a>
                        <div class="o_smenu">
                            <a id="a471" class="o_m02_1" href='../ApplyAQJDBA/AQJDBAList.aspx' target="main"><span>备案申请</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a472" class="o_m02_1" href='../ApplyAQJDBA/VideoList.aspx' target="main"><span>视频登记</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                    <div class="o_menu" >
                        <a id="a12" class="o_m01_1" style="cursor: pointer" target="main"><span>施工许可证申报</span></a>
                        <div class="o_smenu">
                            <a id="a41" class="o_m02_1" href='../ApplySGXKZGL/CCBLList.aspx' target="main"><span>初次办理</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a42" class="o_m02_1" href='../ApplySGXKZGL/YQBLList.aspx' target="main"><span>延期办理</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a4" class="o_m02_1" href='../ApplySGXKZGL/BGBLList.aspx' target="main"><span>变更办理</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                     <div class="o_menu" >
                        <a id="a48" class="o_m01_1" style="cursor: pointer" target="main"><span>招投标备案</span></a>
                        <div class="o_smenu">
                            <a id="a7" class="o_m02_1" href='../ApplyZBBA/BDHFBAList.aspx' target="main"><span>标段划分备案</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a10" class="o_m02_1" href='../ApplyZBBA/ZBYSWJBAList.aspx' target="main"><span>预审文件备案</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a11" class="o_m02_1" href='../ApplyZBBA/ZBYSJGBAList.aspx' target="main"><span>预审结果备案</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a13" class="o_m02_1" href='../ApplyZBBA/ZBWJBAList.aspx' target="main"><span>招标文件备案</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a14" class="o_m02_1" href='../ApplyZBBA/PBBGBAList.aspx' target="main"><span>评标公示备案</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a16" class="o_m02_1" href='../ApplyZBBA/ZBJGBAList.aspx' target="main"><span>中标结果备案</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                    <div class="o_menu"  style="display:none;">
                        <a id="a17" class="o_m01_1" href='../ApplyBHGD/BHGDList.aspx' target="main"><span>
                            标化工地申请111</span></a>
                    </div>
                    <div class="o_menu" style="display:none;">
                        <a id="a18" class="o_m01_1" style="cursor: pointer" target="main"><span>停复工管理</span></a>
                        <div class="o_smenu">
                            <a id="a21" class="o_m02_1" href='../ApplyTFGGL/FGSQList.aspx' target="main"><span>停工申请</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a22" class="o_m02_1" href='../ApplyTFGGL/TGSQList.aspx' target="main"><span>复工申请</span></a>
                            <div class="o_smenu2">
                            </div>
                            <a id="a23" class="o_m02_1" href='../project/ProjectItemList.aspx' target="main"><span>停复工查询</span></a>
                            <div class="o_smenu2">
                            </div>
                        </div>
                    </div>
                     <asp:Literal ID="YDW" runat="server"></asp:Literal>
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
