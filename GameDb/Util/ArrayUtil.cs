using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace GameLib.Util
{
    public class ArrayUtil
    {
        static private LogImplement log = LogFactory.getLogger(typeof(ArrayUtil));


        // 随机函数
        static public void shuffle<T>(T[] array)
        {
            if (array == null || array.Length <= 0)
                return;

            for (int i = 0; i < array.Length; i++)
            {
                int idx = NumericalUtil.numerical(i, array.Length);

                T tmp = array[i];
                array[i] = array[idx];
                array[idx] = tmp;
            }
        }

        // 随机函数
        static public void shuffle<T>(List<T> list)
        {
            if (list == null || list.Count <= 0)
                return;

            for (int i = 0; i < list.Count; i++)
            {
                int idx = NumericalUtil.numerical(i, list.Count);

                T tmp = list[i];
                list[i] = list[idx];
                list[idx] = tmp;
            }
        }
    }
}
