
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class ScreenPositionsRegisterEvent : UnityEvent<ScreenPositionsRegister>
{


}

[System.Serializable]
public class ScreenPositionListEvent : UnityEvent<ScreenPixelPositionsList>
{


}
[System.Serializable]
public class ScreenZoneListEvent : UnityEvent<ScreenZonesList>
{


}

[System.Serializable]
public class ScreenZoneEvent : UnityEvent<ScreenZoneFullRecord>
{


}
[System.Serializable]
public class ScreenPositionEvent : UnityEvent<ScreenPositionFullRecord>
{


}

public class ListCollection<T> {

    public List<T> m_elements = new List<T>();

    public void ClearAll()
    {
        m_elements.Clear();
    }

    public int GetCount()
    {
        return m_elements.Count;
    }

    public void Add(T position)
    {
        m_elements.Add(position);
    }

    public T Get(int index)
    {
        return m_elements[index];
    }

    public void AddAll(IEnumerable<T> positions)
    {
        m_elements.AddRange(positions);

    }
    public void SetWith(IEnumerable<T> positions)
    {
        m_elements.Clear();
        AddAll(positions);

    }
    public void GetDuplication(out T[] positions)
    {
        positions = m_elements.ToArray();
    }
    public void GetDuplication(out List<T> positions)
    {
        positions = m_elements.ToList();
    }
}

[System.Serializable]
public class ScreenZonesList: ListCollection<ScreenZoneFullRecord> {
   
}


[System.Serializable]
public class ScreenPixelPositionsList: ListCollection<ScreenPositionAsPixel> {

}

[System.Serializable]
public class ScreenZoneFullRecord
{
    public string m_givenNamed = "";
    public ScreenZoneInPourcentBean m_zoneInPourcent = new ScreenZoneInPourcentBean();
    public MainScreenDimensionBean m_mainScreenDimention = new MainScreenDimensionBean(0,0);

}

public class ScreenPositionsUtility {
    [Header("Debug")]
    public static Rect m_rectCanvas;
    public static Rect m_rectZone;
    public static Bounds m_zoneInCanvas;
    public static float m_widthCanvas;
    public static float m_heightCanvas;
    public static float m_widthInPx;
    public static float m_heightInPx;
    public static float m_botZone;
    public static float m_leftZone;
    public static float m_topZone;
    public static float m_rightZone;
    public static  void GetZone(Canvas root, RectTransform panel, out ScreenZoneFullRecord zone, string givenName="")
    { 

        zone = new ScreenZoneFullRecord();
        m_rectCanvas = root.pixelRect;
        m_rectZone = RectTransformUtility.PixelAdjustRect(panel, root);
        m_zoneInCanvas = RectTransformUtility.CalculateRelativeRectTransformBounds(root.transform, panel);



        m_widthCanvas = (int)m_rectCanvas.width;
        m_heightCanvas = (int)m_rectCanvas.height;
        m_widthInPx = m_zoneInCanvas.extents.x * 2f;
        m_heightInPx = m_zoneInCanvas.extents.y * 2f;

        m_leftZone = m_zoneInCanvas.center.x + m_widthCanvas / 2f - m_zoneInCanvas.extents.x;
        m_botZone = m_zoneInCanvas.center.y + m_heightCanvas / 2f - m_zoneInCanvas.extents.y;
        m_rightZone = m_zoneInCanvas.center.x + m_widthCanvas / 2f + m_zoneInCanvas.extents.x;
        m_topZone = m_zoneInCanvas.center.y + m_heightCanvas / 2f + m_zoneInCanvas.extents.y;

        zone.m_givenNamed = givenName;
        zone.m_mainScreenDimention.SetWidth((int)m_widthCanvas);
        zone.m_mainScreenDimention.SetHeight((int)m_heightCanvas);
        zone.m_zoneInPourcent.SetWith(
         m_leftZone / m_widthCanvas,
         m_botZone / m_heightCanvas,
         m_topZone / m_heightCanvas,
         m_rightZone / m_widthCanvas
        );

    }


    public ScreenPositionAsPixel GetAsPixel(int leftToRight, int botToTop, int width, int height, string name = "")
    {
        return new ScreenPositionAsPixel()
        {
            m_name = name,
            m_pixel = new ScreenPositionInPixelBean(leftToRight, botToTop),
            m_mainScreenDimention = new MainScreenDimensionBean(width, height)
        };
    }
    public ScreenPositionAsPourcent GetAsPourcent(float leftToRightPct, float botToTopPct, int width, int height, string name = "")
    {
        return new ScreenPositionAsPourcent()
        {
            m_name = name,
            m_pourcent = new ScreenPositionInPourcentBean(leftToRightPct, botToTopPct),
            m_mainScreenDimention = new MainScreenDimensionBean(width, height)
        };
    }

}

