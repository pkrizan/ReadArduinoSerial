using System;

namespace ReadArduinoSerial
{
  class ArduinoData
  {
    public static float[] Quaternion { get; set; }
    public int[] FingerBend { get; set; }
    public  float[] ReadPortQuaternion(string reading)
    {
      var q = new float[4];
 
        string[] inputStringArr = reading.Split(',');
        if (inputStringArr.Length >= 5)
        {
          q[0] = DecodeFloat(inputStringArr[0]);
          q[1] = DecodeFloat(inputStringArr[1]);
          q[2] = DecodeFloat(inputStringArr[2]);
          q[3] = DecodeFloat(inputStringArr[3]);
        }

      Quaternion = q;
      return Quaternion;
    }

    private float DecodeFloat(string data)
    {

      byte[] inData = new byte[4];

      if (data.Length == 8)
      {
        inData[0] = (byte)Convert.ToInt32(data.Substring(0, 2), 16);
        inData[1] = (byte)Convert.ToInt32(data.Substring(2, 2), 16);
        inData[2] = (byte)Convert.ToInt32(data.Substring(4, 2), 16);
        inData[3] = (byte)Convert.ToInt32(data.Substring(6, 2), 16);
      }

      var intbits = (inData[3] << 24) | ((inData[2] & 0xff) << 16) | ((inData[1] & 0xff) << 8) | (inData[0] & 0xff);
      var bytes = BitConverter.GetBytes(intbits);
      var floatbits = BitConverter.ToSingle(bytes, 0);
      return floatbits;
    }
  }
}
