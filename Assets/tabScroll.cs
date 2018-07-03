using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabScroll : MonoBehaviour {

	public ScrollRect scrollrect;
	public Image scrollArea;
	public GameObject scrollBar;
	// Use this for initialization
	void Start () {
		disable();
	}

	public void enable() {
		scrollrect.horizontal = true;
		scrollBar.active = true;
		scrollArea.enabled = true;
	}

	public void disable() {
		scrollrect.horizontal = false;
		scrollBar.active = false;
		scrollArea.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
