using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAudioSource : MonoBehaviour {
	public SoundController soundController;
    public AudioClip[] hitClips;
    public AudioSource buildingCompleteAudioSource;
	public AudioSource clickAudioSource;
	public AudioClip buildingCompleteClip;
	// Use this for initialization
	void Start () {
        // clickAudioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
    public void clickSound() {
        int index = UnityEngine.Random.Range(0, hitClips.Length);
        clickAudioSource.clip = hitClips[index];
		if (!soundController.soundMute)
			clickAudioSource.Play();
    }

	public void PlayBuildingComplete() {
		buildingCompleteAudioSource.clip = buildingCompleteClip;
		if (!soundController.soundMute)
			buildingCompleteAudioSource.Play();
	}
}
