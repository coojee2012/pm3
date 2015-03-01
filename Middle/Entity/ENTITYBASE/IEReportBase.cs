using System;
using System.Data;
using System.Collections;
using Approve.EntityBase;


namespace Cost.EntityBase
{
	/// <summary>
	/// 提供报表接口 --IEReportBase 的摘要说明。
	/// Time:2004-11-16
	/// Author:???
	/// </summary>
	public enum RelationParamEnum
	{
		EntityType = 0,
		Relation = 1,
		KeyField = 2,
		StartRow = 3,
		Endrow = 4
	}
	
	public interface IEReportBase
	{
		int Count
		{
			get;
		}

		string ReportName
		{
			get;
			set;
		}

		string[] FieldName
		{
			get;
		}

		bool IsMayUpdate
		{
			get;
		}
		
		DataRow this[int i]
		{
			get;
		}

		ArrayList Relation
		{
			get;
		}

		int  RelationCount
		{
			get;
		}

		bool AddRelation(EntityTypeEnum eType,IDictionary Relation,string keyField,int startrow,int endrow);	

		bool DelRelation(int relPos);

		bool DelRelation(EntityTypeEnum eType);

		bool ClearRelation();		

		DataTable   GetData();
		Type		GetDataType(string FieleName);
	}
}
