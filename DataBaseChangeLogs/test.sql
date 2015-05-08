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


--ɾ���ظ�����ҵ��Ϣ
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


--ɾ���ظ�����Ա��Ϣ
create trigger dbo.Tri_Del_RepRyinfo on dbo.TC_SGXKZ_RYBGJG 
after insert as
begin
  delete TC_SGXKZ_RYBGJG
      from TC_SGXKZ_RYBGJG c
	 where exists(select 1 from (select a.FLinkId,a.FAppid,a.FPrjItemId from TC_SGXKZ_RYBGJG a, inserted b 
	                                           where a.FAppid     = b.FAppid
                                                 and a.FPrjItemId = b.FPrjItemId
                                                 and a.FLinkId    = b.FLinkId 
											 group by a.FLinkId,a.FAppid,a.FPrjItemId
											   having count(1) >1
							    ) d
						   where c.FLinkId = d.FLinkId 
						     and c.FAppid = d.FAppid 
							 and c.FPrjItemId = d.FPrjItemId)
end
go

















select  FJSDWID,*  from  TC_AJBA_Record  where FAppId = '9d111d7f-b142-41d5-8b2b-712bb1275219'





select  *  from  

select  FJSDWID,*  from  TC_AJBA_Record  where  FJSDWID is null


update  TC_AJBA_Record  set  FJSDWID = '000e7aea-9705-4659-acf4-3032daca2fe2'  where   FJSDWID  = 'D064600B-07C1-4BC4-957C-E77C54B901BD'


select  *  from  JST_XZSPBaseInfo.dbo.RY_RYJBXX  order by QYBM



select a.SYZT, a.* from GC_JQSBXX a,[XM_XMJBXX] b where a.xmbh=b.xmbh


alter  table  xm_baseinfo.dbo.GC_JQSBXX add columns syzt varchar(10);


alter table xm_baseinfo.dbo.GC_JQSBXX  add syzt varchar(10) null 



select  * from  
   JST_XZSPBaseInfo.dbo.RY_RYJBXX a, JST_XZSPBaseInfo.dbo.RY_RYZSXX b,JST_XZSPBaseInfo.dbo.QY_JBXX c
   where a.RYBH = b.RYBH
   and a.QYBM = c.QYBM
   and b.RYZSXXID = ''

           

����Ա�ļ���λ:
�ɶ��������蹤�̹������޹�˾
�������ƽ�����Ŀ������ѯ���޹�˾
��֦���к�Ҷ������Ŀ������ѯ�������ι�˾
�Ĵ���ɺ���Ŀ�������޹�˾
�Ĵ��������蹤�̹������޹�˾
�Ĵ��������̽�������������ι�˾
�Ĵ�����������Ŀ�������޹�˾
�Ĵ����ι��̼������޹�˾
�Ĵ����Ź�����Ŀ�������޹�˾
�Ĵ�����蹤�̹������޹�˾
�Ĵ�����������Ŀ�������޹�˾
�Ĵ�ʡ��ұ���蹤�̼����������ι�˾
�Ĵ���ǿ������Ŀ�������޹�˾
�Ĵ����ƽ���������޹�˾
�Ĵ��ִ�������ѯ�������޹�˾



select  *  from  TC_AJBA_PrjFile

use  JST_XZSPBaseInfo;
go

select c.ZSBH, c.*,a.*  from QY_JBXX a
left join QY_QYZZXX  b
on a.QYBM = b.QYBM
left join QY_QYZSXX c
on  a.QYBM = c.QYBM
where a.QYLXBM = '105'



select *  from  TC_PBBG_ZBHXR


select  *  from  cf_sys_dic  where  fname = ''



select  *  from   [dbo].[CF_Dic_Person]



select  *  from CF_Sys_Dic where FParentid='5080' and FName<>'ȫ��' order by forder,ftime desc






where a.QYLXBM = '101'
and b.SFZX = 1          
and c.ZSLXBM = 2
select a.*  from  QY_JBXX a,QY_QYZSXX b
   where  a.QYBM  = b.QYBM
   and b.ZSLXBM = 2


            


