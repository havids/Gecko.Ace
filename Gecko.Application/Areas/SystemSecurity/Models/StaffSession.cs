using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Gecko.Security.DTO;
using Gecko.Security.Service;
using Gecko.Security.Domain;

/// <summary>
/// 储存在Session中的已登录职员的基本信息。
/// </summary>
public class StaffSession
{
    public StaffSession(string _loginId, int _isInnerUser)
    {
        this.LoginId = _loginId;
        this.IsInnerUser = _isInnerUser;
    }

    public string LoginId;
    public int IsInnerUser;
}
