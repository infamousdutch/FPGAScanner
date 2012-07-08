using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fpgaScanner
{
    class Program
    {
        const string version = "0.40";
        static System.Timers.Timer timer;
        static int elapsed = 60000;

        static void Main(string[] args)
        {
            
            Console.WriteLine("FPGA Port Scanner BTC Mining Utility v. " + version + " by infamousDutch");
            if (args.Length == 0)
            {
                Console.WriteLine("No Miner Parameter Found, using default mining parameters"); 
                scanner.Command = "cgminer.exe -c cgminer.conf --disable-gpu";
            }
            else
            {
                if (args[0].ToLower().Contains("help"))
                {
                    help();
                    return;
                }
                else
                {
                    scanner.Command = args[0];

                }
              
            }
            Console.WriteLine("Mining Parameters: " + scanner.Command);
            timer = new System.Timers.Timer();
            timer.Interval = elapsed;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            timer.Start();
            scanner.Scan();
            string mye = "";
            while (mye.ToLower().Trim() !="quit")
            {
                mye = Console.ReadLine();

            }
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            scanner.Scan();
        }
        static void help()
        {
            string str = @"
                Scans for idle FPGAs (currently only Bitforce) every 2 minutes and fires up a miner to start mining on them.
                Currently supports cgminer and bfgminers.
                Takes one parameter, the miner and mining parameters you wish to start up when a port is found to be open.
                
                    Example: fpgaScanner cgminer -c cgminer.conf --disable-gpu
";
            Console.WriteLine(str);


        }
    }
}
