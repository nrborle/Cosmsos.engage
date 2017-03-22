using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	//public GameObject explosion;
	//public GameObject playerExplosion;
	//public int scoreValue;
    public int damage = 10;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if(gameController == null){
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}

		//Instantiate (explosion, transform.position, transform.rotation);

        if(other.tag == gameObject.tag)
        {
            return;
        }

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if(ph != null)
        {
            ph.TakeDamage(damage);
		}
        else
        {
            Destroy(other.gameObject);
        }
        //Destroy (gameObject);
        GetComponent<PlayerHealth>().TakeDamage(10);

    }
}
