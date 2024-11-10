// https://json2csharp.com/xml-to-csharp

// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Omixml));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Omixml)serializer.Deserialize(reader);
// }

using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot(ElementName = "midiconnectionin")]
public class XmlMidiconnectionIN
{

	[XmlAttribute(AttributeName = "midiName")]
	public string MidiName;
}
	[System.Serializable]
	[XmlRoot(ElementName = "midiconnectionout")]
	public class XmlMidiconnectionOut
	{

		[XmlAttribute(AttributeName = "midiName")]
		public string MidiName;
	}

	[System.Serializable]
	[XmlRoot(ElementName = "analogdigitalcompressioncom")]
	public class XmlAnalogdigitalcompressioncom
	{

		[XmlAttribute(AttributeName = "comIdName")]
		public string ComIdName;

		[XmlAttribute(AttributeName = "nomenclatureName")]
		public string NomenclatureName;
	}

	[System.Serializable]
	[XmlRoot(ElementName = "omixml")]
	public class Omixml
	{

		[XmlElement(ElementName = "midiconnectionin")]
		public List<XmlMidiconnectionIN> Midiconnectionin;

		[XmlElement(ElementName = "midiconnectionout")]
		public List<XmlMidiconnectionOut> Midiconnectionout;

		[XmlElement(ElementName = "analogdigitalcompressioncom")]
		public List<XmlAnalogdigitalcompressioncom> Analogdigitalcompressioncom;


	[XmlElement(ElementName = "jomiudptarget")]
	public List<XmlJomiUdpTarget> JomiUdpTarget;

	[XmlElement(ElementName = "xomiudptarget")]
	public List<XmlXomiUdpTarget> XomiUdpTarget;


		[XmlElement(ElementName = "mouse2booleans")]
		public List<XmlMouse2booleans> Mouse2Booleans;
}

[System.Serializable]
[XmlRoot(ElementName = "jomiudptarget")]
public class XmlJomiUdpTarget
{

	[XmlAttribute(AttributeName = "idName")]
	public string JomiIdName;

	[XmlAttribute(AttributeName = "ip")]
	public string IpAddress;

	[XmlAttribute(AttributeName = "port")]
	public string portName;

}
[System.Serializable]
[XmlRoot(ElementName = "xomiudptarget")]
public class XmlXomiUdpTarget
{

	[XmlAttribute(AttributeName = "idName")]
	public string XomiIdName;

	[XmlAttribute(AttributeName = "ip")]
	public string IpAddress;

	[XmlAttribute(AttributeName = "port")]
	public string portName;

}


[System.Serializable]
[XmlRoot(ElementName = "mouse2booleans")]
public class XmlMouse2booleans
{
	// <mouse2booleans north = "M0N" south="M0S" west="M0W" east="M0E" 
	// southWest="M0SW" southEast="M0SE" northWest="M0NW" northEast="M0NE" mouseMove="M0Move" 
	// noteMovingDelay="0.1" />

	[XmlAttribute(AttributeName = "north")]
	public string m_north;

	[XmlAttribute(AttributeName = "south")]
	public string m_south;
	
	[XmlAttribute(AttributeName = "east")]
	public string m_east;
	
	[XmlAttribute(AttributeName = "west")]
	public string m_west;

	[XmlAttribute(AttributeName = "southEast")]
	public string m_southEast;
	
	[XmlAttribute(AttributeName = "southWest")]
	public string m_southWest;
	
	[XmlAttribute(AttributeName = "northEast")]
	public string m_northEast;
	
	[XmlAttribute(AttributeName = "northWest")]
	public string m_northWest;

	[XmlAttribute(AttributeName = "mouseMove")]
	public string m_mouseMove;


	[XmlAttribute(AttributeName = "mouseMoveEndDelay")]
	public float m_mouseMoveEndDelayInSeconds = 0.1f;


	[XmlAttribute(AttributeName = "wheelLeft")]
	public string m_wheelLeft;
	[XmlAttribute(AttributeName = "wheelUp")]
	public string m_wheelUp;
	[XmlAttribute(AttributeName = "wheelDown")]
	public string m_wheelDown;
	[XmlAttribute(AttributeName = "wheelRight")]
	public string m_wheelRight;
}