[System.Serializable]
public abstract  class ScreenPositionFullRecord : Namable
{
    public MainScreenDimensionBean m_mainScreenDimention = new MainScreenDimensionBean(0,0);
    public abstract ScreenPositionInPixelBean GetAsPixel();
    public abstract ScreenPositionInPourcentBean GetAsPourcent();

}
[System.Serializable]
public class ScreenPositionAsPixel : ScreenPositionFullRecord
{
    public ScreenPositionInPixelBean m_pixel = new ScreenPositionInPixelBean(0,0);

    public override ScreenPositionInPixelBean GetAsPixel()
    {
        return m_pixel;
    }

    public override ScreenPositionInPourcentBean GetAsPourcent()
    {
        return m_mainScreenDimention.GetPourcentPosition(m_pixel);
    }
}

[System.Serializable]
public class NamedScreenPourcentPosition : Namable {

    public ScreenPositionInPourcentBean m_position = new ScreenPositionInPourcentBean(0, 0);

    public NamedScreenPourcentPosition() { }
    public NamedScreenPourcentPosition(string name, float leftRightPourcent, float downTopPourcent)
    {
        SetName(name);
        m_position.SetBotToTopValue(downTopPourcent);
        m_position.SetLeftToRightValue(leftRightPourcent);
    }
}

[System.Serializable]
public class ScreenPositionAsPourcent : ScreenPositionFullRecord
{
    public ScreenPositionInPourcentBean m_pourcent = new ScreenPositionInPourcentBean(0, 0);

    public override ScreenPositionInPixelBean GetAsPixel()
    {
       return  m_mainScreenDimention.GetPixelPosition(m_pourcent);
    }

    public override ScreenPositionInPourcentBean GetAsPourcent()
    {
        return m_pourcent;
    }
}




[System.Serializable]
public class ScreenPositionInPixelBean
{

    [SerializeField] ScreenDirectionFormat m_direction = ScreenDirectionFormat.D2TL2R;
    [SerializeField] float m_cursorBotToTopInPx;
    [SerializeField] float m_cursorLeftToRightInPx;

    public ScreenPositionInPixelBean(int leftToRight, int botToTop)
    {
        this.m_cursorLeftToRightInPx = leftToRight;
        this.m_cursorBotToTopInPx = botToTop;
    }

    public void SetLeftToRightValue(int value)
    {
        m_cursorLeftToRightInPx = value;
    }

    public void SetBotToTopValue(int value)
    {
        m_cursorBotToTopInPx = value;
    }

    public float GetBotToTopValue()
    {
        return m_cursorBotToTopInPx;
    }

    public float GetLeftToRightValue()
    {
        return m_cursorLeftToRightInPx;
    }

    public ScreenDirectionFormat GetDirection()
    {
        return m_direction;
    }
    public void SetDirection(ScreenDirectionFormat direction)
    {
        m_direction = direction;
    }
}
[System.Serializable]
public class ScreenPositionInPourcentBean
{
    [SerializeField] ScreenDirectionFormat m_direction = ScreenDirectionFormat.D2TL2R;
    [SerializeField] float m_cursorBotToTopInPct;
    [SerializeField] float m_cursorLeftToRightInPct;

    public ScreenPositionInPourcentBean(float leftToRightPct, float botToTopPct)
    {
        m_cursorLeftToRightInPct = leftToRightPct;
        m_cursorBotToTopInPct = botToTopPct;
    }

    public float GetLeftToRightValue()
    {
        return m_cursorLeftToRightInPct;
    }

    public float GetBotToTopValue()
    {
        return m_cursorBotToTopInPct;
    }

    public void SetLeftToRightValue(float valueInPct)
    {
        m_cursorLeftToRightInPct = valueInPct;
    }

    public void SetBotToTopValue(float valueInPct)
    {
        m_cursorBotToTopInPct = valueInPct;
    }
    public ScreenDirectionFormat GetDirection()
    {
        return m_direction;
    }
    public void SetDirection(ScreenDirectionFormat direction)
    {
        m_direction = direction;
    }
}



public interface IGetScreenInformation
{

