using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private AudioSource[] audioSource;

    public GameObject weapon;
    private WeaponController weaponController;

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public Transform shotSpawn;
	public float fireRate1;
    public float fireRate2;
    public float fireRate3;

    private float nextFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponents<AudioSource>();
        weaponController = weapon.GetComponent<WeaponController>();
    }

    void Update(){

        //Fire Bolt
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
            audioSource[0].Play();
			nextFire = Time.time + fireRate1;
			Instantiate (shot1, shotSpawn.position, shotSpawn.rotation);
		}

        //Fire Rapid
        if (Input.GetButton("Space") /*&& Time.time > nextFire*/)
        {
            weaponController.Fire();
            /*
            audioSource[1].Play();
            nextFire = Time.time + fireRate2;
            Instantiate(shot2, shotSpawn.position, shotSpawn.rotation);
            */
        }

        //Fire Missile
        if (Input.GetButton("LeftAlt") && Time.time > nextFire)
        {
            audioSource[2].Play();
            nextFire = Time.time + fireRate3;
            Instantiate(shot3, shotSpawn.position, shotSpawn.rotation);
        }

		//TESTS

		//The Minimum x value of the boundary should always be less than the Maxiumum x value
		Assert.IsTrue (boundary.xMin < boundary.xMax);

		//The Minimum z value of the boundary should always be less than the Maxiumum z value
		Assert.IsTrue (boundary.zMin < boundary.zMax);
    }
		
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical);

		rb.velocity = movement * speed;

		rb.position = new Vector3 
			(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);
		
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}


}
