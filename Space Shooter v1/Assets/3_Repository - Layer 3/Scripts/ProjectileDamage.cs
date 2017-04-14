using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour {


    public GameObject explosion;
    public int damage = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }


        /* If the other object has the same tag, ignore the collision.
        * This prevents shots from colliding with the shooter.
        */
        if (other.tag == gameObject.tag)
        {
            return;
        }

        ActorHealth ph = other.GetComponent<ActorHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);

            Destroy (gameObject);
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
    }
}
