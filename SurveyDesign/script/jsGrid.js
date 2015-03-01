//<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">   //如果去掉这一句，在DreamWeaver中打开会乱码

var oSelectedRows = new Array();
var oSelected;

var AllowSelect = true;
var AllowUnSelect = false;
var AllowMultiSelect = true;
var AllowAllSelect = true;
var ShowSelectorCol = false;
var AllowInterlace = true;
var AllowResizeCol = true;

var grid;
var iMousePointX;
var iDragMinLeft;
var iDragMaxLeft;
var oDragTD;
var arrColPos = new Array();
var bFirstTimeSelect = true;  //是否第一次选中(用于确定在选中行时是否要更新后退历史中的选中中行号，第一次选中时不更新)
var iTimeoutID_RowIndex = null;
var t1= new Date();
var t2;

function window.onload(){

    grid = new wgGrid('JsWebGrid1');
    BackupOutputParamDefine();
    if (grid.CurrentPageRecordCount > 0){
        var iSelectedRowIndex = parseInt(document.all.SelectedRowIndex.value);
        if (tabBody.rows[iSelectedRowIndex + 1] != null)
            TR_Click(tabBody.rows[iSelectedRowIndex + 1]);
        else
            TR_Click(tabBody.rows[1]);
    }
    else{
        ClearOutputParamDefine();
    }
  	var oHTMLIMG = document.createElement('<img>');
	oHTMLIMG.src = '../Images/InsterPos.gif';
	oHTMLIMG.width = 13;
	oHTMLIMG.height = 11;
	LayInsertPos.innerHTML = '';
    LayInsertPos.appendChild(oHTMLIMG);
	document.onkeydown = oDocument_KeyDown;
    var t6 = new Date();
}

function init(){
  
}

  
  function oDocument_KeyDown(){
    if (grid.CurrentPageRecordCount == 0) return ;
	if (event.keyCode != 38 & event.keyCode != 40) return;  //不是向上向下键
	var iIndex = -1;
	var oSelectRow;
	if (grid.AllowMultiSelect){
      if (oSelectedRows.length > 1) return;   //选中了多行
	  if (oSelectedRows.length == 1)
	    iIndex = oSelectedRows[0].rowIndex-1;
	}
	else {
	  if (oSelected != null)
	    iIndex = oSelected.rowIndex-1;
	}
	if (event.keyCode == 38){  //向上
	  if (iIndex == -1)
	    iIndex = grid.CurrentPageRecordCount-1;
	  else if (iIndex > 0)
	    iIndex = iIndex - 1;
	  else
	    return;
	}
	else if (event.keyCode == 40){  //向下
	  if (iIndex == -1)
	    iIndex = 0;
	  else if (iIndex < grid.CurrentPageRecordCount-1) 
	    iIndex = iIndex + 1;
	  else 
	    return;
	}
	oSelectRow = tabBody.rows[iIndex+1];
	if (oSelectRow == null) return;
	SelectRow(oSelectRow);
	FillOutputParamValue();
	//window.setTimeout('top.ResetToolBar()', 1);
  }


function TR_Click(oHTMLTR){
    if (oHTMLTR == null) oHTMLTR = this;
//    var oHTMLTR = this;
//    var oDOMRow = GetDOMRow(oHTMLTR);
//    if (!FireEvent(evt_RowClick, oDOMRow)) return;
    
//    if (!grid.AllowSelect) return;  //不允许选中行
    if (!ToBoolean(oHTMLTR.Selectable, true)) return;  //此行不允许被选中

    if (!grid.AllowMultiSelect){  //单选模式 
      if (oHTMLTR.Selected){  //此行已选中
	    if (grid.AllowUnSelect)   //允许不选中
          UnSelectRow(oHTMLTR);  //取消选中此行
	  }
      else
        SelectRow(oHTMLTR); 
    }
    else {  //多选模式
      if (event.ctrlKey){  //按下Ctrl键 
        if (oHTMLTR.Selected){
          if (oSelectedRows.length != 1 || grid.AllowUnSelect)
            UnSelectRow(oHTMLTR);
        }
        else
          SelectRow(oHTMLTR); 
      }
      else { //未按下Ctrl键 
        if (oHTMLTR.Selected && oSelectedRows.length == 1){  //只选中了此行
          if (grid.AllowUnSelect)  //允许不选中
            UnSelectRow(oHTMLTR);
        }
        else {
          UnSelectAll();  //先取消选中所有已选中的行
          SelectRow(oHTMLTR);   //再选中此行
        }
      }
    }
	FillOutputParamValue();
	//window.setTimeout('top.ResetToolBar()', 1);
}


function TR_DblClick(oHTMLTR){
  if (oHTMLTR == null) oHTMLTR = this;
  try {
    if (!oHTMLTR.Selected)
      SelectRow(oHTMLTR);
	if (top.oTop != null && top.oTop.ifrMain != null && top.oTop != top){  //在ModalDialog中
	  ExecDefaultFunction();  //执行查询的相关功能中的默认功能
	  return;
	}
	if (typeof(top.ToolBar) != "undefined")
	{
        if (top.ToolBar.DefaultButton == null || top.ToolBar.DefaultButton.oHTMLElement == null) return;
        top.ToolBar.DefaultButton.oHTMLElement.click();
    }
  }
  catch(err){};
}


