using System.Threading;
using System.Windows.Forms;
using zkemkeeper;

namespace AvlesZKTecoInterface
{
    partial class Service1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        CZKEM axCZKEM1;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Thread createComAndMessagePumpThread = new Thread(() =>
            {
                axCZKEM1 = new zkemkeeper.CZKEM();
                bool connSatus = axCZKEM1.Connect_Net("192.168.8.108", 4370);
                if (connSatus == true)
                {
                    WriteToFile("connSatus");
                    this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(zkemClient_OnAttTransactionEx);

                    if (axCZKEM1.RegEvent(1, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                    {
                        WriteToFile("RegEvent");
                        this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(zkemClient_OnAttTransactionEx);

                    }
                }
                Application.Run();
            });

            createComAndMessagePumpThread.SetApartmentState(ApartmentState.STA);

            createComAndMessagePumpThread.Start();
            components = new System.ComponentModel.Container();
            this.ServiceName = "Service1";
        }

        private void zkemClient_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {
            WriteToFile("zkemClient_OnAttTransactionEx");
        }


        #endregion
    }
}
