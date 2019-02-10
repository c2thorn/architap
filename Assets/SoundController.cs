using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public bool soundMute = false;
	public bool musicMute = false;
	public AudioSource musicAudioSource;
	public SaveStateController saveStateController;

	public ToggleButton soundToggle;
	public ToggleButton musicToggle;

	public void MuteSound() {
		soundMute = !soundMute;
		saveStateController.SaveData();
		soundToggle.Toggle();
	}

	public void MuteMusic() {
		musicMute = !musicMute;
		musicAudioSource.volume = musicMute ? 0 : 0.25f;
		saveStateController.SaveData();
		musicToggle.Toggle();
	}
}
