using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {
	public GameObject[] item;
	public float speed;
	public string colorName;
	public AudioClip[] enemyClip;
	public Animator enemyAnimator;

	// Use this for initialization
	void Start () {
		char[] nama = gameObject.name.ToCharArray ();
		setColorName (nama[0]);

		StartCoroutine (playSound(3.0f));
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
			colorName = "blue";
			break;
		case '2':
			colorName = "pink";
			break;
		case '3':
			colorName = "yellow";
			break;
		case '4':
			colorName = "purple";
			break;
		case '5':
			colorName = "green";
			break;
		case '6':
			colorName = "orange";
			break;
		case '7':
			colorName = "brown";
			break;
		}
	}

	public void destroyEnemy(Text scorePlusText, float result, Color mulColor) {
		float itemRand = Random.Range (0.0f, 1.0f);
		Destroy (gameObject);

		if (SceneManager.GetActiveScene ().buildIndex == 2)
			return;

		if (itemRand >= 0.95f)
			Instantiate (item[0],transform.position,Quaternion.identity);
		else if (itemRand >= 0.9f)
			Instantiate (item[1],transform.position,Quaternion.identity);

		Canvas renderCanvas = GameObject.Find("HUD").GetComponent<Canvas>();
		Text tempTextBox = Instantiate(scorePlusText, transform.position, Quaternion.identity) as Text;
		//Parent to the panel
		tempTextBox.transform.SetParent(renderCanvas.transform, false);
		//Set the text box's text element to the current textToDisplay:
		tempTextBox.text = "+" + result;
		tempTextBox.color = mulColor;

		adjustTextPosition (tempTextBox, renderCanvas);

	}

	void adjustTextPosition(Text t, Canvas c) {
		//this is the ui element
		RectTransform UI_Element = t.rectTransform;

		//first you need the RectTransform component of your canvas
		RectTransform CanvasRect=c.GetComponent<RectTransform>();

		//then you calculate the position of the UI element
		//0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

		Vector2 ViewportPosition=Camera.main.WorldToViewportPoint(this.transform.position);
		Vector2 WorldObject_ScreenPosition=new Vector2(
			((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
			((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));

		//now you can set the position of the ui element
		UI_Element.anchoredPosition=WorldObject_ScreenPosition;
	}

	IEnumerator playSound(float wait) {
		while (true) {
			yield return new WaitForSeconds (Random.Range(wait / 2, wait));

			SoundManagerScript.instance.playRandom (1, enemyClip);
		}
	}

	public IEnumerator playAttack(){
		enemyAnimator.SetTrigger ("Attack");
		yield break;
	}

	public void disableAnimation(){
		enemyAnimator.Stop ();
	}

	public void destroyMata() {
		foreach (Transform child in transform) {
			if(child.gameObject.tag != "Shadow")
				GameObject.Destroy(child.gameObject);
		}
	}
}
