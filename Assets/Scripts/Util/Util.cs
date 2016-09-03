using UnityEngine;
using System;

public static class Util {

    private static double degreeToRadian(double angle)
    {
        return Math.PI * angle / 180.0;
    }

    public static double radiansToDegrees(double angle)
    {
        return angle * (180.0 / Math.PI);
    }

    public static double angle(Vector2 from, Vector2 to)
    {
        return radiansToDegrees(Math.Acos(Vector2.Dot(from.normalized, to.normalized)));
    }
}
