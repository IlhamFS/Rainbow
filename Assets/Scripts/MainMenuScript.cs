using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
	GameObject activeScene;
	public GameObject colorGuideScene;
	public GameObject creditsScene;
	public GameObject quitScene;
	public Text highScoreText;

	void Start() {
		highScoreText.text = "" + PlayerPrefs.GetInt ("highscore",0);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (activeScene == null)
				Quit ();
			else
				activeScene.SetActive (false);
		}
	}

	public void Play() {
		int udahMain = PlayerPrefs.GetInt ("udahMain", 0);

		if (udahMain == 0) {
			AutoFade.LoadLevel(2 ,1,1,Color.black);
		}
		else
			AutoFade.LoadLevel(1 ,1,1,Color.black);
	}

	public void ColorGuide() {
		colorGuideScene.SetActive (true);
		activeScene = colorGuideScene;
	}

	public void Onboarding() {
		AutoFade.LoadLevel(2 ,1,1,Color.black);
	}

	public void Credits() {
		creditsScene.SetActive (true);
		activeScene = creditsScene;
	}

	public void Quit() {
		quitScene.SetActive (true);
		activeScene = quitScene;
	}

	public void YesQuit() {
		Application.Quit ();
	}

	public void Back() {
		activeScene.SetActive (false);
	}
}
