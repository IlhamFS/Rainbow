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
	public Text startText;

	EnemyScript enemy;
	int state = 1;

	// Use this for initialization
	void Start () {
		StartCoroutine (Begin());
		swipeText.SetActive (false);
		combineText.SetActive (false);
		attackText.SetActive (false);
		endText.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			EnemyScript ee = coll.gameObject.GetComponent<EnemyScript> ();
			ee.speed = 0;

			if (ee.wave == 2) {
				swipeText.SetActive (true);
				swipeText.GetComponent<Text>().text = "Please swipe " + ee.getColorName();
				StartCoroutine (Swiping());
			} else {
				combineText.SetActive (true);
				combineText.GetComponent<Text>().text = "Please combine colors to " + ee.getColorName ();
				StartCoroutine (Swiping());
			}
		}
	}

	IEnumerator Begin(){
		startText.CrossFadeAlpha(255, 1.0f, false);
		yield return new WaitForSeconds (1);
		startText.CrossFadeAlpha(1, 1.0f, false);
		yield return new WaitForSeconds (1);
		enemy = oes.SpawnEnemy1 ();
	}

	IEnumerator Swiping() {
		yield return StartCoroutine (CheckSwipe());
		swipeText.SetActive (false);
		combineText.SetActive (false);
		attackText.SetActive (true);

		yield return StartCoroutine (CheckAttack());
		gc.killed = false;
		attackText.SetActive (false);

		if (state == 1) {
			enemy = oes.SpawnEnemy2 ();
			state = 2;
			yield break;
		} else if (state == 2) {
			endText.SetActive (true);
			yield return new WaitForSeconds (5);
		}
	}

	IEnumerator CheckSwipe() {
		string enColor = enemy.getColorName ();
		while (gs.colorName != enColor) {
			gs.colorName = "white";
			yield return null;
		}

		yield break;
	}

	IEnumerator CheckAttack() {
		while (!gc.killed) {
			yield return null;
		}
		yield break;
	}

}
