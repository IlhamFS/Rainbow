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
}
