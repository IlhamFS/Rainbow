using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Restart() {
		SceneManager.LoadScene (0);
		Time.timeScale = 1.0f;
	}

	public void MainMenu() {
		//load scene main menu
	}
}
