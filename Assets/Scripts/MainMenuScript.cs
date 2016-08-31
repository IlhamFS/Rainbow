using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
	GameObject activeScene;
	public GameObject optionScene;
	public GameObject colorGuideScene;
	public GameObject onboardingScene;
	public GameObject creditsScene;
	public GameObject quitScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play() {
		SceneManager.LoadScene (1);
	}

	public void Option() {
		optionScene.SetActive (true);
		activeScene = optionScene;
	}

	public void ColorGuide() {
		colorGuideScene.SetActive (true);
		activeScene = colorGuideScene;
	}

	public void Onboarding() {
		onboardingScene.SetActive (true);
		activeScene = onboardingScene;
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
