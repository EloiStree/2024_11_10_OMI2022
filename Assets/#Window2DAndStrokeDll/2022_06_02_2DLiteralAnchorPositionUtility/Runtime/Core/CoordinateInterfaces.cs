using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPixelValue
{
    int GetPixelValue();
    void GetPixelValue(out int pixelValue);
}
public interface IPercentValue
{
    double GetPercentValue();
    void GetPercentValue(out double percentValue);
}


public interface INeutralUnitValue
{
    double GetNeutralUnitValue();
    void GetNeutralUnitValue(out double percentValue);
}

public interface IHorizontalDirection
{
    void GetAxisHorizontalDirection(out AxisDirectionHorizontal direction);
    AxisDirectionHorizontal GetAxisHorizontalDirection();

}
public interface IVerticalDirection 
{

    void GetAxisVerticalDirection(out AxisDirectionVertical direction);
    AxisDirectionVertical GetAxisVerticalDirection();
}


public interface IDoubleAxisGet {
    public void GetHorizontalDirection(out AxisDirectionHorizontal value);
    public void GetVerticalDirection(out AxisDirectionVertical value);
}


public interface IPixelPointGet: IDoubleAxisGet
{
    public void GetHorizontalPixelValue(out int value);
    public void GetVerticalPixelValue(out int value);
}
public interface IPercentPointGet : IDoubleAxisGet
{
    public void GetHorizontalPercentValue(out double value);
    public void GetVerticalPercentValue(out double value);
}

public interface INeutralUnitPointGet : IDoubleAxisGet
{
    void GetVerticalNeutralUnitValue(out double value);
    void GetHorizontalNeutralUnitValue(out double value);
}