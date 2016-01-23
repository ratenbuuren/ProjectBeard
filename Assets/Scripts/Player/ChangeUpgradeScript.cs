using UnityEngine;
using System.Collections;

public class ChangeUpgradeScript : MonoBehaviour {

    public enum Captains { NoBeard, UsainBeard, FlyingDutchman}
    public Captains newCaptain = Captains.NoBeard;

	void OnCollisionEnter2D(Collision2D collision){        
		if(collision.gameObject.tag == "Player"){            
            if (collision.gameObject.GetComponent<Captain>())
            {
                /* !powerEnabled == power currently in use */
                if (!collision.gameObject.GetComponent<ShipController>().getPowerEnabled())
                {
                    collision.gameObject.GetComponent<Captain>().Deactivate();
                }
                Destroy(collision.gameObject.GetComponent<Captain>());
            }
            switch (newCaptain)
            {
                case Captains.NoBeard:
                default:
                    collision.gameObject.AddComponent<CaptainNoBeard>();
                    break;
                case Captains.UsainBeard:
                    collision.gameObject.AddComponent<CaptainUsainBeard>();
                    break;
                case Captains.FlyingDutchman:
                    collision.gameObject.AddComponent<CaptainFlyingDutchman>();
                    collision.gameObject.GetComponent<CaptainFlyingDutchman>().moveDiff = Vector3.up;
                    break;
            }                        			
		}
    }
}
