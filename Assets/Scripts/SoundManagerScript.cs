﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManagerScript : MonoBehaviour {
	public AudioSource musicSource;
	public AudioSource sfxSource;
	public AudioSource heroSource;
	public AudioSource batSource;
	public AudioSource sheepSource;
	public AudioSource bearSource;

	private int curAudioSource;

	public static SoundManagerScript instance = null; 
	public Toggle soundToggle;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		int soundTog = PlayerPrefs.GetInt ("sound", 1);
		if (soundTog == 1)
			soundToggle.isOn = true;
		else
			soundToggle.isOn = false;

		getSound ();

	}

	void getSound() {
		if (soundToggle.isOn) {
			musicSource.Play ();
			sfxSource.Play ();
			musicSource.mute = false;
			heroSource.mute = false;
			sfxSource.mute = false;
		} else {
			musicSource.Stop ();
			sfxSource.Stop ();
			musicSource.mute = true;
			heroSource.mute = true;
			sfxSource.mute = true;
		}
	}

	public void setSound() {
		if (soundToggle.isOn)
			PlayerPrefs.SetInt ("sound", 1);
		else
			PlayerPrefs.SetInt ("sound", 0);

		getSound ();
	}

	public void increaseBGMPitch(){
		musicSource.pitch += 0.05f;
	}

	public void increaseVolume(float value){
		heroSource.volume += value;
	}

	public void decreaseVolume(float value){
		heroSource.volume -= value;
	}

	public void playSingle(int source, AudioClip clip){
		if (source == 1) {
			if (sfxSource.isPlaying)
				sfxSource.Stop ();

			sfxSource.clip = clip;
			sfxSource.pitch = 1.0f;
			sfxSource.Play ();
		} else if (source == 2) {
			if (heroSource.isPlaying)
				heroSource.Stop ();

			heroSource.clip = clip;
			heroSource.pitch = 1.0f;
			heroSource.Play ();
		}
	}

	public void playRandom(int source, AudioClip[] clip){
		if (source == 1) {
			if (!sfxSource.isPlaying) {
				int clipIndex = Random.Range (0, clip.Length);
				float pitch = Random.Range (0.95f, 1.05f);
				sfxSource.clip = clip[clipIndex];
				sfxSource.pitch = pitch;
				sfxSource.Play ();
			}
		} else if (source == 2) {
			if (!heroSource.isPlaying) {
				int clipIndex = Random.Range (0, clip.Length);
				float pitch = Random.Range (0.95f, 1.05f);
				heroSource.clip = clip[clipIndex];
				heroSource.pitch = pitch;
				heroSource.Play ();
			}
		} else if (source == 3) {
			if (!batSource.isPlaying) {
				int clipIndex = Random.Range (0, clip.Length);
				float pitch = Random.Range (0.95f, 1.05f);
				batSource.clip = clip[clipIndex];
				batSource.pitch = pitch;
				batSource.Play ();
			}
		} else if (source == 4) {
			if (!sheepSource.isPlaying) {
				int clipIndex = Random.Range (0, clip.Length);
				float pitch = Random.Range (0.95f, 1.05f);
				sheepSource.clip = clip[clipIndex];
				sheepSource.pitch = pitch;
				sheepSource.Play ();
			}
		} else if (source == 5) {
			if (!bearSource.isPlaying) {
				int clipIndex = Random.Range (0, clip.Length);
				float pitch = Random.Range (0.95f, 1.05f);
				bearSource.clip = clip[clipIndex];
				bearSource.pitch = pitch;
				bearSource.Play ();
			}
		}
	}
}
