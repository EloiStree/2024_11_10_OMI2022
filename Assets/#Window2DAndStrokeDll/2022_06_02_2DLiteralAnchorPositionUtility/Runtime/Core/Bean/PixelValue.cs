

[System.Serializable]
public class PixelValue : IPixelValue
{
    public int m_pixelValue;
    public int GetPixelValue()
    {
        return m_pixelValue;
    }
    public void GetPixelValue(out int pixelValue)
    {
        pixelValue = m_pixelValue;
    }
}
