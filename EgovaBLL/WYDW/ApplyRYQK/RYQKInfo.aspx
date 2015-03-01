<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RYQKInfo.aspx.cs" Inherits="WYDW_ApplyRYQK_RYQKInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/common/govdeptid.ascx" TagName="govdeptid" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员基本情况</title>
    <asp:Link id="skin1" runat="server"></asp:Link>
    <base target="_self" />

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" src="../Common/script/common.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //changeUpBtnText('3001', $("#hidBYZSCount").val());
            //changeUpBtnText('3002', $("#hidXWZSCount").val());
            //changeUpBtnText('3003', $("#hidZCZSCount").val());
        });
        function checkInfo() {
            if (AutoCheckInfo()) {
                var position = $("#selPosition").val();
                var cardType = $("#<%=t_fCardType.ClientID%>").val();
                //alert(cardType);
                if (position == "-1") {
                    alert('请选择人员职务');
                    return false;
                }
                if (cardType.toString().trim() == "-1") {
                    alert('请选择证件类型');
                    return false;
                }
                else {
                    return true;
                }
            }
            return false;
        }
        function ShowAppPage(sUrl,width, height) {
            window.showModalDialog(sUrl + '&rid=' + Math.random(), window, 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; status:no; help:no;');
            //if (rets != null && rets != undefined) {
            //    var ret = rets.split('|');
            //    if (ret[0] == "ok") {
            //        switch (ret[1]) {
            //            case "3001": $("#t_fphoto").val(ret[2]); $("#img_EmpPic").attr("src",ret[2]); break;
            //            case "3002": $("#BYZSUpload").hide(); $("#BYZSUploadTips").show();break;
            //            case "3003": $("#XWZSUpload").hide(); $("#XWZSUploadTips").show(); break;
            //            case "3004": $("#ZCZSUpload").hide(); $("#ZCZSUploadTips").show(); break;
            //        }

            //    }
            //}
            
        }
        function showPhoto(imgUrl) {
            $("#img_EmpPic").attr("src", imgUrl);
            $("#t_fphoto").val(imgUrl);
        }
        //根据已上传的电子件数改变button的text
        function changeUpBtnText(type,count) {
            switch (type) {
                case '3002': $("#BYZSUpload").val('上传文件(' + count + ')'); break;
                case '3003': $("#XWZSUpload").val('上传文件(' + count + ')'); break;
                case '3004': $("#ZCZSUpload").val('上传文件(' + count + ')'); break;
            }
        }
        //删除一个上传文件button的text变化
        function delUpBtnText(type) {
            //alert('df');
            switch (type) {
                case '3002': var oldCountStr = $("#BYZSUpload").val(); $("#BYZSUpload").val(replaceCountByKH(oldCountStr)); break;
                case '3003': var oldCountStr = $("#XWZSUpload").val(); $("#XWZSUpload").val(replaceCountByKH(oldCountStr)); break;
                case '3004': var oldCountStr = $("#ZCZSUpload").val(); $("#ZCZSUpload").val(replaceCountByKH(oldCountStr)); break;
            }
        }
        function changeBYZS(count) {
            $("#BYZSUpload").val('3002上传文件(' + count + ')');
        }
        //选择父职务时发生
        function selectPPosition() {
            var parentId = $("#PfPosition").val();
            
            getPositionByPid(parentId);
        }
        //选择子职务时发生
        function selectPosition() {
            var position = $("#selPosition").val();
            $("#t_fPosition").val(position);
        }
        //根据父职务的id获取子职务
        function getPositionByPid(parentId) {
            $.post("../Common/script/common_Ajax.ashx?action=GetPositionByPid&parentId=" + parentId, "", function (data) {
                if (data != null && data != "") {
                    $("#selPosition").empty();
                    $("#selPosition").append(data);
                }
            }, "text");
        }
        //根据父职务的id获取子职务并设置选中项
        function getAndSetSelPosByPid(parentId,name,value) {
            $.post("../Common/script/common_Ajax.ashx?action=GetAndSetSelPosByPid&parentId=" + parentId+"&name="+name+"&value="+value, "", function (data) {
                if (data != null && data != "") {
                    //alert(data);
                    $("#selPosition").empty();
                    $("#selPosition").append(data);
                }
            }, "text");
        }
        //function getSelect(type) {
        //    var url = "";
        //    var ppValue=$("#hidPPositionValue").val();
        //    var pText=$("#hidPositionText").val();
        //    //新增
        //    if (type == "1") {
        //        url = "../Common/script/common_Ajax.ashx?action=GetPPosition";
        //    }
        //    else if (type == "2") {
        //        url = "../Common/script/common_Ajax.ashx?action=GetPositionSelByUpdate&selPPValue="+ppValue+"&selPText="+pText;
        //    }
        //    $.post(url, "", function (data) {
        //        if (data != null && data != "") {
        //            $("#PfPosition").empty();
        //            $("#PfPosition").append(data);
        //        }
        //    }, "text");
        //}
        //select中text等于给定的text的item设置为选中
        function selItemByText(selID, text) {
            var sel = "";
            $("#" + selID + " option").each(function () {
                if ($(this).text() == text) {
                    sel = $(this).val();
                    return false;
                }
            });
            $("#" + selID + " option[value='" + sel + "']").attr('selected', 'selected');
        }
    </script>

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
        <div style="height: 100%; width: 100%; ">

            <table width="98%" align="center" class="m_title">
                <tr>
                    <th colspan="2">人员基本情况
                    </th>
                </tr>
                <tr runat="server" id="tr_his" visible="false">
                    <td class="t_r t_bg" width="15%">
                        <tt>历次变更记录：>历次变更记录：</tt>
                    </td>
                    <td class="t_l">
                        <asp:DropDownList ID="ddlHis" runat="server" AutoPostBack="true"
                            TabIndex="10">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div id="divSetup2">
                <table width="98%" align="center" class="m_bar">
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
                </table>
                <table class="m_table" width="98%" align="center">
                    <colgroup width="17%"></colgroup>
                    <colgroup width="25%"></colgroup>
                    <colgroup width="17%"></colgroup>
                    <colgroup width="25%"></colgroup>
                    <colgroup width="16%"></colgroup>
                    
                    <tr>
                        <td class="t_r t_bg">姓名：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fPersonName" runat="server" CssClass="m_txt" Width="195px" MaxLength="30"></asp:TextBox>
                            <tt>*</tt>
                        </td>
                        <td class="t_r t_bg">性别：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="t_fSex" runat="server" CssClass="m_txt" Width="197px">
                                <asp:ListItem Value="">--不填--</asp:ListItem>
                                <asp:ListItem Value="男">男</asp:ListItem>
                                <asp:ListItem Value="女">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td rowspan="8" style="text-align: center">
                            <img id="img_EmpPic" runat="server" height="100" src="~/image/gif_48_103.gif" width="85" />
                            <br /><br />
                            <input type="button" value="修改" class="m_btn_w2" onclick="ShowAppPage('../Common/upload/FileManage.aspx?Ftype=3001&ReadOnly=0&IsSingle=1','3001', '800', '600')" />

                        </td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">证件类型：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="t_fCardType" runat="server" CssClass="m_txt" Width="200px">
                            </asp:DropDownList><tt>*</tt>
                        </td>

                        <td class="t_r t_bg">证件号码：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fCardID" runat="server" CssClass="m_txt" Width="192px" MaxLength="30"></asp:TextBox>
                            <tt>*</tt>
                        </td>

                    </tr>

                    <tr>
                        <td class="t_r t_bg">民族：
                        </td>
                        <td>
                            <asp:TextBox ID="t_fMZ" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">出生日期：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fBirthday" runat="server" CssClass="m_txt" MaxLength="20" onfocus="WdatePicker()" Width="192px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">毕业学校：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fSchool" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>

                        </td>
                        <td class="t_r t_bg">毕业日期：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fbysj" onfocus="WdatePicker()" runat="server" CssClass="m_txt" MaxLength="20" Width="192px"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="t_r t_bg">所学专业：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fMajor" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">毕业证书号：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fbyzsh" runat="server" CssClass="m_txt" MaxLength="10" Width="192px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">毕业证书电子件：

                        </td>
                        <td colspan="1">

                            <input id="BYZSUpload" type="button" value="上传文件(0)" class="m_btn_w6" onclick="ShowAppPage('../Common/upload/FileManage.aspx?Ftype=3002&ReadOnly=0&IsSingle=0',  '800', '600')" runat="server" />
                            <span id="BYZSUploadTips" runat="server" style="color: green;display:none">已上传</span>
                        </td>
                        <td colspan="2"></td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">学历：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="t_fxl" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:DropDownList>

                        </td>
                        <td class="t_r t_bg">学位：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fxw" runat="server" CssClass="m_txt" MaxLength="10" Width="192px"></asp:TextBox>

                        </td>
                        
                    </tr>
                    <tr>
                        <td class="t_r t_bg">学位证书号：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fxwzsh" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">学位证书电子文件：

                        </td>
                        <td colspan="1">
                            <input id="XWZSUpload" type="button" value="上传文件(0)" class="m_btn_w6" onclick="ShowAppPage('../Common/upload/FileManage.aspx?Ftype=3003&ReadOnly=0&IsSingle=0', '800', '600')" runat="server" />
                            <span id="XWZSUploadTips" runat="server" style="color: green;display:none">已上传</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">职称：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="t_fTechnical" runat="server" CssClass="m_txt" Width="195px" MaxLength="20"></asp:DropDownList>
                        </td>
                        <td class="t_r t_bg">职称取得时间：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fzcqdsj" onfocus="WdatePicker()" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="192px"></asp:TextBox>
                        </td>
                        <td rowspan="7"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">职称证书号：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fzczsh" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">职称证书电子件：
                        </td>
                        <td colspan="1">
                            <input id="ZCZSUpload" type="button" value="上传文件(0)" class="m_btn_w6" onclick="ShowAppPage('../Common/upload/FileManage.aspx?Ftype=3004&ReadOnly=0&IsSingle=0', '800', '600')" runat="server" />
                            <span id="ZCZSUploadTips" runat="server" style="color: green;display:none">已上传</span>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="t_r t_bg">职务：
                        </td>
                        <td colspan="3">
                           <%--<asp:DropDownList runat="server" ID="PfPosition" CssClass="m_txt" AutoPostBack="True" OnSelectedIndexChanged="PfPosition_SelectedIndexChanged"></asp:DropDownList><asp:DropDownList runat="server" ID="selPosition" CssClass="m_txt"></asp:DropDownList>--%>
                            <select id="PfPosition" onchange="selectPPosition()" class="m_txt"><option value='-1' selected='selected'>---请选择---</option><%=PPositionStr %></select><nobr />
                            <select id="selPosition" class="m_txt" runat="server" onchange="selectPosition()"><option value="-1" selected="selected">---请选择---</option></select><tt>*</tt>
                        </td>
                    </tr>                  
                    <tr>
                        <td class="t_r t_bg">办公电话：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fbgdh" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>

                        </td>
                        <td class="t_r t_bg">个人电话：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fgrdh" runat="server" CssClass="m_txt" MaxLength="10" Width="192px"></asp:TextBox>

                        </td>                        
                    </tr>
                    <tr>
                        <td class="t_r t_bg">电子邮箱：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fdzyx" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">邮政编码：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="t_fyzbm" runat="server" CssClass="m_txt" MaxLength="10" Width="192px"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="t_r t_bg">住址：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="t_fAddress" runat="server" CssClass="m_txt" Width="97%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">联系地址：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="t_flxdz" runat="server" CssClass="m_txt" Width="97%"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">备注：
                        </td>
                        <td colspan="5" style="height: 90px">
                            <asp:TextBox ID="t_fMemo" runat="server" CssClass="m_txt" Width="98%" Height="80px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <input type="hidden" id="hidfilecount" value="0" runat="server" />
        <input type="hidden" id="t_fphoto" value="" runat="server" />
        <%--<!--显示类型：1表示新增，2表示修改-->
        <input type="hidden" id="hidShowType" value="1" runat="server" />
        <!--选中的父职务的value值-->
        <input type="hidden" id="hidPPositionValue" value="-1" runat="server" />
        <!--选中的子职务的text值-->
        <input type="hidden" id="hidPositionText" value="---请选择---" runat="server" />--%>
        <input type="hidden" id="t_fPosition" value="" runat="server" />
        <!--毕业证书电子件数-->
        <input type="hidden" id="hidBYZSCount" value="0" runat="server" /> 
        <input type="hidden" id="hidXWZSCount" value="0" runat="server" /> 
        <input type="hidden" id="hidZCZSCount" value="0" runat="server" /> 
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

