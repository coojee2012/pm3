<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Quali.ascx.cs" Inherits="Common_Quali" %>
<asp:DropDownList ID="dp1" runat="server" CssClass="cTextBox1">
</asp:DropDownList>
<asp:DropDownList ID="dp2" runat="server" CssClass="cTextBox1">
</asp:DropDownList>
<asp:DropDownList ID="dp3" runat="server" CssClass="cTextBox1">
</asp:DropDownList>
<input id="hNumber" runat="server" type="hidden" /> 
<script>
 
var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP"); 
var FName = new Array();
var FNumber = new Array();
var FCount = new Array(); 
var vPostUrl = '<%= this.Request.ApplicationPath%>';
function XmlPost2(obj,conTrol)
{   
    conTrol.length=0;
    conTrol.options.add(new Option('请选择','请选择')); 
    var webFileUrl = vPostUrl+"/Common/AjaxPage1.aspx?fNumber=" + obj.value;
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

function <%= this.ClientID%>getFnumber()
{ 
  
    var cArea = document.getElementById("<%=dp3.ClientID%>");
    var cCity = document.getElementById("<%=dp2.ClientID%>"); 
    var cProvince = document.getElementById("<%=dp1.ClientID%>");
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

</script>