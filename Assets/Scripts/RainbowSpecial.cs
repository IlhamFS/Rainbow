using UnityEngine;
using System.Collections;

public class RainbowSpecial : MonoBehaviour {
	private GestureScript gest;
	// Use this for initialization
	void Awake () {
		gest = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GestureScript> ();
	}
	
	// Update is called once per frame
	void OnMouseDown () {
		gest.SpecialRainbowAttack ();
		//effek special
		Destroy (gameObject);
	}
}
