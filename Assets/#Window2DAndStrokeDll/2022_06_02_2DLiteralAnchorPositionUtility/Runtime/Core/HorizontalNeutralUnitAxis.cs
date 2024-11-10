

[System.Serializable]
public class HorizontalNeutralUnitAxis : INeutralUnitValue, IHorizontalDirection
{
    public double m_value;
    public AxisDirectionHorizontal m_axisDirection;

    public void GetAxisHorizontalDirection(out AxisDirectionHorizontal direction)
    {
        direction = m_axisDirection;
    }

    public AxisDirectionHorizontal GetAxisHorizontalDirection()
    {
        return m_axisDirection;
    }

    public double GetNeutralUnitValue()
    {
        return m_value;
    }

    public void GetNeutralUnitValue(out double value)
    {
        value = m_value;
    }

}