<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAuthority.aspx.cs" Inherits="UserAuthority" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <asp:Link id="skin1" runat="server">
    </asp:Link>
     <base target="_self" />
     <script src="script/jquery.js" type="text/javascript"></script>
     <script type="text/javascript">
         $(function () {
             $("#checkboxAll").click(function () {
                 $("#center").find(":checkbox").attr("checked", $("#checkboxAll").attr("checked"));
             });
             $("#btnSave").click(function () {
                 var array = new Array();
                 var items = $("#center").find("input[type=checkbox]:checked");
                 $.each(items, function (index, obj) {
                     var val = $(obj).attr("value");
                     if ($.trim(val).length > 0)
                         array.push(val);
                 });
                 $("#hfFnumber").val(array.join(','));
                 return true;
             });
         });
         function ChooseRole(obj) {
             var name = $(obj).attr("name");
             var items = $("#center").find(":checkbox");
             $.each(items, function (index, item) {
                 var itemName = $(item).attr("name");
                 if (itemName.indexOf(name) > -1)
                     $(item).attr("checked", $(obj).attr("checked"));
             });
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:HiddenField ID="hfFnumber" runat="server" />
     <table width="98%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l"></td>
            <td class="t_r" style="text-align:left;padding-left:10px;" width="30%">
              用户：<font color='orange'><strong><asp:Literal ID="ltrName" runat="server" /></strong></font> &nbsp;&nbsp;&nbsp; <input type="checkbox" id="checkboxAll"/>全选/全不选
            </td>
            <td class="t_r">
                <asp:Button ID="btnSave" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnSave_Click" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
     <table class="m_table" width="98%" align="center" id="center">
        <asp:Literal ID="ltrText" runat="server"></asp:Literal>
        <%--<tr>
             <td class="t_r t_bg" width="20%">
                权限：
             </td>
             <td><asp:CheckBoxList ID="cbList" RepeatDirection="Horizontal" RepeatColumns="5" runat="server"></asp:CheckBoxList><br/></td>
        </tr>--%>
     </table>
    </form>
</body>
</html>
