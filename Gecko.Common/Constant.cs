using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Anxin.Common
{
    public class Constant
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ConnectionString"] == null ? (ConfigurationManager.ConnectionStrings["AnxindaiConnStr"] == null ? "" : ConfigurationManager.ConnectionStrings["AnxindaiConnStr"].ConnectionString) : ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static string DefaultResourceServerRoot = "http://static.anxin.com";

        public static string ResourceServerRoot
        {
            get
            {
                var url = ConfigurationManager.AppSettings["ResourceServerRoot"];
                if (string.IsNullOrEmpty(url))
                {
                    return DefaultResourceServerRoot;
                }
                else if (url == "/")  //用于测试环境，在本地部署 css, js, 配置文件等即可
                {
                    return PageUtils.CurrentHost;
                }
                else
                {
                    return url;
                }
            }
        }
        public static string DoMain {
            get {
                var url = ConfigurationManager.AppSettings["domain"];
                if (string.IsNullOrEmpty(url))
                {
                    url = "http://www.anxin.com";
                }
                return url;
            }
        }
        public static int AnxinLccpRootID
        {
            get
            {
                return 29254;
            }
        }
        /// <summary>
        /// 网贷平台 在wiki表中的 根ID
        /// </summary>
        public static int AnxinWikiRootID
        {
            get
            {
                return 1;
            }
        }
        public static int AnxinLicaiRootID
        {
            get 
            {
                return 24176;
            }
        }

        /// <summary>
        /// 银行理财 在wiki表中的 根ID
        /// </summary>
        public static int AnxinYinhangRootID
        {
            get
            {
                return 42021;
            }
        }
      
        /// <summary>
        /// 安心帮助 根类别 对应Wiki表 中的 ID
        /// </summary>
        public static int AnxinHelpRootID
        {
            get
            {
                return 3554;
            }
        }

        /// <summary>
        /// 修改为当期域名
        /// </summary>
        public static string StaticRoot
        {
            get
            {
                string url = Utils.ParseString(ConfigurationManager.AppSettings["StaticRoot"], "http://www.anxin.com/");
                return url;
            }
        }


        public static string ErrPage
        {
            get
            {
                return Utils.ParseString(ConfigurationManager.AppSettings["ErrPage"], "/err.html");
            }
        }

         

        public static string MisDomain = "http://mis.anxindai.com/";
        public static string ImgDomain = "http://img.anxin.com/";
        public static string StaticDomain = "http://static.anxin.com";
        public static string ConfigDomain = "http://config.anxin.com";
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        public static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.3) Gecko/20070309 Firefox/2.0.0.3";
        public static string Title = "-- 安心贷";
        public static int SendMsgSystemID = 101664;
        public static string systemMsgTitle = "系统信息";
        public const string bigImagePath = "/users/userHeadImage/big/";
        public const string normalImagePath = "/users/userHeadImage/normal/";
        public const string miniImagePath = "/users/userHeadImage/mini/";
        public const string defaultImagePath = "/users/userHeadImage/default/";
        public const int IntervalMonth = 1;
        public const string PostBorrow = "/Borrow/";
        public const string PostLaon = "/invest/";
        public const string InviteFriend = "/account/";
        public const string BbsUrl = "";
        public const string SessionFix = "@!!__Anxin_20160125";

        public static Regex SafeImagesExNames = new Regex(@"\.(jpg|gif|bmp|png)$", RegexOptions.IgnoreCase);
        public static Regex SafeTextExNames = new Regex(@"\.(txt|xml|ks|htm)$", RegexOptions.IgnoreCase);
        public static Regex SafeFileExNames = new Regex(@"\.(rar|doc|jpg|gif|bmp|png|docx|xls|xlsx)$", RegexOptions.IgnoreCase);

        public const string SMSrechargeCode = "SMSrechargeCode";
        public const string SMSrechargeTime = "SMSrechargeTime";

        public const string SMSemailresetCode = "SMSemailres etCode";
        public const string SMSemailresetTime = "SMSemailresetTime";

        public const string serviceCode = "x*()_&20160125";

        public const string AnxinSignKey = "anxinlianlian-axax"; 

        public static string citys = "蚌埠|池州|淮北|淮南|合肥|巢湖|阜阳|亳州|滁州|芜湖|安庆|黄山|马鞍山|宣城|六安|宿州|铜陵|北京|辖县|泉州|三明|南平|福州|龙岩|莆田|漳州|宁德|厦门|天水|庆阳|定西|金昌|酒泉|张掖|甘南藏族自治州|白银|临夏回族自治州|武威|兰州|平凉|陇南|嘉峪关|揭阳|东莞|广州|潮州|清远|佛山|韶关|中山|江门|梅州|湛江|汕头|珠海|惠州|云浮|茂名|深圳|肇庆|河源|阳江|汕尾|南宁|崇左|河池|防城港|来宾|柳州|百色|贵港|贺州|玉林|北海|梧州|钦州|桂林|黔东南苗族侗族自治州|贵阳|黔南布依族苗族自治州|六盘水|遵义|黔西南布依族苗族自治州|安顺|毕节地区|铜仁地区|辖|辖县|海口|三亚|邢台|秦皇岛|邯郸|石家庄|承德|衡水|张家口|廊坊|保定|唐山|沧州|濮阳|新乡|鹤壁|南阳|许昌|郑州|驻马店|洛阳|漯河|济源|开封|平顶山|商丘|安阳|三门峡|焦作|信阳|周口|鸡西|鹤岗|哈尔滨|佳木斯|伊春|黑河|牡丹江|双鸭山|绥化|大兴安岭地区|齐齐哈尔|大庆|七台河|荆门|荆州|咸宁|恩施土家族苗族自治州|十堰|武汉|辖区|宜昌|黄冈|随州|辖|襄樊|孝感|鄂州|黄石|娄底|益阳|岳阳|邵阳|张家界|常德|永州|郴州|怀化|长沙|湘西土家族苗族自治州|株洲|衡阳|湘潭|吉林|延边朝鲜族自治州|辽源|长春|通化|四平|白城|白山|松原|常州|南京|无锡|扬州|苏州|宿迁|徐州|泰州|淮安|南通|盐城|镇江|连云港|南昌|九江|鹰潭|景德镇|新余|宜春|萍乡|抚州|赣州|上饶|吉安|葫芦岛|辽阳|抚顺|阜新|朝阳|丹东|本溪|锦州|沈阳|大连|铁岭|营口|鞍山|盘锦|赤峰|锡林郭勒盟|通辽|乌兰察布|呼伦贝尔|乌海|阿拉善盟|兴安盟|巴彦淖尔|包头|鄂尔多斯|呼和浩特|石嘴山|中卫|固原|吴忠|银川|黄南藏族自治州|西宁|玉树藏族自治州|海北藏族自治州|海南藏族自治州|海西蒙古族藏族自治州|海东地区|果洛藏族自治州|济宁|淄博|莱芜|济南|东营|日照|临沂|滨州|泰安|烟台|潍坊|聊城|威海|枣庄|德州|青岛|菏泽|宝鸡|咸阳|汉中|延安|榆林|商洛|渭南|西安|安康|铜川|上海|辖县|阿坝藏族羌族自治州|广安|乐山|德阳|内江|宜宾|广元|攀枝花|凉山彝族自治州|雅安|巴中|南充|甘孜藏族自治州|泸州|绵阳|资阳|成都|达州|眉山|遂宁|自贡|天津|辖县|阿里地区|昌都地区|拉萨|那曲地区|山南地区|林芝地区|日喀则地区|喀什地区|塔城地区|博尔塔拉蒙古自治州|阿勒泰地区|巴音郭楞蒙古自治州|辖|哈密地区|阿克苏地区|昌吉回族自治州|伊犁哈萨克自治州|克孜勒苏柯尔克孜自治州|克拉玛依|吐鲁番地区|和田地区|乌鲁木齐|曲靖|文山壮族苗族自治州|红河哈尼族彝族自治州|昆明|临沧|思茅|怒江傈僳族自治州|丽江|保山|西双版纳傣族自治州|楚雄彝族自治州|大理白族自治州|德宏傣族景颇族自治州|昭通|玉溪|迪庆藏族自治州|衢州|舟山|丽水|金华|杭州|嘉兴|湖州|绍兴|温州|宁波|台州|重庆|辖县|太原|大同|忻州|晋中|阳泉|运城|临汾|长治|晋城|吕梁|朔州";
        public static string provinces = "黑龙江|辽宁|吉林|河北|河南|湖北|湖南|山东|山西|陕西|安徽|浙江|江苏|福建|广东|海南|四川|云南|贵州|青海|甘肃|江西|台湾|内蒙|宁夏|新疆|西藏|广西";

        /// <summary>
        /// 注册奖金
        /// </summary>
        public const decimal RegBouns = 10;
        /// <summary>
        /// 推荐奖金(投标后)    
        /// </summary>
        public const decimal RecommendBonus = 15;

        public const decimal MaxInvoteBouns = 500m;


        public static bool IsDebug = ConfigurationManager.AppSettings["IsDebug"] == "1";
        public static bool DEBUG = string.IsNullOrEmpty(ConfigurationManager.AppSettings["DEBUG"]) ? false :
                Boolean.Parse(ConfigurationManager.AppSettings["DEBUG"]);

        public static string IndexPath
        {
            get
            {
                return ConfigurationManager.AppSettings["LuceneSearch"];
            }
        }

        public static string AjaxDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["AjaxDomain"];
            }
        
        } 

        public static string GetConn(string name)
        {
 
            return ConfigurationManager.ConnectionStrings[name] == null ? "" : ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string ConnAnxin = GetConn("ConnectionString");
        public static string ConnAnxin2016 = GetConn("ConnectionStringNew");
        public static string ConnAnxinMis = GetConn("MisConnectionString");
        public static string ConnAnxinTj = GetConn("TjConnectionString");
        public static string ConnAnxinExt = GetConn("ExtConnectionString");
        public static string ConnAnxinBBS = GetConn("BBSConnectionString");
        public static string ConnAnxinApp = GetConn("AnxinApp");
        public static string ConnAnxinSpider = GetConn("SpiderServerCon");

        /// <summary>
        /// 当期cookie 所在域名
        /// </summary>
        public static string CookieDomain
        {
            get
            {
                string url = Utils.ParseString(ConfigurationManager.AppSettings["cookiedomain"], "www.anxin.com");

                try
                {
                    if (System.Web.HttpContext.Current != null && !string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Url.Authority))
                    {
                        if (System.Web.HttpContext.Current.Request.Url.Authority.IndexOf("anxindai.com") > -1)
                            url = "www.anxin.com";
                    }
                }
                catch
                { }
                return url;
            }
        }

        //积分图标最小单位
        public const int lowCount = 20;

        public const decimal LateFeePercent = 0.003m;

        //提前还款违约金
        public const decimal DeditFeePercent = 0.20m;


        private static HashSet<string> specialRegisterPhoneNumbers = InitRegisterPhoneNumber();

        /// <summary>
        /// 用于注册测试使用的手机号，不验证重复
        /// </summary>
        public static HashSet<string> SpecialRegisterPhoneNumbers
        {
            get
            {
                return specialRegisterPhoneNumbers;
            }
        }

        private static HashSet<string> InitRegisterPhoneNumber()
        {
            var numbers = new HashSet<string>();
            string settings = ConfigurationManager.AppSettings["SpecialRegisterPhoneNumbers"];
            
            if (!string.IsNullOrEmpty(settings))
            {
                var numberArray = settings.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < numberArray.Length; i++)
            	{
            	    numbers.Add(numberArray[i].Trim());
            	}     
            }

            return numbers; 
        }

        /// <summary>
        /// 最小利率
        /// </summary>
        public static string MinRate
        {
            get
            {
                return ConfigurationManager.AppSettings["MinRate"];
            }
            
        }
        /// <summary>
        /// 最大利率
        /// </summary>
        public static string MaxRate
        {
            get
            {
                return ConfigurationManager.AppSettings["MaxRate"];
            }

        }
        /// <summary>
        /// 充值入口
        /// </summary>
        public static string RechargeUrl
        {
            get
            {
                return  ConfigurationManager.AppSettings["RechargeUrl"];
            }
        }
        /// <summary>
        /// 提现入口
        /// </summary>
        public static string WithDrawUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["WithDrawUrl"];
            }
        }
        /// <summary>
        /// 手机验证码发送间隔 暂定90秒
        /// </summary>
        public static string MobileInterval
        {
            get
            {
                string val = ConfigurationManager.AppSettings["MobileInterval"];
                return string.IsNullOrEmpty(val)?"90":val;
            }

        }

        //移动端设置存管开始时间，用于处理老接口，读新库的时间
        public static DateTime MobileCunGuanBenginTime = DateTime.Parse(ConfigurationManager.AppSettings["CunGuanBeginTime"] == null ? "2016-10-01" : ConfigurationManager.AppSettings["CunGuanBeginTime"].ToString());

        //移动端设置存管v2开始状态，用于处理老接口，读新库的时间
        public static int MobileCunGuanV2OnLine = Convert.ToInt32(ConfigurationManager.AppSettings["CunGuanV2"] == null ? "0" : ConfigurationManager.AppSettings["CunGuanV2"].ToString());

    }
}