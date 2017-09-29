using UnityEngine;

public static class ExtensionMethods {
    public static IntVector2 toInt(this Vector2 vector2) {
        return new IntVector2(
            Mathf.RoundToInt(vector2.x),
            Mathf.RoundToInt(vector2.y)
        );
    }
}