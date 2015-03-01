namespace Approve.PersistBase
{
    using System;
    using System.ComponentModel;

    public enum LogSort
    {
        [Description("应用")]
        Apply = 3,
        [Description("操作")]
        Operation = 4,
        [Description("安全")]
        Safety = 2,
        [Description("系统")]
        System = 1
    }
}

