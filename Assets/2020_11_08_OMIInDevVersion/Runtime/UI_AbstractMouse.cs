using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AbstractMouse : MonoBehaviour
{
    public MouseInformationAbstract m_mouseInforamtion;
    public Text m_whereToDisplay;
    public string format = "LR{0}/{2} {4}% : DT{1}/{3} {5}%";
   
    void Update()
    {
        int x, y;
        int width= m_mouseInforamtion.GetScreenWidth()
            , height= m_mouseInforamtion.GetScreenHeight();
        m_mouseInforamtion.GetMousePositionOnScreen(out y, out x);
        if(width!=0 && height!=0)
       m_whereToDisplay.text =string.Format( format,x, y, width, height, x/(float)width, y/ (float)height);
        
    }
}
