using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {
	public float speed;
	public GameObject gameOverText;

	int color;
	string colorName;

	SpriteRenderer sr;
	Color col;
	Camera cam;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		setColor (sr);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + (Vector3.left * speed * Time.deltaTime);
	}

	void OnBecameInvisible() {
		gameOverText.SetActive (true);

		Text tx = gameOverText.GetComponent<Text> ();
		tx.text = "Game Over !!! \n dikalahkan oleh musuh berwarna " + getColorName (); 
	}

	void setColor(SpriteRenderer sr) {
		color = Random.Range(1,8);

		switch (color) {
		case 1:
			//white
			colorName = "white";
			col = new Color (1f, 1f, 1f);
			break;
		case 2:
			//cyan
			colorName = "cyan";
			col = new Color (0f, 1f, 1f);
			break;
		case 3:
			//magenta
			colorName = "magenta";
			col = new Color (1f, 0f, 1f);
			break;
		case 4:
			//yellow
			colorName = "yellow";
			col = new Color (1f, 1f, 0f);
			break;
		case 5:
			//blue
			colorName = "blue";
			col = new Color (0f, 0f, 1f);
			break;
		case 6:
			//green
			colorName = "green";
			col = new Color (0f, 1f, 0f);
			break;
		case 7:
			//red
			colorName = "red";
			col = new Color (1f, 0f, 0f);
			break;
		case 8:
			//black
			colorName = "black";
			col = new Color (0f, 0f, 0f);
			break;
		}

		sr.color = col;
	}

	public string getColorName() {
		return colorName;
	}
}
