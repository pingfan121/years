using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace GameLib.Util
{
    static class StringUtil
    {
        static private LogImplement log = LogFactory.getLogger(typeof(StringUtil));

        static public Dictionary<int, string> stringfmt;
        static StringUtil() {
            stringfmt = new Dictionary<int, string>();
            string str="";
            int i;
            for ( i= 1; i < 256; i++) { 
                str+="{"+(i-1)+"}";
                stringfmt.Add(i, str);
                str += ",";
            }
            str += "{" + (i - 1) + "}";
            stringfmt.Add(i, str);
        }
        static public string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }
        //
        public static void Test(){
            Stopwatch stopwatch = new Stopwatch();

            int b = 1;
            double a = 1.0;
            a += 1;
            a -= 1;
            log.debug(a == b);
            string s = "2";
            object o = a;
            log.debug(Convert.ToSingle(o));
            Dictionary<string, object> tt = new Dictionary<string, object>();
            tt.Add(s, 100);
            tt.Add(a.ToString(), 200);
            
            string[] aaa = { "aaaa", "bbbb", "vvvvv", null, "dddddd" };
            object[] ddd = { 1, 2, 3, 4, 5, 6, "aaaaaaa", "dddddddddd", 20 };
            stopwatch.Start();
            //经过测试，发现csharp内置的string方法效率并不快多少。和我encode是一个级别的，而我支持任何简单类型的处理，因此更胜一筹。
          /*  for (int i = 0; i < 10000; i++)
            {
                //String.Join(",", aaa);
                //  string.Format(StringUtil.stringfmt[aaa.Length], aaa);
                JSON.Encode(ddd);
            }
            LOG.log("耗时=" + stopwatch.ElapsedMilliseconds);
            LOG.log(String.Join(",", aaa));
            LOG.log(string.Format(StringUtil.stringfmt[aaa.Length], aaa));
            LOG.log(JSON.Encode(aaa));
            LOG.log(JSON.Encode(ddd));*/
        }
    }
}
