namespace Approve.PersistBase
{
    using System;
    using System.ComponentModel;

    public enum LogType
    {
        [Description("用户登陆")]
        EntLogin = 5,
        [Description("用户后台登陆")]
        EntLoginBack = 4,
        [Description("错误")]
        Error = 2,
        [Description("信息")]
        Info = 1,
        [Description("警告")]
        Warning = 3
    }
}

