using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
	public controller controller;
	public float buildingDeathWaitTime = 1f;

	[System.Serializable]
	public class LevelBuildingList
	{
		public BuildingPreview[] buildings;
	}
 	public LevelBuildingList[] levelBuildingLists;

    public float[] nextActionTime = new float[] {0.0f,0.0f,0.0f,0.0f,0.0f,0.0f,0.0f};

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("controller").GetComponent<controller>();
		for (int i = 0; i < 7; i++){
            nextActionTime[i] = Time.time + controller.periods[i];
        } 
	}
	
	// Update is called once per frame
	void Update () {
		double sumDamage = 0;
		for (int i = 0; i < 7; i++){
			if (Time.time > nextActionTime[i] ) {
				nextActionTime[i] = Time.time + controller.periods[i];
				sumDamage += controller.units[i+1]*controller.periods[i];
			}
		}
		GameObject enemy = GameObject.FindGameObjectWithTag("enemy");
		if (enemy && sumDamage > 0) {
			House house = enemy.GetComponent<House>();
			house.partnerDamage(sumDamage);
		}
	}
}
