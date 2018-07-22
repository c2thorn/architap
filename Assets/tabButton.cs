using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabButton : MonoBehaviour {
	private Color defaultColor;
	public bool notification = false;
	private Image myImage;
	// Use this for initialization
	void Start () {
		notification = false;
		myImage = gameObject.GetComponent<Image>();
		defaultColor = myImage.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (notification)
			myImage.color = Color.Lerp(defaultColor, Color.white, Mathf.PingPong(Time.time*1.5f, .85f));
	}

	public void startNotification() {
		notification = true;
	}

	public void stopNotification() {
		notification = false;
		myImage.color = defaultColor;
	}

}
