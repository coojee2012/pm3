function setPrintBase(headerText,footerText,rootUrl) { 
 
    // -- advanced features  ��δ��ʹ�ù����д�ȷ�ϡ� 
 
        //factory.printing.SetMarginMeasure(2); // measure margins in inches 
 
        //factory.SetPageRange(false, 1, 3);// need pages from 1 to 3 
 
        //factory.printing.printer = "HP DeskJet 870C"; 
 
        //factory.printing.copies = 2; 
 
        //factory.printing.collate = true; 
 
        //factory.printing.paperSize = "A4"; 
 
        //factory.printing.paperSource = "Manual feed" 
        
   document.body.insertAdjacentHTML('beforeEnd',"<OBJECT id='factory' style='DISPLAY: none' codeBase='http://www.jgj.lnjst.gov.cn/sgxk/script/smsx.cab#Version=6,3,435,20'  classid='clsid:1663ed61-23eb-11d2-b92f-008048fdd814' viewastext></OBJECT> ");
 
    var header = (headerText==null||headerText=="")?'Ĭ��ҳü':headerText; 
 
    var footer = (footerText==null||footerText=="")?'Ĭ��ҳ��':footerText; 
 
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
&w  	��ҳ����
&u 	��ҳ��ַ (URL)
&d 	�����ڸ�ʽ���ɡ�������塱�еġ��������á�ָ����
&D 	�����ڸ�ʽ���ɡ�������塱�еġ��������á�ָ����
&t 	�ɡ�������塱�еġ��������á�ָ����ʱ���ʽ
&T 	24 Сʱʱ���ʽ
&p 	��ǰҳ��
&P 	��ҳ��     
&b 	�ı��Ҷ��루���Ҫ�Ҷ�������ַ��ڡ�&b��֮��
&b&b 	���־��У����Ҫ���е����ַ��ڡ�&b���͡�&b�� ֮�䣩
&& 	���� & �� (&)
*/