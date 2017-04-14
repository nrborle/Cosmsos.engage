using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody rb;
    private WeaponController weaponController;
    private int direction = 1;

    public float leftBound = -15;
    public float rightBound = 15;
    public float zDistance;
    public float speed;
    public float tilt;

    public GameObject weapon;

    void Start()
    {
        weaponController = weapon.GetComponent<WeaponController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (weaponController.Fire() && Random.value > 0.4)
        {
            zDistance -= 1;
        }
    }

    void FixedUpdate()
    {

        Vector3 movement = new Vector3(direction, 0.0f, (zDistance - transform.position.z)/10);
        // Mathf.Lerp(transform.position.z, zDistance, Time.deltaTime*speed)
        rb.velocity = movement * speed;
        if(rb.position.x < leftBound - 3*direction || rb.position.x > rightBound - 3*direction)
        {
            direction *= -1;
        }
        //rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

}
