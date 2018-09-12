using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void StartPointing() {
		StopCoroutine(Pointing());
		StartCoroutine(Pointing());
	}
	

	IEnumerator Pointing() {
		Transform tr = gameObject.transform;
		float offset = 1;
		float timeLimit = 1f;
		while (true) {
			while (timeLimit > 0f){
				tr.position = Vector3.Lerp(tr.position, new Vector3(tr.position.x-offset, tr.position.y-offset, tr.position.z), .01f);
				timeLimit -= 0.01f;
				yield return null;	
			}
			while (timeLimit < 1.0f)
			{
				tr.position = Vector3.Lerp(tr.position, new Vector3(tr.position.x+offset, tr.position.y+offset, tr.position.z), .01f);
				timeLimit += 0.01f;
				yield return null;
			}

		}
    }

}
