using System;
using System.Data;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net.Sockets;
using System.Linq;

namespace Gecko.Common
{

    public delegate void HttpWebRequestInitiator(HttpWebRequest request);
    /// <summary>
    /// Http 网络资源通用类。
    /// 具有对远程网络请求的统一资源处理功能.
    /// </summary>
    public class HttpHelper
    {
        public static readonly string HttpMethod_GET = "GET";
        public static readonly string HttpMethod_POST = "POST";


        public HttpHelper()
        {
        }

        /// <summary>
        /// 判断是否是手机，返回ture，代表是手机
        /// </summary>
        /// <param name="UserAgent">Request.UserAgent</param>
        /// <returns></returns>
        public static Boolean IsMobileDevice(string UserAgent)
        {
            string[] mobileAgents = { "iphone", "android", "phone", "mobile", "wap", "netfront", "java", "opera mobi", "opera mini", "ucweb", "windows ce", "symbian", "series", "webos", "sony", "blackberry", "dopod", "nokia", "samsung", "palmsource", "xda", "pieplus", "meizu", "midp", "cldc", "motorola", "foma", "docomo", "up.browser", "up.link", "blazer", "helio", "hosin", "huawei", "novarra", "coolpad", "webos", "techfaith", "palmsource", "alcatel", "amoi", "ktouch", "nexian", "ericsson", "philips", "sagem", "wellcom", "bunjalloo", "maui", "smartphone", "iemobile", "spice", "bird", "zte-", "longcos", "pantech", "gionee", "portalmmm", "jig browser", "hiptop", "benq", "haier", "^lct", "320x320", "240x320", "176x220", "w3c ", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", "leno", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "oper", "palm", "pana", "pant", "phil", "play", "port", "prox", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-","tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-", "Googlebot-Mobile" };
            bool isMoblie = false;
            if (!string.IsNullOrEmpty(UserAgent))
            {
                for (int i = 0; i < mobileAgents.Length; i++)
                {
                    if (UserAgent.ToLower().IndexOf(mobileAgents[i]) >= 0)
                    {
                        isMoblie = true;
                        break;
                    }
                }
            }
            if (isMoblie)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static string sRefresh(string url, string ip)
        {
            //远程主机 
            string hostName = url.Replace("http://", "").Split('/')[0];
            string page = url.Replace("http://", "").Replace(hostName, "");
            IPEndPoint hostEP;
            //创建Socket 实例 
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //尝试连接 
                hostEP = new IPEndPoint(IPAddress.Parse(ip), 80);
                socket.Connect(hostEP);
            }
            catch (Exception se)
            {
                string ex = se.Message;
                //socket.Shutdown(SocketShutdown.Both); 
                //关闭Socket 
                return ex;
            }
            //发送给远程主机的请求内容串 
            string sendStr = "GET " + page + " HTTP/1.1\r\nHost: " + hostName + "\r\nConnection: Close\r\n\r\n";
            //创建bytes字节数组以转换发送串 
            byte[] bytesSendStr = new byte[1024];
            //将发送内容字符串转换成字节byte数组 
            bytesSendStr = Encoding.ASCII.GetBytes(sendStr);
            try
            {
                //向主机发送请求 
                socket.Send(bytesSendStr, bytesSendStr.Length, 0);
            }
            catch (Exception ce)
            {
                string ex = ce.Message;
                //socket.Shutdown(SocketShutdown.Both); 
                //关闭Socket 
                socket.Close();
                return ex;
            }
            //声明接收返回内容的字符串 
            string recvStr = "";
            //声明字节数组，一次接收数据的长度为1024字节 
            byte[] recvBytes = new byte[1024];
            //返回实际接收内容的字节数 
            int bytes = 0;
            //循环读取，直到接收完所有数据 
            try
            {
                while (true)
                {
                    bytes = socket.Receive(recvBytes, recvBytes.Length, 0);
                    //读取完成后退出循环 
                    if (bytes <= 0)
                        break;
                    //将读取的字节数转换为字符串 
                    recvStr += Encoding.UTF8.GetString(recvBytes, 0, bytes);
                }
            }
            catch { }
            finally
            {
                socket.Close();
            }
            return recvStr;
        }

        /// <summary>
        /// 获取远程Web资源
        /// </summary>
        /// <param name="uri">需要请求获取的Url路径</param>
        /// <returns></returns>
        public static string HttpGetHTML(string uri)
        {
            return HttpGetHTML(uri, System.Text.Encoding.UTF8);
        }

        public static string HttpGetHtml(string url, HttpWebRequestInitiator initiator)
        {
            return HttpGetHTML(url, Encoding.UTF8, new CookieContainer(), initiator);
        }

