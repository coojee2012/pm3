using System;
using System.Data;
using System.Collections;


namespace Approve.EntityBase
{
	/// <summary>
	///   <br>Function:</br>
	///   <br>1.**资质    1系统的实体类基类,所有的实体类型必须继承此基类，以实现所有实体的共同属性和方法  </br>
	///   <br>2.继承本类的类的推荐命名为XxxEntity  </br>
	///   <br>Author:Wengmj,jack </br>
	///   <br>Time:2004-11-16 </br>    
	/// </summary>
	[Serializable]
	public class EBase : IEBase
	{
		                                                                                         ///<summary >字段名</summary> 	
		private    string              _sField;            //                                  
		                                                                                          ///<summary>剪贴要求 a|b|c(列举要显示的字段)</summary> 		
		private    string				m_ReduceOption;    //剪贴要求 a|b|c(列举要显示的字段)   
		                                                                                          ///<summary>是否裁剪</summary> 		
		private    bool					m_IsReduce;		   //是否裁剪
		                                                                                          ///<summary >实体名</summary> 
		protected  EntityTypeEnum       m_EntityType;      // 实体名 
		                                                                                          ///<summary>存储实体数据</summary> 		
		public   SortedList			    m_dict;         	//存储实体数据             
		/// <summary>
		/// 根据调用者传入的字典/dr/dr and dict参数构造实体***----------------------------
		/// </summary>
		/// <param name="dict">字典信息</param>
		public EBase(IDictionary dict) ////根据调用者传入的字典参数构造实体
		{
			if ( dict!=null )
				m_dict=(SortedList)dict;
			else
				m_dict=new SortedList();

			m_IsReduce = false;
			m_ReduceOption = "";
			m_EntityType = EntityTypeEnum.EBase; //实体类型为基类型
		}
		/// <summary>
		/// 根据DataRow 转换数据dr-->SortList，并存储实体数据
		/// </summary>
		/// <param name="dr">DataRow</param>
		public EBase(DataRow dr)
		{
			m_IsReduce = false;
			m_ReduceOption = "";
			m_EntityType = EntityTypeEnum.EBase;		
			m_dict=(SortedList)CovertDataRow(dr);//转换数据dr-->SortList，并存储实体数据
		}
		/// <summary>
		/// Function:用DataRow，IDictionary构造实体
		/// </summary>
		/// <param name="dr">DataRow</param><param name="rel">IDictionary</param>
		public EBase(DataRow dr,IDictionary rel)
		{
			m_IsReduce = false;
			m_ReduceOption = "";
			m_EntityType = EntityTypeEnum.EBase;		
			m_dict=(SortedList)CovertDataRowR(dr,rel);////把字典的key存入SortedList的key,再通过字典的value取得dr的值存入SortedList
		}
		/// <summary>
		/// 根据物理表构造体
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

		/// <summary> 实体名</summary>
		public EntityTypeEnum EntityType
		{
			get
			{
				return m_EntityType;
			}
		}

		/// <summary> 实体名</summary>
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

	
		
		/// <summary>实体的更新时间</summary>
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
		
		/// <summary>实体是否在数据库中是否被标志为删除状态</summary>
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

		/// <summary>是否裁剪</summary>
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

		/// <summary>剪贴要求 a|b|c</summary>
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
		/// <summary> 获得实体属性的总称 </summary>	
		public string  sField     
		{
			get { return _sField ;}
			set {_sField=value;}
		}
		
	
	/// <summary>
	/// 实体数据
	/// </summary>
	/// <returns>m_dict</returns>
		public IDictionary GetData()
		{
			return m_dict;
		}
      /// <summary>
      /// 通过字段的名称，从字典中获得对应的值
      /// </summary>
      /// <param name="name">字段名称</param>
      /// <returns>字段值</returns>
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
	/// 把字段名转化为大写，把字段和其对应的值存入字典中SortedList
	/// </summary>
	/// <param name="name">字段名称</param>
	/// <param name="propValue">字段值</param>
	/// <returns>true，false</returns>
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
		/// 在构造函数中通过传入dr类型数据构造实体
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
		/// **---把字典的key存入SortedList的key,再通过字典的value取得dr的值存入SortedList**--------
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
		/// 获得数据源DataView(dt)对象？？？？
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <param name="dict">IDictionary</param>
		/// <returns>DataView(dtT)</returns>
		public static ICollection CreateDataSource(DataTable dt,IDictionary dict) 
		{
			if ( dict == null ) return new DataView(dt);
			if ( dict.Count == 0 ) return new DataView(dt);

			DataTable dtT = new DataTable() ; 
			//--创建表列
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
			//---赋值
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
			//--得到
			return new DataView(dtT);
		}
	}
}
