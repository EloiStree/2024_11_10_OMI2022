

public class NeutralValuePoint : IVerticalDirection, IHorizontalDirection, INeutralUnitPointGet
{
    public HorizontalNeutralUnitAxis m_horizontalValue;
    public VerticalNeutralUnitAxis m_verticalValue;

    public void GetAxisHorizontalDirection(out AxisDirectionHorizontal direction)
    {
        m_horizontalValue.GetAxisHorizontalDirection(out direction);
    }

    public AxisDirectionHorizontal GetAxisHorizontalDirection()
    {
        return m_horizontalValue.GetAxisHorizontalDirection();
    }

    public void GetAxisVerticalDirection(out AxisDirectionVertical direction)
    {
        m_verticalValue.GetAxisVerticalDirection(out direction);
    }

    public AxisDirectionVertical GetAxisVerticalDirection()
    {
        return m_verticalValue.GetAxisVerticalDirection();
    }

    public void GetHorizontalDirection(out AxisDirectionHorizontal value)
    {
        m_horizontalValue.GetAxisHorizontalDirection(out value);
    }

    public void GetHorizontalNeutralUnitValue(out double percentValue)
    {
        percentValue = m_horizontalValue.m_value;
    }

    public void GetVerticalDirection(out AxisDirectionVertical value)
    {
        value = m_verticalValue.GetAxisVerticalDirection();
    }

    public void GetVerticalNeutralUnitValue(out double percentValue)
    {
        percentValue = m_verticalValue.m_value;
    }
}

