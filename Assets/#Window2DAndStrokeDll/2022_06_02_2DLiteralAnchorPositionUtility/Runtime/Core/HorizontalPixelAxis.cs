using System;

[Serializable]
public class HorizontalPixelAxis : IPixelValue, IHorizontalDirection
{
    public int m_pixelValue;
    public AxisDirectionHorizontal m_axisDirection;

    public void GetAxisHorizontalDirection(out AxisDirectionHorizontal direction)
    {
        direction = m_axisDirection;
    }

    public AxisDirectionHorizontal GetAxisHorizontalDirection()
    {
        return m_axisDirection;
    }

    public int GetPixelValue()
    {
        return m_pixelValue;
    }
    public void GetPixelValue(out int pixelValue)
    {
        pixelValue = m_pixelValue;
    }
}