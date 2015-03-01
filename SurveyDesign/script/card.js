// JScript 文件

function CheckCard(bb)
{
var aCity={11:"北京",12:"天津",13:"河北",14:"山西",15:"内蒙古",21:"辽宁",22:"吉林",23:"黑龙江 ",31:"上海",32:"江苏",33:"浙江",34:"安徽",35:"福建",36:"江西",37:"山东",41:"河南",42:"湖北",43:"湖南",44:"广东",45:"广西",46:"海南",50:"重庆",51:"四川",52:"贵州",53:"云南",54:"西藏 ",61:"陕西",62:"甘肃",63:"青海",64:"宁夏",65:"新疆",71:"台湾",81:"香港",82:"澳门",91:"国外 "}

        var bb=document.getElementById("t_FIdCard");
        if(bb)
        {   var t_FIDCardTypeId=document.getElementById("t_FIDCardTypeId");
            if(t_FIDCardTypeId)
            {
                if(t_FIDCardTypeId.options[t_FIDCardTypeId.selectedIndex].innerText=="身份证")
                {
                     var sId = bb.value;
                     var iSum=0
                     var info=""
                     if(!/^\d{17}[\d|X|x]|\d{15}$/i.test(sId)){
                      alert("请您输入正确的15位或18位的身份证号码");
                      return false;
                     }
                     sId=sId.replace(/x$/i,"a");
                     if(aCity[parseInt(sId.substr(0,2))]==null){
                       alert("请您输入正确的15位或18位的身份证号码");
                      return false;
                     }
                     
//                     sBirthday=sId.substr(6,4)+"-"+Number(sId.substr(10,2))+"-"+Number(sId.substr(12,2));
//                     var d=new Date(sBirthday.replace(/-/g,"/"))
//                     if(sBirthday!=(d.getFullYear()+"-"+ (d.getMonth()+1) + "-" + d.getDate())){
//                      alert("请您输入正确的15位或18位的身份证号码c");
//                      return false;
//                     }
//                     for(var i = 17;i>=0;i --) iSum += (Math.pow(2,i) % 11) * parseInt(sId.charAt(17 - i),11)
//                     if(iSum%11!=1){
//                      alert("请您输入正确的15位或18位的身份证号码d");
//                      return false;
//                     }  
  var card1 = bb.value; if(card1=="")  {
        alert("请您输入正确的15位或18位的身份证号码");
         return false;
    }
     
    else if(card1.length==15)
    {
      if ((!card1.substring(0,15).match(/^\d{15}$/))) 
      {
        return false;
      }
      else
      {             
        var cardYear=parseInt(card1.substring(6,8)); 
        cardYear=cardYear+1900;
        if(cardYear<1900||cardYear>new Date().getFullYear())
        {
         alert("请您输入正确的出生年份");
        
         return false;
        }
        else
        {
         var cardMoth=parseInt(card1.substring(8,10));
          if(cardMoth==0)
         {
          cardMoth=parseInt(card1.substring(9,10)); 
         } 
          if( cardMoth==0 ||cardMoth>12 )
          {
           alert("请您输入正确的出生月份");
         
           return false;
          }
          else
          {
           
            var cardDay=parseInt(card1.substring(10,12)); 
            if(cardDay==0)
            {
             cardDay=parseInt(card1.substring(11,12));  
            }
            if(cardDay==0||cardDay>31)
            {
              alert("请您输入正确的出生日期");
            
              return false;
            }
            else
            {
            return true;
            }
            
          }
        }
        
      }
     
    }
    
   else if(card1.length==18)
    {
    if((!card1.substring(0,17).match(/^\d{17}$/)))  
    { 
     alert("请您输入正确的15位或18位的身份证号码");
        
         return false;
    }
    else
    { 
           
        var cardYear=parseInt(card1.substring(6,10)); 
        if(cardYear<1900||cardYear>new Date().getFullYear())
        {
         alert("请您输入正确的出生年份");
        
         return false;
        }
        else
        {
      
         var cardMoth=parseInt(card1.substring(10,12)); 
         if(cardMoth==0)
         {
          cardMoth=parseInt(card1.substring(11,12)); 
         }
          if( cardMoth==0 ||cardMoth>12 )
          {
           alert("请您输入正确的出生月份");
          
           return false;
          }
          else
          {
           
            var cardDay=parseInt(card1.substring(12,14)); 
            if(cardDay==0)
            {
             cardDay=parseInt(card1.substring(13,14));  
            }
            if(cardDay==0||cardDay>31)
            {
              alert("请您输入正确的出生日期");
             
             return false;
            }
            else
            {
              var flag=card1.substring(17,18);
               if(!isNaN (flag)||flag=="x"||flag=="X"||flag=="Y"||flag=="y")
               {
                return true;
               }
               else 
               {
               return false;
               }
            }
            
          }
         }
      }
    } 
   alert("请您输入正确的15位或18位的身份证号码");
    
    return false;     
                }
            }
            else
            {
	            var num=0;
	            var aa=bb.value.trim();
	            if(aa=="") return true;
	            for(var i=0; i<aa.length;i++)
	            {
		            if(aa.charCodeAt(i)>255)
		            {
			            num=num+2;
		            }
		            else
		            {
			            num=num+1;
		            }
	            }
	            if(num!=15 && num!=18)
	            {
		            alert("身份证证号码不正确，应为15位或18位！")
		            bb.focus();
		            return false;
	            }
	            else
	            {
		            return true;
	            }
            }
           
        }
        return true;
}