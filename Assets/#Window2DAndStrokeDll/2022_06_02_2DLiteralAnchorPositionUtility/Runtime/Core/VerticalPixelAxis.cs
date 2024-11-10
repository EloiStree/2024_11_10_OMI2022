
[System.Serializable]
public class VerticalPixelAxis : IPixelValue, IVerticalDirection
{
    public int m_pixelValue;
    public AxisDirectionVertical m_axisDirection;

    public void GetAxisVerticalDirection(out AxisDirectionVertical direction)
    {
        direction = m_axisDirection;
    }

    public AxisDirectionVertical GetAxisVerticalDirection()
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



