using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject asteroid;
    public GameObject enemy;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;

	void Start(){
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update(){
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);
		while(true){
			for(int i = 0; i < hazardCount; i++){
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x),spawnValues.y,spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (asteroid, spawnPosition, spawnRotation);
                if (i == hazardCount / 2)
                {
                    Instantiate(enemy, spawnPosition, Quaternion.Euler(0, 180, 0));
                }
                yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartText.text = "Press 'R' to Restart The Game";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}
		
}
