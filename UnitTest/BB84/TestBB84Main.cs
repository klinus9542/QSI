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
            //k is package length;from 32bit to 512bit
            //i is threshold for sucess communication, from 10% to 100%
            //every experiment, different package length and different thredshold, runs for 100 times and find the success time
            int runtimeValue = 100;


            int[,] successCounter = new int[10, 5];

            Console.WriteLine("The x-axis is the DATA package length, from 32qbit-512qbit.");
            Console.WriteLine("The y-axis is the sample percentage, from 10%-100%.");
            Console.WriteLine("For every case with different parameters, the case will run about 100 times and add up the success times.");
            Console.WriteLine("\nThe statistical process begins:\n ");

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


           


            //every row is a success threshold 
            //every col is a package length
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