        public static string HttpGetHTML(string uri, System.Text.Encoding code)
        {
            return HttpGetHTML(uri, code, new System.Net.CookieContainer());
        }
        public static string HttpGetHTML(string uri, System.Text.Encoding code, System.Net.CookieContainer container)
        {
            return HttpGetHTML(uri, code, container, null);
        }
        public static string HttpGetHTML(string uri, System.Text.Encoding code, System.Net.CookieContainer cotainer, HttpWebRequestInitiator initiator)
        {
            if (uri == null || uri.ToLower().IndexOf("http") == -1)
                return "";
            StreamReader sr = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Headers.Add("UA-CPU", "x86");
                request.Referer = uri;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727;)";
                request.KeepAlive = false;
                request.CookieContainer = cotainer;
                request.Timeout = 20000; //设置远程页面请求超时时间

                if (initiator != null)
                {
                    initiator(request);
                }

                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    sr = new StreamReader(myResponse.GetResponseStream(), code);
                    string serverResponse = sr.ReadToEnd().Trim();
                    return serverResponse;
                }
                else
                {
                    return "失败:Status:" + myResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return "失败:ex:" + ex.ToString();
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        public static string HttpGetHTML(string uri, Encoding code, NetworkCredential nc)
        {
            if (uri == null || uri.ToLower().IndexOf("http") == -1)
                return "";
            StreamReader sr = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Headers.Add("UA-CPU", "x86");
                request.Referer = uri;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727;)";
                request.KeepAlive = false;
                request.Credentials = nc;
                request.Timeout = 20000;

                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();

                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    sr = new StreamReader(myResponse.GetResponseStream(), code);
                    string serverResponse = sr.ReadToEnd().Trim();

                    return serverResponse;
                }
                else
                {
                    return "失败:Status:" + myResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return "失败:ex:" + ex.ToString();
            }

            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        /// <summary>
        /// Http Post 资源请求
        /// </summary>
        /// <param name="uri">需要请求获取的Url路径</param>
        /// <returns></returns>
        public static string HttpPost(string uri)
        {
            return HttpPost(uri, System.Text.Encoding.Default);
        }
        public static string HttpPost(string uri, System.Text.Encoding code)
        {
            return HttpPost(uri, code, new System.Net.CookieContainer());
        }
        public static string HttpPost(string uri, System.Text.Encoding code, System.Net.CookieContainer container)
        {
            string[] ps = uri.Split('?');
            string param = ps.Length > 1 ? ps[1] : "";
            Stream stream = null;
            byte[] postData = code.GetBytes(param);
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(ps[0]);
                myRequest.CookieContainer = container;
                myRequest.Method = "POST";
                myRequest.UserAgent = "Gecko_Server";
                
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = postData.Length;
                stream = myRequest.GetRequestStream();
                stream.Write(postData, 0, postData.Length);

                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), code);
                    string rs = sr.ReadToEnd().Trim();
                    sr.Close();
                    return rs;
                }
                else
                {
                    return "失败:Status:" + myResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return "失败:ex:" + ex.ToString();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }


        public static int HttpPost(string url, string data, Encoding encoding, out string result)
        {
            return HttpPost(url, data, encoding, null, out result);
        }

        public static int HttpPost(string url, string data, Encoding encoding, HttpWebRequestInitiator initiator, out string result)
        {
            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.Method = "POST";
                webReq.UserAgent = "Gecko_Server";
                webReq.ContentType = "application/x-www-form-urlencoded;charset=GBK";

                if (initiator != null)
                {
                    initiator(webReq);
                }

                if (webReq.ContentType == "application/json" || webReq.ContentType == "text/json")
                {
                    using (var streamWriter = new StreamWriter(webReq.GetRequestStream()))
                    {
                        streamWriter.Write(data);
                        streamWriter.Close();
                    }
                }
                else
                {
                    byte[] bData = encoding.GetBytes(data);
                    webReq.ContentLength = bData.Length;
                    using (Stream PostData = webReq.GetRequestStream())
                    {
                        PostData.Write(bData, 0, bData.Length);
                        PostData.Close();
                    }
                }
                
                HttpWebResponse WebResp = (HttpWebResponse)webReq.GetResponse();
                Stream Answer = WebResp.GetResponseStream();
                result = getResult(Answer, encoding);
                return (int)WebResp.StatusCode;
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
                return 0;
            }
        }


        public static int HttpPostJson(string url, string jsonData, Encoding encoding, out string result)
        {
            try
            {
                byte[] bData = encoding.GetBytes(jsonData);
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(url);
                WebReq.Method = "POST";
                WebReq.UserAgent = "Gecko_Server";
                WebReq.ContentType = "application/json";
                WebReq.ContentLength = bData.Length;
                Stream PostData = WebReq.GetRequestStream();
                PostData.Write(bData, 0, bData.Length);
                PostData.Close();
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                Stream Answer = WebResp.GetResponseStream();
                result = getResult(Answer, encoding);
                return (int)WebResp.StatusCode;
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
                return 0;
            }
        }

        public static string HttpPostJson(string url, string data)
        {
            string result = String.Empty;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";


            // callback for handling server certificates
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;

        }

        /// <summary>
        /// 小杨 2016-06-29
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="stream"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static int HttpPostStream(string url, Encoding encoding, Stream stream, out string result)
        {
            try
            {
                byte[] bData = new byte[stream.Length];
                stream.Read(bData, 0, bData.Length);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.Method = "POST";
                webReq.UserAgent = "Gecko_Server";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.ContentLength = bData.Length;
                webReq.Timeout = 30000;
                Stream PostData = webReq.GetRequestStream();
                PostData.Write(bData, 0, bData.Length);
                PostData.Close();
                HttpWebResponse WebResp = (HttpWebResponse)webReq.GetResponse();
                Stream Answer = WebResp.GetResponseStream();
                result = getResult(Answer, encoding);
                return (int)WebResp.StatusCode;
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
                return 0;
            }
        }
        public static string HttpPost(string url, Dictionary<string, string> data)
        {
            var result = string.Empty;
            var sb = new StringBuilder();
            int i = 0;
            foreach (var key in data.Keys)
            {
                if (i > 0)
                {
                    sb.Append("&");
                    
                }
                sb.AppendFormat("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(data[key]));
                
                i++;
            }

            var dataString = sb.ToString();
            HttpPost(url, dataString, Encoding.UTF8, out result);
            return result;
        }

        public static string getResult(Stream ret, Encoding ef)
        {
            StreamReader _Answer = new StreamReader(ret, ef);
            string retStr = _Answer.ReadToEnd();
            return retStr;
        }


        /// <summary>
        /// 上传文件  可包含其它键值参数   提交文件流
        /// </summary>
        /// <param name="url">提交方式</param>
        /// <param name="encoding">编码</param>
        /// <param name="filePath">文件路径 </param>
        /// <param name="stringDict">nvc键值对</param>
        /// <param name="result">返回结果内容体</param>
        /// <returns>Response.StatusCode  通信状态码</returns>
        public static int HttpPostData(string url, Encoding encoding,
                                    string filePath, NameValueCollection stringDict, out string result)
            {

            try
            {
                string responseContent;
                var memStream = new MemoryStream();
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                // 边界符  
                var boundary = "-----------------------------" + DateTime.Now.Ticks.ToString("x");
                // 边界符  
                var beginBoundary = encoding.GetBytes("--" + boundary + "\r\n");
                var Enter = encoding.GetBytes("\r\n");
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                // 最后的结束符  
                var endBoundary = encoding.GetBytes("--" + boundary + "--\r\n");
                // 设置属性  
                webRequest.Method = "POST";
                webRequest.Timeout = 10000;
                webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                // 写入文件  
                const string filePartHeader =
                    "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                     "Content-Type: application/octet-stream\r\n\r\n";
                var header = string.Format(filePartHeader, "uploadFile", filePath);
                var headerbytes = encoding.GetBytes(header);
                memStream.Write(beginBoundary, 0, beginBoundary.Length);//-----------------------------14536141493365
                memStream.Write(headerbytes, 0, headerbytes.Length);
                var buffer = new byte[fileStream.Length];
                int bytesRead; // =0  
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }
                memStream.Write(Enter, 0, Enter.Length);
                // 写入字符串的Key  
                var stringKeyHeader = "--" + boundary +
                                       "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                       "\r\n\r\n{1}\r\n";
                foreach (byte[] formitembytes in from string key in stringDict.Keys
                                                 select string.Format(stringKeyHeader, key, stringDict[key])
                                                     into formitem
                                                 select encoding.GetBytes(formitem))
                {
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }
                // 写入最后的结束边界符  
                memStream.Write(endBoundary, 0, endBoundary.Length);
                webRequest.ContentLength = memStream.Length;
                var requestStream = webRequest.GetRequestStream();
                memStream.Position = 0;
                var tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();
                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                requestStream.Close();
                var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(),
                                                                Encoding.GetEncoding("GB2312")))
                {
                    responseContent = httpStreamReader.ReadToEnd();
                }
                fileStream.Close();
                httpWebResponse.Close();
                webRequest.Abort();
                result = responseContent;
                return (int)httpWebResponse.StatusCode;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return 0;
            }
            

        }




    }
}