function setPrintBase(headerText,footerText,rootUrl) { 
 
    // -- advanced features  ，未曾使用过，有待确认。 
 
        //factory.printing.SetMarginMeasure(2); // measure margins in inches 
 
        //factory.SetPageRange(false, 1, 3);// need pages from 1 to 3 
 
        //factory.printing.printer = "HP DeskJet 870C"; 
 
        //factory.printing.copies = 2; 
 
        //factory.printing.collate = true; 
 
        //factory.printing.paperSize = "A4"; 
 
        //factory.printing.paperSource = "Manual feed" 
        
   document.body.insertAdjacentHTML('beforeEnd',"<OBJECT id='factory' style='DISPLAY: none' codeBase='http://www.jgj.lnjst.gov.cn/sgxk/script/smsx.cab#Version=6,3,435,20'  classid='clsid:1663ed61-23eb-11d2-b92f-008048fdd814' viewastext></OBJECT> ");
 
    var header = (headerText==null||headerText=="")?'默认页眉':headerText; 
 
    var footer = (footerText==null||footerText=="")?'默认页角':footerText; 
 
  factory.printing.header = "&b"+header+"&b" ; 
 
  factory.printing.footer = "&b"+footer; 
 
  factory.printing.portrait = true; 
   var hiPortrait=document.getElementById("hiPortrait");
  if(hiPortrait)
  {
    if(hiPortrait.value=="0")
    {
        factory.printing.portrait = false;     
    }
    else
    {
        factory.printing.portrait = true; 
    }
  }
  factory.printing.leftMargin =10.00; 
 
  factory.printing.topMargin =10.00; 
 
  factory.printing.rightMargin =10.00; 
 
  factory.printing.bottomMargin =10.00; 
 
}  
window.onload=function(){setPrintBase(' ','&b&p&b');}
/*
&w  	网页标题
&u 	网页地址 (URL)
&d 	短日期格式（由“控制面板”中的“区域设置”指定）
&D 	长日期格式（由“控制面板”中的“区域设置”指定）
&t 	由“控制面板”中的“区域设置”指定的时间格式
&T 	24 小时时间格式
&p 	当前页码
&P 	总页数     
&b 	文本右对齐（请把要右对齐的文字放在“&b”之后）
&b&b 	文字居中（请把要居中的文字放在“&b”和“&b” 之间）
&& 	单个 & 号 (&)
*/