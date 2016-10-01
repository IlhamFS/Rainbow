using UnityEngine;
using System.Collections;

public class RainbowSpecial : MonoBehaviour {
	private GestureScript gest;
	Animator anim;
	public AudioClip attack;

	// Use this for initialization
	void Awake () {
		gest = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GestureScript> ();
		anim = GetComponent<Animator> ();
	}
	void Start(){
		Destroy (gameObject, 3f);
	}
	// Update is called once per frame
	void OnMouseDown () {
		StartCoroutine (lancarkan());
	}

	IEnumerator lancarkan(){
		gest.SpecialRainbowAttack ();
		SoundManagerScript.instance.playSingle (2, attack);
		anim.SetTrigger ("isClicked");
		yield return new WaitForSeconds (0.5f);

		//effek special
		Destroy (gameObject);
		yield break;
	}
}
