using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabButton : MonoBehaviour {
	private Color defaultColor;
	public bool notification = false;
	private Image myImage;
	private Image borderImage;
	private Color defaultBorderColor;
	public UIClickAudio uiClickAudio;
	// Use this for initialization
	void Awake () {
		notification = false;
		myImage = gameObject.GetComponent<Image>();
		borderImage = gameObject.transform.Find("Image").GetComponent<Image>();
		defaultColor = myImage.color;
		defaultBorderColor = borderImage.color;
		borderImage.color = Color.clear;
		uiClickAudio = GameObject.Find("UI Click Audio Source").GetComponent<UIClickAudio>();
	}
	
	// Update is called once per frame
	void Update () {
		if (notification){
			myImage.color = Color.Lerp(defaultColor, Color.white, Mathf.PingPong(Time.time*1.3f, .8f));
			borderImage.color = Color.Lerp(Color.clear, defaultBorderColor,Mathf.PingPong(Time.time*1.3f, .8f));
		}
	}

	public void startNotification() {
		notification = true;
		uiClickAudio.PlayNotificationSound();
	}

	public void stopNotification() {
		notification = false;
		myImage.color = defaultColor;
		borderImage.color = Color.clear;
	}

}