select  *  from  QY_JBXX  where  QYBM = '001E8529-AC91-4CE8-9CB0-3C394767A25B'

select  *  from  QY_QYZSXX  where  QYBM = '001E8529-AC91-4CE8-9CB0-3C394767A25B'



select  *  from  QY_QYZZXX  where QYBM = '001E8529-AC91-4CE8-9CB0-3C394767A25B'



select  *  from  QY_JBXX where  QYMC = '�ɶ���Դ�ｨ���������޹�˾'


--���졢�������ҵ��������Ϣ��ȡ��ʽ
select  *  from  JST_XZSPBaseInfo.dbo.QY_QYZZXX a,JST_XZSPBaseInfo.dbo.QY_QYZZZYXX b
where a.fid = b.FZZID

--��Ա�ȼ����
use Standard_Dic
go
select  *  from  [dbo].[CF_Dic_Person]

use JST_XZSPBaseInfo
go
select ZSJB,*  from  RY_RYZSXX

select FName,FNumber  from Standard_Dic.dbo.[CF_Dic_Person]  where  FType = 'zcrylx'
--֤������
(select    ZSLX  from  JST_XZSPBaseInfo.dbo.RY_RYZSXX)


select  *  from  JST_XZSPBaseInfo.dbo.RY_RYZSXX
select  *  from  JST_XZSPBaseInfo.dbo.RY_RYJBXX


--���졢�����Աû����Ա���ͣ�entinfo.aspx����Ա�б�����

select QYZZID, *  from  JST_XZSPBaseInfo.dbo.QY_QYZZXX where  QYZZID = '0035178F-D7A6-499A-B0DE-7C3A01F78D9A'


use dbCenter
go
select  *  from  TC_SGXKZ_Location where FAppId = '3029d3f1-c240-447b-b7d0-46db20637ad2'


select  *  from  TC_SGXKZ_PrjInfo  where  FAppId = '3029d3f1-c240-447b-b7d0-46db20637ad2'

select  *  from  TC_SGXKZ_Location where FAppId = '140ca213-ee7f-4abb-b9a4-89dc6f563796'

"140ca213-ee7f-4abb-b9a4-89dc6f563796"



--��Ŀ���ڲ��ϣ�����ѡ�������֣�δ��(1�����2������Ҫ����������д)��3���Ժ󲹰�),�Ѱ죺�ӱ�׼���л�ȡ����

--��Ŀ���ڲ��ϣ�����ѡ��Ӧ������������ϸҳ���С�


select  *  from  JST_XZSPBaseInfo.dbo.QY_JBXX  where qybm = 'E8127343-EE7B-4371-A3E1-2FEA7D507C7A'




select  *  from  JST_XZSPBaseInfo.dbo.QY_JBXX   where  QYBM = 'f1ff80f5-1b49-4ba8-bf4a-fdc9eebdb9a'





--��ĳ�����˵���ҵ
declare @qybm varchar(50) ,@lx varchar(20) 
select @lx = QYLXBM from JST_XZSPBaseInfo.[dbo].[QY_JBXX]  where QYMC  ='�Ĵ�������蹤����Ŀ������ѯ���޹�˾'
select @lx


--��ʾͬ������Ա����ҵ�������Ҫ��ĳ����Ա������Ҫ���ӹ�������
select a.QYBM,b.qymc,count(1) from JST_XZSPBaseInfo.dbo.RY_RYJBXX a , JST_XZSPBaseInfo.[dbo].[QY_JBXX]  b
 where a.qybm = b.QYBM and b.QYLXBM = 105
 group by a.QYBM,b.qymc

 ����Ա�Ĵ������
 0527e8bb-95e4-4e75-a2ef-402417253cd3	�Ĵ����Ž�����ѯ���޹�˾	4
