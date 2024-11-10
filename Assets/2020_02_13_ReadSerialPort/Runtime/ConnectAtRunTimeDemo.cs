using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectAtRunTimeDemo : MonoBehaviour
{
  //  public Dropdown m_dropdown;
  //  public int m_defaultBaud = 9600;
  //  public Image m_connectionState;
  //  public RectTransform m_whereToAdd;
  //  public GameObject m_prefabViewRunTime;
    
  //  public void Refresh()
  //  {
  //      List<string> ports = SerialPortUtility.GetListOfAvailaiblePort();
  //      m_dropdown.ClearOptions();
  //      m_dropdown.AddOptions(ports);

  //  }

  //  public void CreateConnection() {
  //      string name = m_dropdown.options[m_dropdown.value].text;
  //      GameObject newObj = new GameObject("New connection with "+name);

  //      ReadSerialPortTest reader= newObj.AddComponent<ReadSerialPortTest>();
  //      reader.SetThePortName(name);
  //      reader.SetBaud(m_defaultBaud);
  //      bool hasFailed;
  //      reader.StartTheThread(out hasFailed);
  //      m_connectionState.color = hasFailed ? Color.red : Color.green;

  //      GameObject view = Instantiate(m_prefabViewRunTime);
  //      view.transform.parent = m_whereToAdd.transform;
  ////      view.GetComponent<RectTransform>().ForceUpdateRectTransforms();
  //      ReadSerialDisplayer displayer = view.GetComponent<ReadSerialDisplayer>();
  //      displayer.m_serialPort = reader;
  //      m_whereToAdd.ForceUpdateRectTransforms();
  //  }
}
