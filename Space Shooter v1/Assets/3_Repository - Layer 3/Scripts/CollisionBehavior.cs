using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehavior : MonoBehaviour {

    public int damage = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        /* If the other object has the same tag, ignore the collision.
         * This prevents enemies from colliding with hazards, as well as stops shots from colliding with the shooter.
         */
        if (other.tag == gameObject.tag)
        {
            return;
        }

        ActorHealth otherHealth = other.GetComponent<ActorHealth>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
        }
        /*
        else
        {
            Destroy(other.gameObject);
        }
        */
    }
}
