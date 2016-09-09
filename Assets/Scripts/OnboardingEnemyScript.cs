using UnityEngine;
using System.Collections;

public class OnboardingEnemyScript : MonoBehaviour {
	public GameObject[] enemy;
	GameObject en;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public EnemyScript SpawnEnemy1() {
		int indEnemy = Random.Range (1, 3);
		en = (GameObject) Instantiate (enemy[indEnemy],transform.position, Quaternion.identity);
		EnemyScript es = en.GetComponent<EnemyScript> ();
		es.speed= 1;
		return es;
	}

	public EnemyScript SpawnEnemy2() {
		int indEnemy = Random.Range (4, 6);
		en = (GameObject) Instantiate (enemy[indEnemy],transform.position, Quaternion.identity);
		EnemyScript es = en.GetComponent<EnemyScript> ();
		es.speed = 1;
		return es;
	}
}
