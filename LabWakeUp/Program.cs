using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Globalization;


namespace LabWakeUp
{
    public class ServerList
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    static class Program
    {
         public static List<string> ServerList;
         public static List<ServerList> DataSource;
         public static List<ServerList> ServerMacList;

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        static extern int SendARP(int destIP, int srcIP, byte[] pMacAddr, ref uint phyAddrLen);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Sends a Wake-On-Lan packet to the specified MAC address.
        /// </summary>
        /// <param name="macAddr">Physical MAC address to send WOL packet to.</param>
        public static void WakeUp(string macAddr)
        {
            // Convert MAC address to Hex bytes
            var value = long.Parse(macAddr, NumberStyles.HexNumber, CultureInfo.CurrentCulture.NumberFormat);
            var macBytes = BitConverter.GetBytes(value);

            Array.Reverse(macBytes);
            var macAddress = new byte[6];
            for (var i = 0; i <= 5; i++)
                macAddress[i] = macBytes[i + 2];

            //
            // WOL packet is sent over UDP 255.255.255.0:40000.
            //
            var client = new UdpClient();
            client.Connect(IPAddress.Broadcast, 40000);

            //
            // WOL packet contains a 6-bytes trailer and 16 times a 6-bytes sequence containing the MAC address.
            //
            var packet = new byte[17 * 6];

            //
            // Trailer of 6 times 0xFF.
            //
            for (var i = 0; i < 6; i++)
            {
                packet[i] = 0xFF;
            }

            //
            // Body of magic packet contains 16 times the MAC address.
            //
            for (var i = 1; i <= 16; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    packet[i * 6 + j] = macAddress[j];
                }
            }

            //
            // Submit WOL packet.
            //
            client.Send(packet, packet.Length);
        }

        static byte[] GetMACAddress(string hostNameOrAddress)
        {
            var hostEntry = Dns.GetHostEntry(hostNameOrAddress);
            if (hostEntry.AddressList.Length == 0)
                return null; // We were not able to resolve given hostname / address 

            var macAddr = new byte[6];
            var macAddrLen = (uint)macAddr.Length;
            
            return SendARP((int)hostEntry.AddressList[0].Address, 0, macAddr, ref macAddrLen) != 0 ? null : macAddr;
        }

        public static void ShutDownMachine(string machineName, string username, string password)
        {
            ManagementScope scope;
            ConnectionOptions connOptions;
            ManagementObjectSearcher objSearcher = null;
            try
            {
                connOptions = new ConnectionOptions
                                  {
                                      Impersonation = ImpersonationLevel.Impersonate,
                                      EnablePrivileges = true
                                  };
                //local machine
                if (machineName.ToUpper() == Environment.MachineName.ToUpper())
                    scope = new ManagementScope(@"\ROOT\CIMV2", connOptions);

                else
                {
                    //remote machine
                    connOptions.Username = username;
                    connOptions.Password = password;
                    scope = new ManagementScope(@"\\" + machineName + @"\ROOT\CIMV2", connOptions);
                }
                scope.Options.EnablePrivileges = true;
                scope.Options.Authentication = AuthenticationLevel.Default;
                scope.Connect();
                var objQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                objSearcher = new ManagementObjectSearcher(scope, objQuery);
                foreach (ManagementObject operatingSystem in objSearcher.Get())
                {
                    if (operatingSystem != null)
                    {
                        operatingSystem.InvokeMethod("Shutdown", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (objSearcher != null) objSearcher.Dispose();
            }
        }

        public static void LoadServerList()
        {
            DataSource = new List<ServerList>();
            try
            {
                using (var sr = new StreamReader("Servers.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            var mac = GetMACAddress(line);
                            CheckServerList(line, ByteArrayToString(mac).ToUpper());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(@"Failed to Open Servers.txt");
                File.Create("ServerMACs.txt").Dispose();
            }

            try
            {
                using (var sr = new StreamReader("ServerMACs.txt"))
                {
                    string line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        var servermac = line.Split(',');
                        DataSource.Add(new ServerList { Name = servermac[0], Value = servermac[1] });
                    }
                }
            }
            catch (Exception)
            {
                File.Create("ServerMACs.txt").Dispose();
            }
        }

        public static void CheckServerList(string servername, string macaddress)
        {
            var serverfound = false;
            using (var sr = new StreamReader("ServerMACs.txt"))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    var servermac = line.Split(',');
                    serverfound = servermac[0].ToUpper().Contains(servername.ToUpper());
                    if (serverfound) break;
                }
            }
            if (serverfound) return;
            using (StreamWriter sw = File.AppendText("ServerMACs.txt"))
            {
                sw.WriteLine(servername+","+macaddress);
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
