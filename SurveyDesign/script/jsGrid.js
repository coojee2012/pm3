//<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=gb2312">   //���ȥ����һ�䣬��DreamWeaver�д򿪻�����

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
var bFirstTimeSelect = true;  //�Ƿ��һ��ѡ��(����ȷ����ѡ����ʱ�Ƿ�Ҫ���º�����ʷ�е�ѡ�����кţ���һ��ѡ��ʱ������)
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
	if (event.keyCode != 38 & event.keyCode != 40) return;  //�����������¼�
	var iIndex = -1;
	var oSelectRow;
	if (grid.AllowMultiSelect){
      if (oSelectedRows.length > 1) return;   //ѡ���˶���
	  if (oSelectedRows.length == 1)
	    iIndex = oSelectedRows[0].rowIndex-1;
	}
	else {
	  if (oSelected != null)
	    iIndex = oSelected.rowIndex-1;
	}
	if (event.keyCode == 38){  //����
	  if (iIndex == -1)
	    iIndex = grid.CurrentPageRecordCount-1;
	  else if (iIndex > 0)
	    iIndex = iIndex - 1;
	  else
	    return;
	}
	else if (event.keyCode == 40){  //����
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
    
//    if (!grid.AllowSelect) return;  //������ѡ����
    if (!ToBoolean(oHTMLTR.Selectable, true)) return;  //���в�����ѡ��

    if (!grid.AllowMultiSelect){  //��ѡģʽ 
      if (oHTMLTR.Selected){  //������ѡ��
	    if (grid.AllowUnSelect)   //����ѡ��
          UnSelectRow(oHTMLTR);  //ȡ��ѡ�д���
	  }
      else
        SelectRow(oHTMLTR); 
    }
    else {  //��ѡģʽ
      if (event.ctrlKey){  //����Ctrl�� 
        if (oHTMLTR.Selected){
          if (oSelectedRows.length != 1 || grid.AllowUnSelect)
            UnSelectRow(oHTMLTR);
        }
        else
          SelectRow(oHTMLTR); 
      }
      else { //δ����Ctrl�� 
        if (oHTMLTR.Selected && oSelectedRows.length == 1){  //ֻѡ���˴���
          if (grid.AllowUnSelect)  //����ѡ��
            UnSelectRow(oHTMLTR);
        }
        else {
          UnSelectAll();  //��ȡ��ѡ��������ѡ�е���
          SelectRow(oHTMLTR);   //��ѡ�д���
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
	if (top.oTop != null && top.oTop.ifrMain != null && top.oTop != top){  //��ModalDialog��
	  ExecDefaultFunction();  //ִ�в�ѯ����ع����е�Ĭ�Ϲ���
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



  //ѡ��ָ����
  //bFireEvent �Ƿ񴥷�Selected�¼���Ĭ��Ϊtrue
  //bFireSelectChangedEvent �Ƿ񴥷�SelectChanged�¼���Ĭ��Ϊtrue
  function SelectRow(tr){
    if (grid.AllowMultiSelect){  //�ɶ�ѡ
      oSelectedRows[oSelectedRows.length] = tr;
      if (grid.ShowSelectorCol) {   //��ʾѡ������
        tr.firstChild.firstChild.checked = true;
        if (grid.AllowAllSelect){  //����һ����ȫ��ѡ��
		  var checkbox = document.all['JsWebGrid1_HeaderCheckbox'];
          checkbox.checked = true;
          if (oSelectedRows.length != tabBody.rows.length-1){  //���ûȫ��ѡ�У���ʹ�������ϵ�checkboxѡ�в����
            checkbox.style.filter = "alpha(opacity=50)";
          }
          else {  //ʹ�������ϵ�checkboxѡ�в������
            checkbox.style.removeAttribute("filter");
          }
        }
      }
    }
    else {   //ֻ�ܵ�ѡ
      if (oSelected != null) UnSelectRow(oSelected);  //ѡȡ��ѡ����ǰ��ѡ�е���
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


  //ȡ��ѡ��ָ����
  function UnSelectRow(tr, bFireEvent, bFireSelectChangedEvent){
    if (grid.AllowMultiSelect){  //�ɶ�ѡ
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
          if (oSelectedRows.length != 0){   //�������һ����ûѡ�У���ʹ�������ϵ�checkboxѡ�в����
            checkbox.checked = true;
            checkbox.style.filter = "alpha(opacity=50)";
          }
          else {  //ʹ�������ϵ�checkbox��ѡ��
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

//���ڲ�ѯ�����У�������������ֵ
//oDOMRow - �б��ж���
//����ֵ������������������XML 
function FillOutputParamValue(){ 
  var i, iCount, xmlDoc, oNode, oNodeList, sFieldName;
  xmlDoc = OutParams.XmlDoc;
  oNodeList = xmlDoc.documentElement.selectNodes('./����[@����]');
  iCount = oNodeList.length;
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
	sFieldName = oNode.getAttribute('����');  //��������������ָ������������oDOMRow���ϲ�Ӧ�ò����ڴ���
    if (!grid.AllowMultiSelect){
      oNode.setAttribute('ֵ', GetCellValue(oSelected, sFieldName));
    }
    else {  //oDOMRow���м���
      if (oSelectedRows.length == 0){
        oNode.setAttribute('ֵ', '');
        continue;
      }
	  else if (oSelectedRows.length == 1){
		oNode.setAttribute('ֵ', GetCellValue(oSelectedRows[0], sFieldName));
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
      oNode.setAttribute('ֵ', newDoc.xml.replace('\r\n', ''));
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

//����ǰ����������ʷ�б��е�ѡ���к�
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
  //�����ѡ���У��ṩ��ѡ�ĸ�ѡ�򣩣���û�С�ColName�����ԣ�������Ҫ�ж�
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

//������������Ķ���
function BackupOutputParamDefine(){
  var i, iCount, xmlDoc, oNode, oNodeList, sValue;
  xmlDoc = OutParams.XmlDoc;
  oNodeList = xmlDoc.documentElement.selectNodes('./����[not(@��ʵ����)]');
  iCount = oNodeList.length;
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
	if (oNode.getAttribute('����') == 'SelectedCount') continue;
	if (oNode.getAttribute('����') != null) continue;
	try{
	  oNode.setAttribute('����', oNode.getAttribute('ֵ'));
	}
	catch(err){
		
	}
  }
}

//����Ϊʱ����������
function ClearOutputParamDefine(){
  var i, iCount, xmlDoc, oNode, oNodeList, sValue;
  xmlDoc = OutParams.XmlDoc;
  oNodeList = xmlDoc.documentElement.selectNodes('./����[not(@��ʵ����)]');
  iCount = oNodeList.length;
  for (i=0; i<iCount; i++){
    oNode = oNodeList[i];
	if (oNode.getAttribute('����') == 'SelectedCount'){
	    oNode.parentNode.removeChild(oNode);
	}
	try{
	  oNode.setAttribute('ֵ', oNode.getAttribute('����'));
	}
	catch(err){
		
	}
  }
  OutParams.XmlDoc = xmlDoc;
}
  
  //tr��SelectedRows�����е�����
  function IndexOfSelected(tr){
    var iCount = oSelectedRows.length;
    for (var i=0; i<iCount; i++){
      if (oSelectedRows[i] == tr) return i;
    }
    return -1;
  }

  
  
  //ѡ��������
  function SelectAll(){
    for (var i=1; i<tabBody.rows.length; i++){
      if (!tabBody.rows[i].Selected)  //�����ǰû��ѡ�в����ǿ��Ա�ѡ��
        SelectRow(tabBody.rows[i]);
    }
    //�����������
    FillOutputParamValue();
  }

  
  //ȡ��ѡ��������ѡ�е���
  function UnSelectAll(){
    var iCount = oSelectedRows.length;
    for (var i=0; i<iCount; i++){
      UnSelectRow(oSelectedRows[0]);
    }
    //�����������
    FillOutputParamValue();
  }


  
  function oCheckbox_Click(element){
	if (element == null) element = this;
    event.cancelBubble = true;
    if (!grid.AllowSelect){   //��������ѡ��
      element.checked = !element.checked;  //����δ���֮ǰ��״̬
      return;
    }
    var tr = element.parentElement.parentElement;

    if (tr.Selected){  //������ѡ��
      if (oSelectedRows.length != 1 || grid.AllowUnSelect)  //�����ǰ�п��Ա�ѡ�� ���� ����ѡ��
        UnSelectRow(tr);
      else
        element.checked = !element.checked;
    }
    else {
      if (tr.Selectable !== false && grid.AllowSelect)  //�����ǰ�п��Ա�ѡ�� ���� ����ѡ��
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
  
    if (oSelectedRows.length == tabBody.rows.length-1){  //��ѡ����ȫ��ѡ��
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
	if (grid == null) return;	//grid����û����
    if (!grid.AllowResizeCol) return;
    if (event.button != 0){
	  if (event.button != 1) return;
	  if (oHTMLTD.className != "jkcListView_TitleTD_Down") return;
	  if (oHTMLTD.ColName == 'LastCell') return;   //�����һ��

      var iX = GetMousePointX();   //������ĵ��е�X����
      if (iMousePointX - iX > 3 || iMousePointX - iX < -3){     //�����ƶ�3����������
        StartDrag(oHTMLTD);
      }
	  return;     //�����������ڱ��������ƶ�
	}

    if (oHTMLTD.ColName == "LastCell"){  //����ڱ��������һ��Ԫ����
      if (event.offsetX < 3 && grid.VisibleColumns[grid.VisibleColumns.Count-1] != null && grid.VisibleColumns[grid.VisibleColumns.Count-1].AllowResize)
        oHTMLTD.style.cursor = "col-resize";
      else
        oHTMLTD.style.removeAttribute("cursor");
      return; 
    }

    if (event.offsetX < 3 || event.offsetX > oHTMLTD.clientWidth-4){
      var column = grid.Columns[GetColName(oHTMLTD)];  //��ǰ�ж���
      var oPrevVisibleCell = grid.VisibleColumns[column.VisibleIndex - 1];
      if (oPrevVisibleCell != null && oPrevVisibleCell.AllowResize && event.offsetX < 3   //ǰһ�ɼ��в�Ϊ���ҿ��Ե����п� ���� �����ǰ��������λ��
          || column.Name != "LastCell" && column.AllowResize && event.offsetX > oHTMLTD.clientWidth-4)  //��ǰ�в������һ���ҿ��Ե����п� ���� ����ں󼸸�����λ��
      oHTMLTD.style.cursor = "col-resize";
	}
    else {
      oHTMLTD.style.removeAttribute("cursor");
	}
}

function oHTMLTHeadTD_MouseDown(oHTMLTD){
	if (oHTMLTD == null) oHTMLTD = this;
    if (event.button != 1) return;    //���µĲ������
    if (oHTMLTD.style.cursor == "col-resize"){    //����ڿɵ����п��λ��
	  StartResize(oHTMLTD);
	  return;
	}
//return;	
	if (oHTMLTD.ColName == 'LastCell') return;   //�����һ��
//    var oDOMCell = grid.Columns[oHTMLTD.ColName];
//    if (!AllowSwapCol && (!AllowSort || !oDOMCell.AllowSort)) return;  //���ܵ�����λ��Ҳ��������
    oHTMLTD.className = "jkcListView_TitleTD_Down";
    iMousePointX = GetMousePointX();
	iOffsetX = event.clientX - posLib.getClientLeft(oHTMLTD);

    oHTMLTD.setCapture();
}

function StartResize(oHTMLTD){
    iMousePointX = GetMousePointX();  //��갴��ʱ��X����
    
    iSplitMinLeft = iMousePointX - event.offsetX + grid.MinColWidth;  //���� - �����TD��˵ľ��� + ��С�п�

    if (oHTMLTD.ColName == "LastCell"){
      grid.ResizeColumn = grid.VisibleColumns[grid.VisibleColumns.Count-1];
      iSplitMinLeft -= grid.ResizeColumn.Width;
    }
    else {
      grid.ResizeColumn = GetColumn(oHTMLTD);  //��ǰ�ж���
      if (event.offsetX < 5){
        grid.ResizeColumn = grid.VisibleColumns[grid.ResizeColumn.VisibleIndex - 1];
        iSplitMinLeft -= grid.ResizeColumn.Width;
      }
    }

    LaySplit.style.left = iMousePointX;   //�ָ���������������갴�µ�X����
    LaySplit.style.top = 0;   
    LaySplit.style.height = document.all[grid.ID].clientHeight;    //�߶����б����ĸ߶�
    LaySplit.style.display = '';   //��ʾ
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
    if (oHTMLTD.contains(event.toElement)) return; ��//��ͬһ��TD���ƶ�

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
	oHTMLTD.onmouseover = oHTMLTD_MouseOver;   //�������е�TD����ͬһ������
    oHTMLTD.onmouseout = oHTMLTHeadTD_MouseOut;
    oHTMLTD.onclick = oHTMLTHeadTD_Click;
    oHTMLTD.ondblclick = oHTMLTHeadTD_DblClick;

*/


  //���ָ����X����
  function GetMousePointX(){
    var iX = parseInt(event.clientX) + parseInt(document.body.clientLeft) ;
    return iX;
  }



  //�ƶ��ָ���
  function LaySplit_MouseMove(){
    var iLeft = GetMousePointX();
    
    if (iLeft <= iSplitMinLeft){   //С����С���
      LaySplit.style.left = iSplitMinLeft;
    }
    else {
      LaySplit.style.left = iLeft;
    }
  }
  

  //ȡ�������п�
  function LaySplit_MouseDown(){
    event.cancelBubble = true;
    if (event.button == 3){   //�����Ҽ�(���϶�ʱ��������Ѿ�����״̬���ٰ����Ҽ���event.button�͵���3��)
      LaySplit.releaseCapture()
      LaySplit.style.display = "none"   
    }
  }


  //��ɵ�������
  function LaySplit_MouseUp(){
    event.cancelBubble = true;
    LaySplit.releaseCapture();
    LaySplit.style.display = "none";
    //�µĿ�� = ��ǰ�Ŀ�� + (������Ⱥ�ķָ���X���� - �������ǰ�ķָ���X����)
    iNewWidth = grid.ResizeColumn.Width + (parseInt(LaySplit.style.left) - iMousePointX);

    SetColWidth(GetHTMLCellIndex(grid.ResizeColumn), iNewWidth);
	SaveListState();
  }
  

  
  //�Զ�����ָ���кŵ��еĿ��
  function AutoResizeWidth(iColIndex){
    var i, iWidth, iMaxWidth, oHTMLTD, oResizeCell, sColName;
	var iCellIndex = GetCellIndexByColumn(grid.Columns[iColIndex]);
	
	//�Ȱ���һ�еĿ������Ϊ��Сֵ
	SetColWidth(iCellIndex, grid.MinColWidth);

    //���ҳ������
    var iMaxWidth = tabHead.rows[1].cells[iCellIndex].scrollWidth;
    for (i=1; i<tabBody.rows.length; i++){
      iWidth = tabBody.rows[i].cells[iCellIndex].scrollWidth;
      if (iWidth > iMaxWidth) iMaxWidth = iWidth;
    }

	iMaxWidth += 4; //���ϱ߿�Ŀ�� �� ������TD��LeftBorder�ľ���
	//�������Ϊ�����
	SetColWidth(iCellIndex, iMaxWidth);
  }
  
  
  //����ָ���еĿ��
  //iColIndex: �к�
  //iWidth: ���ֵ
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
    iLeft  = iMousePointX - iOffsetX;          //������������ͻ�����˵ľ���
    //���汻�����֮ǰ���е�����
    for (i=iVisibleCellIndex-1; i>=0; i--){    
      arrColPos[i] = new Array(3);
      iWidth = grid.VisibleColumns[i].Width;   //��i�еĿ��
      if (i == 0){   //��һ��  
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

    iLeft  = iMousePointX - iOffsetX + column.Width;    //�������е���һ����ͻ�����˵ľ���

    //���汻�����֮����е�����
    for (i=iVisibleCellIndex+1; i<grid.VisibleColumns.Count; i++){    
      arrColPos[i] = new Array(3)
      iWidth = grid.VisibleColumns[i].Width;   //��i�еĿ��
      if (i == grid.VisibleColumns.Count - 1){   //���һ��  
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
	
    //�޶�Drag����СLeft�����Left
    switch (iVisibleCellIndex){
      case 0 : {                 //��һ��
        iDragMinLeft = iMousePointX;1
//		alert(arrColPos[grid.VisibleColumns.Count - 1]);
        iDragMaxLeft = arrColPos[grid.VisibleColumns.Count - 1][1];
        break
      }
      case grid.VisibleColumns.Count - 1 : {   //���һ��
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
    iX = GetMousePointX();   //������ĵ��е�X����
    if (iX < iDragMinLeft) iX = iDragMinLeft;
    if (iX > iDragMaxLeft) iX = iDragMaxLeft;
    //ѭ�������������Ƿ��ڿɲ����е����귶Χ�ڣ�����ǣ���ʾ����ͼ�꣬�������ز���ͼ��
    for (i in arrColPos){
      if (iX >= arrColPos[i][0] && iX <= arrColPos[i][1]){
        LayInsertPos.style.display = "";  //��ʾ
        LayInsertPos.style.left = Math.round(arrColPos[i][2] - LayInsertPos.offsetWidth / 2) - 1;
//		alert(LayInsertPos.outerHTML);
        iDescCellIndex = parseInt(i);    //�����Ŀ���к�
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
    if (LayInsertPos.style.display == "none"){ //û�г����в����־
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
    if (iDescRealCellIndex > iSrcRealCellIndex){   //���Ŀ������Դ��֮��
      sPos = "afterEnd";   //���뵽Ŀ����֮��
    }
    else {
      sPos = "beforeBegin";  //���뵽Ŀ����֮ǰ
    }
    //���������еĵ�Ԫ��
    var tdEmpty = document.createElement("TD");
    var tdSrc = tabHead.rows[1].cells[iSrcRealCellIndex];
    var tdDesc = tabHead.rows[1].cells[iDescRealCellIndex].insertAdjacentElement(sPos, tdEmpty);
//	var tdDesc = GetHTMLHeadTD(grid.VisibleColumns[iDescCellIndex]).insertAdjacentElement(sPos, tdEmpty);
    tabHead.rows[1].replaceChild(tdSrc, tdDesc);

    tdEmpty = document.createElement("TD");
    tdSrc = tabHead.rows[0].cells[iSrcRealCellIndex];
    tdDesc = tabHead.rows[0].cells[iDescRealCellIndex].insertAdjacentElement(sPos, tdEmpty);
    tabHead.rows[0].replaceChild(tdSrc, tdDesc);

    for (var i=0; i<tabBody.rows.length; i++){   //ѭ��������
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
    if (iDescCellIndex > iSrcCellIndex){   //���Ŀ������Դ��֮��
      sPos = "afterEnd";   //���뵽Ŀ����֮��
    }
    else {
      sPos = "beforeBegin";  //���뵽Ŀ����֮ǰ
    }
	
	//prompt('', oDocument.documentElement.outerHTML);
    //���������еĵ�Ԫ��
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

    for (var i=0; i<prop_iRows; i++){   //ѭ��������
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

  

  //ͨ������ģ���ж�����кŻ�ȡHTMLTD��cellIndex
  function GetHTMLCellIndex(column){
    if (grid.ShowSelectorCol)
	  return column.Index + 1;
	else
	  return column.Index;
  }


//���ݹ��ܱ�ű����б�״̬�����Ի����õ�XML������
function SaveListState(){
  var i, iCount, xmlDoc, oRootNode, oColumnNode, oRowNode, oParaListNode, oNode, oColumnsNode, oRowsNode;
  xmlDoc = NewXmlDoc();
  oRootNode = xmlDoc.createElement("ListView");
  xmlDoc.appendChild(oRootNode);
  oRootNode.setAttribute('PageSize', grid.PageSize);
  oRootNode.setAttribute('FunctionID', g_FunctionID); 
  
  oColumnsNode = xmlDoc.createElement("Columns");
  oRootNode.appendChild(oColumnsNode);
  //����Columns
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
    sHTML += '<br>���빴ѡҪ��ʾ���У�';
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
    sHTML += '</table></div><br>������';
//    sHTML += '<button id="btnDefault" class="uButton">Ĭ ��</button>��';
    sHTML += '<button id="btnOK" style="width:48; height:24; font:9pt">ȷ ��</button>����';
    sHTML += '<button id="btnCancel" style="width:48; height:24; font:9pt">ȡ ��</button>';
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
  
  
  
  //��������ʾ״̬
  function SetColVisible(column, iIndex, bVisible){
    var iCellIndex = GetCellIndexByColumn(column);
    if (bVisible){  //����Ҫ��ʾ
	  if (!column.Visible){  //����û����ʾ
		tabHead.rows[0].cells[iCellIndex].style.display = '';
		tabHead.rows[1].cells[iCellIndex].style.display = '';

		for (var i=0; i<tabBody.rows.length; i++){
		  tabBody.rows[i].cells[iCellIndex].style.display = '';
		}
	  }
	  grid.VisibleColumns[iIndex] = column;
	  column.VisibleIndex = iIndex;
	}
	else {  //���в�Ҫ��ʾ
	  if (column.Visible) {  //�����Ѿ���ʾ
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
	  if (!(column.FilterString == null && sReturn == '')){ //�����ǰ��null������ֵ��''������û�иı��������
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
      document.all.CurrentPage.value = 1;  //�����˹���������ʼ��ת���һҳ
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
  //if(!confirm("ȷ�����������Excel����ӡ?"))return;
  var ss=sQueryID;
  if(sQueryID!="00")
  {
  ss=window.prompt("������ǽ���ԭ����ֽ�Ŵ�ӡ����������Ҫ�ճ���������������ֱ��ȷ��!ȡ������ֹ����!","0");
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
      window.alert("���Ӧ��Ϊ2λ����"); return ;
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
  if (top.oTop != null) return;  //�ڵ���������
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
  if (vURL != top.aHistoryBack[top.aHistoryBack.length-1]){  //��ֹͬһ��ַ��������μ��뵽������ʷ
    top.aHistoryBack.push(vURL);
	if (bSaveSelectedRowIndex !== true)  //������window.onbeforeunload�е���ʱ�����ǰ����ʷ
	  top.aHistoryForward.splice(0, top.aHistoryForward.length);  //�����ǰ������ʷ��¼
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