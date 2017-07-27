using System;
using System.Web;

namespace Gecko.Security.NHHelper
{
	/// <summary>
	/// NHHttpModule 的摘要说明。
	/// </summary>
	public class NHHttpModule : IHttpModule
	{
		public void Dispose(){}

		public void Init(HttpApplication context)
		{
			context.EndRequest += new EventHandler(EndRequest);
		}

		public void EndRequest(Object sender, EventArgs e)
		{
			Db.CloseSession();
		}
	}
}
