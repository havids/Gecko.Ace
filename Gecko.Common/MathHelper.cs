using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

namespace Gecko.Common
{
    public class MathHelper
    {

        /// <summary>
        /// 计算表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static T EvalExpression<T>(string expression)
        {
            T defaultVal = default(T);

            object instance;
            MethodInfo method;
            string className = "Expression";
            string methodName = "Compute";
            CompilerParameters p = new CompilerParameters();
            p.GenerateInMemory = true;
            CompilerResults cr = new CSharpCodeProvider().CompileAssemblyFromSource(p, string.
              Format("using System;sealed class {0}{{public object {1}(){{ return {2}; }} }}",
              className, methodName, expression));
            if (cr.Errors.Count > 0)
            {
                string msg = "Expression(\"" + expression + "\"): \n";
                foreach (CompilerError err in cr.Errors) msg += err.ToString() + "\n";
                throw new Exception(msg);
            }
            instance = cr.CompiledAssembly.CreateInstance(className);
            method = instance.GetType().GetMethod(methodName);

            var result = (T)method.Invoke(instance, null);
            if (result == null)
                return defaultVal;
            return result;
        }

        public static decimal GetTwoDot(string value)
        {
            if (value == "0" || Regex.IsMatch(value, @"^0[\d]{0,}?\.0{1,}?$"))
                return decimal.Zero;
            if (!value.Contains("."))
            {
                value = value + ".00";
            }
            if (value.Substring(value.IndexOf('.')).Length < 3)
            {
                for (int i = 0; i < (3 - value.Substring(value.IndexOf('.')).Length); i++)
                {
                    value += "0";
                }
            }
            return Convert.ToDecimal(value.Substring(0, value.IndexOf('.') + 3));
        }

        public static int GetPosition(double xVal, double[] data)
        {
            int start = 0, end = data.Length, mid;

            //   Array.Sort(data);     //data是升序数组

            if (end <= 0) return -1;
            if (xVal < data[0] || xVal > data[end - 1]) return -1;

            for (int i = start; i < end; i++)
            {
                mid = (start + end) / 2;
                if (xVal < data[mid])
                {
                    i = start;            //可不要
                    end = mid;
                }
                else if (xVal > data[mid])
                {
                    i = start;            //可不要
                    start = mid;
                }
                else    //浮点型，可能一直不会到这，但是为了防止很精度的数在内存表现形式一样
                {
                    return mid;
                }
            }

            if (end - start <= 1)   //注意，这是退出点。只要不存在相等，最后肯定卡在两数中间
            {
                return end;     //或return start，看需要
            }
            return -1;
        }
        public static decimal GetFourDot(string value)
        {
            if (value == "0" || Regex.IsMatch(value, @"^0[\d]{0,}?\.0{1,}?$"))
                return decimal.Zero;
            if (!value.Contains("."))
            {
                value = value + ".0000";
            }
            if (value.Substring(value.IndexOf('.')).Length < 5)
            {
                for (int i = 0; i < (3 - value.Substring(value.IndexOf('.')).Length); i++)
                {
                    value += "0";
                }
            }
            return Convert.ToDecimal(value.Substring(0, value.IndexOf('.') + 5));
        }


        public static string GetMoneyTwoDot(decimal value)
        {
            return value.ToString("c").Replace("¥", "").Replace("￥", "");
        }

        public static string GetTwoDot(decimal value)
        {
            var temp = value.ToString();
            if (!temp.Contains("."))
            {
                temp = temp + ".00";
            }
            if (temp.Substring(temp.IndexOf('.')).Length < 3)
            {
                for (int i = 0; i < (3 - temp.Substring(temp.IndexOf('.')).Length); i++)
                {
                    temp += "0";
                }
            }
            return temp.Substring(0, temp.IndexOf('.') + 3);
        }


        public static int GetInteger(string value)
        {
            return Convert.ToInt32(Convert.ToDouble(value));
        }


        /// <summary>
        /// 等额本息计算（每个月还多少钱）
        /// </summary>
        /// <param name="p">本金</param>
        /// <param name="r">月利率</param>
        /// <param name="n">还款期数</param>
        /// <returns></returns>
        public static decimal debxMethod(decimal p, decimal r, decimal n)
        {
            double dp = Convert.ToDouble(p);
            double dr = Convert.ToDouble(r);
            double dn = Convert.ToDouble(n);
            return Convert.ToDecimal(dp * ((dr * Math.Pow((1 + dr), dn)) / ((Math.Pow((1 + dr), dn)) - 1)));
        }

        /// <summary>
        /// 等额本息计算（总共还多少钱）
        /// </summary>
        /// <param name="p">本金</param>
        /// <param name="r">月利率</param>
        /// <param name="n">还款期数</param>
        /// <returns></returns>
        public static decimal debxMethodTotal(decimal p, decimal r, decimal n)
        {
            double dp = Convert.ToDouble(p);
            double dr = Convert.ToDouble(r);
            double dn = Convert.ToDouble(n);
            return Convert.ToDecimal(dp * ((dr * Math.Pow((1 + dr), dn)) / ((Math.Pow((1 + dr), dn)) - 1))) * n;
        }




        //注：
        //BX=等额本息还贷每月所还本金和利息总额
        //B=等额本息还贷每月所还本金
        //a=贷款总金额
        //i=贷款月利率
        //N=还贷总月数
        //n=第n期还贷数
        //X=等额本息还贷每月所还的利息



        /// <summary>
        /// 每个月需要还的本息
        /// 公式: BX=a*i(1+i)^N/[(1+i)^N-1] 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static double debxGetBX(double a, double i, int N)
        {
            double BX = a * i * Math.Pow(1 + i, N) / (Math.Pow(1 + i, N) - 1);

            return BX;
        }



        /// <summary>
        /// 第n个月需要还的本金
        /// 公式: B=a*i(1+i)^(n-1)/[(1+i)^N-1] 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="N"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double debxGetB(double a, double i, int N, int n)
        {
            double B = a * i * Math.Pow(1 + i, n - 1) / (Math.Pow(1 + i, N) - 1);

            return B;
        }


        /// <summary>
        /// 第n个月需要还的利息
        /// 公式: X=BX-B 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="N"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double debxGetX(double a, double i, int N, int n)
        {
            double BX = debxGetBX(a, i, N);
            double B = debxGetB(a, i, N, n);

            return BX - B;
        }


        /// <summary>
        /// 第n个月剩余本金
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="N"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double debxGetY(double a, double i, int N, int n)
        {
            if (n == 0) return a;
            return debxGetY(a, i, N, n - 1) - debxGetB(a, i, N, n);
        }


        /// <summary>
        /// 第n个月利息对应的本金
        /// XB = Y + B
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="N"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double debxGetXB(double a, double i, int N, int n)
        {
            double XB = debxGetY(a, i, N, n) + debxGetB(a, i, N, n);
            return XB;
        }

        /// <summary>
        /// 获取按日还款本息
        /// </summary>
        /// <param name="principal">本金</param>
        /// <param name="rate">年利率(整数)</param>
        /// <param name="days">天数</param>
        /// <returns></returns>
        public static double GetPrincipalAndInterestByDays(double principal, double rate, int days)
        {
            return days * ((rate * principal) / (365 * 100)) + principal;
        }

        /// <summary>
        /// 获取按日还款利息
        /// </summary>
        /// <param name="principal">本金</param>
        /// <param name="rate">年利率(整数)</param>
        /// <param name="days">天数</param>
        /// <returns></returns>
        public static double GetInterestByDays(double principal, double rate, int days)
        {
            return days * ((rate * principal) / (365 * 100));
        }
    }
}
