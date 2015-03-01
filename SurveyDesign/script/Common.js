//<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">   //������һ����Ϊ����DreamWeaver�в�������


function NewXmlDoc(){
  var xmlDoc = new ActiveXObject("Msxml2.DOMDocument");
  xmlDoc.async = false;
  return xmlDoc;
}


//bAsync: �Ƿ�ʹ���첽��ʽ������ʡ�ԣ�ȱʡΪfalse��
function XMLHTTP(sURL, sXML, bAsync){
  if (bAsync == null) 
    bAsync = false;
  var xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
  xmlhttp.Open("POST", sURL, bAsync);
  xmlhttp.Send(sXML);
  return xmlhttp.responseXML;
}


//���÷������ķ���
//sFunctionName: ������
//InParams: �������
//ErrorHint: ������÷�������ʱ��Ҫ��ʾ�ĳ�����ʾ��Ϊfalseʱ����ʾ������ʾ��
//sURL: Ҫ�����URL��ַ��ҳ���ļ�������ʡ�ԣ�ȱʡΪTransfer.aspx
function Call(sFunctionName, InParams, ErrorHint, sURL){
  if (InParams == null || InParams == ''){
    InParams = new ParamClass();
  }
  if (sURL == null || sURL == '') sURL = 'Transfer.aspx';
  InParams.XmlDoc.documentElement.setAttribute('FunctionName', sFunctionName);
  var xmlDoc = XMLHTTP(sURL, InParams.ToString());
  if (xmlDoc.documentElement==null || xmlDoc.xml == ""){
    if (ErrorHint !== false){
	  ErrorHint += '\n��������Ϊ���ݴ������'; 
	  alert(ErrorHint);
    }
	return false;
  }
  if (xmlDoc.documentElement.nodeName != 'Params'){  //����ֵ��XML����
	return xmlDoc.xml;  
  }
  var OutParams = new ParamClass(xmlDoc.xml);
  var sResult = OutParams.GetValue("Result");

  var xmlDoc = NewXmlDoc();
  if (xmlDoc.loadXML(sResult)){  //����ֵ��XML�ַ������ж��Ƿ�Ϊ������Ϣ
    if (xmlDoc.documentElement.nodeName == 'Error'){  //�ǳ�����Ϣ
	  if (ErrorHint !== false){  //��Ҫ��ʾ
        if (ErrorHint != ""){
          ErrorHint += '\n�����Ϣ��\n\n';
        }
        ErrorHint += xmlDoc.documentElement.getAttribute("Message");
  	    alert(ErrorHint);
	  }
      return false;
    }
  }
  xmlDoc = null;
  
  return sResult;
}


//��Call����������ͬ��ֻ���������TransferEx.aspxֻ����Transfer.aspxҳ��
function CallEx(sFunctionName, InParams, ErrorHint){
  return Call(sFunctionName, InParams, ErrorHint, 'TransferEx.aspx')
}


//��ȡָ�����ܵ��������
function GetFunctionInParamXML(sFunctionID){
  var tmpParams = new ParamClass();
  tmpParams.SetValue("FunctionID", sFunctionID);
  var sReturn = Call('GetFunctionInParamXML', tmpParams, false);
  return sReturn;
}

//����ѡ���û�ģ��ĺ���
//iSelectType:��ѡ����û�����(1-����,2-��ɫ,3-���š���ɫ,4-����,5-���š�����,6-��ɫ������,7�����š���ɫ������)
//sSelectedUserXML: ��ǰѡ�еĽڵ��XML�����ڻָ�ѡ��״̬
//bAllowMultiSelect: �Ƿ�����ѡ�����û���Ĭ��Ϊtrue
//bAllowNotSelect: �Ƿ���Բ�ѡ���κ��û���Ĭ��Ϊtrue
function SelectUsers(iSelectType, sSelectedUserXML,  bAllowMultiSelect, bAllowNotSelect){ 
  var oParams = new Object();
  iSelectType = parseInt(iSelectType);
  if (isNaN(iSelectType)) iSelectType = 7;
  if (bAllowMultiSelect == null) bAllowMultiSelect = true;
  if (bAllowNotSelect == null) bAllowNotSelect = true;
  oParams.SelectType = iSelectType;
  oParams.SelectedUserXML = sSelectedUserXML;
  oParams.AllowMultiSelect = bAllowMultiSelect;
  oParams.AllowNotSelect = bAllowNotSelect;
  var sURL = "SelectUsers.aspx";
  var vArguments = oParams;
  var sFeatures;
  if (!bAllowMultiSelect)
    sFeatures = 'dialogWidth:250px; dialogHeight:400px; scroll:no; status:no; help:no; resizable:yes; center:yes';
  else
    sFeatures = 'dialogWidth:550px; dialogHeight:400px; scroll:no; status:no; help:no; resizable:yes; center:yes';
  var sTitle = "�û�ѡ��";
  var oReturn = openModalDialog(sURL, vArguments, sFeatures, sTitle);
  return oReturn;
}


//����ѡ������û�ģ��ĺ���
//iSelectType:��ѡ����û�����(1-����,2-������,3-���˺ͷ�����)
//sSelectedUserXML: ��ǰѡ�еĽڵ��XML�����ڻָ�ѡ��״̬
//bAllowMultiSelect: �Ƿ�����ѡ�����û���Ĭ��Ϊtrue
//bAllowNotSelect: �Ƿ���Բ�ѡ���κ��û���Ĭ��Ϊtrue
function SelectGroupUser(iSelectType, sSelectedUserXML,  bAllowMultiSelect, bAllowNotSelect){ 
  var oParams = new Object();
  iSelectType = parseInt(iSelectType);
  if (isNaN(iSelectType)) iSelectType = 3;
  if (bAllowMultiSelect == null) bAllowMultiSelect = true;
  if (bAllowNotSelect == null) bAllowNotSelect = true;
  oParams.SelectType = iSelectType;
  oParams.SelectedUserXML = sSelectedUserXML;
  oParams.AllowMultiSelect = bAllowMultiSelect;
  oParams.AllowNotSelect = bAllowNotSelect;
  var sURL = "SelectGroupUser.aspx";
  var vArguments = oParams;
  var sFeatures;
  if (!bAllowMultiSelect)
    sFeatures = 'dialogWidth:250px; dialogHeight:400px; scroll:no; status:no; help:no; resizable:yes; center:yes';
  else
    sFeatures = 'dialogWidth:550px; dialogHeight:400px; scroll:no; status:no; help:no; resizable:yes; center:yes';
  var sTitle = "�û�ѡ��";
  var oReturn = openModalDialog(sURL, vArguments, sFeatures, sTitle);
  return oReturn;
}


