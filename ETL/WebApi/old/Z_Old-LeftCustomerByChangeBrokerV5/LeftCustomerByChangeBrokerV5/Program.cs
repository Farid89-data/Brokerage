using RestSharp;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LeftCustomerByChangeBrokerV5
{
    internal class Program
    {
        static async Task Main(string[] args)

        {
            string StartDate = "";
            string EndDate = "";
                StartDate = args[0];
                EndDate   = args[1];

            string exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\jsondata.txt";
            Console.WriteLine("File Will be Written in location :"+ exePath);
            
            string UrlLink = String.Format("http://automationapi.mobinsb.com/api/External/crm/GoneChangeBroker?startDate={0}&endDate={1}", StartDate, EndDate);
            var client = new RestClient(UrlLink);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("access-token", "C9984986-A17B-43A0-B540-73846508788D");
            request.AddHeader("company-id", "125");
            request.AddHeader("platform-key", "851ECEFC-49CE-4BD2-A3B5-2B12EB7CC694");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);

            await File.WriteAllTextAsync(exePath,response.Content, Encoding.GetEncoding("utf-8"));
            Console.WriteLine(response.Content);
        }
    }
}