19b2fc00-196e-40de-86f6-98edc03868b5	�Ĵ�����������ѯ�������޹�˾	1
baa91447-e7a1-4623-bf94-42eea5dbae9d	�ɶ��з���ҵ������Ŀ�������޹�˾	1

 
 select  *  from   JST_XZSPBaseInfo.[dbo].[QY_JBXX]  b  where  QYLXBM = '109'


 select  *  from  JST_XZSPBaseInfo.dbo.QY_JBXX  where  QYBM = 'f1ff80f5-1b49-4ba8-bf4a-fdc9eebdb9a8'


 select  *  from  JST_XZSPBaseInfo.dbo.RY_RYJBXX  where QYBM = 'f1ff80f5-1b49-4ba8-bf4a-fdc9eebdb9a8'

 update  JST_XZSPBaseInfo.dbo.RY_RYJBXX   set QYBM = 'f1ff80f5-1b49-4ba8-bf4a-fdc9eebdb9a8' where qybm = '000e7aea-9705-4659-acf4-3032daca2fe2' 

 update  JST_XZSPBaseInfo.dbo.RY_RYZSXX set QYBM = 'f1ff80f5-1b49-4ba8-bf4a-fdc9eebdb9a8' where qybm = '000e7aea-9705-4659-acf4-3032daca2fe2' 

select  *   from  JST_XZSPBaseInfo.dbo.RY_RYJBXX  where RYBH = '461F6B7F-5144-4614-A502-9F39E9E1A7F4'





select  ZSDJMC,*  from  JST_XZSPBaseInfo.dbo.QY_QYZSXX


select  *  from  JST_XZSPBaseInfo.dbo.QY_JBXX  where QYBM = '1fe9465c-0de7-41b5-8c8f-02ee3a657106'


select  *  from  TC_SGXKZ_RYBGJG


select  *  from  TC_PrjItem_Emp
where (FAppId = 'aa94dd8e-0f38-4c9f-8496-a88aaf993525' or FAppId = '8bd768d4-bf45-424e-90a1-cdad651d41bb')
and FEntId = '1fe9465c-0de7-41b5-8c8f-02ee3a657106'


select *  from  TC_PrjItem_Ent  where  FId = 'f9d43f48-5f40-4f01-ae5c-efceb2b015e1'
dbContext.TC_PrjItem_Ent.Where(t => t.FId == txtFId.Value).FirstOrDefault();
TC_PrjItem_Ent



--ͨ���ϱ��ҳ��鵵���ݿ�
--ʩ�����֤������ҵ��ӹ鵵����,Ŀǰ�Ǵӱ����ȡ��ʩ�����֤��Ŀ��Ϣ�͹�����Ϣ��


--ҵ���  �鵵�� (dbcenter)


--��׼��  (xm_baseinfo)


select  *  from  YW_GD_GCGH_SZ



--��¼�����ϢӦ������ҵ�ϱ���ʱ��д������¼���У���Ӧ����ÿ��ҵ����д�룬���һ����ҵ�����ˣ�ֻ��¼��ҵ�����¼����Ա�Զ�ȫ��ɾ������Ա�����¼֪ʶ��¼һ����ҵ��Ա���������������

--֤������

--���̱���






select *  from  XM_BaseInfo.dbo.GC_SGXKZ  where GCMC = '20150318������Ŀ-���幤��һ'


select *  from XM_BaseInfo.dbo.[XM_XZYJS]  where XMMC = '201503
18������Ŀby psq '



select  *  from  TC_Prj_Info  where  ProjectName = '��ɽ���������½����Ź�����Ŀ�������-�б걸������'


select  *  from  JST_XZSPBaseInfo.dbo.QY_JBXX where  QYBM = '5d5edcf8-265e-491f-a9de-6740bb88ceb5'



select  b.*  from  JST_XZSPBaseInfo.dbo.QY_JBXX a,JST_XZSPBaseInfo.dbo._RY_RYJBXX b
where a.QYBM = b.QYBM
and a.QYMC = '�Ĵ��������蹤�̹������޹�˾'




