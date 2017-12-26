
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;


	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Serial Port - Sharp Develop - Console");
			
	 		SerialPortReader sp = new SerialPortReader();
	 		sp.StartProcess();
          
            
			Console.Write("Press any key to Exit . . . ");
			Console.ReadKey(true);
			sp.Dispose();  
		}	
		
		
		
	}
	
