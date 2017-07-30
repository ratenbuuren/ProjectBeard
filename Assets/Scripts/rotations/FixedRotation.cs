public class FixedRotation : Rotation {
    private float rotation;

    public FixedRotation(float rotation = 0) {
        this.rotation = rotation;
    }

    public override float value() {
        return rotation;
    }
}