using System;

public class RandomRotation : Rotation {

  private float interval;

  public RandomRotation(float interval = 360) {
    this.interval = interval;
  }

  public override float value() {
    int n = (int) Math.Ceiling(360 / interval);
    return UnityEngine.Random.Range (0, n) * interval;
  }
}
