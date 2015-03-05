<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserQXFB.aspx.cs" Inherits="Admin_main_UserQXFB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>权限分配</title>
    <link href="../style/index.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    
.rb
{ border-right:#B8D1EF solid 1px;
	border-bottom:#B8D1EF solid 1px;}
.r
{ border-right:#B8D1EF solid 1px;}
.b{	border-bottom:#B8D1EF solid 1px;}
.bgSelect
{
	background-color:#FFC;
}
.bg2
{
	background-color:#F1F3FF;
}
.bg1
{
	background-color:#FCFDFF;
}
        .style1
        {
            width:20%;
            height:25px;
        }
        .tdRight
{
	text-align:right;
	padding-right:5px;
	background-color:#F1F3FF;
}
.tdleft
{
	text-align:left;
	padding-left:5px;
	background-color:#FCFDFF;
}
.table2
{
	color: #414043;
	font-size: 12px;
	border: solid 1px #B8D1EF;
	margin-top:5px;
}
.table3
{
	color: #414043;
	font-size: 12px;
	border: solid 1px #B8D1EF;
}
.table2 td
{ 
	height:30px;
	border: solid 1px #B8D1EF;
}
.divStyle
{
	display:none;
}
.divStyle2
{
	display:block;
}
a
{
	color:Black;
}
    </style>
    <script language="javascript" type="text/javascript">
        var oCurentDiv = null;
        function fnShow(obj) {
            var a = document.all["DIVr_1"];
            var ii = 0;
            var cArray = new Array();
            var SysCount = document.all["SysCount"].value;
            var inputArray = document.getElementsByTagName("input");
//            for (var j = 0; j < inputArray.length; j++) {
//                var id = inputArray[j].id == null ? "" : inputArray[j].id.substr(0, 2);
//                if (id == "c_")
//                    cArray.push(inputArray[j]);
//            }
            for (var i = 0; i < SysCount; i++) {
                document.getElementById("DIV_" + i).style.display = "none";
                if (document.getElementById("td_" + i).className == "b bg2")
                    document.getElementById("td_" + i).className = "b bg2"
                else
                    document.getElementById("td_" + i).className = "rb bg2"
                if (obj.id == "td_" + i)
                    ii = i;
            }
            document.getElementById("DIV_" + ii).style.display = "block";
            oCurentDiv = document.getElementById("DIV_" + ii);
            //fnCheck(cArray[ii]);
            if (document.getElementById("td_" + ii).className == "b bg2")
                document.getElementById("td_" + ii).className = "b bgSelect"
            else
                document.getElementById("td_" + ii).className = "bgSelect"
        }
//        function fnCheck1(obj) {//暂时没用
//            var cc = 0;
//            var cArray = new Array();
//            var inputArray = document.getElementsByTagName("input");
//            for (var j = 0; j < inputArray.length; j++) {
//                var id = inputArray[j].id == null ? "" : inputArray[j].id.substr(0, 2);
//                if (id=="c_")
//                    cArray.push(inputArray[j]);
//            }
//                //cArray = document.getElementsByName("C_XT");            
//            for (var i = 0; i < cArray.length; i++) {
//                cArray[i].checked = false;
//                if (obj.id == "c_" + i) {
//                    cc = i;
//                }
//            }
//            if (cArray[cc].checked != "true")
//                cArray[cc].checked = true;
//            else
//                cArray[cc].checked = false;
//        }
        function fnCheck(obj) {
            if (obj.checked)
                obj.checked = true;
            else
                obj.checked = false;
        }
        function fnMouseOver(obj) { 
            obj.style.cursor="hand";
        }
        function fnInit() {
            fnShow(document.getElementById("c_0"));
        }
        function fnNo(obj) {

        }
        function OnCheckEvent(systemID,objTree) {
            var objNode = event.srcElement;
            if (objNode.tagName != "INPUT" || objNode.type != "checkbox")
                return;
            //获得当前树结点
            var ck_ID = objNode.getAttribute("ID");
            var node_ID = ck_ID.substring(0, ck_ID.indexOf("CheckBox")) + "Nodes";
            var curTreeNode = document.getElementById(node_ID);
            //级联选择
            SetChildCheckBox(curTreeNode, objNode.checked);
            SetParentCheckBox(objNode);
            SetSystemCheck(systemID, objNode.checked);
        }
        function SetSystemCheck(systemID,objCheck) {
            var objSystem = document.getElementById("c_"+systemID);
            if (objSystem == null || objSystem == "") return;
            if (objCheck)
                objSystem.checked = true;
        }

        //子结点字符串
        var childIds = "";
        //获取子结点ID数组
        function GetChildIdArray(parentNode) {
            if (parentNode == null)
                return;
            var childNodes = parentNode.children;
            var count = childNodes.length;
            for (var i = 0; i < count; i++) {
                var tmpNode = childNodes[i];
                if (tmpNode.tagName == "INPUT" && tmpNode.type == "checkbox") {
                    childIds = tmpNode.id + ":" + childIds;
                }
                GetChildIdArray(tmpNode);
            }
        }

        //设置子结点的checkbox
        function SetChildCheckBox(parentNode, checked) {
            if (parentNode == null)
                return;
            var childNodes = parentNode.children;
            var count = childNodes.length;
            for (var i = 0; i < count; i++) {
                var tmpNode = childNodes[i];
                if (tmpNode.tagName == "INPUT" && tmpNode.type == "checkbox") {
                    tmpNode.checked = checked;
                }
                SetChildCheckBox(tmpNode, checked);
            }
        }

        //设置父结点的checkbox
        function SetParentCheckBox(childNode) {
            if (childNode == null)
                return;
            var parent = childNode.parentNode;
            if (parent == null || parent == "undefined")
                return;
            do {
                parent = parent.parentNode;
            }
            while (parent && parent.tagName != "DIV");
            if (parent == "undefined" || parent == null)
                return;
            var parentId = parent.getAttribute("ID");
            if (parentId == "") return;
            var objParent = document.getElementById(parentId);
            childIds = "";
            GetChildIdArray(objParent);
            //判断子结点状态
            childIds = childIds.substring(0, childIds.length - 1);
            var aryChild = childIds.split(":");
            var result = false;
            //当子结点的checkbox状态有一个为true，其父结点checkbox状态即为true,否则为false
            for (var i in aryChild) {
                var childCk = document.getElementById(aryChild[i]);
                if (childCk.checked)
                    result = true;
            }
            parentId = parentId.replace("Nodes", "CheckBox");
            var parentCk = document.getElementById(parentId);
            if (parentCk == null)
                return;
            if (result)
                parentCk.checked = true;
            else
                parentCk.checked = false;
            SetParentCheckBox(parentCk);
        }

        function Search()
        {
            var oKey = document.getElementById("SearchKeyWord");
            var oRootTD = document.getElementById("TD");
            var oContainer = null;
            if (oCurentDiv == null) oContainer = oRootTD.childNodes[0].rows[0].cells[0];
            else
                var oContainer = oCurentDiv.rows[0].cells[0];
            //alert(oContainer.childNodes[0].rows.length);
            for (var i = 0; i < oContainer.childNodes[0].rows.length; i++)
            {
                var oX = oContainer.childNodes[0].rows[i];
                if (oKey.value == "")
                    oX.style.display = "";
                else
                {
                    var sTemp = oX.cells[0].innerHTML; 
                    if (sTemp.indexOf(oKey.value) >= 0)
                    {
                        oX.style.display = ""; //alert(sTemp.indexOf(oKey.value));
                    }
                    else
                        oX.style.display = "none";
                }
//                alert(oContainer.childNodes.length);
            }
        }

     //window.onload = fnInit;
    </script>
     <base target="_self"></base>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="SysCount" name="SysCount" runat="server" />
    <div style="text-align:center;">
    <table width="800" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td runat="server" id="TDHead">
    
    </td>
  </tr>
  <tr>
    <td id="TD2">
        <input type="text" size="27" id="SearchKeyWord" value="" /> <input type="button" onclick="Search()" value="查询" />
    </td>
  </tr>
  <tr>
    <td  runat="server" id="TD">

    </td>
  </tr>
  <tr>
    <td  runat="server" id="TD1" style="height:40px; vertical-align:bottom;">
        <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="btnSave" 
            onclick="btnSave_Click"  />
        <input id="Button1" class="cBtn3" onclick="window.close();" type="button" value="取 消" />
    </td>
  </tr>
</table>
    </div>    
    </form>
</body>
</html>
