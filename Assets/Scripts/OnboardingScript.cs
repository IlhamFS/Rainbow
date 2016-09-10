using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OnboardingScript : MonoBehaviour {
	public OnboardingEnemyScript oes;
	public GameController gc;
	public GestureScript gs;

	public GameObject swipeText;
	public GameObject combinedColors;
	public GameObject attackText;
	public GameObject tutorialFinished;
	public Image color;
	public Image color1;
	public Image color2;
	public Image colorResult;
	public Text startText;
	public Text phase1;
	public Text wrongText;

	public Sprite colorBlue;
	public Sprite colorPink;
	public Sprite colorYellow;
	public Sprite colorPurple;
	public Sprite colorOrange;
	public Sprite colorGreen;

	public AudioClip nextPhaseClip;
	public AudioClip wrongClip;
	public GameObject tangan;
	public GameObject tangan2;

	EnemyScript enemy;
	int state = 1;

	// Use this for initialization
	void Start () {
		StartCoroutine (Fading(startText, 1.0f));
		enemy = oes.SpawnEnemy1 ();
		swipeText.SetActive (false);
		combinedColors.SetActive (false);
		attackText.SetActive (false);
		tutorialFinished.SetActive (false);
		tangan.SetActive (false);
		tangan2.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			EnemyScript ee = coll.gameObject.GetComponent<EnemyScript> ();
			ee.speed = 0;
			ee.disableAnimation ();

			if (state == 1) {
				StartCoroutine (Swiping());
			} else {
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
		tutorialFinished.SetActive (true);
		Time.timeScale = 0.0f;
	}

	IEnumerator Combining(){
		gs.colorName = "white";
		gs.onboardingAction = true;
		string enColor = enemy.getColorName ();
		combinedColors.SetActive (true);

		switch (enColor) {
		case "purple":
			color1.sprite = imageColor ("blue");
			color2.sprite = imageColor ("pink");
			colorResult.sprite = imageColor ("purple");

			yield return StartCoroutine (CheckSwipe2("blue"));
			yield return StartCoroutine (CheckSwipe3("blue","pink"));
			break;
		case "green":
			color1.sprite = imageColor ("blue");
			color2.sprite = imageColor ("yellow");
			colorResult.sprite = imageColor ("green");

			yield return StartCoroutine (CheckSwipe2("blue"));
			yield return StartCoroutine (CheckSwipe3("blue","yellow"));
			break;
		case "orange":
			color1.sprite = imageColor ("pink");
			color2.sprite = imageColor ("yellow");
			colorResult.sprite = imageColor ("orange");

			yield return StartCoroutine (CheckSwipe2("pink"));
			yield return StartCoroutine (CheckSwipe3("pink","yellow"));
			break;
		}

		if (!gs.onboardingAction)
			yield break;

		SoundManagerScript.instance.playSingle (2, nextPhaseClip);
		yield return StartCoroutine (BackToMainMenu());

		gs.onboardingAction = false;
		yield break;
	}

	IEnumerator Swiping() {
		gs.colorName = "white";
		gs.onboardingAction = true;
		string enColor = enemy.getColorName ();

		yield return StartCoroutine (CheckSwipe(enColor));
		if (!gs.onboardingAction)
			yield break;

		SoundManagerScript.instance.playSingle (2, nextPhaseClip);
		yield return StartCoroutine (Fading (phase1, 1.0f));

		enemy = oes.SpawnEnemy2 ();
		state = 2;

		gs.onboardingAction = false;
		yield break;
	}

	IEnumerator CheckSwipe(string enColor) {
		tangan.SetActive (true);
		color.sprite = imageColor (enColor);
		swipeText.SetActive (true);

		while (gs.colorName != enColor) {
			if (gs.colorName != "white")
				yield return StartCoroutine (Salah());

			if (!gs.onboardingAction)
				yield break;

			setTangan (enColor);
			yield return null;
		}
			
		TanganScript.instance.Reset ();
		tangan.SetActive (false);
		swipeText.SetActive (false);
		combinedColors.SetActive (false);
		attackText.SetActive (true);
		tangan2.SetActive (true);
		yield return StartCoroutine (CheckAttack (enColor));
	}

	IEnumerator CheckSwipe2(string enColor){
		color.sprite = imageColor (enColor);

		while (gs.colorName != enColor) {			
			if (gs.colorName != "white")
				yield return StartCoroutine (Salah());

			if (!gs.onboardingAction)
				yield break;
			
			setTangan (enColor);
			yield return null;
		}
			
		state = 3;
		yield break;
	}

	IEnumerator CheckSwipe3(string oldColor, string newColor){
		color.sprite = imageColor (newColor);

		string actualColor = enemy.getColorName ();

		while (gs.colorName != actualColor) {			
			if (gs.colorName != oldColor)
				yield return StartCoroutine (Salah());

			if (!gs.onboardingAction)
				yield break;

			setTangan (newColor);
			yield return null;
		}

		TanganScript.instance.Reset ();
		combinedColors.SetActive (false);
		attackText.SetActive (true);
		tangan2.SetActive (true);
		yield return StartCoroutine (CheckAttack (actualColor));
	}

	Sprite imageColor(string color){
		Sprite theColor = null;

		switch(color){
		case "blue":
			theColor = colorBlue;
			break;
		case "pink":
			theColor = colorPink;
			break;
		case "yellow":
			theColor = colorYellow;
			break;
		case "purple":
			theColor = colorPurple;
			break;
		case "orange":
			theColor = colorOrange;
			break;
		case "green":
			theColor = colorGreen;
			break;
		}

		return theColor;
	}

	IEnumerator CheckAttack(string enColor) {
		while (gs.colorName != "white") {
			if (gs.colorName != enColor)
				yield return StartCoroutine (Salah());

			if (!gs.onboardingAction)
				yield break;

			yield return null;
		}

		attackText.SetActive (false);
		tangan2.SetActive (false);
		yield break;
	}

	IEnumerator Salah(){
		if (!gs.onboardingAction)
			yield break;

		gs.onboardingAction = false;

		SoundManagerScript.instance.playSingle (2, wrongClip);
		tangan.SetActive (false);
		swipeText.SetActive (false);
		combinedColors.SetActive (false);
		attackText.SetActive (false);
		tangan2.SetActive (true);

		while (gs.colorName != "white") {
			yield return StartCoroutine (Fading(wrongText, 0.5f));
		}

		tangan2.SetActive (false);
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
			TanganScript.instance.N ();
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
