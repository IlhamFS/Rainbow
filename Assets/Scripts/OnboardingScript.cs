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
	public AudioClip wrongClip;
	public GameObject tangan;

	EnemyScript enemy;
	int state = 1;

	// Use this for initialization
	void Start () {
		StartCoroutine (Fading(startText, 1.0f));
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

	IEnumerator Fading(Text aText, float duration){
		aText.CrossFadeAlpha(255, duration, false);
		yield return new WaitForSeconds (1);
		aText.CrossFadeAlpha(1, duration, false);
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
			yield return StartCoroutine (CheckSwipe2("blue"));
			yield return StartCoroutine (CheckSwipe3("blue","pink"));
			break;
		case "green":
			combineText.GetComponent<Text>().text = "Combine colors to " + enColor + " (blue + yellow).";
			yield return StartCoroutine (CheckSwipe2("blue"));
			yield return StartCoroutine (CheckSwipe3("blue","yellow"));
			break;
		case "orange":
			combineText.GetComponent<Text>().text = "Combine colors to " + enColor + " (pink + yellow).";
			yield return StartCoroutine (CheckSwipe2("pink"));
			yield return StartCoroutine (CheckSwipe3("pink","yellow"));
			break;
		}

		SoundManagerScript.instance.playSingle (2, nextPhaseClip);
		yield return StartCoroutine (Fading(phase2, 1.0f));
		yield return StartCoroutine (BackToMainMenu());

		gs.onboardingAction = false;
		yield break;
	}

	IEnumerator Swiping() {
		gs.onboardingAction = true;
		string enColor = enemy.getColorName ();

		yield return StartCoroutine (CheckSwipe(enColor));

		SoundManagerScript.instance.playSingle (2, nextPhaseClip);
		yield return StartCoroutine (Fading(phase1, 1.0f));
		enemy = oes.SpawnEnemy2 ();
		state = 2;

		gs.onboardingAction = false;
		yield break;
	}

	IEnumerator CheckSwipe(string enColor) {
		tangan.SetActive (true);
		swipeText.SetActive (true);
		swipeText.GetComponent<Text>().text = "Please swipe " + enColor + ".";

		while (gs.colorName != enColor) {
			if (gs.colorName != "white")
				yield return StartCoroutine (Salah());

			setTangan (enColor);
			yield return null;
		}
			
		TanganScript.instance.Reset ();
		tangan.SetActive (false);
		swipeText.SetActive (false);
		combineText.SetActive (false);
		attackText.SetActive (true);
		yield return StartCoroutine (CheckAttack (enColor));
	}

	IEnumerator CheckSwipe2(string enColor){
		tangan.SetActive (true);
		swipeText.SetActive (true);
		swipeText.GetComponent<Text>().text = "Please swipe " + enColor + ".";

		while (gs.colorName != enColor) {
			if (gs.colorName != "white")
				yield return StartCoroutine (Salah());
			
			setTangan (enColor);
			yield return null;
		}

		TanganScript.instance.Reset ();
		state = 3;
		yield break;
	}

	IEnumerator CheckSwipe3(string oldColor, string newColor){
		swipeText.GetComponent<Text>().text = "Please swipe " + newColor + ".";

		string actualColor = enemy.getColorName ();

		while (gs.colorName != actualColor) {
			if (gs.colorName != oldColor)
				yield return StartCoroutine (Salah());

			setTangan (newColor);
			yield return null;
		}

		TanganScript.instance.Reset ();
		tangan.SetActive (false);
		swipeText.SetActive (false);
		combineText.SetActive (false);
		attackText.SetActive (true);
		yield return StartCoroutine (CheckAttack (actualColor));
	}

	IEnumerator CheckAttack(string enColor) {
		while (gs.colorName != "white") {
			if (gs.colorName != enColor)
				yield return StartCoroutine (Salah());

			yield return null;
		}

		attackText.SetActive (false);
		yield break;
	}

	IEnumerator Salah(){
		if (!gs.onboardingAction)
			yield break;

		SoundManagerScript.instance.playSingle (2, wrongClip);
		tangan.SetActive (false);
		swipeText.SetActive (false);
		combineText.SetActive (false);
		attackText.SetActive (false);

		while (gs.colorName != "white") {
			yield return StartCoroutine (Fading(wrongText, 0.5f));
		}

		if (state == 1)
			yield return StartCoroutine (Swiping ());
		else {
			state = 2;
			yield return StartCoroutine (Combining());
		}
	}

	void setTangan(string colorName){
		int index = 0;

		switch (colorName) {
		case "blue":
			index = gs.randomGestureIndex [0];
			break;
		case "pink":
			index = gs.randomGestureIndex [1];
			break;
		case "yellow":
			index = gs.randomGestureIndex [2];
			break;
		}
		
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
