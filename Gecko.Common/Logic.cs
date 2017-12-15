using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Anxin.Common
{
    public class Logic
    {
        #region 问题库模块

        public enum QuestClass
        {
            注册认证 = 1000,
            充值相关 = 1100,
            投资相关 = 1200,
            借款相关 = 1300,
            金币相关 = 1400,
            安心活动 = 1500,
            红包问题 = 1600,
            加息券问题 = 1700,
            奖金 = 1800,
            活期理财 = 1900,
            新三板 = 2000,
        }

        public static string[,] QuestSource = new string[,] {
            { "10", "电话" },
            { "20", "企业QQ" },
            { "30", "QQ群" },
            { "40", "微信" },
            { "50", "论坛" },
            { "60", "邮箱" },
            { "70", "多客服" },
            { "80", "QQ" },
            { "90", "短信" },
            { "100", "后台" },
            { "200", "外呼" },
            { "300", "环信" },
        };

        public enum QuestStatus
        {
            未处理 = 0,
            已通过 = 1,
            未通过 = -1,
            待更新 = 2,
        }

        #endregion 问题库模块

        #region BBS

        /// <summary>
        /// BBS 发帖/回复 的来源
        /// </summary>
        public enum BBSImportSource
        {
            PC = 0,
            Wap = 10,
            Android = 11,
            IOS = 12,
        }

        #endregion BBS

        #region 20161123wx 功能邀请枚举

        /// <summary>
        /// 邀请类别
        /// </summary>
        public enum RecommendPrizeType
        {
            初始记录 = 0,
            奖品记录 = 1,
        }

        /// <summary>
        /// 邀请奖品
        /// </summary>
        public enum RecommendPrize
        {
            理财红包 = 0,
            现金红包 = 1,
            抽奖红包 = 2,
            抽奖金币 = 3,
            抽奖会员 = 4,
            抽奖补签卡 = 5,
        }

        /// <summary>
        /// 邀请来源
        /// </summary>
        public enum RecommendPrizeSource
        {
            直接注册邀请 = 0,
            间接注册邀请 = 1,
            直接邀请投资 = 2,
            抽奖 = 3,
        }

        /// <summary>
        /// 邀请状态
        /// </summary>
        public enum RecommendPrizeState
        {
            未领取 = 0,
            已领取 = 1,
        }

        #endregion 20161123wx 功能邀请枚举

        public static string GetValueById(string value, string typeName)
        {
            string result = string.Empty;
            string[,] tempArray = null;
            switch (typeName.ToLower())
            {
                case "privacy":
                    tempArray = Privacy;
                    break;

                case "gender":
                    tempArray = Gender;
                    break;

                case "marrage":
                    tempArray = Marrage;
                    break;

                case "education":
                    tempArray = Education;
                    break;

                case "income":
                    tempArray = Income;
                    break;

                case "autorule":
                    tempArray = AutoRule;
                    break;

                case "securityquestion":
                    tempArray = SecurityQuestion;
                    break;

                case "loanlist":
                    tempArray = LoanList;
                    break;

                case "borrowtype":
                    //tempArray = BorrowType;
                    break;

                case "Privacys":
                    tempArray = Privacys;
                    break;

                case "authenticate":
                    tempArray = Authenticate;
                    break;

                case "houseowen":
                    tempArray = houseOwen;
                    break;

                case "isornoarray":
                    tempArray = IsOrNoArray;
                    break;

                case "relations":
                    tempArray = Relations;
                    break;

                case "workingyears":
                    tempArray = WorkingYears;
                    break;
            }
            for (int i = 0; i < tempArray.GetLength(0); i++)
            {
                if (value == tempArray[i, 0])
                {
                    result = tempArray[i, 1];
                    break;
                }
            }
            return result;
        }

        public static string GetValueById(string value, string[,] arrry)
        {
            string result = string.Empty;
            for (int i = 0; i < arrry.GetLength(0); i++)
            {
                if (value == arrry[i, 0])
                {
                    result = arrry[i, 1];
                    break;
                }
            }
            return result;
        }

        public static Dictionary<string, string> BorrowReason = new Dictionary<string, string>
        {
            { "1","家用装修"},
            { "2","旅游消费"},
            { "3","家具家电"},
            { "4","助学进修"},
            { "5","生活消费"},
            { "6","其他消费"},
            { "7","家用装修"},
            { "8","旅游消费"},
            { "9","家具家电"},
            { "10","生活消费"},
    };

        public static string[,] IsOrNoArray = new string[,] {
            { "1", "是" },
            { "2", "否" }
        };

        public static string[,] houseOwen = new string[,] {
            { "1", "有商品房" },
            { "2", "有其他（非商品房）" },
            { "3", "无房" }
        };

        public static string[,] Authenticate = new string[,] {
              { "Email", "/other/account1.jpg" ,"邮件" },
              { "Mobile", "/other/phoneshiming.gif","手机" },
              { "IdCard", "/other/account1.png" ,"身份"},
              { "UnderLine", "/other/account11.jpg" ,"线下"},
        };

        public static string[,] Relations = new string[,] {
            { "1", "家庭成员" },
            { "2", "朋友" },
            { "3", "商业伙伴" }
        };

        public static string[,] WorkingYears = new string[,] {
            { "1", "1年以下" },
            { "2", "1-3年 " },
            { "3", "3-5年" },
            { "4", "5-10年" },
            { "5", "10年以上" }
        };

        public static string[,] Privacy = new string[,] {
            { "1", "完全公开" },
            { "2", "好友可见" },
            { "3", "完全隐藏" } };

        public static string[,] Gender = new string[,] {
            { "1", "男" },
            { "2", "女" } };

        public static string[,] Marrage = new string[,] {
            { "1", "未婚" },
            { "2", "已婚" }};

        public static string[,] Education = new string[,] {
            { "1", "高中以下" },
            { "2", "大专或本科" },
            { "3", "硕士或硕士以上" } };

        public static string[,] Income = new string[,] {
            { "1", "5000以下" },
            { "2", "5000-10000" },
            { "3", "10000-50000" },
            { "4", "50000以上" }};

        public static string[,] UploadSafeFileType = new string[,] {
            { "1", "图片" ,"\\.(jpg|png|bmp|gif)","/images/icon_image.gif"},
            { "2", "word文档","\\.(doc|docx)","/images/icon_word.gif" },
            { "3", "pdf文档" ,"\\.pdf","/images/icon_pdf.gif"},
            { "4", "excel文档" ,"\\.(xls|xlsx)","/images/icon_excel.gif"},
            { "5", "压缩文件" ,"\\.(rar)","/images/icon_image.gif"}
        };

        public static string[,] AssignmentError = new string[,] {
            { "1","您好，赎回成功，感谢您对安心贷的支持！"},
            { "2","您的申请赎回次数已经超过了单个用户每日赎回最多次数，请明日再试。"},
            { "3","您的申请赎回金额已经超过每日申请最大额度200,000元，请明日再试。"},
            { "4","您的申请赎回金额已经超过今日剩余可赎回额度{left}元，请明日再试。"},
            { "5","您好，只有流转贷才能进行债权回购，请重试。"},
            { "6","您好，尚未到债权赎回可操作时间段（每日上午十点起），请等待。"},
            { "7","您好，当前流转标不属于您的债权，请重试。"},
            { "8","您好，此债权已经赎回，请勿重复操作。"},
            { "9","您好，您的该笔投资中包含了奖金，尚未开放含奖金投标的债权回购。"},
            { "10","您好，您的提现密码不正确，请重试。"},
            { "11","您好，该债权在{time}后才可以进行赎回操作。"}
        };

        public static string[,] AutoRule = new string[,] {
            { "1", "安心贷担保" ,"0.00"},
            { "2", "信用积分"  ,"0.00"},
            { "3", "安心贷担保 或 信用积分"  ,"0.00"},
            { "4", "安心贷担保 并 信用积分"  ,"0.00"}};

        /// <summary>
        /// {"类型","vip类型","月数","金额"}
        /// </summary>
        public static string[,] vipAmount = new string[,] {
            //{"open","1","1","15"},
            //{"open","1","3","35"},
            //{"open","1","6","60"},
            //{"open","1","12","110"},
            //{"open","1","24","200"},
            //{"open","2","1","30"},
            //{"open","2","3","70"},
            //{"open","2","6","120"},
            //{"open","2","12","220"},
            //{"open","2","24","400"},

            //{"renew","1","2","30"},
            //{"renew","1","3","30"},
            //{"renew","1","6","30"},
            //{"renew","1","6","30"},
            //{"renew","1","12","30"},
            //{"renew","2","2","30"},
            //{"renew","2","3","30"},
            //{"renew","2","6","30"},
            //{"renew","2","12","30"},
            //{"renew","2","12","30"},

            {"open","3","1","25"},
            {"open","3","6","100"},
            {"open","3","12","150"}
        };

        /// <summary>
        /// 1开头的是收入 2 开头的是支出
        /// </summary>
        public enum IncomeAndExpenses
        {
            工资 = 100,
            奖金 = 101,
            投资分红 = 102,
            其他收入 = 103,
            房屋住宿 = 200,
            保险汽车 = 201,
            电话手机网络 = 202,
            食品 = 203,
            娱乐 = 204,
            还贷款 = 205,
            其他支出 = 206
        }

        public enum ActivityType
        {
            无 = 0,
            七夕2014 = 1,
            中秋2014 = 2,
            新年2015 = 3
        }

        public enum QixiTopicInfoState
        {
            已删除 = -1,
            审核中 = 1,
            审核失败 = 2,
            审核成功 = 3
        }

        /// <summary>
        /// 登陆平台
        /// </summary>
        public enum Platform
        {
            Wap = 1,
            Android = 2,
            Ios = 3,
            Web = 5,
            Winform = 4,
            WeiXin = 6,
            IosBibao = 7,
            AndroidBibao = 8,
            WapBibao = 9,
            WeiXinBiBao = 10,
            LianHe = 11,
            WeiBo = 12,       // 微博
            QingKe = 13, //20170307 青稞公寓 wx
        }

        public enum LoanType
        {
            用户投标 = 0,
            系统自动投标 = 1,
            手机客户端投标 = 2,
            Ios = 3,
            Wap = 4,
            Android = 5,
            WeiXin = 6,

            安心计划 = 9,
        }

        public static string[,] SecurityQuestion = new string[,]
        {
            {"0","选择一个安全提问问题"},
            {"1","您父亲的姓名是？"},
            {"2","您母亲的姓名是？"},
            {"3","您配偶的姓名是？"},
            {"4","您最后就读的学校名是？"},
            {"5","您母亲的生日是？"},
            {"6","您母亲的姓名是？"},
            {"7","您父亲的生日是？"},
            {"8","您是什么时间参加工作的？"},
            {"9","您孩子的生日是？"},
            {"10","您配偶的名字是？"},
            {"11","您配偶的生日是？"},
            {"12","您的出生地是哪里？"},
            {"13","您最喜欢什么颜色？"},
            {"14","您是什么学历？"},
            {"15","您的属相是什么的？"},
            {"16","您小学就读的是哪所学校？"},
            {"17","您最崇拜谁？"},
            {"18","您打字经常用什么输入法？"},
        };

        public static string[,] LoanList = new string[,] {
            { "1", "1个月" },
            { "2", "2个月" },
            { "3", "3个月" },
            { "4", "4个月" },
            { "5", "5个月" },
            { "6", "6个月" },
            { "7", "7个月" },
            { "8", "8个月" },
            { "9", "9个月" },
            { "10", "10个月" },
            { "11", "11个月" },
            { "12", "12个月" },
        };

        //public static string[,] BorrowType = new string[,] {
        //    { "0", "请选择" },
        //    { "1", "实体店\\工厂经营周转" },
        //    { "2", "网店经营周转" },
        //    { "5", "站内周转" },
        //    { "6", "其它目的" },
        //};

        public static string[,] Privacys = new string[,] {
            { "1001", "所有人" },
            { "1002", "我的好友/好友的好友" },
            { "1003", "我的好友" },
            { "1004", "自己" }
        };

        public static string[,] FromWhere = new string[,] {
            { "1", "搜索引擎" },
            { "2", "网络新闻" },
            { "3", "朋友告知" },
            { "4", "其他" }
        };

        public static string[,] RegPurpose = new string[,]
        {
            { "1", "投资理财" },
            { "2", "资金周转" },
        };

        public enum OfflineRechargeState
        {
            未审核 = 0,
            已到账 = 1,
            初审失败 = 2,
            初审通过 = 3,
            复审失败 = 4,
            存疑交易 = 5,
        }

        public enum DrawState
        {
            待审核 = 1,
            提现中 = 2,
            提现批准 = 3,
            审核失败 = 4,
            已取消 = 5,
            审核失败并退款 = 6,

            抵押贷提现待审核 = 21,
            抵押贷提现中 = 22,
            抵押贷提现完成 = 23,
            抵押贷提现否决 = 24,
            抵押贷提现取消 = 25,
        }

        public enum KeyValueType
        {
            预留信息 = 1,
            手机号码当日发送短信次数 = 2,
            当日赎回限额 = 3,

            信用贷冻结金额比例 = 4,

            /// <summary>
            ///
            /// </summary>
            身份证验证次数 = 5,

            /// <summary>
            /// borrow.id 和 diyaapply.id 关联
            /// </summary>
            抵押借款编号关联 = 6,

            /// <summary>
            /// IP及用户名
            /// </summary>
            IP获取重置邮件次数 = 7,

            /// <summary>
            ///
            /// </summary>
            网贷新闻类别关联 = 8,

            /// <summary>
            ///
            /// </summary>
            邮件证验证次数 = 9,

            每月风险拨备金 = 10,

            移动客户端首次安装 = 11,

            世界杯竞猜 = 12,
            世界杯竞猜奖励发放 = 13,

            原提现密码校验失败次数 = 14,
            原密码校验失败次数 = 15,

            安心赠送金币 = 20,
            安心扣除金币 = 21,

            抢投欢乐送 = 22,

            余额生息年利率 = 23,
            财富会员对应客服 = 24,
            财富会员专属客服资料 = 25,

            用户银行卡绑定记录 = 26,

            提现验证免除 = 27,

            借款标奖息 = 28,

            单日债权转让额度 = 29,

            用户01身份证 = 50,
            用户02电话 = 51,
            用户03个人资料备注 = 52,

            用户04住宅电话 = 53,
            用户05第一联系人 = 54,
            用户06第二联系人 = 55,
            用户07联系方式备注 = 56,

            用户08单位信息 = 57,
            用户09网店信息 = 58,
            用户10单位信息备注 = 59,

            用户11月收入 = 60,
            用户12月支出 = 61,
            用户13住房信息 = 62,
            用户14车辆信息 = 63,
            用户15股票信息 = 64,
            用户16财务信息备注 = 65,

            //用户17第一联保人 = 66,
            //用户18第二联保人 = 67,
            //用户19联保信息备注 = 68,

            手机验证次数 = 70,
            IP检测手机号码次数 = 71,//单IP检测手机号是否注册 限制20次

            同步账户资金用户 = 72,

            IP限制 = 201,
            关键字限制 = 202,
            用户ID限制 = 203,
            手机号码限制 = 204,
            借标否决原因 = 205,

            活动限制 = 206, //活动限制 zhaohaowei

            论坛打赏限制 = 207,//一个帖子每个人只能打赏一个红包

            是否开通了一键登录 = 208,

            用户可用资金 = 301,
            公告和论坛对应 = 311,
            公益和论坛对应 = 312,
            单一统计类型 = 411,//用作记录统计的类别 ，具体类型 看 Logic.StatRecordType
            三周年答题奖励 = 501,
            三周年梦想 = 502,
            元宵节2015 = 503,

            银行卡绑定弹出层 = 600,
            充值弹出层 = 602,
            邮箱绑定提示 = 601,
            安全设置提示 = 603,

            绑卡操作 = 990,
            取消绑卡操作 = 991, //存管v2绑卡取消绑卡单日限制

            临时推广类别 = 994,//邮件推广类别点击数量
            差额弹出层 = 995,
            首页链接配置 = 996,//我要投资，安全保障，走进安心，信息速递链接文字和地址
            节日首页GIF图 = 997,//首页头部节日动态GIF图 Thekey节日名称，TheValue=节日日期，Detail=图片全路径

            收货地址 = 998,
            浮层面板 = 999
        }

        /// <summary>
        ///  记录类别
        /// </summary>
        public enum StatRecordType
        {
            七夕送一个月会员 = 100,
            中秋送一个月会员 = 101,
            端午节2015送特权会员一个月 = 102,
        }

        public enum LimitType
        {
            注册 = 1,
            登录 = 2,
            流转标评论 = 3,
            捐助评论 = 4,
            问题反馈 = 5,
        }

        public enum CheckState
        {
            未提交 = 0,
            待审核 = 1,
            已查看 = 2,
            审核通过 = 3,
            审核否决 = -1,
            已取消 = -2
        }

        public enum LimitState
        {
            待审核 = 1,
            已审核 = 2,
            已取消 = 3,
            已查看 = 4,
        }

        public enum LimitResult
        {
            待审核 = 1,
            审核通过 = 2,
            审核否决 = 3,
            初审通过 = 4,
        }

        public enum BankList
        {
            工商银行 = 3,
            建设银行 = 4,
            农业银行 = 5,
            招商银行 = 1,
            交通银行 = 6,
            中国银行 = 2,
            光大银行 = 10,
            民生银行 = 8,
            兴业银行 = 9,
            中信银行 = 12,
            广发银行 = 13,
            浦发银行 = 7,
            平安银行 = 14,
            华夏银行 = 15,
            中国邮储银行 = 19,
            其他农村商业银行 = 37,
            其他城市商业银行 = 38,
            其他农村信用合作社 = 39,
            其他城市信用合作社 = 40,
            农村合作银行 = 41,
            村镇银行 = 42,

            深发银行 = 11,
            宁波银行 = 16,
            东亚银行 = 17,
            上海银行 = 18,
            南京银行 = 20,
            杭州银行 = 21,
            北京银行 = 22,
            北京农村商业银行 = 23,
            顺德农村信用合作社 = 24,
            浙江稠州商业银行 = 25,
            尧都信用合作联社 = 26,
            渤海银行 = 27,
            晋城市商业银行 = 28,
            温州银行 = 29,
            汉口银行 = 30,
            广州市商业银行 = 31,
            上海农村商业银行 = 32,
            广州市农村信用合作联合社 = 33,
            珠海市农村信用合作社 = 34,
            厦门银行 = 35,
            锦州银行 = 36,

            杭州联合银行 = 43,
            友利银行 = 44,
            恒丰银行 = 45,
            浙商银行 = 46,
            徽商银行 = 47,
            重庆三峡银行 = 48,
            广州银行 = 49,
            其他 = 9999,
        }

        /// <summary>
        /// 1开头的都是+钱
        /// 2开头的都是-钱
        /// 右侧备注为对应 Infos 内容
        /// </summary>
        public enum AccountType
        {
            充值 = 100,                         //PayRecord.ID
            收到还款 = 102,                    //Loan.ID
            成功借入 = 103,                    //Borrow.ID
            系统返回冻结 = 106,               //
            奖金转现金 = 104,                 //奖金转现金用于投资 LOANID: Loan.ID
            红包转现金 = 105,
            取消借出 = 109,                    //

            奖金作弊归入系统 = 110,           //作弊用户User.ID
            现金作弊归入系统 = 113,           //作弊用户User.ID
            债权作弊归入系统 = 114,          //作弊用户User.ID

            提现取消归还手续费 = 111,        //DrawRecord.ID
            本金赎回 = 112,                    //Loan.ID
            收到系统垫付还款 = 115,           //Loan.ID
            收到提前还款 = 116,               //Loan.ID
            收到债权转让折让金 = 117,        //ObjectReocrd.ID
            收到债权转让利息 = 118, //Loan.ID
            余额生息 = 119, //对应有效余额
            收到还款本金 = 132,
            收到还款利息 = 133,

            /// <summary>
            /// IncreaseInterest加息
            /// </summary>
            投资利息奖励 = 120, //Loan.Id

            /// <summary>
            /// KEYVALUE加息
            /// </summary>
            VIP会员利息奖励 = 121,//Loan.Id

            /// <summary>
            ///
            /// </summary>
            加息券利息奖励 = 122,//Loan.Id

            活期理财赎回 = 123, //payplanrecord id
            活期理财利息 = 124, //有效金额

            配置活期理财D = 125,
            配置活期理财C = 126,
            活期理财收到补充流动性资金 = 127,
            活期理财D收到C补充 = 128,

            收到定期申购资金 = 129, //dingqiRecord.Id
            收到定期利息 = 130, //dingqiRecord.Id
            收到定期本金 = 131, //dingqiRecord.Id , UserId为215607
            定期提前赎回 = 134, //dingqiRecord.Id
            定期到期赎回 = 135, //dingqiRecord.Id

            关联账号资金同步收入 = 140, //FromUserId

            /// <summary>
            /// 该枚举仅供系统用户ID:158978用户名:sysbbsbonus使用
            /// </summary>
            回收过期奖金 = 170,               //被回收User.ID //USERID : 258283 张志斌

            认证收入 = 171,                    //
            逾期普通会员利息归入系统 = 172,  //Repayment.ID //USERID : 258283 张志斌
            收到逾期本息 = 173,               //Penaltys.ID //USERID : 258283 张志斌
            收到逾期罚金 = 174,               //Penaltys.ID //USERID : 258283 张志斌

            //USERID : 258283 张志斌
            收到赎回利息手续费 = 175, //LoanId

            //收到赎回手续费 = 176,//LoanId
            收到安心宝活期收入 = 177,//LoanId

            回收投资赎回红包 = 178, //LoanId
            回收债权转让红包 = 179, //LoanId

            安心公益受捐 = 181,

            补充入账 = 197,

            现金奖励 = 198,
            共享补付 = 199,

            取现 = 200,                         //DrawRecord.ID
            成功借出 = 201,                    //Loan.ID
            如约还款 = 202,                    //Borrow.ID
            扣除借入手续费 = 205,             //Borrow.ID
            扣除借出手续费 = 210,             //Loan.ID (UserID 为 101664  的Infos 为Accountbook.ID)
            系统临时冻结 = 206,
            升级会员 = 207,                    //VIPRecord.ID
            会员续费 = 209,                    //VIPRecord.ID
            认证付费 = 211,                    //当Infos不为空时 Infos为认证用户的User.ID,sysUserId = 158978
            信用卡充值手续费 = 212,           //PayRecord.ID
            作弊被没收 = 213,                  //
            扣除提现手续费 = 214,             //DrawRecord.ID
            还款违约扣除金额 = 218,           //Borrow.ID
            提前还款 = 219,                    //Borrow.ID
            逾期还款 = 220,                    //Penaltys.ID

            冻结信用部分借款 = 221,           //borrow.id
            扣除赎回时已支付利息 = 222,  //assignment.id

            /// <summary>
            /// 该枚举仅供系统用户ID:158978用户名:sysbbsbonus使用
            /// </summary>
            支付论坛精华帖奖金 = 215,

            短信费用 = 216,                    //UserMessage.ID
            本金赎回手续费 = 217,             //LoanAssignment.ID

            支付债权转让折让金 = 223, // ObjectReocrd.Id
            支付债权转让利息 = 224, //Loan.ID

            /// <summary>
            /// 用户编号803179支付余额生息利息
            /// </summary>
            支付余额生息 = 225, //对应有效总额,

            红包使用取消 = 226, //对于coupon.id

            活期理财购买 = 227,//payplanrecord id
            活期理财扣除补充流动性资金 = 228,
            活期理财支付利息 = 229, //对应有效总额
            活期理财C补充活期理财D = 230,
            活期理财C用户赎回支出 = 231,

            抵押贷红包支出 = 232, //Borrow.Id ,记录的UserId为对应的抵押贷用户
            抵押贷加息券加息支出 = 233, //Borrow.Id
            抵押贷标的加息支出 = 234, //Borrow.Id

            定期申购 = 235, //dingqiRecord.Id
            支付定期提前赎回 = 236,
            支付定期到期赎回 = 237,

            关联账号资金同步支出 = 240, //ToUserId

            /// <summary>
            /// 该枚举仅供系统用户ID:158978用户名:sysbbsbonus使用
            /// </summary>
            支付注册奖金 = 270,               //注册用户User.ID

            支付邀请用户奖金 = 271,           //邀请者User.ID
            支付其他奖金 = 272,               //
            支付活动奖金 = 273,               //
            支付垫付逾期本金 = 274,           //Repayment.ID //USERID : 258283 张志斌
            支付垫付逾期利息 = 275,           //Repayment.ID //USERID : 258283 张志斌
            支付绑定邮箱奖金 = 276,
            支付绑定手机奖金 = 277,
            支付实名认证奖金 = 278,
            支付绑定银行卡奖金 = 279,
            支付首次提现手续费 = 280, // DrawRecord.ID

            安心公益捐款 = 281,

            现金奖励扣回 = 296,
            补充扣款 = 297,
            共享补收 = 298,
            补扣借出款 = 299,
            转入存管账户 = 300,
        }

        /// <summary>
        /// 1开头的都是+钱
        /// 2开头的都是-钱
        /// </summary>
        public enum BounsType
        {
            注册奖励 = 100,
            推荐好友 = 101,
            作弊归入系统 = 110,
            其他奖励 = 102,
            论坛精华帖奖励 = 103,
            微信奖金 = 104,

            关注微信 = 105,
            首次充值奖励 = 106,
            绑定邮箱奖励 = 107,
            绑定手机奖励 = 108,
            实名认证奖励 = 109,
            绑定银行卡奖励 = 111,

            投标奖励第一人 = 112,
            投标奖励最后一人 = 113,
            投标奖励最多者 = 114,

            世界杯奖励 = 115,

            使用优惠券 = 116,

            余额生息 = 117,

            设置安全问题 = 118,
            虚拟体验金兑换 = 119,
            首次投标奖励 = 120,

            公测奖金 = 129,

            体验金 = 150,
            活动奖金 = 151,
            六一活动2015 = 152,

            生日会 = 199,

            用户投标 = 200,
            认证付费 = 211,
            作弊被没收 = 213,
            系统收回 = 215
        }

        public enum RelationType
        {
            家庭成员 = 1,
            朋友 = 2
        }

        /// <summary>
        /// 尚未归还为1开头
        /// 归还为2开头
        /// </summary>
        public enum AccountBookState
        {
            分批归尚未归还 = 100,
            分批归还已经归还 = 201,
            提前全部还完 = 202,
            分批中提前还完 = 203,
            坏账 = 101,
            逾期 = 102,
            系统担保还款 = 204
        }

        public enum NewsType
        {
            安心公告 = 1,//站内公告
            安心动态 = 2,
            常见问题 = 3,
            担保法规 = 4,
            法律法规 = 5,
            注册登录 = 6,
            借入借出 = 7,
            账户安全 = 8,
            其他问题 = 9,
            充值提现 = 10,
            媒体报道 = 11,
            会员奖金 = 12,
            荣誉奖项 = 13,
            风险控制 = 14,
            仿冒网站 = 15,
            政府关系 = 16,
            行业资讯 = 17,//行业资讯  old 行业新闻
            安心观点 = 18,
            安心故事 = 19,
            理财故事 = 20,//理财故事  old  社区精品
            理财要闻 = 21,//理财要闻  old 推荐阅读
            微信文章 = 22,
            今日话题 = 23,

            //原创300，子类301-399
            原创精品 = 300,

            互联网金融 = 301,
            P2P网贷 = 302,
            股票 = 303,
            基金 = 304,
            财经 = 305,
        }

        public enum EnsureCom
        {
            君安信 = 1,
            兴融 = 2,
            闽商 = 3
        }

        public enum OneTimeBack
        {
            否 = 0,
            是 = 1,
        }

        public enum RecordType
        {
            登录 = 0,
            退出 = 1,
            修改密码 = 2,
            更换图片 = 3,
            安全问题 = 4,
            资金历史明细 = 5,
            用户资料修改 = 6,
            修改提现密码 = 7,
            修改手机号码 = 8,
            修改绑定邮箱 = 9,
            手机Android操作日志 = 10,
            手机IOS操作日志 = 11,
            手机WAP操作日志 = 12,

            安心问答支持评论 = 13,
            安心问答反对评论 = 14,
            安心问答被删除 = 15,

            币宝Android操作日志 = 16,
            币宝IOS操作日志 = 17,
            币宝WAP操作日志 = 18,

            修改用户名 = 20
        }

        public enum BorrowVerify
        {
            投标中 = 0,
            时间已到 = 1,
            主动撤销 = 2,
            审核未通过 = 3,
            审核通过 = 4,
            满标待审核 = 5,
            借款成功 = 6
        }

        /// <summary>
        /// 用户操作状态
        /// </summary>
        public enum BorrowRelease
        {
            未发布 = 0,
            已发布 = 1,
            已撤销 = 2
        }

        public enum LoanState
        {
            正常 = 1,
            流标 = 2,
            撤销 = 3,
            转让 = 4,
        }

        public enum PeoPleRelation
        {
            同学 = 1101,
            亲戚 = 1102,
            同事 = 1103,
            挚交 = 1104,
            普通朋友 = 1105,
            几面之缘 = 1106,
            拍友 = 1201,
            其他 = 1202,

            关注 = 2001,
            黑名单 = 3001,
            未同意 = 4001
        }

        /// <summary>
        /// 发帖状态枚举
        /// </summary>
        public enum TopicState
        {
            正常 = 1000,
            待审核 = 1001,
            删除 = 1002,
            置顶 = 1003,
            未通过 = 1004
        };

        public enum MsgState
        {
            //站内信
            已读 = 101,

            未读 = 100,
            已处理 = 201,
            未处理 = 200,
        }

        public enum MsgDelete
        {
            未删除 = 0,
            发件人删除 = 1,
            收件人删除 = 2
        }

        public enum IntegralType
        {
            还款一千元 = 0,
            身份号认证 = 1,
            手机号认证 = 2,
            还清一笔贷款 = 3,
            身份证图片通过审核 = 4,
            社区精华帖 = 5,
            介绍好友通过实名认证 = 6,
            提供其他材料通过认证 = 7,
            成为金牌或白金会员 = 8,
            注册成为会员 = 9,

            还款逾期 = 10,
            滞纳金未缴 = 11,
        }

        public enum RaypaymentFrozeState
        {
            正常 = 1,
            失效 = 2
        }

        public enum ComplexValue
        {
            高 = 0,
            中 = 1,
            低 = 2,
        };

        public enum VipRecordType
        {
            开通 = 1,
            升级 = 2,
            续费 = 3,
            系统赠送 = 4,
            金币兑换 = 5,
            活动赠送 = 6,
            摇奖518 = 7,
            注册试用 = 8      //新注册用户试用特权会员
        }

        public enum UserType
        {
            前台用户 = 0,
            后台发标用户 = 1,
            后台投标用户 = 2,
            前台大客户用户 = 3,
            前台问答用户 = 4
        }

        public enum IsOrNo
        {
            否,
            是 = 1
        }

        /// <summary>
        /// 图片类别
        /// </summary>
        public enum UserPhotoType
        {
            头像 = 1,
            其它 = 2
        };

        public enum BorrowEnsure
        {
            无担保 = 0,
            安心贷担保 = 1,
            固定资产抵押 = 2,
            股权抵押 = 3,
            林权承包权抵押 = 4,
            商铺产权抵押_土地证抵押 = 5
        }

        public enum VIPType
        {
            受限用户 = -1,

            普通会员 = 0,
            金牌会员 = 1,
            白金会员 = 2,

            一般会员 = 4,
            特权会员 = 3,
            财富会员 = 5,

            提问马甲用户 = 10,
            运营马甲用户 = 11,
            问答抓取数据用户 = 12
        }

        /// <summary>
        /// 会员自动续费类型
        /// </summary>
        public enum AutoReNew
        {
            到期前提醒 = 0,
            自动续费 = 1,
        }

        /// <summary>
        /// 帮助分类
        /// </summary>
        public enum HelpDomType
        {
            注册登录 = 1,
            借入借出 = 2,
            资金账户 = 3,
            推广奖金 = 4,
            其他问题 = 5,
            充值提现 = 6
        }

        /// <summary>
        /// 借贷种类
        /// </summary>
        public enum BorrowKind
        {
            保本贷 = 1,
            保利贷 = 2,
            短期贷 = 3,
            快捷贷 = 4,
            长期担保贷 = 5,
            短期担保贷 = 6,
            流转担保贷 = 7,
            部分担保贷 = 8,
            周转贷 = 9,
            信用担保贷 = 10,
            抵押贷 = 11, //抵押贷A , 旧标 及 不大于3个月的标
            抵押贷B = 12,// 大于3个月的新标,  利息月结, 到期还本
            债权转让 = 13,
            新手标 = 14, //一个月的抵押贷
            债权转让2 = 15,
            分期贷 = 60,
        };

        /// <summary>
        /// 手续费类型
        /// </summary>
        public enum FeeInfoType
        {
            提现手续费 = 1,  //214
            借入手续费 = 2,  //205
            借出手续费 = 3,  //210
            会员升级费 = 4,  //207,209
            充值手续费 = 5,  //212
            认证手续费 = 6,  //211
            短信费用 = 7,    //216
            本金赎回手续费 = 8, //217
            返还赎回后利息 = 9, //222
        }

        public static string[,] BorrowTypeArray = new string[,] {
            { "全部", "0" ,"","","","100.jpg",""},
            { "保本贷", "1" ,"biao1.jpg","个月","按月还款","100.jpg",""},
            { "保利贷", "2","biao2.jpg" ,"个月","按月还款","100.jpg",""},
            { "短期贷", "3","biao3.jpg" ,"天","一次性还款","100.jpg",""},
            { "快捷贷", "4" ,"biao4.jpg","天","一次性还款","100.jpg",""},
            { "流转担保贷", "7" ,"biao7.jpg","天","一次性还款","100.jpg","liuzhuan"},
            { "长期担保贷", "5" ,"biao5.jpg","个月","按月还款","100.jpg","changdan"},
            { "短期担保贷", "6" ,"biao6.jpg","天","一次性还款","100.jpg","duandan"},
            { "部分担保贷", "8" ,"biao8.jpg","个月","按月还款","50.jpg","bufen"},
            { "周转贷", "9" ,"biao9.jpg","个月","按月还款","100.jpg","zhouzhuan"},
            { "信用担保贷", "10" ,"biao10.jpg","个月","按月还款","100.jpg","geren"},
            { "抵押贷", "11" ,"biao11.jpg","个月","一次性还款","100.jpg","diya"},
            { "抵押贷", "12" ,"biao11.jpg","个月","按月还息，到期还本","100.jpg","diya"},
            { "债权转让", "13" ,"biao13.jpg","个月","待定","100.jpg","zhaiquan"},
            { "新手标", "14" ,"biao14.jpg","个月","一次性还款","100.jpg","xinshou"},
        };

        public static string[,,] NetBankArray = new string[,,] {
        {{"gsyh"},{"ICBC-NET-B2C"}, {"工商银行"}},
        {{"nyyh"},{"ABC-NET-B2C"}, {"农业银行"}},
        {{"zgyh"},{"BOC-NET-B2C"}, {"中国银行"}},
        {{"jsyh"},{"CCB-NET-B2C"}, {"建设银行"}},
        {{"zsyh"},{"CMBCHINA-NET-B2C"}, {"招商银行"}},
        {{"jtyh"},{"BOCO-NET-B2C"}, {"交通银行"}},
        {{"ycyh"},{"POST-NET-B2C"}, {"邮政银行"}},
        {{"msyh"},{"CMBC-NET-B2C"}, {"民生银行"}},
        {{"gfyh"},{"GDB-NET-B2C"}, {"广发银行"}},
        {{"sfzyh"},{"SDB-NET-B2C"}, {"深圳发展银行"}},
        {{"pfyh"},{"SPDB-NET-B2C"}, {"浦发银行"}},
        {{"gdyh"},{"CEB-NET-B2C"}, {"光大银行"}},
        {{"zxyh"},{"ECITIC-NET-B2C"}, {"中信银行"}},
        {{"xyyh"},{"CIB-NET-B2C"}, {"兴业银行"}},
        {{"payh"},{"PINGANBANK-NET"}, {"平安银行"}},
        {{"bjyh"},{"BCCB-NET-B2C"}, {"北京银行"}},
        {{"shyh"},{"SHB-NET-B2C"}, {"上海银行"}},
        {{"zhesyh"},{"CZ-NET-B2C"}, {"浙商银行"}},
        {{"nbyh"},{"NBCB-NET-B2C"}, {"宁波银行"}},
        {{"bjnsyh"},{"BJRCB-NET-B2C"}, {"北京农业银行"}}
    };

        public static string GetNetBank(string CODE)
        {
            //string value = "";
            for (int i = 0; i < NetBankArray.GetLength(0); i++)
            {
                if (CODE == NetBankArray[i, 1, 0])
                {
                    return NetBankArray[i, 0, 0];
                }
            }
            return "";
        }

        public static string GetNetBankCode(string name)
        {
            //string value = "";
            for (int i = 0; i < NetBankArray.GetLength(0); i++)
            {
                if (name == NetBankArray[i, 0, 0])
                {
                    return NetBankArray[i, 1, 0];
                }
            }
            return "";
        }

        public static string GetNetBankCnName(string pinyin)
        {
            for (int i = 0; i < NetBankArray.GetLength(0); i++)
            {
                if (pinyin == NetBankArray[i, 0, 0])
                {
                    return NetBankArray[i, 2, 0];
                }
            }
            return "";
        }

        /// <summary>
        /// 注册来源统计
        /// </summary>
        public enum RegistSource
        {
            推荐好友 = 100,

            付费推广 = 200,
            付费邮件 = 201,
            付费搜索引擎 = 202,
            付费广告位 = 203,
            付费微博推广 = 204,
            付费友情链接 = 205,

            自然流量 = 300,
            自然搜索引擎进入 = 301,
            自然其他网址进入 = 302,
            自然键入地址 = 303,

            PC百度统计 = 400,
            m百度 = 401
        }

        public enum CommentType
        {
            流转标评论 = 0,
            捐助评论 = 1,
            借款者评论回复 = 2,
        }

        /// <summary>
        /// 信息通知设置类型枚举
        /// </summary>
        public enum SettingInformType
        {
            短信验证码 = 0,
            注册成功 = 19,
            登陆提醒短信 = 28,
            完成安全验证 = 20,
            修改密码 = 1,
            修改提现密码 = 29,
            重置安全问题 = 21,
            修改手机号 = 22,
            修改邮箱地址 = 23,
            修改银行帐号 = 2,
            升级会员 = 3,
            会员延期 = 24,
            线上充值完成 = 4,

            // 线下充值完成 = 5,
            资金提现 = 6,

            收到奖金 = 7,

            月度对账单 = 204,
            生日祝福 = 205,
            月度简报 = 206,
            金币转会员 = 207,

            普通短信 = 208,
            普通邮件 = 209,

            借款标发标成功 = 9,
            借款标满标 = 10,
            借款标流标 = 11,
            借入人还款提醒 = 12,
            借入人还款成功 = 25,
            借入人逾期提醒 = 26,

            自动投标设置 = 13,
            自动投标借出完成 = 27,
            借出成功 = 14,
            借出流标 = 15,
            收到还款 = 16,
            借款逾期 = 17,
            安心贷代为偿还 = 18,
            债权赎回 = 210,

            收到论坛精华帖奖金 = 201,
            新标上线提醒 = 202,
            //对账单发送提醒 = 203,

            银行卡审核通知 = 301,

            红包过期提醒 = 302,
            加息券过期提醒 = 303,

            //邀请好友相关
            提醒好友开户 = 304,

            提醒好友投资 = 305,

            //公告活动
            平台活动 = 306,

            公告通知 = 307,
            获得红包 = 308,
            礼品兑换成功 = 309,
            获得加息券 = 310,

            注册未开户提醒 = 311,
            存管未投资提醒 = 312,
        }

        public enum MsgSendType
        {
            系统消息 = 1,
            系统邮件,
            普通邮件,
            手机短信,
            营销短信,
            推送消息,
        }

        /// <summary>
        /// 用户审核资料数据
        /// </summary>
        public enum UserVerifyData
        {
            身份验证 = 1,
            邮箱验证 = 2,
            手机验证 = 3,
            营业执照验证 = 4,
            商铺实地考察 = 5,
            户口验证 = 6,
            学历验证 = 7,
            房产证验证 = 8,
            机动车行驶证验证 = 9,
            个人信用报告提交 = 10,
            个人所得税证明 = 11,
            银行流水证明 = 12,
            所在企业固定资产清单 = 13,
            所在企业财务报表 = 14,
            所在企业税务报表 = 15,
            存管开户 = 16
        };

        public enum EmailType
        {
            未知类型 = 0,
            验证邮件 = 1,
            系统邮件 = 2,
            投标邮件 = 3,
            安全验证修改邮件 = 4
        }

        public enum GenderCall
        {
            先生 = 1,
            女士 = 2
        }

        public enum CommentState
        {
            未处理 = 0,
            已被显示 = 1,

            //客服处理后变为待审核
            显示待审核 = 4,

            隐藏待审核 = 5,

            已被隐藏 = 2,
            存疑 = 3
        }

        public enum BorrowState
        {
            未完成还款 = 0,
            已完成还款 = 1,
        }

        /// <summary>
        /// Borrow.OneTimeBack
        /// </summary>
        public enum ReturnType
        {
            按月还款 = 0,
            一次性还款 = 1,
            按月付息 = 2,
        }

        /// <summary>
        /// charity.useMethod
        /// </summary>
        public enum charityUseMethod
        {
            捐款买药 = 1,
            求学 = 2,
            助医 = 3,
            安老 = 4,
            助残 = 5,
            救灾 = 6,
            济困 = 7
        }

        /// <summary>
        /// charity.area
        /// </summary>
        public enum charityArea
        {
            华东 = 1,
            华南 = 2,
            华中 = 3,
            华北 = 4,
            西北 = 5,
            西南 = 6,
            东北 = 7,
            港澳台 = 8
        }

        public enum charityState
        {
            隐藏状态 = -1,
            显示状态 = 0
        }

        public enum charityGender
        {
            男 = 1,
            女 = 2
        }

        public enum LinkPosition
        {
            首页链接 = 1,
            内页链接 = 2,
            资讯链接 = 3,
            网贷平台频道 = 4,
            研究链接 = 5,
            安心问答 = 6,
            安心城市 = 7,
            理财栏目 = 8,
            理财产品 = 9,
            银行理财 = 10,
            基金 = 11,
            股票 = 12,
            理财聚合 = 13,
            热门标签 = 14,
            贷款栏目 = 16,
            贷款热门标签 = 17,
            安心计算器 = 15,
            信托 = 18,
            贵金属 = 19,
            安心app = 20,
            保险理财 = 21
        }

        public enum MLinkPosition
        {
            M站安心问答 = 10,
            M站计算器 = 11,
            M站贷款栏目 = 12,
            M站贷款热门标签 = 13,
        }

        public enum LinkType
        {
            普通链接 = 1,
            交换链接 = 2,
            付费链接 = 3,
            栏目链接 = 4
        }

        public enum UserState
        {
            正常 = 0,
            冻结 = 2,
            注销 = 3,
            锁定一天 = 4,
            锁定一周 = 5
        }

        public enum charityEducation
        {
            高中以下 = 1,
            大专或本科 = 2,
            硕士或硕士以上 = 3
        }

        public enum PriorityType
        {
            不处理 = 0,
            授信额度优先 = 1,
            投标进度优先 = 2,
        }

        public enum BorrowLoanKVType
        {
            借标总份额 = 1,
            借标当前阶段 = 2,
            投标所属阶段 = 3,
            借标担保率 = 4,
        }

        public enum PenaltyState
        {
            未还清 = 0,
            已还清 = 1,
        }

        public enum PrepaymentState
        {
            未还清 = 0,
            已还清 = 1,
        }

        public enum DiyaFileType
        {
            身份证 = 41,
            房产证,
            公证书,
            抵押登记文件,
            其他图片
        }

        //个人上传资料的名称
        public enum FileNameType
        {
            其他 = 0,
            本人近期彩照 = 1,
            视频认证 = 2,
            身份证正反面 = 3,
            银行的征信报告 = 4,

            户口本 = 5,
            单身证明 = 6,
            结婚证 = 7,
            配偶身份证正反面 = 8,
            配偶近期彩照 = 9,
            绑定手机实名话费单 = 10,

            最高学历证明 = 11,
            技术职称认证 = 12,

            社保卡 = 13,
            社保的缴费账单 = 14,
            劳动合同 = 15,
            名片 = 16,

            信用卡 = 17,
            信用卡最近三月账单 = 18,

            房产证 = 19,
            购房合同 = 20,
            购房发票 = 21,

            驾驶证 = 22,
            行驶本 = 23,
            本人与车辆合影 = 24,
            租房合同 = 25,
            水电气缴费清单 = 26,
            有线宽带 = 27,

            工商营业执照 = 28,
            税务登记证 = 29,
            组织机构代码证 = 30,
            办公场所照片 = 31,
            个人所得税 = 32,
            企业汇算清缴 = 33,
            其它财产证明 = 34,
            收入证明 = 35,
        }

        public enum FileDeny
        {
            上传未审核 = 0,
            审核通过 = 1,
            审核失败 = 2,
        }

        /// <summary>
        /// wiki 信息状态
        /// </summary>
        public enum WikiInfoState
        {
            未上线 = 0,
            在线上 = 1,
            已删除 = -1,
            已停用 = -2,
            内部使用 = -3
        }

        /// <summary>
        /// wiki 表 中的记录类型
        /// </summary>
        public enum WikiInfoType
        {
            主题 = 1,
            新闻 = 2,
            标签 = 3,
            关键词跳转 = 4,
            移动Wap关键词跳转 = 41,
            根主题 = 5,
            友情链接 = 6
        }

        public enum UserConnectType
        {
            新浪微博 = 0,
            腾讯微博 = 1,
            QQ = 2,
            微信 = 3,
            已绑定 = 4
        }

        public static ArrayList CityKeyWords()
        {
            ArrayList al = new ArrayList();
            al.Add("银行列表");
            al.Add("小额贷款");
            al.Add("融资租赁");
            al.Add("证券");
            al.Add("期权");
            al.Add("期货公司");
            al.Add("p2p网贷公司");
            return al;
        }

        //城市列表页 赛选条件 类别
        public enum CityLeibie
        {
            p2p贷款 = 1,
            p2p理财 = 2,
            小额贷款 = 3,
            银行贷款 = 4,
            银行理财 = 5
        }

        //城市列表页 赛选条件 类型
        public enum CityLeiXing
        {
            投资理财 = 1,
            房产抵押贷款 = 2,
            无抵押信用贷款 = 3,
            房贷 = 4,
            车贷 = 5,
            信用卡 = 6
        }

        //城市列表页 赛选条件 类别的拼音
        public static Hashtable CityLeibiePinyin()
        {
            Hashtable hs = new Hashtable();
            hs.Add("daikuan", "1");
            hs.Add("licai", "2");
            hs.Add("xiaoe", "3");
            hs.Add("yinhang", "4");
            hs.Add("yinhanglicai", "5");
            return hs;
        }

        //城市列表页 赛选条件 类型的拼音
        public static Hashtable CityLeiXingPinyin()
        {
            Hashtable hs = new Hashtable();
            hs.Add("touzi", "1");
            hs.Add("fangchan", "2");
            hs.Add("wudiya", "3");
            hs.Add("fangdai", "4");
            hs.Add("chedai", "5");
            hs.Add("xinyongka", "6");
            return hs;
        }

        //城市列表页 赛选条件 类别的拼音
        public static Hashtable GetCityLeibiePinyin()
        {
            Hashtable hs = new Hashtable();
            hs.Add("1", "daikuan");
            hs.Add("2", "licai");
            hs.Add("3", "xiaoe");
            hs.Add("4", "yinhang");
            hs.Add("5", "yinhanglicai");
            return hs;
        }

        //城市列表页 赛选条件 类型的拼音
        public static Hashtable GetCityLeiXingPinyin()
        {
            Hashtable hs = new Hashtable();
            hs.Add("1", "touzi");
            hs.Add("2", "fangchan");
            hs.Add("3", "wudiya");
            hs.Add("4", "fangdai");
            hs.Add("5", "chedai");
            hs.Add("6", "xinyongka");
            return hs;
        }

        //问答，问题所属类别
        public enum AskType
        {
            全部 = 116,
            其它 = 100,
            p2p理财 = 102,
            小额贷款 = 103,
            理财产品 = 112,
            投资理财 = 106,
            银行贷款 = 104,
            银行理财 = 105,
            房贷 = 109,
            车贷 = 110,
            无抵押贷款 = 108,
            房产抵押贷款 = 107,
            p2p贷款 = 101,
            p2p网贷 = 117,
            信用卡 = 111,
            网贷平台 = 115
        }

        public static string[,] askTypeArray = new string[,]{
            {"116","全部","quanbu"},
            {"102","p2p理财","licai"},
            {"103","小额贷款","xiaoe"},
            {"112","理财产品","licaichanpin"},
            {"106","投资理财","touzi"},
            {"104","银行贷款","yinhang"},
            {"105","银行理财","yinhanglicai"},
            {"109","房贷","fangdai"},
            {"110","车贷","chedai"},
            {"108","无抵押贷款","wudiya"},
            {"107","房产抵押贷款","fangchan"},
            {"101","p2p贷款","daikuan"},
            {"117","p2p网贷","wangdai"},
            {"111","信用卡","xinyongka"},
            {"100","其它","qita"},
            {"115","网贷平台","wangdaipingtai"},
        };

        public static Hashtable getAskTypeHZ()
        {
            Hashtable hs = new Hashtable();
            hs.Add("116", "全部");
            hs.Add("102", "p2p理财");
            hs.Add("103", "小额贷款");
            hs.Add("112", "理财产品");
            hs.Add("106", "投资理财");
            hs.Add("104", "银行贷款");
            hs.Add("105", "银行理财");
            hs.Add("109", "房贷");
            hs.Add("110", "车贷");
            hs.Add("108", "无抵押贷款");
            hs.Add("107", "房产抵押贷款");
            hs.Add("101", "p2p贷款");
            hs.Add("117", "p2p网贷");
            hs.Add("111", "信用卡");
            hs.Add("100", "其它");
            hs.Add("115", "网贷平台");
            return hs;
        }

        public static Hashtable getAskTypePinyin()
        {
            Hashtable hs = new Hashtable();
            hs.Add("116", "quanbu");
            hs.Add("102", "licai");
            hs.Add("103", "xiaoe");
            hs.Add("112", "licaichanpin");
            hs.Add("106", "touzi");
            hs.Add("104", "yinhang");
            hs.Add("105", "yinhanglicai");
            hs.Add("109", "fangdai");
            hs.Add("110", "chedai");
            hs.Add("108", "wudiya");
            hs.Add("107", "fangchan");
            hs.Add("101", "daikuan");
            hs.Add("117", "wangdai");
            hs.Add("111", "xinyongka");
            hs.Add("100", "qita");
            hs.Add("115", "wangdaipingtai");
            return hs;
        }

       

        public static Hashtable getAskTypeID()
        {
            Hashtable hs = new Hashtable();
            hs.Add("quanbu", "116");
            hs.Add("licai", "102");
            hs.Add("xiaoe", "103");
            hs.Add("licaichanpin", "112");
            hs.Add("touzi", "106");
            hs.Add("yinhang", "104");
            hs.Add("yinhanglicai", "105");
            hs.Add("fangdai", "109");
            hs.Add("chedai", "110");
            hs.Add("wudiya", "108");
            hs.Add("fangchan", "107");
            hs.Add("daikuan", "101");
            hs.Add("wangdai", "117");
            hs.Add("xinyongka", "111");
            hs.Add("qita", "100");
            hs.Add("wangdaipingtai", "115");
            return hs;
        }

        public static List<int> GetAskBlackListUser()
        {
            List<int> blackListUser = new List<int>();
            blackListUser.Add(940169); blackListUser.Add(957526); blackListUser.Add(957261); blackListUser.Add(957245);
            blackListUser.Add(957583); blackListUser.Add(957587); blackListUser.Add(957594); blackListUser.Add(957595);
            blackListUser.Add(957598); blackListUser.Add(957601); blackListUser.Add(957602); blackListUser.Add(957603);
            blackListUser.Add(957604); blackListUser.Add(957607); blackListUser.Add(957608); blackListUser.Add(957610);
            blackListUser.Add(957611); blackListUser.Add(957612); blackListUser.Add(957613); blackListUser.Add(957616);
            blackListUser.Add(957617); blackListUser.Add(957618); blackListUser.Add(957619); blackListUser.Add(957622);
            blackListUser.Add(957623); blackListUser.Add(957624); blackListUser.Add(957625); blackListUser.Add(957626);
            blackListUser.Add(957627); blackListUser.Add(957630); blackListUser.Add(957632); blackListUser.Add(957634);
            blackListUser.Add(957635); blackListUser.Add(957638); blackListUser.Add(957639); blackListUser.Add(957641);
            blackListUser.Add(957642); blackListUser.Add(957644); blackListUser.Add(957646); blackListUser.Add(957647);
            blackListUser.Add(957648); blackListUser.Add(957649); blackListUser.Add(957651); blackListUser.Add(957652);
            blackListUser.Add(957653);
            return blackListUser;
        }

        //安心问答，state
        public enum askState
        {
            用户发布待审核 = -3,
            自动问题未上线 = -2,
            已删除 = -1,
            正常 = 0,
            自动问题已上线 = 2
        }

        //安心问答，opposecount,
        public enum askType
        {
            回答 = 0,
            问题 = 1,
            评论 = 2
        }

        public enum ObjectRecordType
        {
            债权转让折让率 = 1,
            债权购买折让率 = 2,
            债权流标 = 3,

            安心宝货币户 = 5, //Number1=当前账户金额,Number2=累计转入金额, Number3=累计赎回金额, Number4=累计投资金额, ObjectId1=156220, ObjectId2=累计收到补充C, ObjectId3=累计支出补充D
            安心宝投资户 = 6, //Number1=当前账户金额,Number2=累计转入金额, Number3=累计赎回金额, Number4=累计投资金额, ObjectId1=156223, ObjectId2=累计支出补充C, ObjectId3=累计收到补充D

            投资送金币 = 10,  //Number1=投资金额, Number2=奖励金额, Number3=repayment.id, Number4=theperiods, ObjectId1=用户编号, ObjectId2=投资编号, ObjectId3=用户等级, ObjectId4=处理状态(0未处理,1已处理)
        }

        public enum askJinbi
        {
            提问问题 = 1, //6个金币，10个上限

            操作采纳满意答案 = 2, //2个金币，20上限
            回答被选为满意答案 = 3, //30个金币，无上限
            提交回答 = 4,     // 3个金币，90上限
        }

        /// <summary>
        /// 分享平台
        /// </summary>
        public enum SharePlatform
        {
            新浪微博 = 0,
            腾讯微博 = 1,
            QQ好友 = 2,
            微信朋友圈 = 3,
            微信好友 = 4,
            微信收藏 = 5,
            QQ空间 = 6,
            豆瓣 = 7,
        }

        public enum StudyType
        {
            //100+新闻
            网贷新闻 = 101,

            众筹新闻 = 102,
            监管动态 = 103,
            互联网金融 = 104,
            第三方支付 = 105,

            //200+研究
            网贷研究院 = 201,

            网贷方向 = 202,
            人物故事 = 203,
            投资理财 = 204,
            图说网贷 = 205,

            //300+数据
            平台数据 = 301,

            平台指数 = 302,
            研究报告 = 303,
            数据月报 = 304,
            数据年报 = 305,

            //400+学习
            新手入门 = 401,

            网贷知识 = 402,
            理财攻略 = 403,
            经验交流 = 404,
            免费资料 = 405,

            //500+行业
            基金动态 = 501,

            贵金属 = 502,
            证券快报 = 503,
            担保机构 = 504,
            民间借贷 = 505,
        }

        public enum DaikuanType
        {
            //100+个人贷款
            个人贷款 = 100,

            创业贷款 = 101,
            消费贷款 = 102,
            信用贷款 = 103,
            P2P贷款 = 104,
            小额贷款 = 105,
            无抵押贷款 = 106,
            结婚贷款 = 107,
            短期周转贷款 = 108,
            应急贷款 = 109,
            求学_助学贷款 = 110,

            //200+房屋贷款
            房屋贷款 = 200,

            房屋按揭 = 201,
            房屋抵押 = 202,
            二手房贷款 = 203,
            买_购房贷款 = 204,
            公积金贷款 = 205,
            装修贷款 = 206,
            住房贷款 = 207,

            //300+汽车贷款
            汽车贷款 = 300,

            汽车消费贷款 = 301,
            汽车抵押贷款 = 302,
            买_购车贷款 = 303,
            二手车贷款 = 304,

            //400+企业贷款
            企业贷款 = 400,

            企业抵押贷款 = 401,
            企业信用贷款 = 402,
            个体户贷款 = 403,
            企业周转贷款 = 404,
            企业经营贷款 = 405,

            //500+贷款常识
            贷款常识 = 500,

            贷款流程 = 501,
            贷款条件 = 502,
            利率费用 = 503,
            贷款技巧 = 504,
            贷款申请 = 505,
            贷款案例 = 506,

            银行借款 = 600,
            交通银行 = 601,
            平安银行 = 602,
            包商银行 = 603,
            上海银行 = 604,
            重庆银行 = 605,
            广发银行 = 606,
            光大银行 = 607,
            中信银行 = 608,
            花旗银行 = 609,
            浦发银行 = 610,
            广州银行 = 611,
            招商银行 = 612,
            农业银行 = 613,
            华夏银行 = 614,
            兴业银行 = 615,
            民生银行 = 616,
            北京银行 = 617,
            建设银行 = 618,
            工商银行 = 619,
            中国银行 = 620,
            渣打银行 = 621
        }

        public static Dictionary<int, string> getStudyTypePinyin()
        {
            Dictionary<int, string> Dic = new Dictionary<int, string>();
            Dic.Add(101, "wangdai");
            Dic.Add(102, "zhongchou");
            Dic.Add(103, "jianguan");
            Dic.Add(104, "jinrong");
            Dic.Add(105, "zhifu");
            Dic.Add(201, "wangdai");
            Dic.Add(202, "fangxiang");
            Dic.Add(203, "story");
            Dic.Add(204, "touzi");
            Dic.Add(205, "photo");
            Dic.Add(301, "platform");
            Dic.Add(302, "index");
            Dic.Add(303, "report");
            Dic.Add(304, "month");
            Dic.Add(305, "year");
            Dic.Add(401, "new");
            Dic.Add(402, "knowledge");
            Dic.Add(403, "licai");
            Dic.Add(404, "jingyan");
            Dic.Add(405, "free");
            Dic.Add(501, "fund");
            Dic.Add(502, "guijinshu");
            Dic.Add(503, "stock");
            Dic.Add(504, "danbao");
            Dic.Add(505, "jiedai");
            return Dic;
        }

        public static Dictionary<int, string> getDaikuanTypePinyin()
        {
            Dictionary<int, string> Dic = new Dictionary<int, string>();
            Dic.Add(100, "geren");
            Dic.Add(101, "chuangye");
            Dic.Add(102, "xiaofei");
            Dic.Add(103, "xinyong");
            Dic.Add(104, "p2pdaikuan");
            Dic.Add(105, "xiaoe");
            Dic.Add(106, "wudiya");
            Dic.Add(107, "jiehun");
            Dic.Add(108, "zouzuan");
            Dic.Add(109, "yingji");
            Dic.Add(110, "zhuxue");
            Dic.Add(200, "fangwu");
            Dic.Add(201, "anjie");
            Dic.Add(202, "fangchan");
            Dic.Add(203, "ershoufang");
            Dic.Add(204, "maifang");
            Dic.Add(205, "gongjijin");
            Dic.Add(206, "zhuangxiu");
            Dic.Add(207, "zhufang");
            Dic.Add(300, "qiche");
            Dic.Add(301, "qichexiaofei");
            Dic.Add(302, "qichediya");
            Dic.Add(303, "maiche");
            Dic.Add(304, "ershouche");
            Dic.Add(400, "qiye");
            Dic.Add(401, "qiyediya");
            Dic.Add(402, "qiyexinyong");
            Dic.Add(403, "getihu");
            Dic.Add(404, "qiyezhouzhuan");
            Dic.Add(405, "qiyejingying");
            Dic.Add(500, "changshi");
            Dic.Add(501, "liucheng");
            Dic.Add(502, "tiaojian");
            Dic.Add(503, "lixi");
            Dic.Add(504, "jiqiao");
            Dic.Add(505, "shenqing");
            Dic.Add(506, "anli");

            Dic.Add(600, "yinhang");
            Dic.Add(601, "jiaotong");
            Dic.Add(602, "pingan");
            Dic.Add(603, "baoshang");
            Dic.Add(604, "shanghai");
            Dic.Add(605, "chongqi");
            Dic.Add(606, "guangfa");
            Dic.Add(607, "guangda");
            Dic.Add(608, "zhongxin");
            Dic.Add(609, "huaqi");
            Dic.Add(610, "pufa");
            Dic.Add(611, "guangzhou");
            Dic.Add(612, "zhaoshang");
            Dic.Add(613, "nongye");
            Dic.Add(614, "huaxia");
            Dic.Add(615, "xingye");
            Dic.Add(616, "mingsheng");
            Dic.Add(617, "beijing");
            Dic.Add(618, "jianshe");
            Dic.Add(619, "gongshang");
            Dic.Add(620, "zhongguo");
            Dic.Add(621, "zhada");
            return Dic;
        }

        public enum TopLevel
        {
            无 = 0,
            一级 = 1,
            二级 = 2,
            三级 = 3,
            四级 = 4,
            五级 = 5,
        }

        public enum VoucherType
        {
            投资奖金券 = 100,
            三周年投资奖金券 = 101,
            新年金蛋活动奖金卷 = 102,
            跨年投资券 = 103,
            新年投资券 = 104,
            元宵节投资券2015 = 105,
            元宵节猜灯谜出题投资券2015 = 106,
            元宵节猜灯谜答题投资券2015 = 107,
            抵金券 = 108,
            六一活动2015 = 109,

            活动奖励 = 199,
        }

        public enum VoucherStatus
        {
            已领取 = 0,
            已激活 = 1,
            已使用 = 2,
        }

        public enum VoucherMold
        {
            投资券 = 1,
            红包 = 2,
        }

        public enum NavigatorIndex
        {
            读取配置文件 = -1,
            无 = 0,
            首页 = 1,
            我要投资 = 2,
            安心资讯 = 3,
            问答 = 4,
            安心社区 = 5,
            走近安心 = 6,
            安心理财 = 7,
            安心贷款 = 8,
            我要借款 = 9,
            安心公益 = 10,
            信息披露 = 12,
        }

        public enum FootType
        {
            Full = 0,
            Medium = 1, //包括链接与版权
            Mini = 2,
        }

        public enum HeaderType
        {
            Full = 0,
            Medium = 1, //不包括最上的登录注册等
            Compact = 2, //使用一种不同简洁的头部
            Group = 3//组合mini+
        }

        public enum RichesApplyStatus
        {
            未审核 = 0,
            审核通过 = 1,
            审核否决 = 2
        }

        public enum IncomingLinksType
        {
            行业词 = 1,
            品牌词以及品牌下专业词 = 2,
            挖掘词 = 3,
            热点词 = 4
        }

        public enum JinbiType
        {
            站点签到 = 1,
            移动端签到 = 2,
            补发金币 = 3,
            禁言处罚 = 4,
            精华奖励 = 5,
            活动送金币 = 6,
            站点发帖 = 7,
            移动端发帖 = 8,
            站点回帖 = 9,
            移动端回帖 = 10,
            删除主题 = 11,
            删除回复 = 12,
            问答奖励 = 13,
            问答处罚 = 14,
            微信抢金奖励 = 15,
            积分商城金币兑换 = 16,
            投资奖励 = 17,

            提问审核通过 = 18,
            回答审核通过 = 19,
            采纳满意答案 = 29,
            选为满意答案 = 30,

            提问被删除 = 20,
            回答被删除 = 21,
            奖励金币 = 22,
            后台补发金币 = 23,
            扣除金币 = 24,
            大转盘送金币 = 25,
            大转盘消耗 = 26,
            撩妹2016 = 27,
            摇奖518 = 28,

            端午2016 = 33,

            欧洲杯2016 = 34,
            金币兑换会员 = 35, //存管后会员只能用金币兑换
            点赞 = 36,
            奥运2016 = 37,

            打赏 = 38,
            兑换夺宝币 = 39,
            中秋2016 = 40,
            修改中文昵称 = 41,
            优秀纯文字帖 = 42,
            优秀图文帖 = 43,
            开通存管 = 44,
            国庆2016 = 45,
            暖心2016 = 46,
            微信抽奖201610 = 47,

            精彩回复 = 48,
            万圣节2016 = 49,
            微信万圣节2016 = 50,
            周年庆2016 = 51,
            客户端好评 = 52,
            感恩节2016 = 53,
            圣诞2016抽奖 = 54,
            圣诞2016拼图 = 55,

            邀请抽奖金币 = 56, //201612 wx 功能邀请好友

            IOS签到 = 57,
            安卓签到 = 58,
            零元风暴 = 59,
            微信每日抽奖 = 60,
            财神2016 = 61,
            庙会留言拜年 = 62,
            情人节2017 = 63,
            妇女节2017 = 64,
            金币秒杀 = 65,

            获得问答称号 = 66,
            问答登录 = 67,
            回答问题 = 68,
            回答被赞 = 69,
            被选为优质答案 = 70,
            回答被收藏 = 71,
            问题被关注 = 72,
            提问被管理员删除 = 73,
            回答被管理员删除 = 74,
            评论被管理员删除 = 75,
            问答完善个人信息 = 76,
            复活节2017 = 77,
            劳动节2017 = 78,
            母亲节2017 = 79,
            安心游乐会2017 = 80,
            晒奖送金币 = 81,
            父亲节2017 = 82,
            六月666 = 83,
            建军90周年2017 = 84,
            夏日三重奏2017 = 85,
            七夕节2017 = 86,
            教师节2017 = 87,
            金秋诗词大会2017 = 88,
            国庆节2017 = 89,
            社区抽奖活动 = 90,
            御寒福利2017 = 91,
            六周年庆2017 = 92,
            年终鼓励金2017 = 93,
            //社区任务系统

            社区任务点赞 = 601,
            社区任务打赏 = 602,
            社区任务分享 = 603,
            社区任务阅读 = 604,
            社区任务发帖 = 605,
            社区任务回帖 = 606,
            社区任务发帖进阶 = 607,
            社区任务参与辩论 = 608,
            成就称号勤奋小工 = 611,
            成就称号日常能手 = 612,
            成就称号红旗标兵 = 613,
            成就称号带头模范 = 614,
            成就称号先进典型 = 615,
            成就称号五星劳模 = 616,

            成就称号群众演员 = 621,
            成就称号超级龙套 = 622,
            成就称号流量鲜肉 = 623,
            成就称号最佳主角 = 624,
            成就称号明星大咖 = 625,
            成就称号银幕巨匠 = 626,

            成就称号倔强青铜 = 631,
            成就称号秩序白银 = 632,
            成就称号荣耀黄金 = 633,
            成就称号尊贵铂金 = 634,
            成就称号永恒钻石 = 635,
            成就称号最强王者 = 636,

            新手任务修改昵称 = 641,
            新手任务修改头像 = 642,
            新手任务阅读 = 643,
            新手任务发帖 = 644,
            新手任务回帖 = 645,
        }

        /// <summary>
        /// 体验金记录类型
        /// </summary>
        public enum ExperienceRecordType
        {
            领取 = 100,
            回款 = 101,
            投资 = 200,
            回收 = 201,
            兑换 = 202,
        }

        /// <summary>
        /// 体验金获取来源
        /// </summary>
        public enum ExperienceSourceType
        {
            体验金活动 = 1,
            用户注册 = 2,
            安全认证 = 3,

            绑定银行卡及密码 = 4,
            首次投标达到金额 = 5,
            自动投标设置 = 6,
        }

        public enum VLoanState
        {
            竞标中 = 0,
            已满标 = 1,
            已回款 = 2
        }

        /// <summary>
        /// 认证类型
        /// </summary>
        public enum AuthenticationType
        {
            手机认证 = 1,
            身份认证 = 2,
            邮箱验证 = 3,
            安全问题 = 4
        }

        /// <summary>
        /// 活动奖品等级
        /// </summary>
        public enum AwardLevel
        {
            一等奖 = 1,
            二等奖 = 2,
            三等奖 = 3,
            四等奖 = 4,
            五等奖 = 5,
            六等奖 = 6,
            七等奖 = 7,
            八等奖 = 8,
            特等奖 = 9,
            阳光普照奖 = 10
        }

        /// <summary>
        /// 活动获奖条件类型
        /// </summary>
        public enum AwardConditionType
        {
            无需求 = 1,
            充值额 = 2,
            投资总额 = 3,
            注册时间 = 4
        }

        /// <summary>
        /// 活动获奖条件类型
        /// </summary>
        public enum PrizeType
        {
            虚拟 = 1,
            实物 = 2,
            安心专享 = 3,
            家用电器 = 4,
            手机数码 = 5,
            生活家居 = 6,
            汽车用品 = 7,
            图书音像 = 8,
        }

        public static Dictionary<int, string> PrizePriceSection()
        {
            Dictionary<int, string> Dic = new Dictionary<int, string>();
            Dic.Add(1, "0-5000");
            Dic.Add(2, "5001-10000");
            Dic.Add(3, "10000-20000");
            Dic.Add(4, "20001-50000");
            Dic.Add(5, "50001-以上");
            return Dic;
        }

        public enum PrizeZsType
        {
            特权会员 = 1,
            红包 = 2,
            加息券 = 3,
            碎片 = 4,
            补签卡 = 5
        }

        public enum PrizedhState
        {
            审核中 = 1,
            待发货 = 2,
            已发货 = 3,
            驳回 = 4,
            用户取消 = 5
        }

        public enum PrizeListSource
        {
            兑换 = 0,
            拼人品 = 1
        }

        public enum Zjaxlm
        {
            会议活动 = 1,
            媒体采访 = 2,
            荣誉奖项 = 3,
            安友原创 = 4,
            证照资质 = 5
        }

        public enum ImageProject
        {
            积分商城 = 1,
            栏目详情页 = 2,
            安心公益 = 3
        }

        public enum JfStoreType
        {
            轮番图片 = 1,
            商品列表 = 2,
            楼层图片 = 3
        }

        public enum axgyState
        {
            审核中 = 1,
            募捐中 = 2,
            待打款 = 3,
            已打款 = 4,
            完成 = 5
        }

        public enum WeixinHeadImgSize
        {
            Size0 = 0,
            Size46 = 46,
            Size64 = 64,
            Size96 = 96,
            Size132 = 132,
        }

        public enum InititorType
        {
            合作方 = 2,
            执行方 = 1
        }

        public enum CouponState
        {
            被回收 = -10, //特殊原因回收, 需要记录remark
            已领取 = 0,
            已使用 = 1,
            投资赎回 = 2,
            债权转让 = 3,
            转让存管系统 = 4,
            红包合并 = 5,
            红包打赏 = 6,
            红包合并使用 = 7 //给红包统计
        }

        public enum CouponSource
        {
            已打赏 = -2,
            删除 = -1,

            系统赠送 = 0,
            手机验证 = 1,
            实名验证 = 2,
            银行卡绑定 = 3,
            首次充值 = 4,
            首次投标 = 5,
            股神活动 = 6,
            盛夏活动 = 7,
            七夕2015 = 8,
            男神女神2015 = 9,
            阅兵2015 = 10,
            推荐好友 = 11,
            浓情金秋 = 12,
            金币商城 = 13,
            一秒钟2015 = 14,
            微信抢红包 = 15,
            国庆2015 = 16,
            股神活动参与 = 17,
            藏宝图活动 = 18,
            摄影大赛2015 = 19,
            双11_2015 = 20,
            双11数钱2015 = 22,
            周年庆2015 = 21,
            周年庆2015新手任务 = 23,
            微信购物免单 = 24,
            感恩回馈 = 25,
            投标带头大哥 = 26,
            投标土豪 = 27,
            投标终结者 = 28,
            关注微信 = 29,
            微信台词段子 = 30,
            双12_2015 = 31,
            财富会员生日奖励 = 32,
            双旦活动 = 33,
            圣诞2015 = 34,
            迎新年2016 = 35,
            大转盘活动 = 36,
            红包合并 = 37,
            打年兽活动 = 38,
            春节猴年猴开心 = 39,
            迎战猴年排行榜 = 40,
            春节摇奖2016 = 41,
            战队活动 = 42,
            战队活动周奖励 = 43,
            妇女节2016 = 44,
            植树节2016 = 45,
            新手理财2016A = 46,
            新手理财2016B = 47,
            战队活动1期 = 48,
            战队活动2期 = 49,
            战队活动3期 = 50,
            战队活动4期 = 51,
            乐享春天2016 = 52,
            撩妹2016 = 53,
            老炮英雄会2016A = 54,
            老炮英雄会2016B = 55,
            老炮英雄会2016C = 56,
            劳动最光荣2016 = 57,
            五月排名庆典2016 = 58,
            新手任务差额补领 = 59, //2016年5月3日 新增
            摇奖518 = 60,
            六月排行榜2016 = 61,
            粽子节 = 62,
            端午节2016 = 63,
            嘉年华 = 64,
            专栏投稿 = 65,  //2016/6/14 shikai 增
            欧洲杯2016 = 66,
            邀请好友A_2016 = 67,
            邀请好友B_2016 = 68,
            七月排行榜2016 = 69,
            开通存管奖励 = 70,
            微信刮刮卡 = 71,
            未来3天待收超过10000 = 72,
            论坛打赏 = 73,
            八月排行榜2016 = 74,
            教师节 = 75,
            九月排行榜2016 = 76,
            中秋2016 = 77,
            国庆2016 = 78,
            十月排行榜2016 = 79,
            暖心2016 = 80,
            万圣节2016 = 81,
            十一月排行榜2016 = 82,
            周年庆2016 = 83,
            周年庆2016_队员红包 = 84,
            周年庆2016_队长红包 = 85,
            感恩节2016 = 86,
            安心联萌战 = 93,
            微信抽奖201610 = 94,
            十二月排行榜2016 = 95,
            圣诞2016抽奖 = 96,
            圣诞2016拼图 = 97,

            邀请现金红包 = 98,
            邀请理财红包 = 99,
            邀请抽奖红包 = 100,//2016 12 wx 功能邀请好用
            零元风暴 = 101,
            元旦2017 = 102,
            一月排行榜2017 = 103,
            财神到2016 = 104,
            庙会留言拜年 = 105,
            情人节2017 = 106,
            妇女节2017 = 107,
            三月排行榜2017 = 108,
            植树节活动 = 109,   //20170213  微信活动
            金币秒杀 = 110,
            四月排行榜2017 = 111,
            迎春踏青 = 112,
            复活节2017 = 113,
            劳动节2017 = 114,
            市场部补发 = 115, //统计后台 市场部进行加息劵 红包补发
            五月排行榜2017 = 116,
            母亲节2017 = 117,
            六月排行榜2017 = 118,
            安心游乐会2017 = 119,
            父亲节2017 = 118,
            六月666 = 120,
            七月排行榜2017 = 121,
            夏日三重奏2017 = 122,
            八月排行榜2017 = 123,
            建军90周年2017 = 124,
            七夕节2017 = 125,
            教师节2017 = 126,
            九月排行榜2017 = 127,
            诗词大会2017 = 128,
            十月排行榜2017 = 129,
            国庆节2017 = 130,
            社区抽奖活动 = 131,
            御寒福利2017 = 132,
            重阳节2017 = 133,
            十一月排行榜2017 = 134,
            六周年庆2017 = 135,
            年终鼓励金2017 = 136,
            十二月排行榜2017 = 137,
        }

        /// <summary>
        /// 加息券来源。   damiaoer  20151008
        /// </summary>
        public enum InterestCouponSource
        {
            系统赠送 = 0,
            股神活动 = 1,
            金秋2015活动 = 2,
            盛夏2015活动 = 3,
            商城兑换 = 4,
            摄影大赛2015 = 5,
            周年庆2015新手任务 = 6,
            周年庆2015秒杀 = 7,
            双11数钱 = 7,
            问卷调查奖励 = 8,
            双12_2015 = 9,
            财富会员奖励 = 10,
            双旦活动_活动1 = 11,
            双旦活动_活动2 = 12,
            迎新年2016 = 13,
            大转盘活动 = 14,
            春节猴年猴开心 = 16,
            迎战猴年排行榜 = 17,
            战队活动 = 18,
            新手理财2016 = 19,
            乐享春天2016 = 20,
            老炮英雄会2016 = 21,
            劳动最光荣2016 = 22,
            五月排名庆典2016 = 23,
            摇奖518 = 25,
            六月排行榜2016 = 26,
            欧洲杯2016 = 27,
            七月排行榜2016 = 28,
            新手标奖励 = 29,
            微信刮刮卡 = 30,
            八月排行榜2016 = 31,
            九月排行榜2016 = 32,
            中秋2016 = 33,
            国庆2016 = 34,
            十月排行榜2016 = 35,
            暖心2016 = 36,
            万圣节2016 = 37,
            十一月排行榜2016 = 38,
            周年庆2016 = 39,
            投资奖励 = 72,  //未来3天待收超过10000
            回款奖励 = 73, //当天有回款奖励1%加息券，7天过期
            新手首投 = 74,
            微信抽奖201610 = 75,
            感恩节2016 = 76,
            十二月排行榜2016 = 77,
            圣诞2016抽奖 = 78,
            圣诞2016拼图 = 79,
            零元风暴 = 80,
            一月排行榜2017 = 81,
            庙会留言拜年 = 82,
            情人节2017 = 83,
            妇女节2017 = 84,
            三月排行榜2017 = 85,
            金币秒杀 = 86,
            四月排行榜2017 = 87,
            迎春踏青 = 88,
            复活节2017 = 89,
            劳动节2017 = 90,
            五月排行榜2017 = 91,
            母亲节2017 = 92,
            六月排行榜2017 = 91,
            安心游乐会2017 = 92,
            父亲节2017 = 93,
            六月666 = 94,
            七月排行榜2017 = 95,
            夏日三重奏2017 = 96,
            八月排行榜2017 = 97,
            建军90周年2017 = 98,
            七夕节2017 = 99,
            教师节2017 = 100,
            九月排行榜2017 = 101,
            十月排行榜2017 = 102,
            国庆节2017 = 103,
            社区抽奖活动 = 104,
            御寒福利2017 = 105,
            重阳节2017 = 106,
            十一月排行榜2017 = 107,
            六周年庆2017 = 108,
            十二月排行榜2017 = 109,
        }

        public enum PayPlanType
        {
            购买 = 100,
            赎回 = 200,
        }

        public enum PayPlanPlatform
        {
            未知 = 0,
            WEB = 1,
            WAP = 2,
            IOS = 3,
            ANDROID = 4,
        }

        public enum BankApplyState
        {
            用户主动撤销 = -2,
            审核未通过 = -1,
            审核中 = 0,
            审核通过 = 1,
        }

        public enum labelType
        {
            文章页标签 = 1,
            自定义标签 = 2,
            热门推荐 = 3
        }

        public enum LabelClass
        {
            贷款 = 0,
            wiki = 1,
            计算器 = 2,
            研究 = 3
        }

        public enum EnumDataBase
        {
            AnxinExt = 100,
            //AnxinBBS = 200,
        }

        public enum EnumDataTable
        {
            wiki = 101,
            //news = 102,
            //citycompany = 103,
            //askanswer = 104,

            //ax_topics = 201,
            //ax_post1 = 202,
        }

        /// <summary>
        /// 藏宝图类型 对应 anxinext.HuodongLog TypeID
        /// </summary>
        public enum TreasureType
        {
            /// <summary>
            /// 投资抵押获得地图碎片
            /// </summary>
            投资奖励 = 100,

            /// <summary>
            /// 邀请好友获取地图碎片
            /// </summary>
            邀请好友 = 101,

            /// <summary>
            /// 金币兑换地图碎片
            /// </summary>
            金币兑换 = 102,

            /// <summary>
            /// 星际藏宝图 1等奖 需要碎片：500
            /// </summary>
            星际藏宝图 = 200,

            /// <summary>
            /// 2等奖 需要碎片：350个
            /// NineStates：九州藏宝图，深海藏宝图，雨林藏宝图
            /// </summary>
            九州藏宝图 = 201,

            深海藏宝图 = 202,
            雨林藏宝图 = 203,

            /// <summary>
            /// 三等奖 需要碎片：200个
            /// </summary>
            沙漠藏宝图 = 204,

            天山藏宝图 = 205,
            极地藏宝图 = 206,

            /// <summary>
            /// 惠民奖 分别需要碎片：30，20，15
            /// </summary>
            一百元红包 = 207,

            五十元红包 = 208,
            二十元红包 = 209,
            十元红包 = 210
        }

        public enum ChargeFeedbackType
        {
            仅充值尝试 = 1,
            网络不畅 = 2,
            其他事情打断 = 3,
            收不到短信 = 4,
            不显示充值页面 = 5,
            其他 = 6
        }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        public enum BrowserType
        {
            IE6 = 1,
            IE7 = 2,
            IE8 = 3,
            IE9 = 4,
            IE10 = 5,
            IE11 = 6,
            Edge = 7,

            Firefox = 8,

            Chrome = 9,

            T360 = 10, //360浏览器极速模式
            T360_C = 11 //360浏览器兼容模式
        }

        public enum OrderStateType
        {
            新增 = 1,
            审核通过 = 2,
            驳回 = 3,
            已发货 = 4
        }

        public enum GoodsStateType
        {
            线下 = 1,
            线上 = 2,
            删除 = -1
        }

        public enum Anxintools
        {
            房贷计算器 = 1,
            商业贷款计算器 = 2,
            住房公积金贷款计算器 = 3,
            按揭贷款计算器 = 4,
            抵押贷款计算器 = 5,
            购房能力评估计算器 = 6,
            组合贷款计算器 = 7,
            工资计算器 = 8,
            提前还贷计算器 = 9,
            个人贷款计算器 = 10,
            个人所得税计算器 = 11,
            税费计算器 = 12,
            车贷计算器 = 13,
            车险计算器 = 14,
            利息计算器 = 15,
            信用卡分期计算器 = 16,
            等额本金还款计算器 = 17,
            等额本息还款计算器 = 18,
            整存整取计算器 = 19,
            银行贷款计算器 = 20,
            存款计算器 = 21,
            中国银行贷款计算器 = 22,
            工行贷款计算器 = 23,
            建设银行贷款计算器 = 24,
            理财计算器 = 25,
            复利计算器 = 26,
            余额宝收益计算器 = 27,
            安心贷计算器 = 28,
            五险一金计算器 = 29,
            网贷收益计算器 = 30,
            提前还款计算器 = 31,
            二手房贷款计算器 = 32,
            剩余还款计算器 = 33,
            购车税费计算器 = 34,
            汽车贷款计算器 = 35,
            活期存款利率计算器 = 36,
            定期存款利率计算器 = 37,
            贷款买车税费计算器 = 38,
            消费贷款计算器 = 39,
            助学贷款计算器 = 40,
            信用卡预借现金取款手续费计算器 = 41,
            信用卡循环利息计算器 = 42,
            信用卡超限费计算器 = 43,
            信用卡滞纳金计算器 = 44,
            信用卡溢缴款领回手续费计算器 = 45,
            年终奖个人所得税计算器 = 46,
            税后工资计算器 = 47,
            投资收益计算器 = 48,
            基金投资回报计算器 = 49,
            基金转换计算器 = 50,
            失业保险计算器 = 51,
            广州三险一金计算器 = 52,
            上海四金计算器 = 53,
            基本养老保险缴费计算器 = 54,
            住房公积金计算器 = 55,
            基本养老保险领取计算器 = 56,
            北京三险一金计算器 = 57,
            医疗保险计算器 = 58,
            子女教育金计算器 = 59,
            外汇理财产品费用计算器 = 60,
            外币兑换计算 = 61,
            信托产品预期收益率计算器 = 62,
            定期投资计算器 = 63,
            理财产品预期收益计算器 = 64,
            理财产品账面实际收益率计算器 = 65,
            借记卡取款手续费计算器 = 66,
            P2P理财收益计算器 = 67,
            债券收益计算 = 68
        }

        public enum axgyLevel
        {
            爱心人士 = 1,
            爱心义工 = 2,
            爱心达人 = 3,
            爱心天使 = 4,
            公益新星 = 5,
            公益模范 = 6,
            公益翘楚 = 7,
            慈善楷模 = 8,
            慈善明星 = 9,
            大慈善家 = 10
        }

        public enum toolsTyle
        {
            常用工具 = 1,
            买房工具 = 2,
            买车工具 = 3,
            存款工具 = 4,
            利率查询 = 5,
            贷款工具 = 6,
            信用卡工具 = 7,
            工资计算器 = 8,
            基金计算器 = 9,
            保险计算器 = 10,
            外汇工具 = 11,
            信托工具 = 12,
            理财工具 = 13
        }

        public enum AppChannel
        {
            风暴Aso = 1,
            万普 = 2,
        }

        public enum ChannelTaskState
        {
            StartTask = 1,
            Complete = 2,
        }

        /// <summary>
        /// 用户积分类型。                                            by    damiaoer  20160505
        /// 胡帅增加BBS用户积分功能。
        /// </summary>
        public enum ScoreType
        {
            问答提问 = 1,
            问答回答 = 2,
            问答采纳满意答案 = 4,
            问答选为满意答案 = 5,
            问答点赞 = 6,
            问答被赞 = 7,
            问答精彩回答 = 8,
            BBSPC签到 = 9,
            BBSPC发表主题 = 10,
            BBSPC发表回帖 = 11,
            BBS主题加精 = 12,
            BBS移动发表主题 = 13,
            BBS移动发表回帖 = 14,
            BBS移动签到 = 15
        }

        public enum PushMsgType
        {
            会员升级成功时 = 1,
            会员到期当天 = 2,
            获得红包时 = 3,
            获得加息券时 = 4,
            红包还有3天即将过期时 = 5,
            加息券还有3天即将过期时 = 6,
            红包过期当天 = 7,
            加息券过期当天 = 8,
            自动投标出借成功时 = 9,
            出借满标时 = 10,
            出借流标时 = 11,
            出借逾期时一笔 = 12,
            出借逾期时多笔 = 13,
            出借提前还款时一笔 = 14,
            出借提前还款时多笔 = 15,
            出借已经结清时 = 16,
            收到还款时一笔 = 17,
            收到还款时多笔 = 18,
            债权转让成功时 = 19,
            资金周转成功时 = 20,
            提前赎回成功时 = 21,
            新标上线时 = 22,
            活动上线时 = 23,
            有新的公告时 = 24,
            借款还有3天还款时 = 25,
            借款还款当天余额不足时 = 26,
            还款提前还款成功时 = 27,
            逾期还款时 = 28,
            借款标发标成功 = 29,
            借款标满标 = 30,
            借款标流标 = 31,
            注册成功 = 32,
            完成安全验证 = 33,
            修改密码 = 34,
            重置安全问题 = 35,
            修改手机号 = 36,
            修改邮箱地址 = 37,
            修改银行帐号 = 38,
            线上充值完成 = 39,
            线下充值完成 = 40,
            资金提现 = 41,
            收到奖金 = 42,
            自动投标设置 = 43,
            借出成功 = 44,
            安心贷代为偿付 = 45,
            登陆提醒短信 = 46,
            安心贷特权客户电子对账单 = 47,
            安心贷资讯简报 = 48,
            生日祝福 = 49,
            金币转会员 = 50,
            修改提现密码 = 51,
            普通短信 = 52,
            普通邮件 = 53,
            礼品兑换成功 = 54,
            银行卡审核通知 = 55,
            注册未开户提醒 = 56,
            存管未投资提醒 = 57,
            出借满标时单标多次 = 58
        }

        public enum PushMsgPlusType
        {
            账户动态 = 1,
            投标动态 = 2,
            借款动态 = 3,
            活动提醒 = 4,
            新标提醒 = 5
        }

        public static Hashtable GetRootName()
        {
            Hashtable hs = new Hashtable();

            hs.Add("1", "wiki");
            hs.Add("24176", "licai");
            hs.Add("29254", "lccp");
            hs.Add("42021", "yinhang");
            hs.Add("66541", "fund");
            hs.Add("103395", "gupiao");
            hs.Add("288850", "xintuo");
            hs.Add("434886", "gold");
            hs.Add("542750", "baoxian");
            hs.Add("3814", "zqzr");
            return hs;
        }

        /// <summary>
        /// 用户积分对应等级                                           by    damiaoer  20160509
        /// </summary>
        public static string[,] ScoreGroup = new string[,] {
            { "Lv1", "0-100" },
            { "Lv2", "101-220" },
            { "Lv3", "221-520" },
            { "Lv4", "521-1380" },
            { "Lv5", "1381-3800" },
            { "Lv6", "3801-11500" },
            { "Lv7", "11501-38000" },
            { "Lv8", "38001-138000" },
            { "Lv9", "138001-580000" },
            { "Lv10", "580001-99999999" }
        };

        /// <summary>
        /// 新闻状态。                                            shikai 20160520
        /// 用于安心资讯作者专栏 作者发表文章
        /// </summary>
        public enum NewsVerify
        {
            未审核 = 0,
            审核通过 = 1,
            审核不通过 = -1,
            草稿 = 2
        }

        /// <summary>
        /// 用户中心弹出层类型
        /// hxf 20160714
        /// </summary>
        public enum PopLayerType
        {
            ShowEmailTips = 0,
            ShowSafeQuestion = 1,
            PopupLayer
        }

        public enum ImageConvertType
        {
            BBSNormalThumbnail = 1,
            BBSSquareThrubnail = 2,
            BBSBigThumbnail = 3,
        }

        public enum UserAction
        {
            Login = 1,//登录
            Pay = 2,//充值人数
            Loan = 3,//投标人数
            Draw = 4,//提现人数
            FBZQ = 5,//发布债权人数
            FBSH = 6,//发布赎回人数
            FBZZ = 7//发布周转人数
        }

        //Ext库,UserActionRecord
        public enum UserActionRecordType
        {
            用户评论客户端奖励金币 = 1,
            环信用户注册 = 2,
            友盟消息推送 = 3,
            出借晒奖得金币 = 4
        }

        /// <summary>
        /// BBS 金币等级划分。
        /// </summary>
        public static string[] GoldGroup = new string[] {
             "LV1,安心百姓,0,5000",
             "LV2,安心小资,5001,15000",
             "LV3,安心小康,15001,18000",
             "LV4,安心中产,18001,28000",
             "LV5,安心富人,28001,58000",
             "LV6,安心富豪,58001,88000",
             "LV7,安心大富豪,88001,138000",
             "LV8,超级大富豪,138001,280000",
             "LV9,明星大富豪,280001,580000",
             "LV10,安心首富,580001,99999999"
        };
    }
}