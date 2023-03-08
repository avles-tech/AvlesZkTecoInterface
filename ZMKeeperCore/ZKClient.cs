using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using zkemkeeper;

namespace ZMKeeperCore
{
    public class ZKClient
    {
        public  CZKEM axCZKEM1 = new CZKEM();
        private  bool bIsConnected;
        private  string iPAddress;
        private  int port;
        private  int machineNumber= 1;
       
        public bool Connect(string iPAddress, int port)
        {
            bIsConnected = axCZKEM1.Connect_Net(iPAddress, port);
            if (bIsConnected)
            {
                WriteToFile("bIsConnected");
                if (axCZKEM1.RegEvent(1, 32767))
                {
                    WriteToFile("RegEvent");
                    axCZKEM1.OnConnected += ObjCZKEM_OnConnected;
                    axCZKEM1.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(zkemClient_OnAttTransactionEx);
                }
            }
            return bIsConnected;
        }
        private void zkemClient_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {
            Console.WriteLine("zkemClient_OnAttTransactionEx");
            WriteToFile("zkemClient_OnAttTransactionEx");

        }

        private void ObjCZKEM_OnConnected()
        {
            WriteToFile("ObjCZKEM_OnConnected");
        }

        public ICollection<MachineInfo> GetLogData()
        {
            string dwEnrollNumber1 = "";
            int dwVerifyMode = 0;
            int dwInOutMode = 0;
            int dwYear = 0;
            int dwMonth = 0;
            int dwDay = 0;
            int dwHour = 0;
            int dwMinute = 0;
            int dwSecond = 0;
            int dwWorkCode = 0;

            ICollection<MachineInfo> lstEnrollData = new List<MachineInfo>();

            axCZKEM1.ReadAllGLogData(machineNumber);
            // dwInOutMode - dwInOutMode


            while (axCZKEM1.SSR_GetGeneralLogData(machineNumber, out dwEnrollNumber1, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkCode))
            {
                string inputDate = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond).ToString();

                MachineInfo objInfo = new MachineInfo();
                objInfo.MachineNumber = machineNumber;
                objInfo.IndRegID = int.Parse(dwEnrollNumber1);
                objInfo.DateTimeRecord = inputDate;

                lstEnrollData.Add(objInfo);
            }

            return lstEnrollData;
        }



        public void getAllUserInfo()
        {
            string sdwEnrollNumber = string.Empty, sName = string.Empty, sPassword = string.Empty, sTmpData = string.Empty;
            int iPrivilege = 0, iTmpLength = 0, iFlag = 0, idwFingerIndex;
            bool bEnabled = false;

            ICollection<UserInfo> lstFPTemplates = new List<UserInfo>();
             
            axCZKEM1.ReadAllUserID(machineNumber);
            axCZKEM1.ReadAllTemplate(machineNumber);
            while (axCZKEM1.SSR_GetAllUserInfo(machineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
            {
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (axCZKEM1.GetUserTmpExStr(machineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))
                    {
                        UserInfo fpInfo = new UserInfo();
                        fpInfo.MachineNumber = machineNumber;
                        fpInfo.EnrollNumber = sdwEnrollNumber;
                        fpInfo.Name = sName;
                        fpInfo.FingerIndex = idwFingerIndex;
                        fpInfo.TmpData = sTmpData;
                        fpInfo.Privelage = iPrivilege;
                        fpInfo.Password = sPassword;
                        fpInfo.Enabled = bEnabled;
                        fpInfo.iFlag = iFlag.ToString();

                        lstFPTemplates.Add(fpInfo);
                    }
                }

            }

            Console.WriteLine(lstFPTemplates);
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

        static string InorOut(int InOut)
        {
            string InOrOut = "";
            switch (InOut)
            {
                case 0:
                    InOrOut = "IN";
                    break;
                case 1:
                    InOrOut = "OUT";
                    break;
                case 2:
                    InOrOut = "BREAK-OUT";
                    break;
                case 3:
                    InOrOut = "BREAK-IN";
                    break;
                case 4:
                    InOrOut = "OVERTIME-IN";
                    break;
                case 5:
                    InOrOut = "OVERTIME-OUT";
                    break;

            }
            return InOrOut;
        }
    }


}