function Row_SelectChanged(){
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



  //选中指定行
  //bFireEvent 是否触发Selected事件，默认为true
  //bFireSelectChangedEvent 是否触发SelectChanged事件，默认为true
  function SelectRow(tr){
    if (grid.AllowMultiSelect){  //可多选
      oSelectedRows[oSelectedRows.length] = tr;
      if (grid.ShowSelectorCol) {   //显示选择器列
        tr.firstChild.firstChild.checked = true;
        if (grid.AllowAllSelect){  //允许一次性全部选中
		  var checkbox = document.all['JsWebGrid1_HeaderCheckbox'];
          checkbox.checked = true;
          if (oSelectedRows.length != tabBody.rows.length-1){  //如果没全部选中，就使标题行上的checkbox选中并变灰
            checkbox.style.filter = "alpha(opacity=50)";
          }
          else {  //使标题行上的checkbox选中并不变灰
            checkbox.style.removeAttribute("filter");
          }
        }
      }
    }
    else {   //只能单选
      if (oSelected != null) UnSelectRow(oSelected);  //选取消选中以前被选中的行
      oSelected = tr;
      if (grid.ShowSelectorCol){
        tr.firstChild.className = "jkcListView_SelectorTD_Selected";
		tr.firstChild.firstChild.checked = true;
      }
    }

	tr.className = 'jkcListView_TR_Selected';
    tr.Selected = true;
/*	
	if (bFireEvent !== false){
      FireEvent(evt_RowSelected, oDOMRow);
	  if (bFireSelectChangedEvent !== false) 
        FireEvent(evt_RowSelectChanged, oDOMRow);
	}
*/	
  }


  //取消选中指定行
  function UnSelectRow(tr, bFireEvent, bFireSelectChangedEvent){
    if (grid.AllowMultiSelect){  //可多选
      var iIndex = IndexOfSelected(tr);
	  if (iIndex < 0) return;
	  oSelectedRows.splice(iIndex, 1);
//      for (var i=iIndex; i<oSelectedRows.length-1; i++)
//        oSelected[i] = oSelected[i+1];
//      oSelected.length -- ;
      if (grid.ShowSelectorCol) {
        tr.firstChild.firstChild.checked = false;
		var checkbox = document.all['JsWebGrid1_HeaderCheckbox'];
        if (grid.AllowAllSelect){
          if (oSelectedRows.length != 0){   //如果不是一个都没选中，就使标题行上的checkbox选中并变灰
            checkbox.checked = true;
            checkbox.style.filter = "alpha(opacity=50)";
          }
          else {  //使标题行上的checkbox不选中
            checkbox.checked = false;
            checkbox.style.removeAttribute("filter");
          }
        }
      }
    }
    else {
      oSelected = null;
      if (grid.ShowSelectorCol){
        tr.firstChild.className = "jkcListView_SelectorTD_Default";
      }
    }

	tr.className = GetRowDefaultClassName(tr);
    tr.Selected = false;
	
  }
  
  
  function GetRowDefaultClassName(tr){
    if (!grid.AllowInterlace)
	  return 'jkcListView_TR_Default';
	else
      return (tr.rowIndex-1) % 2 == 0 ? 'jkcListView_TR_Default_Interlace': 'jkcListView_TR_Default';
  }
  


var wg_States = [];
var wg_Names = new Array();
var wg_Global = {
  Resizer : null,
  Dragger : null,
  IsHeaderDragged : false, 
  MouseMoved : false, 
  MouseCoord : null, 
  TargetHeader : null, 
  ColSizeTarget : null, 
  ColDragTarget : null, 
  ColPointer : null, 
  MoveColEventArgs : null, 
  SetMenuCssFile : function(cssFile) {if (typeof(Menu) != "undefined") Menu.prototype.cssFile = cssFile;}, 
  LastSelObj: null, 
  SelectorObj: null, 
  TempResizeID : 0, 
  IsResizing : false
};


function wgGrid(id) {
  this.ID = id;
  this.FrameObj = document.getElementById(id);
  this.PopupObj = null;
  this.XmlObj = NewXmlDoc();
  this.XmlObj.loadXML(document.all[id + "_XML"].value);
  this.ColumnsXmlNode = this.XmlObj.selectSingleNode("//Columns");
  this.LayoutSetXmlNode = this.XmlObj.selectSingleNode("//Settings");
  this.DraggerStyle = "position: absolute;z-index: 299;cursor:default;display: none;background-color: RoyalBlue;color: White;filter: Alpha(Style=1, Opacity=90);vertical-align: middle";
  this.ResizerStyle = "position: absolute;z-index: 299;width:2px;background-color:gray;display: none;";
  this.Resizer = null;
  this.ColPointer = null;
  this.LastRequestObj = null;
  this.LastSelObj = null;
  this.IsVisible = this.IsVisible = function() {  return (this.FrameObj.style.display != "none");  };
  this.MinColWidth = 20;
  
  this.RecordCount = parseInt(this.LayoutSetXmlNode.getAttribute('RecordCount'));
  this.PageSize = parseInt(this.LayoutSetXmlNode.getAttribute('PageSize'));
  this.PageCount = parseInt(this.LayoutSetXmlNode.getAttribute('PageCount'));
  this.CurrentPage = parseInt(this.LayoutSetXmlNode.getAttribute('CurrentPage'));
  this.CurrentPageRecordCount = parseInt(document.all[this.ID].CurrentPageRecordCount);
  this.AllowSelect = ToBoolean(this.LayoutSetXmlNode.getAttribute('AllowSelect'), true);
  this.AllowUnSelect = ToBoolean(this.LayoutSetXmlNode.getAttribute('AllowUnSelect'), false);
  this.AllowMultiSelect = ToBoolean(this.LayoutSetXmlNode.getAttribute('AllowMultiSelect'), false);
  this.AllowAllSelect = ToBoolean(this.LayoutSetXmlNode.getAttribute('AllowAllSelect'), true);
  this.AllowInterlace = ToBoolean(this.LayoutSetXmlNode.getAttribute('AllowInterlace'), true);
  this.ShowSelectorCol = ToBoolean(this.LayoutSetXmlNode.getAttribute('ShowSelectorCol'), false);
  this.AllowResizeCol = ToBoolean(this.LayoutSetXmlNode.getAttribute('AllowResizeCol'), true);
  this.AllowPagging = ToBoolean(this.LayoutSetXmlNode.getAttribute('AllowPagging'), true);
  this.AllowSort = ToBoolean(this.LayoutSetXmlNode.getAttribute('AllowSort'), true);

  this.Columns = new Array();
  this.VisibleColumns = new Array();

  this.Columns.Count = 0;
  for (var i=0; i<this.ColumnsXmlNode.childNodes.length; i++){
	var columnNode = this.ColumnsXmlNode.childNodes[i];
	var column = new Column(columnNode);
	column.Index = i;
	this.Columns[columnNode.getAttribute('Name')] = this.Columns[i] = column;
	this.Columns.Count ++;
  }
  
  this.VisibleColumns.Count = 0;
  var iIndex = 0;
  for (var i=0; i<this.Columns.Count; i++){
	if (this.Columns[i].Visible){
	  this.VisibleColumns[iIndex] = this.Columns[i];
	  this.Columns[i].VisibleIndex = iIndex;
	  iIndex ++;
	  this.VisibleColumns.Count ++;
	}
  }

//  this.Columns = new Columns(this.ColumnsXmlNode);
//  this.VisibleColumns = new VisibleColumns(this.Columns);

  return this;
}

function Columns(ColumnsNode){
  this.Count = 0;
  for (var i=0; i<ColumnsNode.childNodes.length; i++){
	var columnNode = ColumnsNode.childNodes[i];
	var column = new Column(columnNode);
	column.Index = i;
	this[columnNode.getAttribute('Name')] = this[i] = column;
	this.Count ++;
  }
}

function VisibleColumns(Columns){
  this.Count = 0;
  var iIndex = 0;
  for (var i=0; i<Columns.Count; i++){
	if (Columns[i].Visible){
	  this[iIndex] = Columns[i];
	  Columns[i].VisibleIndex = iIndex;
	  iIndex ++;
	  this.Count ++;
	}
  }
}

function Column(columnNode){
  this.Name = this.FieldName = columnNode.getAttribute('Name');
  this.Text = columnNode.getAttribute('Text');
  this.Width = parseInt(columnNode.getAttribute('Width'));
  this.Type = columnNode.getAttribute('DataType');
  this.Align = columnNode.getAttribute('Align');
  this.DataType = columnNode.getAttribute('DataType');
  this.ShowType = columnNode.getAttribute('ShowType');
  this.Enum = columnNode.getAttribute('Enum');
  this.FilterString = columnNode.getAttribute('FilterString');
  this.Visible = ToBoolean(columnNode.getAttribute('Visible'), true);
  this.Disabled = ToBoolean(columnNode.getAttribute('Disabled'), false);
  this.AllowFilter = ToBoolean(columnNode.getAttribute('AllowFilter'), true);
  this.AllowResize = ToBoolean(columnNode.getAttribute('AllowResize'), true);
  if (this.Width == null || isNaN(this.Width))
    this.Width = 100;
}

//用于查询功能中，填充输出参数的值
//oDOMRow - 列表行对象
//返回值就是填充后的输出参数的XML 
function FillOutputParamValue(){ 
  var i, iCount, xmlDoc, oNode, oNodeList, sFieldName;
  xmlDoc = OutParams.XmlDoc;
  oNodeList = xmlDoc.documentElement.selectNodes('./参数[@列名]');
  iCount = oNodeList.length;
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
	sFieldName = oNode.getAttribute('列名');  //这个参数定义就是指定的列名，在oDOMRow行上不应该不存在此列
    if (!grid.AllowMultiSelect){
      oNode.setAttribute('值', GetCellValue(oSelected, sFieldName));
    }
    else {  //oDOMRow是行集合
      if (oSelectedRows.length == 0){
        oNode.setAttribute('值', '');
        continue;
      }
	  else if (oSelectedRows.length == 1){
		oNode.setAttribute('值', GetCellValue(oSelectedRows[0], sFieldName));
		continue;
	  }
      var newDoc = NewXmlDoc();
      var oElement = newDoc.createElement('MultiValues');
      newDoc.appendChild(oElement);
      for (var j=0; j<oSelectedRows.length; j++){
        var oItemNode = newDoc.createElement('Item');
        oItemNode.text = GetCellValue(oSelectedRows[j], sFieldName);
        oElement.appendChild(oItemNode);
      }
      oNode.setAttribute('值', newDoc.xml.replace('\r\n', ''));
    }
  }
  if (grid.AllowMultiSelect){
	OutParams.SetValue('SelectedCount', oSelectedRows.length);
  }
  else {
	OutParams.SetValue('SelectedCount', oSelected==null?0:1);
	if (iTimeoutID_RowIndex != null)
	  window.clearTimeout(iTimeoutID_RowIndex);
	iTimeoutID_RowIndex = window.setTimeout(UpdateSelectedRowIndexToHistory, 100);
  }
}

