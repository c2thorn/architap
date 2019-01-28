using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour {
	public bool on = true;
	public GameObject onImage;
	public GameObject offImage;
	public void Toggle() {
		on = !on;
		onImage.SetActive(on);
		offImage.SetActive(!on);
	}
}
