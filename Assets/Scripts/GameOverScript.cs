using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {
	public GameObject gameOver;
	public GameObject gameoverText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			EnemyScript es = coll.gameObject.GetComponent<EnemyScript> ();

			gameOver.SetActive (true);
			Text tx = gameoverText.GetComponent<Text> ();
			tx.text = "Game Over !!! \n dikalahkan oleh musuh berwarna " + es.getColorName();
			Time.timeScale = 0.0f;
		}
	}
}
