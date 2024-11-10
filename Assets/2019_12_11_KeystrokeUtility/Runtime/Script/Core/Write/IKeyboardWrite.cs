using UnityEngine;

public interface IKeyboardWrite
{
    void RealPressDown(KeyboardTouch touch);
    void RealPressUp(KeyboardTouch touch);
    void Stroke(string text);
    void Stroke(char character);
    void Stroke(KeyboardTouchPressRequest key);
    void Stroke(KeyboardCharPressRequest character);
    void Stroke(AsciiStrokeRequest asciiCode);
    void Stroke(UnicodeStrokeRequest unicode);
}

public abstract class KeyboardWriteMono : MonoBehaviour, IKeyboardWrite
{
    public abstract void RealPressDown(KeyboardTouch touch);
    public abstract void RealPressUp(KeyboardTouch touch);
    public abstract void Stroke(string text);
    public abstract void Stroke(char character);
    public abstract void Stroke(KeyboardTouchPressRequest key);
    public abstract void Stroke(KeyboardCharPressRequest character);
    public abstract void Stroke(AsciiStrokeRequest asciiCode);
    public abstract void Stroke(UnicodeStrokeRequest unicode);
}