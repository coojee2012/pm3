
--施工许可证相关表信息
--1、工程简要说明表
   TC_SGXKZ_PrjInfo
 
2、项目参与企业 (企业类型用fenttype区分:2:施工总承包,3:专业承包,4:劳务分包单位,5:勘察单位，6：设计单位,7:监理单位
   TC_PrjItem_Ent
  2.1:项目参与人员
 TC_PrjItem_Emp   (pid为TC_PrjItem_Ent的主键,以关联企业人员)
3、选址意见书
   TC_SGXKZ_Location
  3.1：选址意见书附件
   TC_SGXKZ_File (flinkid为TC_SGXKZ_Location的主键fid，以关联附件)
4、建设用地规划许可证
   TC_SGXKZ_JSYDGHXKZ
   4.1、TC_SGXKZ_File (flinkid为TC_SGXKZ_JSYDGHXKZ的主键fid，以关联附件)
5、建设工程规划许可证
  TC_SGXKZ_JSGCGHXKZ
  5.1、TC_SGXKZ_File (flinkid为TC_SGXKZ_JSGCGHXKZ的主键fid，以关联附件)
6、招投标信息
     TC_SGXKZ_ZBJG
	 6.1、TC_SGXKZ_File(flinkid为TC_SGXKZ_ZBJG的主键fid，以关联附件)
7、 合同备案
  TC_SGXKZ_HTBA
8、 施工审查图信息
  TC_SGXKZ_SGTSC
  8.1、施工审查图人员信息
      TC_SGXKZ_SGTSCRY(FSGTSCId为TC_SGXKZ_SGTSC的主键fid)
9、质量安全监督手续
      TC_SGXKZ_JDSX
    9.1、TC_SGXKZ_File(flinkid为TC_SGXKZ_JDSX的主键fid，以关联附件)
10、资金保函
  TC_SGXKZ_ZJBH
  10.1、TC_SGXKZ_File(flinkid为TC_SGXKZ_ZJBH的主键fid，以关联附件)
11、其他资料
    TC_SGXKZ_QTZL
  11.1、TC_SGXKZ_File(flinkid为TC_SGXKZ_QTZL的主键fid，以关联附件)
12、保证金确认
    TC_SGXKZ_BZJQR

