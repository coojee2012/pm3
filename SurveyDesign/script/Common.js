//<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">   //加上这一句是为了在DreamWeaver中不会乱码


function NewXmlDoc(){
  var xmlDoc = new ActiveXObject("Msxml2.DOMDocument");
  xmlDoc.async = false;
  return xmlDoc;
}


//bAsync: 是否使用异步方式，可以省略，缺省为false。
function XMLHTTP(sURL, sXML, bAsync){
  if (bAsync == null) 
    bAsync = false;
  var xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
  xmlhttp.Open("POST", sURL, bAsync);
  xmlhttp.Send(sXML);
  return xmlhttp.responseXML;
}


//调用服务器的方法
//sFunctionName: 方法名
//InParams: 输入参数
//ErrorHint: 如果调用方法出错时，要显示的出错提示。为false时不显示出错提示。
//sURL: 要请求的URL地址或页面文件名。可省略，缺省为Transfer.aspx
function Call(sFunctionName, InParams, ErrorHint, sURL){
  if (InParams == null || InParams == ''){
    InParams = new ParamClass();
  }
  if (sURL == null || sURL == '') sURL = 'Transfer.aspx';
  InParams.XmlDoc.documentElement.setAttribute('FunctionName', sFunctionName);
  var xmlDoc = XMLHTTP(sURL, InParams.ToString());
  if (xmlDoc.documentElement==null || xmlDoc.xml == ""){
    if (ErrorHint !== false){
	  ErrorHint += '\n可能是因为数据传输出错！'; 
	  alert(ErrorHint);
    }
	return false;
  }
  if (xmlDoc.documentElement.nodeName != 'Params'){  //返回值是XML数据
	return xmlDoc.xml;  
  }
  var OutParams = new ParamClass(xmlDoc.xml);
  var sResult = OutParams.GetValue("Result");

  var xmlDoc = NewXmlDoc();
  if (xmlDoc.loadXML(sResult)){  //返回值是XML字符串，判断是否为出错信息
    if (xmlDoc.documentElement.nodeName == 'Error'){  //是出错信息
	  if (ErrorHint !== false){  //需要提示
        if (ErrorHint != ""){
          ErrorHint += '\n相关信息：\n\n';
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


//与Call函数功能相同，只是请求的是TransferEx.aspx只不是Transfer.aspx页面
function CallEx(sFunctionName, InParams, ErrorHint){
  return Call(sFunctionName, InParams, ErrorHint, 'TransferEx.aspx')
}


//获取指定功能的输入参数
function GetFunctionInParamXML(sFunctionID){
  var tmpParams = new ParamClass();
  tmpParams.SetValue("FunctionID", sFunctionID);
  var sReturn = Call('GetFunctionInParamXML', tmpParams, false);
  return sReturn;
}

//调用选择用户模块的函数
//iSelectType:可选择的用户类型(1-部门,2-角色,3-部门、角色,4-个人,5-部门、个人,6-角色、个人,7－部门、角色、个人)
//sSelectedUserXML: 以前选中的节点的XML，用于恢复选中状态
//bAllowMultiSelect: 是否允许选择多个用户，默认为true
//bAllowNotSelect: 是否可以不选中任何用户，默认为true
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
  var sTitle = "用户选择";
  var oReturn = openModalDialog(sURL, vArguments, sFeatures, sTitle);
  return oReturn;
}


//调用选择分组用户模块的函数
//iSelectType:可选择的用户类型(1-个人,2-分组名,3-个人和分组名)
//sSelectedUserXML: 以前选中的节点的XML，用于恢复选中状态
//bAllowMultiSelect: 是否允许选择多个用户，默认为true
//bAllowNotSelect: 是否可以不选中任何用户，默认为true
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
  var sTitle = "用户选择";
  var oReturn = openModalDialog(sURL, vArguments, sFeatures, sTitle);
  return oReturn;
}


function openModalDialog(sURL, vArguments, sFeatures, sTitle){
  var sURL = 'openModalDialog.aspx?DialogURL=' + escape(sURL) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();
//  alert(sURL);
  var oArguments = new Object();
  if (top.oTop != null)  //解决在ModalDialog中再弹出ModalDialog时出错
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
  if (top.oTop != null)  //解决在ModalDialog中再弹出ModalDialog时出错
    oArguments["top"] = top.oTop;
  else
    oArguments["top"] = top;
  oArguments["Arguments"] = vArguments;
  var vReturn = showModelessDialog(sURL, oArguments, sFeatures);
  return vReturn;
}


//在弹出的模态窗口中打开指定的功能
//sFunctionID: 欲调用功能的功能编号
//sInParamXML: 输入参数的XML字符串
//sTitle: 弹出窗口的标题
//sFeatures: 控制窗口外观的其它参数
//返回值：所调功能的输出参数对象
function openModalFunction(sFunctionID, sInParamXML, sTitle, sFeatures){
  var InParams = new ParamClass();
  InParams.SetValue('FunctionID', sFunctionID);
  var sURL = Call('GetFunctionURL', InParams, ''); 
  sURL = 'openModalFunction.aspx?DialogURL=' + escape(sURL) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();
  var InParams = new ParamClass(sInParamXML);
  var oArguments = new Object();
  if (top.oTop != null)  //解决在ModalDialog中再弹出ModalDialog时出错
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


//在弹出的非模态窗口中打开指定的功能
//sFunctionID: 欲调用功能的功能编号
//sInParamXML: 输入参数的XML字符串
//sTitle: 弹出窗口的标题
//sFeatures: 控制窗口外观的其它参数
//返回值： 所弹出的窗口对象
function openModelessFunction(sFunctionID, sInParamXML, sTitle, sFeatures){
  var InParams = new ParamClass();
  InParams.SetValue('FunctionID', sFunctionID);
  var sURL = Call('GetFunctionURL', InParams, ''); 
  sURL = 'openModalFunction.aspx?DialogURL=' + escape(sURL) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();  
  var InParams = new ParamClass(sInParamXML);
  var oArguments = new Object();
  if (top.oTop != null)  //解决在ModalDialog中再弹出ModalDialog时出错
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
  if (top.oTop != null)  //解决在ModalDialog中再弹出ModalDialog时出错
    oArguments["top"] = top.oTop;
  else
    oArguments["top"] = top;

  var InParams = new ParamClass(sInParamXML);
  var vArguments = ToModalDialogParams(InParams, sFunctionID);
  oArguments["Arguments"] = vArguments;
  var vReturn = showModalDialog(sURL, oArguments, sFeatures);
  return vReturn;
}


//把输入参数对象转换成openModalDialog方法的Arguments参数格式
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


//定位到vURL指定的地址
//vURL可以是URL地址字符串，也可以是包括URL地址、输入参数的对象
//bSaveURL 是否把地址保存在“前进”“后退”的历史记录中，布尔型(true-保存  false-不保存)，默认为true，当sFrameName不等于"ifrMain"时bSaveURL始终为false
//sFrameName： 用于指定在哪个Frame中打开，可以省略或，可以为null，可以为空字符串，省略为ifrMain
　　　　　　　　//如果要在第二层导航栏中打开，sFrameName设置为"ifrMainNavigation"
function LocateTo(vURL, bSaveURL, sFrameName){
  var bInIfrMain = true;  //bInIfrMain表示是否在ifrMain中打开的
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
  if (bSaveURL !== false && vURL != top.aHistoryBack[top.aHistoryBack.length-1]){  //防止同一地址被连续多次加入到后退历史
    top.aHistoryBack.push(vURL);
	top.aHistoryForward.splice(0, top.aHistoryForward.length);  //清除可前进的历史记录
  }

  if (typeof(vURL) == 'object'){   //从导航栏或工具栏调用此功能的。      
    if (vURL.Params != null){   //POST方法
      var oHTMLForm = top.ifrPost.document.all.frmLcation;
	  oHTMLForm.target = sFrameName;
 	  oHTMLForm.action = vURL.URL;
      oHTMLForm.innerHTML = vURL.Params;
      oHTMLForm.submit();
	  oHTMLForm.target = 'ifrMain';
    }
    else {   //GET方式
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
	        vURL.NavigationItem.Select(false);  //false为不触发事件
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


//转到指定功能 
//sFunctionID: 功能编号
//sInParamXML: 功能的输入参数字符串,可以省略或为空字符串
//sTitle: 转向指定功能后，显示的标题,可以省略或为空字符串
//sFrameName： 用于指定在哪个Frame中打开，可以省略或，可以为null，可以为空字符串，省略为ifrMain
　　　　　　　　//如果要在第二层导航栏中打开，sFrameName设置为"ifrMainNavigation"
function LocateToFunction(sFunctionID, sInParamXML, sTitle, sFrameName){
  if (sFrameName == null || sFrameName == '') sFrameName = 'ifrMain';
  if (sInParamXML == null) sInParamXML = '';
  var xmlDoc = NewXmlDoc();
  var oRootNode = xmlDoc.createElement('Params');
  oRootNode.setAttribute('FunctionID', sFunctionID);
  if (sInParamXML != null)
    oRootNode.setAttribute('InParamXML', sInParamXML);
	
  if (sFrameName == 'ifrMain'){  //当不在ifrMain打开时，忽略sTitle
    if (sTitle != null)
      oRootNode.setAttribute('MainTitle', sTitle);
  }
  xmlDoc.appendChild(oRootNode);
  
  var InParams = new ParamClass();
  InParams.SetValue('FunctionID', sFunctionID);
  var Result = Call('GetFunctionURL', InParams, '获取功能所对应的URL时出错！'); 
  if (Result === false) return;
  sURL = Result;
  var vURL = top.GetPostFormParams(sURL, xmlDoc);
  LocateTo(vURL, true, sFrameName);
}



//转到指定页面 
//sURL: 页面文件或URL地址
//sInParamXML: 功能的输入参数字符串,可以省略或为空字符串
//sFrameName： 用于指定在哪个Frame中打开，可以省略或，可以为null，可以为空字符串，省略为ifrMain
　　　　　　　　//如果要在第二层导航栏中打开，sFrameName设置为"ifrMainNavigation"
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

//XML正确性验证
//如果bNoErrorAlert等于true，则不显示任何错误提示信息
//如果sXML为null或空字符串，显示sShowMessage信息
//如果sXML的根节点为Error，显示根节点的Message属性的信息(错误提示信息)
//如果sXML不为null或空字符串，并且sXML的根节点不为Error，返回值为true,否则为false
function XMLValidate(sXML, sShowMessage, bNoErrorAlert){
  if (sXML == null || sXML == ''){
    sShowMessage += '\n可能是因为网络连接不正常或服务器状态丢失，请重新登录！';
	if (bNoErrorAlert != true)
      alert(sShowMessage);
	return false;
  }
  
  var xmlDoc = NewXmlDoc();
  if (!xmlDoc.loadXML(sXML)){  //sXML不是结构正确的XML
    sShowMessage += '\n可能是因为数据传输出错！';
	if (bNoErrorAlert != true)
      alert(sShowMessage);
	return false;
  }
  
  if (xmlDoc.documentElement.nodeName == 'Error'){
    sShowMessage += '\n错误信息：\n\n' + xmlDoc.documentElement.getAttribute("Message");
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


//去掉字符串前后的空格
String.prototype.trim = function(){
  return this.replace(/(^\s*)|(\s*$)/g, "");
}


//把字符串中的系统变量名(如:"&ProjectID&")替换成相应的值(Global.ProjectID)
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

//格式化字符串(如:Format('{0}+{1}={2}', 'a', 'b', 'ab'))
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

//得到每页显示记录数
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

//返回填充值后的XML
//用sSourceXML去填充sTargetXML
function GetFilledParamXML(sTargetXML, sSourceXML){
  var oXmlDoc_Target = NewXmlDoc();
  var oXmlDoc_Source = NewXmlDoc();
  if (!oXmlDoc_Target.loadXML(sTargetXML)) return sTargetXML;
  if (!oXmlDoc_Source.loadXML(sSourceXML)) return sTargetXML;
  var i, iCount, sParamName, sInputType, sValueType, sValue, oNode, oNodeList, oSourceNode;
  oSourceRootNode = oXmlDoc_Source.documentElement;
  oNodeList = oXmlDoc_Target.documentElement.childNodes;
  iCount = oNodeList.length;
//alert('输入参数个数： iCount:' + iCount);  
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
    sValue = oNode.getAttribute('值');
    var re = /^%([^\s%]+)%$/;
	sParamName = sValue.replace(re, function ($0, $1){return $1});
	if (sParamName == sValue) continue;   //如果条件成立，表示"值"属性不是"%xxxx%"的格式
	var sXPath = './*[@名称="' + sParamName + '"]';
	oSourceNode = oSourceRootNode.selectSingleNode(sXPath);
	if (oSourceNode == null) continue;   //没找到相应的输出参数节点
	sValue = oSourceNode.getAttribute('值');
//	alert(sValue);
	if (sValue == null) sValue = '';
	oNode.setAttribute('值', sValue);
	oNode.setAttribute('已实例化', '是');   //标志为已实例化
  }
  var sReturn = oXmlDoc_Target.xml;
  oXmlDoc_Target = null;
  oXmlDoc_Source = null;
  return sReturn;
}

//检测输入参数是否也完全实例化，如果不是则弹出输入窗让用户输入
//sParamXML: 参数XML
//bNoCheckValue: 布尔型，为true时不检测参数的值(若不是类型类型，即使参数有值还是弹出窗口输入值)
function CompleteInParams(sParamXML, bNoCheckValue){ 
  var xmlDoc = NewXmlDoc();
  if (!xmlDoc.loadXML(sParamXML)) return false;
  var i, iCount, sAttrName, sInputType, sValueType, sValue, oNode, oNodeList;
  oNodeList = xmlDoc.documentElement.selectNodes('./参数[not(@已实例化 and @已实例化="是")]');
  iCount = oNodeList.length;
  if (iCount == 0) return sParamXML;   //所有参数已完成了值的实例化
  //循环所有还没实例化值的参数，把系统变量参数实例化，把业务分类的参数转换成枚举类型
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
	sName = oNode.getAttribute('名称');
	sInputType = oNode.getAttribute('输入类型');
	sValueType = oNode.getAttribute('类型');
	sValue = oNode.getAttribute('值');
	if (sValue == null) sValue = '';
	sValue = sValue.toUpperCase();
	if (sValue == '%EMPTYSTRING%'){        //值已实例化为空符串
      oNode.setAttribute('值', '');
      oNode.setAttribute('已实例化', '是');
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
	else if (sInputType == '组织机构代码标识'){
      oNode.setAttribute('值', top.JGDM);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sInputType == '用户名' || sValue == '%SYS_USERNAME%'){
      oNode.setAttribute('值', top.UserName);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sValue == '%SYS_USERNAMEC%'){
      oNode.setAttribute('值', top.UserNameC);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sInputType == '角色' || sValue == '%SYS_ROLE%'){
      oNode.setAttribute('值', top.Role);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sInputType == '部门' || sValue == '%SYS_DEPARTMENT%'){
      oNode.setAttribute('值', top.Department);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sInputType == '系统库' || sValue == '%SYS_SYSTEMDBNAME%'){
      oNode.setAttribute('值', top.SystemDBName);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sInputType == '工作库' || sValue == '%SYS_WORKDBNAME%'){
      oNode.setAttribute('值', top.WorkDBName);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sInputType == '资源库' || sValue == '%SYS_ARCHIVEDBNAME%'){
      oNode.setAttribute('值', top.ArchiveDBName);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sInputType == '回收库' || sValue == '%SYS_RECYCLEDBNAME%'){
      oNode.setAttribute('值', top.RecycleDBName);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sInputType == '机构代码' || sValue == '%SYS_JGDM%'){
      oNode.setAttribute('值', top.JGDM);
      oNode.setAttribute('已实例化', '是');
	}
	else if (sValue != '' && bNoCheckValue != true){       //这个条件应放在其它判断"值"的条件之后
      oNode.setAttribute('已实例化', '是');
	}
	else if (sValueType == '业务分类'){  //是业务分类类型的，转换成枚举类型
	  var Result = Call('GetProjectFlag');
	  var tmpXmlDoc = new NewXmlDoc();
	  tmpXmlDoc.loadXML(Result);
	  if (tmpXmlDoc.documentElement.childNodes.length == 1){
	    oNode.setAttribute('值', tmpXmlDoc.documentElement.firstChild.getAttribute('Value'));
        oNode.setAttribute('已实例化', '是');
	  }
	  else {
	    oNode.setAttribute('值选项', tmpXmlDoc.xml);
        oNode.setAttribute('类型', '枚举');
	  }
	  tmpXmlDoc = null;
	}
	else if (sValueType == '所属部门' || sValueType == '所属角色'){
	  var sValue = sValueType=='所属部门'?top.Department:top.Role;
	  var arr = sValue.split(',');
	  if (arr.length <= 1){  //不属于多个部门或角色
	    oNode.setAttribute('值', top.Department);
        oNode.setAttribute('已实例化', '是');
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
	    oNode.setAttribute('值选项', tmpXmlDoc.xml);
        oNode.setAttribute('类型', '枚举');
	  }
	}
	else{
      oNode.setAttribute('值', '');
      oNode.setAttribute('已实例化', '是');	
	}
  }
  oNodeList = xmlDoc.documentElement.selectNodes('./参数[not(@已实例化)]');
  if (oNodeList.length == 0) return xmlDoc.xml;   //所有参数已完成了值的实例化
  if (bNoCheckValue)  //
    return xmlDoc.xml; //是定义浮动窗口和快捷方式。
  else
    return ParamsInputWindow(xmlDoc.xml,bNoCheckValue);
}

function ParamsInputWindow(ParamXML,bNoCheckValue){
  var sURL = 'InputParam.aspx';
  var vArguments = new Object();
  vArguments.ParamXML = ParamXML;
  if (bNoCheckValue == true)     
    vArguments.CanNoValue = true;   //值可以为空(在下拉选择框中加入空选项)
  else
    vArguments.CanNoValue = false;
  var sFeatures = 'dialogHeight:187px; dialogWidth:308px; scroll:no; status:no; help:no; resizable:no; center:yes';
  var sTitle = "请输入参数值";
  var tmpXmlDoc = openModalDialog(sURL, vArguments, sFeatures, sTitle);
  if (tmpXmlDoc == null)
    return false;
  else
    return tmpXmlDoc.xml;
}

//一个参数信息的类
//使用方法: 
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
  
  //如果由ParamName指定的参数的值是一个集合(由XML表示)，由iIndex指定取集合中的第几个值，如果值不是集合或iIndex不是一个有效的下标，则返回整个"值"属性
  this.GetValue = function (ParamName, iIndex){
    var xmlDoc = this.XmlDoc;
    var oNode = xmlDoc.documentElement.selectSingleNode('./*[@名称="' + ParamName + '"]');
	if (oNode == null)
	  return '';
	else {
	  var sValue = oNode.getAttribute('值');
	  var newDoc = NewXmlDoc();
	  if (!newDoc.loadXML(sValue)) return sValue;  //"值"不是XML，返回整个值
	  if (newDoc.documentElement.nodeName != 'MultiValues') return sValue;  //不是表示有多个值的XML
	  if (isNaN(parseInt(iIndex))) return sValue; //iIndex不是整数
	  var tmpNode = newDoc.documentElement.childNodes[iIndex];
	  if (tmpNode == null) return sValue; //iIndex不是有效的下标
	  return tmpNode.text;
	}
  };
  
  //获取指定参数的值的个数(相当于取列表上选中的行数)，用处: 如果查询列表的ID列作为输出参数，而列表上选中多行，参数ID对应的值就有多个。
  this.GetValueCount = function (ParamName){
    var xmlDoc = this.XmlDoc;
    var oNode = xmlDoc.documentElement.selectSingleNode('./*[@名称="' + ParamName + '"]');
	if (oNode == null)
	  return 0;
	else {
	  var sValue = oNode.getAttribute('值');
	  var newDoc = NewXmlDoc();
	  if (!newDoc.loadXML(sValue)) return 1;  //"值"不是XML
	  if (newDoc.documentElement.nodeName != 'MultiValues') return 1;  //不是表示有多个值的XML
	  return newDoc.documentElement.childNodes.length;
	}
  }
  
  this.SetValue = function (ParamName, ParamValue){
    var xmlDoc = this.XmlDoc;
    var oNode = xmlDoc.documentElement.selectSingleNode('./*[@名称="' + ParamName + '"]');
	if (oNode == null){
	  oNode = xmlDoc.createElement('参数');        
	  xmlDoc.documentElement.appendChild(oNode);
	  oNode.setAttribute('名称', ParamName);
	}
    oNode.setAttribute('值', ParamValue);
	if (this.PostBackName != null && document.all[this.PostBackName] != null)
	  document.all[this.PostBackName].value = this.ToString();
  };
  
  this.Remove = function (ParamName){
    var xmlDoc = this.XmlDoc;
    var oNode = xmlDoc.documentElement.selectSingleNode('./*[@名称="' + ParamName + '"]');
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

//输入参数值集合中的每个值是否相等
function IsEqualValue(str){
  var xmlDoc = NewXmlDoc();
  if (!xmlDoc.loadXML(str)) return true;  //"值"不是XML，返回true
  if (xmlDoc.documentElement.nodeName != 'MultiValues') return true;  //不是表示有多个值的XML
  var iCount = xmlDoc.documentElement.childNodes.length;
  if (iCount <= 1) return true;
  var sValue = xmlDoc.documentElement.childNodes[0].text;
  for (var i=1; i<iCount; i++){
	var tmp = xmlDoc.documentElement.childNodes[i].text;
	if (tmp != sValue) return false;
  }
  return true;
}

//输入参数值集合中的每个值相等时，取第一值
function GetFirstValue(str){
  var xmlDoc = NewXmlDoc();
  if (!xmlDoc.loadXML(str)) return str;  //"值"不是XML，返回false
  if (xmlDoc.documentElement.nodeName != 'MultiValues') return str;  //不是表示有多个值的XML
  var iCount = xmlDoc.documentElement.childNodes.length;
  if (iCount <= 1) return false;
  var sValue = xmlDoc.documentElement.childNodes[0].text;
  for (var i=1; i<iCount; i++){
	var tmp = xmlDoc.documentElement.childNodes[i].text;
	if (tmp != sValue) return false;
  }
  return sValue;
}

//剪贴板的类
//使用方法: 
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
  //删除剪贴板中XML的节点，用于剪切
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
  //获取剪贴板中剪切或复制记录的长度
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
  var cn = "零壹贰叁肆伍陆柒捌玖";
  var unit = new Array("拾佰仟", "分角");
  var unit1= new Array("万亿", "");
  var str = num.toString();
  var numArray = str.split(".");
  var intpart = numArray[0];
  var decimal = numArray[1];
  intpart = intpart.replace(/^0+/g, '');
  if (intpart == '') intpart = '0';
  if (decimal == null || decimal == '') decimal = '0';
  if (decimal.length > 2) decimal = decimal.substr(0, 2);
  decimal = decimal.replace(/^0*$/g, '0');
  if (intpart == '0' && decimal == '0') return '零圆整';
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
      tmp1 = tmp1.replace(/(零.)+/g, "零").replace(/零+$/, "");
      tmp1 = tmp1.replace(/^壹拾/, "拾");
      tmp = (tmp1+unit1[i].charAt(j-1)) + tmp;
    }
    numArray[i] = tmp;
  }

  numArray[1] = numArray[1] ? numArray[1] : "";
  numArray[0] = numArray[0] ? numArray[0]+"圆" : numArray[0], numArray[1] = numArray[1].replace(/^零+/, "");
  numArray[1] = numArray[1].match(/分/) ? numArray[1] : numArray[1]+"整";

  var str = numArray[0]+numArray[1];

  if (intpart.length > 8){
    if (intpart.substr(intpart.length-8,4) == '0000'){
          if (intpart.substr(intpart.length-4,4) == '0000' || intpart.substr(intpart.length-4,1) == '0'){
            str = str.replace(/万/, '');
          }
          else {
        str = str.replace(/万/, '零');
          }
    }
  }
  return str;
}

//数字转换成大写
function NumberToChinese(num)
{
  if (isNaN(num) || num > Math.pow(10, 12)) return "";
  var cn = "零壹贰叁肆伍陆柒捌玖";
  var unit = new Array("拾佰仟", "");
  var unit1= new Array("万亿", "");
  var str = num.toString();
  var numArray = str.split(".");
  var intpart = numArray[0];
  var decimal = numArray[1];
  intpart = intpart.replace(/^0+/g, '');
  if (intpart == '') intpart = '0';
  if (decimal == null || decimal == '') decimal = '0';
  if (decimal.length > 2) decimal = decimal.substr(0, 2);
  decimal = decimal.replace(/^0*$/g, '0');
  if (intpart == '0' && decimal == '0') return '零';
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
          tmp1 = tmp1.replace(/(零.)+/g, "零").replace(/零+$/, "");
          tmp1 = tmp1.replace(/^壹拾/, "拾");
          tmp = (tmp1+unit1[i].charAt(j-1)) + tmp;
      }
    }
    numArray[i] = tmp;
  }

  numArray[1] = numArray[1] ? numArray[1] : "";
  numArray[0] = numArray[0] ? numArray[0]: numArray[0], numArray[1] = numArray[1].replace(/^零+/, "");
  numArray[1] = numArray[1].match(/分/) ? numArray[1] : numArray[1];
  var str = "";
  if (numArray[0] == "")
  {
      if (tmp == "零")
        str = "";
      else
        str = "零点"+tmp;
  }
  else
  {
    str = numArray[0];
    if (tmp != "零")
        str = str+"点"+tmp;
  }

  if (intpart.length > 8){
    if (intpart.substr(intpart.length-8,4) == '0000'){
          if (intpart.substr(intpart.length-4,4) == '0000' || intpart.substr(intpart.length-4,1) == '0'){
            str = str.replace(/万/, '');
          }
          else {
        str = str.replace(/万/, '零');
          }
    }
  }
  return str;
}

//统计Grid中一列数据
//oGrid:Grid对象
//sFiled:在Grid要统计的字段名称
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


  //获取元素位置的类  
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


  
  //把值转换成Boolean型
  //Value:要转换的值； bDefaultValue:默认值
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

//判断字符是否为空值
function IsNullString(Value)
{
    if (Value == "" || Value == null)
        return true;
    else
        return false;
}

//以对话框方式打开URL ZhangAiChun
function openModalURL(sURL, sInParamXML, sTitle, sFeatures, sFunctionID)
{
  sURL = 'openModalFunction.aspx?DialogURL=' + escape(sURL) + '&Title=' + escape(sTitle) + '&__randomnumeric=' + Math.random();
  if (sFeatures == null || sFeatures == '')
    sFeatures = 'dialogHeight:500px; dialogWidth:750px; scroll:no; status:no; help:no; resizable:yes; center:yes';
  var oArguments = new Object();
  if (top.oTop != null)  //解决在ModalDialog中再弹出ModalDialog时出错
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

//提示正在处理
function ShowOperatingHint(){
  ShowHintCommon('../Images/Hint/Operating.gif');
}

//提示正在保存
function ShowSavingHint(){
  ShowHintCommon('../Images/Hint/Loading.gif');
}

//提示正在载入
function ShowLoadingHint(){
  ShowHintCommon('../Images/Hint/Loading.gif');
}

function ShowHintCommon(sImgURL){
  if (top.ifrHint.imgHint.src != sImgURL)
    top.ifrHint.imgHint.src = sImgURL;
  top.document.all.ifrHint.style.visibility = 'visible';
}

//上传文件
function fnUpFile(){
    var sURL = "UpFile.aspx?ImageTable="+arguments[0]+"&ImageField="+arguments[1]+"&ImageKeyField="+arguments[2]+
        "&ImageKeyFieldVal="+arguments[3]+"&LinkField="+arguments[4]+"&LinkFieldVal="+arguments[5];
    var tsTitle = '上传文件';
    var vArguments = this;
    var sFeatures = 'dialogHeight:200px; dialogWidth:260x; scroll:no; status:no; help:no; resizable:no; center:yes';
    var sReturn = openModalDialog(sURL, vArguments, sFeatures, tsTitle);
    if(typeof(sReturn) == "undefined")sReturn = "";
    return sReturn;
}

//隐藏提示
function HideHint(){
  top.document.all.ifrHint.style.visibility = 'hidden';
}

/************************************ Base64编码 解码 函数 **************************************/

/***======================================================================================***
            JavaScript中获得的中文字符是用UTF16进行编码，而统一的页面标准格式为UTF-8。
            所以如果是从服务器接收到的Base64编码后的数据，则在解码后，需要转化为UTF16
            如果是向服务器发送Base64编码数据，则应先转换为UTF-8再进行Base64编码
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
//Base64编码
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
//Base64解码
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
/************************************ Base64编码 解码 函数 (End)**************************************/

/*
 把输入的字符串转换为半角,并删除所有空格
 input： Str    任意字符串
 output：DBCStr 半角字符串
 说明：1、全角空格为12288，半角空格为32
       2、其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
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

//！！发布时，要去掉下面一行代码
//var Debuging = Object();
//用于调试的alert函数
function debugmsg(message)
{
    if (typeof(Debuging) != "undefined")
        alert(message);
}

//断言
function assert(condiction,ErrMsg)
{
    if ((!condiction) && (ErrMsg))
      throw new Error(ErrMsg);
    return condiction;
}