using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GameLib.Util
{
    public class FileSystem
    {
        static private LogImplement log = LogFactory.getLogger(typeof(FileSystem));

        static public string readfile(string path)
        {

            StreamReader s = File.OpenText(path);
            string str = s.ReadToEnd();
            s.Close();
            return str;
        }

        static public byte[] readbin(string path)
        {
            return File.ReadAllBytes(path);
        }
        public static bool exist(string path)
        {

            return File.Exists(path);
        }
        public static void writetxt(string path, string p)
        {

            //myStreamWriter=null;
            try
            {
                using (StreamWriter myStreamWriter = File.CreateText(path))
                {

                    myStreamWriter.Write(p);
                    myStreamWriter.Flush();
                }
            }
            catch (Exception e)
            {
                log.debug("", e);
            }

        }
        public static void writebin(string path, byte[] p)
        {

            FileStream myStream = File.Create(path);

            myStream.Write(p, 0, p.Length);
            myStream.Flush();
            myStream.Close();

        }
    }
}
