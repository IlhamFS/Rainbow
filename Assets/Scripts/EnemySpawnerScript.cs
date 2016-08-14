using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawnerScript : MonoBehaviour {
	public GameObject enemy;
	GameObject en;
	int enemyCount = 0;
	int enemyLimit = 10;

	bool firstWave = true;

	public int wave = 1;
	public Text waveText;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnEnemy(2.0f));
		waveText.text = "Wave " + wave;
		waveText.CrossFadeAlpha (255, 1.0f, false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator SpawnEnemy(float wait) {
		while (true) {
			yield return new WaitForSeconds (wait);

			//reset wait time setelah wave berubah
			if (wait > 2.0f) {
				wait = 2.0f;
				waveText.CrossFadeAlpha (1, 1.0f, false);
			}

			en = (GameObject) Instantiate (enemy,transform.position, Quaternion.Euler(new Vector3(0, 180)));
			en.GetComponent<EnemyScript> ().wave = wave;
			enemyCount++;

			if (enemyCount == enemyLimit) {
				wave++;
				enemyLimit = enemyLimit + (wave / 2);
				enemyCount = 0;
				wait = 10.0f;

				waveText.text = "Wave " + wave;
				waveText.CrossFadeAlpha (255, 1.0f, false);
			}

			if (firstWave) {
				waveText.CrossFadeAlpha (1, 1.0f, false);
				firstWave = false;
			}
		}


	}
}
