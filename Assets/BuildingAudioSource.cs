using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAudioSource : MonoBehaviour {
	public SoundController soundController;
    public AudioClip[] hitClips;
    public AudioSource audioSource;
	public AudioClip buildingCompleteClip;
	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
    public void clickSound() {
        int index = UnityEngine.Random.Range(0, hitClips.Length);
        audioSource.clip = hitClips[index];
		if (!soundController.soundMute)
			audioSource.Play();
    }

	public void PlayBuildingComplete() {
		audioSource.clip = buildingCompleteClip;
		if (!soundController.soundMute)
			audioSource.Play();
	}
}
