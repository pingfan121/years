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

        // �������ڶ�ȡ�ļ��е��ļ�����StreamWriter����
        public void OpenReadFile(string file)
        {
            if (!File.Exists(file))
                File.Create(file).Close();
            fsr = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            sr = new StreamReader(fsr);
        }
        // �رն��ļ���
        public void CloseReadFile()
        {
            if(fsr != null)
                fsr.Close();
        }

        // �����������ļ���׷���е��ļ�����StreamWriter����
        public void OpenWriteFile(string file)
        {
            if (!File.Exists(file))  // ����ļ������ڣ��ȴ�������ļ�
                File.Create(file).Close();
            // ��׷��ģʽ������ļ�
            fsw = new FileStream(file, FileMode.Append ,FileAccess.Write, FileShare.ReadWrite);
            // ���ݴ�����FileStream����������StreamWriter����
            sw = new StreamWriter(fsw,Encoding.UTF8);
        }
        // �ر�д�ļ���
        public void CloseWriteFile()
        {
            if (sw != null)
            {
                //fsw.Close();
                sw.Close();
                fsw.Close();
            }   
        }
        // ���ļ��ж�ȡһ��
        public  string ReadLine()
        {
            if(sr.EndOfStream)  // ����ļ���ָ���Ѿ�ָ���ļ�β��������null
                return null;
            return sr.ReadLine();
        }

        // ���ļ���׷��һ���ַ���
        public void WriteLine(string s)
        {
            sw.WriteLine(s);
            sw.Flush(); // ˢ��д�뻺������ʹ��һ�ж��ڶ��ļ����ɼ�
        }
        // �����ж��ļ���ָ���Ƿ�λ���ļ�β��
        public bool IsEof()
        {
            return sr.EndOfStream;
        }

    }
}
