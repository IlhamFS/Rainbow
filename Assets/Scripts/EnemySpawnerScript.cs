using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawnerScript : MonoBehaviour {
	public GameObject[] enemy;
	GameObject en;
	int enemyCount = 0;
	int enemyLimit = 10;

	bool firstWave = true;
	public int wave = 1;
	public Text waveText;

	int colorLowerBound;
	int colorUpperBound;
	float speed = 1;

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

			configWave (wave);
			int indEnemy = Random.Range (colorLowerBound, colorUpperBound);

			en = (GameObject) Instantiate (enemy[indEnemy],transform.position, Quaternion.Euler(new Vector3(0, 0)));
			en.GetComponent<EnemyScript> ().speed = speed;
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

	void configWave(int wav) {
		speed = 1;		
		float speedPlus;

		switch (wave) {
		case 1:
			//color
			colorLowerBound = 0;
			colorUpperBound = 3;
			break;
		case 2:
			//color
			colorLowerBound = 1;
			colorUpperBound = 3;

			//speed
			speedPlus = speed * 1.5f;
			speed = Random.Range (speed, speedPlus);
			break;
		case 3:
			//color
			colorLowerBound = 4;
			colorUpperBound = 6;
			break;
		case 4:
			//color
			colorLowerBound = 4;
			colorUpperBound = 7;

			//speed
			speedPlus = speed * 1.5f;
			speed = Random.Range (speed, speedPlus);
			break;
		case 5:
			//color
			colorLowerBound = 0;
			colorUpperBound = 6;

			//speed
			speedPlus = speed * 2f;
			speed = Random.Range (speed, speedPlus);
			break;
		case 6:
			//color
			colorLowerBound = 1;
			colorUpperBound = 7;

			//speed
			speedPlus = speed * 2.5f;
			speed = Random.Range (speed, speedPlus);
			break;
		default:
			//color
			colorLowerBound = 0;
			colorUpperBound = 7;

			//speed
			speedPlus = speed * 2.5f;
			speed = Random.Range (speed, speedPlus);
			break;
		}

		/* PS: balancing ga cuma pake warna aja, yang kepikiran :
		 * 1. ubah bobot kelompok warna, misal : di wave x, bobot warna gampang (1-3) 30%, warna susah (4-6) 70%
		 * 2. ubah kecepatan enemy
		 * 3. ubah jumlah enemy sampe wave berikutnya
		*/
	}
}