function openModalDialog(sURL, vArguments, sFeatures, sTitle){
  var sURL = 'openModalDialog.aspx?DialogURL=' + escape(sURL) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();
//  alert(sURL);
  var oArguments = new Object();
  if (top.oTop != null)  //�����ModalDialog���ٵ���ModalDialogʱ����
    oArguments["top"] = top.oTop;
  else
    oArguments["top"] = top;
  oArguments["Arguments"] = vArguments;
  var vReturn = showModalDialog(sURL, oArguments, sFeatures);
  return vReturn;
}


function openModelessDialog(sURL, vArguments, sFeatures, sTitle){
  var sURL = 'openModalDialog.aspx?DialogURL=' + escape(sURL) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();
//  alert(sURL);
  var oArguments = new Object();
  if (top.oTop != null)  //�����ModalDialog���ٵ���ModalDialogʱ����
    oArguments["top"] = top.oTop;
  else
    oArguments["top"] = top;
  oArguments["Arguments"] = vArguments;
  var vReturn = showModelessDialog(sURL, oArguments, sFeatures);
  return vReturn;
}


//�ڵ�����ģ̬�����д�ָ���Ĺ���
//sFunctionID: �����ù��ܵĹ��ܱ��
//sInParamXML: ���������XML�ַ���
//sTitle: �������ڵı���
//sFeatures: ���ƴ�����۵���������
//����ֵ���������ܵ������������
function openModalFunction(sFunctionID, sInParamXML, sTitle, sFeatures){
  var InParams = new ParamClass();
  InParams.SetValue('FunctionID', sFunctionID);
  var sURL = Call('GetFunctionURL', InParams, ''); 
  sURL = 'openModalFunction.aspx?DialogURL=' + escape(sURL) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();
  var InParams = new ParamClass(sInParamXML);
  var oArguments = new Object();
  if (top.oTop != null)  //�����ModalDialog���ٵ���ModalDialogʱ����
    oArguments["top"] = top.oTop;
  else
    oArguments["top"] = top;
  if (sFeatures == null || sFeatures == ''){
	sFeatures = InParams.GetValue('DialogHeight') == '' ? 'dialogHeight:500px; ' : 'dialogHeight:' + InParams.GetValue('DialogHeight') + 'px;';
	sFeatures += InParams.GetValue('DialogWidth') == '' ? 'dialogWidth:750px; ' : 'dialogWidth:' + InParams.GetValue('DialogWidth') + 'px;';
	  
    sFeatures += 'scroll:no; status:no; help:no; resizable:yes; center:yes';
	InParams.Remove();
  }

  var vArguments = ToModalDialogParams(InParams, sFunctionID);
  oArguments["Arguments"] = vArguments;
  var sReturn = showModalDialog(sURL, oArguments, sFeatures);
  var OutParams = new ParamClass(sReturn);
  return OutParams;
}


//�ڵ����ķ�ģ̬�����д�ָ���Ĺ���
//sFunctionID: �����ù��ܵĹ��ܱ��
//sInParamXML: ���������XML�ַ���
//sTitle: �������ڵı���
//sFeatures: ���ƴ�����۵���������
//����ֵ�� �������Ĵ��ڶ���
function openModelessFunction(sFunctionID, sInParamXML, sTitle, sFeatures){
  var InParams = new ParamClass();
  InParams.SetValue('FunctionID', sFunctionID);
  var sURL = Call('GetFunctionURL', InParams, ''); 
  sURL = 'openModalFunction.aspx?DialogURL=' + escape(sURL) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();  
  var InParams = new ParamClass(sInParamXML);
  var oArguments = new Object();
  if (top.oTop != null)  //�����ModalDialog���ٵ���ModalDialogʱ����
    oArguments["top"] = top.oTop;
  else
    oArguments["top"] = top;
  if (sFeatures == null || sFeatures == ''){
	sFeatures = InParams.GetValue('DialogHeight') == '' ? 'dialogHeight:500px; ' : 'dialogHeight:' + InParams.GetValue('DialogHeight') + 'px;';
	sFeatures += InParams.GetValue('DialogWidth') == '' ? 'dialogWidth:750px; ' : 'dialogWidth:' + InParams.GetValue('DialogWidth') + 'px;';
	  
    sFeatures += 'scroll:no; status:no; help:no; resizable:yes; center:yes';
	InParams.Remove();
  }

  var vArguments = ToModalDialogParams(InParams, sFunctionID);
  oArguments["Arguments"] = vArguments;
  var vReturn = showModelessDialog(sURL, oArguments, sFeatures);
  return vReturn;
}


function openQueryForm(sFunctionID, sInParamXML, sQueryFunctionID, sTitle, sFeatures){
//  var InParams = new ParamClass();
//  InParams.SetValue('FunctionID', sFunctionID);
//  var sURL = Call('GetFunctionURL', InParams, ''); 
//  sURL = 'openQueryFrom.aspx?DialogURL=' + escape(sURL) + '&QueryFunctionID=' + escape(sQueryFunctionID) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();
  sURL = 'openQueryForm.aspx?FunctionID=' + escape(sFunctionID) + '&QueryFunctionID=' + escape(sQueryFunctionID) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();
  if (sFeatures == null || sFeatures == '')
    sFeatures = 'dialogHeight:500px; dialogWidth:750px; scroll:no; status:no; help:no; resizable:yes; center:yes';
  var oArguments = new Object();
  if (top.oTop != null)  //�����ModalDialog���ٵ���ModalDialogʱ����
    oArguments["top"] = top.oTop;
  else
    oArguments["top"] = top;

  var InParams = new ParamClass(sInParamXML);
  var vArguments = ToModalDialogParams(InParams, sFunctionID);
  oArguments["Arguments"] = vArguments;
  var vReturn = showModalDialog(sURL, oArguments, sFeatures);
  return vReturn;
}


//�������������ת����openModalDialog������Arguments������ʽ
function ToModalDialogParams(InParams, FunctionID){
  var xmlDoc = NewXmlDoc();
  var oRootNode = xmlDoc.createElement('Params');
  xmlDoc.appendChild(oRootNode);
  if (FunctionID == null) FunctionID = '';
  var oNode;
  oNode = xmlDoc.createElement('Item');
  oNode.setAttribute('Name', 'FunctionID');
  oNode.setAttribute('Value', FunctionID);
  oRootNode.appendChild(oNode);
  oNode = xmlDoc.createElement('Item');
  oNode.setAttribute('Name', 'InParamXML');
  oNode.setAttribute('Value', InParams.ToString());
  oRootNode.appendChild(oNode);
  return xmlDoc.xml;
}


