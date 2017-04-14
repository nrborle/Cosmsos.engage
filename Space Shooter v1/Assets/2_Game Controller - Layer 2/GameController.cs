using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class GameController : MonoBehaviour {

	public GameObject asteroid;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemyFormation;
    public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText highScoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	private bool gameOver;
	private bool restart;
	private int score;
	private int highScore;
    private int nextEnemyFormation = 250;


    void Start(){
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		if (PlayerPrefs.HasKey("HighScore"))
		{
			highScore = PlayerPrefs.GetInt("HighScore");
		}
		UpdateScore ();
		StartCoroutine (SpawnWaves ());

	}

	void Update(){
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
		}

		//TESTS

		//There should always be more than 0 hazards in the wave.
		Assert.IsTrue (hazardCount > 0);

		//There should always be some amount of break time between waves.
		Assert.IsTrue (waveWait > 0);

		//There should be something displayed in the score text.
		Assert.IsNotNull (scoreText);

		//There should be something displayed in the restart text.
		Assert.IsNotNull (restartText);

		//There should be something displayed in the game over text.
		Assert.IsNotNull (gameOverText);
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);
		while(true){
			for(int i = 0; i < hazardCount; i++){
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x),spawnValues.y,spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (asteroid, spawnPosition, spawnRotation);
                //condition for spawning enemy 1
                if (i == hazardCount / 2)
                {
                    if(Random.value > 0.25)
                        Instantiate(enemy1, spawnPosition, Quaternion.Euler(0, 180, 0));
                    else
                        Instantiate(enemy2, spawnPosition, Quaternion.Euler(0, 180, 0));
                }
                if(nextEnemyFormation <= 0 && score != 0)
                {
                    Instantiate(enemyFormation, spawnPosition, Quaternion.identity);
                    nextEnemyFormation = 250 - score%250;
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
        nextEnemyFormation -= newScoreValue;
		if (score > highScore) 
		{
			highScore = score;
			PlayerPrefs.SetInt ("HighScore", highScore);
		}
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
		highScoreText.text = "High Score: \n" + highScore;
	}

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}
		
}
