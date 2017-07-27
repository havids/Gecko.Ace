using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// 用于读、写、删除、比较Session中的用户信息。
/// </summary>
public class SessionUtil
{
    private SessionUtil() { }


    //
    //  当前已登录职员的基本信息。
    //
    public static void SavaStaffSession(StaffSession ss)
    {
        System.Web.HttpContext.Current.Session["STAFF"] = ss;
    }
    public static StaffSession GetStaffSession()
    {
        object o = System.Web.HttpContext.Current.Session["STAFF"];
        //if(o == null) throw new MissSessionException("读取StaffSession失败。");
        if (o == null) return null;
        else return (o as StaffSession);
    }
    public static void RemoveStaffSession()
    {
        object o = System.Web.HttpContext.Current.Session["STAFF"];
        if (o != null) System.Web.HttpContext.Current.Session.Remove("STAFF");
    }


    //
    //  当前已登录职员所处的模块的标示。
    //
    public static void SavaModuleTag(string moduleTag)
    {
        System.Web.HttpContext.Current.Session["MODULE_TAG"] = moduleTag;
    }
    public static string GetModuleTag()
    {
        object o = System.Web.HttpContext.Current.Session["MODULE_TAG"];
        if(o == null) throw new MissSessionException("读取ModuleTag失败。");
        else return o.ToString();
    }
    public static bool CompareModuleTag(string moduleTag)
    {
        object o = System.Web.HttpContext.Current.Session["MODULE_TAG"];
        if (o == null) throw new MissSessionException("读取ModuleTag失败。");
        else return o.ToString() == moduleTag;
    }


    //
    //  当前已登录职员对当前模块拥有的肯定授权的集合。
    //
    public static void SavaGrantPermissions(ArrayList grantPermissions)
    {
        System.Web.HttpContext.Current.Session["GRANT_PERMISSIONS"] = grantPermissions;
    }
    public static ArrayList GetGrantPermissions()
    {
        object o = System.Web.HttpContext.Current.Session["GRANT_PERMISSIONS"];
        if(o == null) throw new MissSessionException("读取GrantPermissions失败。");
        else return (o as ArrayList);
    }
    public static void RemoveGrantPermissions()
    {
        object o = System.Web.HttpContext.Current.Session["GRANT_PERMISSIONS"];
        if (o != null) System.Web.HttpContext.Current.Session.Remove("GRANT_PERMISSIONS");
    }


}
