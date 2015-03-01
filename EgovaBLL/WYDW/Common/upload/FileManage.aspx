<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileManage.aspx.cs" Inherits="PropertyEntApp_Common_FileManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <base target="_self" />
    <link href="../../SupEntApp/style/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../script/jquery.js"></script>
    <style type="text/css">
        * {
            font-size: 12px;
        }


        .tb1 {
            border-top: 1px solid #ccc;
            border-left: 1px solid #ccc;
            border-collapse: collapse;
        }

            .tb1 td {
                border-right: 1px solid #ccc;
                border-bottom: 1px solid #ccc;
                line-height: 25px;
            }

        .thistr td {
            background: #F5F5DC;
        }
    </style>

    <script type="text/javascript">
        //var sFJID = "";
        //var bReadOnly = "0";
        //var sImgPath = "";

        $(function () {

            if ($("#hdReadOnly").val() == "1")
                $("#btnFileAdd").hide();

            $("tr[name=trBody]").mouseover(function () {
                $(this).addClass("thistr").siblings("tr").removeClass("thistr");
            });
        });



        function Upload() {
            var sUrl = "FileUpload.aspx?FTypeID=" + $("#hdFTypeID").val() + "&FAppID=" + $("#hdFAppID").val() + "&p=<%=SecurityEncryption.DesEncrypt(Session["FUserId"]+"|" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now.AddHours(1)),"12345687")%>" + "&rd=" + Math.random();
            var width = "410";
            var height = "180";
            var ret = window.showModalDialog(sUrl + "&IsChangeFile=" + $("#hdIsUpdate").val(), '', 'dialogWidth:' + width + 'px; dialogHeight:' + height + 'px; center:yes;');
            if (ret != null && ret != undefined) {
                var rets = ret.split('|');
                if (rets[0] == "OK") {
                    Reload();
                    var pWindow = window.dialogArguments;
                    if (rets[1] == '3001') {
                        if (pWindow != null) {
                            pWindow.showPhoto('../../../' + rets[2]);
                        }
                    }
                    else if (rets[1] == '3002') {
                        if (pWindow != null) {
                            pWindow.changeUpBtnText('3002', $("#hdFileCount").val());
                        }
                    }
                    else if (rets[1] == '3003') {
                        if (pWindow != null) {
                            pWindow.changeUpBtnText('3003', $("#hdFileCount").val());
                        }
                    }
                    else if (rets[1] == '3004') {
                        if (pWindow != null) {
                            pWindow.changeUpBtnText('3004', $("#hdFileCount").val());
                        }
                    }
                    else if (rets[1] == '1001') {
                        if (pWindow != null) {
                            pWindow.changeUpBtnText('1001', $("#hdFileCount").val());
                        }
                    }
                }
            }
            
        }

        function Reload() {
            document.getElementById("reload").click();
        }

        function FileDel(vID) {
            var url = '../script/FileManage.ashx?F=FFileDel';
            $.ajax({
                url: url,
                type: "Post",
                data: { "FID": vID },
                success: function (result) {
                    if (result == "OK") {
                        $("tr[id=" + vID + "]").remove();
                        var pWindow = window.dialogArguments;
                        if (pWindow != null) {
                            var fType=GetUrlArgs("Ftype");
                            pWindow.delUpBtnText(fType)
                        }
                    }
                    
                    
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.responseText);
                }
            });
        }

        function fnOpenImg(url) {

            if (url.substring(url.lastIndexOf("."), url.length).toUpperCase() == ".JPG") {
                $("#imgPrivew").attr("src", url);
            }
            else {
                $("#imgPrivew").attr("src", "../Images/No.jpg");
            }
        }

        function fnopenwindowImage() {
            var src = $("#imgPrivew").attr("src");
            if (src == "../images/No.jpg") return;
            window.open(src);
        }

        //获得url参数
        function GetUrlArgs(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }
    </script>
</head>
<body style="overflow: auto">
    <form id="form1" runat="server">
        <table style="width: 100%; height: 100%">
            <tr>
                <td style="height: 25px">
                    <table align="center" width="98%" class="marTop">
                        <tr>
                            <td class="td7"></td>
                            <td class="td8" width="16"></td>
                            <td class="td8 txt26 txt20 txt28">附件管理</td>
                            <td class="td6"><a id="reload" href="<%=Request.Url.ToString() %>" style="display: none"></a>
                                <a onclick="reload();"></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" style="padding-left: 10px; padding-right: 10px;">
                    <table style="width: 100%; height: 430px; margin-bottom: 5px;">
                        <tr>
                            <td style="width: 65px; height: 22px;"><strong>电子材料:</strong></td>
                            <td style="width: 240px; height: 22px;">
                                <span id="txtCLMC" style="color: Red" runat="server"></span>
                            </td>
                            <td></td>
                            <td>附件浏览&nbsp; &nbsp; <span id="spanTitle" style="font-size: 15px; color: Red"></span></td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2" style="border: solid 1px #97D9F3;">
                                <div>
                                    <table style="width: 100%; border-left: 0px; border-top: 0px; border-right: 0px;" border="0">
                                        <tr>
                                            <td colspan="2">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 65px; height: 22px;">
                                                <font color="red"><strong>材料说明：</strong></font>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblFileName"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: right">
                                                <input id="btnFileAdd" class="cBtn7" type="button" value="附件上传" onclick="Upload();" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table style="width: 100%;" border="1" class="tb1">
                                                    <thead>
                                                        <tr>
                                                            <td style="width: 35px; text-align: center">序号</td>
                                                            <td style="text-align: center">附件名称</td>
                                                            <td style="width: 35px; text-align: center" id="tdZZ">操作</td>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tableBody">
                                                        <asp:Repeater runat="server" ID="rptFileList">
                                                            <ItemTemplate>
                                                                <tr name="trBody" id="<%#DataBinder.Eval(Container.DataItem,"FID")%>" onmouseover="fnOpenImg('<%#DataBinder.Eval(Container.DataItem,"FFileUrl")%>')">
                                                                    <td style="text-align: center"><%#DataBinder.Eval(Container.DataItem,"Seq")%></td>
                                                                    <td><%#DataBinder.Eval(Container.DataItem,"FFileName")%></td>
                                                                    <%--<td ><a href='#' onclick="fnOpenImg('<%#DataBinder.Eval(Container.DataItem,"FilePath")%>')"><%#DataBinder.Eval(Container.DataItem,"OldFileName")%></a></td>--%>
                                                                    <td style="text-align: center"><%#DataBinder.Eval(Container.DataItem,"tdDel")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 4px; font-size: 2px;">&nbsp;</td>
                            <td align="center" style="border: solid 1px #97D9F3;">
                                <img id="imgPrivew" alt="" src="../../Common/images/No.jpg" style="cursor: hand" border="0" width="400" height="400" onclick="fnopenwindowImage();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input id="hdFTypeID" type="hidden" runat="server" />
        <input id="hdFAppID" type="hidden" runat="server" />
        <input id="hdFLinkID" type="hidden" runat="server" />
        <input id="hdReadOnly" type="hidden" runat="server" />
        <!--此值表示当是单文件上传是并且已经上传一个，那么此时再上传表示更新而不是添加，值为1，否则为0-->
        <input id="hdIsUpdate" type="hidden" runat="server" value="0"/>
        <input id="hdFileCount" type="hidden" runat="server" value="0"/>
    </form>
</body>
</html>
