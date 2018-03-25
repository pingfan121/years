using System;
using System.Collections.Generic;
using System.Text;

namespace util
{
    public class MathUtil
    {
        static Random rm=new Random();
        static public int random(int num){
           return rm.Next(num);
        }
        static public int randomclamp(int num)
        {
            return (int)(-num + random(num*2));
        }
        static public double random()
        {
            return rm.NextDouble();
        }
    }
}