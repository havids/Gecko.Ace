using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Gecko.Common
{
    public static class UrlUtility
    {
        public static string ToAbsolutUrl(string urlOrPath)
        {
            if ((urlOrPath.Length == 0) || (urlOrPath[0] != '~'))
    		{
    		    return urlOrPath;
    		}

            var appPath = HttpContext.Current.Request.ApplicationPath;
    		if (urlOrPath.Length == 1)
    		{
    		    return appPath;
    		}

    		if ((urlOrPath[1] == '/') || (urlOrPath[1] == '\\'))
    		{
    		    if (appPath.Length > 1)
    		    {
    		        return (appPath + "/" + urlOrPath.Substring(2));
    		    }
    		    return ("/" + urlOrPath.Substring(2));
    		}

    		if (appPath.Length > 1)
    		{
    		    return (appPath + "/" + urlOrPath.Substring(1));
    		}

    		return (appPath + urlOrPath.Substring(1));
        }
        /// <summary>
        /// Adds the specified parameter to the Query String.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="name">Name of the parameter to add.</param>
        /// <param name="value">Value for the parameter to add.</param>
        /// <returns>Url with added parameter.</returns>
        public static Uri AddParameter(this Uri uri, string name, string value)
        {
            var ub = new UriBuilder(uri);

            // decodes urlencoded pairs from uri.Query to HttpValueCollection
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);
            httpValueCollection.Add(name, value);

            // this code block is taken from httpValueCollection.ToString() method
            // and modified so it encodes strings with HttpUtility.UrlEncode
            if (httpValueCollection.Count == 0)
                ub.Query = String.Empty;
            else
            {
                var sb = new StringBuilder();

                for (int i = 0; i < httpValueCollection.Count; i++)
                {
                    string text = httpValueCollection.GetKey(i);
                    {
                        text = HttpUtility.UrlEncode(text);

                        string val = (text != null) ? (text + "=") : string.Empty;
                        string[] vals = httpValueCollection.GetValues(i);

                        if (sb.Length > 0)
                            sb.Append('&');

                        if (vals == null || vals.Length == 0)
                            sb.Append(val);
                        else
                        {
                            if (vals.Length == 1)
                            {
                                sb.Append(val);
                                sb.Append(HttpUtility.UrlEncode(vals[0]));
                            }
                            else
                            {
                                for (int j = 0; j < vals.Length; j++)
                                {
                                    if (j > 0)
                                        sb.Append('&');

                                    sb.Append(val);
                                    sb.Append(HttpUtility.UrlEncode(vals[j]));
                                }
                            }
                        }
                    }
                }

                ub.Query = sb.ToString();
            }

            return ub.Uri;
            
        }


        public static string UrlAddParameter(string fullUrl, string paramName, string paramValue)
        {
            var uri = new Uri(fullUrl);
            return uri.AddParameter(paramName, paramValue).AbsoluteUri;
        }

    }
}
