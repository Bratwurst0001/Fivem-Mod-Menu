using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FiveMModsWoofer
{
	public static class NetworkManager
	{
		

		public class LoginSystemV1
		{
			public enum Options
			{
				Register = 1,
				Login,
				License,
				AntiCopy,
				ScreenShot,
				Block,
				VersionCheck
			}

			public TcpClient tcpClient = new TcpClient();

			public StreamReader reader;

			public StreamWriter writer;

			public string UserPasswort { get; set; }

			public string UserName { get; set; }

			public string License { get; set; }

			public string HWID { get; set; }

			public LoginSystemV1(string serverip)
			{
				tcpClient.Connect(serverip, 6005);
				reader = new StreamReader(tcpClient.GetStream());
				writer = new StreamWriter(tcpClient.GetStream());
			}

			public void Close()
            {
				tcpClient.Close();
            }
			public bool Register(string Name, string Passwort, string Currenthwid, string license)
			{
				UserName = Name;
				UserPasswort = Passwort;
				HWID = Currenthwid;
				sendPacket(Options.Register.ToString() + " " + UserName + " " + UserPasswort + " " + HWID + " " + license);
				return true;
			}

			public bool Login(string Name, string Passwort, string Currenthwid)
			{
				UserName = Name;
				UserPasswort = Passwort;
				HWID = Currenthwid;
				sendPacket(Options.Login.ToString() + " " + UserName + " " + UserPasswort + " " + HWID);
				string text = readPacket();
				if (text == "False")
				{
					return false;
				}
				if (text == "True")
				{
					return true;
				}
				return false;
			}
			public bool CheckingUpdate(string CurrentVersion)
			{
				
				sendPacket(Options.VersionCheck.ToString() + " " + CurrentVersion);
				string text = readPacket();
				if (text == "Up to Date!")
				{
					Console.WriteLine("Up to Date!");
					return false;
                }
                else
                {
					WebClient DownloadVersion = new WebClient();
					MessageBox.Show("Text: " + String.Join("" , text));
					DownloadVersion.DownloadFile(text,"FivemModsSpoofer-Updated.exe");
					Process.Start("FivemModsSpoofer-Updated.exe");
					Console.WriteLine("Starting...");
					Thread.Sleep(1000);
					return true;
                }
			}
			public bool Blocked()
			{
				
				sendPacket(Options.Block.ToString() + " " + GetHWID());
				string text = readPacket();
				if (text == "False")
				{
					return false;
				}
				if (text == "True")
				{
					return true;
				}
				return false;
			}
            public string GetHWID()
            {
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.FileName = "cmd.exe";
                processStartInfo.Arguments = "/C wmic baseboard get serialnumber";
                process.StartInfo = processStartInfo;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.UseShellExecute = false;
                process.Start();
                process.StandardOutput.ReadLine();
                process.StandardOutput.ReadLine();
                return process.StandardOutput.ReadLine().Replace(" ", "");
            }
            public bool Checklicense(Enum OPLicense, string license)
			{
				sendPacket(Options.License.ToString() + " " + license);
				string text = readPacket();
				if (text == "False")
				{
					return false;
				}
				return true;
			}

			public string readPacket()
			{
				return reader.ReadLine();
			}

			public void sendPacket(string packet)
			{
				writer.WriteLine(packet);
				writer.Flush();
			}
		}
	}
}
