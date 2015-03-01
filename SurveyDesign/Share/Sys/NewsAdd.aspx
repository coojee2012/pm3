<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsAdd.aspx.cs" Inherits="NewsAdd"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新闻维护</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>
 <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">

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
            var c = showModalDialog("SelectColor.htm", "", "dialogWidth=225px;dialogHeight=195px");
            if (c)
                document.forms[0].t_FColor.value = c;
        }
        function TestValid() {
            if (document.forms[0].t_FName.value.trim() == "") {
                alert('标题名称必须填写');
                return false;
            }

            //        if (document.forms[0].txtFClassID.value.trim()=="")
            //        {
            //          alert('栏目编号必须填写');
            //          return false;
            //        }
            if (document.forms[0].t_FPubDepart.value.trim() == "") {
                alert('发布单位必须填写');
                return false;
            }
            return true;
        }

        function getPageValue(sValue) {
            window.returnValue = sValue;
            self.close();
        }
       
    </script>

    <base target="_self">
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th colspan="5">
                新闻维护
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <!--<input id="btnClear" class="m_btn_w2" onclick="clearPage()" type="button" value="清空" />-->
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
                <asp:Button ID="backBtn" runat="server" CssClass="m_btn_w2" OnClick="backBtn_Click"
                    Text="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" width="98%" align="center">
        <tr style="display: none;">
            <td class="t_r t_bg">
                栏目编号：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtFClassID" runat="server" Width="635px" CssClass="m_txt" 
                    MaxLength="50"></asp:TextBox>
                <input id="btselect" class="cBtn10" onclick="showpage();" type="button" value="选择栏目" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                标题：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_FName" runat="server" Width="495px" CssClass="m_txt" MaxLength="80"></asp:TextBox><span
                    style="color: #ff0033">*</span>(80字)
            </td>
        </tr>
       <%-- <tr>
           <td class="t_r t_bg">
                标题颜色：
            </td>
            <td>
                <asp:TextBox ID="t_FColor" runat="server" CssClass="m_txt" MaxLength="100" Width="80px"></asp:TextBox>
                <input id="selectcolor" class="cBtn10" onclick="showcolor();" type="button" value="选择颜色" />
            </td>
            <td class="t_r t_bg">
                热度类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server">
                    <asp:ListItem Value="0">普通</asp:ListItem>
                    <asp:ListItem Value="1">最新</asp:ListItem>
                    <asp:ListItem Value="2">最热</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
            <td class="t_r t_bg">
                发布情况：
            </td>
            <td>
                发布日期：<asp:TextBox ID="t_FPubTime" onblur="isDate(this);" runat="server" CssClass="cTextBox1"
                    onfocus="WdatePicker()" Width="70px"></asp:TextBox>&nbsp; 发布截止日期：<asp:TextBox ID="t_FValidEnd"
                        runat="server" CssClass="cTextBox1" onblur="isDate(this);" onfocus="WdatePicker()"
                        Width="70px"></asp:TextBox>
                发布顺序:
                <asp:TextBox ID="t_FOrder" onblur="isInt(this);" runat="server" Width="50px" CssClass="cTextBox1">50000</asp:TextBox>
                <asp:CheckBox ID="CBPublish" runat="server" Checked="True" Text="是否发布" />
            </td>
            <td class="t_r t_bg">
                发布单位：
            </td>
            <td>
                <asp:TextBox ID="t_FPubDepart" runat="server" CssClass="m_txt"></asp:TextBox><span
                    style="color: #ff0033">*</span>
            </td>
        </tr>
        <tr style="display: none;">
            <td class="t_r t_bg">
                新闻类别：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="t_FTypeId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                关键字：
            </td>
            <td colspan="3">
                <asp:TextBox ID="t_Fkey" runat="server" CssClass="m_txt" Width="397px" MaxLength="20"></asp:TextBox>(20字)
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                摘要：
            </td>
            <td colspan="3" style="padding: 3px 0px;">
                <asp:TextBox ID="t_FMain" runat="server" CssClass="m_txt" Height="56px" TextMode="MultiLine"
                    Width="92%" MaxLength="200" Style="word-break: break-all; word-wrap: break-word;"></asp:TextBox>(200字)
            </td>
        </tr>
        <%--<tr>
            <td class="t_r t_bg">
                上传图片：
            </td>
            <td>
                <input id="FPicSelect" type="file" size="18" name="PicNote" runat="server" class="m_txt"
                    style="width: 325px; height: 19px"><asp:Button ID="btnUpPic" runat="server" Text="上传"
                        CssClass="cBtn3" Style="margin-left: 6px;" OnClick="btnUpPic_Click"></asp:Button>
            </td>
            <td colspan="2" rowspan="3" align="center">
                <asp:Image ID="impic" runat="server" Height="100px" Width="100px" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                图片保存路径：
            </td>
            <td>
                <asp:TextBox ID="t_FPicUrl" runat="server" Width="328px" CssClass="m_txt" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg" rowspan="2">
                打开方式：
            </td>
            <td>
                <asp:RadioButton ID="rOper1" runat="server" Text="连接地址:" GroupName="0" />
                <asp:TextBox ID="t_FWebId" runat="server" CssClass="cTextBox1" Width="403px" MaxLength="50"
                    Text="http://"></asp:TextBox>(50字)
            </td>
        </tr>
        <tr style="display: none;">
            <td colspan="3" valign="top">
                <table cellpadding="0" cellspacing="0" height="100%" width="100%">
                    <tr>
                        <td rowspan="2" width="10%" style="border-left: none 0px; border-top: none 0px; border-bottom: none 0px;">
                            <asp:RadioButton ID="rOper2" runat="server" Text="文件下载:" GroupName="0" />
                        </td>
                        <td style="border-right: none 0px; border-top: none 0px;">
                            选择要上传的文件:
                            <input id="FFileSelect" type="file" name="PicNote" runat="server" class="cTextBox1"
                                style="width: 342px; height: 19px"><asp:Button ID="BtnUpFile" runat="server" Text="上传"
                                    CssClass="cBtn3" Style="margin-left: 6px;" OnClick="BtnUpFile_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-left: none 0px; border-bottom: none 0px;">
                            上传文件保存路径:
                            <asp:TextBox ID="t_FFileNote" runat="server" Width="276px" CssClass="cTextBox1" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="label_FSize" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr>
            <td colspan="3">
                <asp:RadioButton ID="rOper3" runat="server" Text="明细页:" GroupName="0" Checked="True" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                内容：
            </td>
            <td colspan="3">
                <input id="FMain" type="hidden" value="<p>请输入新闻内容！</p>" name="content1" runat="server"
                    class="m_txt"><iframe id="eWebEditor1" src="../../eWebEditor/ewebeditor.htm?id=FMain&style=light"
                        frameborder="0" width="100%" scrolling="no" height="500" language="javascript"></iframe>
            </td>
        </tr>
    </table>
    <input id="Hfnewsid" type="hidden" runat="server" />
    </form>
</body>
</html>
