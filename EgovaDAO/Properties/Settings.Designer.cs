﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EgovaDAO.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=.\\SQLEXPRESS08;Initial Catalog=dbCenter;Persist Security Info=True;Us" +
            "er ID=sa")]
        public string dbCenterConnectionString {
            get {
                return ((string)(this["dbCenterConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=.\\SQLEXPRESS08;Initial Catalog=dbCenter;Persist Security Info=True;Us" +
            "er ID=sa;Password=sql2008")]
        public string dbCenterConnectionString1 {
            get {
                return ((string)(this["dbCenterConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=172.16.0.115;Initial Catalog=dbCenter;Persist Security Info=True;User" +
            " ID=jkc115;Password=jkc115")]
        public string dbCenterConnectionString2 {
            get {
                return ((string)(this["dbCenterConnectionString2"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.101.9\\MSSQLSERVER08;Initial Catalog=dbCenter;Persist Security" +
            " Info=True;User ID=sa;Password=sql2008")]
        public string dbCenterConnectionString3 {
            get {
                return ((string)(this["dbCenterConnectionString3"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=172.16.0.115;Initial Catalog=JST_XZSPBaseInfo;Persist Security Info=T" +
            "rue;User ID=jkc115;Password=jkc115")]
        public string JST_XZSPBaseInfoConnectionString {
            get {
                return ((string)(this["JST_XZSPBaseInfoConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=.;Initial Catalog=dbCenter;Persist Security Info=True;User ID=sa;Pass" +
            "word=123")]
        public string dbCenter_scConnectionString {
            get {
                return ((string)(this["dbCenter_scConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.101.9\\MSSQLSERVER08;Initial Catalog=JST_XZSPBaseInfo;Persist " +
            "Security Info=True;User ID=sa;Password=sql2008")]
        public string JST_XZSPBaseInfoConnectionString1 {
            get {
                return ((string)(this["JST_XZSPBaseInfoConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=.;Initial Catalog=dbCenter;Integrated Security=True")]
        public string dbCenterConnectionString5 {
            get {
                return ((string)(this["dbCenterConnectionString5"]));
            }
        }
    }
}
