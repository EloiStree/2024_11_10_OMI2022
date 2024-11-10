using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMouse2Booleans {

	void GetNorthLabel(out string label);
	void GetSouthLabel(out string label);
	void GetEastLabel(out string label);
	void GetWestLabel(out string label);

	void GetNorthWestLabel(out string label);
	void GetNorthEastLabel(out string label);
	void GetSouthWestLabel(out string label);
	void GetSouthEastLabel(out string label);

	void GetMouseMoveLabel(out string label);
	void GetMouseMoveEndDelay(out float timeInSeconds);
}


[System.Serializable]
public class Mouse2booleans: IMouse2Booleans
{
	public string m_north="";
	public string m_south = "";
	public string m_east = "";
	public string m_west = "";
	public string m_southEast = "";
	public string m_southWest = "";
	public string m_northEast = "";
	public string m_northWest = "";
	public string m_mouseMove = "";
    public string m_wheelRight = "";
    public string m_wheelLeft = "";
    public string m_wheelUp = "";
    public string m_wheelDown = "";


    public float  m_mouseMoveEndDelayInSeconds=0.1f;



    public void GetEastLabel(out string label)
    {
        label = m_east;
    }

    public void GetMouseMoveEndDelay(out float timeInSeconds)
    {
        timeInSeconds = m_mouseMoveEndDelayInSeconds;
    }

    public void GetMouseMoveLabel(out string label)
    {
        label = m_mouseMove;
    }

    public void GetNorthEastLabel(out string label)
    {
        label = m_northEast;
    }

    public void GetNorthLabel(out string label)
    {
        label = m_north;
    }

    public void GetNorthWestLabel(out string label)
    {
        label = m_northWest;
    }

    public void GetSouthEastLabel(out string label)
    {
        label = m_southEast;
    }

    public void GetSouthLabel(out string label)
    {
        label = m_south;
    }

    public void GetSouthWestLabel(out string label)
    {
        label = m_southWest;
    }

    public void GetWestLabel(out string label)
    {
        label = m_west;
    }

    public void GetWheelUp(out string label)
    {
        label = m_wheelUp;

    }
    public void GetWheelDown(out string label)
    {
        label = m_wheelDown;

    }
    public void GetWheelRight(out string label)
    {
        label = m_wheelRight;

    }
    public void GetWheelLeft(out string label)
    {
        label = m_wheelLeft;

    }
   
}