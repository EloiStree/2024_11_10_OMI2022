[System.Serializable]
public class KeyboardTouchPressRequest
{
    public KeyboardTouchPressRequest(KeyboardTouch touch, PressType pression)
    {
        m_touch = touch;
        m_pression = pression;
    }
    public KeyboardTouch m_touch;
    public PressType m_pression;
}

/// <summary>
/// Correspond of combisation of Touch press depending of the target keyboard language
/// </summary>
[System.Serializable]
public class KeyboardCharPressRequest
{

    public KeyboardCharPressRequest(char character, PressType pression)
    {
        m_character = character;
        m_pression = pression;
    }
    public char m_character;
    public PressType m_pression;
}

[System.Serializable]
public abstract class StrokeRequest
{
    public StrokeRequest(int code)
    {
        m_code = code;
    }
    public int m_code;
}

[System.Serializable]
public class UnicodeStrokeRequest : StrokeRequest
{
    public UnicodeStrokeRequest(int code) : base(code)
    {
    }
}
[System.Serializable]
public class AsciiStrokeRequest : StrokeRequest
{
    public AsciiStrokeRequest(int code) : base(code)
    {
    }
}
[System.Serializable]
public class CharacterStrokeRequest
{
    public CharacterStrokeRequest(char character)
    {
        m_character = character;
    }
    public char m_character;
}
[System.Serializable]
public class TextStrokeRequest
{
    public TextStrokeRequest(string text)
    {
        m_text = text;
    }
    public string m_text;
}
