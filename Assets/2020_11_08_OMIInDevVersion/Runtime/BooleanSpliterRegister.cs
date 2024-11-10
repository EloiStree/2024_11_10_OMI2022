using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanSpliterRegister : MonoBehaviour
{
    public List<DuoSpliter> m_duo = new List<DuoSpliter>();
    public List<TrioLineSpliter> m_trio = new List<TrioLineSpliter>();
    public List<QuatroLineSpliter> m_quatro = new List<QuatroLineSpliter>();
    public List<FingerSpliter> m_fingers = new List<FingerSpliter>();
    public List<DiamondSpliter> m_diamond = new List<DiamondSpliter>();
    public List<SquareSpliter> m_square = new List<SquareSpliter>();
    public List<Joystick2DSpliter> m_joystick2D = new List<Joystick2DSpliter>();
    public List<Joystick3DSpliter> m_joystick3D = new List<Joystick3DSpliter>();

    public void Clear()
    {
        m_duo.Clear(); 
        m_trio.Clear();
        m_quatro.Clear();
        m_fingers.Clear();
        m_diamond.Clear();
        m_square.Clear();
        m_joystick2D.Clear();
        m_joystick3D.Clear();

    }

    public void Add(DuoSpliter value)
    {
        m_duo.Add(value);
    }
    public void Add(TrioLineSpliter value)
    {
        m_trio.Add(value);
    }
    public void Add(QuatroLineSpliter value)
    {
        m_quatro.Add(value);
    }
    public void Add(FingerSpliter value)
    {
        m_fingers.Add(value);
    }
    public void Add(DiamondSpliter value)
    {
        m_diamond.Add(value);
    }
    public void Add(SquareSpliter value)
    {
        m_square.Add(value);
    }
    public void Add(Joystick2DSpliter value)
    {
        m_joystick2D.Add(value);
    }
    public void Add(Joystick3DSpliter value)
    {
        m_joystick3D.Add(value);
    }
}


public abstract class Spliter
{
    public string m_name="";
    //public string m_description="";

   // public abstract List<string> GetInputsCondition();
   // public abstract List<string> GetOutputBooleanId();

    public string GetName() { return m_name; }
    //public string GetDescription() { return m_description; }
    public Spliter()
    {
    }
    public Spliter(string name)
    {
        m_name = name;
    }
    //public Spliter(string name, string description) {
    //    m_name = name;
    //    m_description = description;
    //}
    public void SetName(string name)
    {
        m_name = name;
    }
    //public void SetDescription(string description)
    //{
    //    m_description = description;
    //}
}

[System.Serializable]
public class DiamondSpliter : Spliter
{
    public DiamondSpliter(string forward, string backward, string left, string right)
    {

        m_forwardCondition = forward;
        m_backwardCondition = backward;
        m_leftCondition = left;
        m_rightCondition = right;
    }
    public string m_forwardCondition;
    public string m_backwardCondition;
    public string m_leftCondition;
    public string m_rightCondition;

    public string m_forwardRenamed;
    public string m_backwardRenamed;
    public string m_leftRenamed;
    public string m_rightRenamed;

    public string m_none;
    public string m_all;

    public string m_ne;
    public string m_nw;
    public string m_se;
    public string m_sw;

    public string m_fullleft;
    public string m_fullright;
    public string m_fulldown;
    public string m_fullup;

    public string m_vertical;
    public string m_horizontal;

}
[System.Serializable]
public class Joystick3DSpliter : Spliter
{
    public Joystick3DSpliter(string forward, string backward, string left, string right, string up, string down)
    {

        m_forwardCondition = forward;
        m_backwardCondition = backward;
        m_leftCondition = left;
        m_rightCondition = right;
        m_upCondition = up;
        m_downRenamed = down;
    }
    public string m_forwardCondition;
    public string m_backwardCondition;
    public string m_leftCondition;
    public string m_rightCondition;
    public string m_upCondition;
    public string m_downRenamed;

    public string[] m_direcional = new string[27];
    public string m_leftRenamed;
    public string m_rightRenamed;
    public string m_backwardRenamed;
    public string m_upRenamed;
    public string m_forwardRenamed;
    public string m_none;

    public enum Direction : int { N=0, NE = 1, NW = 2, S = 3, SW = 4, SE = 5, E = 6, W = 7 , C=8}
    public enum Height : int { Up=0, Middle = 1, Down =2  }