//更新前进、后退历史列表中的选中行号
function UpdateSelectedRowIndexToHistory(){
  if (iTimeoutID_RowIndex != null && iTimeoutID_RowIndex > 0)
    window.clearTimeout(iTimeoutID_RowIndex);
  if (bFirstTimeSelect){
    bFirstTimeSelect = false;
    return;
  }
  if (oSelected == null || isNaN(parseInt(oSelected.rowIndex))) return;
  var iIndex = oSelected.rowIndex - 1;
  var xmlDoc = NewXmlDoc();
  var oRootNode = xmlDoc.createElement('Params');
  oRootNode.setAttribute('FunctionID', g_FunctionID);
  oRootNode.setAttribute('MainTitle', PostBackInfo.MainTitle);
  oRootNode.setAttribute('CurrentPage', grid.CurrentPage);
  oRootNode.setAttribute('SelectedRowIndex', iIndex);
  oRootNode.setAttribute('InParamXML', InParams.ToString());
  xmlDoc.appendChild(oRootNode);
  var sURL = "query.aspx";
  var vURL = top.GetPostFormParams(sURL, xmlDoc);
//  top.aHistoryBack.push(vURL);
  if (top.aHistoryBack.length > 0)
    top.aHistoryBack[top.aHistoryBack.length-1] = vURL;
  else
    top.aHistoryBack.push(vURL);
  
}

function GetCellValue(tr, colName){
  if (tr == null || tr.nodeName != 'TR') return '';
  for (var i=0; i<tr.cells.length; i++){
  //如果是选择列（提供可选的复选框），则没有“ColName”属性，这里需要判断
    if (typeof(tabBody.rows[0].cells[i]) == 'undefined') continue;
	if ((typeof(tabBody.rows[0].cells[i].ColName) != 'undefined') && (tabBody.rows[0].cells[i].ColName == colName)){
	  if (tr.cells[i].OriginalValue != null)
	    return tr.cells[i].OriginalValue;
	  else
	    return tr.cells[i].innerText;
	}
  }
  return '';
}

//备份输出参数的定义
function BackupOutputParamDefine(){
  var i, iCount, xmlDoc, oNode, oNodeList, sValue;
  xmlDoc = OutParams.XmlDoc;
  oNodeList = xmlDoc.documentElement.selectNodes('./参数[not(@已实例化)]');
  iCount = oNodeList.length;
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
	if (oNode.getAttribute('名称') == 'SelectedCount') continue;
	if (oNode.getAttribute('列名') != null) continue;
	try{
	  oNode.setAttribute('列名', oNode.getAttribute('值'));
	}
	catch(err){
		
	}
  }
}

//当行为时清除输出参数
function ClearOutputParamDefine(){
  var i, iCount, xmlDoc, oNode, oNodeList, sValue;
  xmlDoc = OutParams.XmlDoc;
  oNodeList = xmlDoc.documentElement.selectNodes('./参数[not(@已实例化)]');
  iCount = oNodeList.length;
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
	if (oNode.getAttribute('名称') == 'SelectedCount'){
	    oNode.parentNode.removeChild(oNode);
	}
	try{
	  oNode.setAttribute('值', oNode.getAttribute('列名'));
	}
	catch(err){
		
	}
  }
  OutParams.XmlDoc = xmlDoc;
}
  
  //tr在SelectedRows数组中的索引
  function IndexOfSelected(tr){
    var iCount = oSelectedRows.length;
    for (var i=0; i<iCount; i++){
      if (oSelectedRows[i] == tr) return i;
    }
    return -1;
  }

  
  
  //选中所有行
  function SelectAll(){
    for (var i=1; i<tabBody.rows.length; i++){
      if (!tabBody.rows[i].Selected)  //如果以前没被选中并且是可以被选中
        SelectRow(tabBody.rows[i]);
    }
    //设置输出参数
    FillOutputParamValue();
  }

  
  //取消选中所有已选中的行
  function UnSelectAll(){
    var iCount = oSelectedRows.length;
    for (var i=0; i<iCount; i++){
      UnSelectRow(oSelectedRows[0]);
    }
    //设置输出参数
    FillOutputParamValue();
  }


  
  function oCheckbox_Click(element){
	if (element == null) element = this;
    event.cancelBubble = true;
    if (!grid.AllowSelect){   //不允许行选中
      element.checked = !element.checked;  //保持未点击之前的状态
      return;
    }
    var tr = element.parentElement.parentElement;

    if (tr.Selected){  //此行已选中
      if (oSelectedRows.length != 1 || grid.AllowUnSelect)  //如果当前行可以被选中 并且 允许不选中
        UnSelectRow(tr);
      else
        element.checked = !element.checked;
    }
    else {
      if (tr.Selectable !== false && grid.AllowSelect)  //如果当前行可以被选中 并且 允许被选中
        SelectRow(tr); 
    }
	FillOutputParamValue();
	//window.setTimeout('top.ResetToolBar()', 1);
  }


  
  function oHeadCheckBox_Click(element){
	if (element == null) element = this;
    if (!grid.AllowSelect){
      element.checked = !element.checked;
      return;
    }
  
    if (oSelectedRows.length == tabBody.rows.length-1){  //可选行已全部选中
      UnSelectAll();
    }
    else {
      SelectAll();
    }
	FillOutputParamValue();
	//window.setTimeout('top.ResetToolBar()', 1);
  }
  
  function Deal_Click(){
    var oHTMLTR = event.srcElement.parentElement.parentElement;
	TR_Click(oHTMLTR);
	TR_DblClick(oHTMLTR);
  }


function ChangePage(PageNum){
  PageNum = parseInt(PageNum);
  if (isNaN(PageNum) || PageNum < 0) return;
  document.all.CurrentPage.value = PageNum;
  DoSubmit();
}

