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

	public void SpawnEnemy1() {
		en = (GameObject) Instantiate (enemy,transform.position, Quaternion.Euler(new Vector3(0, 180)));
		en.GetComponent<EnemyScript> ().wave = 1;
	}

	public void SpawnEnemy2() {
		en = (GameObject) Instantiate (enemy,transform.position, Quaternion.Euler(new Vector3(0, 180)));
		en.GetComponent<EnemyScript> ().wave = 3;
	}
}
