// JScript 文件

var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");

var FName = new Array();
var FNumber = new Array();
var FCount = new Array(); 
 
function XmlPost2(obj,conTrol)
{   
    conTrol.length=0;
    conTrol.options.add(new Option('请选择','请选择')); 
    var webFileUrl = "?fNumber=" + obj.value;
    var result = ""; 
    xmlHttp.open("POST", webFileUrl, false);
    xmlHttp.send("");
    result = xmlHttp.responseText;  
    if(result != "")
    {   
        FNumber = result.split("|");
        for(var i=0;i<FNumber.length;i++)
        {
            if(FNumber[i]==null||FNumber[i]=="")
            {
                continue;
            }
            FName = FNumber[i].split("^");
            var varItem = new Option(FName[0],FName[1]);
            conTrol.options.add(varItem); 
        } 
        
    } 
} 

function clearOptions(obj)
{
    obj.length=0;
    obj.options.add(new Option('请选择','请选择')); 
}

function getFnumber()
{

////    var cArea = document.getElementById('Govdept1_drop_Area');
//    var cCity = document.getElementById('Govdept1_drop_City');
//    var cProvince = document.getElementById('Govdept1_drop_Province');
//    var cNumber = document.getElementById('Govdept1_hNumber');

  
    var cArea = document.getElementById("<%=drop_Area.ClientID%>");
    var cCity = document.getElementById("<%=drop_City.ClientID%>"); 
    var cProvince = document.getElementById("<%=drop_Province.ClientID%>");
    var cNumber = document.getElementById("<%=hNumber.ClientID%>");
    
    
     
    
    if(cArea.value!=""&&cArea.value!="请选择")
    { 
        cNumber.value = cArea.value;
    } 
    else if(cCity.value!=""&&cCity.value!="请选择")
    {  
        cNumber.value = cCity.value;
    }
    else
    {
        
        if(cProvince.value!=""&&cProvince.value!="请选择")
        {
            cNumber.value = cProvince.value;
        }
        else
        {
            cNumber.value = "";
        }
    }  
}

function setValue(obj,val)
{
    obj.value = val;
}