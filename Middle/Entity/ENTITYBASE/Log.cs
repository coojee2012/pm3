using System;

namespace Approve.EntityBase
{
	/// <summary>
	/// Ð´ÈÕÖ¾-->qualification.log¡£
	///  Author:????
	/// Time:2004-11-16
	/// </summary>
	public class Log
	{
		public static void Write(string msg)
		{
			try
			{
				System.IO.StreamWriter sw=new System.IO.StreamWriter("c:\\qualification.log",true,System.Text.Encoding.Default);
				sw.WriteLine(msg);
				sw.Close();
			}
			catch
			{
				
			}
		}
	}
}
