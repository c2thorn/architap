using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour {

	public Dictionary<string,bool> skillsBought = new Dictionary<string,bool>(){
		{"autoClick",false},
		{"clickBoost",false},
		{"partnerBoost",false},
		{"goldBoost",false},
		{"criticalClickChanceBoost",false},
		{"goldHouseChanceBoost",false},
		{"buildingReduction",false},
		{"cooldownReduction",false}
	};
	
	public Dictionary<string,double> skillEffect = new Dictionary<string,double>(){
		{"autoClick",0.1},
		{"clickBoost",2},
		{"partnerBoost",2},
		{"goldBoost",2},
		{"criticalClickChanceBoost",0.5},
		{"goldHouseChanceBoost",0.5},
		{"buildingReduction",1},
		{"cooldownReduction",300}
	};

	public Dictionary<string,float> skillDuration = new Dictionary<string,float>(){
		{"autoClick",8f},
		{"clickBoost",10f},
		{"partnerBoost",10f},
		{"goldBoost",20f},
		{"criticalClickChanceBoost",20},
		{"goldHouseChanceBoost",20},
		{"buildingReduction",60}
	};

	public Dictionary<string,int> skillCooldown = new Dictionary<string,int>(){
		{"autoClick",0},
		{"clickBoost",0},
		{"partnerBoost",0},
		{"goldBoost",0},
		{"criticalClickChanceBoost",0},
		{"goldHouseChanceBoost",0},
		{"buildingReduction",0},
		{"cooldownReduction",0}
	};
	public Dictionary<string,int> skillCooldownStartTime = new Dictionary<string,int>(){
		{"autoClick",480},
		{"clickBoost",600},
		{"partnerBoost",600},
		{"goldBoost",600},
		{"criticalClickChanceBoost",600},
		{"goldHouseChanceBoost",900},
		{"buildingReduction",600},
		{"cooldownReduction",600}
	};
	public Dictionary<string,bool> skillFlag = new Dictionary<string,bool>(){
		{"autoClick",false},
		{"clickBoost",false},
		{"partnerBoost",false},
		{"goldBoost",false},
		{"criticalClickChanceBoost",false},
		{"goldHouseChanceBoost",false},
		{"buildingReduction",false},
		{"cooldownReduction",false}
	};

	public Dictionary<string,Button> skillButtons = new Dictionary<string,Button>() {
		{"autoClick",null},
		{"clickBoost",null},
		{"partnerBoost",null},
		{"goldBoost",null},
		{"criticalClickChanceBoost",null},
		{"goldHouseChanceBoost",null},
		{"buildingReduction",null},
		{"cooldownReduction",null}
	};
	public Dictionary<string,Text> skillText = new Dictionary<string,Text>() {
		{"autoClick",null},
		{"clickBoost",null},
		{"partnerBoost",null},
		{"goldBoost",null},
		{"criticalClickChanceBoost",null},
		{"goldHouseChanceBoost",null},
		{"buildingReduction",null},
		{"cooldownReduction",null}
	};

	// Use this for initialization
	void dooly () {
		InvokeRepeating("DecrementCooldowns", Time.time, 1f);
		List<string> keys = new List<string>();
		foreach(KeyValuePair<string,Button> item in skillButtons) {
			keys.Add(item.Key);
		}
		for (int i = 0; i < keys.Count;i++) {
			string key = keys[i];
			// var name = "AutoClickButton";
			Debug.Log(key);
			Debug.Log(GameObject.FindGameObjectWithTag("Player").name);
			Button button = GameObject.Find(key).GetComponent<Button>();
			skillButtons[key] = button;
			skillText[key] = GameObject.Find(key+"CooldownText").GetComponent<Text>();
			skillText[key].text = NumberFormat.format(skillCooldown[key]);
		}
		// foreach (KeyValuePair<string,Button> item in skillButtons){
		// 	var name = "AutoClickButton";
		// 	Debug.Log(name);
		// 	Debug.Log(GameObject.FindGameObjectWithTag("Player").name);
		// 	Button button = GameObject.Find(name).GetComponent<Button>();
		// 	skillButtons[item.Key] = button;
		// 	skillText[item.Key] = GameObject.Find(item.Key+"CooldownText").GetComponent<Text>();
		// 	skillText[item.Key].text = NumberFormat.format(skillCooldown[item.Key]);
		// }
		//TODO calculate skills cooldown from time

		// CheckIfSkillsBought();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckIfSkillsBought(){
		foreach (KeyValuePair<string,Button> item in skillButtons){
			skillButtons[item.Key].gameObject.SetActive(skillsBought[item.Key]);
			skillButtons[item.Key].interactable = skillCooldown[item.Key] == 0;
			skillText[item.Key].gameObject.SetActive(skillsBought[item.Key]);
		}
	}

	public void activateSkill(string key) {
		dooly();
		// if (!skillFlag[key]) {
		// 	skillCooldown[key] = skillCooldownStartTime[key];
		// 	skillButtons[key].interactable = false;
		// 	switch (key) {
		// 		case "autoClick":
		// 			StartCoroutine(AutoClickForDuration());
		// 			break;
		// 	}
		// }
	}

	// public void autoClickSkill() {
	// 	if (!skillFlag["autoClick"]) {
	// 		StartCoroutine(AutoClickForDuration());
	// 		skillCooldown["autoClick"] = skillCooldownStartTime["autoClick"];
	// 		skillButtons["autoClick"].interactable = false;
	// 	}
	// }

	protected IEnumerator AutoClickForDuration() {
		float autoClickRate = (float)skillEffect["autoClick"];
		int numberClicks = Mathf.FloorToInt(skillDuration["autoClick"]/autoClickRate);
		skillFlag["autoClick"] = true;
		for (int i = 0; i < numberClicks; i++){
			GameObject.FindGameObjectWithTag("enemy").GetComponent<House>().autoClick();
			yield return new WaitForSeconds(autoClickRate);
		}
		skillFlag["autoClick"] = false;
    }

	public void DecrementCooldowns() {
		foreach (KeyValuePair<string,int> item in skillCooldown) {
			if (skillCooldown[item.Key] > 0) {
				skillCooldown[item.Key]--;
				skillText[item.Key].text = NumberFormat.format(skillCooldown[item.Key]);
				if (skillCooldown[item.Key] <= 0) {
					skillButtons[item.Key].interactable = true;
					skillCooldown[item.Key] = 0;
				}
			}
		}
	}


}