select * from dbo.TC_SGXKZ_BGPrjInfo  where FAppId='31961363-8442-452b-92e7-6ea42d717519'


select YDPZSX  from dbo.TC_SGXKZ_Location where FAppId='31961363-8442-452b-92e7-6ea42d717519'


select YDGHXKZBH,HFJG,CreateTime  from dbo.TC_SGXKZ_JSYDGHXKZ where FAppId='31961363-8442-452b-92e7-6ea42d717519'


select SGTJ  from dbo.TC_SGXKZ_QTZL where FAppId='31961363-8442-452b-92e7-6ea42d717519'


select ZBTZSBH  from dbo.TC_SGXKZ_ZBJG where FAppId='31961363-8442-452b-92e7-6ea42d717519'


select HTBH  from dbo.TC_SGXKZ_HTBA where FAppId='31961363-8442-452b-92e7-6ea42d717519'


select a.* from TC_PrjItem_Emp a,TC_PrjItem_Ent b 
where a.FEntId = b.FId and b.FEntType = 7  and a.EmpType = 11220209 and a.FAppId='31961363-8442-452b-92e7-6ea42d717519'


 <!--��׼�ֵ�������ݿ� add by psq 20150420-->
    <add name="Standard_Dic" connectionString="Password=jkc115;Persist Security Info=True;User ID=jkc115;Initial Catalog=Standard_Dic;Data Source=.\sql" providerName="System.Data.SqlClient"/>





--��Ա����
select * from Standard_Dic.dbo.CF_Dic_Person where ftype='zcrylx' order by FOrder


--��ҵ���ͱ���
select * from Standard_Dic.dbo.CF_Dic_Ent where ftype='qylx' order by FOrder
--��ʾͬ������Ա����ҵ�������Ҫ��ĳ����Ա������Ҫ���ӹ�������
select a.QYBM,b.qymc,count(1) from JST_XZSPBaseInfo.dbo.RY_RYJBXX a , JST_XZSPBaseInfo.[dbo].[QY_JBXX]  b
 where a.qybm = b.QYBM and b.QYLXBM = '104'
 group by a.QYBM,b.qymc



 select  *  from  TC_SGXKZ_RYBGJG

 select *  from  TC_SGXKZ_BGJG

 select *  from  TC_SGXKZ_BGPrjInfo


 select  *  from  TC_SGXKZ_BGPrjInfo

 select  *  from  CF_App_List


 old��ҵ��  ->TC_SGXKZ_BGPrjInfo
 TC_SGXKZ_BGPrjInfo - >TC_SGXKZ_BGPrjInfo   



 select  *  from  TC_PrjItem_Ent  where  FPrjItemId = 'CE3B48AA-8D6B-4420-818A-84B6FFA7EB5D'  


 select  *  from  CF_App_List  where  FId = '2E0DA810-DD0F-4328-A86A-462B147D23A3'

 select * from  CF_App_List where  FBaseinfoId = '42595780-4D48-4F74-8058-C7F61CEAA266'



 select  *  from  TC_SGXKZ_PrjInfo  order by FPrjItemId


 select  *  from  TC_SGXKZ_PrjInfo  where  ProjectName = '�����г��й�ˮ�������칤��-�б걸������'

 update  TC_SGXKZ_PrjInfo  set  ProjectNo = '123'  where  ProjectName = '�����г��й�ˮ�������칤��-�б걸������'


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



select  *  from  TC_Prj_Info  where  ProjectName = '�����г��й�ˮ�������칤��-�б걸������'



SELECT  *  FROM  CF_App_List  A WHERE  A.FPrjId IN 
('0766821F-8E2F-4045-B4F4-C1661BE28C22','95D2C7D5-8E95-4C08-BA6E-C9F4980D218C')


SELECT  *  FROM  CF_App_List  WHERE FId = 'dea06762-dd7d-4a3f-80c8-68a79356a2f4'


exec SP_GD_SGXKZ 'FFD6D0DC-0272-488D-B072-A49042E340A0','dea06762-dd7d-4a3f-80c8-68a79356a2f4'



