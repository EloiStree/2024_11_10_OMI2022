

public class PercentValue : IPercentValue
{
    public double m_percentValue;
    public double GetPercentValue()
    {
        return m_percentValue;
    }
    public void GetPercentValue(out double pixelValue)
    {
        pixelValue = m_percentValue;
    }
}
