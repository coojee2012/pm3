<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsAdd.aspx.cs" Inherits="Approve.Admin.Page.NewsAdd"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>newEdit</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../script/Govdept.js" language="javascript"></script>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script language="javascript" src="../../DateSelect/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
        });    
    </script>

    <base target="_self"></base>

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
            if (document.forms[0].t_FName.value.trim() == "") {
                alert('标题名称必须填写');
                return false;
            }

            if (document.forms[0].txtFClassID.value.trim() == "") {
                alert('栏目编号必须填写');
                return false;
            }

            return true;
        }

        function getPageValue(sValue) {
            window.returnValue = sValue;
            self.close();
        }
      
//-->
    </script>

    <style>
        html, body { overflow-x: hidden; }
    </style>
</head>
<body style="text-align: left; overflow-x: hidden; overflow-y: scroll;">
    <form id="Form1" method="post" runat="server">
    <table width="98%" align="center" class="m_title">
        <tr>
            <th>
                文件通知
            </th>
        </tr>
    </table>
    <table width="98%" align="center" class="m_bar" id="Table3">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m" align="right">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" /><input
                    id="btnClear" class="m_btn_w2" onclick="clearPage()" type="button" value="清空" /><asp:Button
                        ID="backBtn" runat="server" CssClass="m_btn_w2" OnClick="backBtn_Click" Text="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0" class="m_table" align="center">
        <tr>
            <td align='center' width="15%" nowrap="noWrap" height="30">
                栏目编号:
            </td>
            <td align='left' colspan="3">
                <asp:TextBox ID="txtFClassID" runat="server" Width="635px" CssClass="m_txt"></asp:TextBox>
                <input id="btselect" class="m_btn_w4" onclick="showpage();" type="button" value="选择栏目" />
            </td>
        </tr>
        <tr>
            <td align="center" height="30">
                标题:
            </td>
            <td align='left' width="55%" nowrap="noWrap">
                <asp:TextBox ID="t_FName" runat="server" Width="495px" CssClass="m_txt" MaxLength="80"></asp:TextBox>(80字)
            </td>
            <td align="center" width="8%" nowrap="noWrap">
                &nbsp;
            </td>
            <td nowrap="noWrap">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" height="30">
                发布情况:
            </td>
            <td align="left">
                发布日期:<asp:TextBox ID="t_FPubTime" onblur="isDate(this);" runat="server" CssClass="m_txt"
                    onfocus="WdatePicker()" Width="70px"></asp:TextBox>&nbsp; 发布截止日期:<asp:TextBox ID="t_FValidEnd"
                        runat="server" CssClass="m_txt" onblur="isDate(this);" onfocus="WdatePicker()"
                        Width="70px"></asp:TextBox>
                发布顺序:
                <asp:TextBox ID="t_FOrder" onblur="isInt(this);" runat="server" Width="50px" CssClass="m_txt">50000</asp:TextBox>
                <asp:CheckBox ID="CBPublish" runat="server" Checked="True" Text="是否发布" />
            </td>
            <td align="center">
                发布单位：
            </td>
            <td>
                <asp:TextBox ID="t_FPubDepart" runat="server" CssClass="m_txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="30">
                关键字:
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="t_Fkey" runat="server" CssClass="m_txt" Width="397px" MaxLength="20"></asp:TextBox>(20字)
            </td>
        </tr>
        <tr>
            <td align="center">
                摘要:
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="t_FMain" runat="server" CssClass="m_txt" Height="56px" TextMode="MultiLine"
                    Width="92%" MaxLength="200" Style="word-break: break-all; word-wrap: break-word;"></asp:TextBox>(200字)
            </td>
        </tr>
        <tr style="display: none">
            <td align="center" height="30">
                新闻类别:
            </td>
            <td>
                <asp:DropDownList ID="t_FTypeId" runat="server" Width="153px" CssClass="m_txt">
                </asp:DropDownList>
            </td>
            <td align="center" nowrap="noWrap">
                热度类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FType" runat="server" CssClass="m_txt">
                    <asp:ListItem Value="0">普通</asp:ListItem>
                    <asp:ListItem Value="1">最新</asp:ListItem>
                    <asp:ListItem Value="2">最热</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="display: none">
            <td align="center">
                上传图片:
            </td>
            <td align="left">
                <input id="FPicSelect" type="file" size="18" name="PicNote" runat="server" class="m_txt"
                    style="width: 325px; height: 19px"><asp:Button ID="btnUpPic" runat="server" Text="上传"
                        CssClass="m_btn_w2" OnClick="btnUpPic_Click"></asp:Button>
            </td>
            <td align="center" colspan="2" rowspan="2">
                <asp:Image ID="impic" runat="server" Height="100px" Width="100px" />
            </td>
        </tr>
        <tr style="display: none">
            <td align="center" nowrap="noWrap">
                图片保存路径:
            </td>
            <td align="left">
                <asp:TextBox ID="t_FPicUrl" runat="server" Width="328px" CssClass="m_txt" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr style="display: none">
            <td align="center" rowspan="3">
                打开方式:
            </td>
            <td align="left" colspan="3" rowspan="1" style="height: 30px">
                <asp:RadioButton ID="rOper1" runat="server" Text="连接地址:" GroupName="0" />
                <asp:TextBox ID="t_FWebId" runat="server" CssClass="m_txt" Width="403px" MaxLength="50"
                    Text="http://"></asp:TextBox>(50字)
            </td>
        </tr>
        <tr style="display: none">
            <td align="left" colspan="3" valign="top">
                <table cellpadding="0" cellspacing="0" height="100%" width="100%">
                    <tr>
                        <td rowspan="2" width="10%" style="border-left: none 0px; border-top: none 0px; border-bottom: none 0px;"
                            nowrap="noWrap">
                            <asp:RadioButton ID="rOper2" runat="server" Text="文件下载:" GroupName="0" />
                        </td>
                        <td style="border-right: none 0px; border-top: none 0px;" height="30">
                            选择要上传的文件:
                            <input id="FFileSelect" type="file" name="PicNote" runat="server" class="m_txt" style="width: 342px;
                                height: 19px"><asp:Button ID="BtnUpFile" runat="server" Text="上传" CssClass="m_btn_w2"
                                    OnClick="BtnUpFile_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-left: none 0px; border-bottom: none 0px;" height="30">
                            上传文件保存路径:
                            <asp:TextBox ID="t_FFileNote" runat="server" Width="276px" CssClass="m_txt" MaxLength="50"></asp:TextBox>
                            <asp:Label ID="label_FSize" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="display: none">
            <td align="left" colspan="3" height="30">
                <asp:RadioButton ID="rOper3" runat="server" Text="明细页:" GroupName="0" Checked="True" />
            </td>
        </tr>
        <tr>
            <td align="center" rowspan="1">
            </td>
            <td align="left" colspan="3">
                <input id="t_FContent" type="hidden" runat="server" />
                <iframe id="eWebEditor1" src='../../webEdit/ewebeditor.aspx?id=t_FContent&style=standard650&cusdir=<%= Session["DFUserId"] %>'
                    frameborder="0" scrolling="no" width="100%" height="400"></iframe>
            </td>
        </tr>
    </table>
    <input id="Hfnewsid" type="hidden" runat="server" />
    </form>
</body>
</html>
