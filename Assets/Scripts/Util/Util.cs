using UnityEngine;
using System;

public static class Util {

    /**
     * Converts degrees to radians
     */
    private static double degreeToRadian(double angle)
    {
        return Math.PI * angle / 180.0;
    }

    /**
     * Converts radians to degrees
     */
    public static double radiansToDegrees(double angle)
    {
        return angle * (180.0 / Math.PI);
    }

    /**
     * Returns the angle between two vectors, ranging from 0 to 180 degrees.
     */
    public static double angle(Vector2 from, Vector2 to)
    {
        return radiansToDegrees(Math.Acos(Vector2.Dot(from.normalized, to.normalized)));
    }
}
