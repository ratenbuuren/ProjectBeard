using System;

public class RandomRotation : Rotation {

  private float angleInterval;

  public RandomRotation(float angleInterval = 360) {
    this.angleInterval = angleInterval;
  }

  public override float value() {
    int numOptions = (int) Math.Ceiling(360 / angleInterval);
    return UnityEngine.Random.Range (0, numOptions) * angleInterval;
  }
}
