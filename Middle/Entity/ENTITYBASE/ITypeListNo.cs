using System;

namespace Approve.EntityBase
{
	/// <summary>
	/// Function:�ṩDropDownList������Դ��
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
