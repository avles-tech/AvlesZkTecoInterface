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
        static System.Timers.Timer tmrDelay;
        static ZKClient client = new ZKClient();
        static void Main(string[] args)
        {
            tmrDelay = new System.Timers.Timer(5000);
            tmrDelay.Elapsed += new System.Timers.ElapsedEventHandler(tmrDelay_Elapsed);

            tmrDelay.Enabled = true;

           
            bool test = client.Connect("192.168.8.108", 4370);
            if (test)
            {
                Console.WriteLine("connected ...");

            }
            else
            {
                Console.WriteLine("somethin wrong");
            }

            Console.ReadLine();
           
        }

        static void tmrDelay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string str = "Timer tick ";


            client.WriteToFile(str);
        }


    }
}
