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
	public Text startText;
	public Text phase1;
	public Text phase2;

	public Button attack;
	public Button cancel;

	EnemyScript enemy;
	int state = 1;

	// Use this for initialization
	void Start () {
		StartCoroutine (Fading(startText));
		enemy = oes.SpawnEnemy1 ();
		swipeText.SetActive (false);
		combineText.SetActive (false);
		attackText.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			EnemyScript ee = coll.gameObject.GetComponent<EnemyScript> ();
			ee.speed = 0;

			if (state == 1) {
				swipeText.SetActive (true);
				swipeText.GetComponent<Text>().text = "Please swipe " + ee.getColorName() + ".\n Press cancel to reset color.";
				StartCoroutine (Swiping());
			} else {
				combineText.SetActive (true);
				combineText.GetComponent<Text>().text = "Combine colors to " + ee.getColorName () + ".\n Press cancel to reset color.";
				StartCoroutine (Swiping());
			}
		}
	}

	IEnumerator Fading(Text aText){
		aText.CrossFadeAlpha(255, 1.0f, false);
		yield return new WaitForSeconds (1);
		aText.CrossFadeAlpha(1, 1.0f, false);
		yield return new WaitForSeconds (1);
	}

	IEnumerator Swiping() {
		cancel.interactable = true;

		yield return StartCoroutine (CheckSwipe());
		swipeText.SetActive (false);
		combineText.SetActive (false);
		attackText.SetActive (true);
		cancel.interactable = false;
		attack.interactable = true;

		yield return StartCoroutine (CheckAttack());
		gc.killed = false;
		attackText.SetActive (false);
		attack.interactable = false;

		if (state == 1) {
			yield return StartCoroutine (Fading(phase1));
			enemy = oes.SpawnEnemy2 ();
			state = 2;
			yield break;
		} else if (state == 2) {
			yield return StartCoroutine (Fading(phase2));
			yield break;
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
