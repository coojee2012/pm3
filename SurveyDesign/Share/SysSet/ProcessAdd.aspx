<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessAdd.aspx.cs" Inherits="Admin_main_ProcessAdd"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Src="../../Common/Govdeptid.ascx" TagName="Govdept" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>建筑施工企业管理信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript" src="../../script/default.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            txtCss();
            DynamicGrid(".m_dg1_i");
            $("#btnXGZL").attr("class", "a tab_btn1");
            $(".tab_btn").hover(function() {
                if ($(this).attr("class") != "a tab_btn1")
                    $(this).attr("class", "tab_btn1");
            },
             function() {
                 if ($(this).attr("class") != "a tab_btn1")
                     $(this).attr("class", "tab_btn");
             });
            $("#btnXGZL").click(function() {
                $(".tab_btn1").attr("class", "tab_btn");
                $(this).attr("class", "a tab_btn1");
                $("#table1").hide();
                $("#tablePager").show();
                $("#Flow_Info").show();
            });
            $("#btnSPYJ").click(function() {
                $(".tab_btn1").attr("class", "tab_btn");
                $(this).attr("class", "a tab_btn1");
                $("#table1").show();
                $("#tablePager").hide();
                $("#Flow_Info").hide();
            });
        });



        function CheckInfo() {
            if (document.getElementById("t_FName").value.trim() == "") {
                alert("流程名称必须填写");
                document.getElementById("t_FName").focus();
                return false;
            }
            if (document.getElementById("t_FNumber").value.trim() == "") {
                alert("编号必须填写");
                document.getElementById("t_FNumber").focus();
                return false;
            }
            return true;
        }


        function showSelect(url, con1, con2, colCount, sWidth, sHeight) {
            if (document.getElementById('hidden_fprocessid').value == null || document.getElementById('hidden_fprocessid').value == "") {
                alert("请先保存");
                return false;
            }

            else {

                showSelectWindow(url + '&fid=' + document.getElementById('hidden_fprocessid').value + '', sWidth, sHeight, con1, colCount);

            }
            con2.innerHTML = con1.value;
        }

        function getValue(con1, con2) {
            document.getElementById(con1).innerHTML = document.getElementById(con2).value;
        }

        function ifSaveOk() {
            var HSaveResult = document.getElementById("HSaveResult");
            if (HSaveResult) {
                window.returnValue = HSaveResult.value;
            }
            window.close();
        }
        function showSelectWindow(sUrl, width, height, con, colcount) {
            var idvalue = window.showModalDialog(sUrl + '&rid=' + Math.random(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes; resizable:yes; status:no; help:no;scroll:yes;');
            if (idvalue == null || idvalue == "") {
                return;
            }

            var htmlCode = "";
            var tempArray = new Array();
            tempArray = idvalue.split('^');
            htmlCode = "<table width='90%' cellspacing='0' cellpadding='0' align='center' class='txt7'>";
            for (var i = 0; i < tempArray.length; i++) {
                if (tempArray[i] == "") {
                    continue;
                }
                if (i % colcount == 0) {
                    htmlCode += "<tr>";
                }
                htmlCode += "<td>";
                htmlCode += tempArray[i];
                htmlCode += "</td>";
                if ((i + 1) % colcount == 0) {
                    htmlCode += "</tr>";
                }
            }
            htmlCode += "</table>";
            con.value = htmlCode;
        }
    </script>

    <base target="_self" />
    <style type="text/css">
        .style1
        {
            height: 41px;
        }
    </style>
</head>
<body style="margin-left: 5px; margin-right: 5px;">
    <form id="form1" runat="server">
    <input id="t_FManageDeptId" runat="server" type="hidden" />
    <input id="hidden_fprocessid" runat="server" type="hidden" />
    <input id="hidden_Value1" runat="server" type="hidden" />
    <input id="hidden_Value2" runat="server" type="hidden" />
    <input id="hidden_Value3" runat="server" type="hidden" />
    <table width="100%" align="center" class="m_title">
        <tr>
            <th colspan="4">
                流程维护
            </th>
        </tr>
    </table>
    <div class="tabBar">
        <div class="tabBar_l">
        </div>
        <a class="tab_btn" id="btnXGZL"><strong>流程信息</strong></a> <a class="tab_btn" id="btnSPYJ"
            style="display: none"><strong>流程图</strong></a>
        <div class="tabBar_r">
        </div>
    </div>
    <table id="tablePager" width="100%" align="center" class="m_bar">
        <tr>
            <td class="m_bar_l">
            </td>
            <td class="m_bar_m t_r">
                <asp:Button ID="btnAdd" runat="server" CssClass="m_btn_w2" Text="保存" OnClick="btnAdd_Click" />
                &nbsp;<asp:Button ID="btnNew" runat="server" CssClass="m_btn_w2" Text="新增" OnClick="btnNew_Click" />
                &nbsp;<input id="btnBack" class="m_btn_w2" onclick="ifSaveOk()" type="button" value="返回" />
            </td>
            <td class="m_bar_r">
            </td>
        </tr>
    </table>
    <table class="m_table" id="Flow_Info" width="100%" align="center">
        <tr>
            <td class="t_r t_bg">
                流程名称：
            </td>
            <td class="style1">
                <asp:TextBox ID="t_FName" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                流程全称：
            </td>
            <td>
                <asp:TextBox ID="t_FFullName" runat="server" CssClass="m_txt" Width="350px"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                编码：
            </td>
            <td>
                <asp:TextBox ID="t_FNumber" runat="server" CssClass="m_txt"></asp:TextBox><tt>*</tt>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                标准时间定义：
            </td>
            <td>
                <asp:TextBox ID="t_FDefineDay" runat="server" CssClass="m_txt" Width="50px" onblur="isInt(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                所属系统：
            </td>
            <td>
                <asp:DropDownList ID="t_FSystemId" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                管理部门：
            </td>
            <td>
                <uc1:Govdept ID="Govdept1" runat="server" SelectDefaultDept="false" />
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                企业类型：
            </td>
            <td>
                <asp:DropDownList ID="t_FBaseType" runat="server" CssClass="m_txt">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                选择包含的资质等级：
            </td>
            <td>
                <input id="Button1" type="button" value="选择" onclick="showSelect('QualiLevelSelected.aspx?fmatypeid=<%=Request["fmatypeid"] %>',document.getElementById('hidden_Value1'),document.getElementById('QualiSpan'),2,500,120)"
                    class="m_btn_w2" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="QualiSpan" runat="server"></asp:Label>

                <script>                    getValue('QualiSpan', 'hidden_Value1');</script>

            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                选择包含的业务：
            </td>
            <td>
                <input id="Button2" type="button" value="选择" onclick="showSelect('ManageTypeSelected.aspx?fmatypeid=<%=Request["fmatypeid"] %>',document.getElementById('hidden_Value2'),document.getElementById('ManageTypeSpan'),3,668,150)"
                    class="m_btn_w2" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="ManageTypeSpan" runat="server"></asp:Label>

                <script>                    getValue('ManageTypeSpan', 'hidden_Value2');</script>

            </td>
        </tr>
        <tr>
            <td class="t_r t_bg">
                选择包含的专业：
            </td>
            <td>
                <input id="Button3" type="button" value="选择" onclick="showSelect('QualiTypeSelected.aspx?fmatypeid=<%=Request["fmatypeid"] %>',document.getElementById('hidden_Value3'),document.getElementById('QualiTypeSpan'),3,890,710)"
                    class="m_btn_w2" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="QualiTypeSpan" runat="server"></asp:Label>

                <script>                    getValue('QualiTypeSpan', 'hidden_Value3');</script>

            </td>
        </tr>
    </table>
    <table align="center" id="table1" style="margin-top: 5px; display: none">
        <tr>
            <td>
                <%--<iframe src="../webflow/webflow.html?fid=<%=ViewState["FID"] %>" width="875" height="700">
                </iframe>--%>
            </td>
        </tr>
    </table>
    <input id="HSaveResult" runat="server" type="hidden" />
    </form>
</body>
</html>
