using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
	public static UIScript instance = null; 

	public Text scoreText;
	public Animator scoreAnim;
	public GameController gm;
	public GameObject pauseScene;
	public Text highScoreText;
	public Text multiplierText;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	void Start() {
		if(SceneManager.GetActiveScene().buildIndex != 2)
			highScoreText.text = "" + PlayerPrefs.GetInt ("highscore",0);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (pauseScene.activeSelf)
				Resume ();
			else
				Pause ();
		}
	}

	public void Restart() {
		Time.timeScale = 1.0f;
		AutoFade.LoadLevel (SceneManager.GetActiveScene().buildIndex, 0.5f, 0.5f, Color.black);
	}

	public void MainMenu() {
		Time.timeScale = 1.0f;
		AutoFade.LoadLevel (0, 0.5f, 0.5f, Color.black);
	}

	public void UpdateScore() {
		scoreAnim.SetTrigger ("Triggered");
		scoreText.text = "SCORE \n" + (int) (gm.score);

		if (gm.multiplier != 1) {
			multiplierText.text = "X" + gm.multiplier;

			switch (gm.multiplier % 8) {
			case 0:
				multiplierText.color = new Color (1.0f, 1.0f, 1.0f);
				break;
			case 1:
				multiplierText.color = new Color (1.0f, 1.0f, 0.0f);
				break;
			case 2:
				multiplierText.color = new Color (1.0f, 0.0f, 1.0f);
				break;
			case 3:
				multiplierText.color = new Color (0.0f, 1.0f, 1.0f);
				break;
			case 4:
				multiplierText.color = new Color (1.0f, 0.0f, 0.0f);
				break;
			case 5:
				multiplierText.color = new Color (0.0f, 1.0f, 0.0f);
				break;
			case 6:
				multiplierText.color = new Color (0.0f, 0.0f, 1.0f);
				break;
			case 7:
				multiplierText.color = new Color (0.0f, 0.0f, 0.0f);
				break;
			}
		}
	}

	public Color getMultiplierColor(){
		return multiplierText.color;
	}

	public void Pause(){
		pauseScene.SetActive (true);
		Time.timeScale = 0.0f;
		SoundManagerScript.instance.musicSource.mute = true;
	}

	public void Resume() {
		pauseScene.SetActive (false);
		Time.timeScale = 1.0f;
		SoundManagerScript.instance.musicSource.mute = false;
	}
}
