using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static MouseOperations;

public class SendKeyStrokeToComputerDLL
{
    [Flags]
    public enum SendInputEventType : uint
    {
        InputMouse,
        InputKeyboard,
        InputHardware
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        ///?? not sure of the lib to use
        public MouseEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBOARDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct MOUSEANDKEYBOARDINPUT
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;

        [FieldOffset(0)]
        public KEYBOARDINPUT ki;

        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public SendInputEventType type;
        public MOUSEANDKEYBOARDINPUT mkhi;
    }

    public class User32 { 
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);
    }


    public static void KeyPressAndRelease(User32KeyboardStrokeCodeEnum keyCode)
    {
        INPUT input = new INPUT
        {
            type = SendInputEventType.InputKeyboard,
            mkhi = new MOUSEANDKEYBOARDINPUT
            {
                ki = new KEYBOARDINPUT
                {
                    wVk = (ushort)keyCode,
                    wScan = 0,
                    dwFlags = 0, // if nothing, key down
                    time = 0,
                    dwExtraInfo = IntPtr.Zero,
                }
            }
        };

        INPUT input2 = new INPUT
        {
            type = SendInputEventType.InputKeyboard,
            mkhi = new MOUSEANDKEYBOARDINPUT
            {
                ki = new KEYBOARDINPUT
                {
                    wVk = (ushort)keyCode,
                    wScan = 0,
                    dwFlags = 2, // key up
                    time = 0,
                    dwExtraInfo = IntPtr.Zero,
                }
            }
        };

        INPUT[] inputs = new INPUT[] { input, input2 }; // Combined, it's a keystroke
        User32.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
    }




    public static void KeyPress(User32KeyboardStrokeCodeEnum keyCode)
    {
        INPUT input = new INPUT
        {
            type = SendInputEventType.InputKeyboard,
            mkhi = new MOUSEANDKEYBOARDINPUT
            {
                ki = new KEYBOARDINPUT
                {
                    wVk = (ushort)keyCode,
                    wScan = 0,
                    dwFlags = 0, // if nothing, key down
                    time = 0,
                    dwExtraInfo = IntPtr.Zero,
                }
            }
        };

       

        INPUT[] inputs = new INPUT[] { input }; // Combined, it's a keystroke
        User32.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
    }




    public static void KeyRelease(User32KeyboardStrokeCodeEnum keyCode)
    {
       

        INPUT input2 = new INPUT
        {
            type = SendInputEventType.InputKeyboard,
            mkhi = new MOUSEANDKEYBOARDINPUT
            {
                ki = new KEYBOARDINPUT
                {
                    wVk = (ushort)keyCode,
                    wScan = 0,
                    dwFlags = 2, // key up
                    time = 0,
                    dwExtraInfo = IntPtr.Zero,
                }
            }
        };

        INPUT[] inputs = new INPUT[] {  input2 }; // Combined, it's a keystroke
        User32.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
    }

    public static void Past(bool removeV = false)
    {
        KeyPress(User32KeyboardStrokeCodeEnum.LCONTROL);
        KeyPress(User32KeyboardStrokeCodeEnum.KEY_V);
        KeyRelease(User32KeyboardStrokeCodeEnum.KEY_V);
        KeyRelease(User32KeyboardStrokeCodeEnum.LCONTROL);
        if (removeV)
        {
            KeyPressAndRelease(User32KeyboardStrokeCodeEnum.BACKSPACE);
            KeyPressAndRelease(User32KeyboardStrokeCodeEnum.BACKSPACE);
        }
    }
    public static void EnterPastDirect(bool removeV = false)
    {
        KeyPressAndRelease(User32KeyboardStrokeCodeEnum.ENTER);
        KeyPress(User32KeyboardStrokeCodeEnum.KEY_V);
        KeyRelease(User32KeyboardStrokeCodeEnum.KEY_V);
        KeyRelease(User32KeyboardStrokeCodeEnum.LCONTROL);
        if (removeV)
        {
            KeyPressAndRelease(User32KeyboardStrokeCodeEnum.BACKSPACE);
            KeyPressAndRelease(User32KeyboardStrokeCodeEnum.BACKSPACE);
        }
        KeyPressAndRelease(User32KeyboardStrokeCodeEnum.ENTER);
    }
}