using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;

namespace GameLib.Util
{
    public class NumericalUtil
    {
        static private LogImplement log = LogFactory.getLogger(typeof(NumericalUtil));

        static private Random _random = null;

        static public int numerical(int minValue, int maxValue)
        {
            if(_random == null)
            {
                byte[] bytes = new byte[4];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(bytes);

                _random = new Random(BitConverter.ToInt32(bytes, 0));
            }

            return _random.Next(minValue, maxValue);
        }

        // 计算概率
        static public bool rate(int seed)
        {
            int random = numerical(1, 10000 + 1);

            return seed >= random ? true : false;
        }
    }
}
