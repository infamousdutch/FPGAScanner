using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace fpgaScanner
{
    public class scanner
    {
        public static System.Timers.Timer timer { get; set; }
        public static string Command { get; set; }
        public scanner()
        {

        }
        public static void Scan()
        {
            bool verbose = false;
            Console.WriteLine("Begin Scan");
            StringBuilder str = new StringBuilder();
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
           
            foreach (string port in ports)
            {
                if (verbose)
                    Console.WriteLine(port);

                try
                {
                    using (var serialPort = new SerialPort(port, 115200, Parity.None, 8, StopBits.One))
                    {
                        serialPort.Open();

                        if(verbose)
                            Console.WriteLine("Port available");
                        serialPort.ReadTimeout = 200;
                        serialPort.WriteTimeout = 50;


                        bool bln = CheckForBitForce(verbose, serialPort);

                        if (bln)
                        {
                            Console.WriteLine("Idle BFL BitForce found on " + port);
                            str.Append(" -S \\\\.\\");
                            str.Append(port);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (verbose)
                        Console.WriteLine("E: " + ex.Message);

                }
            }
            
            Console.WriteLine("Scan Complete");
            string retval = str.ToString();

            if (retval != string.Empty)
            {
                try
                {
                   
                    Console.WriteLine("Starting Miner: " + Command + retval);
                    Process p = new Process();
                    string[] c = Command.Split(" ".ToCharArray());
                    p.StartInfo.FileName = c[0];
                   
                    p.StartInfo.Arguments = Command.Replace(c[0],"") + retval;
                    p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    p.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Opening Miner: " + ex.Message);

                }
            }
            else
            {
                Console.WriteLine("No idle devices were found.");

            }
        }

        private static bool CheckForBitForce(bool verbose, SerialPort serialPort)
        {

            serialPort.Write("ZGX");

            string strS = serialPort.ReadLine().ToLower();
            if (verbose)
                Console.WriteLine("Command Received: " + strS);

            bool retval = strS.Contains("bitforce");
            //if (!retval)
            //{
            //  //  strS.Contains("BitFORCE SHA256");
            //}
            return retval;// strS.Contains("BitFORCE SHA256");
           
        }
    }
}