//��λ��vURLָ���ĵ�ַ
//vURL������URL��ַ�ַ�����Ҳ�����ǰ���URL��ַ����������Ķ���
//bSaveURL �Ƿ�ѵ�ַ�����ڡ�ǰ���������ˡ�����ʷ��¼�У�������(true-����  false-������)��Ĭ��Ϊtrue����sFrameName������"ifrMain"ʱbSaveURLʼ��Ϊfalse
//sFrameName�� ����ָ�����ĸ�Frame�д򿪣�����ʡ�Ի򣬿���Ϊnull������Ϊ���ַ�����ʡ��ΪifrMain
����������������//���Ҫ�ڵڶ��㵼�����д򿪣�sFrameName����Ϊ"ifrMainNavigation"
function LocateTo(vURL, bSaveURL, sFrameName){
  var bInIfrMain = true;  //bInIfrMain��ʾ�Ƿ���ifrMain�д򿪵�
  if (sFrameName == null || sFrameName == '') sFrameName = 'ifrMain';
  if (sFrameName != 'ifrMain') bInIfrMain = false;
  if (!bInIfrMain){
	bSaveURL = false;  
  }
  else {
    try{
       ShowLoadingHint();
    }catch(err){}
  }
  if (bSaveURL !== false && vURL != top.aHistoryBack[top.aHistoryBack.length-1]){  //��ֹͬһ��ַ��������μ��뵽������ʷ
    top.aHistoryBack.push(vURL);
	top.aHistoryForward.splice(0, top.aHistoryForward.length);  //�����ǰ������ʷ��¼
  }

  if (typeof(vURL) == 'object'){   //�ӵ������򹤾������ô˹��ܵġ�      
    if (vURL.Params != null){   //POST����
      var oHTMLForm = top.ifrPost.document.all.frmLcation;
	  oHTMLForm.target = sFrameName;
 	  oHTMLForm.action = vURL.URL;
      oHTMLForm.innerHTML = vURL.Params;
      oHTMLForm.submit();
	  oHTMLForm.target = 'ifrMain';
    }
    else {   //GET��ʽ
	  frames[sFrameName].location.href = vURL.URL;
    }

	if (vURL.NavigationItem != null)
	{
	  try
	  {
	    if(vURL.NavigationItem.ParentNode==null)
	    {
	      if (!vURL.NavigationItem.Expanded)
	        vURL.NavigationItem.Expand();
	    }
	    else
	    {
	      if (!vURL.NavigationItem.ParentNode.Expanded)
	        vURL.NavigationItem.ParentNode.Expand();
	      if (!vURL.NavigationItem.Selected)
	        vURL.NavigationItem.Select(false);  //falseΪ�������¼�
	    }
	  }
	  catch(Ex)
	  {
	  }
	}
  }
  else {
	  frames[sFrameName].location.href = vURL;
  }

  top.SetBackForwardButtonState();
}


//ת��ָ������ 
//sFunctionID: ���ܱ��
//sInParamXML: ���ܵ���������ַ���,����ʡ�Ի�Ϊ���ַ���
//sTitle: ת��ָ�����ܺ���ʾ�ı���,����ʡ�Ի�Ϊ���ַ���
//sFrameName�� ����ָ�����ĸ�Frame�д򿪣�����ʡ�Ի򣬿���Ϊnull������Ϊ���ַ�����ʡ��ΪifrMain
����������������//���Ҫ�ڵڶ��㵼�����д򿪣�sFrameName����Ϊ"ifrMainNavigation"
function LocateToFunction(sFunctionID, sInParamXML, sTitle, sFrameName){
  if (sFrameName == null || sFrameName == '') sFrameName = 'ifrMain';
  if (sInParamXML == null) sInParamXML = '';
  var xmlDoc = NewXmlDoc();
  var oRootNode = xmlDoc.createElement('Params');
  oRootNode.setAttribute('FunctionID', sFunctionID);
  if (sInParamXML != null)
    oRootNode.setAttribute('InParamXML', sInParamXML);
	
  if (sFrameName == 'ifrMain'){  //������ifrMain��ʱ������sTitle
    if (sTitle != null)
      oRootNode.setAttribute('MainTitle', sTitle);
  }
  xmlDoc.appendChild(oRootNode);
  
  var InParams = new ParamClass();
  InParams.SetValue('FunctionID', sFunctionID);
  var Result = Call('GetFunctionURL', InParams, '��ȡ��������Ӧ��URLʱ����'); 
  if (Result === false) return;
  sURL = Result;
  var vURL = top.GetPostFormParams(sURL, xmlDoc);
  LocateTo(vURL, true, sFrameName);
}



//ת��ָ��ҳ�� 
//sURL: ҳ���ļ���URL��ַ
//sInParamXML: ���ܵ���������ַ���,����ʡ�Ի�Ϊ���ַ���
//sFrameName�� ����ָ�����ĸ�Frame�д򿪣�����ʡ�Ի򣬿���Ϊnull������Ϊ���ַ�����ʡ��ΪifrMain
����������������//���Ҫ�ڵڶ��㵼�����д򿪣�sFrameName����Ϊ"ifrMainNavigation"
function LocateToPage(sURL, sInParamXML, sFrameName){
  if (sFrameName == null || sFrameName == '') sFrameName = 'ifrMain';
  if (sInParamXML == null) sInParamXML = '';
  if (sInParamXML == ''){
    LocateTo(sURL, true, sFrameName);
	return;  
  }
  var xmlDoc = NewXmlDoc();
  var oRootNode = xmlDoc.createElement('Params');
  oRootNode.setAttribute('InParamXML', sInParamXML);
  xmlDoc.appendChild(oRootNode);
  var vURL = top.GetPostFormParams(sURL, xmlDoc);
  LocateTo(vURL, true, sFrameName);
}

//XML��ȷ����֤
//���bNoErrorAlert����true������ʾ�κδ�����ʾ��Ϣ
//���sXMLΪnull����ַ�������ʾsShowMessage��Ϣ
//���sXML�ĸ��ڵ�ΪError����ʾ���ڵ��Message���Ե���Ϣ(������ʾ��Ϣ)
//���sXML��Ϊnull����ַ���������sXML�ĸ��ڵ㲻ΪError������ֵΪtrue,����Ϊfalse
function XMLValidate(sXML, sShowMessage, bNoErrorAlert){
  if (sXML == null || sXML == ''){
    sShowMessage += '\n��������Ϊ�������Ӳ������������״̬��ʧ�������µ�¼��';
	if (bNoErrorAlert != true)
      alert(sShowMessage);
	return false;
  }
  
  var xmlDoc = NewXmlDoc();
  if (!xmlDoc.loadXML(sXML)){  //sXML���ǽṹ��ȷ��XML
    sShowMessage += '\n��������Ϊ���ݴ������';
	if (bNoErrorAlert != true)
      alert(sShowMessage);
	return false;
  }
  
  if (xmlDoc.documentElement.nodeName == 'Error'){
    sShowMessage += '\n������Ϣ��\n\n' + xmlDoc.documentElement.getAttribute("Message");
	if (bNoErrorAlert != true)
      alert(sShowMessage);
    return false;
  }
  
  return true;
}


function SetGlobalValue(sName, sValue){
  top.Global[sName] = sValue;
}


function GetGlobalValue(sName){
  return top.Global[sName];
}


//ȥ���ַ���ǰ��Ŀո�
String.prototype.trim = function(){
  return this.replace(/(^\s*)|(\s*$)/g, "");
}


