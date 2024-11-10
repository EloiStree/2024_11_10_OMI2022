using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareZoneBean { }


public interface ISquarePixelZone
{

    public void GetCornerPoint(CornerType type, out IPixelPointGet point);

}
public interface ISquarePercentZone
{

    public void GetCornerPoint(CornerType type, out IPercentPointGet point);

}

public class ISquarePixelZoneUtility {

    public static void GetCoordinateSystemDirectionFromTwoPoints(
        IPixelPointGet downLeft,
        IPixelPointGet topRight,
        out CoordinateSystemDirection directionSystem)
    {

        Eloi.E_CodeTag.ToCodeLater.Info("Technicaly from two point value, you can deduct the direction of the system.");
        Eloi.E_ThrowException.ThrowEasyToCodeButNotCodedYet();
        throw new Eloi.E_ThrowException.EasyToCodeButNotCodedYet("");
    }
    public static void GetCoordinateSystemDirectionFromTwoPoints(
     IPercentPointGet downLeft,
     IPercentPointGet topRight,
     out CoordinateSystemDirection directionSystem)
    {

        Eloi.E_CodeTag.ToCodeLater.Info("Technicaly from two point value, you can deduct the direction of the system.");
        Eloi.E_ThrowException.ThrowEasyToCodeButNotCodedYet();
        throw new Eloi.E_ThrowException.EasyToCodeButNotCodedYet("");
    }

}