using System;

namespace Approve.EntityBase
{
	/// <summary>
	/// Function:提供DropDownList的数据源绑定
	/// Author:????
	/// Time:2004-11-16
	/// </summary>
	public interface ITypeListNo
	{
		string FId
		{
			get;
			set;
		}
		string FName
		{
			get;
			set;
		}

		string FNumber
		{
			get;
			set;
		}
		int FLevel
		{
			get;
			set;
		}
	}
}
