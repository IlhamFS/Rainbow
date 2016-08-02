using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void killEnemies(string color){
		Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(15,10), 1 << LayerMask.NameToLayer("Enemy"));
		foreach (Collider2D enemy in enemies) {
			enemy.GetComponent<Blabla> ();
			if (color == enemy.getColorName ()) {
				enemy.destroyEnemy ();
			}
		}
	}

}
