using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZMKeeperCore;

namespace TestInterface
{
    internal class Program
    {
       
       
        static void Main(string[] args)
        {
            ZKClient client = new ZKClient();
            bool test = client.Connect("192.168.8.108", 4370);
            if (test)
            {
                Console.WriteLine("connected ...");
                //client.GetLogData();
               
               
            }
            else
            {
                Console.WriteLine("somethin wrong");
            }
            Console.WriteLine("press any key to close");
            Console.ReadLine();
        }

        
    }
}
