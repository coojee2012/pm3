// JScript 文件

String.prototype.trim=function()
{
    
	return this.replace(/(\s*$)|(^\s*)/g, "");
}

function checkIdCard(con,sMessage)
{
    if(sMessage==null||sMessage=="")
    {
        sMessage="请您输入正确的15位或18位的身份证号码";
    } 
    if(con.value.trim().length!=18&&con.value.trim().length!=15)
    {  
          alert(sMessage);
          con.focus();
          return false;
    }
    return true;
}