SELECT  *  FROM  GD_TC_SGXKZ_PrjInfo


ALTER TABLE TC_SGXKZ_BGPrjInfo ADD FLinkId varchar(36) NULL
--˵�����״α��ʱ��FLinkId = TC_SGXKZ_PrjInfo���FAppId
--;���μ����ϱ��ʱ��FLinkId = TC_SGXKZ_BGPrjInfo���FAppId

alter table TC_PrjItem_Ent add needDel int default 0 
alter table TC_PrjItem_Emp add needDel int default 0 



select  *  from  TC_SGXKZ_PrjInfo  where  FId = 'F076CCE4-B594-436B-AA78-2BEE7A9A99DA'


select  *  from  GD_TC_SGXKZ_PrjInfo


FFD640FB-E5EA-4FC9-88A6-1579BDEDB409    11223


select * from CF_App_List                where FManageTypeId=11223 and FLinkId='FFD640FB-E5EA-4FC9-88A6-1579BDEDB409'


select *  from  TC_PrjItem_Info  where FId = 'FFD640FB-E5EA-4FC9-88A6-1579BDEDB409'

update  TC_Prj_Info 
set  ProjectType = '2000101'
 where  (ProjectType  = '' or ProjectType is null)



 select  *  from  TC_SGXKZ_PrjInfo  where  FPrjItemId 


 select  *  from  TC_SGXKZ_HTBA


 exec SP_GD_SGXKZ '',''


 select  *  from TC_PrjItem_Ent  where FPrjItemId = 'CE3B48AA-8D6B-4420-818A-84B6FFA7EB5D'

 SELECT  *  FROM  GD_TC_SGXKZ_PrjInfo

  select  *  from  TC_SGXKZ_BGPrjInfo

  SELECT  *  FROM  GD_TC_PrjItem_Emp


  SELECT  *  FROM  TC_PrjItem_Ent  WHERE FPrjItemId = '5e8b6302-1e24-4d1f-8879-0affb3c91a21'  ORDER BY FAppId


  SELECT  *  FROM  TC_SGXKZ_BGPrjInfo  WHERE FAppId = '49270886-e158-4cbf-ba27-e37ea64a8a6c'

  










  SELECT  *  FROM  TC_PrjItem_Info  WHERE ProjectName = '������Ŀ20150414�����1'

  SELECT  *  FROM  TC_SGXKZ_PrjInfo  WHERE  ProjectName = '������Ŀ20150414�����1'

  SELECT  *  FROM     TC_SGXKZ_BGPrjInfo  WHERE  ProjectName = '��ɽ���������½����Ź�����Ŀ�������-�б걸������'


  UPDATE  TC_SGXKZ_BGPrjInfo  SET FLinkId = 'e3d1ddd3-cfdb-4018-94b2-b10cffb88135'  WHERE ProjectName = '��ɽ���������½����Ź�����Ŀ�������-�б걸������'

  �е�����˵�����
  http://localhost/Government/AppSGXKZGL/BGBLJJAuditInfo.aspx?ftype=1&FLinkId=9debd909-c547-4baf-a43f-b6acff2df9e8&fSubFlowId=bd883f37-8a74-40e9-a26a-77700e4a627e&fBaseInfoId=8b1b728c-c2bd-4237-ace4-d872e9dccaa4&fpid=077bbae5-80b1-45d3-8598-fae2ee16392a&ferid=f4091d8d-c1a7-493f-8b5a-3294b0d95d0f&rid=0.9472391328628876



 alter table TC_PrjItem_Emp add checkstate varchar(10) null
 alter table GD_TC_PrjItem_Emp add checkstate varchar(10) null




 from t in dbContext.TC_PrjItem_Info
                  join a in dbContext.GD_TC_SGXKZ_PrjInfo
                  on t.FId equals a.FPrjItemId
                  join b in dbContext.CF_App_List
                  on a.FAppId equals b.FId
                  //where t.FJSDWID == CurrentEntUser.EntId && b.FState == 6
                  where  b.FState == 6
                  orderby t.FId

