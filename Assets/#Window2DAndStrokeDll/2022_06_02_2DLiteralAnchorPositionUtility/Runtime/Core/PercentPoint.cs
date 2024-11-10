
public class PercentPoint : IVerticalDirection, IHorizontalDirection, IPercentPointGet
{
    public HorizontalPercentAxis m_horizontalValue;
    public VerticalPercentAxis m_verticalValue;

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

    public void GetHorizontalPercentValue(out double value)
    {
        m_horizontalValue.GetPercentValue(out value);
    }

    public void GetVerticalDirection(out AxisDirectionVertical value)
    {
        m_verticalValue.GetAxisVerticalDirection(out value);
    }

    public void GetVerticalPercentValue(out double value)
    {
        m_verticalValue.GetPercentValue(out value);
    }
}



