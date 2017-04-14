using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MissileMover : MonoBehaviour {

    public float delay;
    public float acceleration;

    private bool started = false;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (started) {
            rb.velocity += transform.forward * acceleration * Time.deltaTime;
        }
        else
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                started = true;
            }
        }

		//TEST

		//There should always be some delay in firering the missile
		Assert.IsTrue (delay != 0);

		//The missile should not accelerate backwards into the player
		Assert.IsTrue (acceleration >= 0);

    }
}
