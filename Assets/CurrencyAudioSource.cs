using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyAudioSource : MonoBehaviour {
    public AudioSource audioSource;
	public AudioClip coinClip;
	public SoundController soundController;

	public AudioSource[] currencySources;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();	
	}

	public void PlayCoinSound() {
		if (!soundController.soundMute){
			for (int i = 0; i < currencySources.Length;i++) {
				if (!currencySources[i].isPlaying){
					currencySources[i].clip = coinClip;
					currencySources[i].Play();
					break;
				}
			}
		}
    }
}
