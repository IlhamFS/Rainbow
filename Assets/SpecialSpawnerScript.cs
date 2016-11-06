using UnityEngine;
using System.Collections;

public class SpecialSpawnerScript : MonoBehaviour {
	public GameObject rainbow;
	public GameObject time;

	// Use this for initialization
	void Start () {
		StartCoroutine (spawn(15.0f));
	}

	IEnumerator spawn(float wait) {
		yield return new WaitForSeconds (wait);
		Instantiate (rainbow, transform.position, Quaternion.identity);
		yield return new WaitForSeconds (wait + 5.0f);
		Instantiate (time, transform.position, Quaternion.identity);
		yield break;
	}
}
