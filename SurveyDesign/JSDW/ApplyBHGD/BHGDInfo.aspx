<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BHGDInfo.aspx.cs" Inherits="JSDW_ApplyBHGD_BHGDInfo" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../common/govdeptidfalse.ascx" TagName="govdeptidfalse" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>基本信息</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();
            $("#b_PrjItemType").change(function () {
                showTr();
            });
        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function showTr() {
            var t = $("#b_PrjItemType option:selected").val();
            if (t == "2000102") {
                $("tr[name=tr_t1]").show();
                $("tr[name=tr_t2]").hide();
                $("tt[name=tt_t2]").empty();
                $("tt[name=tt_t2]").each(function () {
                    var t = $(this).html();
                    $(this).replaceWith(t);
                });
            }
            else if (t == "2000101") {
                $("tr[name=tr_t2]").show();
                $("tr[name=tr_t1]").hide();
                $("tt[name=tt_t1]").empty();
                $("tt[name=tt_t1]").each(function () {
                    var t = $(this).html();
                    $(this).replaceWith(t);
                });
            } else {
                $("tr[name=tr_t1]").hide();
                $("tr[name=tr_t2]").hide();
                $("tt[name=tt_t1]").empty();
                $("tt[name=tt_t1]").each(function () {
                    var t = $(this).html();
                    $(this).replaceWith(t);
                });
                $("tt[name=tt_t2]").empty();
                $("tt[name=tt_t2]").each(function () {
                    var t = $(this).html();
                    $(this).replaceWith(t);
                });
            }
        }
        function selEnt(obj, tagId, type) {

            var qylx = "101";
            switch (type) {
                case "SG":
                    qylx = "101";
                    break;
                case "SJ":
                    qylx = "155";
                    break;
                case "KC":
                    qylx = "155";
                    break;
                case "JL":
                    qylx = "125";
                    break;
            }
            var url = "../project/EntListSel.aspx";
            url += "?qylx=" + qylx;
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }

        function selEmp(obj, tagId,type) {

            var qybm = "101";
            switch (type) {
                case "XMJ":
                    qybm = document.getElementById("b_SGDWId").value;
                    break;
                case "JGS":
                    qybm = document.getElementById("b_FBaseInfoId").value;
                    break;
                case "JCS":
                    qybm = document.getElementById("b_FBaseInfoId").value;
                    break;
                case "YTG":
                    qybm = document.getElementById("b_KCDWId").value;
                    break;
                case "XMZ":
                    qybm = document.getElementById("b_JLDWId").value;
                    break;
            }
            
            
            var url = "../project/EmpListSel.aspx";
            url += "?qybm=" + qybm;
            var pid = showWinByReturn(url, 1000, 600);
            if (pid != null && pid != '') {
                $("#" + tagId).val(pid);
                __doPostBack(obj.id, '');
            }
        }

        function BtnClear() {
            $("#s_FBaseInfoId").val('');
            $("#s_FId").val('');
            $("#s_FBaseInfoId_c").val('');
            return true;
        }

    </script>

    <base target="_self"></base>
    <style type="text/css">
        .m_txt {}
        .auto-style3 {
            width: 29%;
            height: 43px;
        }
        .auto-style4 {
            height: 43px;
        }
        .auto-style6 {
            width: 29%;
        }
        .auto-style7 {
            height: 85px;
            width: 29%;
        }
        .auto-style8 {
            height: 27px;
            width: 29%;
        }
        .auto-style14 {
            width: 291px;
            text-align: left;
        }
        .t_bg {
            text-align: right;
        }
        .auto-style22 {
            width: 29%;
            height: 25px;
        }
        .auto-style23 {
            height: 25px;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
                
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                标准化工地申报信息
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td>
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                    OnClientClick="return checkInfo();" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">

        <tr>
            <td class="t_r t_bg">
                工程名称：
            </td>
            <td colspan="1" class="auto-style6">
                
                <asp:TextBox ID="b_ProjectName" runat="server" CssClass="m_txt" Width="195px" Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">
                &nbsp;是否境外：</td>
            <td colspan="1">
                <asp:CheckBox ID="b_IsForeign" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                工程所属地：
            </td>
            <td colspan="1" class="auto-style22">
                <uc1:govdeptidfalse ID="govd_FRegistDeptId" runat="server" Enabled="false"/>
            </td>
            <td class="t_r t_bg">
                建设地址：
            </td>
            <td colspan="1" class="auto-style23">
                <asp:TextBox ID="b_Address" runat="server" CssClass="m_txt" Width="195px" MaxLength="30"
                    Enabled="false"></asp:TextBox>
                <tt>*</tt>
            </td>
        </tr>

        <tr>
            <td class="t_r t_bg">
                建筑类型：
            </td>
            <td class="auto-style6">
                <asp:DropDownList ID="b_ProjectType" runat="server" CssClass="m_txt" Width="200px" Enabled="false">
                </asp:DropDownList><tt>*</tt>
            </td>
            <td class="t_r t_bg">
                单体建筑面积(m2)：
            </td>
            <td>
                <asp:TextBox ID="b_Area" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>

            </td>
        </tr>
                <tr><td class="t_r t_bg">

                    工程投资规模：</td>
                    <td class="auto-style7">
<asp:TextBox ID="b_Investment" onblur="isFloat(this)" runat="server" CssClass="m_txt"
                        Width="195px"></asp:TextBox>(万元) <tt>*</tt>
                    </td><td class="t_r t_bg">

                        为代表性的标志工程：</td>
                    <td class="auto-style7">

            
                <asp:CheckBox ID="b_WPZSGD" runat="server" style="text-align: left" />


                    </td>
                </tr>
          <tr>
            <td class="t_r t_bg" colspan="1">
                施工单位名称：
            </td>
            <td class="auto-style8" colspan="1">
                <asp:TextBox ID="b_SGDW" runat="server" CssClass="m_txt" Width="195px" ReadOnly="True"></asp:TextBox>
                <input id="b_SGDWId" runat="server" type="hidden" />
            </td>
        </tr>
           <tr>
            <td class="t_r t_bg">
                建设单位名称：
            </td>
            <td colspan="1" class="auto-style3">
                <asp:TextBox ID="b_JSDW" runat="server" CssClass="m_txt" Width="195px" ReadOnly="True"></asp:TextBox>
                <tt>*</tt>
            </td>
            <td class="t_r t_bg">

            </td>
            <td colspan="1" class="auto-style4">
               
            </td>
        </tr>             

        <tr>
            <td class="t_r t_bg" colspan="1">
                监理单位名称：
            </td>
            <td class="auto-style8" colspan="1">
                <asp:TextBox ID="b_JLDW" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                <input id="b_JLDWId" runat="server" type="hidden" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                施工许可证编号：
            </td>
            <td class="auto-style6">
                <asp:TextBox ID="b_SGXKZBH" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg" style="width:18.8%;">
                资质证书号：
            </td>
            <td>
                <asp:TextBox ID="b_SGDWZS" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="t_r t_bg">
                安全生产许可证编号：
            </td>
            <td colspan="2">
                <asp:TextBox ID="b_SGDWAQSCXKZ" runat="server" CssClass="m_txt"  Width="97%"></asp:TextBox><tt>*</tt>
            </td>
            <tr>
            <td class="t_r t_bg">安全生产业绩评价手册编号：</td><td colspan ="2">
                <asp:TextBox ID="b_SGSCYJPJSC" runat="server" CssClass="m_txt" Width="97%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td class="t_r t_bg">项目经理安全生产能力考核证书编号：</td><td colspan ="2">
                <asp:TextBox ID="b_XMJLAQSCLLKHZS" runat="server" CssClass="m_txt" Width="97%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td class="t_r t_bg">安全员安全生产能力编号：</td><td colspan =" 2">
                <asp:TextBox ID="b_AQYAQSCLLKH" runat="server" CssClass="m_txt" Width="97%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td class="t_r t_bg">开工条件审查情况：</td><td colspan =" 2">
                <asp:TextBox ID="b_KGTJSHQK" runat="server" CssClass="m_txt" Width="97%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td class="t_r t_bg">目前工程进度：</td><td colspan =" 2">
                <asp:TextBox ID="b_MQGCJD" runat="server" CssClass="m_txt" Width="97%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr >
            <td class="t_r t_bg">
                工程造价 ：
            </td>
            <td class="auto-style6">
                 <asp:TextBox ID="b_GCZJ" runat="server" CssClass="m_txt" onblur="isFloat(this)"
                    Width="195px"></asp:TextBox>(万元)
            </td>
            <td class="t_r t_bg">
                结构类型：
            </td>
            <td>
                <asp:DropDownList ID="b_ConstrType" runat="server" CssClass="m_txt" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
          <tr>             
            <td class="t_r t_bg">
                项目经理：</td>
            <td class="auto-style6">
                <asp:TextBox ID="b_XMJL" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
                </td> 
              <td style="text-align: right" class="t_r t_bg">工程安全评价等级：</td><td>                <asp:TextBox ID="b_GCAQDJ" runat="server" CssClass="m_txt" Width="195px"
                    MaxLength="10"></asp:TextBox></td>
          </tr>
        <tr>
            <td class="t_r t_bg">
                开工日期：
            </td>
            <td class="auto-style6">
                <asp:TextBox ID="b_StartDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                竣工验收日期：
            </td>
            <td>
                <asp:TextBox ID="b_EndDate" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>   

        <tr>
            <td class="t_r t_bg">
                重大质量事故：
            </td>
            <td colspan="1" class="auto-style6">
                <asp:CheckBox ID="b_ZDZLSG" runat="server"  />
            </td>
            <td class="t_r t_bg">
                重大安全事故：
            </td>
            <td colspan="1">
                <asp:CheckBox ID="b_ZDAQSG" runat="server"  />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                申报单位名称：
            </td>
            <td class="auto-style8" class="auto-style5" colspan="1">
               <asp:TextBox ID="b_SBDWMC" runat="server" CssClass="m_txt"  Width="195px"></asp:TextBox><tt>*</tt>
                 </td>

            <td class="t_bg">申报时间：</td>
            <td><asp:TextBox ID="b_SBSJ" onfocus="WdatePicker()" runat="server" Width="195px"  CssClass="m_txt"></asp:TextBox><tt>*</tt></td>
        </tr>

        <tr>
            <td class="t_r t_bg">
                联系电话：
               </td>
            <td>
               <asp:TextBox ID="b_FLinkMan" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td><td class="t_bg">联系人：</td><td>
               <asp:TextBox ID="b_FTel" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
                <tr>
            <td class="t_r t_bg">
                通信地址：
               </td>
            <td>
               <asp:TextBox ID="b_FAddres" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td><td class="t_bg">邮政编码：</td><td>
               <asp:TextBox ID="b_Fpost" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
              监督部门：
            </td><td class="auto-style14">
                <asp:TextBox ID="b_JDBM" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr class="t_l t_bg" id="Tr2" runat="server" >
            <td colspan="1" class="t_r t_bg">
                监督部门联系人：
            </td><td class="auto-style14">
                <asp:TextBox ID="b_JDLinkMan" runat="server"  Width="195px" CssClass="m_txt"></asp:TextBox>
            </td><td>监督部门联系电话：</td><td style="text-align: left">
                <asp:TextBox ID="b_LJDTel" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="t_r t_bg" colspan="1">
                创省级安全生产文明施工标准化工地计划：
            </td>
            <td  colspan="2">
                <asp:TextBox ID="b_SJAQWMSGGDJH" runat="server" CssClass="m_txt" Width="97%"  Height="97%" TextMode="MultiLine" OnTextChanged="b_SJAQWMSGGDJH_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                监理单位意见：
            </td>
            <td  colspan="2">
                <asp:TextBox ID="b_GCJLDWYJ" runat="server" CssClass="m_txt" Width="97%"  Height="97%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                监理单位：
            </td>
            <td class="auto-style14">
                <asp:TextBox ID="b_JLDWMC" runat="server" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
            <td class="t_r t_bg">
                填写意见时间：
            </td>
            <td>
                <asp:TextBox ID="b_JLDWTBYJSJ" runat="server" CssClass="m_txt" MaxLength="20" onfocus="WdatePicker()" Width="195px"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                建设单位意见：
            </td>
            <td  colspan="2">
                <asp:TextBox ID="b_JSDWYJ" runat="server" CssClass="m_txt" Width="97%"  Height="100%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                建设单位名称：</td>
            <td class="auto-style14">
                <asp:TextBox ID="b_JSDWMC" runat="server" CssClass="m_txt" MaxLength="20" Width="195px" ReadOnly="True"></asp:TextBox>
                
                <input id="b_JSDWID" runat="server" type="hidden" />
           </td>
            <td class="t_r t_bg">
                填写意见时间：
            </td>
            <td>
                <asp:TextBox ID="b_JSDWSJ" runat="server" onfocus="WdatePicker()" CssClass="m_txt" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" colspan="1">
                工程所在地建设行政主管部门或有关厅局意见：
            </td>
            <td  colspan="2">
                <asp:TextBox ID="TextBox2" runat="server" CssClass="m_txt" Width="97%"  Height="100%" TextMode="MultiLine" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                行政主管单位名称：</td>
            <td class="auto-style14">
                <asp:TextBox ID="TextBox3" runat="server" CssClass="m_txt" MaxLength="20" Width="195px" ReadOnly="True" Enabled="False"></asp:TextBox>
                
                <input id="Hidden1" runat="server" type="hidden" />
           </td>
            <td class="t_r t_bg">
                填写意见时间：
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" CssClass="m_txt" Width="195px" Enabled="False"></asp:TextBox>
            </td>
        </tr>    
       
        <tr>
            <td class="t_r t_bg">
                备注：</td>
            <td colspan="2">
               <asp:TextBox ID="b_Remark" Height="35px" TextMode="MultiLine" runat="server" CssClass="m_txt" Width="97%"></asp:TextBox>&nbsp;</td>
        </tr>
    </table>
   

 
    <input id="b_AddressDept" type="hidden" runat="server" />
    <input id="pj_AddressDept" type="hidden" runat="server" />
    </form>
</body>
</html>

