//<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">   //如果去掉这一句，在DreamWeaver中打开会乱码

window.attachEvent("onload",fnInitQuery);
function fnInitQuery()
{
    top.HideHint();
}

//浏览审核业务
function PopCensorForm()
{
    var sCount = OutParams.GetValue("SelectedCount");
    if (sCount == 0)
    {
        alert("请选择要审核的业务！");
        return;
    }
    OutParams.SetValue("ShowCensor","false");    
    //OutParams.SetValue("ACTIONINDEX",OutParams.GetValue("ActionIndex"));
    top.PopYiJianForm(OutParams);
}

//浏览归档业务
function PopArchForm()
{
    var tmpParams = new ParamClass();
    var sFunctionID = "QYZZ_XBZZSQ";
    tmpParams.SetValue("zzjgdmid", OutParams.GetValue("ZZJGDMID"));
    tmpParams.SetValue("KeyField", "zzjgdmid");
    tmpParams.SetValue("ActionIndex", OutParams.GetValue("ACTIONINDEX"));
    tmpParams.SetValue("ProjectID", OutParams.GetValue("PROJECTID"));
    var sURL = "TreeFormHistory.aspx";
    var vArguments = ToModalDialogParams(tmpParams, "");
    var sFeatures = 'dialogHeight:768px; dialogWidth:1024px; scroll:no; status:no; help:no; resizable:no; center:yes';
    var sTitle = "企业信息";
    var sReturn = openModalDialog(sURL, vArguments, sFeatures, sTitle);
}

//根据归档状态判断要浏览的业务
function PopSynStateForm()
{
    var SynState=OutParams.GetValue("SYNSTATE");
    if(SynState=="2")
    {
        PopArchForm();
    }
    else
    {
        PopCensorForm();
    }
}


//浏览标准
function fnSantandInfo()
{
    Deal_Click();
    var sCount = OutParams.GetValue("SelectedCount");
    if (sCount == 0)
    {
        alert("请选择要审核的业务！");
        return;
    }
    var tmpParams = new ParamClass();
    tmpParams.SetValue("zzjgdmid",OutParams.GetValue("ZZJGDMID"));
    tmpParams.SetValue("ProjectID",OutParams.GetValue("PROJECTID"));
    tmpParams.SetValue("zzxl",OutParams.GetValue("ZZXL"));
    tmpParams.SetValue("szzxl",OutParams.GetValue("SZZXL"));
    tmpParams.SetValue("szzlb",OutParams.GetValue("SZZLB"));
    tmpParams.SetValue("szzdj",OutParams.GetValue("SZZDJ"));
    var sURL = "GradeScrutiny.aspx";
    var vArguments = ToModalDialogParams(tmpParams, "");
    if (OutParams.GetValue("QYXZ") == "1")
    {
        if(OutParams.GetValue("ZZXL") == "3")//施工分包
        {
            var sFeatures = 'dialogHeight:180px; dialogWidth:450px; scroll:no; status:no; help:no; resizable:no; center:yes';        
        }
        else//施工总包专包
        {
            var sFeatures = 'dialogHeight:480px; dialogWidth:450px; scroll:no; status:no; help:no; resizable:no; center:yes';
        }
    }
    else
    {
        var sFeatures = 'dialogHeight:290px; dialogWidth:450px; scroll:no; status:no; help:no; resizable:no; center:yes';    
    }
    var sTitle = "企业标准信息";
    var sReturn = openModalDialog(sURL, vArguments, sFeatures, sTitle);
}

//浏览上级主管部门退件意见
function PopRollBackInfo()
{
    var FlowID = OutParams.GetValue("ID");
    top.ShowRollBackInfo(FlowID);
}
