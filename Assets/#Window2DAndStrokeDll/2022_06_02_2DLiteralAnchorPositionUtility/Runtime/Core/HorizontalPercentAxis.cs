
[System.Serializable]
public class HorizontalPercentAxis : IPercentValue, IHorizontalDirection
{
    public double m_percentValue;
    public AxisDirectionHorizontal m_axisDirection;

    public void GetAxisHorizontalDirection(out AxisDirectionHorizontal direction)
    {
        direction = m_axisDirection ;
    }

    public AxisDirectionHorizontal GetAxisHorizontalDirection()
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


