//<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">   //加上这一句是为了在DreamWeaver中不会乱码

var PostBackInfo;
var Params;
var InParams;
var OutParams;
var IsPostBack;
var HasError;
var g_FunctionID
var g_UserName;
var g_UserNameC;
var g_Department;
var g_Role;
var g_Title;

document.attachEvent('onreadystatechange', On_Init);

function On_Init(){
  if (document.readyState != 'complete') return;
  PostBackInfo = document.all.PostBackInfo;
  g_FunctionID = PostBackInfo.FunctionID;
  g_Title = PostBackInfo.MainTitle;
  if (g_Title == null) g_Title = '';
  
  //先把输出参数中也是输入参数的这部分参数值，用输入参数的值来填充
  PostBackInfo.OutParamXML = GetFilledParamXML(PostBackInfo.OutParamXML, PostBackInfo.InParamXML);

  Params = new ParamClass(PostBackInfo.ParamsXML, '__ParamsXML');
  InParams = new ParamClass(PostBackInfo.InParamXML, '__InParamXML');
  OutParams = new ParamClass(PostBackInfo.OutParamXML, '__OutParamXML');
  if (PostBackInfo.IsPostBack == 'True')
  	IsPostBack = true;
  else
    IsPostBack = false;
  
  if (Params.GetValue("ErrorString") != "")
    HasError = true;
  else
    HasError = false;
  g_UserName = PostBackInfo.UserName;
  g_UserNameC = PostBackInfo.UserNameC;
  g_Department = PostBackInfo.Department;
  g_Role = PostBackInfo.Role;
}
