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
                else if (url == "/")  //���ڲ��Ի������ڱ��ز��� css, js, �����ļ��ȼ���
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
        /// ����ƽ̨ ��wiki���е� ��ID
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
        /// ������� ��wiki���е� ��ID
        /// </summary>
        public static int AnxinYinhangRootID
        {
            get
            {
                return 42021;
            }
        }
      
        /// <summary>
        /// ���İ��� ����� ��ӦWiki�� �е� ID
        /// </summary>
        public static int AnxinHelpRootID
        {
            get
            {
                return 3554;
            }
        }

        /// <summary>
        /// �޸�Ϊ��������
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
        public static string Title = "-- ���Ĵ�";
        public static int SendMsgSystemID = 101664;
        public static string systemMsgTitle = "ϵͳ��Ϣ";
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

        public static string citys = "����|����|����|����|�Ϸ�|����|����|����|����|�ߺ�|����|��ɽ|��ɽ|����|����|����|ͭ��|����|Ͻ��|Ȫ��|����|��ƽ|����|����|����|����|����|����|��ˮ|����|����|���|��Ȫ|��Ҵ|���ϲ���������|����|���Ļ���������|����|����|ƽ��|¤��|������|����|��ݸ|����|����|��Զ|��ɽ|�ع�|��ɽ|����|÷��|տ��|��ͷ|�麣|����|�Ƹ�|ï��|����|����|��Դ|����|��β|����|����|�ӳ�|���Ǹ�|����|����|��ɫ|���|����|����|����|����|����|����|ǭ�������嶱��������|����|ǭ�ϲ���������������|����ˮ|����|ǭ���ϲ���������������|��˳|�Ͻڵ���|ͭ�ʵ���|Ͻ|Ͻ��|����|����|��̨|�ػʵ�|����|ʯ��ׯ|�е�|��ˮ|�żҿ�|�ȷ�|����|��ɽ|����|���|����|�ױ�|����|���|֣��|פ���|����|���|��Դ|����|ƽ��ɽ|����|����|����Ͽ|����|����|�ܿ�|����|�׸�|������|��ľ˹|����|�ں�|ĵ����|˫Ѽɽ|�绯|���˰������|�������|����|��̨��|����|����|����|��ʩ����������������|ʮ��|�人|Ͻ��|�˲�|�Ƹ�|����|Ͻ|�差|Т��|����|��ʯ|¦��|����|����|����|�żҽ�|����|����|����|����|��ɳ|��������������������|����|����|��̶|����|�ӱ߳�����������|��Դ|����|ͨ��|��ƽ|�׳�|��ɽ|��ԭ|����|�Ͼ�|����|����|����|��Ǩ|����|̩��|����|��ͨ|�γ�|��|���Ƹ�|�ϲ�|�Ž�|ӥ̶|������|����|�˴�|Ƽ��|����|����|����|����|��«��|����|��˳|����|����|����|��Ϫ|����|����|����|����|Ӫ��|��ɽ|�̽�|���|���ֹ�����|ͨ��|�����첼|���ױ���|�ں�|��������|�˰���|�����׶�|��ͷ|������˹|���ͺ���|ʯ��ɽ|����|��ԭ|����|����|���ϲ���������|����|��������������|��������������|���ϲ���������|�����ɹ������������|��������|�������������|����|�Ͳ�|����|����|��Ӫ|����|����|����|̩��|��̨|Ϋ��|�ĳ�|����|��ׯ|����|�ൺ|����|����|����|����|�Ӱ�|����|����|μ��|����|����|ͭ��|�Ϻ�|Ͻ��|���Ӳ���Ǽ��������|�㰲|��ɽ|����|�ڽ�|�˱�|��Ԫ|��֦��|��ɽ����������|�Ű�|����|�ϳ�|���β���������|����|����|����|�ɶ�|����|üɽ|����|�Թ�|���|Ͻ��|�������|��������|����|��������|ɽ�ϵ���|��֥����|�տ������|��ʲ����|���ǵ���|���������ɹ�������|����̩����|���������ɹ�������|Ͻ|���ܵ���|�����յ���|��������������|���������������|�������տ¶�����������|��������|��³������|�������|��³ľ��|����|��ɽ׳������������|��ӹ���������������|����|�ٲ�|˼é|ŭ��������������|����|��ɽ|��˫���ɴ���������|��������������|�������������|�º���徰����������|��ͨ|��Ϫ|�������������|����|��ɽ|��ˮ|��|����|����|����|����|����|����|̨��|����|Ͻ��|̫ԭ|��ͬ|����|����|��Ȫ|�˳�|�ٷ�|����|����|����|˷��";
        public static string provinces = "������|����|����|�ӱ�|����|����|����|ɽ��|ɽ��|����|����|�㽭|����|����|�㶫|����|�Ĵ�|����|����|�ຣ|����|����|̨��|����|����|�½�|����|����";

        /// <summary>
        /// ע�ά��
        /// </summary>
        public const decimal RegBouns = 10;
        /// <summary>
        /// �Ƽ�����(Ͷ���)    
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
        /// ����cookie ��������
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

        //����ͼ����С��λ
        public const int lowCount = 20;

        public const decimal LateFeePercent = 0.003m;

        //��ǰ����ΥԼ��
        public const decimal DeditFeePercent = 0.20m;


        private static HashSet<string> specialRegisterPhoneNumbers = InitRegisterPhoneNumber();

        /// <summary>
        /// ����ע�����ʹ�õ��ֻ��ţ�����֤�ظ�
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
        /// ��С����
        /// </summary>
        public static string MinRate
        {
            get
            {
                return ConfigurationManager.AppSettings["MinRate"];
            }
            
        }
        /// <summary>
        /// �������
        /// </summary>
        public static string MaxRate
        {
            get
            {
                return ConfigurationManager.AppSettings["MaxRate"];
            }

        }
        /// <summary>
        /// ��ֵ���
        /// </summary>
        public static string RechargeUrl
        {
            get
            {
                return  ConfigurationManager.AppSettings["RechargeUrl"];
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public static string WithDrawUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["WithDrawUrl"];
            }
        }
        /// <summary>
        /// �ֻ���֤�뷢�ͼ�� �ݶ�90��
        /// </summary>
        public static string MobileInterval
        {
            get
            {
                string val = ConfigurationManager.AppSettings["MobileInterval"];
                return string.IsNullOrEmpty(val)?"90":val;
            }

        }

        //�ƶ������ô�ܿ�ʼʱ�䣬���ڴ����Ͻӿڣ����¿��ʱ��
        public static DateTime MobileCunGuanBenginTime = DateTime.Parse(ConfigurationManager.AppSettings["CunGuanBeginTime"] == null ? "2016-10-01" : ConfigurationManager.AppSettings["CunGuanBeginTime"].ToString());

        //�ƶ������ô��v2��ʼ״̬�����ڴ����Ͻӿڣ����¿��ʱ��
        public static int MobileCunGuanV2OnLine = Convert.ToInt32(ConfigurationManager.AppSettings["CunGuanV2"] == null ? "0" : ConfigurationManager.AppSettings["CunGuanV2"].ToString());

    }
}