using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public bool soundMute = false;
	public bool musicMute = false;
	public AudioSource musicAudioSource;

	public void MuteSound() {
		soundMute = !soundMute;
	}

	public void MuteMusic() {
		musicMute = !musicMute;
		musicAudioSource.volume = musicMute ? 0 : 0.25f;
	}
}
