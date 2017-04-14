using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponController : WeaponController{

    public GameObject projectile;
    public float fireRateMin;
    public float fireRateMax;
    private float nextFire;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + Random.Range(fireRateMin, fireRateMax);
            Instantiate(projectile, transform.position, transform.rotation);
            audioSource.Play();
            return true;
        }
        else return false;
    }
}
