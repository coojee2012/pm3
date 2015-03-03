<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aIndex.aspx.cs" Inherits="Government_AppMain_aIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE7,IE=8,IE=9,IE=10" />
    <title>四川省建设工程监管综合管理信息系统</title>
    <asp:Link id="skin1" runat="server">
    </asp:Link>

    <script src="../../script/jquery.js" type="text/javascript"></script>

    <script src="../../script/default.js" type="text/javascript"></script>

    <script src="../../zDialogNew/zDialog.js" type="text/javascript"></script>

    <script src="../../zDialogNew/zDrag.js" type="text/javascript"></script>

    <script type="text/javascript">

        // 使用message对象封装消息

        var message = {
            time: 0,
            title: document.title,
            timer: null,
            name: "新消息",
            // 显示新消息提示
            show: function() {
                var title = message.title.replace("【】", "").replace("【" + message.name + "】", "");
                // 定时器，设置消息切换频率闪烁效果就此产生
                message.timer = setTimeout(function() {
                    message.time++;
                    message.show();
                    if (message.time % 2 == 0) {
                        document.title = "【" + message.name + "】" + title
                    }
                    else {
                        document.title = "【】" + title
                    };
                }, 600 // 闪烁时间差
                );
                return [message.timer, message.title];
            },

            // 取消新消息提示
            clear: function() {
                clearTimeout(message.timer);
                document.title = message.title;
            }
        };
        var diag = new Dialog();
        //打开窗口
        function showMSG(URL, Title) {//查看
            //diag.AutoClose = 30;
            diag.Title = Title;

            diag.CancelEvent = function() { message.clear(); diag.close(); };
            diag.InvokeElementId = "div1";
            diag.Width = "150px";
            diag.Height = "150px";
            diag.Top = "100%";
            diag.Left = "100%";
            diag.Modal = false;
            diag.Drag = false;
            diag.show();

            //浏览器标题闪烁

            message.name = Title;
            message.show();
            document.onclick = function() {
                top.message.clear();
            };
            //top.message.clear();
        }

        function LinkClick(o) {
            alert(window.frames["BGZM_main"]);
            // o.tage = "BGZM_main" 
        }
    </script>

    <style type="text/css">
        html, body, iframe { width: 100%; height: 100%; border: none 0; _overflow-y: hidden; }
        .content { line-height: 18px; width: 98%; margin: 0px auto; padding: 2px; text-align: left; }
    </style>
</head>
<body style="overflow: hidden;">
    <iframe id="iframe_main" name='iframe_main' src="aframe.aspx" scrolling="no" frameborder="0"
        style="overflow: hidden;"></iframe>
    <dl id="menu_Sub" class="m_dl">
    </dl>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <div id="div1" style="padding: 6px; color: #333333; display: none">
                <div style="width: 98%; margin: 0px auto; text-align: left; line-height: 20px; border-bottom: 1px solid #52A5D5;">
                    <img src="../../image/06.gif" />
                    您有，
                </div>
                <div class="content" style="float: left;">
                    <asp:Literal ID="liMessage" runat="server"></asp:Literal>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="10000">
    </asp:Timer>
    <asp:HiddenField ID="hfTime" runat="server" />
    </form>
</body>
</html>
