﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void killEnemies(string color){
		Collider2D[] enemies = Physics2D.OverlapAreaAll(new Vector2(-7, 5), new Vector2(7, -5), 1 << LayerMask.NameToLayer("Enemy"), -Mathf.Infinity, Mathf.Infinity);
		foreach (Collider2D enemy in enemies) {
			EnemyScript es = enemy.GetComponent<EnemyScript> ();
			if (color == es.getColorName ()) {
				es.destroyEnemy ();

				score += 10;
			}
		}
	}

	public void rainbowSpecial(){
		Collider2D[] enemies = Physics2D.OverlapAreaAll(new Vector2(-7, 5), new Vector2(7, -5), 1 << LayerMask.NameToLayer("Enemy"), -Mathf.Infinity, Mathf.Infinity);
		foreach (Collider2D enemy in enemies) {
			EnemyScript es = enemy.GetComponent<EnemyScript> ();
			es.destroyEnemy ();
			score += 10;
		}
	}
}
