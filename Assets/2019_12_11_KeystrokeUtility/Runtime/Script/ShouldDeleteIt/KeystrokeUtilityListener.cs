using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WindowsInput.Native;

public class KeystrokeUtilityListener : MonoBehaviour
{
    [Header("Listen to Native")]
    public OnNativeCharacter onNativeCharacter;
    public OnNativeString onNativeText;
    public OnNativeKeyCode onNativeKeyCode;
    public OnNativeVirtualKeyCode onNativeWdindowKeyCode;
    [System.Serializable]
    public class OnNativeCharacter : UnityEvent<char> { }
    [System.Serializable]
    public class OnNativeString : UnityEvent<string> { }
    [System.Serializable]
    public class OnNativeKeyCode : UnityEvent<KeyCode, bool> { }
    [System.Serializable]
    public class OnNativeVirtualKeyCode : UnityEvent<VirtualKeyCode, bool> { }

    [Header("Listen to KeyStroke request")]
    public OnKeyboardTouchRequest onKeyDown;
    public OnKeyboardTouchRequest onKeyUp;
    public OnKeyboardCharRequest onCharDown;
    public OnKeyboardCharRequest onCharUp;
    public OnStrokeASCII onStrokeRequestASCII;
    public OnStrokeUnicode onStrokeRequestUnicode;
    public OnStrokeCharacter onStrokeRequestCharacter;
    public OnStrokeText onStrokeRequestText;
    [System.Serializable]
    public class OnKeyboardTouchRequest : UnityEvent<KeyboardTouchPressRequest> { }
    [System.Serializable]
    public class OnKeyboardCharRequest : UnityEvent<KeyboardCharPressRequest> { }
    [System.Serializable]
    public class OnStrokeASCII : UnityEvent<AsciiStrokeRequest> { }
    [System.Serializable]
    public class OnStrokeUnicode : UnityEvent<UnicodeStrokeRequest> { }
    [System.Serializable]
    public class OnStrokeCharacter : UnityEvent<CharacterStrokeRequest> { }
    [System.Serializable]
    public class OnStrokeText : UnityEvent<TextStrokeRequest> { }



    public  void OnReceivedNativeChar(char value)
    { onNativeCharacter.Invoke(value); }
    public  void OnReceivedNativeText(string value)
    { onNativeText.Invoke(value); }
    public  void OnReceivedNativeUnityKeyCode(KeyCode value, bool isDown)
    {

        onNativeKeyCode.Invoke(value, isDown); }
    public  void OnReceivedNativeWindowKeyCode(VirtualKeyCode value, bool isDown)
    {

        onNativeWdindowKeyCode.Invoke(value, isDown); }
    private void OnReceivedText(TextStrokeRequest text)
    {
        onStrokeRequestText.Invoke(text);
    }
    private void OnReceivedCharacter(CharacterStrokeRequest character)
    {
        onStrokeRequestCharacter.Invoke(character);
    }
    private void OnReceivedCharUp(KeyboardCharPressRequest touch)
    {
        onCharUp.Invoke(touch);

    }
    private void OnReceivedCharDown(KeyboardCharPressRequest touch)
    {
        onCharDown.Invoke(touch);
    }
    private void OnReceivedUnicode(UnicodeStrokeRequest codeNumber)
    {
        onStrokeRequestUnicode.Invoke(codeNumber);
    }
    private void OnReceivedASCII(AsciiStrokeRequest codeNumber)
    {
        onStrokeRequestASCII.Invoke(codeNumber);
    }
    private void OnReceivedKeyUp(KeyboardTouchPressRequest touch)
    {
        onKeyUp.Invoke(touch);
    }
    private void OnReceivedKeyDown(KeyboardTouchPressRequest touch)
    {
        onKeyDown.Invoke(touch);
    }
  
}
