using RestSharp;
using System.Net.NetworkInformation;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace GetRequestMasterStockMarket
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

            int WPageSize = Convert.ToInt32(args[2]);
            int WPage = Convert.ToInt32(args[3]);
            string LogReport = " ============================================================================= \n" + dtNow + " Start                    " + StartDate + "  --  " + EndDate + " \n" + " ============================================================================= \n";

            string DirPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string exePath = DirPath + "\\jsondata.txt";
            string LogPath = DirPath + "\\log\\" + string.Format("Log-{0:yyyy-MM-dd}_Page{1}_{0:hh-mm-ss-tt}.txt", DateTime.Now, WPage);

            if (!Directory.Exists(DirPath + "\\log\\"))
            {
                LogReport += (dtNow + "Log Folder is Not Found   ==>     Create Log Folder  ==>     ");
                Directory.CreateDirectory(DirPath + "\\log\\");
                LogReport += ("Log Folder is Created \n");
            }

            string UrlLink = String.Format("http://automationapi.mobinsb.ir/api/External/Bourse/GetChangeBrokerAttachmentInsertDate?ToDate={0}&FromDate={1}&PageSize={2}&PageNumber={3}", EndDate, StartDate, WPageSize, WPage);
            var client = new RestClient(UrlLink);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("access-token", "C9984986-A17B-43A0-B540-73846508788D");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);

            await File.WriteAllTextAsync(exePath, response.Content, Encoding.Unicode);
            LogReport += "\n\n------------------Recived Data Begin------------------------------------\n\n" + response.Content + "\n\n------------------Recived Data End------------------------------------\n" + "\n" + " ============================================================================= \n" + dtNow + " Ends \n" + " ============================================================================= \n";
            Console.WriteLine(LogReport);
            File.WriteAllText(LogPath, LogReport, Encoding.Unicode);

        }
    }
}