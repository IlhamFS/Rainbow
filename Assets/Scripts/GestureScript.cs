using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 
using PDollarGestureRecognizer;
using UnityEngine.SceneManagement;

public class GestureScript : MonoBehaviour {
	//Our
	public SpriteRenderer[] gesturePlaces;
	public Sprite[] gestureArr;
	public GameController gameController;
	public Animator batAnimator;
	public Animator goatAnimator;
	public Animator bearAnimator;
	public Animator stickAnimator;
	public Animator chipmunkAnimator;
	public GameObject hentakan;
	private List<Sprite> randomGesture;
	public List<int> randomGestureIndex;
	private int[] colorArray = new int[3];
	private string animColor = "";

	public string colorName = "white";
	public bool onboardingAction = false;

	//
	private bool gestureErr = false;
	private Vector3 mouseStart;

	private bool attack = false;

	private Gesture candidate;
	private Result gestureResult;
	private bool checkResult = false;

	//Origin
	public Transform gestureOnScreenPrefab;

	private List<Gesture> trainingSet = new List<Gesture>();

	private List<Point> points = new List<Point>();
	private int strokeId = -1;

	private Vector3 virtualKeyPosition = Vector2.zero;
	private Rect drawArea;

	private RuntimePlatform platform;
	private int vertexCount = 0;

	private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
	private LineRenderer currentGestureLineRenderer;

	private bool recognized;

	public AudioClip[] batClip;
	public AudioClip[] bearClip;
	public AudioClip[] sheepClip;
	public AudioClip attackClip;

	void Start () {

		platform = Application.platform;
		drawArea = new Rect(0, 0, Screen.width, Screen.height);

		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet");
		foreach (TextAsset gestureXml in gesturesXml) {
			trainingSet.Add (GestureIO.ReadGestureFromXML (gestureXml.text));
		}

		//Load user custom gestures
		string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		foreach (string filePath in filePaths){
			trainingSet.Add (GestureIO.ReadGestureFromFile (filePath));
		}
		RenderGesture ();

	}

	void Update () {
		GamePlay ();
		if (Input.GetMouseButtonUp(0)) {
			points.Clear();
			foreach (LineRenderer lineRenderer in gestureLinesRenderer) {

				lineRenderer.SetVertexCount(0);
				Destroy(lineRenderer.gameObject);
			}

			gestureLinesRenderer.Clear();
		}
		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
			if (Input.touchCount > 0) {
				virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			}
		} else {
			if (Input.GetMouseButton(0)) {
				virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			}
		}

