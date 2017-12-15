using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.IO;

namespace Anxin.Common
{
    public class WebRequestCallbackData
    {
        private string responseString;
        public WebRequestCallbackData()
        {
            this.Encoding = Encoding.UTF8;
        }

        public bool IsOK { get; set; }
        public HttpWebResponse Response { get; set; }

        public Encoding Encoding { get; set; }

        public string ResponseString
        {
            get
            {
                if (responseString == null)
                {
                    if (Response == null)
                    {
                        return string.Empty;
                    }
                    var sr = new StreamReader(Response.GetResponseStream(),  Encoding);
                    responseString = sr.ReadToEnd().Trim();
                }

                return responseString;
            }
        }
    }

    public delegate void HttpWebRequestAsyncCallback(IAsyncResult ar, WebRequestCallbackData data);
    public class HttpWebRequestAsync
    {
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        public static readonly int DefaultTimeOut = 1000 * 60;
        public static CookieContainer DefaultCookies = new CookieContainer();
        private static Dictionary<string, string> DefaultData = new Dictionary<string, string>();

        public static IAsyncResult HttpGet(string uri, HttpWebRequestAsyncCallback callback)
        {
            return RequestHttp(HttpHelper.HttpMethod_GET, uri, DefaultEncoding, DefaultTimeOut, DefaultCookies, DefaultData, callback);
        }

        public static IAsyncResult HttpGet(string uri, HttpWebRequestInitiator initiator, HttpWebRequestAsyncCallback callback)
        {
            return RequestHttp(HttpHelper.HttpMethod_GET, uri, DefaultEncoding, DefaultTimeOut, DefaultCookies, string.Empty, initiator, callback);
        }

        public static IAsyncResult HttpPost(string uri, HttpWebRequestAsyncCallback callback)
        {
            return RequestHttp(HttpHelper.HttpMethod_POST, uri, DefaultEncoding, DefaultTimeOut, DefaultCookies, DefaultData, callback);
        }

        public static IAsyncResult HttpPost(string uri, string body, HttpWebRequestAsyncCallback callback)
        {
            return RequestHttp(HttpHelper.HttpMethod_POST, uri, DefaultEncoding, DefaultTimeOut, DefaultCookies, body, callback);
        }

        public static IAsyncResult HttpPost(string uri, string body, HttpWebRequestInitiator initiator, HttpWebRequestAsyncCallback callback)
        {
            return RequestHttp(HttpHelper.HttpMethod_POST, uri, DefaultEncoding, DefaultTimeOut, DefaultCookies, body, initiator, callback);
        }

        public static IAsyncResult RequestHttp(string httpMethod, string uri, HttpWebRequestAsyncCallback callback)
        {
            return RequestHttp(httpMethod, uri, DefaultEncoding, DefaultTimeOut, DefaultCookies, DefaultData, callback);
        }

        public static IAsyncResult HttpPost(string uri, Dictionary<string, string> data, HttpWebRequestAsyncCallback callback)
        {
            return RequestHttp(HttpHelper.HttpMethod_POST, uri, DefaultEncoding, DefaultTimeOut, DefaultCookies, data, callback);
        }

        public static IAsyncResult RequestHttp(string httpMethod, string uri, Encoding encoding, int timeOut, CookieContainer cookies, Dictionary<string, string> data, HttpWebRequestAsyncCallback callback)
        {
            var postParams = string.Empty;
            if (httpMethod.ToLower() == HttpHelper.HttpMethod_POST.ToLower())
            {
                var paramArray = uri.Split('?');
                var postUri = paramArray[0];
                postParams = paramArray.Length > 1 ? paramArray[1] : "";
                uri = postUri;

                var buffer = new StringBuilder();
                var i = 0;
                foreach (var key in data.Keys)
                {
                    var dataValue = HttpUtility.UrlEncode(data[key]);
                    var newKey = HttpUtility.UrlEncode(key);

                    if (i > 0)
                    {
                        buffer.Append("&");
                    }

                    buffer.AppendFormat("{0}={1}", newKey, dataValue);
                    i++;
                }

                if (buffer.Length > 0)
                {
                    buffer.Append("&");
                }
                buffer.Append(postParams);
                postParams = buffer.ToString();
            }

            return RequestHttp(httpMethod, uri, encoding, timeOut, cookies, postParams, callback);
        }

        public static IAsyncResult RequestHttp(string httpMethod, string uri, Encoding encoding, int timeOut, CookieContainer cookies, string body, HttpWebRequestAsyncCallback callback)
        {
            return RequestHttp(httpMethod, uri, encoding, timeOut, cookies, body, null, callback);
        }

        public static IAsyncResult RequestHttp(string httpMethod, string uri, Encoding encoding, int timeOut, CookieContainer cookies, string body, HttpWebRequestInitiator initiator, HttpWebRequestAsyncCallback callback)
        {
            var postParams = string.Empty;

            var request = (HttpWebRequest)WebRequest.Create(uri);
            if (cookies != null) 
            {
                request.CookieContainer = cookies;
            }
            request.Timeout = timeOut;
            request.Method = httpMethod;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.1667.0 Safari/537.36 AnxinDotnetHttpClient";
            request.ContentType = "application/x-www-form-urlencoded";

            if (initiator != null)
            {
                initiator(request);
            }

            if (httpMethod.ToLower() == HttpHelper.HttpMethod_POST.ToLower())
            {
                if (request.ContentType == "application/json" || request.ContentType == "text/json")
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(body);
                        streamWriter.Close();
                    }
                }
                else
                {
                    var postData = encoding.GetBytes(body);
                    request.ContentLength = postData.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(postData, 0, postData.Length);
                        stream.Close();
                    }
                }

            }

            var asyncCallback = new AsyncCallback(ar => {
                var serverResponse = string.Empty;
                var isOK = false;
                try
                {
                    using (var response = request.EndGetResponse(ar) as HttpWebResponse)
                    {
                        var statusCode = response.StatusCode;
                        if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created || statusCode == HttpStatusCode.Accepted)
                        {
                            isOK = true;
                        }

                        callback(ar, new WebRequestCallbackData() {
                            IsOK = isOK,
                            Encoding= encoding,
                            Response = response
                        });
                    }
                }
                catch(Exception)
                {
                    callback(ar, new WebRequestCallbackData() {
                        IsOK = false,
                        Encoding = encoding,
                        Response = null,
                    });
                }
            });



            return request.BeginGetResponse(asyncCallback, string.Empty);
        }
    }
}
