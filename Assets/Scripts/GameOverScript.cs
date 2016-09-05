using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {
	public GameObject gameOver;
	public GameObject gameoverText;
	public GameController gm;
	public AudioClip gameOverClip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			DestroyAllEnemy ();

			SoundManagerScript.instance.musicSource.Stop ();
			SoundManagerScript.instance.playSingle (1, gameOverClip);
			EnemyScript es = coll.gameObject.GetComponent<EnemyScript> ();

			StoreHighScore ((int)(gm.score * 100.0f));
			gameOver.SetActive (true);
			Text tx = gameoverText.GetComponent<Text> ();
			tx.text = "Game Over !!! \n dikalahkan oleh musuh berwarna " + es.getColorName () + "\n\n score = " + (int)(gm.score * 100.0f) + "\n\n High Score = " + PlayerPrefs.GetInt ("highscore",0);
			Time.timeScale = 0.0f;
		}
	}

	void StoreHighScore(int newHighScore){
		int oldHighScore = PlayerPrefs.GetInt ("highscore", 0);

		if (newHighScore > oldHighScore)
			PlayerPrefs.SetInt ("highscore", newHighScore);
	}

	void DestroyAllEnemy() {
		Collider2D[] enemies = Physics2D.OverlapAreaAll(new Vector2(-7, 5), new Vector2(7, -5), 1 << LayerMask.NameToLayer("Enemy"), -Mathf.Infinity, Mathf.Infinity);
		foreach (Collider2D enemy in enemies) {
			EnemyScript es = enemy.GetComponent<EnemyScript> ();
			es.destroyEnemy ();
		}
	}
}
