using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public float score = 0;
	public bool timeDur = false;
	public GameObject smoke;
	float vertExtent;
	float horzExtent;

	void Start(){
		vertExtent = (int) Camera.main.orthographicSize;
		horzExtent = vertExtent * Screen.width / Screen.height;
	}

	public void killEnemies(string color){
		Collider2D[] enemies = Physics2D.OverlapAreaAll(new Vector2(-7, 5), new Vector2(7, -5), 1 << LayerMask.NameToLayer("Enemy"), -Mathf.Infinity, Mathf.Infinity);
		foreach (Collider2D enemy in enemies) {
			EnemyScript es = enemy.GetComponent<EnemyScript> ();
			Debug.Log (es.getColorName());
			if (color == es.getColorName ()) {
				Instantiate (smoke, enemy.transform.position, Quaternion.identity);
				es.destroyEnemy ();

				float result = (enemy.transform.position.x + 7.5f) / horzExtent;
				score += result;
			}
		}
	}

	public void rainbowSpecial(){
		Collider2D[] enemies = Physics2D.OverlapAreaAll(new Vector2(-7, 5), new Vector2(7, -5), 1 << LayerMask.NameToLayer("Enemy"), -Mathf.Infinity, Mathf.Infinity);
		foreach (Collider2D enemy in enemies) {
			EnemyScript es = enemy.GetComponent<EnemyScript> ();
			Instantiate (smoke, enemy.transform.position, Quaternion.identity);
			es.destroyEnemy ();

			float result = (enemy.transform.position.x + 7.5f) / horzExtent;
			score += result;
		}
	}

	public void timeSpecial(){
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
