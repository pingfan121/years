using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GameLib.Util
{
    public class FileIO
    {
        private FileStream fsr;
        private FileStream fsw;
        private StreamWriter sw;
        private StreamReader sr;

        // 创建用于读取文件行的文件流和StreamWriter对象
        public void OpenReadFile(string file)
        {
            if (!File.Exists(file))
                File.Create(file).Close();
            fsr = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            sr = new StreamReader(fsr);
        }
        // 关闭读文件流
        public void CloseReadFile()
        {
            if(fsr != null)
                fsr.Close();
        }

        // 创建用于向文件中追加行的文件流和StreamWriter对象
        public void OpenWriteFile(string file)
        {
            if (!File.Exists(file))  // 如果文件不存在，先创建这个文件
                File.Create(file).Close();
            // 以追加模式打开这个文件
            fsw = new FileStream(file, FileMode.Append ,FileAccess.Write, FileShare.ReadWrite);
            // 根据创建的FileStream对象来创建StreamWriter对象
            sw = new StreamWriter(fsw,Encoding.UTF8);
        }
        // 关闭写文件流
        public void CloseWriteFile()
        {
            if (sw != null)
            {
                //fsw.Close();
                sw.Close();
                fsw.Close();
            }   
        }
        // 从文件中读取一行
        public  string ReadLine()
        {
            if(sr.EndOfStream)  // 如果文件流指针已经指向文件尾部，返回null
                return null;
            return sr.ReadLine();
        }

        // 向文件中追加一行字符串
        public void WriteLine(string s)
        {
            sw.WriteLine(s);
            sw.Flush(); // 刷新写入缓冲区，使这一行对于读文件流可见
        }
        // 用于判断文件流指针是否位于文件尾部
        public bool IsEof()
        {
            return sr.EndOfStream;
        }

    }
}
