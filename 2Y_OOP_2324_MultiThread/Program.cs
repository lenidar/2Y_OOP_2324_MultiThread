using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace _2Y_OOP_2324_MultiThread
{
    internal class Program
    {
        static bool[] resources = new bool[] { false, false, false, false, false};
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Thread w0 = new Thread(() => function(new int[] { 0, 1 }));
            Thread w1 = new Thread(() => function(new int[] { 1, 2 }));
            Thread w2 = new Thread(() => function(new int[] { 2, 3 }));
            Thread w3 = new Thread(() => function(new int[] { 3, 4 }));
            Thread w4 = new Thread(() => function(new int[] { 4, 0 }));
            w0.Start();
            w1.Start();
            w2.Start();
            w3.Start();
            w4.Start();

            Console.WriteLine("Done starting all threads.");
        }

        static void function(int[] access)
        {
            int sleepTime = (rnd.Next(25,50) * 100);

            Thread.Sleep(sleepTime);

            while(true) 
            {
                Console.WriteLine($"Function {access[0]} is trying to see if it can activate...");
                if (!resources[access[0]] && !resources[access[1]])
                {
                    resources[access[0]] = true;
                    resources[access[1]] = true;
                    Console.WriteLine($"Function {access[0]} is activating... Resources locked...");
                    Thread.Sleep(((rnd.Next(25, 50) * 100) + sleepTime)/2);
                    Console.WriteLine($"Function {access[0]} finished... Releasing resources...");
                    resources[access[0]] = false;
                    resources[access[1]] = false;
                    Thread.Sleep(((rnd.Next(25, 50) * 100) + sleepTime) / 2);
                    Console.WriteLine($"Function {access[0]} ready to activate...");
                }
                else
                {
                    Console.WriteLine($"Function {access[0]} failed to activate...");
                    Thread.Sleep(sleepTime/2);
                }
            }
        }
    }
}
