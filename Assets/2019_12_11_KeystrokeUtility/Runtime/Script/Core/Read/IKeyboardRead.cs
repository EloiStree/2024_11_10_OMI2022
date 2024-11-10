using System.Collections.Generic;
using UnityEngine;

public interface IKeyboardRead
{
    bool IsTouchActive(KeyboardTouch keyboardTouch);
    bool IsControlActive();
    bool IsShiftActive();
    bool IsMetaActive();
    bool IsAltActive();
    bool IsAltGrActive();
    bool IsCapsLockOn();
    bool IsNumLockOn();
    bool IsScrollLockOn();
    BoolHistory GetTouchHistory(KeyboardTouch keyboardTouch);
    KeyboardTouch[] GetActiveTouches();
}

public enum TouchChange { Up, Down }


public abstract class KeyboardReadMono : MonoBehaviour, IKeyboardRead
{
    public abstract KeyboardTouch[] GetActiveTouches();
    public abstract BoolHistory GetTouchHistory(KeyboardTouch keyboardTouch);
    public abstract bool IsAltActive();
    public abstract bool IsAltGrActive();
    public abstract bool IsCapsLockOn();
    public abstract bool IsControlActive();
    public abstract bool IsMetaActive();
    public abstract bool IsNumLockOn();
    public abstract bool IsScrollLockOn();
    public abstract bool IsShiftActive();
    public abstract bool IsTouchActive(KeyboardTouch keyboardTouch);
}
