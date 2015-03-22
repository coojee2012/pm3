/*创建了一个新的函数，替换原来的 isSuperDept*/
USE [dbCenter]
GO

/****** Object:  UserDefinedFunction [dbo].[isSuperDept_new]    Script Date: 2015/3/17 17:46:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create  FUNCTION [dbo].[isSuperDept_new]
( @fNumber1 AS varchar(50) ,
  @fNumber2 AS varchar(50)
)
RETURNS int
AS
BEGIN
declare @result int,@f2 int
set @result = 0
set @f2 = convert(int,@fNumber2)
  select @result = 1 
    from dbo.CF_Sys_ManageDept 
   where FNumber=@f2 and (@fNumber2 like rtrim(@fNumber1) +'%')
  RETURN @result
END

GO
