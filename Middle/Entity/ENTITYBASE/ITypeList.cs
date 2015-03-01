using System;

namespace Approve.EntityBase
{
	/// <summary>
	/// Function:提供DropDownList的数据源绑定
	/// Time:2004-11-16
	/// </summary>
	public interface ITypeList
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
	}
}