function ChangePageSize(sPageSize){
  grid.PageSize = parseInt(sPageSize);
  SaveListState();
  document.all.PageSize.value = sPageSize;
  document.all.CurrentPage.value = 1;
  DoSubmit();
}


function GetColName(oHTMLTD){
  var el = oHTMLTD.offsetParent;
  var ColName = el.rows[0].cells[GetCellIndex(oHTMLTD)].ColName;
  return ColName;
}

function GetColumn(oHTMLTD){
  return grid.Columns[GetColName(oHTMLTD)];
}

function GetColAttr(oHTMLTD){
	
}

function GetHTMLHeadTD(ColumnName){
  for (var i=0; i<tabHead.rows[1].cells.length; i++){
	if (tabHead.rows[1].cells[i].ColName == ColumnName) return tabHead.rows[1].cells[i];
  }
  return null;
}

function GetTDByName(tr, ColName){
  if (tr == null) return null;
  for (var i=0; i<tr.cells.length; i++){
	if (tr.offsetParent.rows[0].cells[i].ColName == ColName) return tr.cells[i];
  }
  return null;
  
}

function GetCellIndex(oHTMLTD){
  var iCells = oHTMLTD.parentElement.cells.length;
  for (var i=0; i<iCells; i++){
	if (oHTMLTD.parentElement.cells[i] == oHTMLTD)
	  return i;
  }
  return -1;
}

function GetCellIndexByColumn(column){
  var oHTMLTD = GetHTMLHeadTD(column.Name);
  var iIndex = GetCellIndex(oHTMLTD);
  return iIndex;
}


function oHTMLTHeadTD_MouseMove(oHTMLTD){
	if (grid == null) return;	//grid对象还没创建
    if (!grid.AllowResizeCol) return;
    if (event.button != 0){
	  if (event.button != 1) return;
	  if (oHTMLTD.className != "jkcListView_TitleTD_Down") return;
	  if (oHTMLTD.ColName == 'LastCell') return;   //是最后一列

      var iX = GetMousePointX();   //鼠标在文档中的X坐标
      if (iMousePointX - iX > 3 || iMousePointX - iX < -3){     //横向移动3个象素以上
        StartDrag(oHTMLTD);
      }
	  return;     //按下了鼠标键在标题行上移动
	}

    if (oHTMLTD.ColName == "LastCell"){  //鼠标在标题行最后一单元格上
      if (event.offsetX < 3 && grid.VisibleColumns[grid.VisibleColumns.Count-1] != null && grid.VisibleColumns[grid.VisibleColumns.Count-1].AllowResize)
        oHTMLTD.style.cursor = "col-resize";
      else
        oHTMLTD.style.removeAttribute("cursor");
      return; 
    }

    if (event.offsetX < 3 || event.offsetX > oHTMLTD.clientWidth-4){
      var column = grid.Columns[GetColName(oHTMLTD)];  //当前列对象
      var oPrevVisibleCell = grid.VisibleColumns[column.VisibleIndex - 1];
      if (oPrevVisibleCell != null && oPrevVisibleCell.AllowResize && event.offsetX < 3   //前一可见列不为空且可以调整列宽 并且 鼠标在前几个象素位置
          || column.Name != "LastCell" && column.AllowResize && event.offsetX > oHTMLTD.clientWidth-4)  //当前列不是最后一列且可以调整列宽 并且 鼠标在后几个象素位置
      oHTMLTD.style.cursor = "col-resize";
	}
    else {
      oHTMLTD.style.removeAttribute("cursor");
	}
}

function oHTMLTHeadTD_MouseDown(oHTMLTD){
	if (oHTMLTD == null) oHTMLTD = this;
    if (event.button != 1) return;    //按下的不是左键
    if (oHTMLTD.style.cursor == "col-resize"){    //鼠标在可调整列宽的位置
	  StartResize(oHTMLTD);
	  return;
	}
//return;	
	if (oHTMLTD.ColName == 'LastCell') return;   //是最后一列
//    var oDOMCell = grid.Columns[oHTMLTD.ColName];
//    if (!AllowSwapCol && (!AllowSort || !oDOMCell.AllowSort)) return;  //不能调整列位置也不能排序
    oHTMLTD.className = "jkcListView_TitleTD_Down";
    iMousePointX = GetMousePointX();
	iOffsetX = event.clientX - posLib.getClientLeft(oHTMLTD);

    oHTMLTD.setCapture();
}

function StartResize(oHTMLTD){
    iMousePointX = GetMousePointX();  //鼠标按下时的X坐标
    
    iSplitMinLeft = iMousePointX - event.offsetX + grid.MinColWidth;  //鼠标点 - 鼠标与TD左端的距离 + 最小列宽

    if (oHTMLTD.ColName == "LastCell"){
      grid.ResizeColumn = grid.VisibleColumns[grid.VisibleColumns.Count-1];
      iSplitMinLeft -= grid.ResizeColumn.Width;
    }
    else {
      grid.ResizeColumn = GetColumn(oHTMLTD);  //当前列对象
      if (event.offsetX < 5){
        grid.ResizeColumn = grid.VisibleColumns[grid.ResizeColumn.VisibleIndex - 1];
        iSplitMinLeft -= grid.ResizeColumn.Width;
      }
    }

    LaySplit.style.left = iMousePointX;   //分隔条的左坐标是鼠标按下的X坐标
    LaySplit.style.top = 0;   
    LaySplit.style.height = document.all[grid.ID].clientHeight;    //高度是列表对象的高度
    LaySplit.style.display = '';   //显示
    LaySplit.setCapture();
}


function oHTMLTHeadTD_MouseUp(oHTMLTD){
	if (oHTMLTD == null) oHTMLTD = this;
	oHTMLTD.className = "jkcListView_TitleTD_Default";
    oHTMLTD.releaseCapture();
}

function oHTMLTD_MouseOver(oHTMLTD){
	
}

function oHTMLTHeadTD_MouseOut(oHTMLTD){
	if (oHTMLTD == null) oHTMLTD = this;
    oHTMLTD.style.removeAttribute("cursor");
    if (oHTMLTD.contains(event.toElement)) return; 　//在同一个TD中移动

    if (oHTMLTD.scrollWidth > oHTMLTD.clientWidth)
      oHTMLTD.removeAttribute("title");
}

function oHTMLTHeadTD_Click(oHTMLTD){
//alert(grid.AllowSort);	
//alert(oHTMLTD.outerHTML);
    if (!grid.AllowSort) return;
	if (oHTMLTD == null) oHTMLTD = this;
    if (oHTMLTD.style.cursor == "col-resize") return; 
    var column = grid.Columns[oHTMLTD.ColName];
//	alert(column.Type);
	if (column.Type == 'S' || column.Type == 'N' || column.Type == 'I' || column.Type == 'D' || column.Type == 'DT'){
	  StartSort(column);
	}
}

function oHTMLTHeadTD_DblClick(oHTMLTD){
	if (oHTMLTD == null) oHTMLTD = this;
    if (oHTMLTD.style.cursor != "col-resize") return; 

    AutoResizeWidth(grid.ResizeColumn.Index);
	SaveListState();
}


