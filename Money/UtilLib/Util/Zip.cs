using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace GameLib.Util
{
    public class Zip
    {
        public static byte[] compressString(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            return compress(buffer);
        }

        public static string decompressString(byte[] data)
        {
            byte[] bytes = decompress(data);
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] compress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                DeflateStream gz = new DeflateStream(ms, CompressionMode.Compress, true);
                // GZipStream gz = new GZipStream(ms, CompressionMode.Compress, true);
                gz.Write(data, 0, data.Length);
                gz.Close();
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static byte[] decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                DeflateStream gz = new DeflateStream(ms, CompressionMode.Decompress, true);
                // GZipStream gz = new GZipStream(ms, CompressionMode.Decompress, true);
                MemoryStream msreader = new MemoryStream();
                byte[] buffer = new byte[0x1000];
                while (true)
                {
                    int reader = gz.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }
                    msreader.Write(buffer, 0, reader);
                }
                gz.Close();
                ms.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
