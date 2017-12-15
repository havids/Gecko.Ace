using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;

namespace Anxin.Common
{
    public class PageUtils
    {
        public static void JsErrAlert(string message)
        {
            message = message.Replace("\r\n", @"\r\n");
            message = message.Replace("\"", "\\\"");
            message = message.Replace(@"\", @"\\");

            HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert(\"" + message + "\");history.back();</script>");
            HttpContext.Current.Response.End();
        }

        public static void JsAlertAndRedirect(string message, string url)
        {
            message = message.Replace("\r\n", @"\r\n");
            message = message.Replace("\"", "\\\"");
            message = message.Replace(@"\", @"\\");

            HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert(\"" + message + "\");location.href='" + url + "';</script>");
            HttpContext.Current.Response.End();
        }

        public static void JsAlert(string message)
        {
            message = message.Replace("\r\n", "\\r\\n");
            message = message.Replace("\"", "\\\"");
            message = message.Replace("\\", "\\\\");

            Page page = (Page)HttpContext.Current.Handler;
            page.ClientScript.RegisterClientScriptBlock(typeof(string), message, "<script type=\"text/javascript\">alert(\"" + message + "\");</script>");
        }

        public static void EndScript(string code)
        {
            Page page = HttpContext.Current.Handler as Page;
            page.ClientScript.RegisterStartupScript(typeof(string), "x", code, true);
        }


        public static void JsCode(string code)
        {
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">" + code + "</script>");
        }

        public static void JsClose(string info)
        {
            HttpContext.Current.Response.Write("<script type=\"text/javascript\">alert('" + info + "');window.opener=2;window.close();</script>");
        }

        public static void JsRedirect(string url)
        {
            JsCode("location.href='" + url + "'");
        }


        public static void JsRedirect(string url, string target)
        {
            JsCode(target + ".location.href='" + url + "'");
        }

        public static bool SelectListItem(ref DropDownList ddl, string val)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Value == val)
                {
                    ddl.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        public static bool SelectListItem(ref RadioButtonList rbl, string val)
        {
            for (int i = 0; i < rbl.Items.Count; i++)
            {
                if (rbl.Items[i].Value == val)
                {
                    rbl.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        public static int SelectListItem(ref CheckBoxList cbl, string val)
        {
            int c = 0;
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                string itemVal = cbl.Items[i].Value;
                string pattern = string.Format("(^{0},)|(,{0}$)|(,{0},)",itemVal);
                if (Regex.IsMatch(val,pattern) || itemVal == val)
                {
                    cbl.Items[i].Selected = true;
                    c++;
                }
            }
            return c;
        }

        // Use server relative url, which is based on hostname and strart with '/'
        // Do       /ask/res/default.js
        // Don't    res/default.js        
        public static string FullVersionedReference(string filename)
        {
            var version = GetVersionParameter(filename);

            return string.Format("{0}{1}?{2}", CurrentHost, filename, version);
        }

        /// <summary>
        /// 文件必须在本服务器上和远程服务器上都存在
        /// </summary>
        /// <param name="filename">文件必须在本服务器上和远程服务器上都存在，此路径为Server Relative Path。以 '/' 作为开始字符</param>
        /// <returns>v=xxx 形式的字符串</returns>
        public static string FullVersionedReferenceForResoureServer(string filePath)
        {
            var version = GetVersionParameter(filePath);

            return string.Format("{0}{1}?{2}", Constant.ResourceServerRoot, filePath, version);
        }

        private static string GetVersionParameter(string filename)
        {
            var context = HttpContext.Current;
            var key = string.Format("{0}_{1}_{2}", "PageUtils_GetVersionParameter", context.Request.Url.Host, filename);

            return CacheHelper.Get<string>(key, 600, () => {
                var versionTime = new DateTime(1, 1, 1);

                try
                {
                	var physicalPath = context.Server.MapPath(filename);
                
                	if (File.Exists(physicalPath))
                	{
                	    versionTime =new System.IO.FileInfo(physicalPath).LastWriteTime;
                	}
                }
                catch(Exception)
                { 
                    //如果获取不到本地的文件修改信息，则返回一样的版本
                }
                
                var version = "v=" + versionTime.ToString("yyyyMMddHHmmss");
                return version;
            });
        }

        public static string dmoain = ConfigurationManager.AppSettings["domain"];
    	public static string CurrentHost
    	{
            get
            {
                if (dmoain != null) return dmoain;
                return string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host);
            }
    	}
    }
}