function StartSort(column){
  if (document.all.SortColumn.value == column.Name && document.all.SortDirection.value == 'ASC')
    document.all.SortDirection.value = 'DESC';
  else
    document.all.SortDirection.value = 'ASC';
  document.all.SortColumn.value = column.Name;
  DoSubmit();
}

/*
     oHTMLTD.onmousemove = oHTMLTHeadTD_MouseMove;
    oHTMLTD.onmousedown = oHTMLTHeadTD_MouseDown;
    oHTMLTD.onmouseup = oHTMLTHeadTD_MouseUp;
	oHTMLTD.onmouseover = oHTMLTD_MouseOver;   //与数据行的TD调用同一个函数
    oHTMLTD.onmouseout = oHTMLTHeadTD_MouseOut;
    oHTMLTD.onclick = oHTMLTHeadTD_Click;
    oHTMLTD.ondblclick = oHTMLTHeadTD_DblClick;

*/


  //鼠标指针点的X坐标
  function GetMousePointX(){
    var iX = parseInt(event.clientX) + parseInt(document.body.clientLeft) ;
    return iX;
  }



  //移动分隔条
  function LaySplit_MouseMove(){
    var iLeft = GetMousePointX();
    
    if (iLeft <= iSplitMinLeft){   //小于最小宽度
      LaySplit.style.left = iSplitMinLeft;
    }
    else {
      LaySplit.style.left = iLeft;
    }
  }
  

  //取消调整列宽
  function LaySplit_MouseDown(){
    event.cancelBubble = true;
    if (event.button == 3){   //按下右键(在拖动时，左键是已经按下状态，再按下右键后event.button就等于3了)
      LaySplit.releaseCapture()
      LaySplit.style.display = "none"   
    }
  }


  //完成调整动作
  function LaySplit_MouseUp(){
    event.cancelBubble = true;
    LaySplit.releaseCapture();
    LaySplit.style.display = "none";
    //新的宽度 = 以前的宽度 + (调整宽度后的分隔条X坐标 - 调整宽度前的分隔条X坐标)
    iNewWidth = grid.ResizeColumn.Width + (parseInt(LaySplit.style.left) - iMousePointX);

    SetColWidth(GetHTMLCellIndex(grid.ResizeColumn), iNewWidth);
	SaveListState();
  }
  

  
  //自动调整指定列号的列的宽度
  function AutoResizeWidth(iColIndex){
    var i, iWidth, iMaxWidth, oHTMLTD, oResizeCell, sColName;
	var iCellIndex = GetCellIndexByColumn(grid.Columns[iColIndex]);
	
	//先把这一列的宽度设置为最小值
	SetColWidth(iCellIndex, grid.MinColWidth);

    //再找出最大宽度
    var iMaxWidth = tabHead.rows[1].cells[iCellIndex].scrollWidth;
    for (i=1; i<tabBody.rows.length; i++){
      iWidth = tabBody.rows[i].cells[iCellIndex].scrollWidth;
      if (iWidth > iMaxWidth) iMaxWidth = iWidth;
    }

	iMaxWidth += 4; //加上边框的宽度 和 文字与TD的LeftBorder的距离
	//最后设置为最大宽度
	SetColWidth(iCellIndex, iMaxWidth);
  }
  
  
  //设置指定列的宽度
  //iColIndex: 列号
  //iWidth: 宽度值
  function SetColWidth(iColIndex, iWidth){
    var column = grid.Columns[tabHead.rows[0].cells[iColIndex].ColName];
    column.Width = iWidth;
    tabHead.rows[0].cells[iColIndex].width = iWidth;
	tabBody.rows[0].cells[iColIndex].width = iWidth;
  }


  
  function StartDrag(oHTMLTD){
    var i, iLeft, iWidth, iVisibleCellIndex;
///    iVisibleCellIndex = GetCellIndex(oHTMLTD);
    var column = GetColumn(oHTMLTD);
    iVisibleCellIndex = column.VisibleIndex;
	grid.DraggingColumn = column;
    iLeft  = iMousePointX - iOffsetX;          //被调整的列与客户区左端的距离
    //保存被点击列之前的列的坐标
    for (i=iVisibleCellIndex-1; i>=0; i--){    
      arrColPos[i] = new Array(3);
      iWidth = grid.VisibleColumns[i].Width;   //第i列的宽度
      if (i == 0){   //第一列  
        arrColPos[0][0] = iLeft - iWidth;
        arrColPos[0][1] = Math.round(iLeft - iWidth / 2);
        arrColPos[0][2] = arrColPos[0][0];
      }
      else {
        arrColPos[i][0] = Math.round(iLeft - iWidth - grid.VisibleColumns[i-1].Width / 2);
        arrColPos[i][1] = Math.round(iLeft - iWidth / 2);
        arrColPos[i][2] = iLeft - iWidth;
        iLeft -= iWidth;
      }
    }

    iLeft  = iMousePointX - iOffsetX + column.Width;    //被调整列的下一列与客户区左端的距离

    //保存被点击列之后的列的坐标
    for (i=iVisibleCellIndex+1; i<grid.VisibleColumns.Count; i++){    
      arrColPos[i] = new Array(3)
      iWidth = grid.VisibleColumns[i].Width;   //第i列的宽度
      if (i == grid.VisibleColumns.Count - 1){   //最后一列  
        arrColPos[i][0] = Math.round(iLeft + iWidth / 2);
        arrColPos[i][1] = iLeft  + iWidth;
        arrColPos[i][2] = arrColPos[i][1];
      }
      else {
        arrColPos[i][0] = Math.round(iLeft + iWidth / 2);
        arrColPos[i][1] = Math.round(iLeft + iWidth + grid.VisibleColumns[i+1].Width / 2);
        arrColPos[i][2] = iLeft  + iWidth;
        iLeft += iWidth;
      }
    }

    if (arrColPos.length == 0) return;
	
    //限定Drag的最小Left和最大Left
    switch (iVisibleCellIndex){
      case 0 : {                 //第一列
        iDragMinLeft = iMousePointX;1
//		alert(arrColPos[grid.VisibleColumns.Count - 1]);
        iDragMaxLeft = arrColPos[grid.VisibleColumns.Count - 1][1];
        break
      }
      case grid.VisibleColumns.Count - 1 : {   //最后一列
        iDragMinLeft = arrColPos[0][0];
        iDragMaxLeft = iMousePointX;
        break
      }
      default : {
        iDragMinLeft = arrColPos[0][0];
        iDragMaxLeft = arrColPos[grid.VisibleColumns.Count - 1][1];
        break
      }
    }
/*
    oDragTD = oHTMLTD.cloneNode(true);
	oDragTD.style.position = 'absolute';
	oDragTD.style.zIndex = 100;
	oDragTD.style.backgroundColor = '#666666';
	oDragTD.style.color = '#FFFFFF';
	oDragTD.style.filter = 'alpha(opacity=40)';
    oDragTD.style.top = 0;  
    oDragTD.style.left = divHead.scrollLeft + posLib.getLeft(oHTMLTD);
    oDragTD.style.width = oHTMLTD.clientWidth;
    oDragTD.style.height = oHTMLTD.clientHeight;
*/	
//	tabHead.rows[1].appendChild(oDragTD);
	LayInsertPos.style.top = tabHead.rows[1].cells[grid.DraggingColumn.Index].offsetHeight;
	var oDragTD = tabDrag.rows[0].cells[0];
    oDragTD.innerHTML = oHTMLTD.innerHTML; 
	oDragTD.className = oHTMLTD.className;
	oDragTD.style.backgroundColor = '#666666';
	oDragTD.style.color = '#FFFFFF';
	oDragTD.style.filter = 'alpha(opacity=60)';
	tabDrag.style.display = '';
	tabDrag.style.position = 'absolute';
	tabDrag.style.zIndex = 100;
    tabDrag.style.top = 0;  
    tabDrag.style.left = divHead.scrollLeft + posLib.getLeft(oHTMLTD);
    tabDrag.style.width = oHTMLTD.clientWidth;
    tabDrag.style.height = oHTMLTD.clientHeight;
	
	tabDrag.setCapture(false);
	tabDrag.onmousemove = oDragTD_MouseMove;
	tabDrag.onmouseup = oDragTD_MouseUp;
//    alert(tabHead.outerHTML);
//    oDragTD = tabHead.rows[0].insertCell();
  }

  function oDragTD_MouseMove(){
    event.cancelBubble = true;
    var i, iX;
    iX = GetMousePointX();   //鼠标在文档中的X坐标
    if (iX < iDragMinLeft) iX = iDragMinLeft;
    if (iX > iDragMaxLeft) iX = iDragMaxLeft;
    //循环检测鼠标坐标是否在可插入列的坐标范围内，如果是，显示插入图标，否则隐藏插入图标
    for (i in arrColPos){
      if (iX >= arrColPos[i][0] && iX <= arrColPos[i][1]){
        LayInsertPos.style.display = "";  //显示
        LayInsertPos.style.left = Math.round(arrColPos[i][2] - LayInsertPos.offsetWidth / 2) - 1;
//		alert(LayInsertPos.outerHTML);
        iDescCellIndex = parseInt(i);    //插入的目标列号
        break;
      }
      else
        LayInsertPos.style.display = "none";
    }
    this.style.left = parseInt(this.style.left) + (iX - iMousePointX);
    iMousePointX = iX;
  }
  
  
  function oDragTD_MouseUp(){
//	prompt('', oDocument.body.innerHTML);
    event.cancelBubble = true;
    tabDrag.style.display = "none";
//return;
    var column = grid.DraggingColumn;
    var oHTMLTD= GetHTMLHeadTD(column.Name);
    oHTMLTD.className = "jkcListView_TitleTD_Default";
    tabDrag.releaseCapture();
    if (LayInsertPos.style.display == "none"){ //没有出现列插入标志
      return
    }     
    LayInsertPos.style.display = "none";
//    var iSrcCellIndex = grid.DraggingColumn.VisibleIndex;
//    if (iDescCellIndex > grid.DraggingColumn.VisibleIndex) iDescCellIndex --;
	var col = grid.Columns.splice(grid.DraggingColumn.Index, 1);
	grid.Columns.splice(grid.VisibleColumns[iDescCellIndex].Index, 0, col[0]);
	
	SaveListState();
/******************************************************************/	
//	DoSubmit();
//	var a = 0;
    var iSrcCellIndex = column.VisibleIndex;
	
	var iSrcRealCellIndex = GetCellIndexByColumn(column);
	var iDescRealCellIndex = GetCellIndex(GetHTMLHeadTD(grid.VisibleColumns[iDescCellIndex].Name));
	var sPos;
    if (iDescRealCellIndex > iSrcRealCellIndex){   //如果目标列在源列之后
      sPos = "afterEnd";   //插入到目标列之后
    }
    else {
      sPos = "beforeBegin";  //插入到目标列之前
    }
    //交换标题行的单元格
    var tdEmpty = document.createElement("TD");
    var tdSrc = tabHead.rows[1].cells[iSrcRealCellIndex];
    var tdDesc = tabHead.rows[1].cells[iDescRealCellIndex].insertAdjacentElement(sPos, tdEmpty);
//	var tdDesc = GetHTMLHeadTD(grid.VisibleColumns[iDescCellIndex]).insertAdjacentElement(sPos, tdEmpty);
    tabHead.rows[1].replaceChild(tdSrc, tdDesc);

    tdEmpty = document.createElement("TD");
    tdSrc = tabHead.rows[0].cells[iSrcRealCellIndex];
    tdDesc = tabHead.rows[0].cells[iDescRealCellIndex].insertAdjacentElement(sPos, tdEmpty);
    tabHead.rows[0].replaceChild(tdSrc, tdDesc);

    for (var i=0; i<tabBody.rows.length; i++){   //循环所有行
      tdEmpty = document.createElement("TD");
      tdSrc =  tabBody.rows[i].cells[iSrcRealCellIndex];
      tdDesc = tabBody.rows[i].cells[iDescRealCellIndex].insertAdjacentElement(sPos, tdEmpty);
      tabBody.rows[i].replaceChild(tdSrc, tdDesc);
    }
//    RenewIndex(iSrcCellIndex, iDescCellIndex, sPos);
//    RenewVisibleIndex();
//    oHTMLTD.className = "jkcListView_TitleTD_Default";
//    oDragTD.releaseCapture();
//	oDragTD.removeNode(true);
	
    grid.VisibleColumns.Count = 0;
    var iIndex = 0;
    for (var i=0; i<grid.Columns.Count; i++){
	  grid.Columns[i].Index = i;
	  if (grid.Columns[i].Visible){
	    grid.VisibleColumns[iIndex] = grid.Columns[i];
	    grid.Columns[i].VisibleIndex = iIndex;
	    iIndex ++;
	    grid.VisibleColumns.Count ++;
	  }
    }
	
return;	
	
//alert();	
//    var iSrcVisibleCellIndex = oHTMLTD.cellIndex;
//    var iDescVisibleCellIndex = oDOMHeadRow[iDescCellIndex].oHTMLElement.cellIndex;
    var sSrcCellName = column.Name;
    var sDescCellName = grid.VisibleColumns[iDescCellIndex].Name;
    
	var sPos;
    if (iDescCellIndex > iSrcCellIndex){   //如果目标列在源列之后
      sPos = "afterEnd";   //插入到目标列之后
    }
    else {
      sPos = "beforeBegin";  //插入到目标列之前
    }
	
	//prompt('', oDocument.documentElement.outerHTML);
    //交换标题行的单元格
    var tdEmpty = document.createElement("TD");
    var tdSrc = oHTMLTD;
    var tdDesc = GetTDByName(tabHead.rows[1], sDescCellName).insertAdjacentElement(sPos, tdEmpty);
//	var tdDesc = GetHTMLHeadTD(grid.VisibleColumns[iDescCellIndex]).insertAdjacentElement(sPos, tdEmpty);
    tabHead.rows[1].replaceChild(tdSrc, tdDesc);

    tdEmpty = document.createElement("TD");
    tdSrc = GetTDByName(tabHead.rows[0], sSrcCellName);//tabHead.rows[0].cells[GetHTMLCellIndex(iSrcCellIndex)];
    tdDesc = GetTDByName(tabHead.rows[0], sDescCellName).insertAdjacentElement(sPos, tdEmpty); //tabHead.rows[0].cells[GetHTMLCellIndex(iDescCellIndex)].insertAdjacentElement(sPos, tdEmpty);
    tabHead.rows[0].replaceChild(tdSrc, tdDesc);

    tdEmpty = element.document.createElement("TD");
    tdSrc = tabBody.rows[0].cells[GetHTMLCellIndex(iSrcCellIndex)];
    tdDesc = tabBody.rows[0].cells[GetHTMLCellIndex(iDescCellIndex)].insertAdjacentElement(sPos, tdEmpty);
    tabBody.rows[0].replaceChild(tdSrc, tdDesc);

    for (var i=0; i<prop_iRows; i++){   //循环所有行
      tdEmpty = element.document.createElement("TD");
      tdSrc =  oDOMRows[i][sSrcCellName].oHTMLElement;
      tdDesc = oDOMRows[i][sDescCellName].oHTMLElement.insertAdjacentElement(sPos, tdEmpty);
      oDOMRows[i].oHTMLElement.replaceChild(tdSrc, tdDesc);
    }
    RenewIndex(iSrcCellIndex, iDescCellIndex, sPos);
    RenewVisibleIndex();
    oHTMLTD.className = "jkcListView_TitleTD_Default";
    oDragTD.releaseCapture();
	oDragTD.removeNode(true);
//	prompt('', oDocument.body.innerHTML);
	
  }

  

  //通过对象模型列对象的列号获取HTMLTD的cellIndex
  function GetHTMLCellIndex(column){
    if (grid.ShowSelectorCol)
	  return column.Index + 1;
	else
	  return column.Index;
  }


