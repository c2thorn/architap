using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class BuildingPreview : MonoBehaviour,IPointerClickHandler{
	public int index;
	public BuildingController buildingController;
	// public string buildingName;

	// public double baseHealth;

	public BuildingPreview(){
	}

	void Start(){
		buildingController = GameObject.Find("Building Controller").GetComponent<BuildingController>();
	}

	public void OnPointerClick(PointerEventData eventData)
    {
        // OnClick code goes here ...
		buildingController.ChangeLevel(index);
    }

}
