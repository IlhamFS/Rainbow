using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
	public Text scoreText;
	public GameController gm;
	public GameObject pauseScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScore ();
	}

	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1.0f;
	}

	public void MainMenu() {
		SceneManager.LoadScene (0);
		Time.timeScale = 1.0f;
	}

	void UpdateScore() {
		scoreText.text = "Score = " + (int) (gm.score * 100.0f);
	}

	public void Pause(){
		pauseScene.SetActive (true);
		Time.timeScale = 0.0f;
	}

	public void Resume() {
		pauseScene.SetActive (false);
		Time.timeScale = 1.0f;
	}
}
