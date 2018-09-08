using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour {
	public SoundController soundController;
	public AudioSource audioSource;
	public AudioClip[] levelUpSounds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void levelUpSound() {
		int index = UnityEngine.Random.Range(0, levelUpSounds.Length);
        audioSource.clip = levelUpSounds[index];
		if (!soundController.soundMute)
			audioSource.Play();
	}
}
