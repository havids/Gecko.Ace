using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 丢失Session异常。
/// </summary>
public class MissSessionException : Exception
{
    public MissSessionException() : base() { }
	public MissSessionException(string message) : base(message) { }
	public MissSessionException(string message, Exception inner) : base(message,inner) { }
}
