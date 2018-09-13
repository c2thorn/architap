using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dustParticle: MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().Play();
		StartCoroutine(endDust());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator endDust() {
		yield return new WaitForSeconds(1.0f);
		Destroy(gameObject);
    }
}