    ScreenPositionInPixelBean GetPixelPosition(ScreenPositionInPourcentBean pourcent);
    ScreenPositionInPourcentBean GetPourcentPosition(ScreenPositionInPixelBean pixel);
}

[System.Serializable]
public class GlobalScreenDimensionBean : IGetScreenInformation
{

    public ScreenDirectionFormat m_direction = ScreenDirectionFormat.D2TL2R;
    [SerializeField] int m_minWidthPixel;
    [SerializeField] int m_maxWidthPixel;
    [SerializeField] int m_minHeightPixel;
    [SerializeField] int m_maxHeightPixel;

    public void AddPointAllowedZone(int pixelWidthPosition, int pixelHeightPosition)
    {
        if (pixelWidthPosition < m_minWidthPixel)
            m_minWidthPixel = pixelWidthPosition;
        if (pixelWidthPosition > m_maxWidthPixel)
            m_maxWidthPixel = pixelWidthPosition;
        if (pixelHeightPosition < m_minHeightPixel)
            m_minHeightPixel = pixelHeightPosition;
        if (pixelHeightPosition > m_maxHeightPixel)
            m_maxHeightPixel = pixelHeightPosition;
    }

    public ScreenPositionInPixelBean GetPixelPosition(ScreenPositionInPourcentBean pourcent)
    {
        //Deal with direction later !!!
        ScreenPositionInPixelBean posAsPixel = new ScreenPositionInPixelBean(0,0);
        posAsPixel.SetLeftToRightValue(
            (int)Mathf.Lerp((float)m_minWidthPixel, (float)m_maxWidthPixel, pourcent.GetLeftToRightValue())
            );
        posAsPixel.SetBotToTopValue(
            (int)Mathf.Lerp((float)m_minHeightPixel, (float)m_maxHeightPixel, pourcent.GetBotToTopValue())
            );
        return posAsPixel;
    }

    public ScreenPositionInPourcentBean GetPourcentPosition(ScreenPositionInPixelBean pixel)
    {
        //Deal with direction later !!!
        ScreenPositionInPourcentBean posAsPourcent = new ScreenPositionInPourcentBean(0,0) ;
        posAsPourcent.SetLeftToRightValue(
            GetPourcentOfPixel(pixel.GetLeftToRightValue(), m_minWidthPixel, m_maxWidthPixel));
        posAsPourcent.SetBotToTopValue(
            GetPourcentOfPixel(pixel.GetBotToTopValue(), m_minHeightPixel, m_maxHeightPixel));
        return posAsPourcent;
    }

    private float GetPourcentOfPixel(float pixel, float min, float max)
    {
        return (pixel - min) / max - min;
    }
}
[System.Serializable]
public class MainScreenDimensionBean : IGetScreenInformation
{

    public ScreenDirectionFormat m_direction = ScreenDirectionFormat.D2TL2R;
    [SerializeField] int m_screenWidth;
    [SerializeField] int m_screenHeight;

    public MainScreenDimensionBean(int width, int height)
    {
        this.m_screenWidth = width;
        this.m_screenHeight = height;
    }

    public void SetDimension(int width, int height)
    {
        m_screenWidth = width;
        m_screenHeight = height;
    }
    public void SetWidth(int width)
    {
        m_screenWidth = width;
    }
    public void SetHeight(int height)
    {
        m_screenHeight = height;
    }


    public ScreenPositionInPixelBean GetPixelPosition(ScreenPositionInPourcentBean pourcent)
    {
        //Deal with direction later !!!
        ScreenPositionInPixelBean posAsPixel = new ScreenPositionInPixelBean(0,0);
        posAsPixel.SetLeftToRightValue((int)(m_screenWidth * pourcent.GetLeftToRightValue()));
        posAsPixel.SetBotToTopValue((int)(m_screenWidth * pourcent.GetBotToTopValue()));
        return posAsPixel;
    }

    public ScreenPositionInPourcentBean GetPourcentPosition(ScreenPositionInPixelBean pixel)
    {
        //Deal with direction later !!!
        ScreenPositionInPourcentBean posAsPourcent = new ScreenPositionInPourcentBean(0,0);
        posAsPourcent.SetLeftToRightValue((float)pixel.GetLeftToRightValue() / (float)m_screenWidth);
        posAsPourcent.SetBotToTopValue((float)pixel.GetBotToTopValue() / (float)m_screenHeight);
        return posAsPourcent;
    }
}
public enum ScreenDirectionFormat { D2TL2R, T2DL2R, D2TR2L, T2DR2L }