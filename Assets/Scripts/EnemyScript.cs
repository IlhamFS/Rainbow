using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {
	public GameObject item;
	public float speed;
	string colorName;

	// Use this for initialization
	void Start () {
		char[] nama = gameObject.name.ToCharArray ();
		setColorName (nama[0]);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + (Vector3.left * speed * Time.deltaTime);
	}

	public string getColorName() {
		return colorName;
	}

	void setColorName(char figure){
		switch (figure) {
		case '0':
			colorName = "white";
			break;
		case '1':
			colorName = "cyan";
			break;
		case '2':
			colorName = "magenta";
			break;
		case '3':
			colorName = "yellow";
			break;
		case '4':
			colorName = "blue";
			break;
		case '5':
			colorName = "green";
			break;
		case '6':
			colorName = "red";
			break;
		case '7':
			colorName = "black";
			break;
		}
	}

	public void destroyEnemy() {
		float itemRand = Random.Range (0.0f, 1.0f);
		Destroy (gameObject);

		if (itemRand >= 0.8f)
			Instantiate (item,transform.position,Quaternion.identity);
	}
}
