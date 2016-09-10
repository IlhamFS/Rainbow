using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {
	public GameObject gameOver;
	public GameObject gameoverText;
	public GameController gm;
	public AudioClip gameOverClip;
	public AudioClip heroDeathClip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			EnemyScript es = coll.gameObject.GetComponent<EnemyScript> ();
			StartCoroutine (GameOver(es));
		}
	}

	IEnumerator GameOver(EnemyScript es) {
		gm.gameOver = true;
		SoundManagerScript.instance.musicSource.Stop ();
		SoundManagerScript.instance.heroSource.Stop ();
		SoundManagerScript.instance.sfxSource.mute = true;
		SoundManagerScript.instance.playSingle (2, heroDeathClip);

		es.destroyMata ();
		StopAllEnemy ();
		yield return StartCoroutine (es.playAttack ());
		yield return new WaitForSeconds (1.0f);

		SoundManagerScript.instance.sfxSource.mute = false;
		SoundManagerScript.instance.playSingle (1, gameOverClip);

		StoreHighScore ((int)(gm.score * 100.0f));
		gameOver.SetActive (true);
		Text tx = gameoverText.GetComponent<Text> ();
		tx.text = "" + PlayerPrefs.GetInt ("highscore",0);
		Time.timeScale = 0.0f;
		yield break;
	}

	void StoreHighScore(int newHighScore){
		int oldHighScore = PlayerPrefs.GetInt ("highscore", 0);

		if (newHighScore > oldHighScore)
			PlayerPrefs.SetInt ("highscore", newHighScore);
	}

	void StopAllEnemy() {
		Collider2D[] enemies = Physics2D.OverlapAreaAll(new Vector2(-7, 5), new Vector2(7, -5), 1 << LayerMask.NameToLayer("Enemy"), -Mathf.Infinity, Mathf.Infinity);
		foreach (Collider2D enemy in enemies) {
			EnemyScript es = enemy.GetComponent<EnemyScript> ();
			es.speed = 0;
		}
	}
}
