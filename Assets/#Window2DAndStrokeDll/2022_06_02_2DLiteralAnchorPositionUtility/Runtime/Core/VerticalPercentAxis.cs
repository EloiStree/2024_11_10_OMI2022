
[System.Serializable]
public class VerticalPercentAxis : IPercentValue, IVerticalDirection
{
    public double m_percentValue;
    public AxisDirectionVertical m_axisDirection;

    public void GetAxisVerticalDirection(out AxisDirectionVertical direction)
    {
        direction = m_axisDirection;
    }

    public AxisDirectionVertical GetAxisVerticalDirection()
    {
        return m_axisDirection;
    }

    public double GetPercentValue()
    {
        return m_percentValue;
    }
    public void GetPercentValue(out double pixelValue)
    {
        pixelValue = m_percentValue;
    }
}
