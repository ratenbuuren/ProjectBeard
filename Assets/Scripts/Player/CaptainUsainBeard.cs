using UnityEngine;
using System.Collections;

public class CaptainUsainBeard : Captain
{
    private float oldVelocity, oldMaxVelocity, oldAcceleration;

    void Start()
    {
        init("Usain Beard", 0, 4);
    }

    /* Triples the velocity of the ship */
    public override void Activate()
    {
        oldVelocity = player.getVelocity();
        oldMaxVelocity = player.maxVelocity;
        oldAcceleration = player.acceleration;

        player.setVelocity(oldVelocity * 3);
        player.maxVelocity *= 3;
        player.acceleration *= 3;
        
        /* reset the velocity to the original velocity after one second */
        Invoke("Deactivate", 1);
    }

    /* Resets the velocity to the original */
    public override void Deactivate()
    {
        player.setVelocity(oldVelocity);
        player.maxVelocity = oldMaxVelocity;
        player.acceleration = oldAcceleration;
    }
}