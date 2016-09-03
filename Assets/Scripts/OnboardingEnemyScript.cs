using UnityEngine;
using System.Collections;

public class OnboardingEnemyScript : MonoBehaviour {
	public GameObject enemy;
	GameObject en;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public EnemyScript SpawnEnemy1() {
		en = (GameObject) Instantiate (enemy,transform.position, Quaternion.Euler(new Vector3(0, 180)));
		EnemyScript es = en.GetComponent<EnemyScript> ();
		es.wave = 2;
		return es;
	}

	public EnemyScript SpawnEnemy2() {
		en = (GameObject) Instantiate (enemy,transform.position, Quaternion.Euler(new Vector3(0, 180)));
		EnemyScript es = en.GetComponent<EnemyScript> ();
		es.wave = 3;
		return es;
	}
}
