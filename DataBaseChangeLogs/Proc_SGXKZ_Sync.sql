USE [dbCenter]
GO

/****** Object:  StoredProcedure [dbo].[Proc_SGXKZ_Sync]    Script Date: 2015/4/30 11:54:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create procedure [dbo].[Proc_SGXKZ_Sync] @appid varchar(36) as
begin
insert into xm_baseinfo.[dbo].[xm_xzyjs]
           ([xzid],[xmbh],[xmmc],[Xmdz],[jsdw],[nydmj],[njsgm],
            [xmjsyj],[zsbh],[hfrq],[fzjg],[createtime],qtsx)
		select a.fid, a.fprjitemid, a.projectname, a.locationaddress, a.jsdw, a.area ,a.scale, 
		       a.projectbasis, a.xzyjszsbh,a.createtime, a.hfjg,a.createtime,a.ydpzsx
		  from dbcenter.dbo.tc_sgxkz_location as a 
	   where a.fappid = @appid
	   
insert into xm_baseinfo.[dbo].[xm_jsydgh]
           ([jsid],[xmbh],[xmmc],[jsdw],[ydwz],[ydmj]
           ,[ydxz],[jsgm],[zsbh],[hfrq],[fzjg],[bz])	
		select fid,  fprjitemid, projectname, jsdw, address, area,ydxz, constrscale, ydghxkzbh, 
		       createtime, hfjg,others 
		 from dbcenter.dbo.tc_sgxkz_jsydghxkz as a    
	  where a.fappid = @appid       	   
end

GO


