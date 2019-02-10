using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	}

	public void RemoveHousePointer() {
		pointer.gameObject.SetActive(false);
	}
}
