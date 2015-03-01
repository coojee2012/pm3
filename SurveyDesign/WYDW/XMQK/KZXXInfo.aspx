<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KZXXInfo.aspx.cs" Inherits="WYDW_XMQK_KZXXInfo" %>

<%@ Register Src="~/common/govdeptid2.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目基本情况</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../JSDW/../script/jquery.js"></script>

    <script type="text/javascript" src="../../JSDW/../script/default.js"></script>

    <script type="text/javascript" src="../../JSDW/../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();

        });
        function checkInfo() {
            if (document.getElementById("hidMapX").value == "" && document.getElementById("hidMapY").value == "") {
                alert("请标记地图位置！");
                return false;
            }
            return AutoCheckInfo();
        }
        function addPrjItem() {
            var fid = document.getElementById("txtFId").value;;
            if (fid == null || fid == '') {
                alert('请先保存上方的工程项目信息！');
                return;
            }
            showAddWindow('ProjectItemInfo.aspx?fprjId=' + fid, 800, 550);
            //  alert('dd')
        }
        function openLink() {
            var fid = document.getElementById("txtFId").value;
            var projectName = document.getElementById("t_ProjectName").value;

            if (fid == null || fid == '') {
                alert('请先保存上方的工程项目信息！');
                return;
            }
            var url = document.getElementById("txtUrl").value;
            showAddWindow(url + "projectID=" + fid + "&projectName=" + projectName, 800, 550);

        }

        function setmapvalue(x, y) {
            if (x != "" && y != "") {
                document.getElementById("hidMapX").value = x;
                document.getElementById("hidMapY").value = y;
            }
        }

        function clearmapvalue() {
            document.getElementById("hidMapX").value = "";
            document.getElementById("hidMapY").value = "";
        }

        function markmap() {
            var mapx = document.getElementById("hidMapX").value;
            var mapy = document.getElementById("hidMapY").value;
            document.getElementById('BDMap').contentWindow.theLocation(mapx, mapy);
        }
    </script>

    <base target="_self"></base>
    <style type="text/css">
        .modalDiv {
            position: absolute;
            top: 1px;
            left: 1px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: gray;
            opacity: .50;
            filter: alpha(opacity=50);
        }

        .m_btn_w2 {
            height: 21px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="100">
            <ProgressTemplate>
                <div class="modalDiv"> 
                <div style="position:absolute;left:40%;top:50%;background-color:peru;border:solid 3px red;">
                    <table  align="center">
                    <tr>
                    <td ><h1>正在保存数据</h1></td>
                    <td><img src="../../JSDW/../image/load2.gif" alt="请稍候"/></td>
                    </tr>
                                    
                    </table>
                </div>
                    </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        <div style="height: 100%; width: 100%;">

            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">物业扩展信息
                    </th>
                </tr>
                <%--<tr runat="server" id="tr_his" visible="false">
                    <td class="t_r t_bg" width="15%">
                        <tt>历次变更记录：>历次变更记录：</tt>
                    </td>
                    <td class="t_l">
                        <asp:DropDownList ID="ddlHis" runat="server" AutoPostBack="true"
                            TabIndex="10">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
            </table>
            <div id="divSetup2" runat="server">
                <%--<table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td></td>
                        <td class="t_r">
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                OnClientClick="return checkInfo();" />
                            <input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />
                        </td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>--%>
                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td class="t_r t_bg">项目开发单位：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_KFDW" runat="server" CssClass="m_txt" Width="160px"></asp:TextBox><tt>*</tt>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">保障住房类型
                        </td>
                        <td>
                            <asp:DropDownList ID="t_HSTypeID" runat="server" CssClass="m_txt" Width="120px">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                                <asp:ListItem Value="01">廉租房</asp:ListItem>
                                <asp:ListItem Value="02">限价房</asp:ListItem>
                                <asp:ListItem Value="03">公共租赁房</asp:ListItem>
                                <asp:ListItem Value="04">经济适用房</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td width="15%" class="t_r t_bg">建筑面积</td>
                        <td>
                            <asp:TextBox ID="t_JZMJ" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
                            <tt>*</tt></td>
                        <td>万平方米</td>
                        <td width="15%" class="t_r t_bg">占地面积：
                        </td>
                        <td colspan="1" width="25%">
                            <asp:TextBox ID="t_ZDMJ" onblur="isFloat(this)" runat="server" CssClass="m_txt" Width="80px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1" width="10%">万平方米</td>

                    </tr>
                    <tr>
                        <td width="15%" class="t_r t_bg">项目建成年代：
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="t_JCND" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1" width="10%"></td>
                        <td width="15%" class="t_r t_bg">是否属于整治区改造旧住宅小区：
                        </td>
                        <td width="25%">
                            <asp:DropDownList Enabled="False" ID="t_IsZZXQ" runat="server" Width="80px" CssClass="m_txt">
                                <asp:ListItem Selected="True" Value="-1">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="1" width="10%"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">居民户数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JMHS" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1">户</td>
                        <td class="t_r t_bg">非居民户数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_FJMHS" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1">户</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">居住人数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JZRS" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1">人</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目接管日期：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JGRQ" ReadOnly="True" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                        <td class="t_r t_bg">首次接管日期：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_SCJGRQ" ReadOnly="True" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">物业用房面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_WYYFMJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td colspan="1">万平方米</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">多层住宅面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_DC_MJ" ReadOnly="True" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">万平方米</td>
                        <td class="t_r t_bg">多层住宅（带电梯）面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_DCDT_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">万平方米</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">高层住宅面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_GC_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">万平方米</td>
                        <td class="t_r t_bg">公寓、别墅面积：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_BS_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">万平方米</td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">办公楼或写字楼面积:</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_BG_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">万平方米</td>
                        <td class="t_r t_bg">商业用房面积：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_SY_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">万平方米</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">工业用房面积：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_GY_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">万平方米</td>
                        <td class="t_r t_bg">其他类型面积：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_QT_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">万平方米</td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">露天车位面积：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_LTCW_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">平方米</td>
                        <td class="t_r t_bg">露天车位个数：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_LTCW_GS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">个</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">地下车位面积：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_DXCW_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">平方米</td>
                        <td class="t_r t_bg">地下车位个数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_DXCW_GS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">个</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">自行车车库面积：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_ZXC_MJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">平方米</td>
                        <td class="t_r t_bg">是否有安防系统：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList Enabled="False" ID="t_AFXT" runat="server" Width="80px" CssClass="m_txt">
                                <asp:ListItem Selected="True" Value="-1">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">出入口个数：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_CRKGS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">个</td>
                        <td class="t_r t_bg">监控消防室个数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_JKXFSGS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">个</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">电梯个数：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_DTGS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">部</td>
                        <td class="t_r t_bg">配电房个数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_PDFS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">KVA</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">发电机个数：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_FDJS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">KVA</td>
                        <td class="t_r t_bg">月公共电耗：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_YGGDH" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">度</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">电机变频供水数：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_DJBPGS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">KVA</td>
                        <td class="t_r t_bg">二次供水水箱数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_ECGSSXS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">立方米</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">化粪池数：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_HFCS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">立方米</td>
                        <td class="t_r t_bg">月公共水耗：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_YGGSH" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">立方米</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">门禁监控系统数：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_MJJKXTTS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">套</td>
                        <td class="t_r t_bg">消防系统数：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_XFXTTS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">套</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">停车管理系统数：</td>
                        <td colspan="1">
                            <asp:TextBox ID="t_TCGLXTTS" ReadOnly="True" onblur="isInt(this)" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">套</td>
                        <td class="t_r t_bg">水景面积：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_SJMJ" ReadOnly="True" onblur="isFloat(this)" runat="server" CssClass="m_txt" MaxLength="10" Width="80px"></asp:TextBox>
                        </td>
                        <td colspan="1">平方米</td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">地图标记：</td>
                        <td colspan="6" style="height: 400px; width: 100%">
                            <iframe name="bsmap" id="BDMap" src="BMap.aspx" frameborder="0" height="400px" width="100%"></iframe>
                        </td>
                    </tr>
                </table>
            </div>
            <%-- <input id="t_AddressDept" type="hidden" runat="server" />
            <input id="t_Province" type="hidden" runat="server" />
            <input id="t_City" type="hidden" runat="server" />
            <input id="t_County" type="hidden" runat="server" />
            <input id="txtFId" type="hidden" runat="server" />
            <input id="txtUrl" type="hidden" runat="server" />--%>
            <input id="hidMapX" type="hidden" value="" runat="server" />
            <input id="hidMapY" type="hidden" value="" runat="server" />
        </div>


    </form>
</body>

<script type="text/javascript">
    function changeCheck(obj) {
        obj.style.background = obj.checked ? '#1eaffc' : "";
    }
    $.each($(":checkbox[id^=t_F]"), function () {
        $(this).click(function () { changeCheck(this); });
        changeCheck(this);
    });
</script>

</html>
