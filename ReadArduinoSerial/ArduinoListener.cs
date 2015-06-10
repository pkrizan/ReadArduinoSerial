namespace ReadArduinoSerial
{
  using System;
  using System.IO.Ports;

  class ArduinoListener
  {
    private SerialPort MySerialPort { get; set; }

    public static void Main()
    {
      var arduinoListener = new ArduinoListener();
 
      arduinoListener.InitCommunication("COM5", 9600);
      Console.WriteLine("Press any key to continue...");
      Console.WriteLine();
      Console.ReadKey();
      arduinoListener.CloseCommunication();
    }

    private static void DataReceivedHandler( object sender,SerialDataReceivedEventArgs e)
    {
      SerialPort sp = (SerialPort)sender;
      while (sp.IsOpen)
      {
        var arduinoData = new ArduinoData();
        string indata = sp.ReadLine();
        var sensorData = arduinoData.ReadPortQuaternion(indata);
        Console.WriteLine("Data Received:");
        Console.Write(" ");
        foreach (var item in sensorData)
        {
          Console.Write(item);
          Console.Write(" ");
        }
      }
    }

    public void InitCommunication(string comPort, int baudRate)
    { 
      //Add COM port & baud rate
      MySerialPort = new SerialPort(comPort, baudRate);
      MySerialPort.DataReceived += DataReceivedHandler;
      try
      {
        MySerialPort.Open();
      }
      catch (Exception e)
      {
        Console.WriteLine("An error occurred: '{0}'", e);
      }
    }

    public void CloseCommunication()
    {
      try
      {
        MySerialPort.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine("An error occurred: '{0}'", e);
      }
    }
  }
}
