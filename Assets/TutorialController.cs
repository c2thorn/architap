using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using System;

public class TutorialController : MonoBehaviour {

	public PointerArrow pointer;

	// Use this for initialization
	void Start () {
		//PointAtHouse();
		//RemovePointer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PointAtHouse() {
		pointer.gameObject.SetActive(true);
		pointer.transform.position = new Vector3(2,2,-12);
		pointer.StartPointing();

      // Log a tutorial begin event with no parameters.
      Debug.Log("Logging a tutorial begin event.");
      FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventTutorialBegin);
    }

	public void RemovePointer() {
		pointer.gameObject.SetActive(false);
	}
}