//根据功能编号保存列表状态到个性化设置的XML对象上
function SaveListState(){
  var i, iCount, xmlDoc, oRootNode, oColumnNode, oRowNode, oParaListNode, oNode, oColumnsNode, oRowsNode;
  xmlDoc = NewXmlDoc();
  oRootNode = xmlDoc.createElement("ListView");
  xmlDoc.appendChild(oRootNode);
  oRootNode.setAttribute('PageSize', grid.PageSize);
  oRootNode.setAttribute('FunctionID', g_FunctionID); 
  
  oColumnsNode = xmlDoc.createElement("Columns");
  oRootNode.appendChild(oColumnsNode);
  //生成Columns
  for (i=0; i<grid.Columns.Count; i++){
    var column = grid.Columns[i];
//    if (column.Disabled) continue;
    oColumnNode = xmlDoc.createElement("Column");
    oColumnsNode.appendChild(oColumnNode);
    oColumnNode.setAttribute('Name', column.Name);
    oColumnNode.setAttribute('Visible', column.Visible.toString());
    oColumnNode.setAttribute('Width', column.Width);
//      CloneProp2(oDOMHeadRow[i], oColumnNode);
  }
//    alert(xmlDoc.xml);
//    return xmlDoc;
  var sXPath = '//ListView[@FunctionID="' + g_FunctionID + '"]';
  var oNode = top.SetupDataXmlDoc.selectSingleNode(sXPath);
  if (oNode != null)
    oNode.parentNode.replaceChild(oRootNode, oNode);
  else
    top.SetupDataXmlDoc.documentElement.appendChild(oRootNode);
//    prompt('', xmlDoc.xml);
  //top.SaveSetupData();
}




  function ShowCustomDlg(){
	event.returnValue = false;
    var i, sHTML, sChecked, oDOMCell;
    var oPopup = window.createPopup();
	grid.PopupObj = oPopup;
    sHTML = '<html><head></head>';
    sHTML += '<body leftmargin="5" topmargin="0" style="font-size:9pt; color:#0030AB; overflow:hidden; background-color:#EBEBF3; border:1px solid #666666;">';
    sHTML += '<br>　请勾选要显示的列：';
    sHTML += '<div style="width:180; height:160; overflow:auto; margin:5px; border:1px solid #C0C0C0;">';
    sHTML += '<table width="100%" border="0" cellpadding="0" style="font-size:9pt;color:#0030AB; cursor:default">';

    for (i=0; i<grid.Columns.Count; i++){
      var column = grid.Columns[i];
      if (column.Disabled) continue;
      
      if (column.Visible)
        sChecked = ' checked';
      else
        sChecked = '';

      sHTML += '<tr>';
      sHTML += '<td width="20"><input type="checkbox" id="' + column.Name + '"';
      sHTML +=  sChecked + '></td>';
      sHTML += '<td><label for="' + column.Name + '" style="cursor:default">' + column.Text + '</label></td></tr>';
    }//end for
    sHTML += '</table></div><br>　　　';
//    sHTML += '<button id="btnDefault" class="uButton">默 认</button>　';
    sHTML += '<button id="btnOK" style="width:48; height:24; font:9pt">确 定</button>　　';
    sHTML += '<button id="btnCancel" style="width:48; height:24; font:9pt">取 消</button>';
    sHTML += '</body></html>';

    oPopup.document.write(sHTML);

//    oPopup.document.all.btnDefault.onclick = btnDefault_Click;
    oPopup.document.all.btnOK.onclick = btnOK_Click;
    oPopup.document.all.btnCancel.onclick = btnCancel_Click;
    oPopup.show(event.screenX, event.screenY, 200, 250)
//    prompt('',sHTML)
  }
  

  function btnOK_Click(){
    var i, iIndex, iCount, aCheckBox, oCheckbox, column;
	aCheckBox = grid.PopupObj.document.getElementsByTagName("INPUT");
	iCount = aCheckBox.length;
	iIndex = 0;
    grid.PopupObj.hide();
	for (i=0; i<iCount; i++){
	  oCheckbox = aCheckBox[i];
	  column = grid.Columns[oCheckbox.id];
	  SetColVisible(column, iIndex, oCheckbox.checked);
	  if (oCheckbox.checked) iIndex ++;
	}
	grid.VisibleColumns.length = iIndex;
	grid.VisibleColumns.Count = iIndex;	
    grid.PopupObj = null;
	SaveListState();
  }
  

  function btnCancel_Click(){
	grid.PopupObj.hide();
	grid.PopupObj = null;
  }
  
  
  
  //调整列显示状态
  function SetColVisible(column, iIndex, bVisible){
    var iCellIndex = GetCellIndexByColumn(column);
    if (bVisible){  //此列要显示
	  if (!column.Visible){  //此列没有显示
		tabHead.rows[0].cells[iCellIndex].style.display = '';
		tabHead.rows[1].cells[iCellIndex].style.display = '';

		for (var i=0; i<tabBody.rows.length; i++){
		  tabBody.rows[i].cells[iCellIndex].style.display = '';
		}
	  }
	  grid.VisibleColumns[iIndex] = column;
	  column.VisibleIndex = iIndex;
	}
	else {  //此列不要显示
	  if (column.Visible) {  //此列已经显示
		tabHead.rows[0].cells[iCellIndex].style.display = 'none';
		tabHead.rows[1].cells[iCellIndex].style.display = 'none';

		for (var i=0; i<tabBody.rows.length; i++){
		  tabBody.rows[i].cells[iCellIndex].style.display = 'none';
		}
	  }
	}
	column.Visible = bVisible;
	
  }

  function oHTMLFilterButton_Click(element){
    event.cancelBubble = true; 
    var i, sHTML, sChecked, column;
	column = grid.Columns[element.ColName];
    var sURL = 'JsGridFilter.aspx';
    var vArguments = new Object();
	vArguments.Document = document;
	vArguments.column = column;
    var sFeatures = 'dialogHeight:190px; dialogWidth:350px; scroll:no; status:no; help:no; resizable:no; center:yes';
	var sReturn = window.showModalDialog(sURL, vArguments, sFeatures);
	if (sReturn != null && sReturn != column.FilterString){
	  if (!(column.FilterString == null && sReturn == '')){ //如果以前是null，返回值是''，等于没有改变过滤条件
	    column.FilterString = sReturn;
	    if (column.FilterString == '')
	      element.className = 'jkcListView_FilterButton_1';
	    else
	      element.className = 'jkcListView_FilterButton_2';
	    StartFiltrate(column);
	  }
	}
	
  }
  
  
  
  function StartFiltrate(column){
	  var i;
	  var str = '<FilterColumns>';
	  for (i=0; i<grid.Columns.Count; i++){
	    if (grid.Columns[i].FilterString != null && grid.Columns[i].FilterString != ''){
		  str += grid.Columns[i].FilterString;
		}
	  }
	  str += '</FilterColumns>';
//prompt('', str);
      document.all.FilterString.value = str;
      document.all.CurrentPage.value = 1;  //设置了过滤条件后，始终转向第一页
      DoSubmit();
}

  function GetVisibleColumnXML(){
    var xmlDoc = new ActiveXObject("Msxml2.DOMDocument");
	var oRootNode = xmlDoc.createElement("Columns");
	xmlDoc.appendChild(oRootNode);
	
	for (var i=0; i<grid.VisibleColumns.length; i++){
	  var oNode = xmlDoc.createElement('Column');
	  oNode.setAttribute('Name', grid.VisibleColumns[i].Name);
	  oNode.setAttribute('Width', parseInt(grid.VisibleColumns[i].Width));
	  oRootNode.appendChild(oNode);
	}
	return xmlDoc.xml;
  }


