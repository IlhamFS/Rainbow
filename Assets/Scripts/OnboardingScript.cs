using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnboardingScript : MonoBehaviour {
	public OnboardingEnemyScript oes;
	public GameController gc;
	public GestureScript gs;

	public Text swipeText;
	public Text combineText;
	public Text attackText;

	bool swiped;
	bool combined;
	bool attacked;
	bool enemydead;
	bool end;

	// Use this for initialization
	void Start () {
		oes.SpawnEnemy1 ();
		swipeText.enabled = false;
		combineText.enabled = false;
		attackText.enabled = false;

		swiped = false;
		combined = false;
		attacked = false;
		enemydead = false;
		end = false;
	}
	
	// Update is called once per frame
	void Update () {
		checkDead ();

		if (enemydead) {
			oes.SpawnEnemy2 ();
			enemydead = false;
		} else if (swipeText.enabled) {
			checkSwipe ();

			if (swiped) {
				swipeText.enabled = false;
				attackText.enabled = true;
			}
		} else if (combineText.enabled) {
		} else if (attackText.enabled) {
			checkAttack ();

			if (attacked) {
				attackText.enabled = false;
				attacked = false;
				Time.timeScale = 1.0f;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			swipeText.enabled = true;
			Time.timeScale = 0.0f;
		}
	}

	void checkDead() {
		if (gc.score != 0) {
			enemydead = true;
			gc.score = 0;
		}
	}

	void checkSwipe() {
		if (gs.colorName != "white")
			swiped = true;
	}

	void checkAttack() {
		if (EventSystem.current.currentSelectedGameObject.name == "Attack")
			attacked = true;
	}
}