SELECT  * FROM  TC_PrjItem_Info A,GD_TC_SGXKZ_PrjInfo B,CF_App_List C
WHERE  A.FId = B.PrjItemId
AND  B.FAppId = C.FId
AND C.FState = 6

SELECT  *  FROM  
GD_TC_SGXKZ_PrjInfo



SELECT  *  FROM  TC_SGXKZ_BGJG

SELECT  *  FROM  TC_SGXKZ_RYBGJG

SELECT  *  FROM  TC_SGXKZ_QYBGJG



������Ŀ20150401-����һ



select  *  from  TC_PrjItem_Ent
where FAppId = '49270886-e158-4cbf-ba27-e37ea64a8a6c'

select  *  from  TC_PrjItem_Emp 
where --FAppId = '49270886-e158-4cbf-ba27-e37ea64a8a6c' 
--and
 FEntId = '56f358f6-bc20-4007-abd7-6a474c4bc03c'
and FEntType = '2'


select  *  from  GD_TC_PrjItem_Emp
where --FAppId = '49270886-e158-4cbf-ba27-e37ea64a8a6c' 
--and
 FEntId = '56f358f6-bc20-4007-abd7-6a474c4bc03c'
and FEntType = '2'



SELECT  *  FROM  JST_XZSPBaseInfo.dbo.QY_JBXX  where  QYBM = '56f358f6-bc20-4007-abd7-6a474c4bc03c'


select  *  from  JST_XZSPBaseInfo.dbo.QY_JBXX  where QYBM = '0035178F-D7A6-499A-B0DE-7C3A01F78D9A'


select b.QYMC,a.*  from  JST_XZSPBaseInfo.dbo._RY_RYJBXX  a,JST_XZSPBaseInfo.dbo.QY_JBXX b
where  a.QYBM = b.QYBM
--and b.QYLXBM = '104'
and b.QYBM = '81626ab3-62fa-4022-a18c-2864fec555a9'
and b.QYMC = '�Ĵ����Ͻ��蹤��������޹�˾'





select  *  from  JST_XZSPBaseInfo.dbo.QY_JBXX  where  QYMC = '�Ĵ����Ͻ��蹤��������޹�˾'


select  *  from  XM_BaseInfo.dbo.XM_XMBJXX  where   = '��̨�ر�Ȫ�羴��Ժʩ������'


select  *  from  TC_Prj_Info  where  ProjectName = '��̨�ر�Ȫ�羴��Ժʩ������'



select  AddressDept,*  from  TC_AJBA_Record  where ProjectName like '%��̨�ر�Ȫ�羴��Ժʩ������%'


select  *  from  TC_Prj_Info 


select  *  from  CF_Sys_Dic  where  FNumber like '20001%'


5132251505059901-AX-001


select AddressDept,*  from  TC_PrjItem_Info  


select  *  from  TC_PrjItem_Info  where  ProjectName like '%��̨�ر�Ȫ�羴��Ժʩ������%'


exists(select  1  from  TC_PrjItem_Emp_Lock a where  a.ddd  != currddd and FIdCard =ddd.)



select  *  from  JST_XZSPBaseInfo.dbo.QY_JBXX  where QYMC like '%�Ĵ����������������޹�˾%'   


select  *  from  JST_XZSPBaseInfo.dbo.RY_RYJBXX  where QYBM = '0E9E8AB5-5D4D-407F-89FD-0417D155A65A'



select *  from  TC_PrjItem_Emp_Lock c,TC_PrjItem_Info d
 where c.FPrjItemId = d.FId
 and  exists (select  1  from  TC_PrjItem_Emp_Lock a,TC_PrjItem_Info b where  a.FPrjItemId = b.FId and   b.AddressDept != d.Address and c.FIdCard =a.FIdCard)
 and c.FIdCard ='511502198703231305'


 select  *  from  TC_PrjItem_Emp_Lock a,TC_PrjItem_Info b
 where a.FPrjItemId = b.FId
 and  a.FIdCard = '511502198703231305'
 and b.AddressDept != '5133'
 and a.IsLock = '1'


 select  *  from  TC_SGXKZ_ProjectInfo where FAppId = 'ef123519-d786-45ea-9a38-1e80afadb4fd'



