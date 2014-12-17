using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4Smasher
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.SetBufferSize(8000, 8000);
            smashMainPage();
            Console.ReadLine();
        }

        private static void smashMainPage()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(100);
                    string content = smashUrl("http://ps20.software.eu.playstation.com");
                    var bits = content.Split(new string[] { "config.lang = \"en_GB\";" }, StringSplitOptions.None);
                    
                    bits = bits[1].Split(new string[] { "</script>" }, StringSplitOptions.None);

                    Console.Clear();
                    Thread.Sleep(20);
                    Console.WriteLine(bits[0]);
                    bits = bits[0].Split('"');

                    string url = "http://ps20.software.eu.playstation.com/redirect.php?sp=" + bits[1];
                   
                    Console.WriteLine(url);
                    string newUrl = smashUrl(url);
                    Clipboard.SetText(newUrl);
                    Console.WriteLine(newUrl + " copied to clipboard");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception world");
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        private static string smashUrl(string sURL)
        {

            HttpWebRequest wrGETURL;
            wrGETURL = HttpWebRequest.CreateHttp(sURL);
            wrGETURL.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36";
            wrGETURL.Proxy = WebProxy.GetDefaultProxy();

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            string result = "";
            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    result += sLine;
                }
            }

            return result;
        }
    }
}
