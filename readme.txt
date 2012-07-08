FPGA Port Scanner BTC Mining Utility v. 0.39 by infamousDutch

This code is provided entirely free of charge by the programmer in his spare
time so donations would be greatly appreciated. Please consider donating to the
address below.

1M8VMoxLoDXJMAR1xM189Amo7UtofuE5Ff

This software is for miners mining on Windows that would like to use CGMiner or BFGMiner and enable it to scan for idle FPGA devices.

Scans for idle FPGAs (currently only Bitforce) every 2 minutes and fires up a miner to start mining.
Currently supports cgminer and bfgminers.
Takes one parameter, the miner and mining parameters you wish to start up when a port is found to be open.

Example: fpgaScanner cgminer -c cgminer.conf --disable-gpu

Just copy fpgascanner.exe to the same folder as cgminer or bfgminer and start it with your miners configuration.

Software Requirements
Windows XP or newer
.NET Framework 4.0 Runtime   
