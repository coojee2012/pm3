using System;
using System.Data;
using System.Collections;


namespace Approve.EntityBase
{
	/// <summary>
	///   <br>Function:</br>
	///   <br>1.**����    1ϵͳ��ʵ�������,���е�ʵ�����ͱ���̳д˻��࣬��ʵ������ʵ��Ĺ�ͬ���Ժͷ���  </br>
	///   <br>2.�̳б��������Ƽ�����ΪXxxEntity  </br>
	///   <br>Author:Wengmj,jack </br>
	///   <br>Time:2004-11-16 </br>    
	/// </summary>
	[Serializable]
	public class EBase : IEBase
	{
		                                                                                         ///<summary >�ֶ���</summary> 	
		private    string              _sField;            //                                  
		                                                                                          ///<summary>����Ҫ�� a|b|c(�о�Ҫ��ʾ���ֶ�)</summary> 		
		private    string				m_ReduceOption;    //����Ҫ�� a|b|c(�о�Ҫ��ʾ���ֶ�)   
		                                                                                          ///<summary>�Ƿ�ü�</summary> 		
		private    bool					m_IsReduce;		   //�Ƿ�ü�
		                                                                                          ///<summary >ʵ����</summary> 
		protected  EntityTypeEnum       m_EntityType;      // ʵ���� 
		                                                                                          ///<summary>�洢ʵ������</summary> 		
		public   SortedList			    m_dict;         	//�洢ʵ������             
		/// <summary>
		/// ���ݵ����ߴ�����ֵ�/dr/dr and dict��������ʵ��***----------------------------
		/// </summary>
		/// <param name="dict">�ֵ���Ϣ</param>
		public EBase(IDictionary dict) ////���ݵ����ߴ�����ֵ��������ʵ��
		{
			if ( dict!=null )
				m_dict=(SortedList)dict;
			else
				m_dict=new SortedList();

			m_IsReduce = false;
			m_ReduceOption = "";
			m_EntityType = EntityTypeEnum.EBase; //ʵ������Ϊ������
		}
		/// <summary>
		/// ����DataRow ת������dr-->SortList�����洢ʵ������
		/// </summary>
		/// <param name="dr">DataRow</param>
		public EBase(DataRow dr)
		{
			m_IsReduce = false;
			m_ReduceOption = "";
			m_EntityType = EntityTypeEnum.EBase;		
			m_dict=(SortedList)CovertDataRow(dr);//ת������dr-->SortList�����洢ʵ������
		}
		/// <summary>
		/// Function:��DataRow��IDictionary����ʵ��
		/// </summary>
		/// <param name="dr">DataRow</param><param name="rel">IDictionary</param>
		public EBase(DataRow dr,IDictionary rel)
		{
			m_IsReduce = false;
			m_ReduceOption = "";
			m_EntityType = EntityTypeEnum.EBase;		
			m_dict=(SortedList)CovertDataRowR(dr,rel);////���ֵ��key����SortedList��key,��ͨ���ֵ��valueȡ��dr��ֵ����SortedList
		}
		/// <summary>
		/// �������������
		/// </summary>
		public EBase()
		{
			m_dict = new SortedList();
			m_IsReduce = false;
			m_ReduceOption = "";
			m_EntityType = EntityTypeEnum.EBase;	
			SetProperty("FID",Guid.NewGuid().ToString());
			SetProperty("FTIME",System.DateTime.Now);
			SetProperty("FISDELETED",false);
		}

		/// <summary> ʵ����</summary>
		public EntityTypeEnum EntityType
		{
			get
			{
				return m_EntityType;
			}
		}

		/// <summary> ʵ����</summary>
		public string  FId
		{
			set
			{
				SetProperty("FId",value);
			}
			get
			{
				return EConvert.ToString (GetProperty("FId"));
			}
		}

	
		
		/// <summary>ʵ��ĸ���ʱ��</summary>
		public System.DateTime FTime
		{
			set
			{
				SetProperty("FTIME",value);
			}
			get
			{
				return EConvert.ToDateTime(GetProperty("FTIME"));
			}
		}		
		
		/// <summary>ʵ���Ƿ������ݿ����Ƿ񱻱�־Ϊɾ��״̬</summary>
		public bool FIsDeleted
		{
			set
			{
				SetProperty("FISDELETED",value);
			}
			get
			{
				return EConvert.ToBool(GetProperty("FISDELETED"));
			}
		}

		/// <summary>�Ƿ�ü�</summary>
		public bool IsReduce
		{
			get
			{
				return m_IsReduce;
			}
			set
			{
				m_IsReduce = value;
			}
		}

