using UnityEngine;
using System.Collections;

public class CaptainFlyingDutchman : Captain{
        
    public Vector3 moveDiff = Vector3.up * 0.25f;
    public Vector3 prevOffset;

    void Start()
    {
        init("Flying Dutchman", 1, 8);
    }
	
	public override void Activate(){
		Physics2D.IgnoreLayerCollision (8,9,true);

        /*  Try to increase shadow distance,
            This is possible for children nodes with a CopyMovement script attached. */
        CopyMovement[] cms = GetComponentsInChildren<CopyMovement>();
        foreach (CopyMovement cm in cms)
        {
            if (cm.gameObject.tag == "Flyable")
            {
                prevOffset = cm.positionOffset;
                cm.positionOffset += moveDiff;
                break;
            }
        }
        Invoke("Deactivate", 4);
	}

	public override void Deactivate(){
		Physics2D.IgnoreLayerCollision (8,9,false);

        /* return the height difference to normal again */
        CopyMovement[] cms = GetComponentsInChildren<CopyMovement>();
        foreach (CopyMovement cm in cms)
        {
            if (cm.gameObject.tag == "Flyable")
            {
                cm.positionOffset = prevOffset;
                break;
            }
        }
    }
}
