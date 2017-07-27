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
/// 模块访问安全性异常。
/// </summary>
public class ModuleSecurityException : Exception
{
    public ModuleSecurityException() : base() { }
	public ModuleSecurityException(string message) : base(message) { }
	public ModuleSecurityException(string message, Exception inner) : base(message,inner) { }
}
