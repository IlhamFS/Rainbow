using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {


	public string colorName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void destroyEnemy(){
		Destroy (gameObject);
	}
	public string getColorName(){
		return colorName;
	}
}