//���ַ����е�ϵͳ������(��:"&ProjectID&")�滻����Ӧ��ֵ(Global.ProjectID)
String.prototype.ReplaceVar = function(){
  return this.replace(/%([^\s%]+)%/g, 
	function ($0, $1){
	var sName = $1;
	var sValue = '';
	switch (sName){
  	  case 'SYS_USERNAME':
	    sValue = top.UserName;
		break;
	  case 'SYS_USERNAMEC':
	    sValue = top.UserNameC;
	    break;
	  case 'SYS_ROLE':
	    sValue = top.Role;
	    break;
	  case 'SYS_DEPARTMENT':
	    sValue = top.Department;
	    break;
	}
	if (sValue != '') return sValue;
	try{
	  return top.ifrMain.OutParams.GetValue($1);
	}
	catch(err){
	  return this
	}
  }
  );
}

//��ʽ���ַ���(��:Format('{0}+{1}={2}', 'a', 'b', 'ab'))
function Format(){
  var i, iCount;
  var str = arguments[0];
  iCount = arguments.length - 1;
  for (i=0; i<iCount; i++){
    var reg = new RegExp('\\{'+i+'\\}', 'g');
    str = str.replace(reg, arguments[i+1]);   
  }
  return str;
}

//�õ�ÿҳ��ʾ��¼��
function GetPageSize(){
  var sPageSize = "10";
  var oListPageSize = top.SetupDataXmlDoc.documentElement.selectSingleNode("//ListState");
  if (oListPageSize == null)
  {
	 oListPageSize = top.SetupDataXmlDoc.createElement("ListState");
	 top.SetupDataXmlDoc.documentElement.appendChild(oListPageSize);
  }
  else
  {
	var oNode = oListPageSize.getAttribute("PageSize");
	if (oNode == null || oNode == "")
	{
	  oListPageSize.setAttribute("PageSize","10");
	  sPageSize = "10";
	}
	else
	  sPageSize = oNode;
  }
  return sPageSize;
}

//�������ֵ���XML
//��sSourceXMLȥ���sTargetXML
function GetFilledParamXML(sTargetXML, sSourceXML){
  var oXmlDoc_Target = NewXmlDoc();
  var oXmlDoc_Source = NewXmlDoc();
  if (!oXmlDoc_Target.loadXML(sTargetXML)) return sTargetXML;
  if (!oXmlDoc_Source.loadXML(sSourceXML)) return sTargetXML;
  var i, iCount, sParamName, sInputType, sValueType, sValue, oNode, oNodeList, oSourceNode;
  oSourceRootNode = oXmlDoc_Source.documentElement;
  oNodeList = oXmlDoc_Target.documentElement.childNodes;
  iCount = oNodeList.length;
//alert('������������� iCount:' + iCount);  
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
    sValue = oNode.getAttribute('ֵ');
    var re = /^%([^\s%]+)%$/;
	sParamName = sValue.replace(re, function ($0, $1){return $1});
	if (sParamName == sValue) continue;   //���������������ʾ"ֵ"���Բ���"%xxxx%"�ĸ�ʽ
	var sXPath = './*[@����="' + sParamName + '"]';
	oSourceNode = oSourceRootNode.selectSingleNode(sXPath);
	if (oSourceNode == null) continue;   //û�ҵ���Ӧ����������ڵ�
	sValue = oSourceNode.getAttribute('ֵ');
//	alert(sValue);
	if (sValue == null) sValue = '';
	oNode.setAttribute('ֵ', sValue);
	oNode.setAttribute('��ʵ����', '��');   //��־Ϊ��ʵ����
  }
  var sReturn = oXmlDoc_Target.xml;
  oXmlDoc_Target = null;
  oXmlDoc_Source = null;
  return sReturn;
}

//�����������Ƿ�Ҳ��ȫʵ��������������򵯳����봰���û�����
//sParamXML: ����XML
//bNoCheckValue: �����ͣ�Ϊtrueʱ����������ֵ(�������������ͣ���ʹ������ֵ���ǵ�����������ֵ)
function CompleteInParams(sParamXML, bNoCheckValue){ 
  var xmlDoc = NewXmlDoc();
  if (!xmlDoc.loadXML(sParamXML)) return false;
  var i, iCount, sAttrName, sInputType, sValueType, sValue, oNode, oNodeList;
  oNodeList = xmlDoc.documentElement.selectNodes('./����[not(@��ʵ���� and @��ʵ����="��")]');
  iCount = oNodeList.length;
  if (iCount == 0) return sParamXML;   //���в����������ֵ��ʵ����
  //ѭ�����л�ûʵ����ֵ�Ĳ�������ϵͳ��������ʵ��������ҵ�����Ĳ���ת����ö������
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
	sName = oNode.getAttribute('����');
	sInputType = oNode.getAttribute('��������');
	sValueType = oNode.getAttribute('����');
	sValue = oNode.getAttribute('ֵ');
	if (sValue == null) sValue = '';
	sValue = sValue.toUpperCase();
	if (sValue == '%EMPTYSTRING%'){        //ֵ��ʵ����Ϊ�շ���
      oNode.setAttribute('ֵ', '');
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sName.substr(0,3) == 'FN:'){
	  var sFuncName = 'oNode = ' + sName.substr(3) + '(oNode)';
	  try{
	    eval(sFuncName);
	  }
	  catch(err){
	    alert(err.message);
	  }
	}
	else if (sInputType == '��֯���������ʶ'){
      oNode.setAttribute('ֵ', top.JGDM);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sInputType == '�û���' || sValue == '%SYS_USERNAME%'){
      oNode.setAttribute('ֵ', top.UserName);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sValue == '%SYS_USERNAMEC%'){
      oNode.setAttribute('ֵ', top.UserNameC);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sInputType == '��ɫ' || sValue == '%SYS_ROLE%'){
      oNode.setAttribute('ֵ', top.Role);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sInputType == '����' || sValue == '%SYS_DEPARTMENT%'){
      oNode.setAttribute('ֵ', top.Department);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sInputType == 'ϵͳ��' || sValue == '%SYS_SYSTEMDBNAME%'){
      oNode.setAttribute('ֵ', top.SystemDBName);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sInputType == '������' || sValue == '%SYS_WORKDBNAME%'){
      oNode.setAttribute('ֵ', top.WorkDBName);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sInputType == '��Դ��' || sValue == '%SYS_ARCHIVEDBNAME%'){
      oNode.setAttribute('ֵ', top.ArchiveDBName);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sInputType == '���տ�' || sValue == '%SYS_RECYCLEDBNAME%'){
      oNode.setAttribute('ֵ', top.RecycleDBName);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sInputType == '��������' || sValue == '%SYS_JGDM%'){
      oNode.setAttribute('ֵ', top.JGDM);
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sValue != '' && bNoCheckValue != true){       //�������Ӧ���������ж�"ֵ"������֮��
      oNode.setAttribute('��ʵ����', '��');
	}
	else if (sValueType == 'ҵ�����'){  //��ҵ��������͵ģ�ת����ö������
	  var Result = Call('GetProjectFlag');
	  var tmpXmlDoc = new NewXmlDoc();
	  tmpXmlDoc.loadXML(Result);
	  if (tmpXmlDoc.documentElement.childNodes.length == 1){
	    oNode.setAttribute('ֵ', tmpXmlDoc.documentElement.firstChild.getAttribute('Value'));
        oNode.setAttribute('��ʵ����', '��');
	  }
	  else {
	    oNode.setAttribute('ֵѡ��', tmpXmlDoc.xml);
        oNode.setAttribute('����', 'ö��');
	  }
	  tmpXmlDoc = null;
	}
	else if (sValueType == '��������' || sValueType == '������ɫ'){
	  var sValue = sValueType=='��������'?top.Department:top.Role;
	  var arr = sValue.split(',');
	  if (arr.length <= 1){  //�����ڶ�����Ż��ɫ
	    oNode.setAttribute('ֵ', top.Department);
        oNode.setAttribute('��ʵ����', '��');
	  }
	  else {
	    var tmpXmlDoc = NewXmlDoc();
		var oRootNode = tmpXmlDoc.createElement('Items');
		tmpXmlDoc.appendChild(oRootNode);
		for (var j=0; j<arr.length; j++){
		  var tmpNode = tmpXmlDoc.createElement('Item');
		  tmpNode.setAttribute('Text', arr[j].trim());
		  tmpNode.setAttribute('Value', arr[j].trim());
		  oRootNode.appendChild(tmpNode);
		}
	    oNode.setAttribute('ֵѡ��', tmpXmlDoc.xml);
        oNode.setAttribute('����', 'ö��');
	  }
	}
	else{
      oNode.setAttribute('ֵ', '');
      oNode.setAttribute('��ʵ����', '��');	
	}
  }
  oNodeList = xmlDoc.documentElement.selectNodes('./����[not(@��ʵ����)]');
  if (oNodeList.length == 0) return xmlDoc.xml;   //���в����������ֵ��ʵ����
  if (bNoCheckValue)  //
    return xmlDoc.xml; //�Ƕ��帡�����ںͿ�ݷ�ʽ��
  else
    return ParamsInputWindow(xmlDoc.xml,bNoCheckValue);
}

