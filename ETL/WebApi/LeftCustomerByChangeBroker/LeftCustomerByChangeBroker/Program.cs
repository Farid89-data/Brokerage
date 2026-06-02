//using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net.NetworkInformation;
using System.Text;
using System.IO;
using System.Security.Cryptography;
//using System.Text.Json.Nodes;

namespace LeftCustomerByChangeBroker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string dtNow = string.Format("{0:yyyy-MM-dd hh-mm-ss-tt}   :   ", DateTime.Now);
           
            string StartDate = "";
            string EndDate = "";
            StartDate = args[0];
            EndDate = args[1];
            string LogReport = " ============================================================================= \n" + dtNow + " Start                    " + StartDate+ "  --  "+EndDate + " \n" + " ============================================================================= \n";

            string DirPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string exePath = DirPath + "\\jsondata.txt";
            string LogPath = DirPath + "\\log\\"+ string.Format("Log-{0:yyyy-MM-dd_hh-mm-ss-tt}.txt", DateTime.Now);

            if (!Directory.Exists(DirPath + "\\log\\"))
            {
                LogReport += (dtNow + "Log Folder is Not Found   ==>     Create Log Folder  ==>     ");
                Directory.CreateDirectory(DirPath + "\\log\\");
                LogReport += ("Log Folder is Created \n");
            }

            //LogReport +=(dtNow + "File Will be Written in location  :   "+ exePath+"\n" + dtNow + "Check if File existes !! \n");
            //if (File.Exists(exePath)) 
            //{
            //    LogReport += (dtNow + "jsondata.txt Found , Move to Old Folder \n");
            //    string OldDir = DirPath + "\\Old\\";
            //    string OldFile = DirPath + "\\Old\\" + string.Format("jsondata-{0:yyyy-MM-dd_hh-mm-ss-tt}.txt", DateTime.Now);
            //    LogReport += (dtNow + "Old File Adress  :    " + OldFile + "\n");

            //    if (!Directory.Exists(OldDir))
            //    {
            //        LogReport += (dtNow + "Old Folder is Not Found   ==>     Create Old Folder  ==>     ");
            //        Console.WriteLine();
            //        Directory.CreateDirectory(OldDir);
            //        LogReport += ("Old Folder is Created \n"); 
            //    }
            //    File.Move(exePath, OldFile);
            //    LogReport += (dtNow + "File Moved to Old Folder  :  " + OldFile + "\n");
            //}


            string UrlLink = String.Format("http://automationapi.mobinsb.com/api/External/crm/GoneChangeBroker?startDate={0}&endDate={1}", StartDate, EndDate);
            var client = new RestClient(UrlLink);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("access-token", "C9984986-A17B-43A0-B540-73846508788D");
            request.AddHeader("company-id", "125");
            request.AddHeader("platform-key", "851ECEFC-49CE-4BD2-A3B5-2B12EB7CC694");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            
            await File.WriteAllTextAsync(exePath, response.Content+"\n", Encoding.Unicode);
            LogReport += "\n\n------------------Recived Data Begin------------------------------------\n\n"+response.Content+ "\n\n------------------Recived Data End------------------------------------\n" + "\n" + " ============================================================================= \n" +  dtNow + " Ends \n" + " ============================================================================= \n";
            
            Console.WriteLine(LogReport);
            File.WriteAllText(LogPath,LogReport, Encoding.Unicode);

            //if (File.Exists(LogPath))
            //    File.AppendText(LogReport);
            //else
            //    File.WriteAllText (LogPath, LogReport);

        }
    }
}