function LoadList(){
  document.all.SelectedRowIndex.value = 0;
  DoSubmit();
}

function DoSubmit(){
  document.all.Form1.submit();
}



function Print(){
  top.JsClientProxy.ShowQueryReport(g_UserName, g_FunctionID); 
}

function ExportExcel(){
  window.open("QueryXLS.aspx?QueryFID=" + g_FunctionID + '0000' + '&ColumnInfo=' + GetVisibleColumnXML(), 'ExeclWindow', '');
  //top.JsClientProxy.ShowQueryReport(g_UserName, g_FunctionID); 
}

function ExportExcelEx(sQueryID){  
  //if(!confirm("确定您将输出到Excel并打印?"))return;
  var ss=sQueryID;
  if(sQueryID!="00")
  {
  ss=window.prompt("如果您是接着原来的纸张打印，请输入需要空出的行数；否则请直接确定!取消将终止操作!","0");
  if(ss==null)
    return;
  }
  var i=parseFloat(ss);
  var sval=i.toString(); 
  if(sval=="NaN") 
  {  sval="0";   }
  var j=2-sval.length; 
  for(var a=0;a<j;a++) 
    sval="0"+sval;   
  if(sQueryID==null||sQueryID=="")
  {
    return ;
    window.open("QueryXLS.aspx?QueryFID=" + g_FunctionID + sval+ '&ColumnInfo=' + GetVisibleColumnXML(), 'ExeclWindow',      '')
  }
  else
  {  
    if(sQueryID.length!=2)
    { 
      window.alert("编号应设为2位编码"); return ;
    }
    window.open("QueryXLS.aspx?QueryFID=" + g_FunctionID + sQueryID + sval + '&ColumnInfo=' + GetVisibleColumnXML(),        'ExeclWindow', '')
  }
  //top.JsClientProxy.ShowQueryReport(g_UserName, g_FunctionID); 
}



