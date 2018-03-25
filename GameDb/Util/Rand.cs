using System;
using System.Collections.Generic;
using System.Text;

namespace util
{
    public class Rand
    {
        static double[] rands;//预先生成的随机数组
        static uint index;
        static double[] questrands;//预先生成的规则随机数组

        static Rand() {
            rands = new double[100000];
            for (int i = 0; i < 100000; i++) {
                rands[i] = RandomNumber();
            }
            index = 0;

            questrands = new double[20];
            for (int i = 0; i < 20; i++)
            {
                questrands[i] = i / 20d;
            }
            Random rd = new Random(Environment.TickCount);
            for (int i = 0; i < 100; i++)
            {
                int x = rd.Next(0, questrands.Length);
                int y = rd.Next(0, questrands.Length);
                double temp = questrands[x];
                questrands[x] = questrands[y];
                questrands[y] = temp;
            }

        }
        static Random random = new Random();
        public static int RandomNumber(int min, int max)
        {
            if (min <= max)
            {
                return random.Next(min, max);
            }
            return random.Next(max,min);
        }
        public static int RandomNumber(int max)
        {
            if (max > 0)
            {
                return random.Next(max);
            }
            return 0;
        }
        public static double RandomNumber()
        {
            return (double)random.NextDouble();
        }
        public static double wRandomNumber()
        {
            return rands[(++index)%100000];
        }
        public static double qRandomNumber(uint num)
        {
            return questrands[num % 20];
        }
        static char[] chars = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        public static string RandomString(int num)
        {
            string re = "";
            for (int i = 0; i < num; i++)
            {
                re += chars[(int)RandomNumber(chars.Length)];
            }
            return re;
        }
    }
}
