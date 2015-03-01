using System;
using System.Data;
using System.Collections;
using Approve.EntityBase;

namespace Cost.EntityBase
{
	/// <summary>
	/// �ṩ������ --IEReportBase ��ժҪ˵����
	/// Time:2004-11-16
	/// Author:???
	/// </summary>
	[Serializable]
	public class EReportBase:IEReportBase
	{
		private DataTable m_DataTable;
		private string    m_ReportName;
		private bool	  m_IsMayUpdate;

		private ArrayList m_Relation;

		public EReportBase()
		{
			m_DataTable  = new DataTable();
			m_ReportName = "Report";
			m_IsMayUpdate= false;
			m_Relation=new ArrayList();
		}

		public EReportBase(DataTable dt,string rptName,bool isMayUpdate)
		{
			if ( dt==null )
				m_DataTable=new DataTable();
			else
				m_DataTable=dt;
			m_ReportName = rptName;
			m_IsMayUpdate= isMayUpdate;
			m_Relation=new ArrayList();
		}

		#region IEReportBase ��Ա

		public int Count
		{
			get
			{
				return m_DataTable.Rows.Count;
			}
		}

		public string ReportName
		{
			get
			{
				return m_ReportName;
			}
			set
			{
				m_ReportName=value;
			}
		}

		public string[] FieldName
		{
			get
			{
				int col = m_DataTable.Columns.Count;
				string[] tmp=new string[col];
				for ( int i=0; i<col; i++ )
				{
					tmp[i] = m_DataTable.Columns[i].ColumnName;
				}
				return tmp;
			}
		}

		public bool IsMayUpdate
		{
			get
			{
				return m_IsMayUpdate;
			}
		}
        //--���Datatable
		public DataTable GetData()
		{
			return m_DataTable;
		}
        //--���Datatable�е�����
		public Type GetDataType(string Name)
		{
			DataColumn dc = m_DataTable.Columns[Name];
			if ( dc !=null) 
				return dc.DataType;
			else
				return null;
		}
       //--���Datatableĳһ��
		public DataRow this[ int i ]
		{
			get
			{
				return m_DataTable.Rows[i];
			}
		}

		public ArrayList Relation
		{
			get
			{
				return m_Relation;
			}
		}

		public int RelationCount
		{
			get
			{
				return m_Relation.Count;
			}
		}
        //--����Relation��ϵ��m_Relation�ֶ��б���,��������
		public bool AddRelation(EntityTypeEnum eType,IDictionary Relation,string keyField,int startRow,int endRow)
		{
			if ( Relation == null ) return false;
			if ( Relation.Count == 0 ) return false;
			if ( keyField == "" ) return false;

			object[] Item = new object[5];
			Item[(int)RelationParamEnum.EntityType]=eType;
			Item[(int)RelationParamEnum.Relation]=Relation;
			Item[(int)RelationParamEnum.KeyField]=keyField;
			Item[(int)RelationParamEnum.StartRow]=startRow;
			Item[(int)RelationParamEnum.Endrow]=endRow;
			m_Relation.Add(Item);		
			return true;
		}
        //--�Ƴ�m_Relation�е�ָ��λRelation��ϵ,��������
		public bool DelRelation( int pos)
		{
			m_Relation.RemoveAt(pos);
			return true;
		}
       //--�Ƴ�----------****??????
		public bool DelRelation( EntityTypeEnum eType)
		{
			foreach (object[] obj in m_Relation)
			{
				EntityTypeEnum eTy=(EntityTypeEnum)obj[0];//????
				if ( eTy == eType )
					m_Relation.Remove(obj);
			}
			return true;
		}
       //--�Ƴ�m_Relation�е�������ϵ,��������
		public bool ClearRelation()
		{
			m_Relation.Clear();
			return true;
		}

		#endregion
	}
}