function ParamsInputWindow(ParamXML,bNoCheckValue){
  var sURL = 'InputParam.aspx';
  var vArguments = new Object();
  vArguments.ParamXML = ParamXML;
  if (bNoCheckValue == true)     
    vArguments.CanNoValue = true;   //ֵ����Ϊ��(������ѡ����м����ѡ��)
  else
    vArguments.CanNoValue = false;
  var sFeatures = 'dialogHeight:187px; dialogWidth:308px; scroll:no; status:no; help:no; resizable:no; center:yes';
  var sTitle = "���������ֵ";
  var tmpXmlDoc = openModalDialog(sURL, vArguments, sFeatures, sTitle);
  if (tmpXmlDoc == null)
    return false;
  else
    return tmpXmlDoc.xml;
}

//һ��������Ϣ����
//ʹ�÷���: 
//  var oParam = new ParamClass(sParamXML)
//  var sValue = oParam.GetValue(sName)
//  oParam.SetValue(sName, sValue)
function ParamClass(sParamXML, PostBackName){
  var xmlDoc = new ActiveXObject("Msxml2.DOMDocument");
  xmlDoc.async = false;
  this.PostBackName = PostBackName;
  this.XmlDoc = xmlDoc;
  if (!xmlDoc.loadXML(sParamXML)){
    var oNode = xmlDoc.createElement('Params');
	xmlDoc.appendChild(oNode);
  }
  
  //�����ParamNameָ���Ĳ�����ֵ��һ������(��XML��ʾ)����iIndexָ��ȡ�����еĵڼ���ֵ�����ֵ���Ǽ��ϻ�iIndex����һ����Ч���±꣬�򷵻�����"ֵ"����
  this.GetValue = function (ParamName, iIndex){
    var xmlDoc = this.XmlDoc;
    var oNode = xmlDoc.documentElement.selectSingleNode('./*[@����="' + ParamName + '"]');
	if (oNode == null)
	  return '';
	else {
	  var sValue = oNode.getAttribute('ֵ');
	  var newDoc = NewXmlDoc();
	  if (!newDoc.loadXML(sValue)) return sValue;  //"ֵ"����XML����������ֵ
	  if (newDoc.documentElement.nodeName != 'MultiValues') return sValue;  //���Ǳ�ʾ�ж��ֵ��XML
	  if (isNaN(parseInt(iIndex))) return sValue; //iIndex��������
	  var tmpNode = newDoc.documentElement.childNodes[iIndex];
	  if (tmpNode == null) return sValue; //iIndex������Ч���±�
	  return tmpNode.text;
	}
  };
  
  //��ȡָ��������ֵ�ĸ���(�൱��ȡ�б���ѡ�е�����)���ô�: �����ѯ�б��ID����Ϊ������������б���ѡ�ж��У�����ID��Ӧ��ֵ���ж����
  this.GetValueCount = function (ParamName){
    var xmlDoc = this.XmlDoc;
    var oNode = xmlDoc.documentElement.selectSingleNode('./*[@����="' + ParamName + '"]');
	if (oNode == null)
	  return 0;
	else {
	  var sValue = oNode.getAttribute('ֵ');
	  var newDoc = NewXmlDoc();
	  if (!newDoc.loadXML(sValue)) return 1;  //"ֵ"����XML
	  if (newDoc.documentElement.nodeName != 'MultiValues') return 1;  //���Ǳ�ʾ�ж��ֵ��XML
	  return newDoc.documentElement.childNodes.length;
	}
  }
  
  this.SetValue = function (ParamName, ParamValue){
    var xmlDoc = this.XmlDoc;
    var oNode = xmlDoc.documentElement.selectSingleNode('./*[@����="' + ParamName + '"]');
	if (oNode == null){
	  oNode = xmlDoc.createElement('����');        
	  xmlDoc.documentElement.appendChild(oNode);
	  oNode.setAttribute('����', ParamName);
	}
    oNode.setAttribute('ֵ', ParamValue);
	if (this.PostBackName != null && document.all[this.PostBackName] != null)
	  document.all[this.PostBackName].value = this.ToString();
  };
  
  this.Remove = function (ParamName){
    var xmlDoc = this.XmlDoc;
    var oNode = xmlDoc.documentElement.selectSingleNode('./*[@����="' + ParamName + '"]');
	if (oNode != null){
	  xmlDoc.documentElement.removeChild(oNode);
	  if (this.PostBackName != null && document.all[this.PostBackName] != null)
	    document.all[this.PostBackName].value = this.ToString();
	}
  }
  
  this.Count = function (){
    var xmlDoc = this.XmlDoc;
    return xmlDoc.documentElement.childNodes.length;
  }
  
  this.ToString = function (){
    return this.XmlDoc.xml;
  }
  
  this.Clear = function (){
    var iCount = this.XmlDoc.documentElement.childNodes.length;
    for (var i=0; i<iCount; i++){
	  this.XmlDoc.documentElement.removeChild(this.XmlDoc.documentElement.lastChild);
	}
  }
}

