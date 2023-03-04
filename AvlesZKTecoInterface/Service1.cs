using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ZMKeeperCore;

namespace AvlesZKTecoInterface
{
    public partial class Service1 : ServiceBase
    {
       
       
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            
            ZKClient client = new ZKClient();
            client.WriteToFile("Service is started at " + DateTime.Now);
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

        protected override void OnStop()
        {
        }

        
    }
}
