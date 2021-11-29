using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AB.UI_Class
{

    class utility_class
    {
        public string appName = "Atlantic Grains";
        public string versionName = "5.4";
        public string abWindowsProdURLFile = "prodURL.txt";
        public string URL = System.IO.File.ReadAllText("URL.txt");
        public string abWindowsVersionFile = "agi_window_file.txt";
        public string githubVersionFileLink = @"https://raw.githubusercontent.com/laikamanor/files/master/";
        public string githubDownload32FileLink = @"https://github.com/laikamanor/mobile-pos-v2/releases/download/v1.17/AGI.Setup.exe";
        public string githubDownload64FileLink = @"https://github.com/laikamanor/mobile-pos-v2/releases/download/v1.17/AGI.Setup64.exe";

        public string localDirectory32Exe = @"C:\AGI Installer\AGISetup.exe";
        public string localDirectory64Exe = @"C:\AGI Installer\AGISetup64.exe";
        public string localDirectoryFolder = @"C:\AGI Installer";
        public int apiTimeOut = 200000;
        public string getTextfromGithub(string value)
        {
            var strContent = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var webRequest = WebRequest.Create(githubVersionFileLink + value);
                Console.WriteLine(githubVersionFileLink + value);

                using (var response = webRequest.GetResponse())
                using (var content = response)
                using (var reader = new StreamReader(content.GetResponseStream()))
                {
                    strContent = reader.ReadToEnd();
                }
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return strContent;
        }
    }
}
