using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor;

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
	public Text wrongText;

	public AudioClip nextPhaseClip;
	public GameObject tangan;

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
			ee.disableAnimation ();

			if (state == 1) {
				gs.colorName = "white";
				StartCoroutine (Swiping());
			} else {
				gs.colorName = "white";
				StartCoroutine (Combining());
			}
		}
	}

	IEnumerator Fading(Text aText){
		aText.CrossFadeAlpha(255, 1.0f, false);
		yield return new WaitForSeconds (1);
		aText.CrossFadeAlpha(1, 1.0f, false);
		yield return new WaitForSeconds (1);
	}

	IEnumerator BackToMainMenu(){
		yield return new WaitForSeconds (2.0f);
		SceneManager.LoadScene (0);
	}

	IEnumerator Combining(){
		gs.onboardingAction = true;
		string enColor = enemy.getColorName ();
		combineText.SetActive (true);

		switch (enColor) {
		case "purple":
			combineText.GetComponent<Text>().text = "Combine colors to " + enColor + " (blue + pink).";
			yield return StartCoroutine (CheckSwipe("blue"));
			yield return StartCoroutine (CheckSwipe("pink"));
			break;
		case "green":
			combineText.GetComponent<Text>().text = "Combine colors to " + enColor + " (blue + yellow).";
			yield return StartCoroutine (CheckSwipe("blue"));
			yield return StartCoroutine (CheckSwipe("yellow"));
			break;
		case "orange":
			combineText.GetComponent<Text>().text = "Combine colors to " + enColor + " (pink + yellow).";
			yield return StartCoroutine (CheckSwipe("pink"));
			yield return StartCoroutine (CheckSwipe("yellow"));
			break;
		}

		SoundManagerScript.instance.playSingle (1, nextPhaseClip);
		yield return StartCoroutine (Fading(phase2));
		yield return StartCoroutine (BackToMainMenu());

		gs.onboardingAction = false;
		yield break;
	}

	IEnumerator Swiping() {
		gs.onboardingAction = true;
		string enColor = enemy.getColorName ();

		yield return StartCoroutine (CheckSwipe(enColor));

		SoundManagerScript.instance.playSingle (1, nextPhaseClip);
		yield return StartCoroutine (Fading(phase1));
		enemy = oes.SpawnEnemy2 ();
		state = 2;

		gs.onboardingAction = false;
		yield break;
	}

	IEnumerator CheckSwipe(string enColor) {
		tangan.SetActive (true);
		int index = 0;

		swipeText.SetActive (true);
		swipeText.GetComponent<Text>().text = "Please swipe " + enColor + ".";

		string actualColor = enColor;
		if (state == 3)
			actualColor = enemy.getColorName ();

		while (gs.colorName != actualColor) {
			if (state == 3) {
				string[] combinedColors = { "purple", "green", "orange" };
				if (ArrayUtility.IndexOf (combinedColors, gs.colorName) != -1) {
					state = 2;
					yield return StartCoroutine (Fading (wrongText));
					yield return StartCoroutine (Combining ());
				}
			} else {
				string[] combinedColors = { "blue", "pink", "yellow" };
				if (ArrayUtility.IndexOf (combinedColors, gs.colorName) != -1) {
					StartCoroutine (Fading (wrongText));
				}
			}

			index = PickColor (enColor);
			setTangan (index);
			yield return null;
		}
			
		if (state == 1 || state == 3) {
			TanganScript.instance.Reset ();
			tangan.SetActive (false);
			swipeText.SetActive (false);
			combineText.SetActive (false);
			attackText.SetActive (true);
			yield return StartCoroutine (CheckAttack (actualColor));
		}
		else if (state == 2) {
			state = 3;
		}

		yield break;
	}

	IEnumerator CheckAttack(string enColor) {
		while (gs.colorName != "white") {

			if (gs.colorName != enColor) {
				attackText.SetActive (false);

				if (state == 1) {
					swipeText.SetActive (true);
					yield return StartCoroutine (Fading(wrongText));
					yield return StartCoroutine(CheckSwipe(enColor));
				}
				else {
					combineText.SetActive (true);
					state = 2;
					yield return StartCoroutine (Fading(wrongText));
					yield return StartCoroutine (Combining());
				}
			}

			yield return null;
		}

		attackText.SetActive (false);
		yield break;
	}

	int PickColor(string colorName){
		int index1 = 0;

		switch (colorName) {
		case "blue":
			index1 = gs.randomGestureIndex [0];
			break;
		case "pink":
			index1 = gs.randomGestureIndex [1];
			break;
		case "yellow":
			index1 = gs.randomGestureIndex [2];
			break;
		}

		return index1;

	}

	void setTangan(int index){
		
		switch (index) {
		case 0:
			TanganScript.instance.Horizontal ();
			break;
		case 1:
			TanganScript.instance.Down ();
			break;
		case 2:
			TanganScript.instance.Cinverse ();
			break;
		case 3:
			TanganScript.instance.Vertical ();
			break;
		case 4:
			TanganScript.instance.Z ();
			break;
		case 5:
			TanganScript.instance.S ();
			break;
		}

	}

}
