using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {            
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Task<int> t = Task<int>.Run(() =>
            {
                int i = 1;
                int sum = 0;
                while (!tokenSource.Token.IsCancellationRequested)
                {
                    sum = sum + i;
                    i++;
                    Thread.Sleep(500);
                }
                return sum;
            }, tokenSource.Token).ContinueWith((Task<int> t2) =>
             {
                 Console.WriteLine(t2.Result);
                 int sum = t2.Result + 5;
                 return sum;
             });
            Console.ReadLine();
            tokenSource.Cancel();
            Console.WriteLine(t.Result);
            Task t3 = Task.Run(() =>
            {
                int[] array = new int[5];
                array[6] = 41;
            });
            Thread.Sleep(1000);
            if (t3.IsFaulted)
            {
                t3.Exception.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                }
                );
            }
        }
    }
}
