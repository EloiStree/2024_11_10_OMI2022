
[System.Serializable]
public class VerticalNeutralUnitAxis : INeutralUnitValue, IVerticalDirection
{
    public double m_value;
    public AxisDirectionVertical m_axisDirection;

    public void GetAxisVerticalDirection(out AxisDirectionVertical direction)
    {
        direction = m_axisDirection;
    }

    public AxisDirectionVertical GetAxisVerticalDirection()
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