    public string Get(Height height, Direction direction) { return m_direcional[GetIndex(height, direction)]; }
    public void Set(Height height, Direction direction, string name) { m_direcional[GetIndex(height, direction)]= name; }


    public int GetIndex(Height height, Direction direction) { return (int)direction + (9 * (int)height); }

    public string Get(Joystick3DEnum selection)
    {
        switch (selection)
        {
            case Joystick3DEnum.TN: return Get(Height.Up,      Direction.N);
            case Joystick3DEnum.TS: return Get(Height.Up,      Direction.S);
            case Joystick3DEnum.TW: return Get(Height.Up,      Direction.W);
            case Joystick3DEnum.TE: return Get(Height.Up,      Direction.E);
            case Joystick3DEnum.TNE: return Get(Height.Up,     Direction.NE);
            case Joystick3DEnum.TNW: return Get(Height.Up,     Direction.NW);
            case Joystick3DEnum.TSE: return Get(Height.Up,     Direction.SE);
            case Joystick3DEnum.TSW: return Get(Height.Up, Direction.SW);
            case Joystick3DEnum.TC: return Get(Height.Up, Direction.C);

            case Joystick3DEnum.DN: return Get(Height.Down,    Direction.N);
            case Joystick3DEnum.DS: return Get(Height.Down,    Direction.S);
            case Joystick3DEnum.DW: return Get(Height.Down,    Direction.W);
            case Joystick3DEnum.DE: return Get(Height.Down,    Direction.E);
            case Joystick3DEnum.DNE: return Get(Height.Down,   Direction.NE);
            case Joystick3DEnum.DNW: return Get(Height.Down,   Direction.NW);
            case Joystick3DEnum.DSE: return Get(Height.Down,   Direction.SE);
            case Joystick3DEnum.DSW: return Get(Height.Down, Direction.SW);
            case Joystick3DEnum.DC: return Get(Height.Down, Direction.C);

            case Joystick3DEnum.MN: return Get(Height.Middle,  Direction.N);
            case Joystick3DEnum.MS: return Get(Height.Middle,  Direction.S);
            case Joystick3DEnum.MW: return Get(Height.Middle,  Direction.W);
            case Joystick3DEnum.ME: return Get(Height.Middle,  Direction.E);
            case Joystick3DEnum.MNE: return Get(Height.Middle, Direction.NE);
            case Joystick3DEnum.MNW: return Get(Height.Middle, Direction.NW);
            case Joystick3DEnum.MSE: return Get(Height.Middle, Direction.SE);
            case Joystick3DEnum.MSW: return Get(Height.Middle, Direction.SW);
            case Joystick3DEnum.MC: return Get(Height.Middle, Direction.C);

            case Joystick3DEnum.Neutral:
                return m_none;
            default:
                return m_none;
        }
    }
}
[System.Serializable]
public class Joystick2DSpliter : Spliter
{
    public Joystick2DSpliter(string forward, string backward, string left, string right)
    {
        m_forwardCondition = forward;
        m_backwardCondition = backward;
        m_leftCondition = left;
        m_rightCondition = right;
    }
    public string m_forwardCondition;
    public string m_backwardCondition;
    public string m_leftCondition;
    public string m_rightCondition;

    public string m_forwardRenamed;
    public string m_backwardRenamed;
    public string m_leftRenamed;
    public string m_rightRenamed;


    public string m_none;
    public string m_ne;
    public string m_nw;
    public string m_se;
    public string m_sw;
    public string m_n;
    public string m_e;
    public string m_s;
    public string m_w;


 }

[System.Serializable]
public class SquareSpliter : Spliter
{
    public SquareSpliter(string leftTop, string rightTop, string leftDown, string rightDown)
    {
        m_leftTopCondition = leftTop;
        m_rightTopCondition = rightTop;
        m_leftDownCondition = leftDown;
        m_rightDownCondition = rightDown;
    }
    public string m_leftTopCondition;
    public string m_rightTopCondition;
    public string m_leftDownCondition;
    public string m_rightDownCondition;

    public string m_leftTopRenamed;
    public string m_rightTopRenamed;
    public string m_leftDownRenamed;
    public string m_rightDownRenamed;

