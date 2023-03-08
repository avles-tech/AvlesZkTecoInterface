using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZMKeeperCore;
namespace TestWIndowsForm
{
    public partial class Form1 : Form
    {
        ZKClient client = new ZKClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
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
