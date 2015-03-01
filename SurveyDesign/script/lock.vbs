Sub Read_OnClick
	on error resume next
	dim ErrCode
	dim ctx	
	dim buf
	Dim i,xx,xxlen     	    
	set ctx = CreateObject("epsmodu.epsCtx")
	if(err <> 0) then
		MsgBox "您的身份认证锁安装的不正确，不能进行验证"&vbnewline&"错误信息提示："&err.description
		Exit Sub
	end if
	ErrCode = epass1.OpenDevice (1)
	if ErrCode <> 0 then
		MsgBox "您没有插好身份认证锁或者它正被别的程序占用！"&vbnewline&"错误信息提示："&ErrCode
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
		MsgBox "您的身份认证锁安装的不正确，不能进行验证"&vbnewline&"错误信息提示："&err.description
		Exit Sub
	end if
	ErrCode = epass1.OpenDevice (1)
	if ErrCode <> 0 then
		MsgBox "您没有插好身份认证锁或者它正被别的程序占用！"&vbnewline&"错误信息提示："&ErrCode
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
		MsgBox "您的身份认证锁安装的不正确，不能进行验证"&vbnewline&"错误信息提示："&err.description
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
    	'打开设备
     ret = ET99.OpenToken("FFFFFFFF", 1)

     ret= ET99.GetSN() 
 
     ET99.CloseToken()
			
	end if
	MsgBox ret
	FORM1.id.value = ret 
end sub