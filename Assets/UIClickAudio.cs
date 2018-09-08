using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickAudio : MonoBehaviour {

	public SoundController soundController;
	public AudioSource audioSource;
	//public AudioClip[] clickSounds;
	
	public float defaultVolume = 0.2f;
	public float notificationVolume = 0.25f;

	public AudioClip navigateClip;
	public AudioClip tabClip;
	public AudioClip clickClip;
	public AudioClip notificationSound;

	public void navigateSound() {
		audioSource.volume = defaultVolume;
		audioSource.clip = navigateClip;
		if (!soundController.soundMute)
			audioSource.Play();
	}

	public void tabSound() {
		audioSource.volume = defaultVolume;
		audioSource.clip = tabClip;
		if (!soundController.soundMute)
			audioSource.Play();
	}

	public void clickSound() {
		audioSource.volume = defaultVolume;
		audioSource.clip = clickClip;
		if (!soundController.soundMute)
			audioSource.Play();
	}

	public void PlayNotificationSound() {
		audioSource.volume = notificationVolume;
		audioSource.clip = notificationSound;
		if (!soundController.soundMute)
			audioSource.Play();
	}
}
