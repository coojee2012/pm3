//<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">   //���ȥ����һ�䣬��DreamWeaver�д򿪻�����

window.attachEvent("onload",fnInitQuery);
function fnInitQuery()
{
    top.HideHint();
}

//������ҵ��
function PopCensorForm()
{
    var sCount = OutParams.GetValue("SelectedCount");
    if (sCount == 0)
    {
        alert("��ѡ��Ҫ��˵�ҵ��");
        return;
    }
    OutParams.SetValue("ShowCensor","false");    
    //OutParams.SetValue("ACTIONINDEX",OutParams.GetValue("ActionIndex"));
    top.PopYiJianForm(OutParams);
}

//����鵵ҵ��
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
    var sTitle = "��ҵ��Ϣ";
    var sReturn = openModalDialog(sURL, vArguments, sFeatures, sTitle);
}

//���ݹ鵵״̬�ж�Ҫ�����ҵ��
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


//�����׼
function fnSantandInfo()
{
    Deal_Click();
    var sCount = OutParams.GetValue("SelectedCount");
    if (sCount == 0)
    {
        alert("��ѡ��Ҫ��˵�ҵ��");
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
        if(OutParams.GetValue("ZZXL") == "3")//ʩ���ְ�
        {
            var sFeatures = 'dialogHeight:180px; dialogWidth:450px; scroll:no; status:no; help:no; resizable:no; center:yes';        
        }
        else//ʩ���ܰ�ר��
        {
            var sFeatures = 'dialogHeight:480px; dialogWidth:450px; scroll:no; status:no; help:no; resizable:no; center:yes';
        }
    }
    else
    {
        var sFeatures = 'dialogHeight:290px; dialogWidth:450px; scroll:no; status:no; help:no; resizable:no; center:yes';    
    }
    var sTitle = "��ҵ��׼��Ϣ";
    var sReturn = openModalDialog(sURL, vArguments, sFeatures, sTitle);
}

//����ϼ����ܲ����˼����
function PopRollBackInfo()
{
    var FlowID = OutParams.GetValue("ID");
    top.ShowRollBackInfo(FlowID);
}