		if (drawArea.Contains(virtualKeyPosition)) {
			if (Input.GetMouseButtonDown(0)) {

				if (recognized) {

					recognized = false;
					strokeId = -1;

					points.Clear();
					foreach (LineRenderer lineRenderer in gestureLinesRenderer) {

						lineRenderer.SetVertexCount(0);
						Destroy(lineRenderer.gameObject);
					}

					gestureLinesRenderer.Clear();
				}
				++strokeId;

				Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
				currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

				gestureLinesRenderer.Add(currentGestureLineRenderer);

				vertexCount = 0;
			}

			if (Input.GetMouseButton(0)) {

				points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

				currentGestureLineRenderer.SetVertexCount(++vertexCount);
				currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
			}
		}
	}

	void GamePlay(){

		//Attack jika gesture benar dengan salah satu gesture yang terdapat pada pemain
		if (checkResult == true) {
			if (gestureResult.GestureClass == randomGesture [0].name && gestureResult.Score > 0.7) {
				colorArray [0] = 1;
				gestureResult.GestureClass = "";
				RenderGesture ();
				batAnimator.SetTrigger ("Attack");
				SoundManagerScript.instance.playRandom (2, batClip);
			} else if (gestureResult.GestureClass == randomGesture [1].name && gestureResult.Score > 0.7) {
				colorArray [1] = 1;
				gestureResult.GestureClass = "";
				RenderGesture ();
				goatAnimator.SetTrigger ("Attack");
				SoundManagerScript.instance.playRandom (2, sheepClip);
			} else if (gestureResult.GestureClass == randomGesture [2].name && gestureResult.Score > 0.7) {
				colorArray [2] = 1;
				gestureResult.GestureClass = "";
				RenderGesture ();
				bearAnimator.SetTrigger ("Attack");
				SoundManagerScript.instance.playRandom (2, bearClip);
			}

			colorName = GetColorName (colorArray);
			changeAnim ();
		}

	}
	void changeAnim(){
		if (animColor != colorName) {
			animColor = colorName;
			if (animColor == "white") {
				stickAnimator.SetTrigger ("White");
			}
			else if(animColor == "blue"){
				stickAnimator.SetTrigger ("Cyan");
			}
			else if(animColor == "pink"){
				stickAnimator.SetTrigger ("Pink");
			}
			else if(animColor == "yellow"){
				stickAnimator.SetTrigger ("Yellow");
			}
			else if(animColor == "purple"){
				stickAnimator.SetTrigger ("Purple");
			}
			else if(animColor == "green"){
				stickAnimator.SetTrigger ("Green");
			}
			else if(animColor == "orange"){
				stickAnimator.SetTrigger ("Orange");
			}
			else if(animColor == "brown"){
				stickAnimator.SetTrigger ("Brown");
			}
		}
	}
	void OnGUI() {

		//GUI.Box(drawArea,"Draw Area");
		if(Input.GetMouseButtonDown(0)){
			mouseStart= Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0)) {

			if (mouseStart == Input.mousePosition) {
				gestureErr = true;
				return;
			}
			gestureErr = false;
			recognized = true;

			candidate = new Gesture (points.ToArray ());
			gestureResult = PointCloudRecognizer.Classify (candidate, trainingSet.ToArray ());
			checkResult = true;
		}
	}

	public string GetColorName(int[] colors){
		string color = "";
		for(int i = 0; i < colors.Length ; i++){
			color += colors [i];
		}
		if (color == "000") {
			color = "white";
		} 
		else if(color == "100"){
			color = "blue";
		} 
		else if(color == "010"){
			color = "pink";
		} 
		else if(color == "001"){
			color = "yellow";
		} 
		else if(color == "110"){
			color = "purple";
		} 
		else if(color == "101"){
			color = "green";
		} 
		else if(color == "011"){
			color = "orange";
		} 
		else if(color == "111"){
			color = "brown";
		}
		return color;
	}

	//membuat list random gesture
	List<Sprite> GetRandomGesture (){
		List<Sprite> result = new List<Sprite>();
		randomGestureIndex = new List<int> ();
		bool full = false;
		int i = 0;


		while (full == false) {
			int index;

			if (SceneManager.GetActiveScene ().buildIndex == 2) {
				index = UnityEngine.Random.Range (0, 6);
			} else {
				index = UnityEngine.Random.Range (0, gestureArr.Length);
			}

			Sprite rand = gestureArr[index];

			if(!result.Exists(element => element == rand )){
				result.Add(rand);
				randomGestureIndex.Add (index);
				i++;
			}
			if(i == 3){
				full =  true;
			}

		}
		return result;
	}

	void RenderGesture(){
		randomGesture = GetRandomGesture ();
		int count = 0;
		foreach (SpriteRenderer gest in gesturePlaces) {
			gest.sprite = randomGesture [count];
			count++;
		}


	}

	public void PlayerAttack(){
		Instantiate (hentakan, new Vector3(-3.6f, -3.8f, 0), Quaternion.identity);
		if (SceneManager.GetActiveScene ().buildIndex == 2) {
			if (onboardingAction) {
				//player animation attak
				gameController.killEnemies (colorName);
				stickAnimator.SetTrigger ("Attack");
				chipmunkAnimator.SetTrigger ("Attack");
			}

			stickAnimator.SetTrigger ("Attack");
			//player attack
			colorArray = new int[3];
			colorName = "white";
			return;
		}

		SoundManagerScript.instance.playSingle (2, attackClip);

		//player animation attak
		gameController.killEnemies (colorName);
		stickAnimator.SetTrigger ("Attack");
		chipmunkAnimator.SetTrigger ("Attack");
		//player attack
		colorArray = new int[3];
		colorName = "white";

		SoundManagerScript.instance.playSingle (2, attackClip);
	}
	public void SpecialRainbowAttack(){
		//player animation Special Rainbow
		Instantiate (hentakan, new Vector3(-3.6f, -3.8f, 0), Quaternion.identity);
		gameController.rainbowSpecial ();
		stickAnimator.SetTrigger ("Rainbow");

		colorArray = new int[3];
		colorName = "white";
	}
	public void SpecialTimeAttack(){
		//player animation Special Rainbow
		Instantiate (hentakan, new Vector3(-3.6f, -3.8f, 0), Quaternion.identity);
		gameController.timeSpecial ();
		stickAnimator.SetTrigger ("Time");

		colorArray = new int[3];
		colorName = "white";
	}
	public void CancelAttack(){
		//player attack canceled
		colorArray = new int[3];
		colorName = "white";
	}
}