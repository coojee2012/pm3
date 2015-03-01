using System;
using System.IO;

namespace RDesign.EntityBase
{
	/// <summary>
	/// ObjectClass 的摘要说明。
	/// </summary>
	public class ObjectClass
	{
		private string m_name; 
		public string m_add;
		protected int _pre;
		public int _int;
		public ObjectClass()
		{
			//
		}
		protected  string  Fuc_GetConn()
		{
			m_name="1111";
			return "";

		}
		
	}

	public class StringClass:RDesign.EntityBase.ObjectClass
	{
		private string m_name; 
		public StringClass()
		{
			//
		}
		
		protected void Fuc_GetConn2()
		{
				ObjectClass oc=new ObjectClass();
		    //	 base.
			    //oc.

				
			}
	}
}
