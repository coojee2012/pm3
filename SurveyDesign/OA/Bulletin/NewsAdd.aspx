<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsAdd.aspx.cs" Inherits="Government_DataCollect_NewsAdd"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <title>文件通知管理</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../DateSelect/WdatePicker.js" type="text/javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"> </script>

    <script language="javascript">
<!--
        function validate(obj) {
            if (TestValid()) {
                obj.style.display = "none";
                obj.style.width = 100;
                obj.value = '提交中...';
                return true;
            }
            else {
                return false;
            }
        }
        rnd.today = new Date();
        rnd.seed = rnd.today.getTime();
        function rnd() {
            rnd.seed = (rnd.seed * 9301 + 49297) % 233280;
            return rnd.seed;
        }
        function showpage() {

            var sVal = showModalDialog("newPubTree.aspx?e=0&rnd=" + rnd() + "&ftype=1" + "&fnewsid=" + document.forms[0].Hfnewsid.value + '&fcol=' + document.forms[0].txtFClassID.value, document.forms[0].txtFClassID.value, "dialogWidth=250px;dialogHeight=600px");

            document.forms[0].txtFClassID.value = sVal;
            if (sVal != null) {
                var now = new Date();
                strTime = now.toLocaleString();
                strYear = now.getFullYear();
                strMonth = now.getMonth() + 1;
                strDay = now.getDate();

                strMonth = strMonth + 3;
                if (strMonth > 12) {
                    strYear = strYear + 1;
                    strMonth = strMonth - 12;
                }

                arrStr = sVal.split(',');
                if (arrStr != null && arrStr.length > 0) {
                    for (var i = 0; i < arrStr.length; i++) {
                        if (arrStr[i] == "200005002") {
                            document.getElementById("t_FValidEnd").value = strYear + '-' + (strMonth) + '-' + strDay;
                            break;
                        }
                        if (arrStr[i] == "200005003") {

                            document.getElementById("t_FValidEnd").value = strYear + '-' + (strMonth) + '-' + strDay;
                            break;
                        }
                        if (arrStr[i] == "200005004") {
                            document.getElementById("t_FValidEnd").value = strYear + '-' + (strMonth) + '-' + strDay;
                            break;
                        }
                        if (arrStr[i] == "200005005") {
                            document.getElementById("t_FValidEnd").value = strYear + '-' + (strMonth) + '-' + strDay;
                            break;
                        }
                        if (arrStr[i] == "200005006") {
                            document.getElementById("t_FValidEnd").value = strYear + '-' + (strMonth) + '-' + strDay;
                            break;
                        }
                    }
                }
            }
        }
        function showcolor() {
            document.forms[0].t_FColor.value = showModalDialog("SelectColor.htm", "", "dialogWidth=225px;dialogHeight=195px");
        }
        function TestValid() {

        }
        function getPageValue(sValue) {
            window.returnValue = sValue;
            self.close();
        }
        function selColNum() {
            var t_FWebId = $("#t_FWebId");
            var v = showWinByReturn('newPubTree.aspx?e=0&ftype=<%=Request["fcol"]%>&cols=' + t_FWebId.val(), 250, 300);
            if (v != null && v != '')
                t_FWebId.val(v);
        }
//-->
    </script>

    <base target="_self">
    </base>
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="Form1" method="post" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="7">
                <asp:Literal ID="lPostion" runat="server">文件通知管理</asp:Literal>
            </th>
        </tr>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l">
                </td>
                <td class="m_bar_m t_r">
                    <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                    <input id="btnReturn" class="m_btn_w2" onclick="window.close()" type="button" value="返回" />
                </td>
                <td class="m_bar_r">
                </td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">
                    栏目编号：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_FWebId" runat="server" Width="500px" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                    <input id="btselect" class="m_btn_w4" onclick="selColNum()" type="button" value="选择栏目" />
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">
                    标题：
                </td>
                <td nowrap colspan="3">
                    <asp:TextBox ID="t_FName" runat="server" Width="500px" CssClass="m_txt" MaxLength="80"></asp:TextBox><tt>*</tt>(80字)
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" rowspan="2">
                    发布情况：
                </td>
                <td>
                    是否发布:<asp:CheckBox ID="CBPublish" runat="server" Checked="True" />
                </td>
            </tr>
            <tr>
                <td>
                    发布日期:<asp:TextBox ID="t_FPubTime" onblur="isDate(this);" runat="server" CssClass="m_txt"
                        onfocus="WdatePicker()" Width="70px"></asp:TextBox>&nbsp;&nbsp;截止日期:<asp:TextBox
                            ID="t_FValidEnd" runat="server" CssClass="m_txt" onblur="isDate(this);" onfocus="WdatePicker()"
                            Width="70px"></asp:TextBox>
                    &nbsp;&nbsp;发布顺序:
                    <asp:TextBox ID="t_FOrder" onblur="isInt(this);" runat="server" Width="50px" CssClass="m_txt">50000</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan="1">
                </td>
                <td colspan="3">
                    <input id="FMain" type="hidden" value="<p>请输入新闻内容！</p>" name="content1" runat="server"
                        class="m_txt"><iframe id="eWebEditor1" src="../../eWebEditor/ewebeditor.htm?id=FMain&amp;style=light"
                            frameborder="0" width="100%" scrolling="no" height="500" language="javascript"></iframe>
                </td>
            </tr>
        </table>
        <input id="Hfnewsid" type="hidden" runat="server" />
    </form>
</body>
</html>
