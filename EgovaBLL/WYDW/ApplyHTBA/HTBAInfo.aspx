<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HTBAInfo.aspx.cs" Inherits="WYDW_ApplyContract_ContractInfoaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>项目合同备案</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" src="../../JSDW/../script/jquery.js"></script>

    <script type="text/javascript" src="../../JSDW/../script/default.js"></script>

    <script type="text/javascript" src="../../JSDW/../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" src="../Common/script/common.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            txtCss();

        });
        function checkInfo() {
            return AutoCheckInfo();
        }
        function ShowAppPage(sUrl,width, height) {
            var ret = window.showModalDialog(sUrl + '?Ftype=1001&rid=' + Math.random(),window, 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; status:no; help:no;');

            if (ret == "ok") {
                location.reload();
            }
        }
        //根据已上传的电子件数改变button的text
        function changeUpBtnText(type, count) {
            switch (type) {
                case '1001': $("#UpdateBtn").val('上传文件(' + count + ')'); break;
            }
        }
        //删除一个上传文件button的text变化
        function delUpBtnText(type) {
            switch (type) {
                case '1001': var oldCountStr = $("#UpdateBtn").val(); $("#UpdateBtn").val(replaceCountByKH(oldCountStr)); break;
            }
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
                    <th colspan="2">项目合同备案
                    </th>
                </tr>
            </table>
            <div id="divSetup2" runat="server">
                <table width="98%" align="center" class="m_bar">
                    <tr>
                        <td class="m_bar_l"></td>
                        <td></td>
                        <td class="t_r">
                                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="m_btn_w2"
                                        OnClientClick="return checkInfo();" />
                            <%--<input type="button" id="btnReturn" runat="server" value="返回" class="m_btn_w2" onclick="window.close();" />--%>
                        </td>
                        <td class="m_bar_r"></td>
                    </tr>
                </table>
                <table class="m_table" width="98%" align="center">
                    <tr>
                        <td class="t_r t_bg">合同编号
                        </td>
                        <td style="height: 35px">
                            <asp:TextBox ID="t_HTBH" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox><tt>*</tt>
                        </td>
                        <td class="t_r t_bg">委托单位
                        </td>
                        <td style="height: 35px">
                            <asp:TextBox ID="t_WTDW" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox><tt>*</tt>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">项目取得方式
                        </td>
                        <td style="height: 35px">
                            <asp:DropDownList ID="t_XMHQFS" runat="server" AutoPostBack="true" CssClass="m_txt" MaxLength="15">
                                <asp:ListItem Selected="True" Value="-1">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">招投标</asp:ListItem>
                                <asp:ListItem Value="2">协议</asp:ListItem>
                                <asp:ListItem Value="3">自建自管</asp:ListItem>
                                <asp:ListItem Value="4">后勤转制</asp:ListItem>
                                <asp:ListItem Value="5">其他</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="t_r t_bg">项目接管日期
                        </td>
                        <td style="height: 35px">
                            <asp:TextBox ID="t_JGRQ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox><tt>*</tt>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">签订日期
                        </td>
                        <td colspan="3" style="height: 35px">
                            <asp:TextBox ID="t_HTQDRQ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox><tt>*</tt>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">生效日期
                        </td>
                        <td style="height: 35px">
                            <asp:TextBox ID="t_HTSXRQ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox><tt>*</tt>
                        </td>
                        <td class="t_r t_bg">截止日期
                        </td>
                        <td style="height: 35px">
                            <asp:TextBox ID="t_HTJZRQ" onfocus="WdatePicker()" runat="server" CssClass="m_txt" MaxLength="15"></asp:TextBox><tt>*</tt>
                        </td>
                    </tr>
                    <tr>
                        <td class="t_r t_bg">相关文件
                        </td>
                        <td colspan="3" height="35px">
                            <input id="UpdateBtn" type="button" runat="server" value="上传文件(0)" class="m_btn_w6" onclick="ShowAppPage('../Common/upload/FileManage.aspx?Ftype=1001&ReadOnly=0&IsSingle=0', '800', '600')" />
                            <%--<label id="UploadFilesCount" style="color: red"><%=filecount %></label>--%>
                        </td>
                    </tr>
                    <%--                    <tr>
                        <td class="t_r t_bg">填写人
                        </td>
                        <td style="height: 35px">
                            <asp:Label ID="t_CreateName" runat="server"></asp:Label>
                        </td>
                        <td class="t_r t_bg">填写日期
                        </td>
                        <td style="height: 35px">
                            <asp:Label ID="t_CreateDate" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                </table>
            </div>
            <input type="hidden" id="hidfilecount" value="0" runat="server" />
        </div>
    </form>
</body>
</html>