select * from  TC_PrjItem_Emp_Lock a,TC_PrjItem_Info b
                       where a.FPrjItemId = b.FId
	and  a.FIdCard = '512924196307088799'  and b.AddressDept != ''   and a.IsLock = '1'



	select  *  from  TC_SGXKZ_PrjInfo  

	select  *  from  TC_PrjItem_Info  where FId = 'FF89E69D-3D76-4069-80E9-4910D275DFF3'


	select *  from   TC_PrjItem_Emp  where FAppId = 'ef123519-d786-45ea-9a38-1e80afadb4fd'


	select  *   from  cf_sys_dic where fname like '%�Թ�%'


		select  *  from  TC_PrjItem_Info  where  AddressDept  is null

select  a.*  from  TC_PrjItem_Emp_Lock a,TC_PrjItem_Info b
                            where a.FPrjItemId = b.FId
							                             and  a.FIdCard = '512924196307088799'  and b.AddressDept != '51'   and a.IsLock = '1'


select  *  from   TC_PrjItem_Emp_Lock  where FIdCard = '510182197906122015'

select  *  from  JST_XZSPBaseInfo.dbo._RY_RYJBXX  where  SFZH = '510182197906122015'

select  *  from JST_XZSPBaseInfo.dbo.QY_JBXX where QYBM = '333750A5-5790-491F-9F9A-3406CC05D4A6'


select  *  from  TC_PrjItem_Info  where  FId = 'FF336A07-9E22-4E22-8BC1-9035CE68A3DF'

update  TC_PrjItem_Info  set  AddressDept = '511022' where  FId = 'FF336A07-9E22-4E22-8BC1-9035CE68A3DF'



select  PlanStartTime,PlanEndTime  from  TC_AJBA_Record

select a.* from GC_JQSBXX a,[XM_XMJBXX] b where a.xmbh=b.xmbh 

select  *  from  XM_BaseInfo.dbo.GC_JQSBXX a


select a.* from XM_BaseInfo.dbo.GC_JQSBXX a left join XM_BaseInfo.dbo.[XM_XMJBXX] b 
on a.xmbh=b.xmbh 


select  *  from  JST_XZSPBaseInfo.dbo.QY_QYZSXX  where 
 ZSLXBM = '2'

select  *  from  JST_XZSPBaseInfo.dbo.QY_JBXX  where QYMC = '�Ĵ����������������޹�˾'


select *  from JST_XZSPBaseInfo.dbo.QY_JBXX  where  QYBM = '1AB8CD76-4621-49E8-870C-5925AF6BAC02'

select  a.qymc,b.*  from  JST_XZSPBaseInfo.dbo.QY_JBXX a,JST_XZSPBaseInfo.dbo.QY_QYZSXX b
where a.QYBM = b.QYBM
and b.ZSLXBM = 2
and b.QYBM = '0000325F-1AA2-4AF2-9410-7BAF3BE01C1A'

�ɶ���Դ�ｨ���������޹�˾


select  b.zsbh,b.*  from  JST_XZSPBaseInfo.dbo.QY_JBXX a,JST_XZSPBaseInfo.dbo.QY_QYZSXX b
                          where a.QYBM = b.QYBM
						  and b.ZSLXBM = 2
 and a.QYMC = '�ɶ���Դ�ｨ���������޹�˾'

 
select *  from  JST_XZSPBaseInfo.dbo.QY_JBXX  where  QYBM = '8b1b728c-c2bd-4237-ace4-d872e9dccaa4'



select  *  from  TC_AJBA_Record   where  fappid = 'e342f14b-5052-462d-bdd0-4dd1f5dc7aae'


select  *  from      