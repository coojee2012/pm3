<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editProduct.aspx.cs" Inherits="JNCLEnt_mangeInfo_editProduct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>
    <script type="text/javascript" src="../../script/jquery.js"></script>
    <script type="text/javascript" src="../../script/default.js"></script>
    <script type="text/javascript" src="../../DateSelect/WdatePicker.js"></script>
    <base target="_self"></base>
    <script type="text/javascript">
        function checkInfo() {
            if ($("#t_SBJB").val() == "") {
                alert("请填写申报级别");
                return false;
            }
            if ($("#t_CPLB").val() == "") {
                alert("请填写产品类别");
                return false;
            }
            if ($("#t_MC").val() == "") {
                alert("请填写材料和产品名称");
                return false;
            }
            if ($("#t_CPGG").val() == "") {
                alert("请填写产品规格");
                return false;
            }
            if ($("#t_CPXH").val() == "") {
                alert("请填写产品型号");
                return false;
            }
            if ($("#t_BZMC").val() == "") {
                alert("请填写产品标准名称");
                return false;
            }
            if ($("#t_BZH").val() == "") {
                alert("请填写产品标准号");
                return false;
            }
            if ($("#t_JSBZ").val() == "") {
                alert("请填写执行的技术标准");
                return false;
            }
            if ($("#t_JCJG").val() == "") {
                alert("请填写检测机构名称");
                return false;
            }
            if ($("#t_BGBH").val() == "") {
                alert("请填写报告编号");
                return false;
            }
            if ($("#t_JYSJ").val() == "") {
                alert("请填写检测时间");
                return false;
            }
            if ($("#FMain").val() == "") {
                alert("请填写主要性能及指标说明");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" align="center" class="m_title">
            <tr>
                <th colspan="5">已备案材料和产品
                </th>
            </tr>
        </table>
        <table width="98%" align="center" class="m_bar">
            <tr>
                <td class="m_bar_l"></td>
                <td class="t_r" style="padding-right: 10px;">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="m_btn_w2" />
                    <input id="btnBack" class="m_btn_w2" onclick="window.close();" type="button" value="返回" />
                </td>
                <td class="m_bar_r"></td>
            </tr>
        </table>
        <table class="m_table" width="98%" align="center">
            <tr>
                <td class="t_r t_bg">申报级别：
                </td>
                <td>
                    <asp:TextBox ID="t_SBJB" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">产品类别：
                </td>
                <td>
                    <asp:TextBox ID="t_CPLB" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">材料和产品名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_MC" Width="450px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">产品规格：
                </td>
                <td>
                    <asp:TextBox ID="t_CPGG" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
                <td class="t_r t_bg">产品型号：
                </td>
                <td>
                    <asp:TextBox ID="t_CPXH" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">产品标准名称：
                </td>
                <td>
                    <asp:TextBox ID="t_BZMC" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>

                <td class="t_r t_bg">产品标准号：
                </td>
                <td>
                    <asp:TextBox ID="t_BZH" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">执行的技术标准：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_JSBZ" Width="450px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">检测机构名称：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="t_JCJG" Width="450px" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg">报告编号：
                </td>
                <td>
                    <asp:TextBox ID="t_BGBH" Width="150px" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>

                </td>
                <td class="t_r t_bg">检验时间：
                </td>
                <td>
                    <asp:TextBox ID="t_JYSJ" Width="150px" onfocus="WdatePicker()" runat="server" CssClass="m_txt"></asp:TextBox>
                    <tt>*</tt>
                </td>
            </tr>
            <tr>
                <td class="t_r t_bg" colspan="4">主要性能及指标说明（按产品形式检测报告指标填写）：
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;&nbsp;
                    <input id="FMain" type="hidden" value="<p>请输入主要性能及指标说明！</p>" name="content1" runat="server"
                        class="cTextBox1"><iframe id="eWebEditor1" src="../../eWebEditor/ewebeditor.htm?id=FMain&amp;style=light"
                            frameborder="0" width="630" scrolling="no" height="500" language="javascript"></iframe>
                    <tt>*</tt></td>
            </tr>
        </table>
        <input id="t_FID" runat="server" type="hidden" />
        <input id="t_fappid" runat="server" type="hidden" />
    </form>
</body>
</html>
