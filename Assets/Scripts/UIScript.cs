using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
	public Text scoreText;
	public GameController gm;
	public GameObject pauseScene;
	public Text highScoreText;

	// Use this for initialization
	void Start () {
		highScoreText.text = "" + PlayerPrefs.GetInt ("highscore",0);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScore ();
	}

	public void Restart() {
		Time.timeScale = 1.0f;
		AutoFade.LoadLevel (SceneManager.GetActiveScene().buildIndex, 0.5f, 0.5f, Color.black);
	}

	public void MainMenu() {
		Time.timeScale = 1.0f;
		AutoFade.LoadLevel (0, 0.5f, 0.5f, Color.black);
	}

	void UpdateScore() {
		scoreText.text = "Score = " + (int) (gm.score * 100.0f);
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