		/// <summary>����Ҫ�� a|b|c</summary>
		public string ReduceOption
		{
			get
			{
				return m_ReduceOption;
			}
			set
			{
				m_ReduceOption = value;
			}
		}
		/// <summary> ���ʵ�����Ե��ܳ� </summary>	
		public string  sField     
		{
			get { return _sField ;}
			set {_sField=value;}
		}
		
	
	/// <summary>
	/// ʵ������
	/// </summary>
	/// <returns>m_dict</returns>
		public IDictionary GetData()
		{
			return m_dict;
		}
      /// <summary>
      /// ͨ���ֶε����ƣ����ֵ��л�ö�Ӧ��ֵ
      /// </summary>
      /// <param name="name">�ֶ�����</param>
      /// <returns>�ֶ�ֵ</returns>
		protected object GetProperty(string name)
		{
			if (name.Equals("")) return null;
			name=name.ToUpper();

			if ( m_dict.ContainsKey(name))
				return m_dict[name];
			else
				return null;
		}
	/// <summary>
	/// ���ֶ���ת��Ϊ��д�����ֶκ����Ӧ��ֵ�����ֵ���SortedList
	/// </summary>
	/// <param name="name">�ֶ�����</param>
	/// <param name="propValue">�ֶ�ֵ</param>
	/// <returns>true��false</returns>
		protected bool SetProperty(string name,object propValue)
		{
			if (name.Equals("")) return false;
			if ( propValue==null )	return false;

			name=name.ToUpper();
			if ( m_dict.ContainsKey(name) )
				m_dict.Remove(name);
			m_dict.Add(name,propValue);
			return true;
		}
		/// <summary>
		/// �ڹ��캯����ͨ������dr�������ݹ���ʵ��
		/// </summary>
		/// <param name="dr">DataRow</param>
		/// <returns>dict</returns>
		public static IDictionary CovertDataRow(DataRow dr)
		{
			SortedList dict=new SortedList();
			if ( dr != null )
			{
				for ( int i=0;i<dr.Table.Columns.Count;i++ )
				{
					string name=dr.Table.Columns[i].ColumnName;
				
					dict.Add(name.ToUpper(),dr[name]);
					
				}
			}
			return dict;				
		}

		/// <summary>
		/// **---���ֵ��key����SortedList��key,��ͨ���ֵ��valueȡ��dr��ֵ����SortedList**--------
		/// </summary>
		/// <param name="dr">DataRow</param>
		/// <param name="rel">IDictionary</param>
		/// <returns>dict</returns>
		public static IDictionary CovertDataRowR(DataRow dr,IDictionary rel)
		{
			SortedList dict=new SortedList();
			if ( dr != null && rel != null )
			{
				IEnumerator ekey=rel.Keys.GetEnumerator();
				IEnumerator eval=rel.Values.GetEnumerator();
				ekey.MoveNext();
				eval.MoveNext();
				for ( int i=0;i<rel.Count;i++ )
				{
					string name=((string)ekey.Current).ToUpper();
					object val=dr[eval.Current.ToString()];				
					dict.Add(name,val);
					ekey.MoveNext();
					eval.MoveNext();
				}
			}
			return dict;				
		}
		/// <summary>
		/// �������ԴDataView(dt)���󣿣�����
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <param name="dict">IDictionary</param>
		/// <returns>DataView(dtT)</returns>
		public static ICollection CreateDataSource(DataTable dt,IDictionary dict) 
		{
			if ( dict == null ) return new DataView(dt);
			if ( dict.Count == 0 ) return new DataView(dt);

			DataTable dtT = new DataTable() ; 
			//--��������
			IEnumerator ekey=dict.Keys.GetEnumerator();
			IEnumerator eval=dict.Values.GetEnumerator();
			ekey.MoveNext();
			eval.MoveNext();
			for ( int i=0;i<dict.Count;i++)
			{
				dtT.Columns.Add(eval.Current.ToString(),dt.Columns[ekey.Current.ToString()].DataType);
				ekey.MoveNext();
				eval.MoveNext();
			}
			//---��ֵ
			object[] tObj;
			for ( int  k=0 ;k<dt.Rows.Count;k++)
			{
				tObj=new object[dict.Count];
				ekey=dict.Keys.GetEnumerator();
				ekey.MoveNext();
				for ( int j=0 ;j<dict.Count;j++)
				{
					tObj[j]=dt.Rows[k][ekey.Current.ToString()];
					ekey.MoveNext();
				}
				dtT.Rows.Add(tObj);
			}
			//--�õ�
			return new DataView(dtT);
		}
	}
}
