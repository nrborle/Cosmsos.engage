using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationSpawner : MonoBehaviour {

    public int hazardCount;
    public float spawnWait;
    public GameObject hazard;
    public Vector3 spawnPosition;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnFormation());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnFormation()
    {
  
        for (int i = 0; i < hazardCount; i++)
        {
            //Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);
 
            yield return new WaitForSeconds(spawnWait);
        }
        Destroy(this);
    }
}
