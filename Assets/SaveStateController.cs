using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveStateController : MonoBehaviour {

	public controller controller;

	public void LoadData() {
		controller.totalBuildings = PlayerPrefs.GetString("totalBuildings").Length > 0 ? double.Parse(PlayerPrefs.GetString("totalBuildings")) : 0;
	}

	public void SaveData() {
		PlayerPrefs.SetString("totalBuildings", controller.totalBuildings +"");
	}

	public void DeleteAll() {
		PlayerPrefs.DeleteAll();
	}
}
