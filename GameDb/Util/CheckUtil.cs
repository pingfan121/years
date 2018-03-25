using System;
using System.Collections.Generic;
using System.Text;


namespace GameLib.Util
{
    public class CheckUtil
    {
        /// <summary>  
        /// 验证位身份证  
        /// </summary>  
        /// <param name="Id">身份证号码</param>  
        /// <returns>是否真实身份证</returns>  
        /// 
        //返回true符合验证的标准，反之则非法；  
        static public bool CheckIDCard(string Id)
        {
            int intLen = Id.Length;
            long n = 0;

            if (intLen == 18)
            {
                if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false;//数字验证  
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(Id.Remove(2)) == -1)
                {
                    return false;//省份验证  
                }
                string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证  
                }
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = Id.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }
                int y = -1;
                Math.DivRem(sum, 11, out y);
                if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
                {
                    return false;//校验码验证  
                }
                return true;//符合GB11643-1999标准  
            }
            else if (intLen == 15)
            {
                if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
                {
                    return false;//数字验证  
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(Id.Remove(2)) == -1)
                {
                    return false;//省份验证  
                }
                string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证  
                }
                return true;//符合15位身份证标准  
            }
            else
            {
                return false;//位数不对  
            }
        }
        // Check(object obj,string reg)验证基函数
        public static bool Check(object obj, string reg)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(obj.ToString(), reg, System.Text.RegularExpressions.RegexOptions.None);
        }

        public static bool IsValidName(object obj)
        {
            return Check(obj, @"^[\u4E00-\u9FA5a-zA-Z][\u4E00-\u9FA5a-zA-Z0-9]{1,7}$");
        }
        // IsNotEmpty验证是否为空

        public static bool IsNotEmpty(object obj)
        {
            return Check(obj, @".?[^\s　]+");
        }


        // 验证是不是正常字符 字母，数字，下划线的组合
        ///

        /// ///
        public static bool IsNormalChar(object obj)
        {
            return Check(obj, @"[\w\d_]+");
        }


        // IsEnglish验证是否为英文字符及下划线
        ///

        /// IsEnglish验证是否为英文字符及下划线
        ///


        public static bool IsEnglish(object obj)
        {
            return Check(obj, @"^[a-zA-Z0-9_\-]+$");
        }


        // IsChinese验证是否为中文字符
        ///

        /// IsChinese验证是否为中文字符
        ///


        public static bool IsChinese(object obj)
        {
            return Check(obj, @"^[\u0391-\uFFE5]+$");
        }


        // IsDate是否为有效的日期格式
        ///

        /// IsDate是否为有效的日期格式
        ///


        public static bool IsDate(object obj)
        {
            try
            {
                DateTime time = Convert.ToDateTime(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }


        // IsEmail是否为有效的邮箱格式
        ///

        /// IsEmail是否为有效的邮箱格式
        ///


        public static bool IsEmail(object obj)
        {
            return Check(obj, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
        }


        // IsUrl是否为有效的超链接格式
        ///

        /// IsUrl是否为有效的超链接格式
        ///


        public static bool IsUrl(object obj)
        {
            return Check(obj, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&%_\./-~-]*)?$");
        }


        // IsPhone是否为有效的电话号码
        ///

        /// 例如：XXX-XXXXXXX
        ///


        public static bool IsPhone(object obj)
        {
            return Check(obj, @"^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$");
        }


        // IsMobile是否为有效的手机号码
        ///

        /// IsMobile是否为有效的手机号码
        ///


        public static bool IsMobile(object obj)
        {
            return Check(obj, @"^((\(\d{2,3}\))|(\d{3}\-))?((13\d{9})|(159\d{8}))$");
        }


        // IsIP是否为有效的IP地址
        ///

        /// CheckIP是否为有效的IP地址
        ///


        public static bool IsIP(object obj)
        {
            return Check(obj, @"^(0|[1-9]\d?|[0-1]\d{2}|2[0-4]\d|25[0-5]).(0|[1-9]\d?|[0-1]\d{2}|2[0-4]\d|25[0-5]).(0|[1-9]\d?|[0-1]\d{2}|2[0-4]\d|25[0-5]).(0|[1-9]\d?|[0-1]\d{2}|2[0-4]\d|25[0-5])$");
        }


        // IsZipCode是否为有效的邮政编码
        ///

        ///IsZipCode是否为有效的邮政编码
        ///


        public static bool IsZipCode(object obj)
        {
            return Check(obj, @"^[1-9]\d{5}$");
        }


        // IsIdCard是否为有效的身份证号码
        ///

        /// IsIdCard是否为有效的身份证号码
        ///

        public static bool IsIdCard(object obj)
        {
            return Check(obj, @"(^\d{15}$)|(^\d{17}[0-9Xx]$)");
        }


        // IsQQ是否为有效的QQ号码
        ///

        /// IsQQ是否为有效的QQ号码
        ///


        public static bool IsQQ(object obj)
        {
            return Check(obj, @"^[1-9]\d{4,10}$");
        }


        // IsMSN是否为有效的MSN帐户
        ///

        /// IsMSN是否为有效的MSN帐户
        ///


        public static bool IsMSN(object obj)
        {
            return Check(obj, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }


        // IsNumber验证是不是数字
        ///

        /// IsNumber验证是不是数字
        ///


        public static bool IsNumber(object obj)
        {
            return Check(obj, @"^[-\+]?\d+(\.\d+)?$");
        }


        // IsInteger验证是不是整数
        ///

        /// IsInteger验证是不是整数
        ///


        public static bool IsInteger(object obj)
        {
            return Check(obj, @"^-?\d+$");
        }


        // IsUnsignedInteger验证是不是正整数
        ///

        /// IsUnsignedInteger验证是不是正整数
        ///
        public static bool IsUnsignedInteger(object obj)
        {
            return Check(obj, @"^[0-9]*[1-9][0-9]*$");
        }


        // IsSignedInteger验证是不是负整数
        ///

        /// IsSignedInteger验证是不是负整数
        ///


        public static bool IsSignedInteger(object obj)
        {
            return Check(obj, @"^-[0-9]*[1-9][0-9]*$");
        }

        public static bool IsValidPass(string p)
        {
            if (p.Length > 5 && p.Length < 32)
            {
                return Check(p, @"^[a-zA-Z0-9_\-]+$");
            }
            return false;
        }
    }
}