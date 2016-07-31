﻿using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 
using PDollarGestureRecognizer;

public class GestureScript : MonoBehaviour {
	//Our
	private List<Sprite> randomGesture;
	public SpriteRenderer[] gesturePlaces;
	public Sprite[] gestureArr;

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

	void Start () {

		platform = Application.platform;
		drawArea = new Rect(0, 0, Screen.width, Screen.height);

		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

		//Load user custom gestures
		string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		foreach (string filePath in filePaths){
			trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
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
			if (gestureResult.GestureClass == "yolo" && gestureResult.Score > 0.6) {
	

			}
			if(randomGesture.Exists(element => element.name == gestureResult.GestureClass )){
				Debug.Log (gestureResult.GestureClass);
				RenderGesture ();
			}
		}

	}

	void OnGUI() {

		//GUI.Box(drawArea,"Draw Area");
		GUI.Label(new Rect(10, Screen.height - 40, 500, 50), "<color=red>"+message+"</color>");
		if(Input.GetMouseButtonDown(0)){
			mouseStart= Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0)) {

			if(mouseStart == Input.mousePosition){
				gestureErr = true;
				return;
			}
			gestureErr = false;
			recognized = true;

			candidate = new Gesture(points.ToArray());
			gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
			checkResult = true;

		}

	}


	//membuat list random gesture
	List<Sprite> getRandomGesture (){
		List<Sprite> result = new List<Sprite>();
		bool full = false;
		int i = 0;


		while (full == false) {
			Sprite rand = gestureArr[UnityEngine.Random.Range(0,gestureArr.Length)];

			if(!result.Exists(element => element == rand )){
				result.Add(rand);
				i++;
			}
			if(i == 3){
				full =  true;
			}

		}
		return result;
	}
	void RenderGesture(){
		randomGesture = getRandomGesture ();
		int count = 0;
		foreach (SpriteRenderer gest in gesturePlaces) {
			gest.sprite = randomGesture [count];
			count++;
		}
	

	}
}




