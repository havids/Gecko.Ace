using System;
using System.Reflection;
using System.Configuration;
using System.Data;

//using Anole.Common.Data;

namespace Gecko.Security
{
    public class InitSecurity
    {
        //private static readonly string ConnectString = ConfigurationManager.AppSettings["CSP Connection String"];
        

        /// <summary>
        /// 初始化，包括创建表，初始化数据
        /// </summary>
        public void Initialize()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            //SQLHelper.InstallTables(ConnectString, assembly, "security.sql");
        }

        ///// <summary>
        ///// 清除所有表中的数据
        ///// </summary>
        //public override void ClearData()
        //{
        //    SQLHelper.ExecuteNonQuery(ConnectString, CommandType.Text, SQL_CLEAR_ALL, null);
        //}

        ///// <summary>
        ///// 设置初始值
        ///// </summary>
        //public override void SetDefaultData()
        //{
        //    SQLHelper.ExecuteNonQuery(ConnectString, CommandType.Text, SQL_INIT_ALL_TABLE, null);
        //}

        ///// <summary>
        ///// 清除表数据SQL脚本
        ///// </summary>
        //private const string SQL_CLEAR_ALL =
        //    "DELETE FROM Tbl_Hel_Module;" +
        //    "DELETE FROM Tbl_Hel_Function;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Help_Question;" +
        //    "DELETE FROM Tbl_Hel_Identity;";

        //private const string SQL_INIT_ALL_TABLE =
        //     "INSERT INTO Tbl_Hel_Identity (ModuleID, FunctionID,HelpQuestionID) VALUES (0, 0, 0);";
    }
}
