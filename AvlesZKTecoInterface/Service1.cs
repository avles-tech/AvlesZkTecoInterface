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
using zkemkeeper;
using System.Windows.Forms;
using System.Threading;

namespace AvlesZKTecoInterface
{
    public partial class Service1 : ServiceBase
    {

        System.Timers.Timer tmrDelay;
        int count;

        public Service1()
        {
            InitializeComponent();
            tmrDelay = new System.Timers.Timer(5000);
            tmrDelay.Elapsed += new System.Timers.ElapsedEventHandler(tmrDelay_Elapsed);
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("OnStart");


            tmrDelay.Enabled = true;
            
        }

        protected override void OnStop()
        {
            tmrDelay.Enabled = false;
        }

        
        void tmrDelay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string str = "Timer tick " + count;
            
            count++;

            WriteToFile(str);
        }

        public void WriteToFile(string Message)
        {
            Console.WriteLine(Message);
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }


    }
}
