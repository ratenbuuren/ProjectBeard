using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Captain))]
[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour {

    public float minVelocity = 300f;
    public float maxVelocity = 500f;    
    public float acceleration = 100f;    
    public float maxTorque = 270;
    public float dTorque = 180f;

    private float velocity;
    private float torque = 0f;
    private bool powerEnabled = true;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();        
        velocity = (minVelocity+maxVelocity)/2;
        rb.velocity = Vector2.up * velocity; /* Initial speed of the ship is the slowest speed of the ship*/        
	}
	
	void Update () {
        /* Rotate ship */
        if (Input.GetKey(KeyCode.RightArrow))
        {
            torque = Mathf.Max(-maxTorque, torque <= 0 ? torque - (dTorque * Time.deltaTime) : torque - (2 * dTorque * Time.deltaTime));
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            torque = Mathf.Min(maxTorque, torque >= 0 ? torque + (dTorque * Time.deltaTime) : torque + (2 * dTorque * Time.deltaTime));
		}         
        else
        {            
            if (torque < 0)
            {
                torque = Mathf.Min(0, torque + (dTorque * Time.deltaTime));
            }
            else if (torque > 0)
            {
                torque = Mathf.Max(0, torque - (dTorque * Time.deltaTime));
            }
        }

        rb.angularVelocity = 0;
        rb.AddTorque(torque);

        /* Change the velocity */       
        if (Input.GetKey(KeyCode.UpArrow)){
            velocity = Mathf.Min(maxVelocity, velocity + (2 * acceleration * Time.deltaTime));		
		}
		if(Input.GetKey(KeyCode.DownArrow)){
            velocity = Mathf.Max(minVelocity, velocity - (2 * acceleration * Time.deltaTime));			
		}

        rb.velocity = Vector2.zero;		
		rb.AddRelativeForce(Vector2.up * velocity);				
				
		/* Use captain's power */
		if(Input.GetKeyDown(KeyCode.LeftShift) && powerEnabled){
            powerEnabled = false;
			captain().Activate();
            Invoke("enablePower", captain().getCooldown());
            gameObject.SendMessage("UpdateCaptainPowerUI", captain().getCooldown(), SendMessageOptions.DontRequireReceiver);                
		}

        /* Show stats if available */
        gameObject.SendMessage("UpdateStats", string.Format(
                "fps:     {0}\n\n" +
                "velocity {1}\n" +
                "torque:  {2}",
                1.0f / Time.deltaTime, velocity, torque), SendMessageOptions.DontRequireReceiver);
        
	}	

    private void enablePower()
    {
        powerEnabled = true;
    }

    public Captain captain()
    {
        return gameObject.GetComponent<Captain>();
    }

    public float getVelocity()
    {
        return velocity;
    }

    public void setVelocity(float newVelocity)
    {
        velocity = newVelocity;
    }

    public float getTorque()
    {
        return torque;
    }

    public void setTorque(float newTorque)
    {
        torque = newTorque;
    }

    public bool getPowerEnabled()
    {
        return powerEnabled;
    }
}
