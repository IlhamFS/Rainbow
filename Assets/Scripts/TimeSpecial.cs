using UnityEngine;
using System.Collections;

public class TimeSpecial : MonoBehaviour {
	private GestureScript gest;
	Animator anim;
	public AudioClip attack;

	void Awake () {
		gest = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GestureScript> ();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void OnMouseDown () {
		StartCoroutine (lancarkan());
	}

	IEnumerator lancarkan(){
		SoundManagerScript.instance.playSingle (2, attack);
		anim.SetTrigger ("isClicked");
		yield return new WaitForSeconds (0.5f);
		gest.SpecialTimeAttack ();

		//effek special
		Destroy (gameObject);
		yield break;
	}
}