    public string m_none;
    public string m_onlytopleft;
    public string m_onlytopright;
    public string m_onlydownleft;
    public string m_onlydownright;
    public string m_allexcepttopleft;
    public string m_allexcepttopright;
    public string m_allexceptdownleft;
    public string m_allexceptdownright;
    public string m_fulldown;
    public string m_fullup;
    public string m_fullleft;
    public string m_fullright;
    public string m_slashdiagonal;
    public string m_backslashdiagonal;
    public string m_all;


}




[System.Serializable]
public class DuoSpliter : Spliter
{
    public DuoSpliter(string first, string second) {
        m_firstCondition = first;
        m_secondCondition = second;
    }
    public string m_firstCondition;
    public string m_secondCondition;

    public string m_firstRenamed;
    public string m_secondRenamed;

    public string m_none;
    public string m_all;
    public string m_onlyfirst;
    public string m_onlysecond;
}


[System.Serializable]
public class TrioLineSpliter : Spliter
{
    public TrioLineSpliter(string first, string second, string third)
    {
        m_firstCondition = first;
        m_secondCondition = second;
        m_thirdCondition = third;
    }
    public string m_firstCondition;
    public string m_secondCondition;
    public string m_thirdCondition;

    public string m_firstRenamed;
    public string m_secondRenamed;
    public string m_thirdRenamed;

    public string m_ooo;
    public string m_ooi;
    public string m_oio;
    public string m_oii;
    public string m_ioo;
    public string m_ioi;
    public string m_iio;
    public string m_iii;

    public string Get(TrioBinary selection)
    {
        switch (selection)
        {
            case TrioBinary.ooo: return m_ooo;
            case TrioBinary.ooi: return m_ooi;
            case TrioBinary.oio: return m_oio;
            case TrioBinary.oii: return m_oii;
            case TrioBinary.ioo: return m_ioo;
            case TrioBinary.ioi: return m_ioi;
            case TrioBinary.iio: return m_iio;
            case TrioBinary.iii: return m_iii;
            default:
                break;
        }
        return "";
    }
}
[System.Serializable]
public class QuatroLineSpliter : Spliter
{
    public QuatroLineSpliter(string first, string second, string third,string fourth)
    {
        m_firstCondition = first;
        m_secondCondition = second;
        m_thirdCondition = third;
        m_fourthCondition = fourth;
    }
    public string m_firstCondition;
    public string m_secondCondition;
    public string m_thirdCondition;
    public string m_fourthCondition;

    public string m_firstRenamed;
    public string m_secondRenamed;
    public string m_thirdRenamed;
    public string m_fourthRenamed;

    public string m_oooo;
    public string m_oooi;
    public string m_ooio;
    public string m_ooii;
    public string m_oioo;
    public string m_oioi;
    public string m_oiio;
    public string m_oiii;
    public string m_iooo;
    public string m_iooi;
    public string m_ioio;
    public string m_ioii;
    public string m_iioo;
    public string m_iioi;
    public string m_iiio;
    public string m_iiii;

    public string Get(QuatroBinary selection)
    {
        switch (selection)
        {
            case QuatroBinary.oooo: return m_oooo;
            case QuatroBinary.oooi: return m_oooi;
            case QuatroBinary.ooio: return m_ooio;
            case QuatroBinary.ooii: return m_ooii;
            case QuatroBinary.oioo: return m_oioo;
            case QuatroBinary.oioi: return m_oioi;
            case QuatroBinary.oiio: return m_oiio;
            case QuatroBinary.oiii: return m_oiii;
            case QuatroBinary.iooo: return m_iooo;
            case QuatroBinary.iooi: return m_iooi;
            case QuatroBinary.ioio: return m_ioio;
            case QuatroBinary.ioii: return m_ioii;
            case QuatroBinary.iioo: return m_iioo;
            case QuatroBinary.iioi: return m_iioi;
            case QuatroBinary.iiio: return m_iiio;
            case QuatroBinary.iiii: return m_iiii;
            default:
                break;
        }
        return "";
    }
}
[System.Serializable]
public class FingerSpliter : Spliter
{
    public FingerSpliter(string pinky, string ring, string middle, string index, string thumb)
    {
        m_pinkyCondition = pinky;
        m_ringCondition = ring;
        m_middleCondition = middle;
        m_indexCondition = index;
        m_thumbCondition = thumb;
    }
    public string m_pinkyCondition;
    public string m_ringCondition;
    public string m_middleCondition;
    public string m_indexCondition;
    public string m_thumbCondition;

