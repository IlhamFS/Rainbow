using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public float score = 0;

	int enemyCount = 0;
	public int multiplier = 1;
	int enemyLimit;

	public bool timeDur = false;
	public GameObject smoke;
	float vertExtent;
	float horzExtent;

	public bool gameOver = false;

	void Start(){
		enemyLimit = 5 * multiplier;
		vertExtent = (int) Camera.main.orthographicSize;
		horzExtent = vertExtent * Screen.width / Screen.height;
	}

	public void killEnemies(string color){
		if (gameOver)
			return;

		Collider2D[] enemies = Physics2D.OverlapAreaAll(new Vector2(-7, 5), new Vector2(7, -5), 1 << LayerMask.NameToLayer("Enemy"), -Mathf.Infinity, Mathf.Infinity);
		foreach (Collider2D enemy in enemies) {
			EnemyScript es = enemy.GetComponent<EnemyScript> ();
			if (color == es.getColorName ()) {
				enemyCount++;

				if (enemyCount > enemyLimit) {
					enemyCount = 0;
					multiplier++;
					enemyLimit = 5 * multiplier;
				}

				Instantiate (smoke, enemy.transform.position, Quaternion.identity);
				es.destroyEnemy ();

				float result = 10 * multiplier;
				score += result;
				UIScript.instance.UpdateScore ();
			}
		}
	}

	public void rainbowSpecial(){
		if (gameOver)
			return;
		
		Collider2D[] enemies = Physics2D.OverlapAreaAll(new Vector2(-7, 5), new Vector2(7, -5), 1 << LayerMask.NameToLayer("Enemy"), -Mathf.Infinity, Mathf.Infinity);

		foreach (Collider2D enemy in enemies) {
			enemyCount++;

			if (enemyCount > enemyLimit) {
				enemyCount = 0;
				multiplier++;
				enemyLimit = 5 * multiplier;
			}

			EnemyScript es = enemy.GetComponent<EnemyScript> ();
			Instantiate (smoke, enemy.transform.position, Quaternion.identity);
			es.destroyEnemy ();

			float result = 10 * multiplier;
			score += result;
			UIScript.instance.UpdateScore ();
		}
	}

	public void timeSpecial(){
		if (gameOver)
			return;
		
		//StartCoroutine (TimeSpecialDuration (5.0f));
		Collider2D[] enemies = Physics2D.OverlapAreaAll (new Vector2 (-7, 5), new Vector2 (7, -5), 1 << LayerMask.NameToLayer ("Enemy"), -Mathf.Infinity, Mathf.Infinity);
		foreach (Collider2D enemy in enemies) {
			EnemyScript es = enemy.GetComponent<EnemyScript> ();
			es.speed = es.speed / 2;
		}

	}
	IEnumerator TimeSpecialDuration(float time){
		yield return new WaitForSeconds (time);
		timeDur = false;
	}
}
