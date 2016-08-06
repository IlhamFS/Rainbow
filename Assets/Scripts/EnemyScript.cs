using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {
	public int wave;
	public float speed;
	int color;
	string colorName;

	SpriteRenderer sr;
	Color col;
	Camera cam;

	float colorLowerBound;
	float colorUpperBound;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		setColor (sr);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + (Vector3.left * speed * Time.deltaTime);
	}

	void setColor(SpriteRenderer sr) {
		ConfigWave (wave);
		color = (int) Random.Range(colorLowerBound,colorUpperBound + 0.9f);

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

	void ConfigWave(int wave) {
		switch (wave) {
		case 1:
			colorLowerBound = 1;
			colorUpperBound = 3;
			break;
		case 2:
			colorLowerBound = 1;
			colorUpperBound = 6;
			break;
		case 3:
			colorLowerBound = 1;
			colorUpperBound = 8;
			break;
		case 4:
			colorLowerBound = 4;
			colorUpperBound = 6;
			break;
		case 5:
			colorLowerBound = 4;
			colorUpperBound = 8;
			break;
		case 6:
			colorLowerBound = 7;
			colorUpperBound = 8;
			break;
		default:
			colorLowerBound = 1;
			colorUpperBound = 8;
			break;
		}

		/*PS: balancing ga cuma pake warna aja, yang kepikiran :
		 * 1. ubah bobot kelompok warna, misal : di wave x, bobot warna gampang (1-3) 30%, warna susah (4-6) 70%
		 * 2. ubah kecepatan enemy
		 * 3. ubah jumlah enemy sampe wave berikutnya
		 */
		
	}

	public string getColorName() {
		return colorName;
	}
}