    public string m_pinkyRenamed;
    public string m_ringRenamed;
    public string m_middleRenamed;
    public string m_indexRenamed;
    public string m_thumbRenamed;

    public string m_ooooo;
    public string m_ooooi;
    public string m_oooio;
    public string m_oooii;
    public string m_ooioo;
    public string m_ooioi;
    public string m_ooiio;
    public string m_ooiii;
    public string m_oiooi;
    public string m_oioio;
    public string m_oioii;
    public string m_oiioo;
    public string m_oiooo;
    public string m_oiioi;
    public string m_oiiio;
    public string m_oiiii;
    public string m_ioooo;
    public string m_ioooi;
    public string m_iooio;
    public string m_iooii;
    public string m_ioioo;
    public string m_ioioi;
    public string m_ioiio;
    public string m_ioiii;
    public string m_iiooo;
    public string m_iiooi;
    public string m_iioio;
    public string m_iioii;
    public string m_iiioo;
    public string m_iiioi;
    public string m_iiiio;
    public string m_iiiii;

    public string Get(FingerBinary selection)
    {
        switch (selection)
        {
            case FingerBinary.ooooo: return m_ooooo;
            case FingerBinary.ooooi: return m_ooooi;
            case FingerBinary.oooio: return m_oooio;
            case FingerBinary.oooii: return m_oooii;
            case FingerBinary.ooioo: return m_ooioo;
            case FingerBinary.ooioi: return m_ooioi;
            case FingerBinary.ooiio: return m_ooiio;
            case FingerBinary.ooiii: return m_ooiii;
            case FingerBinary.oiooo: return m_oiooo;
            case FingerBinary.oiooi: return m_oiooi;
            case FingerBinary.oioio: return m_oioio;
            case FingerBinary.oioii: return m_oioii;
            case FingerBinary.oiioo: return m_oiioo;
            case FingerBinary.oiioi: return m_oiioi;
            case FingerBinary.oiiio: return m_oiiio;
            case FingerBinary.oiiii: return m_oiiii;

            case FingerBinary.ioooo: return m_ioooo;
            case FingerBinary.ioooi: return m_ioooi;
            case FingerBinary.iooio: return m_iooio;
            case FingerBinary.iooii: return m_iooii;
            case FingerBinary.ioioo: return m_ioioo;
            case FingerBinary.ioioi: return m_ioioi;
            case FingerBinary.ioiio: return m_ioiio;
            case FingerBinary.ioiii: return m_ioiii;
            case FingerBinary.iiooo: return m_iiooo;
            case FingerBinary.iiooi: return m_iiooi;
            case FingerBinary.iioio: return m_iioio;
            case FingerBinary.iioii: return m_iioii;
            case FingerBinary.iiioo: return m_iiioo;
            case FingerBinary.iiioi: return m_iiioi;
            case FingerBinary.iiiio: return m_iiiio;
            case FingerBinary.iiiii: return m_iiiii;
            default:
                break;
        }
        return "";
    }
}



public enum TrioBinary : int
{
    ooo,
    ooi,
    oio,
    oii,
    ioo,
    ioi,
    iio,
    iii,
}
public enum QuatroBinary : int
{
    oooo,
    oooi,
    ooio,
    ooii,
    oioo,
    oioi,
    oiio,
    oiii,
    iooo,
    iooi,
    ioio,
    ioii,
    iioo,
    iioi,
    iiio,
    iiii,
}
public enum FingerBinary : int
{
    ooooo,
    ooooi,
    oooio,
    oooii,
    ooioo,
    ooioi,
    ooiio,
    ooiii,
    oiooo,
    oiooi,
    oioio,
    oioii,
    oiioo,
    oiioi,
    oiiio,
    oiiii,
    ioooo,
    ioooi,
    iooio,
    iooii,
    ioioo,
    ioioi,
    ioiio,
    ioiii,
    iiooo,
    iiooi,
    iioio,
    iioii,
    iiioo,
    iiioi,
    iiiio,
    iiiii,
}
public enum Joystick3DEnum : int
{
    TN, TS, TW, TE, TNE, TNW, TSE, TSW,

    DN, DS, DW, DE, DNE, DNW, DSE, DSW,

    MN, MS, MW, ME, MNE, MNW, MSE, MSW, Neutral,
    TC,
    DC,
    MC
}