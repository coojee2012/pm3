USE [dbCenter]
GO

/****** Object:  StoredProcedure [dbo].[po_PrjFieldToText]    Script Date: 2015/4/17 14:59:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[po_PrjFieldToText]
@sInput AS varchar(50),
@sType as varchar(50),
@sOutput AS varchar(500) OUTPUT 
AS
BEGIN
  declare @sql nvarchar(max)
  DECLARE @sOutput1 varchar(500)
  if (ltrim(isnull(@sType,'')) != null) and (ltrim(isnull(@sInput,'')) != null)
  begin
	  set @sql = N' select @sOutput = SUBSTRING(names, 1, len(names)-1) 
	   from (select FText+'','' from TC_PrjAddition_FieldType 
	   where FNumber='''+@sType +''' 
	   and  FValue in ('+@sInput+')  
	   FOR XML PATH('''') ) C (names) '
	 -- exec(N' DECLARE @sOutput varchar(500) ' + @sql + N' select @sOutput')--把整个语句用字符串加起来执行
	  exec sp_executesql @sql,N'@sOutput varchar(500) output',@sOutput output 
	 -- set @sOutput = @sOutput1
  end 
END

GO


