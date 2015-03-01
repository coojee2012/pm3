﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RYQKInfo.aspx.cs" Inherits="WYDW_XMQK_RYQKInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员基本情况</title>
    <asp:Link id="skin1" runat="server"></asp:Link>

    <script type="text/javascript" src="../../script/jquery.js"></script>

    <script type="text/javascript" src="../../script/default.js"></script>

    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript">
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
        function ShowUploadPage(sUrl, width, height) {
            var fid = $("#hidFId").val();
            var readonly = "1";
            if (fid != null && fid.toString() != "") {
                window.showModalDialog(sUrl +'&ReadOnly=' + readonly + '&FId=' + fid + '&rid=' + Math.random(), window,'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; status:no; help:no;');
            }


        }
        function showPhoto(imgUrl) {
            $("#img_EmpPic").attr("src", imgUrl);
            $("#t_fphoto").val(imgUrl);
        }

        //选择父职务时发生
        function selectPPosition() {
            var parentId = $("#PfPosition").val();

            getPositionByPid(parentId);
        }
        //选择子职务时发生
        function selectPosition() {
            var positionText = $("#selPosition").find("option:selected").text();
            $("#t_fPosition").val(positionText);
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
        function getAndSetSelPosByPid(parentId, name, value) {
            $.post("../Common/script/common_Ajax.ashx?action=GetAndSetSelPosByPid&parentId=" + parentId + "&name=" + name + "&value=" + value, "", function (data) {
                if (data != null && data != "") {
                    //alert(data);
                    $("#selPosition").empty();
                    $("#selPosition").append(data);
                }
            }, "text");
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
                    <th colspan="2">人员基本情况
                    </th>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
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
                            <asp:TextBox ReadOnly="True" ID="t_fPersonName" runat="server" CssClass="m_txt" Width="195px" MaxLength="30"></asp:TextBox>

                        </td>
                        <td class="t_r t_bg">性别：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList Enabled="False" ID="t_fSex" runat="server" CssClass="m_txt" Width="197px">
                                <asp:ListItem Value="">--不填--</asp:ListItem>
                                <asp:ListItem Value="男">男</asp:ListItem>
                                <asp:ListItem Value="女">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td rowspan="8" style="text-align: center">
                            <img id="img_EmpPic" runat="server" height="100" src="../Common/images/nophoto.jpg" width="85" />
                            <br />
                            <br />
                            <%--<input type="button" value="修改" class="m_btn_w2" onclick="ShowAppPage('../Common/upload/FileManage.aspx?Ftype=3001&ReadOnly=0&IsSingle=1', '3001', '800', '600')" />--%>

                        </td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">证件类型：
                        </td>
                        <td colspan="1">
                            <asp:DropDownList Enabled="False" ID="t_fCardType" runat="server" CssClass="m_txt" Width="200px">
                            </asp:DropDownList>
                        </td>

                        <td class="t_r t_bg">证件号码：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fCardID" runat="server" CssClass="m_txt" Width="192px" MaxLength="30"></asp:TextBox>

                        </td>

                    </tr>

                    <tr>
                        <td class="t_r t_bg">民族：
                        </td>
                        <td>
                            <asp:TextBox ReadOnly="True" ID="t_fMZ" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">出生日期：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fBirthday" runat="server" CssClass="m_txt" MaxLength="20" Width="192px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">毕业学校：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fSchool" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>

                        </td>
                        <td class="t_r t_bg">毕业日期：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fbysj" runat="server" CssClass="m_txt" MaxLength="20" Width="192px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="t_r t_bg">所学专业：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fMajor" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">毕业证书号：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fbyzsh" runat="server" CssClass="m_txt" MaxLength="10" Width="192px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">毕业证书电子件：

                        </td>
                        <td colspan="1">

                            <input id="BYZSUpload" type="button" value="上传文件(0)" class="m_btn_w6" onclick="ShowUploadPage('../Common/upload/FileManage.aspx?Ftype=3002&IsSingle=0', '800', '600')" runat="server" />
                            <span id="BYZSUploadTips" runat="server" style="color: green; display: none">已上传</span>
                        </td>
                        <td colspan="2"></td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">学历：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fxl" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>

                        </td>
                        <td class="t_r t_bg">学位：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fxw" runat="server" CssClass="m_txt" MaxLength="10" Width="192px"></asp:TextBox>

                        </td>

                    </tr>
                    <tr>
                        <td class="t_r t_bg">学位证书号：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fxwzsh" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">学位证书电子文件：

                        </td>
                        <td colspan="1">
                            <input id="XWZSUpload" type="button" value="上传文件(0)" class="m_btn_w6" onclick="ShowUploadPage('../Common/upload/FileManage.aspx?Ftype=3003&IsSingle=0', '800', '600')" runat="server" />
                            <span id="XWZSUploadTips" runat="server" style="color: green; display: none">已上传</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">职称：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fTechnical" runat="server" CssClass="m_txt" Width="195px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">职称取得时间：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_zcqdsj" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="192px"></asp:TextBox>
                        </td>
                        <td rowspan="7"></td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">职称证书号：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fzczsh" runat="server" CssClass="m_txt"
                                MaxLength="20" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">职称证书电子件：
                        </td>
                        <td colspan="1">
                            <input id="ZCZSUpload" type="button" value="上传文件(0)" class="m_btn_w6" onclick="ShowUploadPage('../Common/upload/FileManage.aspx?Ftype=3004&IsSingle=0', '800', '600')" runat="server" />
                            <span id="ZCZSUploadTips" runat="server" style="color: green; display: none">已上传</span>
                        </td>

                    </tr>
                    <tr>
                        <td class="t_r t_bg">职务：
                        </td>
                        <td colspan="3">
                            <%--<asp:DropDownList runat="server" ID="PfPosition" CssClass="m_txt" AutoPostBack="True" OnSelectedIndexChanged="PfPosition_SelectedIndexChanged"></asp:DropDownList><asp:DropDownList runat="server" ID="selPosition" CssClass="m_txt"></asp:DropDownList>--%>
                            <select id="PfPosition" disabled="disabled" onchange="selectPPosition()" class="m_txt">
                                <option value='-1' selected='selected'>---请选择---</option>
                                <%=PPositionStr %>
                            </select><nobr />
                            <select id="selPosition" disabled="disabled" class="m_txt" runat="server" onchange="selectPosition()">
                                <option value="-1" selected="selected">---请选择---</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">办公电话：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fbgdh" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>

                        </td>
                        <td class="t_r t_bg">个人电话：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fgrdh" runat="server" CssClass="m_txt" MaxLength="10" Width="192px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">电子邮箱：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fdzyx" runat="server" CssClass="m_txt" MaxLength="10" Width="195px"></asp:TextBox>
                        </td>
                        <td class="t_r t_bg">邮政编码：
                        </td>
                        <td colspan="1">
                            <asp:TextBox ReadOnly="True" ID="t_fyzbm" runat="server" CssClass="m_txt" MaxLength="10" Width="192px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="t_r t_bg">住址：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ReadOnly="True" ID="t_fAddress" runat="server" CssClass="m_txt" Width="97%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">联系地址：
                        </td>
                        <td colspan="3">
                            <asp:TextBox ReadOnly="True" ID="t_flxdz" runat="server" CssClass="m_txt" Width="97%"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="t_r t_bg">备注：
                        </td>
                        <td colspan="5" style="height: 90px">
                            <asp:TextBox ReadOnly="True" ID="t_fMemo" runat="server" CssClass="m_txt" Width="98%" Height="80px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <input type="hidden" id="hidfilecount" value="0" runat="server" />
        <input type="hidden" id="t_fphoto" value="" runat="server" />
        <input type="hidden" id="t_fPosition" value="" runat="server" />
        <!--传到upload的fid,与filelist里的flinkid对应-->
        <input type="hidden" id="hidFId" value="" runat="server" />
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
