using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;

	public class SerialPortReader: IDisposable
	{
		 
		string portName;
		string baudRate;
		string parity ;
		string dataBits;
		string stopBits;
		
		 SerialPort mySerialPort;		
		
		public void StartProcess()
		{
			ReadConfig();
			PrintToConsole();
			OpenPort();
			
		
		}
		private void OpenPort()
		{
			int baudRate_ = Convert.ToInt32(baudRate);
            Parity parity_ = (Parity)Enum.Parse(typeof(Parity), parity);
            int dataBits_ = Convert.ToInt32(dataBits);
            StopBits stopBits_ = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
            
            try
            {
             	mySerialPort = new SerialPort(portName, baudRate_, parity_, dataBits_, stopBits_);
             	if(! mySerialPort.IsOpen)
             	{
                	mySerialPort.Open();
             	}
                mySerialPort.DataReceived += new SerialDataReceivedEventHandler(MySerialPort_DataReceived);
            }
            catch (IOException ex)
            {
            	  PrintException(ex);             
            }            
		}
		
		 private  void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
               string data =mySerialPort.ReadExisting();  
				Console.WriteLine("data:  "+ data);               
            }
            catch (IOException ex)
            {
            	PrintException(ex);
            }
        }
		 
		private void ReadConfig()
		{
			  var appSettings = ConfigurationManager.AppSettings;
           
			 portName = appSettings["portName"];
             baudRate = appSettings["baudRate"];
             parity   = appSettings["parity"];
             dataBits = appSettings["dataBits"];
             stopBits = appSettings["stopBits"];
		}
		private void PrintException(Exception ex)
		{
			string exerr= "";
			exerr = exerr + "Message : "		+ ex.Message 		+ Environment.NewLine ;
			exerr = exerr + "InnerException : "	+ ex.InnerException	+ Environment.NewLine ;
			exerr = exerr + "StackTrace : "		+ ex.StackTrace		+ Environment.NewLine ;
			exerr = exerr + "Source : "			+ ex.Source			+ Environment.NewLine ;
        	Console.Write(exerr);
		}
		
		private void PrintToConsole()
		{
			Console.WriteLine("portName:  "+ portName);
			Console.WriteLine("baudRate:  "+ baudRate);
			Console.WriteLine("parity:    "+ parity);
			Console.WriteLine("dataBits:  "+ dataBits);
			Console.WriteLine("stopBits:  "+ stopBits);			
		}
		
		 ~SerialPortReader()
        {
            Dispose(false);
       
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Free managed resource here  
                mySerialPort.Close();
                Console.WriteLine("mySerialPort has disposed");
            }
            //Free unmanaged resource here            
            
        }       
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
	}