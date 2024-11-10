using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class SerialPortUtility2 
{


    public string[] GetSerialPortNames() {
        return SerialPort.GetPortNames();
    }

    public uint[] GetSerialPortId() {

        List<uint> ports=  new List<uint>();
        foreach (var item in GetSerialPortNames())
        {
            uint value;
            string idAsString = item.ToUpper().Replace("COM", "");
            if (uint.TryParse(idAsString, out value)){
                ports.Add(value);
            }

        }
        return ports.ToArray() ;
    }
}
