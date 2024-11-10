////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;


////public class UnityKeyboardState : AbstractKeyboardState
////{

////    public bool GetRealStateOf(KeyCode touch)
////    {
////        return Input.GetKey(touch);
////    }

////    public override bool GetRealStateOf(KeyboardTouch touch)
////    {
////        //       KeyCode keyCode = KeyBindingTable.ConvertKeyToUnityKey();
////        KeyCode keyCode;
////        bool isConvertable;
////        KeyBindingTable.ConvertTouchToUnityKey(touch, out keyCode, out isConvertable);
////        if (isConvertable)
////        {
////            return GetRealStateOf(keyCode);
////        }
//////        else  Debug.Log(" Can't convert touch as Keyboard: " + touch);
////        return false;
////    }

////    public override bool GetRealStateOfCapsLock()
////    {
////        return GetRealStateOf(KeyCode.CapsLock);
////    }

////    public override bool GetRealStateOfNumLock()
////    {
////        return GetRealStateOf(KeyCode.Numlock);
////    }

////    public override bool GetRealStateOfScrollLock()
////    {
////        return GetRealStateOf(KeyCode.ScrollLock);
////    }
    
////}
