using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : WeaponController {

    private LineRenderer line;
    private AudioSource audioSource;
    private bool active = false;
    private float currentCooldown = 0;

    public GameObject explosion;
    public float dps;
    public float duration = 0.25f;
    public float cooldown = 3;

	// Use this for initialization
	void Start ()
    {
        line = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
        line.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentCooldown -= Time.deltaTime;
	}
    private void LateUpdate()
    {
        //active = false;
    }

    public override bool Fire()
    {
        if (currentCooldown < 0)
        {
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
            audioSource.Play();
            currentCooldown = cooldown;
            return true;
        }
        else
            return false;
    }

    IEnumerator FireLaser()
    {
        float dur = duration;
        line.enabled = true;
        while (dur > 0)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            line.SetPosition(0, ray.origin);
            if (Physics.Raycast(ray, out hit, 20))
            {
                line.SetPosition(1, hit.point);

                Collider other = hit.collider;
                ActorHealth ph = other.GetComponent<ActorHealth>();
                if (ph != null)
                {
                    if(other.tag != this.tag)
                        ph.TakeDamage(dps*Time.deltaTime);

                    if (explosion != null)
                    {
                        Instantiate(explosion, hit.point, other.transform.rotation);
                    }
                }
            }
            else
                line.SetPosition(1, ray.GetPoint(20));
            dur -= Time.deltaTime;

            yield return null;
        }
        line.enabled = false;
    }
}
