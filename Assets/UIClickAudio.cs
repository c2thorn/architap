using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickAudio : MonoBehaviour {
	public AudioSource audioSource;
	//public AudioClip[] clickSounds;
	
	public AudioClip navigateClip;
	public AudioClip tabClip;
	public AudioClip clickClip;

	public void navigateSound() {
        audioSource.clip = navigateClip;
        audioSource.Play();
	}

	public void tabSound() {
        audioSource.clip = tabClip;
        audioSource.Play();
	}

	public void clickSound() {
        audioSource.clip = clickClip;
        audioSource.Play();
	}
}
