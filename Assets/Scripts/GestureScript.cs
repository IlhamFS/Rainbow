My Drive
Details
Activity
TODAY

Muhammad Luthfiuploaded an item
2:40 PM
Text
GestureScript.cs

Nayana Tuploaded 6 items
2:27 PM
Image
Menu-23.png
Image
Menu-24.png
Image
Menu-25.png
Image
Menu-26.png
Image
Menu-22.png
Image
Menu-21.png

Firda Dhea Sauzanuploaded 2 items
11:53 AM
Word
Laporan.docx
PDF
Laporan Rancangan Kegiatan Ristek Fasilkom UI 1.0.pdf

Nayana Tedited an item
11:41 AM
Google Docs
Font Guide

Nayana Tuploaded 8 items
11:41 AM
Image
Menu-12.png
Image
Menu-20.png
Image
Menu-17.png
Image
Menu-18.png
Image
Menu-15.png
Image
Menu-16.png
Show all...

Riza Anjari Putrishared an item
10:50 AM
Google Sheets
List Email Marketing
T
Can edit
Timotius Kevin Levandi

Riza Anjari Putriedited an item
10:50 AM
Google Sheets
List Email Marketing

Nayana Tedited an item
10:36 AM
Google Sheets
Backlog

Nayana Tuploaded an item
10:31 AM
Image
Menu-13.png

Nayana Tcreated an item
10:30 AM
Google Docs
Font Guide

Nayana Tuploaded 15 items
10:29 AM
Image
Menu-11.png
Unknown File
NOVITONOVA-REGULAR.OTF
Unknown File
NOVITONOVA-THIN.OTF
Unknown File
RUNAWAY.OTF
Image
Menu back-01.png
Image
Menu-09.png
Show all...

Muhammad Firza Pratamaedited an item
10:25 AM
Google Docs
Laporan

Nayana Tcreated and shared an item in
10:19 AM
Google Drive Folder
Assets
Google Drive Folder
menu

Can edit
Muhammad Azmi

Can edit
Muhammad Luthfi

Can edit
You

Muhammad Firza Pratamaedited an item
8:30 AM
Google Docs
Laporan
YESTERDAY

Muhammad Firza Pratamaedited an item
Wed 7:55 PM
Google Docs
Laporan

You and 3 otherscommented on an item
Wed 6:17 PM
Google Docs
Laporan

You and 3 othersedited an item
Wed 6:14 PM
Google Docs
Laporan

Muhammad Luthfiuploaded 2 items
Wed 5:16 PM
Binary File
enemy_pink_attack.anim
Binary File
Enemy Controller.controller

Muhammad Luthficreated and shared an item in
Wed 5:16 PM
Google Drive Folder
[IGI Compfest] Muffin
Google Drive Folder
buat feti

Can edit
Muhammad Azmi

Can edit
You

Can edit
Nayana T

Mohammad Awwaab Abdul Malik and Muhammad Firza Pratamaedited an item
Wed 5:15 PM
Google Docs
Laporan


﻿using UnityEngine;
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

	private List<Sprite> randomGesture;
	public List<int> randomGestureIndex;
	private int[] colorArray = new int[3];
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

	private string message;
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
			string player = "";
			if (gestureResult.GestureClass == randomGesture [0].name && gestureResult.Score > 0.5) {
				colorArray [0] = 1;
				gestureResult.GestureClass = "";
				RenderGesture ();
				batAnimator.SetTrigger ("Attack");
				SoundManagerScript.instance.playRandom (2, batClip);
			} else if (gestureResult.GestureClass == randomGesture [1].name && gestureResult.Score > 0.6) {
				colorArray [1] = 1;
				gestureResult.GestureClass = "";
				RenderGesture ();
				goatAnimator.SetTrigger ("Attack");
				SoundManagerScript.instance.playRandom (2, sheepClip);
			} else if (gestureResult.GestureClass == randomGesture [2].name && gestureResult.Score > 0.6) {
				colorArray [2] = 1;
				gestureResult.GestureClass = "";
				RenderGesture ();
				bearAnimator.SetTrigger ("Attack");
				SoundManagerScript.instance.playRandom (2, bearClip);
			}

			colorName = GetColorName (colorArray);
			message = colorName;
		}

	}

	void OnGUI() {

		//GUI.Box(drawArea,"Draw Area");
		GUI.Label(new Rect(10, Screen.height - 40, 500, 50), "<color=red>"+message+"</color>");
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
			int index = UnityEngine.Random.Range (0, gestureArr.Length);
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
		if (SceneManager.GetActiveScene ().buildIndex == 2 && !onboardingAction) {
			colorArray = new int[3];
			colorName = "white";
			return;
		}
		//player animation attak
		gameController.killEnemies (colorName);

		//player attack
		colorArray = new int[3];
		colorName = "white";

		SoundManagerScript.instance.playSingle (2, attackClip);
	}
	public void SpecialRainbowAttack(){
		//player animation Special Rainbow
		gameController.rainbowSpecial ();

		colorArray = new int[3];
		colorName = "white";
	}
	public void SpecialTimeAttack(){
		//player animation Special Rainbow
		gameController.timeSpecial ();

		colorArray = new int[3];
		colorName = "white";
	}
	public void CancelAttack(){
		//player attack canceled
		colorArray = new int[3];
		colorName = "white";
	}
}



