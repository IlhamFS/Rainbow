using UnityEngine;
using System.Collections;

public class TimeSpecial : MonoBehaviour {
	private GestureScript gest;

	void Awake () {
		gest = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GestureScript> ();
	}

	// Update is called once per frame
	void OnMouseDown () {
		gest.SpecialTimeAttack ();
		//effek special
		Destroy (gameObject);
	}
}
