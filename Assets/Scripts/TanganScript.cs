using UnityEngine;
using System.Collections;

public class TanganScript : MonoBehaviour {
	public Animator anim;
	string curBool = "";
	public static TanganScript instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	public void Reset() {
		anim.SetBool (curBool, false);
	}

	public void S() {
		Reset ();
		anim.SetBool ("S", true);
		curBool = "S";
	}

	public void Sinverse() {
		Reset ();
		anim.SetBool ("S Inverse", true);
		curBool = "S Inverse";
	}

	public void Cinverse() {
		Reset ();
		anim.SetBool ("C Inverse", true);
		curBool = "C Inverse";
	}

	public void Horizontal() {
		Reset ();
		anim.SetBool ("Horizontal", true);
		curBool = "Horizontal";
	}

	public void Vertical() {
		Reset ();
		anim.SetBool ("Vertical", true);
		curBool = "Vertical";
	}

	public void Down() {
		Reset ();
		anim.SetBool ("Down", true);
		curBool = "Down";
	}

	public void Up() {
		Reset ();
		anim.SetBool ("Up", true);
		curBool = "Up";
	}

	public void Z() {
		Reset ();
		anim.SetBool ("Z", true);
		curBool = "Z";
	}

	public void Zinverse() {
		Reset ();
		anim.SetBool ("Z Inverse", true);
		curBool = "Z Inverse";
	}
}
