Sub Read_OnClick
	on error resume next
	dim ErrCode
	dim ctx	
	dim buf
	Dim i,xx,xxlen     	    
	set ctx = CreateObject("epsmodu.epsCtx")
	if(err <> 0) then
		MsgBox "���������֤����װ�Ĳ���ȷ�����ܽ�����֤"&vbnewline&"������Ϣ��ʾ��"&err.description
		Exit Sub
	end if
	ErrCode = epass1.OpenDevice (1)
	if ErrCode <> 0 then
		MsgBox "��û�в�������֤��������������ĳ���ռ�ã�"&vbnewline&"������Ϣ��ʾ��"&ErrCode
		exit sub
	end if
	FORM1.id.value = Hex(ctx.SerialNumber(1)) + Hex(ctx.SerialNumber(0))
	epass1.CloseDevice 
	set ctx=nothing
end sub

Sub btnRead_OnClick
	on error resume next
	dim ErrCode
	dim ctx	
	dim buf
	Dim i,xx,xxlen     	    
	set ctx = CreateObject("epsmodu.epsCtx")
	if(err <> 0) then
		MsgBox "���������֤����װ�Ĳ���ȷ�����ܽ�����֤"&vbnewline&"������Ϣ��ʾ��"&err.description
		Exit Sub
	end if
	ErrCode = epass1.OpenDevice (1)
	if ErrCode <> 0 then
		MsgBox "��û�в�������֤��������������ĳ���ռ�ã�"&vbnewline&"������Ϣ��ʾ��"&ErrCode
		exit sub
	end if
	FORM1.id.value = Hex(ctx.SerialNumber(1)) + Hex(ctx.SerialNumber(0))
	epass1.CloseDevice 
	set ctx=nothing
end sub

Sub btnLogin_OnClick
	on error resume next
	dim ErrCode
	dim ctx	
	dim buf
	Dim i,xx,xxlen         

	set ctx = CreateObject("epsmodu.epsCtx")
	if(err <> 0) then
		MsgBox "���������֤����װ�Ĳ���ȷ�����ܽ�����֤"&vbnewline&"������Ϣ��ʾ��"&err.description
		Exit Sub
	end if
	ErrCode = epass1.OpenDevice (1)

	dim ret
	
	if ErrCode=0 then
	   ret=Hex(ctx.SerialNumber(1)) + Hex(ctx.SerialNumber(0))
	end if
	
	epass1.CloseDevice 
	set ctx=nothing
	

	if ( ret=Empty) then
    	'���豸
     ret = ET99.OpenToken("FFFFFFFF", 1)

     ret= ET99.GetSN() 
 
     ET99.CloseToken()
			
	end if
	MsgBox ret
	FORM1.id.value = ret 
end sub