//�������ֵ�����е�ÿ��ֵ�Ƿ����
function IsEqualValue(str){
  var xmlDoc = NewXmlDoc();
  if (!xmlDoc.loadXML(str)) return true;  //"ֵ"����XML������true
  if (xmlDoc.documentElement.nodeName != 'MultiValues') return true;  //���Ǳ�ʾ�ж��ֵ��XML
  var iCount = xmlDoc.documentElement.childNodes.length;
  if (iCount <= 1) return true;
  var sValue = xmlDoc.documentElement.childNodes[0].text;
  for (var i=1; i<iCount; i++){
	var tmp = xmlDoc.documentElement.childNodes[i].text;
	if (tmp != sValue) return false;
  }
  return true;
}

//�������ֵ�����е�ÿ��ֵ���ʱ��ȡ��һֵ
function GetFirstValue(str){
  var xmlDoc = NewXmlDoc();
  if (!xmlDoc.loadXML(str)) return str;  //"ֵ"����XML������false
  if (xmlDoc.documentElement.nodeName != 'MultiValues') return str;  //���Ǳ�ʾ�ж��ֵ��XML
  var iCount = xmlDoc.documentElement.childNodes.length;
  if (iCount <= 1) return false;
  var sValue = xmlDoc.documentElement.childNodes[0].text;
  for (var i=1; i<iCount; i++){
	var tmp = xmlDoc.documentElement.childNodes[i].text;
	if (tmp != sValue) return false;
  }
  return sValue;
}

//���������
//ʹ�÷���: 
//  var oClipBoard = new ClipBoardClass(sClipBoardXML)
//  var sValue = oClipBoard.GetClipBoardCount();
function ClipBoardClass(sClipBoardXML){
  var xmlDoc = new ActiveXObject("Msxml2.DOMDocument");
  xmlDoc.async = false;
  this.XmlDoc = xmlDoc;
  if (!xmlDoc.loadXML(sClipBoardXML))
  {
    var oNode = xmlDoc.createElement('CopyToClipboardXMLDataBank');
	xmlDoc.appendChild(oNode);
    Global["ClipboardXmlDoc"] = xmlDoc;
  }
  //ɾ����������XML�Ľڵ㣬���ڼ���
	this.DelClipBoardNode = function ()
	{
		var oNodeList,iListLength;
		var xmlDoc = this.XmlDoc;
		if(xmlDoc != null)
		{
			oNodeList=ClipboardXmlDoc.documentElement.selectNodes('//Row');
			iListLength=oNodeList.length;
			if(iListLength>0)
			{
				for(i=0;i<iListLength;i++)
				{
					var cFileType=oNodeList[i].getAttribute('FileType');
					var oNode = ClipboardXmlDoc.selectSingleNode('//Row[@FileType="' + cFileType + '"]');
					if (oNode != null)
					{
						oNode.parentNode.removeChild(oNode);
					}		
				}		 
			}      
		}	
	}
  //��ȡ�������м��л��Ƽ�¼�ĳ���
    this.GetClipBoardCount = function ()
	{
	   return this.XmlDoc.documentElement.childNodes.length;	   
   }
}

function GetWorkTableID(TableName, TableType, FieldName, FieldValue){
  var InParams = new ParamClass();
  InParams.SetValue('TableName', TableName);
  InParams.SetValue('TableType', TableType);
  InParams.SetValue('FieldName', FieldName);
  InParams.SetValue('FieldValue', FieldValue);
  var Result = Call('GetWorkTableID', InParams, false); 
  if (Result === false) return -1;
  return Result;
}

function MoneyToChinese(num)
{
  if (isNaN(num) || num > Math.pow(10, 12)) return "";
  var cn = "��Ҽ��������½��ƾ�";
  var unit = new Array("ʰ��Ǫ", "�ֽ�");
  var unit1= new Array("����", "");
  var str = num.toString();
  var numArray = str.split(".");
  var intpart = numArray[0];
  var decimal = numArray[1];
  intpart = intpart.replace(/^0+/g, '');
  if (intpart == '') intpart = '0';
  if (decimal == null || decimal == '') decimal = '0';
  if (decimal.length > 2) decimal = decimal.substr(0, 2);
  decimal = decimal.replace(/^0*$/g, '0');
  if (intpart == '0' && decimal == '0') return '��Բ��';
  numArray[0] = intpart;
  numArray[1] = decimal;
  var start = new Array(numArray[0].length-1, 2);

  function toChinese(num, index) 
  {
    var num = num.replace(/\d/g, function ($1){return cn.charAt($1)+unit[index].charAt(start--%4 ? start%4 : -1)});
    return num;
  }

  for (var i=0; i<numArray.length; i++)
  {
    var tmp = "";
    for (var j=0; j*4<numArray[i].length; j++)
    {
      var strIndex = numArray[i].length-(j+1)*4;
      var str = numArray[i].substring(strIndex, strIndex+4);
      var start = i ? 2 : str.length-1;
      var tmp1 = toChinese(str, i);
      tmp1 = tmp1.replace(/(��.)+/g, "��").replace(/��+$/, "");
      tmp1 = tmp1.replace(/^Ҽʰ/, "ʰ");
      tmp = (tmp1+unit1[i].charAt(j-1)) + tmp;
    }
    numArray[i] = tmp;
  }

  numArray[1] = numArray[1] ? numArray[1] : "";
  numArray[0] = numArray[0] ? numArray[0]+"Բ" : numArray[0], numArray[1] = numArray[1].replace(/^��+/, "");
  numArray[1] = numArray[1].match(/��/) ? numArray[1] : numArray[1]+"��";

  var str = numArray[0]+numArray[1];

  if (intpart.length > 8){
    if (intpart.substr(intpart.length-8,4) == '0000'){
          if (intpart.substr(intpart.length-4,4) == '0000' || intpart.substr(intpart.length-4,1) == '0'){
            str = str.replace(/��/, '');
          }
          else {
        str = str.replace(/��/, '��');
          }
    }
  }
  return str;
}

