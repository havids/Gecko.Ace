using AiChou.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace Gecko.Common
{
    public static class SessionHelper
    {

        /// <summary>
        /// Sets the value in the session.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetValue(string key, object value)
        {
            string sessionName = Gecko.Common.Constant.SessionFix + Security.DecodeDec(key);
            HttpSessionState state = System.Web.HttpContext.Current.Session;
            if (state == null) return;
            state[sessionName] = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="creator">A method used to create the default, or initial value, if not found in the session.</param>
        /// <returns>The object, if found, or the return value of <paramref name="creator"/> if not.</returns>
        public static T GetValue<T>(string key, Func<T> creator)
        {
            string sessionName = Gecko.Common.Constant.SessionFix + key;

            HttpSessionState state = System.Web.HttpContext.Current.Session;
            if (state == null) return default(T);

            object result = state[sessionName];

            if (result == null)
            {
                result = creator();
                state[sessionName] = result;
            }

            return (T)result;
        }
        public static string GetValue(string key)
        {
            return SessionHelper.GetValue<String>(key, delegate() { string str = string.Empty; return str; });
        }

        public static void SetValue(HttpSessionState session, string key, object value)
        {
            string sessionName = Gecko.Common.Constant.SessionFix + key;
            if (session == null) return;
            session[sessionName] = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="creator">A method used to create the default, or initial value, if not found in the session.</param>
        /// <returns>The object, if found, or the return value of <paramref name="creator"/> if not.</returns>
        public static T GetValue<T>(HttpSessionState session, string key, Func<T> creator)
        {
            string sessionName = Gecko.Common.Constant.SessionFix + key;

            if (session == null) return default(T);

            object result = session[sessionName];

            if (result == null)
            {
                result = creator();
                session[sessionName] = result;
            }

            return (T)result;
        }

        public static string GetValue(HttpSessionState session, string key)
        {
            return SessionHelper.GetValue<String>(session, key, delegate() { string str = string.Empty; return str; });
        }
    }
}
