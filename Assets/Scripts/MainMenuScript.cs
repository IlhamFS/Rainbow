using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
	GameObject activeScene;
	public GameObject optionScene;
	public GameObject colorGuideScene;
	public GameObject creditsScene;
	public GameObject quitScene;
	public Text highScoreText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play() {
		AutoFade.LoadLevel(1 ,1,1,Color.black);
	}

	public void Option() {
		optionScene.SetActive (true);
		activeScene = optionScene;
		highScoreText.text = "" + PlayerPrefs.GetInt ("highscore",0);
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
