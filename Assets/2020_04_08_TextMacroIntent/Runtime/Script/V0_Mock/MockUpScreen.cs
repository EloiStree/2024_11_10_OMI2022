using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockUpScreen : MonoBehaviour
{
    public RectTransform cursor;
    public RectTransform m_globalScreen;
    public RectTransform [] m_screens;


    public void SetGlobalCursorPosition(float horizontalLeftToRight, float verticalDownToTop) {
        cursor.anchorMin = new Vector2(horizontalLeftToRight, verticalDownToTop);
        cursor.anchorMax = new Vector2(horizontalLeftToRight, verticalDownToTop);
    }

}
