using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mth {

    public static float Map(float x,float inMin,float inMax,float outMin,float outMax) {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public static Vector2 getVCoordinates(Vector2 v,float d,float a) {
        return new Vector2(v.x + d * Mathf.Cos(a),v.y + d * Mathf.Sin(a));
    }

    public static float getAngleFromCenter(Vector2 v, Vector2 ScreenSize) {
        return Mathf.Atan2(v.y - ScreenSize.y / 2,v.x - ScreenSize.x / 2);
    }

   
}