//����ת���ɴ�д
function NumberToChinese(num)
{
  if (isNaN(num) || num > Math.pow(10, 12)) return "";
  var cn = "��Ҽ��������½��ƾ�";
  var unit = new Array("ʰ��Ǫ", "");
  var unit1= new Array("����", "");
  var str = num.toString();
  var numArray = str.split(".");
  var intpart = numArray[0];
  var decimal = numArray[1];
  intpart = intpart.replace(/^0+/g, '');
  if (intpart == '') intpart = '0';
  if (decimal == null || decimal == '') decimal = '0';
  if (decimal.length > 2) decimal = decimal.substr(0, 2);
  decimal = decimal.replace(/^0*$/g, '0');
  if (intpart == '0' && decimal == '0') return '��';
  numArray[0] = intpart;
  numArray[1] = decimal;
  var start = new Array(numArray[0].length-1, 2);

  function toChinese(num, index) 
  {
    var num = num.replace(/\d/g, function ($1){return cn.charAt($1)+unit[index].charAt(start--%4 ? start%4 : -1)});
    return num;
  }

  for (var i=0; i<numArray.length; i++)
  {
    var tmp = "";
    for (var j=0; j*4<numArray[i].length; j++)
    {
      var strIndex = numArray[i].length-(j+1)*4;
      var str = numArray[i].substring(strIndex, strIndex+4);
      var start = i ? 2 : str.length-1;
      var tmp1 = toChinese(str, i);
      if (i==1)
      {
        tmp = tmp1;
      }
      else
      {
          tmp1 = tmp1.replace(/(��.)+/g, "��").replace(/��+$/, "");
          tmp1 = tmp1.replace(/^Ҽʰ/, "ʰ");
          tmp = (tmp1+unit1[i].charAt(j-1)) + tmp;
      }
    }
    numArray[i] = tmp;
  }

  numArray[1] = numArray[1] ? numArray[1] : "";
  numArray[0] = numArray[0] ? numArray[0]: numArray[0], numArray[1] = numArray[1].replace(/^��+/, "");
  numArray[1] = numArray[1].match(/��/) ? numArray[1] : numArray[1];
  var str = "";
  if (numArray[0] == "")
  {
      if (tmp == "��")
        str = "";
      else
        str = "���"+tmp;
  }
  else
  {
    str = numArray[0];
    if (tmp != "��")
        str = str+"��"+tmp;
  }

  if (intpart.length > 8){
    if (intpart.substr(intpart.length-8,4) == '0000'){
          if (intpart.substr(intpart.length-4,4) == '0000' || intpart.substr(intpart.length-4,1) == '0'){
            str = str.replace(/��/, '');
          }
          else {
        str = str.replace(/��/, '��');
          }
    }
  }
  return str;
}

//ͳ��Grid��һ������
//oGrid:Grid����
//sFiled:��GridҪͳ�Ƶ��ֶ�����
function StatGridCellNumber(oGrid,sFiled){
  var iNum = 0;
  var oRows = oGrid.Items;
  var i,iCount;
  iCount = oRows.length;
  for(i=0;i<iCount;i++){
    var sValue = parseInt(oRows[i][sFiled].Value);
    if (isNaN(sValue)) sValue = 0;
    iNum = iNum + sValue;
  }
  return iNum;
}


  //��ȡԪ��λ�õ���  
  var posLib = {
    getClientLeft:    function (el) {
      var r = el.getBoundingClientRect();
      return r.left - this.getBorderLeftWidth(this.getCanvasElement(el));
    },

    getClientTop:    function (el) {
      var r = el.getBoundingClientRect();
      return r.top - this.getBorderTopWidth(this.getCanvasElement(el));
    },

    getLeft:    function (el) {
      return this.getClientLeft(el) + this.getCanvasElement(el).scrollLeft;
    },

    getTop:    function (el) {
      return this.getClientTop(el) + this.getCanvasElement(el).scrollTop;
    },

    getInnerLeft:    function (el) {
      return this.getLeft(el) + this.getBorderLeftWidth(el);
    },

    getInnerTop:    function (el) {
      return this.getTop(el) + this.getBorderTopWidth(el);
    },

    getWidth:    function (el) {
      return el.offsetWidth;
    },

    getHeight:    function (el) {
      return el.offsetHeight;
    },

    getCanvasElement:    function (el) {
      var doc = el.ownerDocument || el.document;    // IE55 bug
      if (doc.compatMode == "CSS1Compat")
        return doc.documentElement;
      else
        return doc.body;
    },

    getBorderLeftWidth:    function (el) {
      return el.clientLeft;
    },

    getBorderTopWidth:    function (el) {
      return el.clientTop;
    },

    getScreenLeft:    function (el) {
      var doc = el.ownerDocument || el.document;    // IE55 bug
      var w = doc.parentWindow;
      return w.screenLeft + this.getBorderLeftWidth(this.getCanvasElement(el)) + this.getClientLeft(el);
    },

    getScreenTop:    function (el) {
      var doc = el.ownerDocument || el.document;    // IE55 bug
      var w = doc.parentWindow;
      return w.screenTop + this.getBorderTopWidth(this.getCanvasElement(el)) + this.getClientTop(el);
    }
  }


  
  //��ֵת����Boolean��
  //Value:Ҫת����ֵ�� bDefaultValue:Ĭ��ֵ
  function ToBoolean(Value, bDefaultValue){
    switch (typeof(Value)) {
      case "boolean" :
        return Value;
      case "string" : 
	    Value = Value.toLowerCase();
        if (Value == "true" || Value == "1") return true;
        if (Value == "false" || Value == "0") return false;
        return bDefaultValue;
      case "undefined" :
        return bDefaultValue;
      default :
        return bDefaultValue;
    }
  }

//�ж��ַ��Ƿ�Ϊ��ֵ
function IsNullString(Value)
{
    if (Value == "" || Value == null)
        return true;
    else
        return false;
}

//�ԶԻ���ʽ��URL ZhangAiChun
function openModalURL(sURL, sInParamXML, sTitle, sFeatures, sFunctionID)
{
  sURL = 'openModalFunction.aspx?DialogURL=' + escape(sURL) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();
  if (sFeatures == null || sFeatures == '')
    sFeatures = 'dialogHeight:500px; dialogWidth:750px; scroll:no; status:no; help:no; resizable:yes; center:yes';
  var oArguments = new Object();
  if (top.oTop != null)  //�����ModalDialog���ٵ���ModalDialogʱ����
    oArguments["top"] = top.oTop;
  else
    oArguments["top"] = top;

  var InParams = new ParamClass(sInParamXML);
  var vArguments = ToModalDialogParams(InParams, sFunctionID);
  oArguments["Arguments"] = vArguments;
  var sReturn = showModalDialog(sURL, oArguments, sFeatures);
  var OutParams = new ParamClass(sReturn);
  return OutParams;
}

//��ʾ���ڴ���
function ShowOperatingHint(){
  ShowHintCommon('../Images/Hint/Operating.gif');
}

//��ʾ���ڱ���
function ShowSavingHint(){
  ShowHintCommon('../Images/Hint/Loading.gif');
}

//��ʾ��������
function ShowLoadingHint(){
  ShowHintCommon('../Images/Hint/Loading.gif');
}

function ShowHintCommon(sImgURL){
  if (top.ifrHint.imgHint.src != sImgURL)
    top.ifrHint.imgHint.src = sImgURL;
  top.document.all.ifrHint.style.visibility = 'visible';
}

