using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assertions;

public class ActorHealth : MonoBehaviour
{
    public float startingHealth = 100;                            // The amount of health the actor starts the game with.
    public float currentHealth = 1;                                    // The current health the actor has.
    public int scoreValue;
    //public Slider healthSlider;                                 // Reference to the UI's health bar.
    //public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public GameObject deathExplosion;

    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    bool isDead = false;                                                // Whether the actor is dead.
    bool damaged;                                               // True when the player gets damaged.

    private GameController gameController;

    void Start()
    {
        // Setting up the references.
        playerAudio = GetComponents<AudioSource>()[0];

        // Set the initial health of the player.
        currentHealth = startingHealth;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

	void Update(){
		//TESTS

		//The Player should never be dead and have health greater than 0
		Assert.IsFalse (isDead == true && currentHealth > 0);

		//The Player score should always be 0 or more
		Assert.IsTrue (scoreValue >= 0);
	}

    /*
    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }
    */


    public void TakeDamage(float amount)
    {
        // Set the damaged flag so the screen will flash.
        //damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        //healthSlider.value = currentHealth;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        
        gameController.AddScore(scoreValue);
        // Turn off any remaining shooting effects.
        //playerShooting.DisableEffects();

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play();

        // Destroy player and spawn explosion
        Destroy(gameObject);
        Instantiate(deathExplosion, transform.position, transform.rotation);

        if (gameObject.tag == "Player")
        {
            gameController.GameOver();
        }
    }
}
/*

	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
		gameController.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
 * */


