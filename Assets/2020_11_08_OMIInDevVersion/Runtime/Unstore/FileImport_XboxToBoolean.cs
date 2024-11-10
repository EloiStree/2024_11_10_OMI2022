using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImport_XboxToBoolean : MonoBehaviour
{
    public XInputToBooleanMono m_register;
    public void ClearRegister()
    {

        m_register.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                LoadXboxToBoolean(File.ReadAllText(filePath[i]), m_register);
            }
        }
    }

    private void LoadXboxToBoolean(string textToLoad, XInputToBooleanMono register)
    {
   

        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            tokens = new TileLine(lines[i]);
            if (tokens.GetCount() >= 3)
            {
                string index = tokens.GetValue(0).Trim();
                uint selectedXbox = 1;
                uint.TryParse(index, out selectedXbox);
                string buttonName = tokens.GetValue(1).Trim();
                string booleanName = tokens.GetValue(2).Trim();

                bool enumFound;
                if (tokens.GetCount() == 3 && selectedXbox >= 1 && selectedXbox <= 4)
                {
                    XInputBoolable button;
                    Get(buttonName, out enumFound, out button);
                    if(enumFound)
                        register.Add(selectedXbox, button, booleanName);
                }

                else if (tokens.GetCount() == 5) {

                    float minValue;
                    float maxValue;

                    if (float.TryParse(tokens.GetValue(3).Trim(), out minValue) && float.TryParse(tokens.GetValue(4).Trim(), out maxValue)) {
                        XInputFloatableValue floatable;
                        Get(buttonName, out enumFound, out floatable);
                        if (enumFound)
                            register.Add(selectedXbox, floatable, booleanName, minValue, maxValue);

                    }
                }

            }

        }


    }

    private void Get(string name, out bool enumFound, out XInputBoolable enumValue)
    {
        m_boolAlias.Get(name, out enumFound, out enumValue);
    }
    private void Get(string name, out bool enumFound, out XInputFloatableValue enumValue)
    {
        m_floatAlias.Get(name, out enumFound, out enumValue);
    }


    public GroupOfAlias<XInputBoolable> m_boolAlias = new GroupOfAlias<XInputBoolable>(
     new EnumAlias<XInputBoolable>(XInputBoolable.TriggerLeft,  "tl"),
     new EnumAlias<XInputBoolable>(XInputBoolable.TriggerRight,  "tr"),
     new EnumAlias<XInputBoolable>(XInputBoolable.SideButtonLeft,  "sbl", "bl"),
     new EnumAlias<XInputBoolable>(XInputBoolable.SideButtonRight,  "sbr", "br"),
     new EnumAlias<XInputBoolable>(XInputBoolable.ArrowLeft,  "al"),
     new EnumAlias<XInputBoolable>(XInputBoolable.ArrowRight,  "ar"),
     new EnumAlias<XInputBoolable>(XInputBoolable.ArrowDown,  "ad"),
     new EnumAlias<XInputBoolable>(XInputBoolable.ArrowUp, "au"),
     new EnumAlias<XInputBoolable>(XInputBoolable.ButtonA,  "a", "ba", "paddown", "pd"),
     new EnumAlias<XInputBoolable>(XInputBoolable.ButtonB,  "b", "bb", "padright", "pr"),
     new EnumAlias<XInputBoolable>(XInputBoolable.ButtonX,  "x", "bx", "padleft", "pl"),
     new EnumAlias<XInputBoolable>(XInputBoolable.ButtonY,  "y", "by", "padup", "pu"),
     new EnumAlias<XInputBoolable>(XInputBoolable.MenuBackButton, "m", "menu", "b", "back"),
     new EnumAlias<XInputBoolable>(XInputBoolable.StartButton, "s", "start"),
     new EnumAlias<XInputBoolable>(XInputBoolable.JoystickLeft,  "jl"),
     new EnumAlias<XInputBoolable>(XInputBoolable.JoystickRight,  "jr"));

    public GroupOfAlias<XInputFloatableValue> m_floatAlias = new GroupOfAlias<XInputFloatableValue>(
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.TriggerLeft,"tl"),
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.TriggerRight,"tr"),
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.JoystickLeftEast,"jlr","jle"),
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.JoystickLeftNorth,"jlu","jln"),
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.JoystickLeftSouth,"jld","jls"),
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.JoystickLeftWest,"jll","jlw"),
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.JoystickRightEast,"jrr","jre"),
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.JoystickRightNorth,"jru","jrn"),
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.JoystickRightSouth,"jrd","jrs"),
     new EnumAlias<XInputFloatableValue>(XInputFloatableValue.JoystickRightWest,"jrl","jrw"));
    
}


     