//�ϴ��ļ�
function fnUpFile(){
    var sURL = "UpFile.aspx?ImageTable="+arguments[0]+"&ImageField="+arguments[1]+"&ImageKeyField="+arguments[2]+
        "&ImageKeyFieldVal="+arguments[3]+"&LinkField="+arguments[4]+"&LinkFieldVal="+arguments[5];
    var tsTitle = '�ϴ��ļ�';
    var vArguments = this;
    var sFeatures = 'dialogHeight:200px; dialogWidth:260x; scroll:no; status:no; help:no; resizable:no; center:yes';
    var sReturn = openModalDialog(sURL, vArguments, sFeatures, tsTitle);
    if(typeof(sReturn) == "undefined")sReturn = "";
    return sReturn;
}

//������ʾ
function HideHint(){
  top.document.all.ifrHint.style.visibility = 'hidden';
}

/************************************ Base64���� ���� ���� **************************************/

/***======================================================================================***
            JavaScript�л�õ������ַ�����UTF16���б��룬��ͳһ��ҳ���׼��ʽΪUTF-8��
            ��������Ǵӷ��������յ���Base64���������ݣ����ڽ������Ҫת��ΪUTF16
            ����������������Base64�������ݣ���Ӧ��ת��ΪUTF-8�ٽ���Base64����
***=======================================================================================***/

function utf16to8(str) {
    var out, i, len, c;

    out = "";
    len = str.length;
    for(i = 0; i < len; i++) {
 c = str.charCodeAt(i);
 if ((c >= 0x0001) && (c <= 0x007F)) {
     out += str.charAt(i);
 } else if (c > 0x07FF) {
     out += String.fromCharCode(0xE0 | ((c >> 12) & 0x0F));
     out += String.fromCharCode(0x80 | ((c >>  6) & 0x3F));
     out += String.fromCharCode(0x80 | ((c >>  0) & 0x3F));
 } else {
     out += String.fromCharCode(0xC0 | ((c >>  6) & 0x1F));
     out += String.fromCharCode(0x80 | ((c >>  0) & 0x3F));
 }
    }
    return out;
}

function utf8to16(str) {
    var out, i, len, c;
    var char2, char3;

    out = "";
    len = str.length;
    i = 0;
    while(i < len) {
 c = str.charCodeAt(i++);
 switch(c >> 4)
 {
   case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7:
     // 0xxxxxxx
     out += str.charAt(i-1);
     break;
   case 12: case 13:
     // 110x xxxx   10xx xxxx
     char2 = str.charCodeAt(i++);
     out += String.fromCharCode(((c & 0x1F) << 6) | (char2 & 0x3F));
     break;
   case 14:
     // 1110 xxxx  10xx xxxx  10xx xxxx
     char2 = str.charCodeAt(i++);
     char3 = str.charCodeAt(i++);
     out += String.fromCharCode(((c & 0x0F) << 12) |
        ((char2 & 0x3F) << 6) |
        ((char3 & 0x3F) << 0));
     break;
 }
    }

    return out;
}

var base64EncodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
var base64DecodeChars = new Array(
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63,
    52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1,
    -1,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14,
    15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1,
    -1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
    41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1);
//Base64����
function base64encode(str) {
    var out, i, len;
    var c1, c2, c3;

    len = str.length;
    i = 0;
    out = "";
    while(i < len) {
 c1 = str.charCodeAt(i++) & 0xff;
 if(i == len)
 {
     out += base64EncodeChars.charAt(c1 >> 2);
     out += base64EncodeChars.charAt((c1 & 0x3) << 4);
     out += "==";
     break;
 }
 c2 = str.charCodeAt(i++);
 if(i == len)
 {
     out += base64EncodeChars.charAt(c1 >> 2);
     out += base64EncodeChars.charAt(((c1 & 0x3)<< 4) | ((c2 & 0xF0) >> 4));
     out += base64EncodeChars.charAt((c2 & 0xF) << 2);
     out += "=";
     break;
 }
 c3 = str.charCodeAt(i++);
 out += base64EncodeChars.charAt(c1 >> 2);
 out += base64EncodeChars.charAt(((c1 & 0x3)<< 4) | ((c2 & 0xF0) >> 4));
 out += base64EncodeChars.charAt(((c2 & 0xF) << 2) | ((c3 & 0xC0) >>6));
 out += base64EncodeChars.charAt(c3 & 0x3F);
    }
    return out;
}
//Base64����
function base64decode(str) {
    var c1, c2, c3, c4;
    var i, len, out;

    len = str.length;
    i = 0;
    out = "";
    while(i < len) {
 /* c1 */
 do {
     c1 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
 } while(i < len && c1 == -1);
 if(c1 == -1)
     break;

 /* c2 */
 do {
     c2 = base64DecodeChars[str.charCodeAt(i++) & 0xff];
 } while(i < len && c2 == -1);
 if(c2 == -1)
     break;

 out += String.fromCharCode((c1 << 2) | ((c2 & 0x30) >> 4));

 /* c3 */
 do {
     c3 = str.charCodeAt(i++) & 0xff;
     if(c3 == 61)
  return out;
     c3 = base64DecodeChars[c3];
 } while(i < len && c3 == -1);
 if(c3 == -1)
     break;

 out += String.fromCharCode(((c2 & 0XF) << 4) | ((c3 & 0x3C) >> 2));

 /* c4 */
 do {
     c4 = str.charCodeAt(i++) & 0xff;
     if(c4 == 61)
  return out;
     c4 = base64DecodeChars[c4];
 } while(i < len && c4 == -1);
 if(c4 == -1)
     break;
 out += String.fromCharCode(((c3 & 0x03) << 6) | c4);
    }
    return out;
}
/************************************ Base64���� ���� ���� (End)**************************************/

/*
 ��������ַ���ת��Ϊ���,��ɾ�����пո�
 input�� Str    �����ַ���
 output��DBCStr ����ַ���
 ˵����1��ȫ�ǿո�Ϊ12288����ǿո�Ϊ32
       2�������ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ�ǣ������65248
 */
function toTrimDBC(Str) {
    var DBCStr = "";
    for(var i=0; i<Str.length; i++){
        var c = Str.charCodeAt(i);
        if(c == 12288 ||c == 32) {
            continue;
        }
            if (c > 65280 && c < 65375) {
            DBCStr += String.fromCharCode(c - 65248);
            continue;
        }
        DBCStr += String.fromCharCode(c);
    }
    return DBCStr;
} 

//��������ʱ��Ҫȥ������һ�д���
//var Debuging = Object();
//���ڵ��Ե�alert����
function debugmsg(message)
{
    if (typeof(Debuging) != "undefined")
        alert(message);
}

//����
function assert(condiction,ErrMsg)
{
    if ((!condiction) && (ErrMsg))
      throw new Error(ErrMsg);
    return condiction;
}