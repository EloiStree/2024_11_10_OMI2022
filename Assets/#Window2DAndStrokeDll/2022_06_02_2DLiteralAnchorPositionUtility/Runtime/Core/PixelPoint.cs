
public class PixelPoint :  IVerticalDirection, IHorizontalDirection, IPixelPointGet
{
    public HorizontalPixelAxis m_horizontalValue;
    public VerticalPixelAxis m_verticalValue;

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

    public void GetHorizontalPixelValue(out int value)
    {
        m_horizontalValue.GetPixelValue(out value);
    }

    public void GetVerticalDirection(out AxisDirectionVertical value)
    {
        m_verticalValue.GetAxisVerticalDirection(out value);
    }

    public void GetVerticalPixelValue(out int value)
    {
        m_verticalValue.GetPixelValue(out value);
    }
}
