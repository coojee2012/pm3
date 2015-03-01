// JScript 文件

 function checkIdCard1(obj,card)
　　{
　　 
　　var card1=trim(card);　
　 　　 
    if(card1=="")
    {
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
         obj.focus();
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
           obj.focus();
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
              obj.focus();
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
         obj.focus();
         return false;
    }
    else
    { 
           
        var cardYear=parseInt(card1.substring(6,10)); 
        if(cardYear<1900||cardYear>new Date().getFullYear())
        {
         alert("请您输入正确的出生年份");
         obj.focus();
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
           obj.focus();
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
              obj.focus();
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
    obj.focus();
    return false;
}

　　function trim(string)
    {
        return string.replace(/(^\s*)|(\s*$)/g, "");
    }