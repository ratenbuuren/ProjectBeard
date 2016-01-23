using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class AIMovement: MonoBehaviour {

    public float range = 20;    
    public float maxVelocity = 300f;
    public float acceleration = 100f;
    public float maxTorque = 270;
    public float dTorque = 180f;
    public GameObject target;

    public float acceptableAngleError = 0;        

    private Rigidbody2D rb;
    private float torque;
    private float velocity;

    // Use this for initialization
    void Start () {
        torque = 0;
        velocity = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        float angle = Vector2.Angle(transform.TransformDirection(Vector2.left), target.transform.position - transform.position);

        /* Target in range */
        if (target && Vector3.Distance(transform.position, target.transform.position) <= range)
        {                        
            /* Rotate */
            if (angle >= acceptableAngleError)
            {           
                if (Vector3.Cross(transform.TransformDirection(Vector2.left), target.transform.position - transform.position).z < 0)
                {
                    // turn right
                    torque = Mathf.Max(-maxTorque, torque <= 0 ? torque - (dTorque * Time.deltaTime) : torque - (2 * dTorque * Time.deltaTime));
                }
                else
                {
                    // turn left
                    torque = Mathf.Min(maxTorque, torque >= 0 ? torque + (dTorque * Time.deltaTime) : torque + (2 * dTorque * Time.deltaTime));
                }
            }

            /* Move */
            velocity = Mathf.Min(maxVelocity, velocity + (acceleration * Time.deltaTime));
        }
        /* Target out range */
        else
        {
            /* Slowly stop rotating */
            if (torque < 0)
            {
                torque = Mathf.Min(0, torque + (dTorque * Time.deltaTime));
            }
            else if (torque > 0)
            {
                torque = Mathf.Max(0, torque - (dTorque * Time.deltaTime));
            }
            velocity = Mathf.Max(0, velocity - (2 * acceleration * Time.deltaTime));
        }
        rb.AddTorque(torque);
        rb.AddRelativeForce((angle < 90 ? 1f : 0.5f) * Vector2.up * velocity);

    }

    private void OnDrawGizmosSelected()
    {    
        UnityEditor.Handles.color = Color.gray;
        UnityEditor.Handles.DrawWireDisc(transform.root.position, Vector3.forward, range);
    }
}
