using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_MockMouseUI : MonoBehaviour
{
    public MockMouseUI m_mockMouse;
    public float m_timeBetweenRandomClick=0.1f;
    void Start()
    {
        InvokeRepeating("RandomInput",0, m_timeBetweenRandomClick);
    }

    // Update is called once per frame
    void RandomInput()
    {
        m_mockMouse.SetOn(GetRandom(), Random.value<0.5f? true : false);
    }
    MockMouseUI.MouseButton GetRandom() {
        switch (Random.Range(0,10))
        {
            case 0: return MockMouseUI.MouseButton.B0;
            case 1: return MockMouseUI.MouseButton.B1;
            case 2: return MockMouseUI.MouseButton.B2;
            case 3: return MockMouseUI.MouseButton.B3;
            case 4: return MockMouseUI.MouseButton.B4;
            case 5: return MockMouseUI.MouseButton.ScCenter;
            case 6: return MockMouseUI.MouseButton.ScDown;
            case 7: return MockMouseUI.MouseButton.ScUp;
            case 8: return MockMouseUI.MouseButton.ScLeft;
            case 9: return MockMouseUI.MouseButton.ScRight;
 
            default:
                return MockMouseUI.MouseButton.B0;
        }
    }
}
