using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManagerScript : MonoBehaviour {
	public AudioSource musicSource;
	public AudioSource sfxSource;
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
		} else {
			musicSource.Stop ();
			sfxSource.Stop ();
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

	public void playSingle(AudioClip clip){
		if (sfxSource.isPlaying)
			sfxSource.Stop ();

		sfxSource.clip = clip;
		sfxSource.Play ();
	}

	public void playRandom(AudioClip[] clip){
		if (!sfxSource.isPlaying) {
			int clipIndex = Random.Range (0, clip.Length);
			float pitch = Random.Range (0.95f, 1.05f);
			sfxSource.clip = clip[clipIndex];
			sfxSource.pitch = pitch;
			sfxSource.Play ();
		}
	}
}
