﻿using UnityEngine;
using System.Collections;

public class ScorePlusScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.up);
	}
}
