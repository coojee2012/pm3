<%@ Application Language="C#" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ProjectData" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码
        string machineName = Server.MachineName;//计算机名称
        string title = "应用程序启动";
        string content = "应用程序启动。\n 主机：" + machineName + "\n.Net版本：.NET CLR "
            + Environment.Version.ToString();
        DataLog.Write(LogType.Info, LogSort.System, title, content);

        RuleApp.WorkQueue<string> workQueue = new RuleApp.WorkQueue<string>(50);
        workQueue.UserWork += new RuleApp.UserWorkEventHandler<string>
        (workQueue_UserWork);
        workQueue.WorkSequential = true;

        Application.Add("workQueue", workQueue);
        Application.UnLock();
    }
    void workQueue_UserWork(RuleApp.WorkQueue<string>.EnqueueEventArgs e)
    {
        ShareTool st = new ShareTool();
        st.DownloadSingleEnt(e.Item);
    }
    private void UpdateEnt(object o)
    {
     
    }
    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码
        string mname = Server.MachineName;//计算机名称
        DataLog.Write(LogType.Info, LogSort.System, "应用程序停止", mname + "应用程序停止");
    }
 
    void Application_Error(object sender, EventArgs e)
    {
        try
        {
            Exception except = Server.GetLastError().GetBaseException();
            string machineName = Server.MachineName;//计算机名称
            DataLog.Write(LogType.Error, LogSort.System, except.Message, "主机：" + machineName + "\n详细信息：" + except.StackTrace);
        }
        catch
        {
        }

    }

    void Session_Start(object sender, EventArgs e)
    {


    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。

    }
    void Application_BeginRequest(object sender, EventArgs e)
    {
        string sUrl = Request.RawUrl;
        int ipos = sUrl.IndexOf("?");
        if (ipos >= 0)
        {
            sUrl = sUrl.Substring(ipos + 1);
            sUrl = sUrl.ToLower();


            if (sUrl.IndexOf("delete") != -1)
            {
                this.Response.Redirect("~/ApproveWeb/webhall/webhall.aspx");
            }
            if (sUrl.IndexOf("update") != -1)
            {
                this.Response.Redirect("~/ApproveWeb/webhall/webhall.aspx");
            }
            if (sUrl.IndexOf("alter") != -1)
            {
                this.Response.Redirect("~/ApproveWeb/webhall/webhall.aspx");
            }
            if (sUrl.IndexOf("exec") != -1 && Request.RawUrl.IndexOf("key=") < 0)
            {
                this.Response.Redirect("~/ApproveWeb/webhall/webhall.aspx");
            }
            if (sUrl.IndexOf("declare") != -1)
            {
                this.Response.Redirect("~/ApproveWeb/webhall/webhall.aspx");
            }
            if (sUrl.IndexOf("drop") != -1)
            {
                this.Response.Redirect("~/ApproveWeb/webhall/webhall.aspx");
            }
            if (sUrl.IndexOf("create") != -1)
            {
                this.Response.Redirect("~/ApproveWeb/webhall/webhall.aspx");
            }
            if (sUrl.IndexOf("'") != -1)
            {
                this.Response.Redirect("~/ApproveWeb/webhall/webhall.aspx");
            }
            if (sUrl.IndexOf("select") != -1)
            {
                this.Response.Redirect("~/ApproveWeb/webhall/webhall.aspx");
            }
        }
    }   
</script>

