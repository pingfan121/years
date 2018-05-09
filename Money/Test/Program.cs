using Money.huobi_api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            HuobiHome.init();


            while(true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
