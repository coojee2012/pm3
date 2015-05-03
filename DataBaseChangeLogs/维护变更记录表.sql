if exists (select 1
          from sysobjects
          where id = object_id('dbo.Tri_del_RepPrjinfo')
          and type = 'TR')
   drop trigger dbo.Tri_del_RepPrjinfo
go


create trigger dbo.Tri_del_RepPrjinfo on dbo.TC_SGXKZ_BGJG for insert as
begin
    delete TC_SGXKZ_BGJG
      from TC_SGXKZ_BGJG a,inserted b 
     where a.FAppid     = b.FAppid
       and a.FPrjItemId = b.FPrjItemId
       and a.BGNR       = a.BGNR

    return
end
go


--删除重复的企业信息
if exists (select 1
          from sysobjects
          where id = object_id('dbo.Tri_Del_RepQyinfo')
          and type = 'TR')
   drop trigger dbo.Tri_Del_RepQyinfo
go


create trigger dbo.Tri_Del_RepQyinfo on dbo.TC_SGXKZ_QYBGJG 
after insert as
begin
    delete TC_SGXKZ_QYBGJG
      from TC_SGXKZ_QYBGJG c
	 where exists(select 1 from (select a.FLinkId,a.FAppid,a.FPrjItemId from TC_SGXKZ_QYBGJG a, inserted b 
	                                           where a.FAppid     = b.FAppid
                                                 and a.FPrjItemId = b.FPrjItemId
                                                 and a.FLinkId    = b.FLinkId 
												 and a.YQLX       = b.YQLX
											 group by a.FLinkId,a.FAppid,a.FPrjItemId
											   having count(1) >1
							    ) d
						   where c.FLinkId = d.FLinkId 
						     and c.FAppid = d.FAppid 
							 and c.FPrjItemId = d.FPrjItemId)
end
go

if exists (select 1
          from sysobjects
          where id = object_id('dbo.Tri_Del_RepRyinfo')
          and type = 'TR')
   drop trigger dbo.Tri_Del_RepRyinfo
go


--删除重复的人员信息
create trigger dbo.Tri_Del_RepRyinfo on dbo.TC_SGXKZ_RYBGJG 
after insert as
begin
  delete TC_SGXKZ_RYBGJG
      from TC_SGXKZ_RYBGJG c
	 where exists(select 1 from (select a.FLinkId,a.FAppid,a.FPrjItemId from TC_SGXKZ_RYBGJG a, inserted b 
	                                           where a.FAppid     = b.FAppid
                                                 and a.FPrjItemId = b.FPrjItemId
                                                 and a.FLinkId    = b.FLinkId
												 and a.RYLX       = b.RYLX
											 group by a.FLinkId,a.FAppid,a.FPrjItemId
											   having count(1) >1
							    ) d
						   where c.FLinkId = d.FLinkId 
						     and c.FAppid = d.FAppid 
							 and c.FPrjItemId = d.FPrjItemId)
end
go


if exists (select 1
          from sysobjects
          where id = object_id('dbo.Tri_Upd_CheckSate')
          and type = 'TR')
   drop trigger dbo.Tri_Upd_CheckSate
go


create trigger dbo.Tri_Upd_CheckSate on dbo.TC_SGXKZ_RYBGJG for update as
begin
     update TC_PrjItem_Emp 
        set checkstate = b.checkstate 
       from TC_PrjItem_Emp a ,inserted b
      where a.fappid = b.fappid
        and a.FEmpId = b.FLinkId
        and isnull(a.checkstate,3) <> b.checkstate
        and b.checkstate in (0, 1)
end
go

