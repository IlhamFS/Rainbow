using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnboardingScript : MonoBehaviour {
	public OnboardingEnemyScript oes;
	public GameController gc;
	public GestureScript gs;

	public GameObject swipeText;
	public GameObject combineText;
	public GameObject attackText;
	public GameObject endText;

	EnemyScript enemy;

	bool paused;
	int state = 1;

	// Use this for initialization
	void Start () {
		enemy = oes.SpawnEnemy1 ();
		swipeText.SetActive (false);
		combineText.SetActive (false);
		attackText.SetActive (false);
		endText.SetActive (false);

		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!paused)
			return;

		if (swipeText.activeInHierarchy || combineText.activeInHierarchy) {
			//phase 1, nunggu swipe
			string enColor = enemy.getColorName ();

			if (gs.colorName == enColor) {
				swipeText.SetActive (false);
				combineText.SetActive (false);
				attackText.SetActive (true);
			} else {
				gs.colorName = "white";
			}
		} else if (attackText.activeInHierarchy) {
			//phase 2, nunggu klik attack
			if (gc.killed) {
				paused = false;
				gc.killed = false;
				attackText.SetActive (false);

				if (state == 1) {
					enemy = oes.SpawnEnemy2 ();
					state = 2;
				} else if (state == 2) {
					endText.SetActive (true);
				}
			}
		} else if (endText.activeInHierarchy) {
			//phase 3, kalo musuh udah abis
			Invoke("Pause", 5);
		}
	}

	void Pause() {
		Time.timeScale = 0.0f;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			EnemyScript ee = coll.gameObject.GetComponent<EnemyScript> ();

			if (ee.wave == 2) {
				swipeText.SetActive (true);
				swipeText.GetComponent<Text>().text = "Please swipe " + ee.getColorName();
			} else {
				combineText.SetActive (true);
				combineText.GetComponent<Text>().text = "Please combine colors to " + ee.getColorName ();
			}

			ee.speed = 0;
			paused = true;
		}
	}
}