function ExecDefaultFunction(){
  var xmlDoc = NewXmlDoc();
  if (!xmlDoc.loadXML(PostBackInfo.FuncButtonsXML)) return;
  oNodeList = xmlDoc.selectNodes('//Item[@IsDefault="true"]');
  for (var i=0; i<oNodeList.length; i++){
    var oNode = oNodeList[i];
    var sAttrValue = oNode.getAttribute("Enable");
	if (top.ReplaceStatusAttr(sAttrValue) != 'true') continue;
	
    var oButton = new Object();
	oButton.OperateFID = oNode.getAttribute("OperateFID");
	oButton.UrlName = oNode.getAttribute("UrlName");
	oButton.Text = oNode.getAttribute("Text");
	oButton.ParamInfo = oNode.getAttribute("ParamInfo");
	oButton.FuncRunType = oNode.getAttribute("FuncRunType");
    top.ToolButton_Click(oButton);
	break;
  }
}



function SaveToBackHistory(bSaveSelectedRowIndex){
  if (top.oTop != null) return;  //在弹出窗口内
//  InParams.SetValue('SelectedRowIndex', SelectedRowIndex);
  var xmlDoc = NewXmlDoc();
  var oRootNode = xmlDoc.createElement('Params');
  oRootNode.setAttribute('FunctionID', g_FunctionID);
  oRootNode.setAttribute('MainTitle', PostBackInfo.MainTitle);
  oRootNode.setAttribute('InParamXML', InParams.ToString());
  oRootNode.setAttribute('PageSize', document.all.PageSize.value);
  oRootNode.setAttribute('CurrentPage', document.all.CurrentPage.value);
  oRootNode.setAttribute('SortColumn', document.all.SortColumn.value);
  oRootNode.setAttribute('SortDirection', document.all.SortDirection.value);
  oRootNode.setAttribute('FilterString', document.all.FilterString.value);
  xmlDoc.appendChild(oRootNode);
  var sURL = "query.aspx";
  var vURL = top.GetPostFormParams(sURL, xmlDoc);
  if (vURL != top.aHistoryBack[top.aHistoryBack.length-1]){  //防止同一地址被连续多次加入到后退历史
    top.aHistoryBack.push(vURL);
	if (bSaveSelectedRowIndex !== true)  //不是在window.onbeforeunload中调用时才清除前进历史
	  top.aHistoryForward.splice(0, top.aHistoryForward.length);  //清除可前进的历史记录
  }
  top.SetBackForwardButtonState();
}

function oButton_MouseOver(oHTMLTD){
    if(oHTMLTD.disabled)return;
    var oHTMLIMG, oHTMLTextDIV;

    oHTMLIMG = oHTMLTD.firstChild;
    oHTMLTextDIV = oHTMLTD.lastChild;

    oHTMLTD.className = "jkcTB_Button_Hover";
    oHTMLIMG.className = "jkcTB_Ico_Hover";
    oHTMLTextDIV.className = "jkcTB_Text_Hover";
}

function oButton_MouseDown(oHTMLTD){
    if(oHTMLTD.disabled)return;
    var oHTMLIMG, oHTMLTextDIV;

    oHTMLIMG = oHTMLTD.firstChild;
    oHTMLTextDIV = oHTMLTD.lastChild;

    oHTMLTD.className = "jkcTB_Button_Down";
    oHTMLIMG.className = "jkcTB_Ico_Down";
    oHTMLTextDIV.className = "jkcTB_Text_Down";
}

function oButton_MouseOut(oHTMLTD){
    if(oHTMLTD.disabled)return;
    var oHTMLIMG, oHTMLTextDIV;

    oHTMLIMG = oHTMLTD.firstChild;
    oHTMLTextDIV = oHTMLTD.lastChild;

    oHTMLTD.className = "jkcTB_Button_Normal";
    oHTMLIMG.className = "jkcTB_Ico_Normal";
    oHTMLTextDIV.className = "jkcTB_Text_Normal";
}