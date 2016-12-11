using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    class TestBB84Main
    {
        static public void TestMethod()
        {
            //For BB84 channel check only

            int runtimeValue = 100;


            int[,] successCounter = new int[10, 5];

            for (int k = 0; k < 5; k++)       //2^5-2^9
            {
                for (int i = 0; i < 10; i++)  //10%-100%
                {
                    for (var j = 0; j < runtimeValue; j++)
                    {
                        successCounter[i, k] += TestBB84WithChannelCheckAuto.TestMethod(Convert.ToInt32(Math.Pow(2, k + 5)), (i + 1) * 10);
                    }
                }
            }


            Console.WriteLine("The success times are ");

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(successCounter[i, j] + "\t");
                }
                Console.WriteLine("\r");
            }




        }

    }
}
